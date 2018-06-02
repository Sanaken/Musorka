using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Population;

namespace GenAlgorithm.Selection
{
    /** 
     * This population selector just for minimum critery problems
     * (like salesman)
     * Returns descendants population, that contains a half person count of input
     */
    public class RouletteSelection : IStrategySelection
    {
        public Person[] Selection(Person[] persons, AMatrixWrapper mWrapper)
        {
            Random rand = new Random();
            int resultPersonsSize = persons.Length / 2;
            Person[] resultPersons = new Person[resultPersonsSize];

                for (int i = 0; i < resultPersonsSize; i++)
                {
                    double[] strit = new double[persons.Length];
                    double sum = 0;

                    for (int j = 0; j < persons.Length; j++)
                    {
                        strit[j] = mWrapper.FitnessFunction(persons[j]);
                        sum += strit[j];
                    }

                    double[] revStrit = new double[persons.Length + 1];
                    revStrit[0] = 0;
                    for (int j = 0; j < persons.Length; j++)
                    {
                        revStrit[j + 1] = (sum / strit[j]) * (sum / strit[j]) + revStrit[j];
                    }

                    int randPoint = rand.Next((int)revStrit[persons.Length]);
                    for (int j = 0; j < persons.Length; j++)
                    {
                        if (randPoint <= revStrit[j + 1])
                        {
                            resultPersons[i] = persons[j];
                            break;
                        }
                    }
                }
           
           return resultPersons;
        }

        public override string ToString()
        {
            return "Roulette-Selection";
        }
    }
}
