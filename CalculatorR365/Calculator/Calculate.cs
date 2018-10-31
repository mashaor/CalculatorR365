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

            var delimiters = ParseDelimiters(numbers).ToArray();

            var extractedNumbersString = ExtractNumbersString(numbers);

            var splitByDelimiters = extractedNumbersString.Trim().Split(delimiters, StringSplitOptions.None);

            List<int> convertedToInts = new List<int>();

            try
            {
                //assuming: sum up only of all members are integers. otherwise return 0
                convertedToInts = Array.ConvertAll(splitByDelimiters, int.Parse).ToList();
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

            int sum = convertedToInts.Where(n => n < 1000).Sum();

            return sum;

        }

        public List<string> ParseDelimiters(string numbers)
        {
            //assuming only the new line is a valid delimiter
            List<string> delimiters = new List<string>() { "\n" };

            if (IsDelimiterSpecified(numbers))
            {
                if (numbers[2] == '[')
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
                    delimiters.Add(numbers[2].ToString());
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

            var firstTwoChars = numbers.Substring(0, 2);

            return firstTwoChars == "//";
        }
    }
}
