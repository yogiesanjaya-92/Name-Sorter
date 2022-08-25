using Microsoft.VisualStudio.TestTools.UnitTesting;
using name_sorter;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace name_sorter_test
{
    [TestClass]
    public class UnitTest
    {
        public string startupPath;
        public List<IPerson> listUnsortedPerson;
        public List<IPerson> listSortedPerson;

        [TestInitialize]
        public void Initialize()
        {
            startupPath = Assembly.GetExecutingAssembly().Location;
            startupPath = startupPath.Substring(0, startupPath.LastIndexOf('\\'));

            listUnsortedPerson = Factory.CreateListPerson();
            Factory.AddPersonToList(listUnsortedPerson, "Anne Marie", "Jeane");
            Factory.AddPersonToList(listUnsortedPerson, "Michael", "Carey");
            Factory.AddPersonToList(listUnsortedPerson, "Sarah Van", "Houten");

            listSortedPerson = Factory.CreateListPerson();
            Factory.AddPersonToList(listUnsortedPerson, "Susan De", "Jongh");
            Factory.AddPersonToList(listUnsortedPerson, "Bill", "Ramos");
            Factory.AddPersonToList(listUnsortedPerson, "Katherine Celine", "Viny");
        }

        [DataTestMethod]
        [DataRow(".\\blank-list.txt", 0)]
        [DataRow(".\\unsorted-names-list.txt", 10)]
        public void Test_getListName(string commandArgument, int listCount)
        {
            var importFilePath = Path.Combine(startupPath, commandArgument);
            var listPerson = Program.getListName(importFilePath);
            Assert.AreEqual(listPerson.Count, listCount);
        }

        [TestMethod]
        public void Test_sortListName()
        {
            var sortedListName = Program.sortListName(listUnsortedPerson);
            var isSorted = true;
            var previous = 0;

            foreach (var sortedName in sortedListName.Select(x => x.LastName.ToUpper()))
            {
                var current = Encoding.ASCII.GetBytes(sortedName.FirstOrDefault().ToString()).FirstOrDefault();
                if (previous > current)
                {
                    isSorted = false;
                }
                previous = current;
            }
            Assert.IsTrue(isSorted);
        }
        
        [TestMethod]
        public void Test_exportSortedName()
        {
            var exportFilePath = Path.Combine(startupPath, ".\\unsorted-names-list.txt");
            Program.exportSortedName(listSortedPerson);
            Assert.IsTrue(File.Exists(exportFilePath));
        }

        [TestCleanup]
        public void Cleanup()
        {
            var exportFilePath = Path.Combine(startupPath, ".\\sorted-names-list.txt");
            if (File.Exists(exportFilePath))
            {
                File.Delete(exportFilePath);
            }

            listUnsortedPerson.Clear();
            listSortedPerson.Clear();
            startupPath = string.Empty;
        }
    }
}
