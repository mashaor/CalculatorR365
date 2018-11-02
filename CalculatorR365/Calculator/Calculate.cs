using Domain;

namespace Calculator
{
    public class Calculate
    {
        public int ParseInputStringAndCalculate(string numbers)
        {
            InputParser parser = new InputParser();

            CalculationObject calcObj = parser.ExtractOperationDelimiterNumbers(numbers);

            int result = calcObj.PerformCalculation();

            return result;
        }


    }
}
