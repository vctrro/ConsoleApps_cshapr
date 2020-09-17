using System;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;

namespace Learning
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new System.Text.StringBuilder(1, 3);
            x.Insert(0, "df5");
            Console.WriteLine(x);
            Foo(x:2);

            var y = 64;
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

            scope.SetVariable("y", y);
            engine.Execute(@"print 'hello'");
            engine.ExecuteFile(@"Learning/code.py", scope);

            dynamic func1 = scope.GetVariable("factorial");
            dynamic res1 = func1(y);
            Console.WriteLine(res1);
        }

        static void Foo(int x = 1, int y = 3)
        {
            Console.WriteLine($"{x}, {y}");
        }
    }
}
