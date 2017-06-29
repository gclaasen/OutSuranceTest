using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSuranceBComp
{
    public class Person
    {
        private string _name = string.Empty;
        private string _lastname = string.Empty;
        private string _address = string.Empty;
        private string _con = string.Empty;

        public string Name { get {return _name;} set { _name = value; } }
        public string Lastname { get {return _lastname;} set { _lastname = value; } }
        public string Address { get {return _address;} set { _address = value; } }
        public string ContactNumber { get {return _con;} set { _con = value; } }

    }
}
