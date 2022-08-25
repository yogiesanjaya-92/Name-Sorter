using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace name_sorter
{
    public class Person : IPerson
    {
        public string GivenName { get; set; }
        public string LastName { get; set; }
    }
}
