using System;
using System.Collections.Generic;
using System.Text;

namespace ES.Data.Models
{
    public class BaseEntity
    {
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }
        public string Last_Updated_By { get; set; }
        public DateTime Last_Update_Date { get; set; }
    }
}
