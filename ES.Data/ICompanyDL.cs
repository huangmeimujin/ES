using ES.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ES.Data
{
    public interface ICompanyDL
    {
        public IQueryable<Company> LoadPageItems(int pageSize, int pageIndex, out int total);
    }
}
