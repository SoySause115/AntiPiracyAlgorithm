using System;
using System.IO;

namespace Kozin_AntiPiracyAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //tells the program to start or not based on the key
            bool canRunProgram = false;
            //main folder path (starting from Kozin_Quest7\bin\Debug\netcoreapp3.1 and going backwards)
            string folderPath = @"..\..\..\";
            //where the key is stored
            string keyPath = @"DRM.txt";

            //if the file doesn't exist (only for first time installation)
            if (!File.Exists(keyPath))
            {
                //create the file
                using (StreamWriter sw = File.CreateText(keyPath))
                {
                    //get the time created by the file
                    DateTime masterKey = File.GetCreationTime(folderPath);
                    //write it to the file
                    sw.WriteLine(masterKey.ToString());
                    canRunProgram = true;
                }
            }
            else //if it does
            {
                //open the file
                using (StreamReader sr = File.OpenText(keyPath))
                {
                    //get the date creation of the folder
                    DateTime keyCreation = File.GetCreationTime(folderPath);
                    //if the line in the file is the same as the creation date of the folder, and it hasn't been copied (same name)
                    if (sr.ReadLine() == keyCreation.ToString() && new DirectoryInfo(folderPath).Name == typeof(Program).Namespace)
                    {
                        canRunProgram = true;
                    }
                    else
                    {
                        canRunProgram = false;
                        Console.WriteLine("The key was wrong!");
                    }
                }
            }

            //if the program can run
            if (canRunProgram)
            {
                Console.WriteLine("Hello World");
            }

            Console.ReadKey();
        }
    }
}
