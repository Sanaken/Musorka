using Population;

namespace GenAlgorithm
{
    public abstract class AMatrixWrapper
    {
        protected int mMatrixSize;
        protected double[,] mMatrix;
        public double[,] Matrix => mMatrix;

        // Min-max flag (true - max, false - min)
        //public bool mMinMax { get; protected set; }
        // Int state code
        public int mState { get; protected set; }

        public AMatrixWrapper(string filename)
        {
            // Try to read matrix from file...
            mState = ReadMatrix(filename);
            if (mState != 0)
                // If unable to read as matrix, try to read as coordinates...
                mState = ReadCoordinates(filename);
        }
        protected abstract int ReadMatrix(string filename);
        protected abstract int ReadCoordinates(string filename);
        public abstract bool MatrixIsCorrect();
        public abstract double FitnessFunction(Person person);
        public abstract int Distance(Person p1, Person p2);
    }
}
