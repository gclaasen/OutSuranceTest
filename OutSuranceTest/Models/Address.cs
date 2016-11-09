using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSuranceTest
{
    public class Address
    {
        private int _number;
        private string _street;

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }
    }
}
