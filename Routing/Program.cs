using System;

namespace Routing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new RouteAnalyzer().Process(new[] { "2 -> 3", "5, 6", "1 -> 2", "4" });
        }
    }
}
