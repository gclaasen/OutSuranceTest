using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OutSuranceTesting
{
    [TestClass]
    public class ConfirmAssigningValuesToPersonObject
    {
        public List<Person> AssigningValues()
        {
            string[] values = null;

            var p = new List<Person>();

            foreach (var line in File.ReadAllLines(@"../../data.csv", Encoding.GetEncoding(1250)).Skip(1))
            {
                values = line.Split(',');

                p.Add(new Person
                {
                    Name = values[0],
                    Lastname = values[1],
                    Address = values[2],
                    ContactNumber = values[3]
                });
            }

            return p;
        }

        [TestMethod]
        public void CreateOrderedListOfAddressesAscTest()
        {
            List<Person> personList = new List<Person>();

            List<Address> address = new List<Address>();

            personList = AssigningValues();

            foreach (var aa in personList)
            {
                address.Add(new Address
                {
                    Number = Convert.ToInt16(aa.Address.Substring(0, aa.Address.IndexOf(' '))),
                    Street = aa.Address.Remove(0, aa.Address.IndexOf(' ') + 1)
                });
            }

            List<Address> sortedList = address
                .OrderBy(a => a.Street)
                .ThenBy(a => a.Number)
                .ToList();

            List<string> addlist = new List<string>();

            foreach (var aa in sortedList)
            {
                addlist.Add(aa.Number + " " + aa.Street);
            }

            WriteResultToTextFile(addlist, "CreateOrderedListOfAddressesAsc");
            Assert.IsNotNull(sortedList);
        }

        [TestMethod]
        public void CreateFrequencyListOfSurnamesTest()
        {
            List<Person> personList = new List<Person>();

            List<string> surname = new List<string>();

            var surnameFreq = new List<SurnameFrequency>();

            personList = AssigningValues();

            foreach (var surn in personList)
            {
                surname.Add(surn.Lastname);
            }

            foreach (var s in surname.GroupBy(s => s))
            {
                surnameFreq.Add(new SurnameFrequency
                {
                    Surname = s.Key,
                    NoOfTimes = s.Count()
                });
            }

            surnameFreq = surnameFreq.OrderByDescending(s => s.Surname).ToList();

            List<string> addlist = new List<string>();

            foreach (var aa in surnameFreq)
            {
                addlist.Add(aa.Surname + ", " + aa.NoOfTimes);
            }

            WriteResultToTextFile(addlist, "CreateFrequencyListOfSurnames");

        }

        [TestMethod]
        public void WriteResultToTextFile(List<string> list, string whichFile)
        {
            ConstructFile(list, whichFile);
        }

        private static void ConstructFile(List<string> list, string whichFile)
        {
            string path = string.Empty;


            if (whichFile == "CreateOrderedListOfAddressesAsc")
            {
                path = @"../../" + whichFile + ".txt";
            }
            else 
            {
                path = @"../../" + whichFile + ".txt";
            }

            using (FileStream fs = File.Create(path))
            {
                TextWriter t = new StreamWriter(fs);
                t.WriteLine("Test ran : " + DateTime.Today);
                t.WriteLine("________________________START______________________________");

                foreach (var values in list)
                {
                    t.WriteLine(values);
                }

                t.WriteLine("________________________END________________________________");
                t.Close();
            }
        }
    }
}
