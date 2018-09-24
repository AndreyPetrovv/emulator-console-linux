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
                string s = Console.ReadLine();
                choiceCommand(s);
                
            }
            Console.ReadKey();
        }

        static void choiceCommand(string com) {
            string[] mas = com.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            switch (mas[0]) {
                case "help": help(); break;

            }

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

            return user+" -l:~$ ";
        }

        static private void help()
        {
            List<string> command = new List<string> {
                "ls — выдать список файлов в текущем каталоге",
                "cd [каталог] — сменить текущий каталог",
                "rm <файлы> — удалить файлы",
                "mkdir <каталог> — создать новый каталог",
                "pwd — вывести имя текущего каталога",
                "clear — очистить экран консоли",
                "none",
                "none",
                "none",
                "none" };

            foreach (var item in command)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
