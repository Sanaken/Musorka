using Population;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgorithm
{
    public class X_Algorithm
    {
        bool ok = true;
        AMatrixWrapper mMWrapper;
        public X_Algorithm(string filename)
        {
            try
            {
                mMWrapper = new SalesmanMatrixWrapper(filename + ".txt");
            }
            catch (WrongMatrixException e)
            {
                Console.WriteLine(e);
                ok = false;
            }
        }

        public Person Run()
        {
            if (!ok)
            {
                Console.WriteLine("Not OK!");
                return null;
            }

            int N = mMWrapper.Matrix.GetLength(0);
            double[,] xMatrix = new double[N, N];

            for (int i = 0; i < N; i++) 
            {
                xMatrix[i, i] = double.MaxValue;
                for (int j = 0; j < N; j++)
                {
                    if (i == j)
                        continue;

                    double penance = -1 * (mMWrapper.Matrix[i, j]);
                    for (int k = 0; k < i; k++) { penance += mMWrapper.Matrix[k, j]; }
                    for (int k = i + 1; k < N; k++) { penance += mMWrapper.Matrix[k, j]; }
                    for (int k = 0; k < j; k++) { penance += mMWrapper.Matrix[i, k]; }
                    for (int k = j + 1; k < N; k++) { penance += mMWrapper.Matrix[i, k]; }

                    xMatrix[i, j] = penance;
                }
            }

            List<int> takenI = new List<int>(), takenJ = new List<int>();
            for (int k = 0; k < N; k++)
            {
                int iToTake = 0, jToTake = 0;
                double minPenance = double.MaxValue;

                for (int i = 0; i < N; i++)
                {
                    if (takenI.Contains(i))
                        continue;

                    for (int j = 0; j < N; j++)
                    {
                        if (takenJ.Contains(j))
                            continue;

                        if (xMatrix[i, j] < minPenance)
                        {
                            iToTake = i;
                            jToTake = j;
                            minPenance = xMatrix[i, j];
                        }
                    }
                }

                takenI.Add(iToTake);
                takenJ.Add(jToTake);
            }

            int[] retTrans = new int[N];
            int indexOf = 0;
            for (var aldrkh = 0; aldrkh < N; aldrkh++)
            {
                retTrans[aldrkh] = takenJ[indexOf];
                indexOf = takenI.IndexOf(retTrans[aldrkh]);
                Console.Write($"{retTrans[aldrkh]} ");
            }

            Console.Write(mMWrapper.FitnessFunction(new Person(retTrans)));
            return new Person(retTrans);
        }
    }
}
