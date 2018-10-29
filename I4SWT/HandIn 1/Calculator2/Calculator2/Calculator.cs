using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator2
{
    public class Calculator : ICalculator
    {
        public Calculator() { }
        public double Add(double a, double b)
        {
            var result = a + b;
            Accumulator = result;
            return result;
        }

        public double Subtract(double a, double b)
        {
            var result = a - b;
            Accumulator = result;
            return result;
        }

        public double Multiply(double a, double b)
        {
            var result = a * b;
            Accumulator = result;
            return result;
        }

        public double Power(double x, double exp)
        {
            var result = Math.Pow(x, exp);
            Accumulator = result;
            return result;
        }

        //Other functions
        public double Divide(double dividend, double divisor)
        {
            try
            {
                var result = dividend / divisor;
                Accumulator = result;
                return result;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine($"An exception has occured! The attempt of dividing by zero");
                return 0;
            }
        }

        public double Accumulator { get; private set; }

        public void Clear()
        {
            Accumulator = 0;
        }

        public double Add(double addend)
        {
            var result = addend + Accumulator;
            Accumulator = result;
            return result;
        }

        public double Subtract(double subtractor)
        {
            var result = subtractor - Accumulator;
            Accumulator = result;
            return result;
        }

        public double Multiply(double multiplier)
        {
            var result = multiplier * Accumulator;
            Accumulator = result;
            return result;
        }

        public double Divide(double divisor)
        {
            try
            {
                var result = Accumulator / divisor;
                Accumulator = result;
                return result;
            }

            catch(DivideByZeroException)
            {
                Console.WriteLine($"An exception has occured! The attempt of dividing by zero");
                return 0;
            }
        }

        public double Power(double exponent)
        {
            var result = Math.Pow(Accumulator, exponent);
            Accumulator = result;
            return result;
        }
    }
}
