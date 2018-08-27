using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Population;

namespace GenAlgorithm.Generation
{
    /**
     * This person generator is just for minimum-critery problems 
     * (like a salesman)
     */
    public class RouletteGeneration : IStrategyGeneration
    {
        Random mRand;
        public RouletteGeneration()
        {
            mRand = new Random();
        }
        public Person[] GeneratePop(AMatrixWrapper wrapper, int capacity)
        {
            Person[] retPop = new Person[capacity];

            int dimension = wrapper.Matrix.GetLength(1);
            double[,] matrix = wrapper.Matrix;

            for (int iter = 0; iter < capacity; iter++)
            {
                int[] retCode = new int[dimension];
                List<int> LeftCities = new List<int>();
                for (int i = 0; i < dimension; i++)
                    LeftCities.Add(i);

                int startCity = mRand.Next(dimension);

                retCode[0] = startCity;
                LeftCities.Remove(startCity);

                for (int i = 0; i < dimension - 1; i++)   // Array-completing cycle.
                {
                    double sum = 0;
                    foreach (int j in LeftCities)
                    {
                        sum += matrix[retCode[i], j];
                    }

                    sum = sum * sum;
                    double[] roulette = new double[dimension - i];
                    roulette[0] = 0;
                    for (int j = 0; j < dimension - i - 1; j++)
                    {
                        roulette[j + 1] = roulette[j] + sum / (matrix[retCode[i], LeftCities[j]]* matrix[retCode[i], LeftCities[j]]);
                    }

                    int randPoint = mRand.Next((int)roulette[dimension - i - 1]);
                    for (int j = 1; j < dimension - i; j++)
                    {
                        if (randPoint <= roulette[j])
                        {
                            retCode[i + 1] = LeftCities[j - 1];
                            LeftCities.RemoveAt(j - 1);
                            break;
                        }
                    }
                }
                retPop[iter] = new Person(retCode);
            }
            return retPop;
        }
    }
}
