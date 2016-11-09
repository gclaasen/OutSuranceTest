using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSuranceTesting
{
    public class SurnameFrequency
    {
        private int _NoOfTimes;
        private string _Surname;
        private string _name;

        public int NoOfTimes
        {
            get { return _NoOfTimes; }
            set { _NoOfTimes = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }
    }
}
