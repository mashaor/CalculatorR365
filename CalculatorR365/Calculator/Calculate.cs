using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class Calculate
    {
        public int ParseInputString(string numbers)
        {
            CalculationObject calcObj = new CalculationObject();

            List<int> convertedToInts = ParseInput(numbers);

            convertedToInts = ApplyRules(convertedToInts);

            calcObj.InputNumbers = convertedToInts;

            calcObj.Operation = "+";

            int result = calcObj.PerformCalculation();

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
