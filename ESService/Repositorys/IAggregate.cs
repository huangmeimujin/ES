using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESService.Repositorys
{
    public interface IAggregate
    {
        public void BaseAggregation(string indexName);

        public void Aggregations(string indexName);

        public void AnalysisAggregationsResult(string indexName);
    }
}
