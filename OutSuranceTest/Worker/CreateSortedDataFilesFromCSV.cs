using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutSuranceTest.Models;
using OutSuranceTest.Helpers;


namespace OutSuranceTest.Worker
{
    public class CreateSortedDataFilesFromCSVFile
    {
        private string _filePath = @"../../data/data.csv";

        private WriteToFiles wf = new WriteToFiles();

        private List<Person> personList = new List<Person>();

        public void CreateSortedDataFiles()
        {
           personList = AssigningValues(_filePath);

            CreateOrderedListOfAddressesAsc();

            CreateFrequencyListOfSurnames();
        }

        private void CreateFrequencyListOfSurnames()
        {
            List<string> surname = new List<string>();

            var surnameFreq = new List<SurnameFrequency>();
            var surnameFreqAsc = new List<SurnameFrequency>();

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
            surnameFreqAsc = surnameFreq.OrderBy(s => s.Surname).ToList();

            List<string> addlist = new List<string>();
            List<string> addlistAsc = new List<string>();

            foreach (var aa in surnameFreq)
            {
                addlist.Add(aa.Surname + ", " + aa.NoOfTimes);
            }

            foreach (var aa in surnameFreqAsc)
            {
                addlistAsc.Add(aa.Surname + ", " + aa.NoOfTimes);
            }

            wf.WriteResultToTextFile(addlist, addlistAsc, "CreateFrequencyListOfSurnames");
        }

        private void CreateOrderedListOfAddressesAsc()
        {
            List<Address> address = new List<Address>();

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

            wf.WriteResultToTextFile(addlist, null, "CreateOrderedListOfAddressesAsc");
        }

        private List<Person> AssigningValues(string _filePath)
        {
            string[] values = null;

            var p = new List<Person>();

            foreach (var line in File.ReadAllLines(_filePath, Encoding.GetEncoding(1250)).Skip(1))
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
    }
}

