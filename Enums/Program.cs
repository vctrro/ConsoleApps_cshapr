using System;
using System.Collections.Generic;
using System.Linq;

namespace Enums
{
    class Program
    {
        enum Numbers { One = 1, Two, Five = 5, Six, Seven, Any = 255 }


        public enum ColorsRGB //перечисления как битовые флаги, значения должны быть степенью двойки 1,2,4,8,16...
        {
            Red = 1,    // 0b_0000_0001
            Green = 2,  // 0b_0000_0010
            Blue = 4    // 0b_0000_0100
        }

        [Flags]
        public enum ColorsCMYK //перечисления как битовые флаги, значения должны быть степенью двойки 1,2,4,8,16...
        {
            Cyan = 1,       // 0b_0000_0001
            Magenta = 2,    // 0b_0000_0010
            Yellow = 4,     // 0b_0000_0100
            Key = 8         // 0b_0000_1000
        }

        static void Main(string[] args)
        {
            Numbers number;
            for (number = Numbers.One; number <= Numbers.Seven; number++)
                Console.WriteLine("Элемент: {0}, \tзначение {1}", number, (int)number); //странное поведение

            foreach (Numbers item in Enum.GetValues(typeof(Numbers)))
            {
                Console.WriteLine("{0} \t {1} \t {2} \t {3}",
                                    item.ToString("G"), item.ToString("F"), item.ToString("D"), item.ToString("X")); //форматирование вывода
            }

            int x = (int)Numbers.Six;        //
            number = (Numbers)x + 250;       // различные преобразования
            if (Numbers.Five != 0) number--; //            

            ColorsRGB RGB = ColorsRGB.Blue | ColorsRGB.Green | ColorsRGB.Red; //несколько значений в одну переменную, только если они 1,2,4,8,16
            Console.WriteLine(RGB); // 7  -- 1+2+4 нет атрибута [Flags]

            ColorsCMYK CMYK = ColorsCMYK.Cyan | ColorsCMYK.Magenta | ColorsCMYK.Yellow | ColorsCMYK.Key; //несколько значений в одну переменную, только если они 1,2,4,8,16
            Console.WriteLine(CMYK); // Cyan, Magenta, Yellow, Key  -- благодаря [Flags]

            ColorsCMYK Red = (ColorsCMYK)2 | (ColorsCMYK)4;
            ColorsCMYK Green = (ColorsCMYK)1 | (ColorsCMYK)4;
            ColorsCMYK Blue = (ColorsCMYK)1 | (ColorsCMYK)2;
            ColorsCMYK Black = (ColorsCMYK)1 | (ColorsCMYK)2 | (ColorsCMYK)4;

            Console.WriteLine($"{nameof(Blue)} color consists of the following colors: {Blue}");
            Console.WriteLine($"{nameof(Black)} color consists of the following colors: {Black}");

            if ((Blue & ColorsCMYK.Cyan) == ColorsCMYK.Cyan) Console.WriteLine("{0} color contained", ColorsCMYK.Cyan); // (Red & ColorsCMYK.Cyan) - битовое И даёт в сумме ColorsCMYK.Cyan, если он присутствует
            if (Green.HasFlag(ColorsCMYK.Yellow)) Console.WriteLine("{0} color contained in {1}", ColorsCMYK.Yellow, Green);

            Console.WriteLine("Common colors:");
            IDictionary<String, ColorsCMYK> colors = new Dictionary<String, ColorsCMYK>();
            colors.Add(nameof(Red), Red);
            colors.Add(nameof(Green), Green);
            colors.Add(nameof(Blue), Blue);
            colors.Add(nameof(Black), Black);
            foreach (var color1 in colors)
            {
                foreach (var color2 in colors)
                {
                    if (color1.Key != color2.Key)
                        Console.WriteLine($"{color1.Key} & {color2.Key} : {color1.Value & color2.Value}");
                }
            }
        }
    }
}
