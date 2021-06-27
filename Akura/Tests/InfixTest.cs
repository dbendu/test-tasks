using Calculator;
using System;
using Xunit;

namespace Tests
{
    public class InfixTest
    {
        private SimpleCalculator Calculator;

        public InfixTest()
        {
            Calculator = new SimpleCalculator();
        }

        [Fact]
        public void Valid()
        {
            Assert.Equal(1, Calculator.Evaluate("1"));
            Assert.Equal(-1, Calculator.Evaluate("-1"));
            Assert.Equal(1, Calculator.Evaluate("0 + 1"));
            Assert.Equal(1, Calculator.Evaluate("0+1"));
            Assert.Equal(1, Calculator.Evaluate("0+                         1"));
            Assert.Equal(8, Calculator.Evaluate("10 - 2"));
            Assert.Equal(5, Calculator.Evaluate("10 / 2"));
            Assert.Equal(20, Calculator.Evaluate("10 * 2"));
            Assert.Equal(16, Calculator.Evaluate("10 + 2 * 3"));
            Assert.Equal(3, Calculator.Evaluate("1 + 2 * 3 - 4"));
            Assert.Equal(-8, Calculator.Evaluate("1 + 2 * 24 / 4 / 2 - 15"));
            Assert.Equal(-2, Calculator.Evaluate("1 + -3"));
            Assert.Equal(-20, Calculator.Evaluate("1 + 2 * 24 / -4 / 2 - 15"));
            Assert.Equal(3, Calculator.Evaluate("-2 - -5"));
            Assert.Equal(0, Calculator.Evaluate("0 / 5"));
        }

        [Fact]
        public void InvalidFormat()
        {
            Assert.Throws<Exception>(() => Calculator.Evaluate(""));
            Assert.Throws<Exception>(() => Calculator.Evaluate("0-"));
            Assert.Throws<Exception>(() => Calculator.Evaluate("0+"));
            Assert.Throws<Exception>(() => Calculator.Evaluate("+0"));
            Assert.Throws<Exception>(() => Calculator.Evaluate("1 + 2 +"));
            Assert.Throws<Exception>(() => Calculator.Evaluate("++"));
            Assert.Throws<Exception>(() => Calculator.Evaluate("/9"));
            Assert.Throws<Exception>(() => Calculator.Evaluate("-2 +"));
            Assert.Throws<Exception>(() => Calculator.Evaluate("-2 - +5"));
        }

        [Fact]
        public void DivisionByZero()
        {
            Assert.Throws<Exception>(() => Calculator.Evaluate("1/0"));
            Assert.Throws<Exception>(() => Calculator.Evaluate("15 * 0 / 0"));
        }
    }
}
