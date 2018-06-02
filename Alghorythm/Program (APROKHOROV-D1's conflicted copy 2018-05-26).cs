using GenAlgorithm.Crossover;
using GenAlgorithm.Generation;
using GenAlgorithm.Selection;
using Population;
using System;

namespace GenAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            new Algorithm(200, 100).Run();
            Console.ReadLine();
        }
    }
}
