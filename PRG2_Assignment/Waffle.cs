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
    internal class Waffle:IceCream
    {
        private string waffleFlavour;
        public string WaffleFlavour { get; set; }
        public Waffle() { }
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) : base(option, scoops, flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
        }
        public override double CalculatePrice()
        {
            double price = 0;
            if (Scoops == 1)
            {
                price = 7;
            }
            else if (Scoops == 2)
            {
                price = 8.5;
            }
            else
            {
                price = 9.5;
            }
            if (new[] { "red velvet", "charcoal", "pandan" }.Contains(WaffleFlavour.ToLower()))
            {
                price += 3;
            }
            
            double toppingPrice = Toppings.Count;
            price += toppingPrice;

            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Premium)
                {
                    price += 2 * flavour.Quantity; // Add $2 for each premium flavor per scoop
                }
            }

            return price;
        }
        public override string ToString()
        {
            return base.ToString() + "\tWaffle Flavour: " + WaffleFlavour;
        }
    }
}
