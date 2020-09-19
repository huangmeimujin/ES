using System;
using System.Collections.Generic;

namespace ES.Data.Models
{
    public partial class Company: BaseEntity
    {
        public string Name { get; set; }
        public DateTime HeadOfficeHours { get; set; }
        public int Id { get; set; }
    }
}
