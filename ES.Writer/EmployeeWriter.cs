using ES.Data;
using ES.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ES.Writer
{
    public class EmployeeWriter : BaseWriter<Employee>
    {
        public DateTime? Start { get; set; }
        public EmployeeWriter(DateTime? start)
        {
            this.Start = start;
        }

        private IQueryable<ES.Data.Models.Employee> GetSource(ESContext db,DateTime? start )
        {
            var query = db.Employee.AsNoTracking();
            if (start.HasValue)
            {
                query = query.Where(q=>q.Last_Update_Date>=start || q.Creation_Date>=start);
            }
            return query;
        }
       
        public override async Task<IEnumerable<Employee>> GetDatas(int pagesize,int pagecount)
        {
            var opt = new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted };
            using (var scope = new TransactionScope(TransactionScopeOption.Required, opt, TransactionScopeAsyncFlowOption.Enabled))
            using (var db = new ESContext())
            {

                var source =await this.GetSource(db, this.Start)
                    .OrderBy(l => l.Id)
                    .Skip(pagesize * pagecount)
                    .Take(pagecount)
                    .ToListAsync();

                scope.Complete();

                return source.Select(s => new Employee()
                {
                         FirstName=s.FirstName,
                         LastName=s.LastName,
                         Salary=s.Salary,
                         Birthday=s.Birthday,
                         IsManager=s.IsManager,
                         OfficeHours=s.OfficeHours
                });
            }
        }

        public override Task<int> GetTotalAsync()
        {
            using (var db=new ESContext())
            {
                return this.GetSource(db,this.Start).CountAsync();
            }
                
        }
    }
}
