using System;
using System.Collections.Generic;
using System.IO;

namespace pz_3_4
{
    public class Ex4
    {
        public static void poch()
        {

            const string textfile4 = "../../../ex4.txt";

            Console.Write("\nSign up --- 1\nsign in --- 2\nType 1 or 2: ");
            char operation = Console.ReadKey().KeyChar; 
            Console.WriteLine(); 


            if (operation != '1' && operation != '2') return;
            bool notCompleted = true;

            while (notCompleted)
            {
                Console.Write("login: ");
                string log = Console.ReadLine();
                Console.Write("password: ");
                string password = Console.ReadLine();


                string passwordHash = hashmade.GMD5(password);

                // Create file if not exist
                if (!File.Exists(textfile4)) File.Create(textfile4);
                // Read data from file
                string[] fileData = File.ReadAllLines(textfile4);

                var db = new Dictionary<string, string>(); //create a dict
                foreach (string item in fileData)
                {
                    var temp = item.Split('\t');
                    db.Add(temp[0], temp[1]);
                }


                if (operation == '1')//signing up
                {
                    if (db.ContainsKey(log))
                    {
                        Console.WriteLine("Login has beed registrated before. Please try another one!");
                        notCompleted = true;
                    }
                    else
                    {
                        Console.WriteLine("Signing up completed!");

                        string dataToSave = $"{log}\t{passwordHash}\n";//into file
                        File.AppendAllText(textfile4, dataToSave);
                        notCompleted = false;
                    }
                }
                else if (operation == '2')//signing in
                {


                    if (db.ContainsKey(log) && db[log] == passwordHash)// hash
                    {
                        Console.WriteLine("You are in.");
                        notCompleted = false;
                    }
                    else
                    {
                        Console.WriteLine("Account not exist. Please sign up first.");
                        notCompleted = true;
                    }
                }
            }

            Console.WriteLine();
        }
    }
}