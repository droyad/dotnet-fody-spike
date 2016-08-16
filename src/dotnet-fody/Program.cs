using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_fody
{
    public class Program
    {
        public static void Main(string[] args)
        {
                Console.WriteLine(args.Length);
            foreach (var arg in args)
                Console.WriteLine(arg);
        }
    }
}
