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
        private static string reway = "";
        private static string way = "C:/Users/";

        static void Main(string[] args)
        {
            way += users();
            
            while (true)
            {

                Console.Write(users() + "ubuntu -l:~$ ");
                string s = Console.ReadLine();
                choiceCommand(s);
                
            }
            Console.ReadKey();
        }

        static void choiceCommand(string com) {
            com = com.ToLower();
            string[] mas = com.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            switch (mas[0])
            {
                case "help": help(); break;
                case "reset": reset(); break;
                case "pwd": pwd(); break;
                case "cd":
                    try
                    {
                        cd(mas[1]);
                    }
                    catch
                    {
                        cd("");
                    }
                    break;
            }

        }

        private static string users()
        {

            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            int i;
            for (i = 0; i<userName.Length; i++)
            {
                if (userName[i] == '\\')
                    break;
            }

            string user = userName.Substring(++i);



            return user;
        }

        private static void cd(string cdCom) {

           

            switch (cdCom)
            {
                case "":
                case "~":
                    reway = way;
                    way = "C:/Users/" + users();
                    break;
                case "-":
                    if(reway != "")
                        way = reway;
                    break;
                case "/":
                    reway = way;
                    way = "C:/";
                    break;
                case "/home":
                    reway = way;
                    way = "C:/Users/";
                    break;

                default:
                    if (Directory.Exists(way +"/"+ cdCom))
                    {
                        reway = way;
                        way +="/" + cdCom;
                    }
                    else
                        Console.WriteLine("Путь не найден");
                    break;
            }

        }

        private static void help()
        {
            List<string> command = new List<string> {
                "ls — выдать список файлов в текущем каталоге",
                "cd [каталог] — сменить текущий каталог",
                "rm <файлы> — удалить файлы",
                "mkdir <каталог> — создать новый каталог",
                "pwd — вывести имя текущего каталога",
                "reset — очистить экран консоли",
                "none",
                "none",
                "none",
                "none" };

            foreach (var item in command)
            {
                Console.WriteLine(item);
            }
            
        }

        private static void reset() {
            Console.Clear();
        }

        private static void pwd() {

            Console.WriteLine(way);
        }
    }
}
