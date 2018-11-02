using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class Calculate
    {
        public int Add(string numbers)
        {
            List<int> convertedToInts = ParseInput(numbers);

            convertedToInts = ApplyRules(convertedToInts);

            int sum = convertedToInts.Sum();

            return sum;
        }

        public int PerformCalculation(CalculationObject calcObj)
        {
            int result = 0;
            switch (calcObj.Operation)
            {
                case "+":
                    result = Add(calcObj.InputNumbers);
                    break;
                case "*":
                    result = Multiply(calcObj.InputNumbers);
                    break;
                case "-":
                    result = Subtract(calcObj.InputNumbers);
                    break;
                case "/":
                    result = Divide(calcObj.InputNumbers);
                    break;
            }

            return result;
        }

        public int Add(List<int> input)
        {
            int result = 0;

            if (input != null)
            {
                result = input.Sum();
            }

            return result;
        }

        public int Multiply(List<int> input)
        {
            int result = 0;

            if (input != null)
            {
                result = input.Aggregate(1, (a, b) => a * b);
            }

            return result;
        }

        public int Divide(List<int> input)
        {
            int result = 0;

            if (input != null && input.Count > 0)
            {
                result = input[0];

                for (int i = 1; i < input.Count; i++)
                {
                    if (input[i] != 0)
                    {
                        result /= input[i];
                    }
                }
            }

            return result;
        }

        public int Subtract(List<int> input)
        {
            int result = input[0];

            for (int i = 1; i < input.Count; i++)
            {
                result -= input[i];
            }

            return result;
        }

        public List<int> ParseInput(string numbers)
        {
            List<int> convertedToInts = new List<int>();

            if (string.IsNullOrEmpty(numbers) || string.IsNullOrWhiteSpace(numbers))
            {
                return convertedToInts;
            }

            var delimiters = ParseDelimiters(numbers).ToArray();

            var extractedNumbersString = ExtractNumbersString(numbers);

            var splitByDelimiters = extractedNumbersString.Trim().Split(delimiters, StringSplitOptions.None);

            try
            {
                //assuming: sum up only of all members are integers. otherwise return 0
                convertedToInts = Array.ConvertAll(splitByDelimiters, int.Parse).ToList();
            }
            catch
            { }

            return convertedToInts;
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

        public List<string> ParseDelimiters(string numbers)
        {
            //assuming only the new line is a valid delimiter
            List<string> delimiters = new List<string>() { "\n" };

            if (IsDelimiterSpecified(numbers))
            {
                if (numbers.Contains('[') && numbers.Contains(']'))
                {
                    //Miltiple delimiters
                    var delimiterBrackets = (new List<char>() { '[', ']' }).ToArray();
                    int indexEndOfDelimiters = numbers.IndexOf('\n') - 1;
                    var extractedDelimiters = numbers.Substring(0, indexEndOfDelimiters)
                                                     .Replace("//", string.Empty)
                                                     .Split(delimiterBrackets)
                                                     .Where(s => string.IsNullOrEmpty(s) == false)
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
                //no delimiters specified by the user
                delimiters.Add(",");
            }

            return delimiters;
        }

        public string ExtractNumbersString(string numbers)
        {
            if (IsDelimiterSpecified(numbers))
            {
                int indexEndOfDelimiters = numbers.IndexOf('\n');
                return numbers.Substring(indexEndOfDelimiters + 1);
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
    }
}
