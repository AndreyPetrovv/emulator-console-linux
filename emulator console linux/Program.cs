using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace emulator_console_linux
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                Console.Write(users());
                Console.ReadLine();
            }
            Console.ReadKey();
        }

        static private string users()
        {

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            int i;
            for (i = 0; i<userName.Length; i++)
            {
                if (userName[i] == '\\')
                    break;
            }

            string user = userName.Substring(++i);

            return user+"-l:~$ ";
        }

        static private string help()
        {

            return "";
        }
    }
}
