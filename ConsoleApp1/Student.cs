namespace DelgateForNotes
{
    class Student
    {
        private string name;
        private int rollno;
        private int marks;
        // Инициализация объекта класса
        public Student(string name, int rollno, int marks)
        {
            this.name = name;
            this.rollno = rollno;
            this.marks = marks;
        }

        // Переопределение метода для вывода результата
        public override string ToString()
        {
            return string.Format("Name => {0}, RollNumber => {1}, Marks => {2} ", name, rollno, marks);
        }

        // Пользовательская функция сравнение, возвращающая булевое значение
        public static bool RhsIsGreater(object lhs, object rhs)
        {
             Student stdLhs = (Student)lhs;
            Student stdRhs = (Student)rhs;
            return stdRhs.marks > stdLhs.marks;
        }
    }
}