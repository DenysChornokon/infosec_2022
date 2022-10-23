using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class LoginApp
    {
        const int itColl = 6 * 10000;


        private static Dictionary<string, User> users = new Dictionary<string, User>();


        public static void Register()
        {
            Console.WriteLine("Login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();


            byte[] bytePass = Encoding.UTF8.GetBytes(password);
            byte[] byteSalt = PBKDF2.GenerateSalt();
            byte[] hashPass = SaltedHash.HashPasswordWithSalt(bytePass, byteSalt, itColl);


            if (!users.ContainsKey(login))
            {
                Console.WriteLine("Success");
                User newUser = new User(login, hashPass, byteSalt);
                users.Add(login, newUser);
            }
            else
                Console.WriteLine("No user found");
        }

        public static void Login()
        {
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (users.ContainsKey(login) == false)
                Console.WriteLine("No user found");
            else
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltBytes = users[login].Salt;
                byte[] passHash = SaltedHash.HashPasswordWithSalt(passwordBytes, saltBytes, itColl);
                string passInStr = Convert.ToBase64String(passHash);

                if (users[login].Password == passInStr)
                    Console.WriteLine("You're in!");
                else
                    Console.WriteLine("Wrong password!");
            }
        }
    }
}
