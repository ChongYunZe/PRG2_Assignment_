using PRG2_Assignment;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
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
Dictionary<int ,Order> orderHistDict = new Dictionary<int, Order> (); //From option 2
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

void ReadingCustomerFile()
{
    using (StreamReader sr = new StreamReader("customers.csv"))
    {
        string? s = sr.ReadLine();
        if (s != null)
        {
            string[] heading = s.Split(',');
            Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
                heading[0], heading[1], heading[2], heading[3], heading[4], heading[5]);
            
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
                    if (valuesOrders[4] == "Cone")
                    {
                        Console.WriteLine("Dipped: ", valuesOrders[6]);
                    }
                    else if (valuesOrders[4] == "Waffle")
                    {
                        Console.WriteLine("Waffle Flavour: ", valuesOrders[7]);
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

bool CheckFlavour(List<string> flavourlist)
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
                foreach (Customer customer in customerlist)
                {
                    Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
                    customer.Name, customer.Memberid, customer.Dob, customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);



                }
                Console.WriteLine();

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
                }

                Console.WriteLine();

                Console.WriteLine("============= Regular Queue ==============");
                foreach (Order order in regularQueue)
                {
                    Console.WriteLine(order);

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

            foreach (Customer customer in customerlist)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
                customer.Name, customer.Memberid, customer.Dob, customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);



            }
            Console.WriteLine();
            Console.Write("Please select a customer (Enter Member ID): ");
            int customeridInput = Convert.ToInt32(Console.ReadLine());
            while (true)
            {

                List<string> flavourlist = new List<string>();
                List<string> toppingslist = new List<string>();
                List<Flavour> FlavourObject = new List<Flavour>();
                List<Topping> ToppingObject = new List<Topping>();


                Order newOrder = new Order(customeridInput, DateTime.Now);
                Console.Write("Enter an option (Cup, Cone or Waffle): ");
                string optionInput = Console.ReadLine();
                Console.Write("Enter number of scoops (1-3): ");
                int scoopInput = Convert.ToInt32(Console.ReadLine());
                
                /*Console.Write("Enter quantity (1-3): ");
                int quantityInput = Convert.ToInt32(Console.ReadLine());*/
                
                Console.Write("Enter ice cream flavour: ");
                string flavourInput = Console.ReadLine();
                if (scoopInput == 1)
                {

                    bool flavourPremium = CheckFlavour(flavourlist);

                    Flavour newFlavour = new Flavour(flavourInput, flavourPremium, scoopInput);
                    flavourlist.Add(flavourInput);
                    


                }

                else if (scoopInput == 2)
                {
                    Console.Write("Enter quantity: ");
                    int quantityInput = Convert.ToInt32(Console.ReadLine());
                    if (quantityInput < scoopInput)
                    {

                        bool flavourPremium = CheckFlavour(flavourlist);

                        Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);
                        Console.Write("Enter ice cream flavour for scoop 2: ");
                        string flavourInput2 = Console.ReadLine();
                        flavourlist.Add(flavourInput2);
                    }
                    
                }
                else if (scoopInput > 2)
                {
                    Console.Write("Enter quantity: ");
                    int quantityInput = Convert.ToInt32(Console.ReadLine());
                    while (quantityInput < scoopInput)
                    {

                        bool flavourPremium = CheckFlavour(flavourlist);

                        Flavour newFlavour = new Flavour(flavourInput, flavourPremium, quantityInput);
                        Console.WriteLine("Enter ice cream flavour for scoop 2: ");
                        string flavourInput2 = Console.ReadLine();
                        flavourlist.Add(flavourInput2);
                        Console.Write("Enter quantity: ");
                        int quantityInput2 = Convert.ToInt32(Console.ReadLine());
                        if (quantityInput2 > 1)
                        {
                            for (int i=0; i < quantityInput2; i++)
                            {
                                flavourlist.Add(flavourInput2);
                            }
                        }
                        else
                        {
                            flavourlist.Add(flavourInput2);
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


                    IceCream newIceCream = new Cone(optionInput, scoopInput,FlavourObject ,ToppingObject , dippedBool);
                    newOrder.AddIceCream(newIceCream);
                }
                else if (optionInput.ToLower() == "waffle")
                {
                    Console.Write("Enter a waffle flavour: ");
                    string waffleflavourInput = Console.ReadLine();
                    IceCream newIceCream = new Waffle(optionInput, scoopInput, FlavourObject, ToppingObject, waffleflavourInput);
                    newOrder.AddIceCream(newIceCream);
                }
                else if (optionInput.ToLower() == "cup")
                {
                    IceCream newIceCream = new Cup(optionInput, scoopInput,FlavourObject, ToppingObject);
                    newOrder.AddIceCream(newIceCream);
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
                    }
                }
                Console.WriteLine("Order has been made successfully.");
                if (AddOrderInput.ToUpper() == "N")
                {
                    break;
                }
                
            }
        }


        
        else if (option == 5)
        {
            foreach (Customer customer in customerlist)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
                customer.Name, customer.Memberid, customer.Dob, customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);



            }
            Console.WriteLine();
            Console.Write("Please select a customer (Enter Member ID): ");
            int customerInput = Convert.ToInt32(Console.ReadLine());
            int customerIndex = 0;
            for (int i = 0; i < customerlist.Count; i++)
            {
                if (customerInput == Convert.ToInt32(customerlist[i].Memberid))
                {
                    customerIndex = i;
                    break;
                }
            }
            Console.WriteLine("============= Current ==============");
            if (customerlist[customerIndex].Rewards.Tier == "Gold")
            {
                foreach (Order order in goldQueue)
                {
                    if (customerlist[customerIndex].Memberid == customerInput)
                    {
                        Console.WriteLine(order);
                    }

                }
            }
            else
            {
                foreach (Order order in regularQueue)
                {
                    if (customerlist[customerIndex].Memberid == customerInput)
                    {
                        Console.WriteLine(order);
                    }
                }
            }
            



            Console.WriteLine();

            

            Console.WriteLine("============== Past ================");
            OrderHistory(customerInput);
        }

        else if (option == 6)
        {
            foreach (Customer customer in customerlist)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
                customer.Name, customer.Memberid, customer.Dob, customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);



            }
            Console.WriteLine();
            Console.WriteLine("Select a Customer ID: ");
            int customerInput = Convert.ToInt32(Console.ReadLine());
            Customer selectedcustomer = null;

            for (int i = 0; i < customerlist.Count; i++)
            {
                if (customerlist[i].Memberid == customerInput)
                {
                    selectedcustomer = customerlist[i];
                    Console.WriteLine(selectedcustomer.CurrentOrder);

                }
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



//else if (option == 2)
//{
//    try
//    {
//        /*Console.WriteLine("=============== Gold Queue ===============");
//        foreach(order in goldQueue)
//        {
//            Console.WriteLine(order);
//        }
//        Console.WriteLine("============= Regular Queue ==============");
//        foreach (order in goldQueue)
//        {
//            Console.WriteLine(order);
//        }*/










