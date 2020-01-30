using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        enum Numbers { One = 1, Two, Five = 5, Six, Seven }
        static double SumNumbers(List<double[]> setsOfNumbers, int indexOfSetToSum)
        {
            return setsOfNumbers?[indexOfSetToSum]?.Sum() ?? double.NaN;
        }

        static void Main(string[] args)
        {
            var sum1 = SumNumbers(null, 0);
            Console.WriteLine(sum1);  // output: NaN
            var numberSets = new List<double[]> { new[] { 1.0, 2.0, 3.0 }, null };

            var sum2 = SumNumbers(numberSets, 0);
            Console.WriteLine(sum2);  // output: 6

            var sum3 = SumNumbers(numberSets, 1);
            Console.WriteLine(sum3);  // output: NaN

            Numbers number;
            for (number = Numbers.One; number <= Numbers.Seven; number++)
                Console.WriteLine("Элемент: \"{0}\", значение {1}", number, (int)number);

            number = Numbers.Five;
            number +=3;
            Console.WriteLine(number);


        }
    }
}
