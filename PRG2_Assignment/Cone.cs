using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//==========================================================
// Student Number : S10257927
// Student Name : Chong Yun Ze
// Partner Name : Daniel Sha
//==========================================================

namespace PRG2_Assignment
{
    internal class Cone : IceCream
    {
        private bool dipped;
        public bool Dipped { get; set; }
        public Cone() { }
        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped) : base("Cone", scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            double price = 0;
            if (Scoops == 1)
            {
                price = 4;
            }
            else if (Scoops == 2)
            {
                price = 5.5;
            }
            else
            {
                price = 6.5;
            }
            if (Dipped == true)
            {
                price += 2;
            }
            
            double toppingPrice = Toppings.Count; 
            price += toppingPrice; 
            return price;
        }
        public override string ToString()
        {
            return base.ToString() + "\tDipped: " + Dipped;
        }
    }
}
