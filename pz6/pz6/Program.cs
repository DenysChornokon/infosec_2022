using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace pz6
{
    class Program
    {
        
        public static int myVar = 26000;
        static void Main(string[] args)
        {
            string task;

            while (true)
            {
                Console.Write("Choose your task number:\nFirst task: 1\nSecond task: 2\n");
                do
                { task = Console.ReadLine(); }

                while ((task != "1") && (task != "2"));
                int conv = Convert.ToInt32(task);
                Console.Write("\n");

                switch (conv)

                {
                    case 1:
                        Task1();
                        break;

                    case 2:
                        Task2();
                        break;
                }
            }
        }
        private static void Task1()
        {
            var aes = new aesChipher();
            var des = new desChipher();
            var desTriple = new trippleDES();              //Algoritmes
            var key_aes = aes.GenerateRandomNumber(32);
            var iv_aes = aes.GenerateRandomNumber(16);
            var key_des = des.GenerateRandomNumber(8);
            var iv_des = des.GenerateRandomNumber(8);
            var key_tdes = desTriple.GenerateRandomNumber(24);
            var iv_tdes = desTriple.GenerateRandomNumber(8);         //Keys

            //вводимо пароль
            Console.Write("Type your password: ");
            string firstPass = Console.ReadLine();

            byte[] bytePass = Encoding.ASCII.GetBytes(firstPass);

            //AES
            
            var aesEnc = aes.Encrypt(bytePass, key_aes, iv_aes); //Encryption
            var desEnc = aes.Decrypt(aesEnc, key_aes, iv_aes); //Decryption
            var aesDecMes = Encoding.UTF8.GetString(desEnc); //Back to text
            Console.WriteLine("\nAES:");
            Console.WriteLine("Default text: " + firstPass);
            Console.WriteLine("Encrypted text: " + Convert.ToBase64String(aesEnc));
            Console.WriteLine("Decrypted text: " + aesDecMes + "\n");

            //DES
            
            var encrypted_des = des.Encrypt(bytePass, key_des, iv_des);
            var decrypted_des = des.Decrypt(encrypted_des, key_des, iv_des);
            
            var decryptedString_des = Encoding.UTF8.GetString(decrypted_des);
            Console.WriteLine("DES");
            Console.WriteLine("Default text: " + firstPass);
            Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted_des));
            Console.WriteLine("Decrypted text: " + decryptedString_des + "\n\n");

            //TrDES

            var encrypted_tdes = desTriple.Encrypt(bytePass, key_tdes, iv_tdes);
            var decrypted_tdes = desTriple.Decrypt(encrypted_tdes, key_tdes, iv_tdes);
            
            var decryptedString_tdes = Encoding.UTF8.GetString(decrypted_tdes);
            Console.WriteLine("Triple DES");
            Console.WriteLine("Default text: " + firstPass);
            Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted_tdes));
            Console.WriteLine("Decrypted text: " + decryptedString_tdes);
            Console.ReadKey();
        }
        private static void Task2()
        {
            var aes = new aesChipher();
            var des = new desChipher();
            var desTriple = new trippleDES();

            
            Console.Write("Type your password: ");
            string firstData = Console.ReadLine();
            byte[] ori_bytes = Encoding.ASCII.GetBytes(firstData);


            byte[] a_key = PBKDF2.Generator(ori_bytes, 32);
            byte[] a_iv = PBKDF2.Generator(ori_bytes, 16);
            byte[] d_key = PBKDF2.Generator(ori_bytes, 8);
            byte[] d_iv = PBKDF2.Generator(ori_bytes, 8);
            byte[] key_tdes = PBKDF2.Generator(ori_bytes, 24);
            byte[] iv_tdes = PBKDF2.Generator(ori_bytes, 8);

            var encrypted_AES = aes.Encrypt(ori_bytes, a_key, a_iv);
            var decrypted_DES = aes.Decrypt(encrypted_AES, a_key, a_iv);
          
            var decString_DES = Encoding.UTF8.GetString(decrypted_DES);
            Console.WriteLine("\nAES");
            Console.WriteLine("Default text: " + firstData);
            Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted_AES));
            Console.WriteLine("Decrypted text: " + decString_DES + "\n\n");

            var encrypted_des = des.Encrypt(ori_bytes, d_key, d_iv);
            var decrypted_des = des.Decrypt(encrypted_des, d_key, d_iv);

            var decryptedString_des = Encoding.UTF8.GetString(decrypted_des);
            Console.WriteLine("DES");
            Console.WriteLine("Default text" + firstData);
            Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted_des));
            Console.WriteLine("Decrypted text: " + decryptedString_des + "\n\n");


            var encrypted_tdes = desTriple.Encrypt(ori_bytes, key_tdes, iv_tdes);
            var decrypted_tdes = desTriple.Decrypt(encrypted_tdes, key_tdes, iv_tdes);

            var decryptedString_tdes = Encoding.UTF8.GetString(decrypted_tdes);
            Console.WriteLine("Triple DES: ");
            Console.WriteLine("Default text: " + firstData);
            Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted_tdes));
            Console.WriteLine("Decrypted text: " + decryptedString_tdes);
            Console.ReadKey();
        }

        class aesChipher
        {

            public byte[] GenerateRandomNumber(int length)
            {
                using (var rundomGenerator = new RNGCryptoServiceProvider())
                {
                    var randomNum = new byte[length];
                    rundomGenerator.GetBytes(randomNum);
                    return randomNum;
                }
            }
            public byte[] Encrypt(byte[] encData, byte[] key, byte[] iv)
            {
                using (var aes = new AesCryptoServiceProvider()) //symmeric
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7; //write text to the end
                    aes.Key = key;
                    aes.IV = iv;
                    using (var memoryStream = new MemoryStream())
                    {
                        var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                       
                        cryptoStream.Write(encData, 0, encData.Length);
                        
                        cryptoStream.FlushFinalBlock();
                       
                        return memoryStream.ToArray();
                    }
                }
            }
            public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
            {
                
                using (var aes = new AesCryptoServiceProvider())
                {
                    
                    aes.Mode = CipherMode.CBC;
                    
                    aes.Padding = PaddingMode.PKCS7;
                    
                    aes.Key = key;
                    
                    aes.IV = iv;
                    using (var memoryStream = new MemoryStream())
                    {
                        var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                        cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
        class desChipher
        {
            
            public byte[] GenerateRandomNumber(int length)
            {
                using (var randomNumberGenerator = new RNGCryptoServiceProvider())
                {
                    var randomNumber = new byte[length];
                    randomNumberGenerator.GetBytes(randomNumber);
                    return randomNumber;
                }
            }
            public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
            {
                using (var des = new DESCryptoServiceProvider()) //symmeric
                {
                    des.Mode = CipherMode.CBC;
                    
                    des.Padding = PaddingMode.Zeros;                  
                    des.Key = key;               
                    des.IV = iv;
                    using (var memoryStream = new MemoryStream())
                    {
                        
                        var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);

                        cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);                       
                        cryptoStream.FlushFinalBlock();                       
                        return memoryStream.ToArray();
                    }
                }
            }
            public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
            {
                using (var des = new DESCryptoServiceProvider())
                {
                    
                    des.Mode = CipherMode.CBC;                   
                    des.Padding = PaddingMode.Zeros;                    
                    des.Key = key;                    
                    des.IV = iv;

                    using (var memoryStream = new MemoryStream())
                    {
                        
                        var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);

                        cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                        cryptoStream.FlushFinalBlock();

                        return memoryStream.ToArray();
                    }
                }
            }
        }
        class trippleDES
        {
            
            public byte[] GenerateRandomNumber(int length)
            {
                
                using (var randomNumberGenerator = new RNGCryptoServiceProvider())
                {

                    var randomNum = new byte[length];                   
                    randomNumberGenerator.GetBytes(randomNum);                   
                    return randomNum;
                }
            }
            public byte[] Encrypt(byte[] encData, byte[] key, byte[] iv)
            {
                using (var des = new TripleDESCryptoServiceProvider())
                {
                    
                    des.Mode = CipherMode.CBC;                  
                    des.Padding = PaddingMode.PKCS7;                
                    des.Key = key;
                    des.IV = iv;
                    using (var mem_iv = new MemoryStream())
                    {
                       
                        var cryptoStream = new CryptoStream(mem_iv, des.CreateEncryptor(), CryptoStreamMode.Write);
                        cryptoStream.Write(encData, 0, encData.Length);
                        cryptoStream.FlushFinalBlock();
                        return mem_iv.ToArray();
                    }
                }
            }
            public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
            {
                using (var des = new TripleDESCryptoServiceProvider())
                {
                    des.Mode = CipherMode.CBC;
                    des.Padding = PaddingMode.PKCS7;
                    des.Key = key;
                    des.IV = iv;

                    using (var mem_iv = new MemoryStream())
                    {
                        var cryptoStream = new CryptoStream(mem_iv, des.CreateDecryptor(), CryptoStreamMode.Write);
                        cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                        cryptoStream.FlushFinalBlock();
                        return mem_iv.ToArray();
                    }
                }
            }
        }
        class PBKDF2
        {
            public static byte[] GenerateSalt(int length)
            {
                using (var randomNumberGenerator = new RNGCryptoServiceProvider())
                {
                    var randomNum = new byte[length];
                    randomNumberGenerator.GetBytes(randomNum);
                    return randomNum;
                }
            }
            public static byte[] Generator(byte[] toBeHashed, int length)
            {
                byte[] salt = GenerateSalt(16);
                using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, myVar))
                {
                    return rfc2898.GetBytes(length);
                }
            }
        }
    }
}
