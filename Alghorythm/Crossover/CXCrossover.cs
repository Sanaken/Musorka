using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Population;

namespace GenAlgorithm.Crossover
{
    public class CXCrossover : IStrategyCrossover
    {
        public Person CrossingOver(Person person1, Person person2)
        {
            int[] code1 = person1.GetCode();
            int[] code2 = person2.GetCode();
            int[] retCode = new int[code1.Length];

            for(int i = 0; i < retCode.Length; i++)
            {
                retCode[i] = -1;
            }

            bool quitFlag = true;
            int currentPoint = 0;

            for(int i = 0; i < retCode.Length; i++)
            {
                if (code1[i] != code2[i])
                {
                    currentPoint = i;
                }
            }

            while (quitFlag)
            {
                retCode[currentPoint] = code1[currentPoint];

                for (int i = 0; i < code2.Length; i++) 
                {
                    if (code2[i] == code1[currentPoint])
                    {
                        currentPoint = i;
                        break;
                    }
                }

                if (retCode.Contains(code1[currentPoint]))
                    quitFlag = false;
            }

            for (int i = 0; i < retCode.Length; i++)
            {
                if (retCode[i] == -1)
                {
                    retCode[i] = code2[i];
                }
            }
           
            return new Person(retCode);
        }

        public override string ToString()
        {
            return "CX-Crossover";
        }
    }
}
