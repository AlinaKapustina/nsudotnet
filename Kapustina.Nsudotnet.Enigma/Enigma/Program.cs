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
                    break;
            }
        }


        static void DecryptorType(SymmetricAlgorithm sa, string file_output, string key, string file_input)
        {
            byte[] key_byte;
            byte[] iv_byte;
            UnicodeEncoding e = new UnicodeEncoding();
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
                    byte[] decrypto_byte = new byte[64];
                    int size = 1;
                    while (size != 0)
                    {
                        size = output.Read(decrypto_byte, 0, decrypto_byte.Length);
                        stream.Write(decrypto_byte, 0, size);
                    }
                    stream.FlushFinalBlock();
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
                    Byte[] bytes = new Byte[64];
                    int size = 1;
                    while (size != 0)
                    {
                        size = input.Read(bytes, 0, bytes.Length);
                        stream.Write(bytes, 0, size);
                    }
                }
                using (StreamWriter key = new StreamWriter("file.key.txt"))
                {
                    string s = Convert.ToBase64String(sa.IV);
                    key.WriteLine(s);
                    s = Convert.ToBase64String(sa.Key);
                    key.Write(s);
                }
                stream.FlushFinalBlock();
            }
        }

        static void Main(string[] args)
        {
            switch(args[0])
            {
                case "encrypt": Encrypt(args[1], args[2], args[3]);
                    break;
                case "decrypt": Decrypt(args[1], args[2], args[3], args[4]);
                    break;
                default:
                    break;
            }      
        }
    }
}
