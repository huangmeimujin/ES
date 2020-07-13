using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESService
{
    public class B2BElasticClient:ElasticClient
    {
        public B2BElasticClient(IConnectionPool pool) : base(new ConnectionSettings(pool))
        {

        }
    }
}
