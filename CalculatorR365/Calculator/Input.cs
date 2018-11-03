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

        public InputParser(string rawString)
        {
            RawString = rawString;
        }

        public string RawString { get; set; }

        public List<int> Numbers
        {
            get
            {
                List<int> convertedToInts = new List<int>();

                var splitByDelimiters = NumbersAsString.Trim().Split(Delimeters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    //assuming: sum up only of all members are integers. otherwise return empty list
                    convertedToInts = Array.ConvertAll(splitByDelimiters, int.Parse).ToList();
                }
                catch
                { }

                return convertedToInts;
            }
        }

        public SupportedOperations Operation
        {
            get
            {
                if (IsOperationSpecified)
                {
                    var oprtationsSign = RawString.Substring(0, 1);

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
        }
        private string NumbersAsString
        {
            get
            {
                if (IsDelimiterSpecified)
                {
                    //get everything after the delimiters
                    var indexOfDelimiterEscape = RawString.IndexOf("//");
                    int indexEndOfDelimiters = RawString.Substring(indexOfDelimiterEscape + 2).IndexOf('\n');
                    return RawString.Substring(indexEndOfDelimiters + indexOfDelimiterEscape + 3);
                }
                else if (IsOperationSpecified)
                {
                    //get everything after the operation
                    return RawString.Substring(2);
                }
                return RawString;
            }
        }

        private List<string> Delimeters
        {
            get
            {
                List<string> delimiters = new List<string>();

                if (IsDelimiterSpecified)
                {
                    string multipleDelimitersPattern = @"\[([^)]+)\]";
                    Match delimitersExtracted = Regex.Match(RawString, multipleDelimitersPattern);

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
                        var indexOfDelimiterEscape = RawString.IndexOf("//");
                        delimiters.Add(RawString.Substring(indexOfDelimiterEscape + 2, 1));
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
        }
        private bool IsDelimiterSpecified
        {
            get
            {
                if (RawString == null || RawString.Length < 3)
                {
                    return false;
                }

                var indexOfDelimiterEscape = RawString.IndexOf("//");

                return indexOfDelimiterEscape > -1;
            }
        }

        private bool IsOperationSpecified
        {
            get
            {
                if (RawString == null || RawString.Length < 3)
                {
                    return false;
                }

                string possibleOperation = RawString.Substring(0, 2);

                if (AvaiableOperations.Contains(possibleOperation))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
