using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Calculator2;

namespace Calculator.Test.Unit
{
    [TestFixture]
    [Author("SWTGruppe12")]
    public class TestCalculator
    {
        private ICalculator _uut;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _uut = new Calculator2.Calculator();
        }

        [TestCase(3, 2, 5)]
        [TestCase(-3, -2, -5)]
        [TestCase(-3, 2, -1)]
        [TestCase(3, -2, 1)]
        [TestCase(3, 10, 13)]
        [TestCase(5, 5, 10)]
        [TestCase(4 , 4, 8)]
        public void Add_AddPosAndNegNumbers_ResultIsCorrect(double a, double b, double result)
        {
            Assert.That(_uut.Add(a, b), Is.EqualTo(result));
        }
        
        [TestCase(3, 2, 1)]
        [TestCase(-3, -2, -1)]
        [TestCase(-3, 2, -5)]
        [TestCase(3, -2, 5)]
        [TestCase(3, 3, 0)]
        [TestCase(8, 2, 6)]
        public void Subtract_SubtractPosAndNegNumbers_ResultIsCorrect(double a, double b, double result)
        {
            Assert.That(_uut.Subtract(a, b), Is.EqualTo(result));
        }


        [TestCase(3, 2, 6)]
        [TestCase(-3, -2, 6)]
        [TestCase(-3, 2, -6)]
        [TestCase(3, -2, -6)]
        [TestCase(0, -2, 0)]
        [TestCase(-2, 0, 0)]
        [TestCase(0, 0, 0)]
        public void Multiply_MultiplyNumbers_ResultIsCorrect(double a, double b, double result)
        {
            Assert.That(_uut.Multiply(a, b), Is.EqualTo(result));
        }

        [TestCase(2, 3, 8)]
        [TestCase(2, -3, 0.125)]
        [TestCase(-2, -3, -0.125)]
        [TestCase(1, 10, 1)]
        [TestCase(1, -10, 1)]
        [TestCase(10, 0, 1)]
        [TestCase(4, 0.5, 2.0)]
        [TestCase(9, 0.5, 3.0)]
        public void Power_RaiseNumbers_ResultIsCorrect(double x, double exp, double result)
        {
            Assert.That(_uut.Power(x, exp), Is.EqualTo(result));
        }

        //TestCases on the other functions

        [TestCase(2, 2, 4, 8)]
        [TestCase(2, 4, 6, 12)]
        [TestCase(5, 5, 10, 20)]
        public void Add_AddNumbersTogether_ResultIsCorrect(double a, double b, double addend, double result)
        {
            _uut.Add(a, b);
            Assert.That(_uut.Add(addend), Is.EqualTo(result));
        }

        [TestCase(20, 5, 15, 0)]
        [TestCase(6, 1, 3, -2)]
        [TestCase(-2, 1, 5, 8)]
        public void Subtract_SubtractNumbersTogether_ResultIsCorrect(double a, double b, double subtractor,
            double result)
        {
            _uut.Subtract(a, b);
            Assert.That(_uut.Subtract(subtractor), Is.EqualTo(result));
        }

        [TestCase(3, 3, 3, 27)]
        [TestCase(4, 4, 4, 64)]
        [TestCase(2, 3, -6, -36)]
        [TestCase(4, 4, 10, 160)]
        public void Multiply_MultiplyNumbersTogether_ResultIsCorrect(double a, double b, double multiplier,
            double result)
        {
            _uut.Multiply(a, b);
            Assert.That(_uut.Multiply(multiplier), Is.EqualTo(result));
        }

        [TestCase(2, 2, 2, 16)]
        [TestCase(6, 6, 2, 2176782336)]
        [TestCase(1, 1, 1, 1)]
        [TestCase(3, 2, 2, 81)]
        public void Power_PowerNumbersTogether_ResultIsCorrect(double x, double exp, double exponent, double result)
        {
            _uut.Power(x, exp);
            Assert.That(_uut.Power(exponent), Is.EqualTo(result));
        }

        [TestCase(6, 2, 3)]
        [TestCase(0, 2, 0)]
        [TestCase(-4, 2, -2)]
        public void Divide_DivideNumbers_ResultIsCorrect(double dividend, double divisor, double result)
        {
            Assert.That(_uut.Divide(dividend, divisor), Is.EqualTo(result));
        }

        [TestCase(0, 2, 2, 0)]
        [TestCase(6, 2, 6, 0.5)]
        [TestCase(3, 3, 1, 1)]
        public void Divide_DivideNumbersTogether_ResultIsCorrect(double dividend, double divisor, double divisor2, double result)
        {
            _uut.Divide(dividend, divisor);
            Assert.That(_uut.Divide(divisor2), Is.EqualTo(result));
        }
    }
}
