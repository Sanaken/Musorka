using Population;

namespace GenAlgorithm
{
    public interface IStrategySelection
    {
        Person[] Selection(Person[] persons, AMatrixWrapper mWrapper);
    }
}
