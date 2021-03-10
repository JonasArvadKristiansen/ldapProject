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

            // Array of all the options we want to be displayed.
            string[] options = { "Name", "Mail", "Mobile", "TelephoneNumber", "StreetAddress", "PostalCode", "memberof" };

            // If the logged in person group is Administration then show him every user we got.
            if (memberOf.Contains("CN=Administration"))
                searcher.Filter = $"(&(objectCategory=User)(objectClass=person)(" + searchOption.ToLower() + "=" + searchValue + "*))";
            else
                searcher.Filter = $"(&(objectCategory=User)(objectClass=person)(memberOf={memberOf.ToLower()})(" + searchOption.ToLower() + "=" + searchValue + "*))";

            results = searcher.FindAll();

            // If we get no results, throw us an alert.
            if (results.Count > 0)
            {
                // Foreach result in our results collection.
                foreach (SearchResult res in results)
                {
                    // Foreach option in our options array.
                    foreach (string option in options)
                    {
                        // If the infomation exist write it out or write out the information dosent exist.
                        try
                        {

                            Console.WriteLine($"{option}: {res.Properties[option][0].ToString()}");

                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{option} is missing");
                            Console.ResetColor();
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to try again or 'Enter' to exit");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    Find(searcher, memberOf);
                }
            } else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account dosent exist :(");
                Console.ResetColor();
                Console.WriteLine("Press any key to try again or 'Enter' to exit");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    Find(searcher, memberOf);
                }
            }


        }

    }
}
