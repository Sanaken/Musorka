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
            this.code = (int[])code.Clone();
        }
        
        public override string ToString()
        {
            string retStr = "";
            foreach (int i in code)
                retStr += i + " ";

            return retStr;
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
    }
}
