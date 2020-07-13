using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESService.Repositorys
{
    public interface ISearch
    {
        public void Search(string indexName);

        public void ManyWhereSearch(string indexName);

        public void SearchScanScroll(string indexName);

        public void SearchSetBoost(string indexName);

        public void SearchAfter(string indexName);
    }
}
