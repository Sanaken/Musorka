using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Population;

namespace GenAlgorithm.Mutation
{
    public class PointMutation : AStrategyMutation
    {
        protected override void DirectMutation(Person person)
        {
            int[] code = person.GetCode();

            Random rand = new Random();
            int index = rand.Next(0, code.Length - 1);
            int buf = code[index];
            code[index] = code[index + 1];
            code[index + 1] = buf;

            person = new Person(code);
        }
        public override string ToString()
        {
            return "Point-Mutation";
        }
    }
}
