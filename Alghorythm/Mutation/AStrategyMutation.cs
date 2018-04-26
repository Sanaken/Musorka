using Population;
using System;

namespace GenAlgorithm
{
    public abstract class AStrategyMutation
    {
        // Describes a mutation chance when crossed persons (/1000)
        int mMutationChance;
        Random rand;
        public AStrategyMutation(int MutationChance = 1)
        {
            if ((MutationChance >= 0) && (MutationChance <= 1000))
            {
                mMutationChance = MutationChance;
            }
            else mMutationChance = 1;
            rand = new Random();
        }
        public void Mutation(Person person)
        {
            int randValue = rand.Next(1000);
            if (randValue < mMutationChance)
                DirectMutation(person);
        }
        protected abstract void DirectMutation(Person person);
    }
}
