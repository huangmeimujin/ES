using ES.Attributes;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES
{
    public abstract class BaseWriter<T>where T:class
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ElasticClient Conn = null;

        /// <summary>
        /// 
        /// </summary>
        private static readonly int BatchCount = 5000;

        public ESMeta Meta { get; }

        static BaseWriter()
        {
            var meta = ESMeta.Get<T>();
            if (meta.AutoMap)
            {
                var elasticClient = ESConfigHelper.GetElasticClient(meta.Server);
                var rsp = elasticClient.Indices.Exists(meta.Index);
                if (!rsp.Exists)
                {
                    var createIndexResponse = elasticClient.Indices.Create(meta.Index, c => 
                    c.Map<T>(m => m.AutoMap())
                    .Settings(s => s
                            .NumberOfShards(5)
                            .NumberOfReplicas(2)
                            .NumberOfRoutingShards(5)
                            )
                    );
                }
            }
        }

        public BaseWriter()
        {
            var meta = ESMeta.Get<T>();
            this.Conn = ESConfigHelper.GetElasticClient(meta.Server);
        }

        /// <summary>
        /// 要写入的数据源
        /// </summary>
        /// <returns></returns>

        public abstract Task<IEnumerable<T>> GetDatas(int pagesize, int pagecount);
       


        /// <summary>
        /// 获取总共要写入的总条数
        /// </summary>
        /// <returns></returns>
        public abstract Task<int> GetTotalAsync();

        public async Task Write()
        {
            var tasks = new List<Task>();
            var total =await this.GetTotalAsync();
            for (int i=0;i<total;i+= BatchCount)
            {
                var docs =await this.GetDatas(i, BatchCount);
                var task = this.BatchSave(docs);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks.ToArray());
        }

        public async Task<BulkResponse> BatchSave(IEnumerable<T> docs)
        {
            if (docs==null || docs.Count()==0)
            {
                return null;
            }
            var db = new BulkDescriptor();
            db.Index(this.Meta.Index);
            var bulkIndexResponse = await this.Conn.BulkAsync(b =>db);
            return bulkIndexResponse;
        }



    }
}
