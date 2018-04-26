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
    class RangeSelection : IStrategySelection
    {
        public Person[] Selection(Person[] persons, AMatrixWrapper mWrapper)
        {
            Person[] retPersons = new Person[persons.Length / 2];
            double[] fitArray = new double[persons.Length];

            for (int i = 0; i < persons.Length; i++)
            {
                fitArray[i] = mWrapper.FitnessFunction(persons[i]);
            }

            double buf = 0;
            Person persBuf;
            for (int i = 0; i < fitArray.Length; i++)
            {
                for (int j = i + 1; j < fitArray.Length; j++)
                {
                    if (fitArray[i] > fitArray[j])
                    {
                        buf = fitArray[i];
                        fitArray[i] = fitArray[j];
                        fitArray[j] = buf;
                        persBuf = persons[i];
                        persons[i] = persons[j];
                        persons[j] = persBuf;
                    }
                }
            }

            double avg = 0;
            for (int i = 0; i < fitArray.Length / 2; i++)
            {
                avg += fitArray[i];
            }
            avg /= (fitArray.Length / 2);

            // Now fitArray is range array:
            for(int i = 0; i < fitArray.Length / 2; i++)
            {
                fitArray[i] = avg / fitArray[i];
            }

            avg = 0;
            // Calc new avg:
            for (int i = 0; i < fitArray.Length / 2; i++)
            {
                avg += fitArray[i];
            }
            avg /= fitArray.Length / 2;

            // Scaling range array:
            for (int i = 0; i < fitArray.Length / 2; i++)
            {
                fitArray[i] *= avg;
            }

            int counter = 0;
            for (int i = 0; i < fitArray.Length / 2; i++)
            {
                for (int j = 0; j < (int)fitArray[i]; j++) 
                {
                    retPersons[counter] = persons[i];
                    counter++;
                }
            }

            for (int i = 0; i < fitArray.Length / 2 - counter; i++)
            {
                retPersons[counter + i] = persons[i];
            }
            return retPersons;
        }

        public override string ToString()
        {
            return "Range-Selection";
        }
    }
}
