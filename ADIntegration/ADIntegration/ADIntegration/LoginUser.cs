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

            try
            {
                searcher.FindAll();
                return searcher;
            }
            catch
            {
                Program.App();
                return null;
            }
              
        }

    }
}
