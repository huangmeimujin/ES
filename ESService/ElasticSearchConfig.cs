using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESService
{
    public class ElasticSearchConfig
    {
        public IEnumerable<string> Uris { get; set; }
    }
}
