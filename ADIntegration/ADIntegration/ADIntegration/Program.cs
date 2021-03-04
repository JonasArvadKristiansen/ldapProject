using System;
using System.DirectoryServices;

namespace ADIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            App();
        }

        static void App()
        {
            Console.Clear();
            //Name, Mail, Mobile, Telephone, Address, Postal
            SearchResultCollection results;
            string ip = "LDAP://192.168.132.10";

            Console.WriteLine("Enter Username: ");
            string user = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("How do you want to lookup this person? Mail/Name/Mobile: ");
            string searchOption = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Value: ");
            string input = Console.ReadLine();

            Console.Clear();

            string[] options = { "Name", "Mail", "Mobile", "TelephoneNumber", "StreetAddress", "PostalCode" };

            try
            {
                DirectoryEntry de = new DirectoryEntry(ip, user, password);
                DirectorySearcher ds = new DirectorySearcher(de);

                switch (searchOption.ToLower())
                {
                    case "mail":
                        ds.Filter = "(&(objectCategory=User)(objectClass=person)(mail=" + input + "*))";
                        break;
                    case "name":
                        ds.Filter = "(&(objectCategory=User)(objectClass=person)(name=" + input + "*))";
                        break;
                    case "mobile":
                        ds.Filter = "(&(objectCategory=User)(objectClass=person)(mobile=" + input + "*))";
                        break;

                }

                results = ds.FindAll();

                foreach (SearchResult res in results)
                {
                    foreach (string option in options)
                    {
                        try
                        {
                            Console.WriteLine($"{option}: {res.Properties[option][0].ToString()}");
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{option} is missing");
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Something went wrong :(");
                Console.WriteLine("Press any key to try again");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    App();
                }
            }

        }
    }
}
