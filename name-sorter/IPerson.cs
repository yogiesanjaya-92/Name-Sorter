using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace name_sorter
{
    public interface IPerson
    {
        string GivenName { get; set; }
        string LastName { get; set; }
    }
}
