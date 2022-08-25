using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace name_sorter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var filePath = getFilePath(args.FirstOrDefault());
                var unsortedName = getListName(filePath);
                var sortedName = sortListName(unsortedName);
                exportSortedName(sortedName);
            }
            catch (Exception except)
            {
                Console.WriteLine(string.Format("{0}: {1}", except.GetType().Name, except.Message));
            }
        }

        public static string getFilePath(string commandArgument)
        {
            var startupPath = Assembly.GetExecutingAssembly().Location;
            startupPath = startupPath.Substring(0, startupPath.LastIndexOf('\\'));
            return Path.Combine(startupPath, commandArgument);
        }

        public static List<IPerson> getListName(string filePath)
        {
            var listPerson = Factory.CreateListPerson();
            string[] listName = File.ReadAllLines(filePath);
            foreach (string fullname in listName)
            {
                var names = fullname.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Factory.AddPersonToList(listPerson, string.Join(" ", names.Take(names.Count() - 1)), names.LastOrDefault());
            }
            return listPerson;
        }

        public static List<IPerson> sortListName(List<IPerson> listName)
        {
            return listName.OrderBy(x => x.LastName).ThenBy(y => y.GivenName).ToList();
        }

        public static void exportSortedName(List<IPerson> sortedName)
        {
            var sortedNameArr = sortedName.Select(x => string.Format("{0} {1}", x.GivenName, x.LastName)).ToArray();
            File.WriteAllLines("sorted-names-list.txt", sortedNameArr);
            foreach (string name in sortedNameArr)
            {
                Console.WriteLine(name);
            }
        }
    }
}
