using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Calculate
    {
        private static List<string> AvaiableOperations = new List<string>() { "+\n", "-\n", "*\n", "/\n" };


        public int ParseInputStringAndCalculate(string numbers)
        {
            CalculationObject calcObj = ExtractOperationDelimiterNumbers(numbers);
            int result = calcObj.PerformCalculation();

            return result;
        }

        public CalculationObject ExtractOperationDelimiterNumbers(string numbers)
        {
            CalculationObject calcObj = new CalculationObject();

            calcObj.Operation = ParseOperation(numbers);

            List<int> convertedToInts = new List<int>();

            if (string.IsNullOrEmpty(numbers) || string.IsNullOrWhiteSpace(numbers))
            {
                return calcObj;
            }

            var delimiters = ParseDelimiters(numbers).ToArray();

            var extractedNumbersString = ExtractNumbersString(numbers);

            var splitByDelimiters = extractedNumbersString.Trim().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                //assuming: sum up only of all members are integers. otherwise return 0
                convertedToInts = Array.ConvertAll(splitByDelimiters, int.Parse).ToList();
            }
            catch
            { }

            convertedToInts = ApplyRules(convertedToInts);

            calcObj.InputNumbers = convertedToInts;



            return calcObj;
        }
        private List<int> ApplyRules(List<int> convertedToInts)
        {

            //check for any negative numbers
            if (convertedToInts.Any(n => n < 0))
            {
                var allNegatives = String.Join(", ", convertedToInts.Where(n => n < 0));
                throw new Exception(string.Format("Negatives are not allowed: {0}", allNegatives));
            }

            //exclude numbers larger than 1000
            convertedToInts = convertedToInts.Where(n => n < 1000).ToList();

            return convertedToInts;
        }

        public string ParseOperation(string numbers)
        {
            if (IsOperationSpecified(numbers))
            {
                return numbers.Substring(0, 1);
            }

            return "+";
        }


        public List<string> ParseDelimiters(string numbers)
        {
            List<string> delimiters = new List<string>();

            if (IsDelimiterSpecified(numbers))
            {
                string multipleDelimitersPattern = @"\[([^)]+)\]";
                Match delimitersExtracted = Regex.Match(numbers, multipleDelimitersPattern);

                if (string.IsNullOrEmpty(delimitersExtracted.Value) == false)
                {
                    //Miltiple delimiters
                    var delimiterBrackets = (new List<char>() { '[', ']' }).ToArray();
                    var extractedDelimiters = delimitersExtracted.Value
                                                     .Split(delimiterBrackets, StringSplitOptions.RemoveEmptyEntries)
                                                     .ToList();

                    delimiters.AddRange(extractedDelimiters);
                }
                else
                {
                    //single delimiter
                    var indexOfDelimiterEscape = numbers.IndexOf("//");
                    delimiters.Add(numbers.Substring(indexOfDelimiterEscape + 2, 1));
                }
            }
            else
            {
                //no delimiters specified by the user - add default
                delimiters.Add(",");
                delimiters.Add("\n");
            }

            return delimiters;
        }

        public string ExtractNumbersString(string numbers)
        {
            if (IsDelimiterSpecified(numbers))
            {
                //get everything after the delimiters
                var indexOfDelimiterEscape = numbers.IndexOf("//");
                int indexEndOfDelimiters = numbers.Substring(indexOfDelimiterEscape + 2).IndexOf('\n');
                return numbers.Substring(indexEndOfDelimiters + indexOfDelimiterEscape + 3);
            }
            else if (IsOperationSpecified(numbers))
            {
                //get everything after the operation
                return numbers.Substring(2);
            }
            return numbers;
        }

        private bool IsDelimiterSpecified(string numbers)
        {
            if (numbers.Length < 3)
            {
                return false;
            }

            var indexOfDelimiterEscape = numbers.IndexOf("//");

            return indexOfDelimiterEscape > -1;
        }
        private bool IsOperationSpecified(string numbers)
        {
            if (numbers.Length < 3)
            {
                return false;
            }

            string possibleOperation = numbers.Substring(0, 2);

            if (AvaiableOperations.Contains(possibleOperation))
            {
                return true;
            }

            return false;
        }
    }
}
