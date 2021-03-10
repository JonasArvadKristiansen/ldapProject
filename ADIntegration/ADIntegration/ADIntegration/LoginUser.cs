using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.DirectoryServices.AccountManagement;

namespace ADIntegration
{
    class LoginUser
    {
        public static Tuple<DirectorySearcher, string> Login()
        {
            Console.Clear();
            // Our server LDAP ip that we want a connection to.
            string ip = "LDAP://192.168.132.10";


            Console.WriteLine("Enter Username: ");
            string user = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            // Making our entry with the ip, username and password
            DirectoryEntry entry = new DirectoryEntry(ip, user, password);

            // Creating our searcher with our entry from above
            DirectorySearcher searcher = new DirectorySearcher(entry);

            // Creates a collection where we can save the queries
            SearchResultCollection results;

            // Sets search filter to everyone in our AD
            searcher.Filter = "(&(objectCategory=User)(objectClass=person))";
            try
            {
                results = searcher.FindAll();

                // Foreach results we find.
                foreach (SearchResult result in results)
                {
                    // If the found result name is equal to the username we entered, then create a string variable with what group the person belongs to.
                    if (result.Properties["SamAccountName"][0].ToString().ToLower() == user.ToLower())
                    {
                        string memberOf = result.Properties["memberof"][0].ToString();
                        
                        // Returns our Tuple with our DirectorySearcher and our string variable
                        return Tuple.Create(searcher, memberOf);
                    }
                }

                return null;
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account dosent exist or you dont have permission to view this :(");
                Console.ResetColor();
                Console.WriteLine("Press any key to try again or 'Enter' to exit");
                if (Console.ReadKey().Key != ConsoleKey.Enter)
                    Login();

                return null;
            }
              
        }

    }
}
