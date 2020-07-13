using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESService.Repositorys
{
    public abstract class BaseSearch : ISearch
    {
        public void ManyWhereSearch(string indexName)
        {
            throw new NotImplementedException();
        }

        public void Search(string indexName)
        {
            throw new NotImplementedException();
        }

        public void SearchAfter(string indexName)
        {
            throw new NotImplementedException();
        }

        public void SearchScanScroll(string indexName)
        {
            throw new NotImplementedException();
        }

        public void SearchSetBoost(string indexName)
        {
            throw new NotImplementedException();
        }
    }
}
