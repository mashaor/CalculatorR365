using Calculator;
using System;

namespace CalculatorR365
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            string numbers = string.Empty;

            if (args.Length > 0)
            {
                numbers = args[0];               
            }
            else
            {
                Console.WriteLine("Enter numbers string: ");
                numbers = Console.ReadLine();
            }

            try
            {
                Calculate calc = new Calculate();
                result = calc.Add(numbers);

                Console.WriteLine("Result is: " + result);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
