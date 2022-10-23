using System;

namespace pz_3_4
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Choose what task you want to execute(1 - 4): ");
                char task = Console.ReadKey().KeyChar;
                Console.WriteLine();


                switch (task)
                {
                    case '1':
                        Ex1.poch();
                        break;

                    case '2':
                        Ex2.poch();
                        break;

                    case '3':
                        Ex3.poch();
                        break;

                    case '4':
                        Ex4.poch();
                        break;

                    default:
                        Console.WriteLine("Finished!");
                        return;
                }
            }
        }
    }
}