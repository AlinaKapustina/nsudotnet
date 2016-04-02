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
            bool bracket1 = false;
            bool isBracket = true;
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
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                line = line.Trim();
                                isBracket = true;
                                if (line != string.Empty)
                                {
                                    int stringsPosition1 = line.IndexOf('"');
                                    string s = line.Remove(0, stringsPosition1 + 1);
                                    int stringsPosition2 = stringsPosition1 + 1;
                                    if (stringsPosition1 != -1)
                                    {
                                        int position = s.IndexOf('"');
                                        while (s.IndexOf('"') != -1)
                                        {
                                            position = s.IndexOf('"');
                                            s = s.Remove(0, position + 1);
                                            stringsPosition2 += position + 1;
                                        }
                                        if (position != -1)
                                        {
                                            line = line.Remove(stringsPosition1 + 1, stringsPosition2 - stringsPosition1 - 2);
                                            line = line.Trim();
                                        }
                                    }
                                    stringsPosition1 = line.IndexOf('\'');
                                    s = line.Remove(0, stringsPosition1 + 1);
                                    stringsPosition2 = stringsPosition1 + 1;
                                    if (stringsPosition1 != -1)
                                    {
                                        int position = s.IndexOf('\'');
                                        while (s.IndexOf('\'') != -1)
                                        {
                                            position = s.IndexOf('\'');
                                            s = s.Remove(0, position + 1);
                                            stringsPosition2 += position + 1;
                                        }
                                        if (position != -1)
                                        {
                                            line = line.Remove(stringsPosition1 + 1, stringsPosition2 - stringsPosition1 - 2);
                                            line = line.Trim();
                                        }
                                    }
                                    int commentsPosition = line.IndexOf("//");
                                    if (commentsPosition != -1)
                                    {
                                        line = line.Remove(commentsPosition, line.Length - commentsPosition);
                                        line = line.Trim();
                                    }
                                    while (isBracket)
                                    {
                                        if (!bracket1)
                                        {
                                            int positionBracket1 = line.IndexOf("/*");
                                            if (positionBracket1 != -1)
                                            {
                                                bracket1 = true;
                                                int positionBracket2 = line.IndexOf("*/");
                                                if (positionBracket2 != -1)
                                                {
                                                    line = line.Remove(positionBracket1, positionBracket2 - positionBracket1 +2);
                                                    line = line.Trim();
                                                    bracket1 = false;
                                                }
                                                else
                                                {
                                                    line = line.Remove(positionBracket1, line.Length - positionBracket1);
                                                    line = line.Trim();
                                                    isBracket = false;
                                                }
                                            }
                                            else
                                            {
                                                isBracket = false;
                                            }
                                        }
                                        else
                                        {
                                            int positionBracket2 = line.IndexOf("*/");
                                            if (positionBracket2 != -1)
                                            {
                                                line = line.Remove(0, positionBracket2 + 2);
                                                line = line.Trim();
                                                bracket1 = false;
                                            }
                                            else
                                            {
                                                line = line.Remove(0, line.Length);
                                                line = line.Trim();
                                                isBracket = false;
                                            }
                                        }
                                    }
                                    if (line != string.Empty)
                                    {
                                        length++;
                                    }
                                }
                            }

                        }
                    }
                    Console.WriteLine(length); 
                }
        }
    }
}
