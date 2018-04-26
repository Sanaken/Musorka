using Population;

namespace GenAlgorithm
{
    public interface IStrategyCrossover
    {
        Person CrossingOver(Person person1, Person person2);
    }
}
