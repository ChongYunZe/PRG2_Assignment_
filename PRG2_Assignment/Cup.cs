using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Cup:IceCream
    {
        public Cup()
        {

        }
        public Cup(string option, List<Flavour> flavours)
        {
            Option= option;
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

            //Not sure how to access the toppings
            double toppingPrice = toppings.Count;
            price += toppingPrice; //Probably wrong
            return price;
        }

        public override string ToString()
        {
            return $"Option: {Option}";
        }
    }
}
