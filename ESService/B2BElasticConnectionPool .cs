using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESService
{
    public class B2BElasticConnectionPool: SniffingConnectionPool
    {
        public B2BElasticConnectionPool(IOptions<ElasticSearchConfig> options) : base(options.Value.Uris.Select(uri => new Uri(uri)))
        {
        }
    }
}
