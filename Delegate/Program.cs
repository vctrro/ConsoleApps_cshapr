using System;

namespace DelgateForNotes
{
    public class Program
    {
        // Объявление делегата, ссылающегося на функцию
        // с двумя параметрами и выводом булевого типа
        public delegate bool CompareDelegate(object lhs, object rhs);

        static void Main(string[] args)
        {
            // Создание массива объектов класса Student.cs
            Student[] students = {
        new Student("Mark", 1, 799),
        new Student("David", 2, 545),
        new Student("Lavish", 3, 999),
        new Student("Voora", 4, 228),
        new Student("Boll", 5, 768),
        new Student("Donna", 6, 367),
        new Student("Adam", 7, 799),
        new Student("Steve", 8, 867),
        new Student("Ricky", 9, 978),
        new Student("Brett", 10, 567)
      };

            // Создание делегата с передачей
            // статического метода класса Student в качестве аргумента
            // CompareDelegate StudentCompareOp = new CompareDelegate(Student.RhsIsGreater);

            CompareDelegate StudentCompareOp = Student.RhsIsGreater; // но проще так

            // Вызов статического метода класса BubbleSortClass,
            // передача массива объектов и делегата
            BubbleSortClass.Sort(students, StudentCompareOp);
            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine(students[i].ToString());
            }
            Console.Read();
        }
    }
}