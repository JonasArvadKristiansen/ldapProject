using System;
using System.DirectoryServices;
using System.Threading;

namespace ADIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            App();
        }

        public static void App()
        {

            DirectorySearcher searcher = LoginUser.Login();

            FindUser.Find(searcher);

        }
    }
}
