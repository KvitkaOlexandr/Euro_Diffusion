using System;
using System.Collections.Generic;

namespace Euro_diffusion
{
    class Program
    {
        static void Main(string[] args)
        {
            var mode = UI.Mode();
            if (mode == 1)
                TestMode();
            else
                ConsoleMode();
        }

        public static void TestMode()
        {
            var testCases = UI.BufferReader(UI.FileReader());
            for (int i = 0; i < testCases.Count; i++)
            {
                Console.WriteLine($"Case Number {i}");
                var test = new Simulator(testCases[i]);
                test.Start();
            }
        }

        public static void ConsoleMode()
        {
            Console.WriteLine("");
        }
    }
}
