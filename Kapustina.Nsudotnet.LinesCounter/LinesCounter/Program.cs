using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LinesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool bracket = false;
            bool strart = false;
            bool file = false;
            int length = 0;
            if (args.Length != 0)
            {
                string expansion = args[0];
                string directory = Directory.GetCurrentDirectory();
                string[] files = Directory.GetFiles(directory, expansion, SearchOption.AllDirectories);
                foreach (var value in files)
                {
                    using (StreamReader reader = new StreamReader(value))
                    {
                        string line = null;
                        while (!reader.EndOfStream)
                        {
                            if (!file)
                            {
                                line = reader.ReadLine();
                                line = line.Trim();
                            }
                            if (line.Length != 0)
                            {
                                if (bracket)
                                {
                                    int index = line.IndexOf("*/");
                                    if (index != -1)
                                    {
                                        if (!line.EndsWith("*/"))
                                        {
                                            if (!strart)
                                            {
                                                length++;
                                            }
                                        }
                                        bracket = false;
                                    }
                                    else
                                    {
                                        strart = false;
                                    }
                                    file = false;
                                }
                                else
                                {
                                    if (line.IndexOf("//") == -1)
                                    {
                                        int index = line.IndexOf("/*");
                                        if (index != -1)
                                        {
                                            bracket = true;
                                            if (!line.StartsWith("/*"))
                                            {
                                                strart = true;
                                                length++;
                                            }
                                            file = true;
                                        }
                                        else
                                        {
                                            length++;
                                        }
                                    }
                                    else
                                    {
                                        if (!line.StartsWith("//"))
                                        {
                                            length++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine(length);
                Console.ReadKey();
            }
        }

    }


}
