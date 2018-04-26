using Population;
using System;

namespace GenAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            new Algorithm(50000, 100, "Oliver30").Run();
            new NearestNeighbour("Oliver30").Run();
            Console.ReadLine();
        }
    }
}
