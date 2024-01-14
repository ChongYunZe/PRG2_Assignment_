using PRG2_Assignment;
using System;
using System.Numerics;
//Daniel --> Features 1, 3, 4
//YunZe --> Features 2, 5, 6

int option;
List<Customer> customerlist = new List<Customer>();
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
}




