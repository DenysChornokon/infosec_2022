using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Lab5
{
    class MENU
    {
        public const int var = 6 * 10000;

        static void Main(string[] args)
        {
            Console.WriteLine("Task 3-5:");
            char menu = Console.ReadKey().KeyChar;
            Console.WriteLine();
            Console.WriteLine();

            switch (menu)
            {
                case '3':
                    Exc3();
                    break;

                case '4':
                    Console.WriteLine("Choose the hash algorithm:\n1)SHA1\n2)SHA256\n3)SHA384\n4)SHA512");
                    char typeOfHash = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    Exc4(typeOfHash);
                    break;

                case '5':
                    Exc5();
                    break;
            }
        }

        private static void Exc3()
        {
            Console.WriteLine("Type your password:");

            string password = Console.ReadLine();

            byte[] salt = SaltedHash.GenerateSalt();

            Console.WriteLine("Your password: " + password);

            Console.WriteLine("Salt: " + Convert.ToBase64String(salt));

            Console.WriteLine();

            var firstHashedPass = SaltedHash.HashPasswordWithSalt(

                Encoding.UTF8.GetBytes(password), salt);

            Console.WriteLine("Hashed password: " + Convert.ToBase64String(firstHashedPass));
        }

        private static void Exc4(char typeOfHash)
        {
            Console.WriteLine("Type your password:");
            string password = Console.ReadLine();
            int iterations = var;
            HashAlgorithmName algorithmType;
            switch (typeOfHash)
            {
                case '1':
                    algorithmType = HashAlgorithmName.SHA1;
                    break;

                case '2':
                    algorithmType = HashAlgorithmName.SHA256;
                    break;

                case '3':
                    algorithmType = HashAlgorithmName.SHA384;
                    break;

                case '4':
                    algorithmType = HashAlgorithmName.SHA512;
                    break;

                default:
                    Console.WriteLine("Error");
                    return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < 10; i++)
            {
                var hashedPassword = PBKDF2.HashPassword(
                    Encoding.UTF8.GetBytes(password),
                    PBKDF2.GenerateSalt(), iterations,
                    algorithmType);
                Console.WriteLine();
                Console.WriteLine("Your password: " + password);
                Console.WriteLine("Hashed password: " + Convert.ToBase64String(hashedPassword));
                Console.WriteLine($"Time spent: {timer.ElapsedMilliseconds}ms\n" +
                                  $"Iterations done: {iterations}");
                iterations += 50000;
            }

            timer.Stop();
            Console.ReadKey();
        }

        private static void Exc5()
        {
            while (true)
            {
                Console.WriteLine($"Log up - 1\n" +
                              $"Log in - 2");
                char operation = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (operation)
                {
                    case '1':
                        {
                            Console.WriteLine("Registration: ");
                            LoginApp.Register();
                            break;
                        }

                    case '2':
                        {
                            Console.WriteLine("Loging in:");
                            LoginApp.Login();
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Error");
                            return;
                        }
                }
            }
        }
    }
}
