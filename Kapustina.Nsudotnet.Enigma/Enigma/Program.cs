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
                    UnicodeEncoding encoding = new UnicodeEncoding();
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
                    
                    using (FileStream key = new FileStream("file.key.txt",FileMode.Create,FileAccess.Write))
                    {
                        key.Write(Convert.ToBase64String(des.IV, 0, des.IV.Length,Base64FormattingOptions.InsertLineBreaks),);
                        key.Write(des.Key,0,des.Key.Length);
                    }

                    stream.FlushFinalBlock();
                }
            }

            AesCryptoServiceProvider a = new AesCryptoServiceProvider();
            RC2CryptoServiceProvider r = new RC2CryptoServiceProvider();
        }

       static void decrypte(string file_output, string type,string key, string file_input)
       {
           if (type == "des")
           {
               
               DESCryptoServiceProvider des = new DESCryptoServiceProvider();
               ICryptoTransform transform = des.CreateEncryptor(des.Key, des.IV);
               

           }
       }

        static void Main(string[] args)
        {
          encrypt("./input.txt","des","./output.bin");
        }

        

    }
}
