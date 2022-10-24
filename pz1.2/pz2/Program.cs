using System;
using System.Security.Cryptography;

class RandomNum
{
    public static void Main()
    {

        for (int r = 0; r < 5; r++)
        {
            var rnd = GenerateRandomNumber();

            Console.WriteLine(Convert.ToBase64String(rnd));

        }

    }


    public static byte[] GenerateRandomNumber()
    {
        using (var randomNumberGenerator = RandomNumberGenerator.Create())
        {
            int bit = 64;
            var randomNumber = new byte[bit];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
}
