using ES.Data;
using ES.Data.Models;
using ESService.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ESService.Controllers
{
    public class IndexController : Controller
    {
        private IElasticClient B2BElasticClient;
        private ICompanyDL CompanyDL;
        public IndexController(IElasticClient elasticClient,ICompanyDL companyDL)
        {
            this.B2BElasticClient = elasticClient;
            this.CompanyDL = companyDL;
        }
        public IActionResult Index()
        {
            bool result = false;
            const int numberOfCycles = 10000;

            var sw = Stopwatch.StartNew();
            IndexOperation indexOperation = new IndexOperation(this.B2BElasticClient);
            var indexName = nameof(Company).ToLower();//索引名称小写
            var createSuccess = indexOperation.CreateIndex<Company>(indexName);
            if (createSuccess)
            {
                int total = 0;
                var list = this.CompanyDL.LoadPageItems(0, numberOfCycles, out total).ToList();
                result = indexOperation.BulkAll(indexName, list);
                 
            }
            sw.Stop();
            return this.Ok(result);

            
        }
    }
}