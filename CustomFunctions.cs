using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLibrary
{
    public class CustomFunctions
    {
        public static void Pause()
        {
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey(intercept: true);
        }
        static public int InputInteger(string stringForUser = "")
        {
            int input;
            if (stringForUser != "")
                Console.WriteLine(stringForUser);
            bool isInteger = Int32.TryParse(Console.ReadLine(), out input);
            while (!isInteger)
            {
                Console.WriteLine("Ошибка ввода! Попробуйте снова:");
                isInteger = Int32.TryParse(Console.ReadLine(), out input);
            }
            return input;
        }

        static public void CheckNumber(int lowerBound, int upperBound, ref int value, string msgRepetitive = "Неверное значение! Попробуйте снова: ")
        {
            if (lowerBound > upperBound)
                (lowerBound, upperBound) = (upperBound, lowerBound);
            while (value < lowerBound || value > upperBound)
            {
                Console.WriteLine(msgRepetitive);
                value = InputInteger();
            }
        }
        static public string InputString(string stringForUser = "")
        {
            if (stringForUser != "")
                Console.WriteLine(stringForUser);

            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input) || input.Any(char.IsDigit))
            {
                Console.WriteLine("Ошибка ввода! Пожалуйста, введите непустую строку без цифр:");
                input = Console.ReadLine();
            }

            return input;
        }

    }
}
