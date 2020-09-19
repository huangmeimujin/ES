using System;
using System.Collections.Generic;
using System.Text;

namespace ES
{
    public class Servers
    {
        public string Name { get; private set; }

        public Servers(string name)
        {
            this.Name = name;
        }
        public const string B2C = "B2C";
        public const string ESTest = "ESTest";
    }
}
