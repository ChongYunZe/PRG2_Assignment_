using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Customer
    {
        private string name;
        public string Name { get;set; }

        private int memberid;
        public int Memberid { get; set; }

        private DateTime dob;
        public DateTime Dob { get; set; }

        private List<Order> orderHistory;
        public List<Order> OrderHistory { get; set;}

        private PointCard rewards;

        public PointCard Rewards { get; set; }

        public Customer() { }

        public Customer(string name, int memberid, DateTime dob)
        {
            Name = name;
            Memberid = memberid;
            Dob = dob;
        }

        public Order MakeOrder()
        {

        }

        public bool IsBirthday()
        {

        }

        public override string ToString()
        {
            return $"Name: {Name} Memberid: {Memberid} Dob {Dob}";
        }


    }
}
