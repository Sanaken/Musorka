using Population;

namespace GenAlgorithm
{
    public interface IStrategyGeneration
    {
        Person[] GeneratePop(AMatrixWrapper wrapper, int capacity);
    }
}
