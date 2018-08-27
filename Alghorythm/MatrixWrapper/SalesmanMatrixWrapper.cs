using Population;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenAlgorithm
{
    /**
     * A wrapper for Salesman problem matrix
     */
    public class SalesmanMatrixWrapper : AMatrixWrapper
    {      
        private bool _symmetric = true;
        public SalesmanMatrixWrapper(string filename) : base(filename)
        {
            for (int i = 0; i < mMatrix.GetLength(1); i++)
                for (int j = 0; j < mMatrix.GetLength(1); j++)
                    if ((i != j) && (mMatrix[i, j] != mMatrix[j, i]))
                    {
                        _symmetric = false;
                        return;
                    }                       
        }

        public override bool MatrixIsCorrect()
        {
            for(int i = 0; i < mMatrix.GetLength(1); i++)
                for(int j = 0; j < mMatrix.GetLength(1); j++)
                {
                    if (((i == j) && (mMatrix[i, j] != 0)) || ((i != j) && (mMatrix[i, j] == 0)))
                        return false;        
                }
            return true;
        }
        public override double FitnessFunction(Person person)
        {
            double result = 0;
            int[] code = person.GetCode();
            for (int i = 1; i < code.Count(); i++)
            {
                result += mMatrix[code[i - 1], code[i]];
            }
            return result + mMatrix[code[0], code.Last()];
        }

        public void PrintPers(Person p)
        {
            Console.Write(FitnessFunction(p) + "---");
            p.PrintPers();
        }

        public override int Distance(Person p1, Person p2)
        {
            int distance = 0, distanceRev = int.MaxValue;
            int[] code = p1.GetCode();
            int[] code2 = p2.GetCode();

            for (int i = 0; i < code.Length; i++)
            {
                int currentCity = code[i];
                int index = Array.IndexOf(code2, currentCity);

                int nextIndex = (i == code.Length - 1) ? 0 : (i + 1);
                int nextIndex2 = (index == code.Length - 1) ? 0 : (index + 1);

                if (code[nextIndex] != code2[nextIndex2])
                    distance++;
            }

            if (_symmetric)
            {
                distanceRev = 0;
                for (int i = code.Length - 1; i >= 0; i--)
                {
                    int currentCity = code[i];
                    int index = Array.IndexOf(code2, currentCity);

                    int nextIndex = (i == 0) ? code.Length - 1 : (i - 1);
                    int nextIndex2 = (index == 0) ? code.Length - 1 : (index - 1);

                    if (code[nextIndex] != code2[nextIndex2])
                        distanceRev++;
                }
            }

            return distance < distanceRev ? distance : distanceRev;
        }
    }
}
