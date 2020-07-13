using LocationService;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESService.Repositorys
{
    /// <summary>
    /// 索引操作
    /// </summary>
    public class IndexOperation
    {
        private IElasticClient B2BElasticClient;
        public IndexOperation(IElasticClient elasticClient)
        {
            this.B2BElasticClient = elasticClient;
        }

        public bool CreateIndex<T>(string indexName) where T:class
        {
            var existsResponse = B2BElasticClient.Indices.Exists(indexName);
            if (existsResponse.Exists)
            {
                return true;
            }
            IIndexState indexState = new IndexState
            {
                Settings = new IndexSettings
                {
                    NumberOfReplicas=1,//副本数
                    NumberOfShards=2,//分片数
                }
            };
            CreateIndexResponse response = B2BElasticClient.Indices.Create(indexName,p=>p.InitializeUsing(indexState).Map<T>(m=>m.AutoMap()));

            return response.IsValid;

        }

        public bool BulkAll<T>(string indexName, IEnumerable<T> docs) where T:class
        {
            const int size = 1000;
            var tokenSource = new CancellationTokenSource();

            var observableBulk = B2BElasticClient.BulkAll(docs, b => b
                .Index(indexName)
                .BackOffTime(TimeSpan.FromSeconds(10))
                .BackOffRetries(2)
                .RefreshOnCompleted()
                .MaxDegreeOfParallelism(Environment.ProcessorCount)
                .Size(size)
                .BufferToBulk((r, buffer) => r.IndexMany(buffer)), tokenSource.Token
            );
            var countdownEvent = new CountdownEvent(1);

            Exception exception = null;

            void OnCompleted()
            {
                Logger.Info("BulkAll Finished");
                countdownEvent.Signal();
            }
            var bulkAllObserver = new BulkAllObserver(
                onNext: response =>
                {
                    Logger.Info($"Indexed {response.Page * size} with {response.Retries} retries");
                },
                onError: ex =>
                {
                    Logger.Info("BulkAll Error : {0}", ex);
                    exception = ex;
                    countdownEvent.Signal();
                },
                OnCompleted);

            observableBulk.Subscribe(bulkAllObserver);

            countdownEvent.Wait(tokenSource.Token);

            if (exception != null)
            {
                Logger.Info("BulkHotelGeo Error : {0}", exception);
                return false;
            }
            else
            {
                return true;
            }
        }




    }
}
