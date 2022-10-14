using System;

public class RandomNum
{
    public static void Main()
    {
        Console.WriteLine("Enter the seed number-> ");
        int seed = Convert.ToInt32(Console.ReadLine());

        Random rnd = new Random(seed);
        for (int r = 0; r < 10; r++)
        {
            Console.Write("{0, 3}   ", rnd.Next(0, 15));
        }
    }
}
