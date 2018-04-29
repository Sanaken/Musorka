using Population;
using System;
using GenAlgorithm.Mutation;
namespace GenAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            new Algorithm(50000, 100, "Matrix43Swith").Run();
            new NearestNeighbour("Matrix43Swith").Run();

            Console.ReadLine();
        }
    }
}
