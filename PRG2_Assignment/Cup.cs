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
            return 1;
        }

        public override string ToString()
        {
            return $"Option: {Option}";
        }
    }
}
