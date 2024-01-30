using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10258472
// Student Name : Daniel Sha
// Partner Name : Chong Yun Ze
//==========================================================


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
            IceCreamList = new List<IceCream>();
        }

        public void ModifyIceCream(int index, IceCream ModifiedIceCream)
        {
            if (index>= 0 && index< IceCreamList.Count)
            {
                IceCreamList[index]=ModifiedIceCream;
            }

        }

        public void AddIceCream(IceCream icecream)
        {
            IceCreamList.Add(icecream);

        }

        public void DeleteIceCream(int index)
        {
            if (index >= 0 && index < IceCreamList.Count)
            {
                IceCreamList.RemoveAt(index);
            }

        }

        public double CalculateTotal()
        {
            return 1;
        }

        public override string ToString()
        {
            return $"Id: {Id} Time Received: {TimeReceived} ";//Time Fullfilled: {TimeFulfilled}
        }
    }
}
