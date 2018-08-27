using Population;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenAlgorithm
{
    public class WrongMatrixException : Exception
    {
        public WrongMatrixException(string message) : base(message)
        {
        }

        public WrongMatrixException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    public abstract class AMatrixWrapper
    {
        // 2-x Dimension for coordinate...
        private const int DIM_COUNT = 2;
        protected double[,] mMatrix;
        public double[,] Matrix => mMatrix;

        public AMatrixWrapper(string filename)
        {
            var lines = ReadLines(filename);

            try { ReadMatrix(lines); }
            catch (WrongMatrixException e)
            {
                try { ReadCoordinates(lines); }
                catch (WrongMatrixException e2)
                {
                    throw new AggregateException(e, e2);
                }
            }

            if (!MatrixIsCorrect())
                throw new WrongMatrixException("Matrix is incorrect for current problem");
        }

        private List<string> ReadLines(string filename)
        {
            FileStream file = null;
            StreamReader streamReader;
            try
            {
                file = new FileStream(filename, FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(file);
            }
            catch (Exception e)
            {
                throw new WrongMatrixException("Error while reading lines from file: ", e);
            }

            List<string> readedText = new List<string>();
            do
            {
                readedText.Add(streamReader.ReadLine());
            }
            while (readedText.Last() != null);

            streamReader.Close();
            return readedText;
        }
        private void ReadMatrix(List<string> readedText)
        {        
            mMatrix = new double[readedText.Count, readedText.Count];

            for (int i = 0; i < readedText.Count; i++)
            {
                // Split by separators
                string[] line = readedText[i].Split(' ', '\t', '\r', '\n');
                // Remove all empty strings received due to duplicate separators
                line = Array.FindAll(line, x => x != "");

                if (line.Length != readedText.Count)
                {
                    throw new WrongMatrixException("[ReadMatrix]: Matrix isn't square");
                }
                for (int j = 0; j < readedText.Count; j++)
                {
                    // LMAO
                    line[j] = line[j].Replace('.', ',');
                    if (!double.TryParse(line[j], out mMatrix[i, j]))
                    {
                        throw new WrongMatrixException("[ReadMatrix]: Matrix file contains wrong symbols!");
                    }
                }
            }

            Console.WriteLine("Matrix file reading is success");
        }
        private void ReadCoordinates(List<string> readedText)
        {
            double[,] CoordBuffer = new double[DIM_COUNT, readedText.Count];
            for (int i = 0; i < readedText.Count; i++)
            {
                // Split by separators
                string[] line = readedText[i].Split(' ', '\t', '\r', '\n');
                // Remove all empty strings received due to duplicate separators
                line = Array.FindAll(line, x => x != "");

                if (line.Length != DIM_COUNT)
                {
                    throw new WrongMatrixException("[ReadCoordinate]: Incorrect count of dimetsions");
                }

                for (int j = 0; j < DIM_COUNT; j++)
                {
                    // LMAO
                    line[j] = line[j].Replace('.', ',');
                    if (!double.TryParse(line[j], out CoordBuffer[j, i]))
                    {
                        throw new WrongMatrixException("[ReadCoordinate]: Coordinate file contains wrong symbols");
                    }
                }
            }

            mMatrix = new double[readedText.Count, readedText.Count];
            for (int i = 0; i < readedText.Count; i++)
            {
                for (int j = 0; j < readedText.Count; j++)
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

            Console.WriteLine("Coordinate file reading is success");
        }

        public abstract bool MatrixIsCorrect();
        public abstract double FitnessFunction(Person person);
        public abstract int Distance(Person p1, Person p2);
    }
}
