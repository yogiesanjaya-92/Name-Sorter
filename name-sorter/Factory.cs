using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace name_sorter
{
    public static class Factory
    {   
        public static List<IPerson> CreateListPerson()
        {
            return new List<IPerson>();
        }

        public static List<IPerson> AddPersonToList(List<IPerson> listAddPerson, string GivenName, string LastName)
        {
            listAddPerson.Add(new Person() { GivenName = GivenName, LastName = LastName });
            return listAddPerson;
        }
    }
}
