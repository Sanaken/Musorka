using GenAlgorithm.Crossover;
using GenAlgorithm.Generation;
using GenAlgorithm.Selection;
using GenAlgorithm.Mutation;
using Population;
using System;

// I need to save best person in every generations.

namespace GenAlgorithm
{
    public class Algorithm
    {
        // the selection of operators via roulette method:
        double[] mCrossRoulette = new double[3];
        double[] mMutRoulette = new double[3];
        double[] mSelRoulette = new double[3];

        IStrategyCrossover[] mCrossOperators = { new OXCrossOver(), new CXCrossover(), new PMXCrossover() };
        AStrategyMutation[] mMutOperators = { new SaltationMutation(), new InversionMutation(), new PointMutation() };
        IStrategySelection[] mSelOperators = { new BTornamentSelection(), new RouletteSelection(), new RangeSelection() };

        int[] mCurrentOperators = new int[3];
        AMatrixWrapper mMWrapper;
        Random mRandomizer;

        // The EGA settings:
        int mPopulationCapacity;
        int mIterationNumber;

        // The EGA operators:
        IStrategyGeneration mGenOperator;
        IStrategyCrossover mCrossOperator;
        AStrategyMutation mMutOperator;
        IStrategySelection mSelOperator;

        // The EGA population & additional 2x-capacity buffer population
        Person[] mMainPopulation;
        Person[] mBufferPopulation;

        // The best person in current generation (supporting the elite scheme)
        Person mBestPerson = null, mPreBestPerson = null;
        double mDiversity = 0, mPreDiversity = 0;
        double mPreAveFit = 0, mAveFit = 0;

        public Algorithm(int iterationNumber, int populationCapacity)
        {
            mMWrapper = new SalesmanMatrixWrapper("Matrix.txt");
            mRandomizer = new Random();
            mIterationNumber = iterationNumber;
            mPopulationCapacity = populationCapacity;

            mGenOperator = new RouletteGeneration();

            // Generating start population:
            mMainPopulation = mGenOperator.GeneratePop(mMWrapper, mPopulationCapacity);
            mBufferPopulation = new Person[2 * populationCapacity];

            // ... for statistics and something else
            SetBestPerson();
            SetAveFintess();
            SetDiversity();

            for (int i = 0; i < mCrossRoulette.Length; i++)
            {
                mCrossRoulette[i] = mAveFit;
                mMutRoulette[i] = mAveFit;
                mSelRoulette[i] = mAveFit;
            }
        }

        public void Run()
        {
            // Main cycle of EGA
            for (int iteration = 0; iteration < mIterationNumber; iteration++)
            {
                ChangeOperators();

                for (int i = 0; i < mPopulationCapacity; i++)
                {
                    // Parents take part on selection
                    mBufferPopulation[i] = mMainPopulation[i];


                    // According to the outbreeding scheme, each pair is selected by a pair
                    int indexOfPair = 0;
                    int hammingDistMax = 0;

                    for (int j = 1; j < mPopulationCapacity; j++)
                    {
                        int hammingDistBuf = mMainPopulation[i].HammingDistance(mMainPopulation[(i + j) % mPopulationCapacity]);

                        if (hammingDistBuf > hammingDistMax)
                        {
                            indexOfPair = (i + j) % mPopulationCapacity;
                            hammingDistMax = hammingDistBuf;
                        }
                    }

                    // Childs as a results of crossing-over take pars on selection too
                    mBufferPopulation[mPopulationCapacity + i] = mCrossOperator.CrossingOver(
                        mMainPopulation[i], mMainPopulation[indexOfPair]);
                    // Childs can mutate with a given probablity
                    mMutOperator.Mutation(mBufferPopulation[mPopulationCapacity + i]);
                }

                mMainPopulation = mSelOperator.Selection(mBufferPopulation, mMWrapper);

                SetBestPerson();
                SetAveFintess();
                SetDiversity();

                mCrossRoulette[mCurrentOperators[0]] += AddPointsToCrossOverRoulette();
                if (mCrossRoulette[mCurrentOperators[0]] < 0)
                {
                    mCrossRoulette[mCurrentOperators[0]] = 0;
                }
                mSelRoulette[mCurrentOperators[2]] += AddPointsToSelectionRoulette();
                if (mSelRoulette[mCurrentOperators[2]] < 0)
                {
                    mSelRoulette[mCurrentOperators[2]] = 0;
                }
                PrintStatistics(iteration + 1);

                /*foreach (Person p in mMainPopulation)
                {
                    p.PrintPers();
                    Console.Write(mMWrapper.FitnessFunction(p));
                }*/

                if (mDiversity == 0)
                {
                    Console.WriteLine("Алгоритм сошелся!");
                    return;
                }
            }
        }

        private double AddPointsToSelectionRoulette()
        {
            // min-crit
            return (mPreAveFit - mAveFit) + (mDiversity - mPreDiversity) * mAveFit;
        }

        private double AddPointsToCrossOverRoulette()
        {
            return mMWrapper.FitnessFunction(mPreBestPerson) - mMWrapper.FitnessFunction(mBestPerson);
        }

        private void SetBestPerson()
        {
            mPreBestPerson = mBestPerson;
            mBestPerson = mMainPopulation[0];
            double fitBuffer = mMWrapper.FitnessFunction(mMainPopulation[0]);

            foreach(Person pers in mMainPopulation)
            {
                if (fitBuffer > mMWrapper.FitnessFunction(pers))
                    mBestPerson = pers;
            }
        }

        private void SetDiversity()
        {
            mPreDiversity = mDiversity;

            int originPersCount = 1;
            for (int i = 1; i < mPopulationCapacity; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (mMainPopulation[i].IsEqual(mMainPopulation[j]))
                    {
                        originPersCount--;
                        break;
                    }
                    originPersCount++;
                }
            }

            mDiversity = originPersCount / mPopulationCapacity;
        }

        private void SetAveFintess()
        {
            mPreAveFit = mAveFit;

            double sumFit = 0;
            foreach(Person pers in mMainPopulation)
            {
                sumFit += mMWrapper.FitnessFunction(pers);
            }
            mAveFit = sumFit / mPopulationCapacity;
        }

        // Roulette function returns index of massive
        private int OperatorsRoulette(params double[] probablities)
        {
            double[] strit = new double[probablities.Length + 1];

            strit[0] = 0;
            for(int i = 0; i < probablities.Length; i++)
            strit[i + 1] = strit[i] + probablities[i];

            int rPoint = mRandomizer.Next((int)strit[strit.Length - 1]);
            for (int i = 0; i < probablities.Length; i++) 
            {
                if (rPoint < strit[i + 1])
                {
                    return i;
                }
            }

            return probablities.Length - 1;
        }

        // Changes operators via roulette-operators function
        private void ChangeOperators()
        {
            mCurrentOperators[0] = OperatorsRoulette(mCrossRoulette);
            mCurrentOperators[1] = OperatorsRoulette(mMutRoulette);
            mCurrentOperators[2] = OperatorsRoulette(mSelRoulette);
            mCrossOperator = mCrossOperators[mCurrentOperators[0]];
            mMutOperator = mMutOperators[mCurrentOperators[1]];
            mSelOperator = mSelOperators[mCurrentOperators[2]];
        }

        private void PrintStatistics(int iteration)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Iteration: " + iteration);
            Console.WriteLine("Average fitness: " + mAveFit);
            Console.WriteLine("Best person: " + mMWrapper.FitnessFunction(mBestPerson) + " ");
            mBestPerson.PrintPers();
            Console.WriteLine("Used operators: " + mCrossOperator + ", " + 
                mMutOperator + ", " + mSelOperator);
        }
    }
}