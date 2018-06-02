using Population;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgorithm
{
    public abstract class AMatrixWrapper
    {
        protected int mMatrixSize;
        protected double[,] mMatrix;

        // Min-max flag (true - max, false - min)
        //public bool mMinMax { get; protected set; }
        // Int state code
        public int mState { get; }

        public AMatrixWrapper(string filename) { mState = ReadMatrix(filename); }
        protected abstract int ReadMatrix(string filename);
        public abstract bool MatrixIsCorrect();
        public abstract double FitnessFunction(Person person);
        public int GetSize()
        {
            return mMatrixSize;
        }
        public double[,] GetMatrix() { return mMatrix; }
    }
}
