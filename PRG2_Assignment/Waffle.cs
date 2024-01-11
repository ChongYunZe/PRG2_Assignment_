using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Waffle
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

        }
        public override string ToString()
        {
            return base.ToString() + "\tWaffle Flavour: " + WaffleFlavour;
        }
    }
}
