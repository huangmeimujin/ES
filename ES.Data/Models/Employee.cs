using System;
using System.Collections.Generic;

namespace ES.Data.Models
{
    public partial class Employee:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsManager { get; set; }
        public DateTime OfficeHours { get; set; }
        public int Id { get; set; }

        public int Company { get; set; }




    }
}
