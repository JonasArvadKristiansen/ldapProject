using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ADIntegration
{
    class LoginUser
    {

        public static DirectorySearcher Login()
        {
            string user = null;
            string password = null;
            DirectorySearcher searcher = null;
            DirectoryEntry entry = null;
            SearchResultCollection results = null;

            Console.Clear();
            //Name, Mail, Mobile, Telephone, Address, Postal
            string ip = "LDAP://192.168.132.10";

            Console.WriteLine("Enter Username: ");
            user = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Enter Password: ");
            password = Console.ReadLine();

            entry = new DirectoryEntry(ip, user, password);
            searcher = new DirectorySearcher(entry);


            // If login fails... Dosent work yet.
            try
            {
                results = searcher.FindAll();

                return searcher;
            }
            catch
            {
                Login();
                return null;
            }
              

        }

    }
}
