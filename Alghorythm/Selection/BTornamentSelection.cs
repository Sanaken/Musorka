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
    class BTornamentSelection : IStrategySelection
    {
        public Person[] Selection(Person[] persons, AMatrixWrapper mWrapper)
        {
            int resultPersonsSize = persons.Length / 2;
            Person[] resultPersons = new Person[resultPersonsSize];

            for (int i = 0; i < resultPersonsSize; i++)
            {
                if (mWrapper.FitnessFunction(persons[i]) < mWrapper.FitnessFunction(persons[i + resultPersonsSize]))
                {
                    resultPersons[i] = persons[i];
                }
                else
                    resultPersons[i] = persons[i + resultPersonsSize];
            }

            return resultPersons;
        }

        public override string ToString()
        {
            return "BTornament-Selection";
        }
    }
}
