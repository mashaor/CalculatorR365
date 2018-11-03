using Domain;

namespace Calculator
{
    public class Calculate
    {
        public int ParseAndCalculate(string numbers)
        {
            string preParseNewLines = numbers.Replace("\\n", "\n");

            InputParser inputParser = new InputParser(preParseNewLines);

            CalculationObject calcObj = new CalculationObject();
            calcObj.InputNumbers = inputParser.Numbers;
            calcObj.Operation = inputParser.Operation;

            int result = calcObj.PerformCalculation();

            return result;
        }


    }
}
