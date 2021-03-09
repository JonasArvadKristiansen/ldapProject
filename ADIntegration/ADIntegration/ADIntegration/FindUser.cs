using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADIntegration
{
    public class FindUser
    {
        public static void Find(DirectorySearcher searcher, string memberOf)
        {

            Console.Clear();
            Console.WriteLine("How do you want to lookup this person? Mail/Name/Mobile: ");
            string searchOption = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Value: ");
            string searchValue = Console.ReadLine();

            Console.Clear();
            SearchResultCollection results;
            string[] options = { "Name", "Mail", "Mobile", "TelephoneNumber", "StreetAddress", "PostalCode", "memberof" };
            searcher.Filter = $"(&(objectCategory=User)(sAMAccountName=*)(objectClass=person)(memberof={memberOf})(" + searchOption.ToLower()+"=" + searchValue + "*))";
            //searcher.Filter = $"(&(objectCategory=Person)({memberOf})(" + searchOption.ToLower() + "=" + searchValue + "*))";
            results = searcher.FindAll();
            try
            {
                foreach (SearchResult res in results)
                {
                    foreach (string option in options)
                    {
                        try
                        {
                            if (option.ToLower() == "memberof")
                            {
                                Console.WriteLine($"{option}: {res.Properties["memberof"][0].ToString()}");
                                Console.WriteLine($"{option}: {res.Properties["memberof"][1].ToString()}");

                            }
                            else
                            {
                                Console.WriteLine($"{option}: {res.Properties[option][0].ToString()}");
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{option} is missing");
                            Console.ResetColor();
                        }
                    }

                    Console.WriteLine();
                }
            }
            catch
            {
                Console.WriteLine("No access :(");
                Console.ReadKey();
            }

            Console.WriteLine("Press any key to try again or 'Enter' to exit");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Find(searcher, memberOf);
            }

        }

    }
}
