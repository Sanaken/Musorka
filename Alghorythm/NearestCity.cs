using Population;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgorithm
{
    public class NearestNeighbour
    {
        AMatrixWrapper mMWrapper;
        public NearestNeighbour(string filename)
        {
            mMWrapper = new SalesmanMatrixWrapper(filename + ".txt");
            if (mMWrapper.mState != 0)
            {
                Console.WriteLine("No matrix (code " + mMWrapper.mState + ")");
                return;
            }
        }

        public Person Run()
        {
            int[] solution = new int[mMWrapper.GetSize()];

            List<int> leftCities = new List<int>();
            solution[0] = new Random().Next(mMWrapper.GetSize());

            for (int i = 0; i < mMWrapper.GetSize(); i++)
            {
                leftCities.Add(i);
            }
            leftCities.Remove(solution[0]);

            for (int i = 1; i < mMWrapper.GetSize(); i++)
            {
                double buffer = double.MaxValue;
                int currentNeighbour = 0;

                foreach(int city in leftCities)
                {
                    if (mMWrapper.GetMatrix()[city,solution[i-1]] < buffer)
                    {
                        currentNeighbour = city;
                        buffer = mMWrapper.GetMatrix()[city, solution[i - 1]];
                    }
                }

                solution[i] = currentNeighbour;
                leftCities.Remove(currentNeighbour);
            }

            Console.WriteLine(mMWrapper.FitnessFunction(new Person(solution)));
            return new Person(solution);
        }
    }
}
