using System;

namespace ConsoleApp3
{
    class Program
    {
        delegate Func<T, int> del1<T>();  //Func<int, int> - это тип возвращаемого значения функция (int) => int 
        delegate ref int renm1(ref int x);

        class Class1
        {
            delegate int renm1(int x);
        }
        static Int32 SampleFunc(Int32 a, Int32 b, Int32 c)
        {
            return a + b + c;
        }

        //перегруженные версии ApplyPartial принимают аргументы и подставляют их в другие позиции в окончательном выполнении    функции
        static Func<T2, T3, TResult> ApplyPartial<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function, T1 arg1)
        {
            return (b, c) => function(arg1, b, c);
        }

        static Func<T3, TResult> ApplyPartial<T2, T3, TResult>(Func<T2, T3, TResult> function, T2 arg2)
        {
            return (c) => function(arg2, c);
        }

        static Func<TResult> ApplyPartial<T3, TResult>(Func<T3, TResult> function, T3 arg3)
        {
            return () => function(arg3);
        }

        static void Main(string[] args)
        {
            Func<int, int> fun1 = i => i * i;
            del1<int> a = () => fun1;

            int fun2(int x, del1<int> y) => y()(x);

            Func<Int32, Int32, Int32, Int32> function = SampleFunc;

            Func<Int32, Int32, Int32> partial1 = ApplyPartial(function, 1);
            Func<Int32, Int32> partial2 = ApplyPartial(partial1, 2);
            Func<Int32> partial3 = ApplyPartial(partial2, 3);

            var resp = partial3(); // эта строчка вызовет исходную функцию

            Console.WriteLine($"{fun2(4, a)}");
            Console.ReadKey();
        }
    }
}
