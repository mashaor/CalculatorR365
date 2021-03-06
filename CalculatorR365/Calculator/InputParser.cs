﻿using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class InputParser
    {
        private static List<string> AvaiableOperations = new List<string>() { "+\n", "-\n", "*\n", "/\n" };

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

            calcObj.InputNumbers = convertedToInts;

            return calcObj;
        }

        public SupportedOperations ParseOperation(string numbers)
        {
            if (IsOperationSpecified(numbers))
            {
                var oprtationsSign = numbers.Substring(0, 1);

                switch (oprtationsSign)
                {
                    case "+":
                        return SupportedOperations.Add;
                    case "*":
                        return SupportedOperations.Multiply;
                    case "-":
                        return SupportedOperations.Subtract;
                    case "/":
                        return SupportedOperations.Divide;
                }
            }

            return SupportedOperations.Add;
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
