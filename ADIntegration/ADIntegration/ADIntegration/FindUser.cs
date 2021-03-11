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
    class FindUser
    {
        public static void Find(DirectorySearcher searcher, string memberOf, List<User> users)
        {
            Console.Clear();
            Console.WriteLine("How do you want to lookup this person? Mail/Name/Mobile: ");
            string searchOption = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Value: ");
            string searchValue = Console.ReadLine();

            Console.Clear();

            // If the logged in person group is Administration then show him every user we got.
            if (memberOf.Contains("CN=Administration"))
                searcher.Filter = $"(&(objectCategory=User)(objectClass=person)(" + searchOption.ToLower() + "=" + searchValue + "*))";
            else
                searcher.Filter = $"(&(objectCategory=User)(objectClass=person)(memberOf={memberOf.ToLower()})(" + searchOption.ToLower() + "=" + searchValue + "*))";
        }
    }
}
