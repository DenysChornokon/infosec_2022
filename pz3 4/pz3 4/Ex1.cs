using System;

namespace pz_3_4
{
    public static class Ex1
    {
        public static void poch()
        {
            Console.WriteLine("Type your string: ");
            string input = Console.ReadLine();
            Console.WriteLine();

            Console.Write("MD5-algorithm:  ");
            Console.WriteLine(hashmade.GMD5(input));

            Console.Write("SHA1-algorithm:  ");
            Console.WriteLine(hashmade.GSHA1(input));

            Console.Write("SHA256-algorithm:  ");
            Console.WriteLine(hashmade.GSHA256(input));

            Console.Write("SHA512-algorithm:  ");
            Console.WriteLine(hashmade.GSHA512(input));

            Console.WriteLine();
        }
    }
}
