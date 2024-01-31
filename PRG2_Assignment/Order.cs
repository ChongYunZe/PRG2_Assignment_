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

        public DateTime? TimeReceived { get; set; }

        private DateTime timeFulfilled;

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

        public void ModifyIceCream(int index)
        {
            /*if (index>= 0 && index< IceCreamList.Count)
            {
                IceCreamList[index]=ModifiedIceCream;
            }*/
            IceCream modifyIceCream = IceCreamList[index];
            List<string> flavourlist = new List<string>();
            List<string> toppingslist = new List<string>();
            List<Flavour> FlavourObject = new List<Flavour>();
            List<Topping> ToppingObject = new List<Topping>();

            Console.Write("Enter an option (Cup, Cone or Waffle): ");
            string newOption = Console.ReadLine();
            modifyIceCream.Option = newOption;

            Console.Write("Enter number of scoops (1-3): ");
            int scoopInput = Convert.ToInt32(Console.ReadLine());
            modifyIceCream.Scoops = scoopInput;
            /*Console.Write("Enter quantity (1-3): ");
            int quantityInput = Convert.ToInt32(Console.ReadLine());*/

            Console.Write("Enter ice cream flavour: ");
            string flavourInput = Console.ReadLine();
            flavourlist.Add(flavourInput); //Changed position
            if (scoopInput == 1)
            {

                /*bool flavourPremium = CheckFlavour(flavourlist);

                Flavour newFlavour = new Flavour(flavourInput, flavourPremium, scoopInput);*/
                int quantityInput = 1;
                //flavourlist.Add(flavourInput);


                for (int i = 0; i < flavourlist.Count; i++)
                {
                    bool flavourPremium;
                    if (new[] { "durian", "ube", "sea salt" }.Contains(flavourlist[i].ToLower()))
                    {
                        flavourPremium = true;
                    }
                    else
                    {
                        flavourPremium = false;
                    }
                    Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);
                    FlavourObject.Add(newFlavour);
                }



            }

            else if (scoopInput == 2)
            {
                Console.Write("Enter quantity: ");
                int quantityInput = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < flavourlist.Count; i++)
                {
                    bool flavourPremium;
                    if (new[] { "durian", "ube", "sea salt" }.Contains(flavourlist[i].ToLower()))
                    {
                        flavourPremium = true;
                    }
                    else
                    {
                        flavourPremium = false;
                    }
                    Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);
                    FlavourObject.Add(newFlavour);
                }

                if (quantityInput < scoopInput)
                {

                    /*bool flavourPremium = CheckFlavour(flavourlist);

                    Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);*/
                    Console.Write("Enter ice cream flavour for scoop 2: ");
                    string flavourInput2 = Console.ReadLine();
                    flavourlist.Add(flavourInput2);

                    for (int i = 0; i < flavourlist.Count; i++)
                    {
                        bool flavourPremium;
                        if (new[] { "durian", "ube", "sea salt" }.Contains(flavourlist[i].ToLower()))
                        {
                            flavourPremium = true;
                        }
                        else
                        {
                            flavourPremium = false;
                        }
                        Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);
                        FlavourObject.Add(newFlavour);
                    }

                }

            }
            else if (scoopInput > 2)
            {
                Console.Write("Enter quantity: ");
                int quantityInput = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < flavourlist.Count; i++)
                {
                    bool flavourPremium;
                    if (new[] { "durian", "ube", "sea salt" }.Contains(flavourlist[i].ToLower()))
                    {
                        flavourPremium = true;
                    }
                    else
                    {
                        flavourPremium = false;
                    }
                    Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);
                    FlavourObject.Add(newFlavour);
                }

                while (quantityInput < scoopInput)
                {

                    /*bool flavourPremium = CheckFlavour(flavourlist);

                    Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);*/

                    Console.WriteLine("Enter ice cream flavour for scoop 2: ");
                    string flavourInput2 = Console.ReadLine();
                    flavourlist.Add(flavourInput2);

                    Console.Write("Enter quantity: ");
                    int quantityInput2 = Convert.ToInt32(Console.ReadLine());
                    if (quantityInput2 > 1)
                    {
                        for (int i = 0; i < quantityInput2; i++)
                        {
                            flavourlist.Add(flavourInput2);

                        }

                    }
                    else
                    {
                        flavourlist.Add(flavourInput2);

                    }

                    for (int i = 0; i < flavourlist.Count; i++)
                    {
                        bool flavourPremium;
                        if (new[] { "durian", "ube", "sea salt" }.Contains(flavourlist[i].ToLower()))
                        {
                            flavourPremium = true;
                        }
                        else
                        {
                            flavourPremium = false;
                        }
                        Flavour newFlavour = new Flavour(flavourInput2, flavourPremium, quantityInput2);
                        FlavourObject.Add(newFlavour);
                    }
                    quantityInput += quantityInput2;
                }



            }
            Console.WriteLine("Enter quantity of types of toppings: ");
            int quantitytoppings = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < quantitytoppings; i++)
            {
                Console.Write("Enter ice cream topping: ");
                string toppingInput = Console.ReadLine();
                Topping newTopping = new Topping(toppingInput);
                ToppingObject.Add(newTopping);




            }




            //Checking type of ice cream

            if (newOption.ToLower() == "cone")
            {
                Console.Write("Dipped Ice Cream? [Y/N]: ");
                string dippedInput = Console.ReadLine();

                bool dippedBool = false;
                if (dippedInput.ToUpper() == "Y")
                {
                    dippedBool = true;
                }
                else
                {
                    dippedBool = false;
                }


                IceCream newIceCream = new Cone(newOption, scoopInput, FlavourObject, ToppingObject, dippedBool);
                
                
                IceCreamList.Insert(index, newIceCream); // Insert the new ice cream at the specified index
                IceCreamList.RemoveAt(index + 1); // Remove the ice cream at the next index 
                //newOrder.AddIceCream(newIceCream);
                //IceCreamOrderList.Add(newIceCream);
            }
            else if (newOption.ToLower() == "waffle")
            {
                Console.Write("Enter a waffle flavour: ");
                string waffleflavourInput = Console.ReadLine();
                IceCream newIceCream = new Waffle(newOption, scoopInput, FlavourObject, ToppingObject, waffleflavourInput);

                IceCreamList.Insert(index, newIceCream); // Insert the new ice cream at the specified index
                IceCreamList.RemoveAt(index + 1); // Remove the ice cream at the next index 
                //newOrder.AddIceCream(newIceCream);
                //IceCreamOrderList.Add(newIceCream);
            }
            else if (newOption.ToLower() == "cup")
            {
                IceCream newIceCream = new Cup(newOption, scoopInput, FlavourObject, ToppingObject);

                IceCreamList.Insert(index, newIceCream); // Insert the new ice cream at the specified index
                IceCreamList.RemoveAt(index + 1); // Remove the ice cream at the next index 

                //newOrder.AddIceCream(newIceCream);
                //IceCreamOrderList.Add(newIceCream);

            }
            else
            {
                Console.WriteLine("Invalid Option.");
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

        public double CalculateTotal(Customer customer)
        {
            double total = 0;

            if (customer.Rewards.PunchCard >= 10)
            {
                // Set the cost of the first ice cream in the order to $0.00
                if (IceCreamList.Count > 0)
                {
                    IceCream firstIceCream = IceCreamList[0];
                    total -= firstIceCream.CalculatePrice();

                    // Reset the punch card to 0
                    customer.Rewards.PunchCard = customer.Rewards.PunchCard - 10;
                }
            }


            foreach (IceCream iceCream in IceCreamList)
            {
                total += iceCream.CalculatePrice();
            }
            if (customer.IsBirthday())
            {
                double maxPrice = 0;

                // Iterate through each ice cream to find the most expensive one
                foreach (IceCream icecream in IceCreamList)
                {
                    double iceCreamPrice = icecream.CalculatePrice();

                    // Update maxPrice if the current ice cream is more expensive
                    if (iceCreamPrice > maxPrice)
                    {
                        maxPrice = iceCreamPrice;
                    }
                }

                // Subtract the price of the most expensive ice cream from the total
                total -= maxPrice;
            }

           if (customer.Rewards.Tier == "Silver" || customer.Rewards.Tier == "Gold")
            {
                // Prompt the user for the number of points they want to use
                Console.Write("Enter the number of points you want to use to offset your final bill: ");
                int pointsToUse = int.Parse(Console.ReadLine());

                // Calculate the discount based on the entered points
                /*double discount = Math.Min(total, pointsToUse);*/
                double discount = pointsToUse * 0.02;
                total -= discount;

                // Deduct the used points from the customer's points
                customer.Rewards.Points -= pointsToUse;
            }


            if (customer.Rewards.Points >= 50)
            {
                // Calculate the discount based on the redeemed points
                double discount = Math.Min(total, customer.Rewards.Points);
                total -= discount;

                // Deduct the redeemed points from the customer's points
                customer.Rewards.Points -= (int)discount;
            }

            return total;
        }

        public override string ToString()
        {
            return $"Id: {Id} Time Received: {TimeReceived} ";//Time Fullfilled: {TimeFulfilled}
        }
    }
}
