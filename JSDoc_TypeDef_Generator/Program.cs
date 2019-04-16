using Mono.Options;
using System;
using System.Collections.Generic;

namespace JSDoc_TypeDef_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            string JSONString;
            if (args.Length == 1)
            {
                JSONString = args[0];
            }
            else {
#if DEBUG
                Console.WriteLine("Enter JSON Object:");
                JSONString = Console.ReadLine();
#else
                Console.Error.WriteLine("This utility generates a JSDOC TypeDef comment based on a JSON object");
                Console.Error.WriteLine("Usage: gentypedef <JSON Object>");
                return;
#endif
            }

            Console.ReadLine();
        }
    }
}
