using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        [TestMethod]
        public void TestAddWithNewLine()
        {
            Calculate calc = new Calculate();

            string numbers = "1\n2,3";
            int result = calc.Add(numbers);
            Assert.AreEqual(result, 6);

            numbers = "1\n,2,3";
            result = calc.Add(numbers);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void TestAddWithDelimiters()
        {
            Calculate calc = new Calculate();

            string numbers = "//;\n1;2";
            int result = calc.Add(numbers);
            Assert.AreEqual(result, 3);

            numbers = "//?\n1?2?5";
            result = calc.Add(numbers);
            Assert.AreEqual(result, 8);

            numbers = "//?\n1?2,5";
            result = calc.Add(numbers);
            Assert.AreEqual(result, 0);

            numbers = "//?\n";
            result = calc.Add(numbers);
            Assert.AreEqual(result, 0);

            numbers = "//";
            result = calc.Add(numbers);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void TestNegatives()
        {
            Calculate calc = new Calculate();

            try
            {
                string numbers = "//;\n1;-2";
                int result = calc.Add(numbers);
            }
            catch (Exception e)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(e.Message));
            }           
        }
    }
}
