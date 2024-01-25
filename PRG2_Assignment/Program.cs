using PRG2_Assignment;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;
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
Dictionary<int ,Order> orderDict = new Dictionary<int, Order> (); //From option 2
//Queue<> goldQueue = new Queue<>();
//Queue<> regularQueue = new Queue<>();
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
    Console.WriteLine("[a] Process an order and checkout");
    Console.WriteLine("[b] Display monthly charged amounts breakdown & total charged amounts for the year");
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
                //customerdict.Add(customer.Name, customer);
                
            }

            foreach (Customer customer in customerlist)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-20} {4,-20} {5,-15}",
                customer.Name, customer.Memberid, customer.Dob, customer.Rewards.Tier, customer.Rewards.Points, customer.Rewards.PunchCard);
                


            }
            Console.WriteLine();
        }

    }
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
                ReadingCustomerFile();

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
                /*Console.WriteLine("=============== Gold Queue ===============");
                foreach(order in goldQueue)
                {
                    Console.WriteLine(order);
                }
                Console.WriteLine("============= Regular Queue ==============");
                foreach (order in goldQueue)
                {
                    Console.WriteLine(order);
                }*/


                using (StreamReader srOrders = new StreamReader("orders.csv"))
                {
                    string? sOrders = srOrders.ReadLine();
                    if (sOrders != null)
                    {
                        //string[] headingOrders = sOrders.Split(',');
                        //Console.WriteLine("{0, -4} {1, -10} {2, -18} ", headingOrders[0], headingOrders[1], headingOrders[2]);

                        while ((sOrders = srOrders.ReadLine()) != null)
                        {
                            string[] valuesOrders = sOrders.Split(",");
                            Order orders = new Order(Convert.ToInt32(valuesOrders[0]), Convert.ToDateTime(valuesOrders[2]));
                            orderDict[Convert.ToInt32(valuesOrders[1])] = orders;
                            customer.OrderHistory.Add(Order)
                        }
                        foreach (KeyValuePair<int, Order> kvp in orderDict)
                        {
                            Console.WriteLine("{0, -4} {1, -10} {2, -18} ", kvp.Value.Id, kvp.Key, kvp.Value.TimeReceived);
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
                Console.WriteLine("Please enter your name: ");
                string customername = Console.ReadLine();
                Console.WriteLine("Please enter your id: ");
                int customermemberid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter your date of birth (dob): ");
                //DateTime.Parse makes sure the date entered is correctly read
                DateTime customerdob = DateTime.Parse(Console.ReadLine());

                //creating customer object
                Customer newcustomer = new Customer(customername, customermemberid, customerdob);
                PointCard pointCard = new PointCard();
                newcustomer.Rewards = pointCard;

                using (StreamWriter sw = new StreamWriter("customers.csv", true))
                {
                    sw.WriteLine($"{customername},{customermemberid},{customerdob:dd/MM/yyyy}");
                }

                // Displaying registration status
                Console.WriteLine("Customer registered successfully!");
            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        else if (option == 4)
        {
            ReadingCustomerFile(); //Method to read the customer.csv file
            Console.WriteLine("Please select a customer: ");
            string customer = Console.ReadLine();
            //hello

            //   customer.OrderHistory.Add(Order)
        }

        else if (option > 6)
        {
            Console.WriteLine("Please enter an option from 0 to 6");

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


//        using (StreamReader srOrders = new StreamReader("orders.csv"))
//        {
//            string? sOrders = srOrders.ReadLine();
//            if (sOrders != null)
//            {
//                //string[] headingOrders = sOrders.Split(',');
//                //Console.WriteLine("{0, -4} {1, -10} {2, -18} ", headingOrders[0], headingOrders[1], headingOrders[2]);

//                while ((sOrders = srOrders.ReadLine()) != null)
//                {
//                    string[] valuesOrders = sOrders.Split(",");
//                    Order orders = new Order(Convert.ToInt32(valuesOrders[0]), Convert.ToDateTime(valuesOrders[2]));
//                    orderDict[Convert.ToInt32(valuesOrders[1])] = orders;
//                    customer.OrderHistory.Add(Order)
//                        }
//                foreach (KeyValuePair<int, Order> kvp in orderDict)
//                {
//                    Console.WriteLine("{0, -4} {1, -10} {2, -18} ", kvp.Value.Id, kvp.Key, kvp.Value.TimeReceived);
//                }


//            }

//        }

//    }

//    catch (FileNotFoundException ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}

//else if (option == 3)
//{
//    try
//    {
//        Console.WriteLine("Please enter your name: ");
//        string customername = Console.ReadLine();
//        Console.WriteLine("Please enter your id: ");
//        int customermemberid = Convert.ToInt32(Console.ReadLine());
//        Console.WriteLine("Please enter your date of birth (dob): ");
//        //DateTime.Parse makes sure the date entered is correctly read
//        DateTime customerdob = DateTime.Parse(Console.ReadLine());

//        //creating customer object
//        Customer newcustomer = new Customer(customername, customermemberid, customerdob);
//        PointCard pointCard = new PointCard();
//        newcustomer.Rewards = pointCard;

//        using (StreamWriter sw = new StreamWriter("customers.csv", true))
//        {
//            sw.WriteLine($"{customername},{customermemberid},{customerdob:dd/MM/yyyy}");
//        }

//        // Displaying registration status
//        Console.WriteLine("Customer registered successfully!");
//    }

//    catch (FileNotFoundException ex)
//    {
//        Console.WriteLine(ex.Message);
//    }


//}

//else if (option == 4)
//{
//    ReadingCustomerFile(); //Method to read the customer.csv file
//    Console.WriteLine("Please select a customer: ");
//    string customer = Console.ReadLine();


//}

//else if (option == 5)
//{
//    ReadingCustomerFile();
//    Console.Write("Please select a customer: ");
//    int customerInput = Convert.ToInt32(Console.ReadLine());
//    for (int i = 0; i < customerlist.Count; i++)
//    {
//        if (customerInput == Convert.ToInt32(customerlist[i].Memberid))
//        {

//        }
//    }
    

//    //   customer.OrderHistory.Add(Order)
//}







