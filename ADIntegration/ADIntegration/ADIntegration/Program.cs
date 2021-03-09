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

            var login = LoginUser.Login();

            FindUser.Find(login.Item1, login.Item2);

        }
    }
}
