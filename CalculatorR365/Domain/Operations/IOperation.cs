using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Operations
{
    interface IOperation
    {
        int Calculate(List<int> input);

        bool IsMatch(SupportedOperations operation);
        
    }
}
