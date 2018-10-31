using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class Calculate
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers) || string.IsNullOrWhiteSpace(numbers))
            {
                return 0;
            }

            var delimiters = ParseDelimiters(numbers);

            var extractedNumbersString = ExtractNumbersString(numbers);

            var splitByDelimiter = extractedNumbersString.Trim().Split(delimiters);

            List<int> convertedToInts = new List<int>();

            try
            {
                //assuming: sum up only of all members are integers. otherwise return 0
                convertedToInts = Array.ConvertAll(splitByDelimiter, int.Parse).ToList();
            }
            catch
            {
                return 0;
            }

            //check for any negative numbers
            if (convertedToInts.Any(n => n < 0))
            {
                var allNegatives = String.Join(", ", convertedToInts.Where(n => n < 0));
                throw new Exception(string.Format("Negatives are not allowed: {0}", allNegatives));
            }

            int sum = convertedToInts.Sum();

            return sum;

        }

        public char[] ParseDelimiters(string numbers)
        {
            //assuming only the new line is a valid delimiter
            List<char> defaultDelimiters = new List<char>() { '\n' };

            if (IsDelimiterSpecified(numbers))
            {
                defaultDelimiters.Add(numbers[2]);
            }
            else
            {
                defaultDelimiters.Add(',');
            }

            return defaultDelimiters.ToArray();
        }

        public string ExtractNumbersString(string numbers)
        {

            if (IsDelimiterSpecified(numbers))
            {
                int indexEndOfDelimiters = numbers.IndexOf("\n");
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

            var firstTwoChars = numbers.Substring(0, 2);

            return firstTwoChars == "//";
        }
    }
}
