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

            try
            {
                //assuming: sum up only of all members are integers. otherwise return 0
                var convertedToInts = Array.ConvertAll(splitByDelimiter, int.Parse);

                return convertedToInts.Sum();
            }
            catch
            {
                return 0;
            }
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
            if(numbers.Length < 3)
            {
                return false;
            }

            var firstTwoChars = numbers.Substring(0,2);

            return firstTwoChars == "//";
        }
    }
}
