using System;
using System.Collections.Generic;
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
                ChoiceCommand(s);
            }
        }

        private static void ChoiceCommand(string com)
        {
            com = com.ToLower();
            string[] mas = com.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (mas.Length != 0)
                switch (mas[0])
                {
                    case "help": Help(); break;
                    case "reset": Reset(); break;
                    case "pwd": Pwd(); break;
                    case "ls": Ls(); break;
                    case "exit": Exit(); break;
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
                    case "rm":
                        try
                        {
                            Rm(mas[1]);
                        }
                        catch
                        {
                            Console.WriteLine("Файл не найден");
                        }
                        break;
                    case "rmdir":
                        try
                        {
                            RmDir(mas[1]);
                        }
                        catch
                        {
                            Console.WriteLine("Каталог не найден");
                        }
                        break;
                    case "mv":
                        try
                        {
                            Mv(mas[1], mas[2]);
                        }
                        catch {
                            Console.WriteLine("Путь не найден, либо копируемый файл не существует");
                        }
                        break;
                    case "sh":
                        try
                        {
                            Sh(mas[1]);
                        }
                        catch
                        {
                            Console.WriteLine("Путь не найден, либо исполнительный файл не существует");
                        }
                        break;
                    case "echo":
                        try
                        {
                            Echo(mas);
                        }
                        catch
                        {
                            Echo("");
                        }
                        break;
                    default:
                        Console.WriteLine("Команда не найдена");
                        break;
                }

        }
        private static void Echo(string [] eho)
        {
            for (int i = 1; i < eho.Length; i++)
            {

                Console.Write(eho[i] + " ");
            }
            Console.WriteLine();
        }
        private static void Echo(string eho)
        {
            Console.WriteLine(eho);
        }

        private static void Sh(string usedFile)
        {
            using (StreamReader sr = new StreamReader(way + "/" + usedFile, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ChoiceCommand(line);
                }
            }
            
        }

        private static void Mv(string nameFile, string wayCopyFile)
        {
            try
            {
                if (Directory.Exists(way + "/" + wayCopyFile))
                    File.Copy(way+ "/" + nameFile, way + "/" + wayCopyFile + "/" + nameFile, true);
                else if (Directory.Exists(wayCopyFile))
                    File.Copy(way + "/" + nameFile, wayCopyFile + "/" + nameFile, true);
                else {
                    Console.WriteLine("что то не верно");
                }
            }
            catch
            {
                Console.WriteLine("Копирование не прошло");
            }

        }

        private static void RmDir(string nameKatalog)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(way + "\\" + nameKatalog);
                dirInfo.Delete(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Rm(string nameFile)
        {
            FileInfo fileInf = new FileInfo(way+"\\" + nameFile);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                

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
            int i = way.Length ;
            foreach (string s in dirs)
            {
                i = way.Length;
                if (s[way.Length] == '\\')
                    i += 1;
                Console.WriteLine(s.Remove(0,i));
            }
            Console.WriteLine();
            Console.WriteLine("Файлы:");
            string[] files = Directory.GetFiles(way);
            foreach (string s in files)
            {
                i = way.Length;
                if (s[way.Length] == '\\')
                    i += 1;
                Console.WriteLine(s.Remove(0, i));
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
                "rm <файл> — удалить файл",
                "rmdir <каталог> — удалить каталог",
                "mkdir <каталог> — создать новый каталог",
                "pwd — вывести имя текущего каталога",
                "reset — очистить экран консоли",
                "exit — выход из консоли ",
                "help — выводит справочную инфорацию о командах эмулятора linux",
                "sh <файл> — запускает командный файл",
                "mv <файл> <путь до места копирования> — копирует определённый файл"};

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
