using Population;

namespace GenAlgorithm
{
    public abstract class AMatrixWrapper
    {
        protected int mMatrixSize;
        protected double[,] mMatrix;

        // Min-max flag (true - max, false - min)
        //public bool mMinMax { get; protected set; }
        // Int state code
        public int mState { get; protected set; }

        public AMatrixWrapper(string filename) { mState = ReadMatrix(filename); }
        protected abstract int ReadMatrix(string filename);
        public abstract bool MatrixIsCorrect();
        public abstract double FitnessFunction(Person person);
        public int GetSize()
        {
            return mMatrixSize;
        }
        public abstract int Distance(Person p1, Person p2);
        public double[,] GetMatrix() { return mMatrix; }
    }
}
