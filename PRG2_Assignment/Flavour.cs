using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10258472
// Student Name : Daniel Sha
// Partner Name : Chong Yun Ze
//==========================================================
//==========================================================
// Student Number : S10257927
// Student Name : Chong Yun Ze
// Partner Name : Daniel Sha
//==========================================================

namespace PRG2_Assignment
{
    internal class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;
        public string Type { get; set; }    
        public bool Premium { get; set; }
        public int Quantity { get; set; }
        public Flavour() { }
        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }
        public override string ToString()
        {
            return "Type: " + Type + "\tPremium: " + Premium + "\tQuantity: " + Quantity;
        }
    }
}
