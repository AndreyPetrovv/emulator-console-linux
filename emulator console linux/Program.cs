﻿using System;
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
            Console.Title = "eTerminal Linux";

            way += Users();
            
            while (true)
            {

                Console.Write(Users() + "@ubuntu -l:~$ ");
                string s = Console.ReadLine();
                СhoiceCommand(s);
                
            }
        }

        static void СhoiceCommand(string com)
        {
            com = com.ToLower();
            string[] mas = com.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            switch (mas[0])
            {
                case "help": Help(); break;
                case "reset": Reset(); break;
                case "pwd": Pwd(); break;
                case "cd":
                    try
                    {
                        Cd(mas[1]);
                    }
                    catch
                    {
                        Cd("");
                    }
                    break;
                case "exit": Exit(); break;
                case "ls": Ls(); break;
                case "mkdir":
                    try
                    {
                        MkDir(mas[1]);
                    }
                    catch
                    {
                        Console.WriteLine("Команда указанна не верно");
                    }
                    break;

                default:
                    Console.WriteLine("Команда не найдена");
                    break;
            }

        }

        private static string Users()

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

        private static void MkDir(string subpath)
        {
            DirectoryInfo dirInfo;
            try
            {
                dirInfo = new DirectoryInfo(way + "/" + subpath);
            }
            catch {
                dirInfo = new DirectoryInfo(subpath);
            }

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        private static void Ls()
        {
            Console.WriteLine("Подкаталоги:");
            string[] dirs = Directory.GetDirectories(way);
            int i;
            foreach (string s in dirs)
            {
                i = s.Length;
                Console.WriteLine(s.Remove(0, way.Length+1));
            }
            Console.WriteLine();
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(way);
            foreach (string s in files)
            {
                i = s.Length;
                Console.WriteLine(s.Remove(0, way.Length+1));
            }
        }

        private static void Cd(string cdCom)
        {
            switch (cdCom)
            {
                case "":
                case "~":
                    reway = way;
                    way = "C:/Users/" + Users();
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
                case "..":
                    bool trye = true;
                    int i ;
                    while (trye)
                    {
                        i = way.Length - 1;
                        if (way[i] != '/')
                            way = way.Remove(i);
                        else
                        {
                            way = way.Remove(i);
                            trye = false;
                        }
                    }
                    break;

                default:
                    if (Directory.Exists(way + "/" + cdCom))
                    {
                        reway = way;
                        if (cdCom[cdCom.Length - 1] != '/')
                            way += "/" + cdCom;
                        else
                        {
                            way += "/" + cdCom.Substring(0, cdCom.Length - 1);
                        }
                    }
                    else if (Directory.Exists(cdCom)) {
                        way = cdCom;
                    }
                    else
                        Console.WriteLine("Путь не найден");
                    break;
            }

        }

        private static void Help()

        {
            List<string> command = new List<string> {
                "ls — выдать список файлов в текущем каталоге",
                "cd [каталог] — сменить текущий каталог",
                "rm <файлы> — удалить файлы",
                "mkdir <каталог> — создать новый каталог",
                "pwd — вывести имя текущего каталога",
                "reset — очистить экран консоли",
                "exit — выход из консоли ",
                "help — выводит справочную инфорацию о командах linux",
                "none",
                "none" };

            foreach (var item in command)
            {
                Console.WriteLine(item);
            }
            
        }

        private static void Reset()
        {
            Console.Clear();
        }

        private static void Pwd()
        {

            Console.WriteLine(way);
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
