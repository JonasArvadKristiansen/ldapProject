using System;
using System.DirectoryServices;

namespace ADIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            //Name, Mail, Mobile, Telephone, Address, Postal
            SearchResultCollection results;
            string ip = "LDAP://192.168.132.10";
            string user = "Administrator";
            string password = "Password1";
            string[] options = { "Name", "Mail", "Mobile", "TelephoneNumber", "StreetAddress", "PostalCode" };

            DirectoryEntry de = new DirectoryEntry(ip, user, password);
            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&(objectCategory=User)(objectClass=person))";
            results = ds.FindAll();

            foreach (SearchResult res in results)
            {
                foreach (string option in options)
                {
                    try{
                        Console.WriteLine($"{option}: {res.Properties[option][0].ToString()}");
                    }
                    catch{
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{option} is missing");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();

        }
    }
}
