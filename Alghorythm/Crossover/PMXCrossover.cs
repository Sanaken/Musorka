using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Population;

namespace GenAlgorithm.Crossover
{
    public class PMXCrossover : IStrategyCrossover
    {
        public Person CrossingOver(Person person1, Person person2)
        {
            int size = person1.GetCode().Length;
            int[] retCode = new int[size];
            for(int i = 0; i < size; i++)
            {
                retCode[i] = -1;
            }
            int[] code1 = person1.GetCode();
            int[] code2 = person2.GetCode();

            bool[] busy = new bool[size];
            Random rand = new Random();

            int firstPoint = rand.Next(2 * size / 3);
            int secondPoint = firstPoint + (size / 3);

            for (int i = firstPoint; i < secondPoint; i++)
            {
                retCode[i] = code1[i];
                busy[code1[i]] = true;
            }
            
            for (int i = 0; i < firstPoint; i++)
            {
                if (!busy[code2[i]])
                {
                    retCode[i] = code2[i];
                    busy[code2[i]] = true;
                }
            }
            for (int i = secondPoint; i < size; i++)
            {
                if (!busy[code2[i]])
                {
                    retCode[i] = code2[i];
                    busy[code2[i]] = true;
                }
            }
            for (int i = 0; i < firstPoint; i++)
            {
                if (retCode[i] == -1)
                for(int j = 0; j < size; j++)
                {
                    if (!busy[j])
                    {
                        retCode[i] = j;
                        busy[j] = true;
                        break;
                    }
                }
            }
            for (int i = secondPoint; i < size; i++)
            {
                if (retCode[i] == -1)
                    for (int j = 0; j < size; j++)
                    {
                        if (!busy[j])
                        {
                            retCode[i] = j;
                            busy[j] = true;
                            break;
                        }
                    }
            }

            return new Person(retCode);
        }

        public override string ToString()
        {
            return "PMX-Crossover";
        }
    }
}
