using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorUnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestSimpleAdd()
        {
            Calculate calc = new Calculate();

            string numbers = string.Empty;
            int result = calc.Add(numbers);
            Assert.AreEqual(result, 0);

            numbers = "test";
            result = calc.Add(numbers);
            Assert.AreEqual(result, 0);

            numbers = "1";
            result = calc.Add(numbers);
            Assert.AreEqual(result, 1);

            numbers = "1,2";
            result = calc.Add(numbers);
            Assert.AreEqual(result, 3);
        }
    }
}
