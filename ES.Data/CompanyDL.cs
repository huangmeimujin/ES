using ES.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace ES.Data
{
    public class CompanyDL : ICompanyDL
    {
        public IQueryable<Company> LoadPageItems(int pageSize, int pageIndex, out int total)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            using (var dbcontext=new ESContext())
            {
                total = dbcontext.Company.Count();
                var temp = dbcontext.Company.AsNoTracking()
                                .OrderBy(o=>o.Id)
                                .Skip(pageSize * (pageIndex - 1))
                                .Take(pageSize);
                return temp.AsQueryable();
            }  
        }
    }
}
