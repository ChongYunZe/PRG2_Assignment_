using PRG2_Assignment;
using System;
using System.Numerics;
//Daniel --> Features 1, 3, 4
//YunZe --> Features 2, 5, 6

int option;
List<Customer> customerlist = new List<Customer>(); //From option 1
Dictionary<int ,Order> orderDict = new Dictionary<int, Order> (); //From option 2

void DisplayMenu()
{
    Console.WriteLine("-------------I.C.Treats Menu--------------");
    Console.WriteLine("[1] List all customers");
    Console.WriteLine("[2] List all current orders");
    Console.WriteLine("[3] Register a new customer");
    Console.WriteLine("[4] Create a customer's order");
    Console.WriteLine("[5] Display order details of a customer");
    Console.WriteLine("[6] Modify order details");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Please enter an option: ");
    option = Convert.ToInt32(Console.ReadLine());

}


while (true)
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

        catch (FileNotFoundException ex )
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
                    Console.WriteLine("{0, -4} {1, -10} {2, -18} {3, -18} {4, -8} {5, -7} {6, -7} {7, -15} {8, -12} {9, -12} {10, -12} {11, -10} {12, -10} {13, -10} {14, -10}", headingOrders[0], headingOrders[1], headingOrders[2], headingOrders[3], headingOrders[4], headingOrders[5], headingOrders[6], headingOrders[7], headingOrders[8], headingOrders[9], headingOrders[10], headingOrders[11], headingOrders[12], headingOrders[13], headingOrders[14]);

                    while ((sOrders = srOrders.ReadLine()) != null)
                    {
                        string[] valuesOrders = sOrders.Split(",");
                        Order orders = new Order(Convert.ToInt32(valuesOrders[0]), Convert.ToDateTime(valuesOrders[2]));
                        orderDict[Convert.ToInt32(valuesOrders[0])] = orders;
                    }
                    foreach (Order order in orderDict.Values)
                    {
                        Console.WriteLine(order.ToString());
                    }

                    
                }

            }

        }

        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
}




