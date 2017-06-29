using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OutSuranceBComp.Models;
using OutSuranceBComp.Helpers;
using System.Configuration;


namespace OutSuranceBComp.Worker
{
    public class CreateSortedDataFilesFromCSVFile
    {
        private string _filePath = ConfigurationManager.AppSettings["DataFolder"].ToString();

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
            var surnameFreq = new List<SurnameFrequency>();

            List<string> surname = new List<string>();
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

            List<string> addlist, addlistAsc;

            SortingNameSurnameData(surnameFreq, out addlist, out addlistAsc);

            wf.WriteResultToTextFile(addlist, addlistAsc, "CreateFrequencyListOfSurnames");
        }

        private static void SortingNameSurnameData(List<SurnameFrequency> surnameFreq, out List<string> addlist, out List<string> addlistAsc)
        {


            var surnameFreqAsc = new List<SurnameFrequency>();

            surnameFreq = surnameFreq.OrderByDescending(s => s.Surname).ToList();
            surnameFreqAsc = surnameFreq.OrderBy(s => s.Surname).ToList();

            addlist = new List<string>();
            addlistAsc = new List<string>();
            foreach (var aa in surnameFreq)
            {
                addlist.Add(aa.Surname + ", " + aa.NoOfTimes);
            }

            foreach (var aa in surnameFreqAsc)
            {
                addlistAsc.Add(aa.Surname + ", " + aa.NoOfTimes);
            }
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

