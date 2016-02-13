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
            DateTime result_date;
            if (!DateTime.TryParse(date, out result_date))
            {
                Console.WriteLine("Error date format!");
            }
            else
            {
                string[] weeks_days = new string[7];
                int i = 6;
                foreach (var value in Enum.GetValues(typeof(DayOfWeek)))
                {
                    weeks_days[i] =   value.ToString().Substring(0,3);
                    if (i == 6)
                    {
                        i = -1;
                    }
                    i++;
                }
                for (int j = 0; j < 7; j++)
                {
                    Console.Write("{0,-6}", weeks_days[j]);
                }
                Console.WriteLine();
                DateTime begin_month;
                DateTime.TryParse("01." + result_date.Month +"."+ result_date.Year, out begin_month);
                i = 0;
                string begin_day = begin_month.DayOfWeek.ToString().Substring(0, 3);
                while (!begin_day.Equals(weeks_days[i]))
                {
                    i++;
                }
                int number = 1;
                Console.SetCursorPosition(i * 6, Console.CursorTop);
                Console.Write("{0,-6}", number);
                int weekend = 0;
                while(number<= DateTime.DaysInMonth(result_date.Year,result_date.Month))
                {
                    for (int k = i+1; k < 7; k++)
                    {
                        number++;
                        if (number > DateTime.DaysInMonth(result_date.Year,result_date.Month))
                        {
                            break;
                        }
                        i = -1;
                        if (k >= 5)
                        {
                            weekend++;
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        if (number == DateTime.Now.Day && (result_date.Month == DateTime.Now.Month) && (result_date.Year == DateTime.Now.Year))
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        if (number == result_date.Day)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        Console.Write("{0,-6}", number);
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Working days: {0}" , DateTime.DaysInMonth(result_date.Year, result_date.Month) - weekend);
            }
        }
    }
}
