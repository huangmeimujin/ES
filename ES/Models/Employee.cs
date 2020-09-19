using System;
using System.Collections.Generic;
using System.Text;

namespace ES.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsManager { get; set; }
        public DateTime OfficeHours { get; set; }
        public Company Company { get; set; }

        public List<Skill> Skills { get; set; }

    }

    public  class Company
    {
        public string Name { get; set; }
        public DateTime HeadOfficeHours { get; set; }
        public int Id { get; set; }
    }

    public  class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Proficiency { get; set; }
    }
}
