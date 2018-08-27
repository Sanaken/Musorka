﻿using Population;
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
            int[] solution = new int[mMWrapper.Matrix.GetLength(1)];

            List<int> leftCities = new List<int>();
            solution[0] = new Random().Next(mMWrapper.Matrix.GetLength(1));

            for (int i = 0; i < mMWrapper.Matrix.GetLength(1); i++)
            {
                leftCities.Add(i);
            }
            leftCities.Remove(solution[0]);

            for (int i = 1; i < mMWrapper.Matrix.GetLength(1); i++)
            {
                double buffer = double.MaxValue;
                int currentNeighbour = 0;

                foreach(int city in leftCities)
                {
                    if (mMWrapper.Matrix[city,solution[i-1]] < buffer)
                    {
                        currentNeighbour = city;
                        buffer = mMWrapper.Matrix[city, solution[i - 1]];
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
