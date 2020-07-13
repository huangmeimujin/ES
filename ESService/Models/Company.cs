using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESService.Models
{
    [ElasticsearchType(RelationName ="company")]
    public class Company
    {
        [Keyword(NullValue = "null", Similarity = "BM25")]
        public string Name { get; set; }

        [Text(Name = "office_hours")]
        public TimeSpan? HeadOfficeHours { get; set; }

        [Object(Store = false)]
        public List<Employee> Employees { get; set; }
    }
}
