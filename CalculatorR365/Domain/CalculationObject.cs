using Domain.Enums;
using Domain.Operations;
using Domain.Rules;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class CalculationObject
    {
        private List<IOperation> ListOfOperations;
        private List<IRule> ListOfRules;
        public SupportedOperations Operation { get; set; }
        public List<int> InputNumbers { get; set; }

        
        public CalculationObject()
        {
            InputNumbers = new List<int>();

            ListOfOperations = new List<IOperation>();
            ListOfOperations.Add(new Add());
            ListOfOperations.Add(new Subtract());
            ListOfOperations.Add(new Multiply());
            ListOfOperations.Add(new Divide());

            ListOfRules = new List<IRule>();
            ListOfRules.Add(new IgnoreNegatives());
            ListOfRules.Add(new ExcludeLargeNumbers());
        }

        public int PerformCalculation()
        {
            int result = 0;

            foreach(var rule in ListOfRules)
            {
                InputNumbers = rule.Apply(InputNumbers);
            }

            var operationToPerform = ListOfOperations.FirstOrDefault(o => o.IsMatch(Operation));

            if (operationToPerform != null)
            {
                result = operationToPerform.Calculate(InputNumbers);
            }

            return result;          
        }
    }
}
