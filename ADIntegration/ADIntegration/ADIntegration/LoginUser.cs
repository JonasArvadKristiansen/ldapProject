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
            results = searcher.FindAll();
            UserData(results);
            try
            {
                
                // Foreach results we find.
                foreach (SearchResult result in results) {
                    string name = result.Properties["name"][0].ToString();
                    string mail = result.Properties["mail"][0].ToString();
                    int mobile = int.Parse(result.Properties["mobile"][0].ToString());
                    int telephone = int.Parse(result.Properties["telephoneNumber"][0].ToString());
                    string streetAddress = result.Properties["streetAddress"][0].ToString();
                    int postalCode = int.Parse(result.Properties["postalCode"][0].ToString());
                    string memberOf = result.Properties["memberOf"][0].ToString();
                    //User person = new User(name, mail, mobile, telephone, streetAddress, postalCode, memberOf);
                    
                }
                return null;
            }
            catch{
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
        public static void UserData(SearchResultCollection results)
        {
            List<User> users = new List<User>();
            string[] options = { "Name", "Mail", "Mobile", "TelephoneNumber", "StreetAddress", "PostalCode", "memberof" };




            foreach (SearchResult result in results)
            {
                foreach(string op in options)
                {
                    if (result.Properties[op].Count <= 0)
                    {
                        Console.WriteLine("I WIN");
                    }
                    else
                        Console.WriteLine("I LOST");
                }
                Console.WriteLine(result.Properties["Mail"].Count);
               
            }
        }
    }
}
