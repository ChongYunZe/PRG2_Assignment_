using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Order
    {
        private int id;

        public int Id { get; set; }

        private DateTime timeReceived;

        public DateTime TimeReceived { get; set; }

        private DateTime? timeFulfilled;

        public DateTime? TimeFulfilled { get; set; }

        private List<IceCream> iceCreamList;

        public List<IceCream> IceCreamList { get; set; }

        public Order()
        {

        }

        public Order(int id, DateTime timeReceived)
        {
            Id=id;
            TimeReceived=timeReceived;
        }

        public void ModifyIceCream(int id)
        {

        }

        public void AddIceCream(IceCream)
        {

        }

        public void DeleteIceCream(int id)
        {

        }

        public double CalculateTotal()
        {

        }

        public override string ToString()
        {
            return $"Id: {Id} Time Received: {TimeReceived} Time Fullfilled: {TimeFulfilled}";
        }
    }
}
