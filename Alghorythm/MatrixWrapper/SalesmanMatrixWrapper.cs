using Population;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenAlgorithm
{
    /**
     * A wrapper for Salesman problem matrix
     */
    public class SalesmanMatrixWrapper : AMatrixWrapper
    {
        // 2-x Dimension for coordinate...
        private const int numberOfCoordinateDimensions = 2;
        private bool _symmetric = true;
        public SalesmanMatrixWrapper(string filename) : base(filename)
        {
            if (mState != 0)
            {
                mState = ReadCoordinates(filename);
            }

            for (int i = 0; i < mMatrixSize; i++)
                for (int j = 0; j < mMatrixSize; j++)
                    if ((i != j) && (mMatrix[i, j] != mMatrix[j, i]))
                    {
                        _symmetric = false;
                        return;
                    }                       
        }
        /**
         * Returns: 
         * -1: Error while working with files
         * -2: Wrong type of matrix
         * -3: Invalid symbols (allowable separators: space, tab, \r, \n)
         *  0: Matrix has been readed correctly
         **/
        protected override int ReadMatrix(string fileName)
        {
            mMatrixSize = 0;
            FileStream file = null;
            StreamReader streamReader;
            try
            {
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(file);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e + " (in mWrapper)");
                return -1;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e + " (in mWrapper)");
                return -1;
            }

            List<string> readedText = new List<string>();
            do
            {
                readedText.Add(streamReader.ReadLine());
                mMatrixSize++;
            }
            while (readedText.Last() != null);
            mMatrixSize--;
            mMatrix = new double[mMatrixSize, mMatrixSize];

            for(int i = 0; i < mMatrixSize; i++)
            {
                // Split by separators
                string[] line = readedText[i].Split(' ', '\t', '\r', '\n');
                // Remove all empty strings received due to duplicate separators
                line = Array.FindAll(line, x => x != "");

                if (line.Length != mMatrixSize)
                {
                    streamReader.Close();
                    return -2;
                }
                for(int j = 0; j < mMatrixSize; j++)
                {
                    // LMAO
                    line[j] = line[j].Replace('.', ',');
                    if (!double.TryParse(line[j], out mMatrix[i, j])) {
                        streamReader.Close();
                        return -3;
                    }
                }
            }
            streamReader.Close();
            return 0;
        }
        /**
         * Returns: 
         * -1: Error while working with files
         * -2: Wrong type of coordinate fields
         * -3: Invalid symbols (allowable separators: space, tab, \r, \n)
         *  0: Matrix has been created correctly
         **/
        protected override int ReadCoordinates(string fileName)
        {
            mMatrixSize = 0;
            FileStream file = null;
            StreamReader streamReader;
            try
            {
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(file);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e + " (in mWrapper)");
                return -1;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e + " (in mWrapper)");
                return -1;
            }

            List<string> readedText = new List<string>();
            do
            {
                readedText.Add(streamReader.ReadLine());
                mMatrixSize++;
            }
            while (readedText.Last() != null);
            mMatrixSize--;

            double[,] CoordBuffer = new double[numberOfCoordinateDimensions, mMatrixSize];
            for(int i = 0; i < mMatrixSize; i++)
            {
                // Split by separators
                string[] line = readedText[i].Split(' ', '\t', '\r', '\n');
                // Remove all empty strings received due to duplicate separators
                line = Array.FindAll(line, x => x != "");

                if(line.Length!= numberOfCoordinateDimensions)
                {
                    streamReader.Close();
                    return 2;
                }

                for (int j = 0; j < numberOfCoordinateDimensions; j++)
                {
                    // LMAO
                    line[j] = line[j].Replace('.', ',');
                    if (!double.TryParse(line[j], out CoordBuffer[j, i]))
                    {
                        streamReader.Close();
                        return -3;
                    }
                }
            }

            mMatrix = new double[mMatrixSize, mMatrixSize];
            for (int i = 0; i < mMatrixSize; i++)
            {
                for (int j = 0; j < mMatrixSize; j++)
                {
                    if (i == j)
                        mMatrix[i, j] = 0;
                    else if (i > j)
                        mMatrix[i, j] = mMatrix[j, i];
                    else
                    {
                        mMatrix[i, j] = Math.Sqrt(Math.Pow((CoordBuffer[0, i] - CoordBuffer[0, j]), 2) +
                            Math.Pow((CoordBuffer[1, i] - CoordBuffer[1, j]), 2));
                    }
                }
            }

            streamReader.Close();
            return 0;
        }
        public override bool MatrixIsCorrect()
        {
            for(int i = 0; i < mMatrixSize; i++)
                for(int j = 0; j < mMatrixSize; j++)
                {
                    if (((i == j) && (mMatrix[i, j] != 0)) || ((i != j) && (mMatrix[i, j] == 0)))
                        return false;        
                }
            return true;
        }
        public override double FitnessFunction(Person person)
        {
            double result = 0;
            int[] code = person.GetCode();
            for (int i = 1; i < code.Count(); i++)
            {
                result += mMatrix[code[i - 1], code[i]];
            }
            return result + mMatrix[code[0], code.Last()];
        }

        public void PrintPers(Person p)
        {
            Console.Write(FitnessFunction(p) + "---");
            p.PrintPers();
        }

        public override int Distance(Person p1, Person p2)
        {
            int distance = 0, distanceRev = int.MaxValue;
            int[] code = p1.GetCode();
            int[] code2 = p2.GetCode();

            for (int i = 0; i < code.Length; i++)
            {
                int currentCity = code[i];
                int index = Array.IndexOf(code2, currentCity);

                int nextIndex = (i == code.Length - 1) ? 0 : (i + 1);
                int nextIndex2 = (index == code.Length - 1) ? 0 : (index + 1);

                if (code[nextIndex] != code2[nextIndex2])
                    distance++;
            }

            if (_symmetric)
            {
                distanceRev = 0;
                for (int i = code.Length - 1; i >= 0; i--)
                {
                    int currentCity = code[i];
                    int index = Array.IndexOf(code2, currentCity);

                    int nextIndex = (i == 0) ? code.Length - 1 : (i - 1);
                    int nextIndex2 = (index == 0) ? code.Length - 1 : (index - 1);

                    if (code[nextIndex] != code2[nextIndex2])
                        distanceRev++;
                }
            }

            return distance < distanceRev ? distance : distanceRev;
        }
    }
}
