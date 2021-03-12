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
        public static Tuple<List<User>, string> Login()
        {

            Console.Clear();
            // Our server LDAP ip that we want a connection to.
            string ip = "LDAP://192.168.132.10";

            Console.WriteLine("Enter Username: ");
            string firstname = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            // Making our entry with the ip, username and password
            DirectoryEntry entry = new DirectoryEntry(ip, firstname, password);

            // Creating our searcher with our entry from above
            DirectorySearcher searcher = new DirectorySearcher(entry);

            // Creates a collection where we can save the queries
            SearchResultCollection results;

            // Sets search filter to everyone in our AD
            searcher.Filter = "(&(objectCategory=User)(objectClass=person))";
            
            try {
                results = searcher.FindAll();
                return Tuple.Create(UserData(results), firstname);
            }
            catch {
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
        public static List<User> UserData(SearchResultCollection results) {
            List<User> users = new List<User>();
            string[] options = { "givenName", "Name", "Mail", "Mobile", "TelephoneNumber", "StreetAddress", "PostalCode", "Memberof" };

            string FirstName = "";
            string Name = "";
            string Mail = "";
            string Mobile = "";
            string TelephoneNumber = "";
            string StreetAddress = "";
            string PostalCode = "";
            string Memberof = "";

            string[] subcat = {FirstName, Name, Mail, Mobile, TelephoneNumber, StreetAddress, PostalCode, Memberof};

            foreach (SearchResult result in results){
                for(int i = 0; i < options.Length; i++) {
                    if (result.Properties[options[i]].Count <= 0)
                        subcat[i] = "No data found";
                    else
                        subcat[i] = result.Properties[options[i]][0].ToString();
                }
                User person = new User(subcat[0], subcat[1], subcat[2], subcat[3], subcat[4], subcat[5], subcat[6], subcat[7]);
                users.Add(person);
            }
            /*foreach(User user in users)
                Console.WriteLine($"{user.name}, {user.mail}, {user.mobile}, {user.telephone}, {user.streetAddress}, {user.postalCode}, {user.memberOf},");*/
            return users;
        }
    }
}
