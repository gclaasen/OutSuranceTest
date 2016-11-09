using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSuranceTest.Models
{
    public class SurnameFrequency
    {
        private int _NoOfTimes;
        private string _Surname;

        public int NoOfTimes
        {
            get { return _NoOfTimes; }
            set { _NoOfTimes = value; }
        }

        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }
    }
}
