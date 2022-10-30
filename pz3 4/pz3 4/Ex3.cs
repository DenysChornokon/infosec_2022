using System;
using System.IO;
using System.Linq;

namespace pz_3_4
{
    public static class Ex3
    {
        const string textfile3 = "../../../ex3.txt";

        public static void poch()
        {
            Console.Write("Your string: ");
            string ystr = Console.ReadLine();


            string hash = hashmade.GMD5(ystr);
            if (!File.Exists(textfile3)) File.Create(textfile3);
            string[] db = File.ReadAllLines(textfile3);

            if (db.Contains(hash)) //if repeat
            {
                Console.WriteLine("You've already hashed this one!");
            }
            else
            {
                Console.WriteLine("Data saved.");
                File.AppendAllText(textfile3, "\n" + hash);//write  data in txt
            }

            Console.WriteLine();
        }
    }
}
