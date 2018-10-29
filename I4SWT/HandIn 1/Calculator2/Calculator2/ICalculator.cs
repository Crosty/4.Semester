using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator2
{
    public interface ICalculator
    {
        double Add(double a, double b);

        double Subtract(double a, double b);

        double Multiply(double a, double b);

        double Power(double x, double exp);

        //Other functions
        double Divide(double dividend, double divisor);

        void Clear();

        double Add(double addend);

        double Subtract(double subtractor);

        double Multiply(double multiplier);

        double Divide(double divisor);

        double Power(double exponent);
    }
}
