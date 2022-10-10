using System;

namespace Interpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter();

            var source = " double x = )(10 + (x/2);";

            interpreter.runtime(source);



        }
    }
}
