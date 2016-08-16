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
            PrintOutcome(isVirtual, "Virtuosity");

            var memberDoesNotExist = typeof(Program).GetMethod(nameof(RemovedMember)) == null;
            PrintOutcome(memberDoesNotExist, "Obsolete");
        }

        private static void PrintOutcome(bool success, string test)
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{test}: It Works!!!!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{test}: It didn't work :(");
            }
            Console.ResetColor();
        }

        public void TestMethod()
        {

        }

        [ObsoleteEx(RemoveInVersion = "0.0.0.0")]
        public void RemovedMember()
        {
            
        }
    }
}
