using GenAlgorithm.Crossover;
using GenAlgorithm.Generation;
using GenAlgorithm.Selection;
using GenAlgorithm.Mutation;
using Population;
using System;
using System.IO;

namespace GenAlgorithm
{
    public class Algorithm
    {
        //       ~filename_output.txt  ~filename
        static string mOutputFileName, mFileName;
        StreamWriter sw;

        // the selection of operators via roulette method:
        double[] mCrossRoulette = new double[3];
        double[] mMutRoulette = new double[3];

        // Mutation chance (in %)
        int mMutationChance = 10;
        // Period of using Roulette selection between using
        // BTournament selection (in iterations)
        const double CRITICAL_DIVERSITY = 0.1;
        const int ROULETTE_SEL_MAX_PERIOD = 5;
        int mRouletteSelectionPeriod = ROULETTE_SEL_MAX_PERIOD;

        IStrategyCrossover[] mCrossOperators = { new OXCrossOver(), new CXCrossover(), new PMXCrossover() };
        IMutator[] mMutOperators = { new SaltationMutator(), new InversionMutator(), new PointMutator() };
        IStrategySelection[] mSelOperators = { new BTornamentSelection(), new RouletteSelection()};

        int[] mCurrentOperators = new int[3];
        AMatrixWrapper mMWrapper;
        Random mRandomizer;

        // The EGA settings:
        int mPopulationCapacity;
        int mIterationNumber;

        // The EGA operators:
        IStrategyGeneration mGenOperator;
        IStrategyCrossover mCrossOperator;
        IMutator mMutOperator;
        IStrategySelection mSelOperator;

        // The EGA population & additional 2x-capacity buffer population
        Person[] mMainPopulation;
        Person[] mBufferPopulation;

        // The best person in current generation (supporting the elite scheme)
        Person mBestPerson = null, mPreBestPerson = null;
        double mDiversity = 0, mPreDiversity = 0;
        double mPreAveFit = 0, mAveFit = 0;

        public Algorithm(int iterationNumber, int populationCapacity, string filename)
        {
            // Initializing of File logging
            mOutputFileName = filename + "_output.txt";
            sw = new StreamWriter(new FileStream("output/" + mOutputFileName, FileMode.Create, FileAccess.Write));

            mFileName = filename;
            mMWrapper = new SalesmanMatrixWrapper(filename + ".txt");
            if (mMWrapper.mState != 0)
            {
                Console.WriteLine("No matrix (code " + mMWrapper.mState + ")");
                return;
            }
            mRandomizer = new Random();
            mIterationNumber = iterationNumber;
            mPopulationCapacity = populationCapacity;

            mSelOperator = new RouletteSelection();
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
                mCrossRoulette[i] = mMWrapper.FitnessFunction(mBestPerson);
                mMutRoulette[i] = mAveFit;
            }
        }

        public void Run()
        {
            if (mMWrapper.mState != 0)
            {
                Console.WriteLine("Matrix is invalid, run is impossible!");
                return;
            }
            // Print initial statistics for generated population:
            PrintStatistics(0);
        
            // Main cycle of EGA
            for (int iteration = 0; iteration < mIterationNumber; iteration++)
            {
                ChangeOperators();
            // Crossing-over cycle...
            for (int i = 0; i < mPopulationCapacity; i++)
                {
                    // Parents take part on selection
                    mBufferPopulation[i] = mMainPopulation[i];

                    // According to the outbreeding scheme, each pair is selected by a pair
                    int indexOfPair = 0;
                    int distMax = 0;

                    for (int j = 1; j < mPopulationCapacity; j++)
                    {
                        int distBuf = mMWrapper.Distance(mMainPopulation[i], 
                            mMainPopulation[(i + j) % mPopulationCapacity]);

                        if (distBuf > distMax)
                        {
                            indexOfPair = (i + j) % mPopulationCapacity;
                            distMax = distBuf;
                        }
                    }

                    // Childs as a results of crossing-over take pars on selection too
                    mBufferPopulation[mPopulationCapacity + i] = 
                        CrossAddPoints(mMainPopulation[i], mMainPopulation[indexOfPair]);

                    // Childs can mutate with a given probablity
                    if (mRandomizer.Next(100) < mMutationChance) 
                    MutateAndAddPoints(mBufferPopulation[mPopulationCapacity + i]);
                }

                mMainPopulation = mSelOperator.Selection(mBufferPopulation, mMWrapper);

                // If selection is B-Tournament, The champion is guaranteed to move into the next generation
                if(mSelOperator is RouletteSelection)
                    SaveChampion();

                SetBestPerson();
                SetAveFintess();
                SetDiversity();

                PrintStatistics(iteration + 1);                

                if (mDiversity <= (double)1/mPopulationCapacity)
                {
                    Console.WriteLine("Алгоритм сошелся!");
                    PrintOPRouletteState();
                    Console.WriteLine(LoadBestTour());
                    return;
                }       
            }

            Console.WriteLine("Алгоритм завершен");
            PrintOPRouletteState();
            Console.WriteLine(LoadBestTour());
        }

        private void PrintOPRouletteState()
        {
            double sumBuf = 0;
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Состояние рулеток:");
            Console.WriteLine("Мутация: ");
            foreach (double d in mMutRoulette)
                sumBuf += d;
            for(int i = 0; i < mMutRoulette.Length; i++)
            {
                Console.WriteLine(mMutOperators[i] + ": {0:0.00}%", mMutRoulette[i] / sumBuf * 100);
            }
            
            sumBuf = 0;
            Console.WriteLine("Кроссовер: ");
            foreach (double d in mCrossRoulette)
                sumBuf += d;
            for (int i = 0; i < mCrossRoulette.Length; i++)
            {
                Console.WriteLine(mCrossOperators[i] + ": {0:0.00}%", mCrossRoulette[i] / sumBuf * 100);
            }
            Console.WriteLine("-----------------------------------");
        }

        private Person CrossAddPoints(Person p1, Person p2)
        {
            var pRet = mCrossOperator.CrossingOver(p1, p2);
            var retv = mMWrapper.FitnessFunction(pRet);
            var v1 = mMWrapper.FitnessFunction(p1);
            var v2 = mMWrapper.FitnessFunction(p2);

            var points = (v1 + v2) / 2 - retv;

            for (int iter = 0; iter < mCrossRoulette.Length; iter++)
            {
                if (iter != mCurrentOperators[0])
                {
                    mCrossRoulette[iter] -= points;
                    if (mCrossRoulette[iter] < 0)
                    {
                        mCrossRoulette[iter] = 0;
                    }
                }
            }

            mCrossRoulette[mCurrentOperators[0]] += points;
            return pRet;
        }

        //Tries to load the best pers from ~filename_tour.txt
        private string LoadBestTour()
        {
            FileStream file = null;
            StreamReader streamReader;
            try
            {
                file = new FileStream(mFileName + "_tour.txt", FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(file);
            }
            catch (FileNotFoundException)
            {
                return "File with best tour is not found";
            }
            catch (ArgumentException)
            {
                return "An error while work with best tour file has occured";
            }

            int[] codeBPers = new int[mMWrapper.GetSize()];
            for(int i=0;i< mMWrapper.GetSize(); i++)
            {
                if(!int.TryParse(streamReader.ReadLine(), out codeBPers[i]))
                {
                    return "The best tour file has corrupted!";
                }
                codeBPers[i]--;
            }

            return "The best tour ffunction: " + mMWrapper.FitnessFunction(new Person(codeBPers));
        }
        private void PrintFinalPop(string exitReason)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("--------Конечная популяция!--------");
            Console.WriteLine("-----------------------------------");
            foreach (Person p in mMainPopulation)
            {
                Console.Write(mMWrapper.FitnessFunction(p) + ": ");
                p.PrintPers();
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("---------" + exitReason + "---------");
            Console.WriteLine("-----------------------------------");
        }

        private double AddPointsToSelectionRoulette()
        {
            // min-crit
            return (mPreAveFit - mAveFit) + (mDiversity - mPreDiversity)*Math.Sqrt(mAveFit);
        }

        private void MutateAndAddPoints(Person pers)
        {
            Person buf = new Person(pers.GetCode());
            mMutOperator.Mutate(pers);
            double dif = mMWrapper.FitnessFunction(buf) -
                mMWrapper.FitnessFunction(pers);

            if (!buf.IsEqual(pers))
            {
                for (int iter = 0; iter < mCrossRoulette.Length; iter++)
                {
                    if (iter != mCurrentOperators[1])
                    {
                        mMutRoulette[iter] -= dif;
                        if (mMutRoulette[iter] < 0)
                        {
                            mMutRoulette[iter] = 0;
                        }
                    }

                    mMutRoulette[mCurrentOperators[1]] += dif;
                    if (mMutRoulette[mCurrentOperators[1]] < 0)
                    {
                        mMutRoulette[mCurrentOperators[1]] = 0;
                    }
                }
            }
        }

        // Finds the best person from buffer population to implement 
        // save champion mechanism via substitution the worst person in
        // main population by champion (if champion not in main population yet)

        private void SaveChampion()
        {
            Person champion = mBufferPopulation[0];
            double fitBuffer = mMWrapper.FitnessFunction(champion);

            // Here fitbuffer - buffer for best fitness (fit of champion)
            foreach (Person pers in mBufferPopulation)
            {
                if (fitBuffer > mMWrapper.FitnessFunction(pers))
                {
                    champion = pers;
                    fitBuffer = mMWrapper.FitnessFunction(pers);
                }
            }

            int indexOfReplaceable = 0;
            // Here fitbuffer - buffer for worst fitness (fit of replaceable pers)
            for(int i=0;i<mMainPopulation.Length;i++)
            {
                if (champion == mMainPopulation[i])
                {
                    return;
                }
                else
                {
                    if (fitBuffer < mMWrapper.FitnessFunction(mMainPopulation[i]))
                    {
                        indexOfReplaceable = i;
                        fitBuffer = mMWrapper.FitnessFunction(mMainPopulation[i]);
                    }
                }
            }

            mMainPopulation[indexOfReplaceable] = champion;
        }
        private void SetBestPerson()
        {
            mPreBestPerson = mBestPerson;
            mBestPerson = mMainPopulation[0];
            double fitBuffer = mMWrapper.FitnessFunction(mBestPerson);

            foreach(Person pers in mMainPopulation)
            {
                if (fitBuffer > mMWrapper.FitnessFunction(pers))
                {
                    mBestPerson = pers;
                    fitBuffer = mMWrapper.FitnessFunction(pers);
                }
            }
        }

        private void SetDiversity()
        {
            mPreDiversity = mDiversity;

            int originPersCount = 0;
            for (int i = 1; i < mPopulationCapacity; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (mMainPopulation[i].IsEqual(mMainPopulation[j]))
                    {
                        originPersCount--;
                        break;
                    }            
                }
                originPersCount++;
            }

            mDiversity = (double) originPersCount / mPopulationCapacity;
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

            while(strit[strit.Length - 1] > int.MaxValue)
            {
                for(int i = 1; i < strit.Length - 1; i++)
                {
                    strit[i] /= 2;
                }
            }

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
            if (mDiversity < CRITICAL_DIVERSITY || mDiversity == mPreDiversity)
            {
                mSelOperator = mSelOperators[1];
            }
            else
            {
                mSelOperator = mSelOperators[0];
            }
            mCurrentOperators[0] = OperatorsRoulette(mCrossRoulette);
            mCurrentOperators[1] = OperatorsRoulette(mMutRoulette);
            mCrossOperator = mCrossOperators[mCurrentOperators[0]];
            mMutOperator = mMutOperators[mCurrentOperators[1]];
        }

        private void PrintStatistics(int iteration)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Iteration: " + iteration);
            Console.WriteLine("Average fitness: " + mAveFit);
            Console.WriteLine("Diversity: " + (mDiversity * 100) + "%");
            Console.WriteLine("Best person: " + mMWrapper.FitnessFunction(mBestPerson) + " ");
            mBestPerson.PrintPers();
            Console.WriteLine("Used operators: " + mCrossOperator + ", " + 
                mMutOperator + ", " + mSelOperator);

            

            sw.WriteLine("-----------------------------");
            sw.WriteLine("-----------------------------");
            sw.WriteLine("Iteration: " + iteration);
            sw.WriteLine("Average fitness: " + mAveFit);
            sw.WriteLine("Diversity: " + (mDiversity * 100) + "%");
            sw.WriteLine("Best person: " + mMWrapper.FitnessFunction(mBestPerson) + " ");
            sw.WriteLine(mBestPerson);
            sw.WriteLine("Used operators: " + mCrossOperator + ", " +
                mMutOperator + ", " + mSelOperator);
            sw.Close();
        }

        private bool WhetherMutate()
        {
            return (mRandomizer.Next(100) < mMutationChance);
        }
    }
}