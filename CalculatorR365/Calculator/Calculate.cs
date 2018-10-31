using System;
using System.Linq;

namespace Calculator
{
    public class Calculate
    {
        public int Add(string numbers)
        {
            if(string.IsNullOrEmpty(numbers) || string.IsNullOrWhiteSpace(numbers))
            {
                return 0;
            }

            var splitByDelimiter = numbers.Trim().Split(',');

            try
            {
                var convertedToInts = Array.ConvertAll(splitByDelimiter, int.Parse);

                return convertedToInts.Sum();
            }
            catch
            {
                return 0;
            }
        }
    }
}
