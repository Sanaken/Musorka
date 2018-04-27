using Population;
using System;
using GenAlgorithm.Mutation;
namespace GenAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            new Algorithm(50000, 100, "Oliver30").Run();
            new NearestNeighbour("Oliver30").Run();

            /*Person pers = new Person(new int[]{ 1,3,2,5,7,4,0,6});

            pers.PrintPers();
            for (int pydr = 0; pydr < 10000; pydr++)
            new InversionMutation().Mutation(pers);
            pers.PrintPers();*/
            Console.ReadLine();
        }
    }
}
