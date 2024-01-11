using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal abstract class IceCream
    {
        private string option;
        private int scoops;
        public List <Flavour> flavours { get; set; } 
        public List <Topping> toppings { get; set; }
        public string Option { get; set; }
        public int Scoops { get; set; }
        public IceCream() { }
        public IceCream(string option, int scoops, List <Flavour> flavours, List <Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            flavours = new List<Flavour>();
            toppings = new List<Topping>();
        }
        public abstract double CalculatePrice();
        
        public override string ToString()
        {
            return "Option: " + Option + "\tScoops: " + Scoops;
        }
    }
}
