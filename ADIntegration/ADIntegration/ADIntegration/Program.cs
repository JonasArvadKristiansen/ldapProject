using System;
using System.DirectoryServices;

namespace ADIntegration
{
    class Program
    {
        public static void Main(string[] args)
        {
            App();
        }

        public static void App()
        {
            Console.Clear();
            SearchResultCollection results;
            string ip = "LDAP://192.168.132.10";
            string user = "Administrator";
            string pass = "Password1";
            DirectoryEntry de = new DirectoryEntry(ip, user, pass);
            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&(objectCategory=User)(objectClass=person))";
            results = ds.FindAll();
            foreach (SearchResult res in results)
            {
                Console.WriteLine(res.Properties["name"][0].ToString());

            }
            Console.WriteLine();

            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.Clear();

            foreach (SearchResult res in results)
            {
                if (res.Properties["name"][0].ToString() == username)
                {
                    try
                    {
                        Console.WriteLine("Name: " + res.Properties["name"][0].ToString());
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Name: None");
                        Console.ResetColor();
                    }

                    try
                    {
                        Console.WriteLine("Mail: " + res.Properties["mail"][0].ToString());
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Mail: None");
                        Console.ResetColor();
                    }

                    try
                    {
                        Console.WriteLine("Mobile: " + res.Properties["mobile"][0].ToString());
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Mobile: None");
                        Console.ResetColor();
                    }

                    try
                    {
                        Console.WriteLine("Telephone: " + res.Properties["telephoneNumber"][0].ToString());
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Telephone: None");
                        Console.ResetColor();
                    }

                    try
                    {
                        Console.WriteLine("Address: " + res.Properties["streetAddress"][0].ToString());
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Address: None");
                        Console.ResetColor();
                    }

                    try
                    {
                        Console.WriteLine("Postal: " + res.Properties["postalCode"][0].ToString());
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Postal: None");
                        Console.ResetColor();
                    }


                }

            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to go back");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                App();
            }

        }

    }
}
