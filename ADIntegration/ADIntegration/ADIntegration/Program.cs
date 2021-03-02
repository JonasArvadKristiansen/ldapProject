using System;
using System.DirectoryServices;

namespace ADIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            SearchResultCollection results;
            string ip = "LDAP://192.168.132.10";
            string user = "Administrator";
            string password = "Password1";
            DirectoryEntry de = new DirectoryEntry(ip, user, password);
            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&(objectCategory=User)(objectClass=person))";
            results = ds.FindAll();
            foreach(SearchResult res in results) 
            {
                Console.WriteLine(res.Properties["name"][0].ToString());
            }
            Console.ReadKey();

        }
    }
}
