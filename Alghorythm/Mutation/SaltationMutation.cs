using System;
using Population;

namespace GenAlgorithm.Mutation
{
    public class SaltationMutation : AStrategyMutation
    {
        public SaltationMutation(int MutationChance = 1) : base(MutationChance)
        {
        }

        protected override void DirectMutation(Person person)
        {
            int[] code = person.GetCode();

            Random rand = new Random();

            for (int i = 0; i < (code.Length / 5); i++)
            {
                int index1 = rand.Next(0, code.Length);
                int index2 = rand.Next(0, code.Length);
                int buf = code[index1];
                code[index1] = code[index2];
                code[index2] = buf;
            }

            person = new Person(code);
        }
        public override string ToString()
        {
            return "Saltation-Mutation";
        }
    }
}
