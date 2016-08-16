using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var isVirtual = typeof(Program).GetMethod(nameof(TestMethod)).IsVirtual;
            if (isVirtual)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("It Works!!!!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("It didn't work :(");
            }
            Console.ResetColor();
        }

        public void TestMethod()
        {

        }
    }
}
