using Calculator;
using System;
using Xunit;

namespace Tests
{
    public class PrefixTest
    {
        private SimpleCalculator Calculator;

        public PrefixTest()
        {
            Calculator = new SimpleCalculator();
        }

        [Fact]
        public void Valid()
        {
            Assert.Equal(1, Calculator.EvaluatePrefix("1"));
            Assert.Equal(-1, Calculator.EvaluatePrefix("-1"));
            Assert.Equal(1, Calculator.EvaluatePrefix("+ 0 1"));
            Assert.Equal(1, Calculator.EvaluatePrefix("+0 1"));
            Assert.Equal(1, Calculator.EvaluatePrefix("+0                         1"));
            Assert.Equal(8, Calculator.EvaluatePrefix("- 10 2"));
            Assert.Equal(5, Calculator.EvaluatePrefix("/ 10  2"));
            Assert.Equal(20, Calculator.EvaluatePrefix("* 10 2"));
            Assert.Equal(16, Calculator.EvaluatePrefix("+ 10 * 2 3"));
            Assert.Equal(3, Calculator.EvaluatePrefix("+1 -* 2  3  4"));
            Assert.Equal(10, Calculator.EvaluatePrefix("+1 -* 2 / 24 / 4  2  15"));
            Assert.Equal(-2, Calculator.EvaluatePrefix("+ 1 -3"));
            Assert.Equal(-38, Calculator.EvaluatePrefix("+1 -* 2 / 24 / -4  2  15"));
            Assert.Equal(3, Calculator.EvaluatePrefix("- -2 -5"));
            Assert.Equal(0, Calculator.EvaluatePrefix("/ 0 5"));
        }

        [Fact]
        public void InvalidFormat()
        {
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix(""));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("- 0"));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("+ 0"));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("+0"));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("+ 1 + 2"));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("++"));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("/9"));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("+ -2"));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("- -2 +5"));
        }

        [Fact]
        public void DivisionByZero()
        {
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("/ 1 0"));
            Assert.Throws<Exception>(() => Calculator.EvaluatePrefix("* 15 / 0 0"));
        }
    }
}
