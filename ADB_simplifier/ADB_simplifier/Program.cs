using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading;


namespace ADB_simplifier
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (!isAdbHere())
            {
                Console.WriteLine("ADB není nainstalované!");
                downandinstadb();
                Thread.Sleep(100);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Zadejte jméno aplikace, která je v adresáři tohoto programu, nebo její celou cestu: ");
            String pathtogame = Console.ReadLine();
            if (isFileValit(pathtogame))
            {
                //Process proc = Process.Start("cmd.exe", strCmdText);
                Process proc = new Process();
                proc.StartInfo.FileName = "adb.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.Arguments = "install " + "\"" +pathtogame+ "\"";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                String test = proc.StandardOutput.ReadToEnd();
                Console.WriteLine(test);
                Console.WriteLine("Instaluje se......");
                Console.WriteLine("Pokud máte obb složku, nezapomeňte ji umístit do Oculus/Android/obb/");
                Console.ForegroundColor = ConsoleColor.Red;
                proc.WaitForExit();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Zadaná cesta je neplatná!");
                for (int i = 5; i >= 0; i--)
                {
                    Console.WriteLine("Ukončení za " + i + " sekund");
                    Thread.Sleep(1000);
                }
            }
        }
        }
        public static bool isFileValit(String path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isAdbHere()
        {
            if (File.Exists("adb.exe"))
            {
                return true;
            }
            return false;
        }

        public static void downandinstadb()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" ----- ");
                Console.WriteLine(" ----- ");
                Console.WriteLine("Jelikož nebylo nalezeno adb, instaluji jej.....");
                Console.WriteLine(" ----- ");
                Console.WriteLine(" ----- ");
                Thread.Sleep(50);
                Console.WriteLine("Stahuji adb.....");
                
                string path = Environment.CurrentDirectory+"\\adb.zip";
                Process proc2 = new Process();
                proc2.StartInfo.FileName = "curl";
                proc2.StartInfo.UseShellExecute = false;
                proc2.StartInfo.Arguments = "https://filebin.net/archive/r2vlvrw10oo7cg8r/zip --output " + path;
                proc2.StartInfo.RedirectStandardOutput = true;
                proc2.Start();
                proc2.WaitForExit();
                
                Console.WriteLine(" ----- ");
                Console.WriteLine(" ----- ");
                Console.WriteLine("Extrahuji adb.....");
                Process proc = new Process();
                proc.StartInfo.FileName = "tar";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.Arguments = "-xf adb.zip";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                proc.WaitForExit();
                
                Console.WriteLine(" ----- ");
                Console.WriteLine(" ----- ");
                Console.WriteLine("Mažu archiv.....!");
                
                string delpath = Environment.CurrentDirectory+"\\adb.zip";
                File.Delete(delpath);
                
                Console.WriteLine(" ----- ");
                Console.WriteLine(" ----- ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Hotovo, zmáčkněte jakoukoliv klávesu pro restartování programu.");
                Console.ReadKey();

                string restartpath = Environment.CurrentDirectory+"\\ADB_simplifier.exe";
                System.Diagnostics.Process.Start(restartpath);
                Thread.Sleep(100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}