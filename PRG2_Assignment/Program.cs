using PRG2_Assignment;
using System;
using System.Numerics;
using System.Xml.Linq;
//Daniel --> Features 1, 3, 4
//YunZe --> Features 2, 5, 6

//==========================================================
// Student Number : S10257927 & S10258472
// Student Name : Chong Yun Ze
// Partner Name : Daniel Sha
//==========================================================

//Test

int option;
List<Customer> customerlist = new List<Customer>(); //From option 1
Dictionary<int ,Order> orderDict = new Dictionary<int, Order> (); //From option 2

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
            Console.WriteLine("{0,-15} {1,-15} {2,-15}",
                heading[0], heading[1], heading[2]);

            while ((s = sr.ReadLine()) != null)
            {
                string[] values = s.Split(',');
                Customer customer = new(values[0], Convert.ToInt32(values[1]), Convert.ToDateTime(values[2]));
                customerlist.Add(customer);
            }

            foreach (Customer customer in customerlist)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-15}",
                customer.Name, customer.Memberid, customer.Dob);


            }
            Console.WriteLine();
        }

    }
}

while (true)
{
    try
    {
        DisplayMenu();
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
                using (StreamReader srOrders = new StreamReader("orders.csv"))
                {
                    string? sOrders = srOrders.ReadLine();
                    if (sOrders != null)
                    {
                        string[] headingOrders = sOrders.Split(',');
                        Console.WriteLine("{0, -4} {1, -10} {2, -18} ", headingOrders[0], headingOrders[1], headingOrders[2]);

                        while ((sOrders = srOrders.ReadLine()) != null)
                        {
                            string[] valuesOrders = sOrders.Split(",");
                            Order orders = new Order(Convert.ToInt32(valuesOrders[0]), Convert.ToDateTime(valuesOrders[2]));
                            orderDict[Convert.ToInt32(valuesOrders[1])] = orders;
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
            ReadingCustomerFile();
            Console.WriteLine("Please select a customer: ");
            string customer = Console.ReadLine();

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










