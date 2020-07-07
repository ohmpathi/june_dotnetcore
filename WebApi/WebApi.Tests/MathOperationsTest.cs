using System;
using Xunit;

namespace WebApi.Tests
{
    public class MathOperationsTest
    {
        [Fact]
        public void AddTest()
        {
            MathOperations instance = new MathOperations();

            double result = instance.Add(10, 12);

            Assert.Equal(22, result);
        }

        [Fact]
        public void DivideTest1()
        {
            MathOperations instance = new MathOperations();

            double result = instance.Divide(10, 2);

            Assert.Equal(5, result);
        }

        [Fact]
        public void DivideTest2()
        {
            MathOperations instance = new MathOperations();

            Assert.Throws<DivideByZeroException>(
                () => instance.Divide(10, 0)
            );
        }
    }
}
