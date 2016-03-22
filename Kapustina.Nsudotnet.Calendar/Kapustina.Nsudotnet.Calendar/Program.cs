using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapustina.Nsudotnet.Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter date, please ");
            string date = Console.ReadLine();
            DateTime resultDate;
            if (!DateTime.TryParse(date, out resultDate))
            {
                Console.WriteLine("Error date format!");
            }
            else
            {
                string[] weeksDays = new string[7];
                int i = 6;
                foreach (var value in Enum.GetValues(typeof(DayOfWeek)))
                {
                    weeksDays[i] =   value.ToString().Substring(0,3);
                    if (i == 6)
                    {
                        i = -1;
                    }
                    i++;
                }
                for (int j = 0; j < 7; j++)
                {
                    Console.Write("{0,-6}", weeksDays[j]);
                }
                Console.WriteLine();
                DateTime beginMonth;
                DateTime.TryParse("01." + resultDate.Month +"."+ resultDate.Year, out beginMonth);
                i = 0;
                string beginDay = beginMonth.DayOfWeek.ToString().Substring(0, 3);
                while (!beginDay.Equals(weeksDays[i]))
                {
                    i++;
                }
                int number = 1;
                Console.SetCursorPosition(i * 6, Console.CursorTop);
                Console.Write("{0,-6}", number);
                int weekend = 0;
                while(number<= DateTime.DaysInMonth(resultDate.Year,resultDate.Month))
                {
                    for (int k = i+1; k < 7; k++)
                    {
                        number++;
                        if (number > DateTime.DaysInMonth(resultDate.Year,resultDate.Month))
                        {
                            break;
                        }
                        i = -1;
                        if (k >= 5)
                        {
                            weekend++;
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        if (number == DateTime.Now.Day && (resultDate.Month == DateTime.Now.Month) && (resultDate.Year == DateTime.Now.Year))
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        if (number == resultDate.Day)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        Console.Write("{0,-6}", number);
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                    i = -1;
                }
                Console.WriteLine("Working days: {0}" , DateTime.DaysInMonth(resultDate.Year, resultDate.Month) - weekend);
            }
        }
    }
}
