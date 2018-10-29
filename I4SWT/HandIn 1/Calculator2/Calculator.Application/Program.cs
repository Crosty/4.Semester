using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Calculator2.Calculator();

            Console.WriteLine("Enter the first number: ");
            var firstNumber = Console.ReadLine();
            var a = double.Parse(firstNumber ?? throw new InvalidOperationException("Error try again!"));

            Console.WriteLine("Enter the second number: ");
            var secondNumber = Console.ReadLine();
            var b = double.Parse(secondNumber ?? throw new InvalidOperationException("Error try again!"));

            Console.WriteLine("Add:");
            Console.WriteLine($"{firstNumber} + {secondNumber} = {test.Add(a, b)}");

            Console.WriteLine("\n\nSubtract:");
            Console.Write($"{firstNumber} - {secondNumber} = {test.Subtract(a, b)}");

            Console.WriteLine("\n\nMultiply:");
            Console.Write($"{firstNumber} * {secondNumber} = {test.Multiply(a, b)}");

            Console.WriteLine("\n\nPower:");
            Console.Write($"{firstNumber} power to {secondNumber} = {test.Power(a, b)}");

            Console.WriteLine("\n\nDivide:");
            Console.Write($"{firstNumber} / {secondNumber} = {test.Divide(a, b)}");

            Console.WriteLine("\n\nPress enter to exit");
            Console.ReadLine();
        }
    }
}
