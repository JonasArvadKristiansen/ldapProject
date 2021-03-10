﻿using System;
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

            if (memberOf.Contains("CN=Administration"))
                searcher.Filter = $"(&(objectCategory=User)(objectClass=person)(" + searchOption.ToLower() + "=" + searchValue + "*))";
            else
                searcher.Filter = $"(&(objectCategory=User)(objectClass=person)(memberOf={memberOf.ToLower()})(" + searchOption.ToLower() + "=" + searchValue + "*))";
            
                try
                {
                    results = searcher.FindAll();
                    foreach (SearchResult res in results)
                    {
                        foreach (string option in options)
                        {
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
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No access :(");
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
