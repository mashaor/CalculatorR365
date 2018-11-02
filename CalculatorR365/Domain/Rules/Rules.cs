using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rules
{
    public class IgnoreNegatives : IRule
    {
        public List<int> Apply(List<int> input)
        {
            //check for any negative numbers
            if (input.Any(n => n < 0))
            {
                var allNegatives = String.Join(", ", input.Where(n => n < 0));
                throw new Exception(string.Format("Negatives are not allowed: {0}", allNegatives));
            }

            return input;
        }
    }

    public class ExcludeLargeNumbers : IRule
    {
        public List<int> Apply(List<int> input)
        {
            return input.Where(n => n < 1000).ToList();
        }
    }
}
