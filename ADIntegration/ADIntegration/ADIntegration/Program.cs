using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Threading;

namespace ADIntegration
{
    class Program
    {
        static void Main(string[] args) {
            App();
        }

        public static void App() {
            // Defines login variable and sets it to the tuple we return in our function
            Tuple<DirectorySearcher, string, List<User>> login = LoginUser.Login();
            Console.WriteLine(login.Item3.Count);

            // Running the find function with our tuple items as a parameter
            FindUser.Find(login.Item1, login.Item2, login.Item3);
        }
    }
}
