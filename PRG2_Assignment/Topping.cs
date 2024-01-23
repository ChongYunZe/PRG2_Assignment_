using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10257927 & S10258472
// Student Name : Chong Yun Ze
// Partner Name : Daniel Sha
//==========================================================

namespace PRG2_Assignment
{
    internal class Topping
    {
        private string type;
        public string Type { get; set; }
        public Topping() { }
        public Topping(string type)
        {
            Type = type;
        }
        public override string ToString()
        {
            return "Type: " + Type;
        }
    }
}
