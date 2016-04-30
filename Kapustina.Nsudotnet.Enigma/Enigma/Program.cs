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
        static void encrypt(string file_input, string type, string file_output)
        {

            if (type == "des")
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.GenerateKey();
                des.GenerateIV();
                ICryptoTransform transform = des.CreateEncryptor(des.Key, des.IV);
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
                        string s = Convert.ToBase64String(des.IV);
                        key.WriteLine(s);
                        s = Convert.ToBase64String(des.Key);
                        key.WriteLine(s);
                    }
                    stream.FlushFinalBlock();
                }
            }
            else
            {
                if (type == "aes")
                {
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    aes.GenerateKey();
                    aes.GenerateIV();
                    ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
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
                            string s = Convert.ToBase64String(aes.IV);
                            key.WriteLine(s);
                            s = Convert.ToBase64String(aes.Key);
                            key.WriteLine(s);
                        }
                        stream.FlushFinalBlock();
                    }
                }
                else
                {
                    if (type == "rc2")
                    {
                        RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                        rc2.GenerateKey();
                        rc2.GenerateIV();
                        ICryptoTransform transform = rc2.CreateEncryptor(rc2.Key, rc2.IV);
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
                                string s = Convert.ToBase64String(rc2.IV);
                                key.WriteLine(s);
                                s = Convert.ToBase64String(rc2.Key);
                                key.WriteLine(s);
                            }
                            stream.FlushFinalBlock();
                        }
                    }
                }
            }
            AesCryptoServiceProvider a = new AesCryptoServiceProvider();
            RC2CryptoServiceProvider r = new RC2CryptoServiceProvider();
        }

        static void decrypte(string file_output, string type, string key, string file_input)
        {
            if (type == "des")
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
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                ICryptoTransform transform = des.CreateDecryptor(key_byte, iv_byte);
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
            else
            {
                if (type == "aes")
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
                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    ICryptoTransform transform = aes.CreateDecryptor(key_byte, iv_byte);
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
                else
                {
                    if (type == "rc2")
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
                        RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                        ICryptoTransform transform = rc2.CreateDecryptor(key_byte, iv_byte);
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
                }
            }
        }


        static void DecryptorType(SymmetricAlgorithm sa)
        {

        }

        static void Main(string[] args)
        {
            encrypt("./input.txt", "rc2", "./output.bin");
            decrypte("./output.bin", "rc2", "file.key.txt", "file.txt");
        }



    }
}
