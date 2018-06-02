using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Population
{
    public class Person
    {
        int[] code;
        public Person(int[] code)
        {
            this.code = code;
        }
        
        public int[] GetCode()
        {
            return code;
        }
        public void PrintPers()
        {
            foreach(int i in code)
            {
                Console.Write((i + 1) + " ");
            }
            Console.WriteLine();
        }
        public bool IsEqual(Person person)
        {
            int[] pCode = person.GetCode();
            if (code.Length != pCode.Length)
                return false;

            for(int i = 0; i < code.Length; i++)
            {
                if (code[i] != pCode[i])
                    return false;
            }
            return true;
        }

        public int HammingDistance(Person person)
        {
            int distance = 0;
            int[] code2 = person.GetCode();

            for(int i = 0; i < code.Length; i++)
            {
                int currentCity = code[i];
                int index = Array.IndexOf(code2, currentCity);

                int nextIndex = (i == code.Length - 1) ? 0 : (i + 1);
                int nextIndex2 = (index == code.Length - 1) ? 0 : (index + 1);

                if (code[nextIndex] != code2[nextIndex2])
                    distance++;
            }               
            return distance;
        }
    }
}
