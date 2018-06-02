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
        public SalesmanMatrixWrapper(string filename) : base(filename) {/*mMinMax = false;*/ }
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
            catch (FileNotFoundException)
            {
                return -1;
            }
            catch (ArgumentException)
            {
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
                string[] line = readedText[i].Split(' ');
                if (line.Length != mMatrixSize)
                {
                    return -2;
                }
                for(int j = 0; j < mMatrixSize; j++)
                {
                    // LMAO
                    line[j] = line[j].Replace('.', ',');
                    if (!double.TryParse(line[j], out mMatrix[i, j])) {
                        return -3;
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
    }
}
