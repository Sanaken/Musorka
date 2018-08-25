using System;
using Population;

namespace GenAlgorithm
{
    public class InversionMutator : IMutator
    {
        public void Mutate(Person person)
        {
            int[] code = person.GetCode();

            Random rand = new Random();
            int index1 = rand.Next(0, code.Length/2);
            int index2 = rand.Next(code.Length / 2 + 1, code.Length);

            for (int i = 0; i < (index2 - index1 + 1) / 2; i++) 
            {
                int buf = code[index1 + i];
                code[index1 + i] = code[index2 - i];
                code[index2 - i] = buf;
            }
            person = new Person(code);
        }

        public override string ToString()
        {
            return "Inversion-Mutation";
        }
    }
}
