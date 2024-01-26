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

namespace PRG2_Assignment
{
    internal class Cup:IceCream
    {
        public Cup()
        {

        }
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
        {
            
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

            
            double toppingPrice = Toppings.Count;
            price += toppingPrice; 
            return price;
        }

        public override string ToString()
        {
            return $"Option: {Option}";
        }
    }
}
