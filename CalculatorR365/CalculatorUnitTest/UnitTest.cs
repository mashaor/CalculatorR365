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
            int result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 0);

            numbers = "test";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 0);

            numbers = "1";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 1);

            numbers = "1,2";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void TestAddWithNewLine()
        {
            Calculate calc = new Calculate();

            string numbers = "1\n2,3";
            int result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 6);

            numbers = "1\n,2,3";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void TestAddWithDelimiters()
        {
            Calculate calc = new Calculate();

            string numbers = "//;\n1;2";
            int result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 3);

            numbers = "//?\n1?2?5";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 8);

            numbers = "//?\n1?2,5";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 0);

            numbers = "//?\n";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 0);

            numbers = "//";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void TestNegatives()
        {
            Calculate calc = new Calculate();

            try
            {
                string numbers = "//;\n1;-2";
                int result = calc.ParseAndCalculate(numbers);
            }
            catch (Exception e)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(e.Message));
            }
        }

        [TestMethod]
        public void TestIgnoreLargeNumbers()
        {
            Calculate calc = new Calculate();

            string numbers = "1,3,1001,4,2000";
            int result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 8);

            numbers = "1001,1500,2000";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void TestAddWithLongDelimiters()
        {
            Calculate calc = new Calculate();

            string numbers = "//[***]\n1***2***6";
            int result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 9);

            numbers = "//[***][^^]\n1***2^^6***9^^3";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 21);

            numbers = "//[*][%]\n1*2%3";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 6);

            numbers = "//[*][%]\n1";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void TestMultiply()
        {
            Calculate calc = new Calculate();

            string numbers = "*\n//[***]\n1***2***6";
            int result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 12);

            numbers = "*\n//[*][%]\n1";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 1);

            numbers = "*\n1,3,1001,4,2000";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 12);

        }


        [TestMethod]
        public void TestSubtract()
        {
            Calculate calc = new Calculate();

            string numbers = "-\n//[***]\n6***1***2";
            int result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 3);

            numbers = "-\n//[*][%]\n1";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 1);

            numbers = "-\n7,2,1001,1,2000";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 4);

        }

        [TestMethod]
        public void TestDivide()
        {
            Calculate calc = new Calculate();

            string numbers = "/\n//[***]\n20***2***5";
            int result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 2);

            numbers = "/\n//[*][%]\n1";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 1);

            numbers = "/\n16,4,1001,2,2000";
            result = calc.ParseAndCalculate(numbers);
            Assert.AreEqual(result, 2);

        }

    }
}
