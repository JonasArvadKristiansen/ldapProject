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
            results = searcher.FindAll();

            try
            {
                foreach (SearchResult result in results)
                {
                    //Console.WriteLine(result.Properties["name"][0]);
                    //Console.WriteLine(result.Properties["memberof"][0]+ "\n");
                    if (result.Properties["SamAccountName"][0].ToString().ToLower() == user.ToLower())
                    {
                        string memberOf = result.Properties["memberof"][0].ToString();
                        return Tuple.Create(searcher, memberOf);
                    }
                }
                return null;
            }
            catch
            {
                Console.WriteLine("TESSSSSSSSSSST");
                Console.ReadKey();
                Program.App();
                return null;
            }
              
        }

    }
}
