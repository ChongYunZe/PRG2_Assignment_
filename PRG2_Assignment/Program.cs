using PRG2_Assignment;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using System.Globalization;
//Daniel --> Features 1, 3, 4
//YunZe --> Features 2, 5, 6

//==========================================================
// Student Number : S10257927 & S10258472
// Student Name : Chong Yun Ze
// Partner Name : Daniel Sha
//==========================================================



int option;
List<Customer> customerlist = new List<Customer>(); //From option 1
Dictionary<string, Customer> customerdict = new Dictionary<string, Customer>(); //From option 4
Dictionary<int, Order> orderHistDict = new Dictionary<int, Order>(); // opt 5
Dictionary<int, Order> OrderDict = new Dictionary<int, Order>(); //linking to below for opt 4

Dictionary<Order, List<IceCream>> IceCreamOrderDict = new Dictionary<Order, List<IceCream>>(); //Storing info for opt 4
Queue<Order> goldQueue = new Queue<Order>();
Queue<Order> regularQueue = new Queue<Order>();
void DisplayMenu()
{
    Console.WriteLine("");
    Console.WriteLine("-------------I.C.Treats Menu--------------");
    Console.WriteLine("[1] List all customers");
    Console.WriteLine("[2] List all current orders");
    Console.WriteLine("[3] Register a new customer");
    Console.WriteLine("[4] Create a customer's order");
    Console.WriteLine("[5] Display order details of a customer");
    Console.WriteLine("[6] Modify order details");
    Console.WriteLine("[7] Process an order and checkout");
    Console.WriteLine("[8] Display monthly charged amounts breakdown & total charged amounts for the year");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("------------------------------------------");
    Console.Write("Please enter an option: ");
    option = Convert.ToInt32(Console.ReadLine());

}
string[] heading = null;
void ReadingCustomerFile()
{
    using (StreamReader sr = new StreamReader("customers.csv"))
    {
        string? s = sr.ReadLine();
        if (s != null)
        {
            heading = s.Split(',');
            //Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
            //    heading[0], heading[1], heading[2], heading[3], heading[4], heading[5]);

            while ((s = sr.ReadLine()) != null)
            {
                string[] values = s.Split(',');
                Customer customer = new(values[0], Convert.ToInt32(values[1]), Convert.ToDateTime(values[2]));
                PointCard pointcard = new(values[3], Convert.ToInt32(values[4]), Convert.ToInt32(values[5]));
                customer.Rewards = pointcard;
               
                customerlist.Add(customer);
                
                
            }

            
        }

    }
}
ReadingCustomerFile();
void DisplayCustomer()
{
    Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
                heading[0], heading[1], heading[2], heading[3], heading[4], heading[5]);
    foreach (Customer customer in customerlist)
    {
        Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
        customer.Name, customer.Memberid, customer.Dob, customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);



    }
    Console.WriteLine();
}
void OrderHistory(int memberID)
{
    using (StreamReader srOrders = new StreamReader("orders.csv"))
    {
        string? sOrder = srOrders.ReadLine(); //Reading the headings

        if (sOrder != null)
        {
            while ((sOrder = srOrders.ReadLine()) != null)
            {
                string[] valuesOrders = sOrder.Split(",");
                if (Convert.ToInt32(valuesOrders[1]) == memberID)
                {
                    Order orders = new Order(Convert.ToInt32(valuesOrders[0]), Convert.ToDateTime(valuesOrders[2]));
                    Console.WriteLine(orders);
                    Console.WriteLine("Time Fulfilled: {0}", valuesOrders[3]);
                    Console.WriteLine();
                    Console.WriteLine("Option: {0}", valuesOrders[4]);
                    Console.WriteLine("Scoops: {0}", valuesOrders[5]);
                    if (valuesOrders[4].ToLower() == "cone")
                    {
                        Console.WriteLine("Dipped: {0}", valuesOrders[6]);
                    }
                    else if (valuesOrders[4].ToLower() == "waffle")
                    {
                        Console.WriteLine("Waffle Flavour: {0}", valuesOrders[7]);
                    }
                    List<string>flavourHist = new List<string>();
                    List<string>toppingHist = new List<string>();
                    for (int i = 8; i< valuesOrders.Length; i++)
                    {
                        if (new[] { "durian", "ube", "sea salt", "vanilla", "chocolate", "strawberry"}.Contains(valuesOrders[i].ToLower()))
                        {
                            flavourHist.Add(valuesOrders[i]);
                        }
                        else if (new[] { "sprinkles", "mochi", "sago", "oreos"}.Contains(valuesOrders[i].ToLower()))
                        {
                            toppingHist.Add(valuesOrders[i]);
                        }
                    }
                    Console.WriteLine();

                    Console.WriteLine("-----Flavours-----");
                    foreach (string flavour in flavourHist)
                    {                        
                        Console.WriteLine(flavour);
                    }
                    
                    Console.WriteLine();    

                    Console.WriteLine("-----Toppings-----");
                    foreach (string topping in toppingHist)
                    {                        
                        Console.WriteLine(topping);
                    }
                }
                /*Order orders = new Order(Convert.ToInt32(valuesOrders[0]), Convert.ToDateTime(valuesOrders[2]));
                orderHistDict[Convert.ToInt32(valuesOrders[1])] = orders;
                for (int i = 0; i < customerlist.Count; i++)
                {
                    if (customerlist[i].Memberid == memberID)
                    {
                        customerlist[i].OrderHistory.Add(orders);
                    }
                }*/

            }
        }
    }
}

/*bool CheckFlavour(List<string> flavourlist)
{
    for (int i = 0; i < flavourlist.Count; i++)
    {
        
        if (new[] { "durian", "ube", "sea salt" }.Contains(flavourlist[i].ToLower()))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    return false;
}*/
void CheckFlavour2(string flavourInput, int quantity, List<string> flavourlist, List<Flavour> FlavourObject)
{
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
        Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantity);
        FlavourObject.Add(newFlavour);
    }
    
    
}

static int CalculatePointsEarned(Order order, Customer customer)
{
    double totalAmount = order.CalculateTotal(customer);

    // Calculate points earned using the conversion rate (72% of the total amount paid)
    int pointsEarned = (int)Math.Floor(totalAmount * 0.72);

    return pointsEarned;
}



while (true)
{
    try
    {
        DisplayMenu(); //Displays the Menu
        if (option == 0)
        {
            break;
        }

        else if (option == 1)
        {
            try
            {
                /*foreach (Customer customer in customerlist)
                {
                    Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
                    customer.Name, customer.Memberid, customer.Dob, customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);



                }
                Console.WriteLine();*/
                DisplayCustomer();

            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else if (option == 2)
        {
            try
            {
                Console.WriteLine("=============== Gold Queue ===============");
                foreach (Order order in goldQueue)
                {
                    Console.WriteLine(order);
                    foreach(IceCream icecream in order.IceCreamList)
                    {
                        Console.WriteLine("Option: {0}", icecream.Option);
                        Console.WriteLine("Scoops: {0}", icecream.Scoops);

                        if (icecream is Cone)
                        {
                            Cone icecreamCone = (Cone)icecream;
                            Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
                        }
                        else if (icecream is Waffle)
                        {
                            Waffle icecreamWaffle = (Waffle)icecream;
                            Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
                        }

                        Console.WriteLine("----- Flavour(s) -----");
                        foreach (Flavour flavour in icecream.Flavours)
                        {
                            Console.WriteLine("Type: {0}  Quantity: {1}", flavour.Type, flavour.Quantity);
                        }

                        Console.WriteLine("----- Topping(s) -----");
                        foreach (Topping topping in icecream.Toppings)
                        {
                            Console.WriteLine(topping.Type);
                        }
                    }
                }

                Console.WriteLine();

                Console.WriteLine("============= Regular Queue ==============");
                foreach (Order order in regularQueue)
                {
                    Console.WriteLine(order);
                    foreach (IceCream icecream in order.IceCreamList)
                    {
                        Console.WriteLine("Option: {0}", icecream.Option);
                        Console.WriteLine("Scoops: {0}", icecream.Scoops);

                        if (icecream is Cone)
                        {
                            Cone icecreamCone = (Cone)icecream;
                            Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
                        }
                        else if (icecream is Waffle)
                        {
                            Waffle icecreamWaffle = (Waffle)icecream;
                            Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
                        }

                        Console.WriteLine("----- Flavour(s) -----");
                        foreach (Flavour flavour in icecream.Flavours)
                        {
                            Console.WriteLine("Type: {0}  Quantity: {1}", flavour.Type, flavour.Quantity);
                        }

                        Console.WriteLine("----- Topping(s) -----");
                        foreach (Topping topping in icecream.Toppings)
                        {
                            Console.WriteLine(topping.Type);
                        }
                    }
                }



            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        else if (option == 3)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Please enter your name: ");
                    string customername = Console.ReadLine();
                    Console.WriteLine("Please enter your id (6 digits): ");
                    int customermemberid = Convert.ToInt32(Console.ReadLine());
                    if (customermemberid! > 000000 && customermemberid > 999999)
                    {
                        Console.WriteLine("Please enter an id that is 6 digits in betweeen 000000 and 999999");
                        break;
                    }
                    Console.WriteLine("Please enter your date of birth (dob): ");
                    //DateTime.Parse makes sure the date entered is correctly read
                    DateTime customerdob = DateTime.Parse(Console.ReadLine());

                    //creating customer object
                    Customer newcustomer = new Customer(customername, customermemberid, customerdob);
                    PointCard pointCard = new PointCard();
                    newcustomer.Rewards = pointCard;

                    using (StreamWriter sw = new StreamWriter("customers.csv", true))
                    {
                        sw.WriteLine($"{customername},{customermemberid},{customerdob:dd/MM/yyyy},{"Ordinary"},{pointCard.Points},{pointCard.PunchCard}");
                    }

                    // Displaying registration status
                    Console.WriteLine("Customer registered successfully!");
                    break;
                }

            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        else if (option == 4)
        {

            DisplayCustomer();
            Console.Write("Please select a customer (Enter Member ID): ");
            int customeridInput = Convert.ToInt32(Console.ReadLine());
            List<IceCream> IceCreamOrderList = new List<IceCream>();//opt 4 linking to dictionary made at start

            while (true)
            {

                List<string> flavourlist = new List<string>();
                List<string> toppingslist = new List<string>();
                List<Flavour> FlavourObject = new List<Flavour>();
                List<Topping> ToppingObject = new List<Topping>();



                Order newOrder = new Order(customeridInput, DateTime.Now);
                OrderDict[customeridInput] = newOrder;
                Console.Write("Enter an option (Cup, Cone or Waffle): ");
                string optionInput = Console.ReadLine();
                Console.Write("Enter number of scoops (1-3): ");
                int scoopInput = Convert.ToInt32(Console.ReadLine());

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
                    CheckFlavour2(flavourInput, quantityInput, flavourlist, FlavourObject);



                }

                else if (scoopInput == 2)
                {
                    Console.Write("Enter quantity: ");
                    int quantityInput = Convert.ToInt32(Console.ReadLine());
                    CheckFlavour2(flavourInput, quantityInput, flavourlist, FlavourObject);
                    if (quantityInput < scoopInput)
                    {

                        /*bool flavourPremium = CheckFlavour(flavourlist);

                        Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);*/
                        Console.Write("Enter ice cream flavour for scoop 2: ");
                        string flavourInput2 = Console.ReadLine();
                        flavourlist.Add(flavourInput2);
                        CheckFlavour2(flavourInput2, quantityInput, flavourlist, FlavourObject);
                    }

                }
                else if (scoopInput > 2)
                {
                    Console.Write("Enter quantity: ");
                    int quantityInput = Convert.ToInt32(Console.ReadLine());
                    CheckFlavour2(flavourInput, quantityInput, flavourlist, FlavourObject);
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

                        CheckFlavour2(flavourInput2, quantityInput2, flavourlist, FlavourObject);
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

                if (optionInput.ToLower() == "cone")
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


                    IceCream newIceCream = new Cone(optionInput, scoopInput, FlavourObject, ToppingObject, dippedBool);
                    newOrder.AddIceCream(newIceCream);
                    IceCreamOrderList.Add(newIceCream);
                }
                else if (optionInput.ToLower() == "waffle")
                {
                    Console.Write("Enter a waffle flavour: ");
                    string waffleflavourInput = Console.ReadLine();
                    IceCream newIceCream = new Waffle(optionInput, scoopInput, FlavourObject, ToppingObject, waffleflavourInput);
                    newOrder.AddIceCream(newIceCream);
                    IceCreamOrderList.Add(newIceCream);
                }
                else if (optionInput.ToLower() == "cup")
                {
                    IceCream newIceCream = new Cup(optionInput, scoopInput, FlavourObject, ToppingObject);
                    newOrder.AddIceCream(newIceCream);
                    IceCreamOrderList.Add(newIceCream);

                }
                else
                {
                    Console.WriteLine("Invalid Option.");
                }



                //User Input For Another Order
                Console.Write("Would you like to another ice cream to the order? ");
                string AddOrderInput = Console.ReadLine();



                //Checking customer membership
                for (int i = 0; i < customerlist.Count; i++)
                {
                    if (customerlist[i].Memberid == customeridInput)
                    {
                        Customer c = customerlist[i];
                        if (c.Rewards.Tier.ToLower() == "gold")
                        {
                            goldQueue.Enqueue(newOrder);
                        }
                        else
                        {
                            regularQueue.Enqueue(newOrder);
                        }
                        customerlist[i].CurrentOrder = newOrder;
                    }
                }
                Console.WriteLine("Order has been made successfully.");
                if (AddOrderInput.ToUpper() == "N")
                {
                    IceCreamOrderDict[newOrder] = IceCreamOrderList;
                    break;
                }

            }
        }

        
        
        else if (option == 5)
        {
            DisplayCustomer();
            Console.Write("Select a Customer ID: ");
            int customerInput = Convert.ToInt32(Console.ReadLine());
            Order icecreamOrder = OrderDict[customerInput];
            List<IceCream> icecreamDetails = IceCreamOrderDict[icecreamOrder];

            Console.WriteLine("============= Current ==============");
            Console.WriteLine(OrderDict[customerInput]);
            Console.WriteLine("Time Fulfilled: - ");
            for (int i = 0; i < icecreamDetails.Count; i++)
            {
                IceCream details = icecreamDetails[i];
                Console.WriteLine("Option: {0}", details.Option);
                Console.WriteLine("Scoops: {0}", details.Scoops);
                if (details is Cone)
                {
                    Cone icecreamCone = (Cone)details;
                    Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
                }
                else if (details is Waffle)
                {
                    Waffle icecreamWaffle = (Waffle)details;
                    Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
                }
                Console.WriteLine();


                Console.WriteLine("----- Flavour(s) -----");
                foreach (Flavour f in details.Flavours)
                {
                    Console.WriteLine("Type: {0} \tQuantity: {1}", f.Type, f.Quantity);
                }

                Console.WriteLine("----- Topping(s) -----");
                foreach (Topping t in details.Toppings)
                {
                    Console.WriteLine(t.Type);
                }
                Console.WriteLine();

            }

            //DisplayCustomer();
            //Console.Write("Please select a customer (Enter Member ID): ");
            //int customerInput = Convert.ToInt32(Console.ReadLine());
            //int customerIndex = 0;
            //for (int i = 0; i < customerlist.Count; i++)
            //{
            //    if (customerInput == Convert.ToInt32(customerlist[i].Memberid))
            //    {
            //        customerIndex = i;
            //        break;
            //    }
            //}
            //Console.WriteLine("============= Current ==============");

            //if (customerlist[customerIndex].Rewards.Tier == "Gold")
            //{
            //    foreach (Order order in goldQueue)
            //    {
            //        if (customerlist[customerIndex].Memberid == customerInput)
            //        {
            //            Console.WriteLine(order);
            //            foreach (Order orders in goldQueue)
            //            {

            //                foreach (IceCream icecream in orders.IceCreamList)
            //                {
            //                    Console.WriteLine("Option: {0}", icecream.Option);
            //                    Console.WriteLine("Scoops: {0}", icecream.Scoops);

            //                    if (icecream is Cone)
            //                    {
            //                        Cone icecreamCone = (Cone)icecream;
            //                        Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
            //                    }
            //                    else if (icecream is Waffle)
            //                    {
            //                        Waffle icecreamWaffle = (Waffle)icecream;
            //                        Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
            //                    }


            //                    Console.WriteLine("----- Flavour(s) -----");
            //                    foreach (Flavour flavour in icecream.Flavours)
            //                    {
            //                        Console.WriteLine("Type: {0}  Quantity: {1}", flavour.Type, flavour.Quantity);
            //                    }

            //                    Console.WriteLine("----- Topping(s) -----");
            //                    foreach (Topping topping in icecream.Toppings)
            //                    {
            //                        Console.WriteLine(topping.Type);
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}
            //else
            //{
            //    foreach (Order order in regularQueue)
            //    {
            //        if (customerlist[customerIndex].Memberid == customerInput)
            //        {
            //            Console.WriteLine(order);
            //            foreach (Order orders in regularQueue)
            //            {

            //                foreach (IceCream icecream in orders.IceCreamList)
            //                {
            //                    Console.WriteLine("Option: {0}", icecream.Option);
            //                    Console.WriteLine("Scoops: {0}", icecream.Scoops);

            //                    if (icecream is Cone)
            //                    {
            //                        Cone icecreamCone = (Cone)icecream;
            //                        Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
            //                    }
            //                    else if (icecream is Waffle)
            //                    {
            //                        Waffle icecreamWaffle = (Waffle)icecream;
            //                        Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
            //                    }


            //                    Console.WriteLine("----- Flavour(s) -----");
            //                    foreach (Flavour flavour in icecream.Flavours)
            //                    {
            //                        Console.WriteLine("Type: {0}  Quantity: {1}", flavour.Type, flavour.Quantity);
            //                    }

            //                    Console.WriteLine("----- Topping(s) -----");
            //                    foreach (Topping topping in icecream.Toppings)
            //                    {
            //                        Console.WriteLine(topping.Type);
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}




            Console.WriteLine();



            Console.WriteLine("============== Past ================");
            OrderHistory(customerInput);
            
            

                foreach (Order pastOrder in selectedcustomer.OrderHistory)
                {
                    Console.WriteLine($"Order ID: {pastOrder.Id}");
                    Console.WriteLine($"Time Fulfilled: {pastOrder.TimeFulfilled}");
                    // Display other details of the past order if needed
                    Console.WriteLine();
                }
            }
            

        }

        else if (option == 6)
        {
            DisplayCustomer();
            Console.Write("Select a Customer ID: ");
            int customerInput = Convert.ToInt32(Console.ReadLine());
            Order icecreamOrder = OrderDict[customerInput];
            List<IceCream> icecreamDetails = IceCreamOrderDict[icecreamOrder];
            for (int i = 0; i < icecreamDetails.Count; i++)
            {
                IceCream details = icecreamDetails[i];
                Console.WriteLine("---------------[{0}]---------------", i + 1);
                Console.WriteLine("Option: {0}", details.Option);
                Console.WriteLine("Scoops: {0}", details.Scoops);
                if (details is Cone)
                {
                    Cone icecreamCone = (Cone)details;
                    Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
                }
                else if (details is Waffle)
                {
                    Waffle icecreamWaffle = (Waffle)details;
                    Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
                }
                Console.WriteLine();


                Console.WriteLine("----- Flavour(s) -----");
                foreach (Flavour f in details.Flavours)
                {
                    Console.WriteLine("Type: {0} \tQuantity: {1}", f.Type, f.Quantity);
                }

                Console.WriteLine("----- Topping(s) -----");
                foreach (Topping t in details.Toppings)
                {
                    Console.WriteLine(t.Type);
                }
                Console.WriteLine();

            }
            /*Customer selectedcustomer = null;

            for (int i = 0; i < customerlist.Count; i++)
            {
                if (customerlist[i].Memberid == customerInput)
                {
                    selectedcustomer = customerlist[i];
                    Console.WriteLine(selectedcustomer.CurrentOrder);

                }
            }*/
            /*int customerIndex = 0;
            for (int i = 0; i < customerlist.Count; i++)
            {
                if (customerInput == Convert.ToInt32(customerlist[i].Memberid))
                {
                    customerIndex = i;
                    break;
                }
            }*/
            //if (customerlist[customerIndex].Rewards.Tier == "Gold")
            //{
            //    foreach (Order order in goldQueue)
            //    {
            //        if (customerlist[customerIndex].Memberid == customerInput)
            //        {
            //            Console.WriteLine(order);
            //            foreach (Order orders in goldQueue)
            //            {

            //                foreach (IceCream icecream in orders.IceCreamList)
            //                {
            //                    Console.WriteLine("Option: {0}", icecream.Option);
            //                    Console.WriteLine("Scoops: {0}", icecream.Scoops);

            //                    if (icecream is Cone)
            //                    {
            //                        Cone icecreamCone = (Cone)icecream;
            //                        Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
            //                    }
            //                    else if (icecream is Waffle)
            //                    {
            //                        Waffle icecreamWaffle = (Waffle)icecream;
            //                        Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
            //                    }


            //                    Console.WriteLine("----- Flavour(s) -----");
            //                    foreach (Flavour flavour in icecream.Flavours)
            //                    {
            //                        Console.WriteLine("Type: {0}  Quantity: {1}", flavour.Type, flavour.Quantity);
            //                    }

            //                    Console.WriteLine("----- Topping(s) -----");
            //                    foreach (Topping topping in icecream.Toppings)
            //                    {
            //                        Console.WriteLine(topping.Type);
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}
            //else
            //{
            //    foreach (Order order in regularQueue)
            //    {
            //        if (customerlist[customerIndex].Memberid == customerInput)
            //        {
            //            Console.WriteLine(order);
            //            foreach (Order orders in regularQueue)
            //            {

            //                foreach (IceCream icecream in orders.IceCreamList)
            //                {
            //                    Console.WriteLine("Option: {0}", icecream.Option);
            //                    Console.WriteLine("Scoops: {0}", icecream.Scoops);

            //                    if (icecream is Cone)
            //                    {
            //                        Cone icecreamCone = (Cone)icecream;
            //                        Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
            //                    }
            //                    else if (icecream is Waffle)
            //                    {
            //                        Waffle icecreamWaffle = (Waffle)icecream;
            //                        Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
            //                    }


            //                    Console.WriteLine("----- Flavour(s) -----");
            //                    foreach (Flavour flavour in icecream.Flavours)
            //                    {
            //                        Console.WriteLine("Type: {0}  Quantity: {1}", flavour.Type, flavour.Quantity);
            //                    }

            //                    Console.WriteLine("----- Topping(s) -----");
            //                    foreach (Topping topping in icecream.Toppings)
            //                    {
            //                        Console.WriteLine(topping.Type);
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}

            Console.WriteLine();

            Console.WriteLine("----------Modify Order Details-----------");
            Console.WriteLine("[1] Modify ice cream object");
            Console.WriteLine("[2] Add new ice cream object to order");
            Console.WriteLine("[3] Delete ice cream object from order");
            Console.WriteLine("-----------------------------------------");
            Console.Write("Enter an option: ");
            int optionInput = Convert.ToInt32(Console.ReadLine());

            if (optionInput == 1)
            {
                Console.Write("Select an ice cream to modify: ");
                int modifyInput = Convert.ToInt32(Console.ReadLine());
                Order orderObject = OrderDict[customerInput];
                List<IceCream> icecream = IceCreamOrderDict[orderObject];
                //icecream.Add()

                //Finding index of orignal ice cream object
                int icListIndex = 0;
                for (int ic = 0; ic < orderObject.IceCreamList.Count; ic++)
                {
                    if (orderObject.IceCreamList[ic] == icecream[modifyInput-1])
                    {
                        icListIndex = ic;
                    }
                }
                
                orderObject.ModifyIceCream(icListIndex);
                

            }

            else if (optionInput == 2)
            {
                while (true)
                {

                    List<string> flavourlist = new List<string>();
                    List<string> toppingslist = new List<string>();
                    List<Flavour> FlavourObject = new List<Flavour>();
                    List<Topping> ToppingObject = new List<Topping>();


                    Order newOrder = new Order(customerInput, DateTime.Now);
                    Console.Write("Enter an option (Cup, Cone or Waffle): ");
                    string optionInputOpt6 = Console.ReadLine();
                    Console.Write("Enter number of scoops (1-3): ");
                    int scoopInput = Convert.ToInt32(Console.ReadLine());

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
                        CheckFlavour2(flavourInput, quantityInput, flavourlist, FlavourObject);



                    }

                    else if (scoopInput == 2)
                    {
                        Console.Write("Enter quantity: ");
                        int quantityInput = Convert.ToInt32(Console.ReadLine());
                        CheckFlavour2(flavourInput, quantityInput, flavourlist, FlavourObject);
                        if (quantityInput < scoopInput)
                        {

                            /*bool flavourPremium = CheckFlavour(flavourlist);

                            Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);*/
                            Console.Write("Enter ice cream flavour for scoop 2: ");
                            string flavourInput2 = Console.ReadLine();
                            flavourlist.Add(flavourInput2);
                            CheckFlavour2(flavourInput2, quantityInput, flavourlist, FlavourObject);
                        }

                    }
                    else if (scoopInput > 2)
                    {
                        Console.Write("Enter quantity: ");
                        int quantityInput = Convert.ToInt32(Console.ReadLine());
                        CheckFlavour2(flavourInput, quantityInput, flavourlist, FlavourObject);
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
                                CheckFlavour2(flavourInput2, quantityInput2, flavourlist, FlavourObject);
                            }
                            else
                            {
                                flavourlist.Add(flavourInput2);
                                CheckFlavour2(flavourInput2, quantityInput2, flavourlist, FlavourObject);
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

                    if (optionInputOpt6.ToLower() == "cone")
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


                        IceCream newIceCream = new Cone(optionInputOpt6, scoopInput, FlavourObject, ToppingObject, dippedBool);
                        newOrder.AddIceCream(newIceCream);
                    }
                    else if (optionInputOpt6.ToLower() == "waffle")
                    {
                        Console.Write("Enter a waffle flavour: ");
                        string waffleflavourInput = Console.ReadLine();
                        IceCream newIceCream = new Waffle(optionInputOpt6, scoopInput, FlavourObject, ToppingObject, waffleflavourInput);
                        newOrder.AddIceCream(newIceCream);
                    }
                    else if (optionInputOpt6.ToLower() == "cup")
                    {
                        IceCream newIceCream = new Cup(optionInputOpt6, scoopInput, FlavourObject, ToppingObject);
                        newOrder.AddIceCream(newIceCream);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Option.");
                    }

                    //Checking customer membership
                    for (int i = 0; i < customerlist.Count; i++)
                    {
                        if (customerlist[i].Memberid == customerInput)
                        {
                            Customer c = customerlist[i];
                            if (c.Rewards.Tier.ToLower() == "gold")
                            {
                                goldQueue.Enqueue(newOrder);
                            }
                            else
                            {
                                regularQueue.Enqueue(newOrder);
                            }
                            customerlist[i].CurrentOrder = newOrder;
                        }
                    }
                    Console.WriteLine("Order has been made successfully.");


                }
            }
            else if (optionInput == 3)
            {
                //Console.WriteLine("Select ice cream to delete: ");
                //int index = Convert.ToInt32(Console.ReadLine());
                //selectedcustomer.CurrentOrder.DeleteIceCream(index - 1); 

                Console.Write("Select an ice cream to remove: ");
                int modifyInput = Convert.ToInt32(Console.ReadLine());
                Order orderObject = OrderDict[customerInput];
                List<IceCream> icecream = IceCreamOrderDict[orderObject];

                //Finding index of orignal ice cream object
                int icListIndex = 0;
                for (int ic = 0; ic < orderObject.IceCreamList.Count; ic++)
                {
                    if (orderObject.IceCreamList[ic] == icecream[modifyInput - 1])
                    {
                        icListIndex = ic;
                    }
                }

                if (icecream.Count > 1)
                {
                    orderObject.DeleteIceCream(icListIndex);
                    icecream.RemoveAt(modifyInput - 1);
                }
                else
                {
                    Console.WriteLine("You need to have at least one ice cream in the order.");
                }

            }
            else
            {
                Console.WriteLine("Invalid Option.");
            }



        }
        else if (option == 7)
        {
            /*Order currentOrder = new Order();
            Customer selectedcustomer = new Customer();
            
            if (goldQueue.Count > 0)
            {
                currentOrder = goldQueue.Dequeue();
            }
            else if (regularQueue.Count > 0)
            {
                currentOrder = regularQueue.Dequeue();
            }
            if (currentOrder != null)
            {
                Console.WriteLine("Ice Creams in the Order:");
                foreach (IceCream icecream in currentOrder.IceCreamList)
                {
                    Console.WriteLine("Option: {0}", icecream.Option);
                    Console.WriteLine("Scoops: {0}", icecream.Scoops);

                    if (icecream is Cone)
                    {
                        Cone icecreamCone = (Cone)icecream;
                        Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
                    }
                    else if (icecream is Waffle)
                    {
                        Waffle icecreamWaffle = (Waffle)icecream;
                        Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
                    }

                    Console.WriteLine("----- Flavour(s) -----");
                    foreach (Flavour flavour in icecream.Flavours)
                    {
                        Console.WriteLine("Type: {0}  Quantity: {1}", flavour.Type, flavour.Quantity);
                    }

                    Console.WriteLine("----- Topping(s) -----");
                    foreach (Topping topping in icecream.Toppings)
                    {
                        Console.WriteLine(topping.Type);
                    }
                }
                Console.Write($"Total Bill Amount: {currentOrder.CalculateTotal()}");
                Console.Write($"Membership Status: {selectedcustomer.Rewards.Tier} Points: {selectedcustomer.Rewards.Points}");

                if (selectedcustomer.IsBirthday() == true)
                {
                    double totalprice = currentOrder.CalculateTotal();
                    Console.WriteLine(totalprice);
                }
            }
            else
            {
                Console.WriteLine("No orders available.");
            }*/
            Order currentOrder = null;

            if (goldQueue.Count > 0)
            {
                currentOrder = goldQueue.Dequeue();
            }
            else if (regularQueue.Count > 0)
            {
                currentOrder = regularQueue.Dequeue();
            }

            if (currentOrder != null)
            {
                Customer selectedcustomer = null;

                
                foreach (Customer customer in customerlist)
                {
                    if (customer.CurrentOrder != null && customer.CurrentOrder.Id == currentOrder.Id)
                    {
                        selectedcustomer = customer;
                        break; // Exit the loop once a matching customer is found
                    }
                }

                if (selectedcustomer != null)
                {
                    Console.WriteLine("Ice Creams in the Order:");
                    //foreach (IceCream icecream in currentOrder.IceCreamList)
                    //{
                    //    Console.WriteLine("Option: {0}", icecream.Option);
                    //    Console.WriteLine("Scoops: {0}", icecream.Scoops);

                    //    if (icecream is Cone)
                    //    {
                    //        Cone icecreamCone = (Cone)icecream;
                    //        Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
                    //    }
                    //    else if (icecream is Waffle)
                    //    {
                    //        Waffle icecreamWaffle = (Waffle)icecream;
                    //        Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
                    //    }

                    //    Console.WriteLine("----- Flavour(s) -----");
                    //    foreach (Flavour flavour in icecream.Flavours)
                    //    {
                    //        Console.WriteLine("Type: {0}  Quantity: {1}", flavour.Type, flavour.Quantity);
                    //    }

                    //    Console.WriteLine("----- Topping(s) -----");
                    //    foreach (Topping topping in icecream.Toppings)
                    //    {
                    //        Console.WriteLine(topping.Type);
                    //    }
                    //}
                    int customerInput = selectedcustomer.Memberid;
                    if (OrderDict.ContainsKey(customerInput))
                    {
                        Order icecreamOrder = OrderDict[customerInput];
                        List<IceCream> icecreamDetails = IceCreamOrderDict[icecreamOrder];
                        for (int i = 0; i < icecreamDetails.Count; i++)
                        {
                            IceCream details = icecreamDetails[i];
                            Console.WriteLine("---------------[{0}]---------------", i + 1);
                            Console.WriteLine("Option: {0}", details.Option);
                            Console.WriteLine("Scoops: {0}", details.Scoops);
                            if (details is Cone)
                            {
                                Cone icecreamCone = (Cone)details;
                                Console.WriteLine("Dipped: {0}", icecreamCone.Dipped);
                            }
                            else if (details is Waffle)
                            {
                                Waffle icecreamWaffle = (Waffle)details;
                                Console.WriteLine("Waffle Flavour: {0}", icecreamWaffle.WaffleFlavour);
                            }
                            Console.WriteLine();


                            Console.WriteLine("----- Flavour(s) -----");
                            foreach (Flavour f in details.Flavours)
                            {
                                Console.WriteLine("Type: {0} \tQuantity: {1}", f.Type, f.Quantity);
                            }

                            Console.WriteLine("----- Topping(s) -----");
                            foreach (Topping t in details.Toppings)
                            {
                                Console.WriteLine(t.Type);
                            }
                            Console.WriteLine();

                        }

                        double totalbill = currentOrder.CalculateTotal(selectedcustomer);
                        Console.WriteLine($"Total Bill Amount: {totalbill.ToString("F2")}");
                        Console.WriteLine($"Membership Status: {selectedcustomer.Rewards.Tier}  Points: {selectedcustomer.Rewards.Points}");

                        if (selectedcustomer.IsBirthday())
                        {
                            Console.WriteLine("It is your birthday! The expensive ice cream would be free!");
                            double birthdayTotal = currentOrder.CalculateTotal(selectedcustomer);
                            Console.WriteLine($"Total Bill Amount on Birthday: {birthdayTotal.ToString("F2")}");
                        }

                        Console.WriteLine("Press any key to make payment...");
                        Console.ReadKey();

                        // Increment the punch card for every ice cream in the order
                        foreach (var iceCream in currentOrder.IceCreamList)
                        {
                            selectedcustomer.Rewards.Punch();
                        }


                        // Earn points for the order
                        /*int pointsEarned = CalculatePointsEarned(currentOrder, selectedcustomer);*/

                        
                        //using (StreamWriter sw = new StreamWriter("orders.csv", true))
                        //{
                        //    sw.WriteLine($"{currentOrder.Id},{selectedcustomer.Memberid},{currentOrder.TimeReceived},{currentOrder.TimeFulfilled},{},{pointCard.PunchCard}");
                        //}

                        int pointsEarned = (int)Math.Floor(totalbill * 0.72);
                        //selectedcustomer.OrderHistory.Add(currentOrder);


                        Console.WriteLine($"Punch card: {selectedcustomer.Rewards.PunchCard}");
                        Console.WriteLine($"Points earned: {pointsEarned}");

                        currentOrder.TimeFulfilled = DateTime.Now;
                        

                        // Add the fulfilled order to the customer's order history
                        //selectedcustomer.OrderHistory.Add(currentOrder);
                        


                        // Thank the customer
                        Console.WriteLine("Thank you for your order!");

                    }

                    // Additional method to calculate points earned based on the order

                }
                else
                {
                    Console.WriteLine("Error: Customer not found for the order.");
                }
            }
            else
            {
                Console.WriteLine("No orders available.");
            }





        }
        else if (option == 8)
        {
            Console.Write("Enter the year: ");
            int yearInput=Convert.ToInt32(Console.ReadLine());
            foreach(var order in orderHistDict)
            {
                Console.WriteLine($"{order.Key} {order.Value}");
            }
            
            




        }

        else if (option > 8)
        {
            Console.WriteLine("Please enter an option from 0 to 8");

        }
    }

    catch (FormatException ex)
    {
        Console.WriteLine(ex.Message);
    }


 }















