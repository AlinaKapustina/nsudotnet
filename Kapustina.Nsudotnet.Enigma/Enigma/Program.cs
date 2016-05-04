using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Enigma
{
    class Program
    {
        static void Encrypt(string file_input, string type, string file_output)
        {
            switch (type)
            {
                case "des": EncryptorType(new DESCryptoServiceProvider(), file_output, file_input);
                    break;
                case "aes": EncryptorType(new AesCryptoServiceProvider(), file_output, file_input);
                    break;
                case "rc2": EncryptorType(new RC2CryptoServiceProvider(), file_output, file_input);
                    break;
                case "rijndael": EncryptorType(new RijndaelManaged(), file_output, file_input);
                    break;
                default:
                    Console.WriteLine("Неверный параметр");
                    break;
            }
            
        }

        static void Decrypt(string file_output, string type, string key, string file_input)
        {
            switch(type)
            {
                case "des":DecryptorType(new DESCryptoServiceProvider(), file_output, key, file_input);
                    break;
                case "aes": DecryptorType(new AesCryptoServiceProvider(), file_output, key, file_input);
                    break;
                case "rc2": DecryptorType(new RC2CryptoServiceProvider(), file_output, key, file_input);
                    break;
                case "rijndael": DecryptorType(new RijndaelManaged(), file_output, key, file_input);
                    break;
                default:
                    Console.WriteLine("Неверный параметр");
                    break;
            }
        }


        static void DecryptorType(SymmetricAlgorithm sa, string file_output, string key, string file_input) 
        {
            byte[] key_byte;
            byte[] iv_byte;
            using (StreamReader key_file = new StreamReader(key))
            {
                String s = key_file.ReadLine();
                iv_byte = Convert.FromBase64String(s);
                s = key_file.ReadLine();
                key_byte = Convert.FromBase64String(s);
            }
            ICryptoTransform transform = sa.CreateDecryptor(key_byte, iv_byte);
            using (FileStream output = new FileStream(file_output, FileMode.Open, FileAccess.Read))
            {
                using (FileStream input = new FileStream(file_input, FileMode.Create, FileAccess.Write))
                {
                    CryptoStream stream = new CryptoStream(input, transform, CryptoStreamMode.Write);
                    output.CopyTo(stream);
                    stream.Close();
                }
            }
        }

        static void EncryptorType(SymmetricAlgorithm sa, string file_output, string file_input)
        {
            sa.GenerateKey();
            sa.GenerateIV();
            ICryptoTransform transform = sa.CreateEncryptor(sa.Key, sa.IV);
            using (FileStream file = new FileStream(file_output, FileMode.Create, FileAccess.Write))
            {
                CryptoStream stream = new CryptoStream(file, transform, CryptoStreamMode.Write);
                using (FileStream input = new FileStream(file_input, FileMode.Open))
                {
                    input.CopyTo(stream);
                }
                using (StreamWriter key = new StreamWriter("file.key.txt"))
                {
                    string s = Convert.ToBase64String(sa.IV);
                    key.WriteLine(s);
                    s = Convert.ToBase64String(sa.Key);
                    key.Write(s);
                }
                stream.Close();
            }
        }

        static void Main(string[] args)
        {
            try
            {
                switch (args[0])
                {
                    case "encrypt": Encrypt(args[1], args[2], args[3]);
                        break;
                    case "decrypt": Decrypt(args[1], args[2], args[3], args[4]);
                        break;
                    default:
                        Console.WriteLine("Неверный параметр");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
