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
    internal abstract class IceCream
    {
        private string option;
        private int scoops;
        private List<Flavour> flavours;
        private List<Topping> toppings;
        public List <Flavour> Flavours { get; set; } 
        public List <Topping> Toppings { get; set; }
        public string Option { get; set; }
        public int Scoops { get; set; }
        public IceCream() { }
        public IceCream(string option, int scoops, List <Flavour> flavours, List <Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = new List<Flavour>();
            Toppings = new List<Topping>();
        }
        public abstract double CalculatePrice();
        
        public override string ToString()
        {
            return "Option: " + Option + "\tScoops: " + Scoops;
        }
    }
}
