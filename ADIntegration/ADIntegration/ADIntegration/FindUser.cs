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
        public static void Find(List<User> users, string firstname)
        {
            Console.Clear();

            Console.WriteLine("Enter Name, Mail or Mobile number here");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Search: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string searchValue = Console.ReadLine();
            Console.ResetColor();

            Console.Clear();

            string group = "";

            // Gets the current logged in user group
            foreach (User user in users)
            {
                if (user.firstname == firstname)
                {
                    if (user.memberOf.Contains("CN=Administration"))
                    {
                        group = "Administration";
                    } 
                    else if (user.memberOf.Contains("CN=Ledelse"))
                    {
                        group = "Ledelse";
                    }
                }
            }

            // Handling user permissions
            foreach (User user in users)
            {
                if (user.firstname.ToLower() == searchValue.ToLower() || user.mail.ToLower() == searchValue.ToLower() || user.mobile.ToLower() == searchValue.ToLower())
                {

                    if (user.memberOf.Contains("Administration"))
                    {
                        if (group == "Administration")
                        {
                            InfoController(user);
                        }

                        if (group == "Ledelse")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("You dont have permission to see ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(user.name);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(" information\n");
                            Console.ResetColor();
                            Console.WriteLine("Press any key to return or enter to exit");
                            if (Console.ReadKey().Key != ConsoleKey.Enter)
                            {
                                Find(users, firstname);
                            }
                        }
                    }

                    if (user.memberOf.Contains("Ledelse"))
                    {
                        if (group == "Administration")
                        {
                            InfoController(user);
                        }

                        if (group == "Ledelse")
                        {
                            InfoController(user);
                        }
                    }

                }

            }

            Console.WriteLine("Press any key to continue or enter to exit");
            Console.ResetColor();
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Find(users, firstname);
            }

        }

        public static void InfoController(User user)
        {
            Console.WriteLine("Name: " + user.firstname);
            Console.WriteLine("Mail: " + user.mail);
            Console.WriteLine("Mobile: " + user.mobile);
            Console.WriteLine("Telephone: " + user.streetAddress);
            Console.WriteLine("Postal Code: " + user.postalCode);
            Console.WriteLine("Group: " + user.memberOf);
            Console.WriteLine();
        }
    }
}
