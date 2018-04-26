using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Population;

namespace GenAlgorithm.Crossover
{
    public class OXCrossOver : IStrategyCrossover
    {
        public Person CrossingOver(Person person1, Person person2)
        {
            int size = person1.GetCode().Length;
            int[] retCode = new int[size];
            int[] code1 = person1.GetCode();
            int[] code2 = person2.GetCode();

            Random rand = new Random();

            int firstPoint = rand.Next(2 * size / 3);
            int secondPoint = firstPoint + (size / 3);

            for(int i = firstPoint; i < secondPoint; i++)
            {
                retCode[i] = code1[i];
            }

            int counter = 0;
            for (int i = 0; i < size; i++)
            {
                bool flag = true;
                for (int j = firstPoint; j < secondPoint; j++) 
                {
                    if (retCode[j] == code2[i])
                        flag = false;
                }
                if (flag)
                {
                    if (counter == firstPoint)
                        counter = secondPoint;
                    retCode[counter] = code2[i];
                    counter++;
                }
            }
            return new Person(retCode);
        }

        public override string ToString()
        {
            return "OX-Crossover";
        }
    }
}
