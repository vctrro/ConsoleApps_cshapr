using System;

namespace Arrays
{
    class Program
    {
        //ИНДЕКСАТОРЫ - классы к которым можно обращаться, как к массивам
        class Person
        {
            public string Name { get; set; }
        }
        class People
        {
            Person[] data;
            public People()
            {
                data = new Person[5];
            }
            // индексатор
            public Person this[int index]
            {
                get
                {
                    return data[index];
                }
                set
                {
                    data[index] = value;
                }
            }
        }
        static void Main(string[] args)
        {
            People people = new People();
            people[0] = new Person { Name = "Tom" };
            people[1] = new Person { Name = "Bob" };

            Person tom = people[0];
            Console.WriteLine(tom?.Name);

            //МАССИВЫ
            int[] arr10 = new int[4];
            int[] arr12 = new int[4] { 1, 2, 3, 5 };
            int[] arr13 = new int[] { 1, 2, 3, 5 };
            int[] arr14 = new[] { 1, 2, 3, 5 };
            int[] arr15 = { 1, 2, 3, 5 };

            int[,] arr20 = new int[2, 3]; // двухмерный массив (матрица)
            int[,] arr21 = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
            int[,] arr22 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            int[,] arr23 = new[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            int[,] arr24 = { { 1, 2, 3 }, { 4, 5, 6 } };

            int[,,] arr30 = new int[3, 4, 5]; // трёхмерный массив

            int[][] arr40 = new int[5][]; // зубчатый массив, массив из массивов разной длины
            arr40[0] = arr10;
            arr40[1] = new int[] { 10, 5, 7, 15 };
            Array.Copy(arr13, arr40[2], arr13.Length);
            Array.Copy(arr14, 0, arr40[3], 5, arr13.Length);




            Console.WriteLine($"{arr40[3]}");
        }
    }
}
