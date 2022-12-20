using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;
using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Encrypt the file and fill the folder with .dat extension: 1\n");
        Console.WriteLine("Decrypt the file: 2\n");
        int num = Convert.ToInt32(Console.ReadLine());
        string path = "C:/Users/Windows10/source/repos/Projects/pz2/pz2/encrypted.dat";
        byte[] inside = File.ReadAllBytes("C:/Users/Windows10/source/repos/Projects/pz2/pz2/file.txt").ToArray();

        switch (num)
        {
            
            case 1:
                var encryption = new XOR_program();
                Console.WriteLine("Enter the password: ");
                string password = Console.ReadLine();
                Console.WriteLine("\n");
                byte[] passwordForEnc = Encoding.UTF8.GetBytes(password);
                var encrypting = encryption.Encryption(inside, passwordForEnc);
                Console.WriteLine("Saved to file encrypted.dat\n");
                File.WriteAllBytes(path, encrypting);
                break;
            case 2:
                byte[] encryptContent = File.ReadAllBytes(path).ToArray();
                var decryption = new XOR_program();
                Console.WriteLine("Enter the password: ");
                string pass = Console.ReadLine();
                Console.WriteLine("\n");
                byte[] password_d = Encoding.UTF8.GetBytes(pass);
                var decryptedProcess = decryption.Decryption(encryptContent, password_d);
                Console.WriteLine(Encoding.UTF8.GetString(decryptedProcess));
                Console.WriteLine("\n");
                Console.WriteLine("Decryption done!");
                break;
        }



    }



}

public class XOR_program
{
    private byte[] GetSecretKey(byte[] key, byte[] array)
    {
        byte[] secret = new byte[array.Length];
        for (int i = 0; i < secret.Length; i++)
        {
            secret[i] = key[i % key.Length];
        }
        return secret;
    }
    private byte[] XoR(byte[] text, byte[] pas)
    {
        byte[] secretKey = GetSecretKey(pas, text);
        int array_size = text.Length;
        for (int i = 0; i < text.Length; i++)
        {
            text[i] = (byte)(text[i] ^ secretKey[i]);
        }
        return text;
    }

    public byte[] Encryption(byte[] ourText, byte[] ourPas)
    {
        return XoR(ourText, ourPas);
    }

    public byte[] Decryption(byte[] encryptedText, byte[] ourPas)
    {
        return XoR(encryptedText, ourPas);
    }

}

