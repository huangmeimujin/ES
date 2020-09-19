using System;
using System.Collections.Generic;

namespace ES.Data.Models
{
    public partial class Skill: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Proficiency { get; set; }
    }
}
