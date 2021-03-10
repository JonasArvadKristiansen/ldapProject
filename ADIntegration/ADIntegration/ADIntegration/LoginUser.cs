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
            //Name, Mail, Mobile, Telephone, Address, Postal
            string ip = "LDAP://192.168.132.10";


            Console.WriteLine("Enter Username: ");
            string user = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            DirectoryEntry entry = new DirectoryEntry(ip, user, password);
            DirectorySearcher searcher = new DirectorySearcher(entry);
            SearchResultCollection results;
            searcher.Filter = "(&(objectCategory=User)(objectClass=person))";
            string memberOf = null;
            try
            {
                results = searcher.FindAll();
                foreach (SearchResult result in results)
                {
                    //Console.WriteLine(result.Properties["name"][0]);
                    //Console.WriteLine(result.Properties["memberof"][0]+ "\n");
                    if (result.Properties["SamAccountName"][0].ToString().ToLower() == user.ToLower())
                    {
                        memberOf = result.Properties["memberof"][0].ToString();
                        
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
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    Login();
                }
                return null;
            }
              
        }

    }
}
