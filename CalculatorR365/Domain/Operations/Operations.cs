using Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class Add : IOperation
    {
        public int Calculate(List<int> input)
        {
            int result = 0;

            if (input != null)
            {
                result = input.Sum();
            }

            return result;
        }

        public bool IsMatch(SupportedOperations operation)
        {
            return SupportedOperations.Add == operation;
        }
    }

    public class Multiply : IOperation
    {
        public int Calculate(List<int> input)
        {
            int result = 0;

            if (input != null)
            {
                result = input.Aggregate(1, (a, b) => a * b);
            }

            return result;
        }

        public bool IsMatch(SupportedOperations operation)
        {
            return SupportedOperations.Multiply == operation;
        }
    }

    public class Divide : IOperation
    {
        public int Calculate(List<int> input)
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

        public bool IsMatch(SupportedOperations operation)
        {
            return SupportedOperations.Divide == operation;
        }
    }

    public class Subtract : IOperation
    {
        public int Calculate(List<int> input)
        {
            int result = input[0];

            for (int i = 1; i < input.Count; i++)
            {
                result -= input[i];
            }

            return result;
        }

        public bool IsMatch(SupportedOperations operation)
        {
            return SupportedOperations.Subtract == operation;
        }
    }
}
