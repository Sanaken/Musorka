using System;
using GenAlgorithm;
using System.Windows.Forms;
using GenAlgorithm.Generation;
using GenAlgorithm.Crossover;
using GenAlgorithm.Selection;
using GenAlgorithm.Mutation;

namespace GeneticKV
{
    public partial class MainWindow : Form
    {
        AMatrixWrapper mMWrapper;
        Algorithm mAlgorithm;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            mMWrapper = new SalesmanMatrixWrapper(tbFileName.Text);
            switch (mMWrapper.mState) {
                case -1:
                    MessageBox.Show("File is not found!", "Oops");
                    break;
                case -2:
                    MessageBox.Show("Matrix is not square-type!", "Oops");
                    break;
                case -3:
                    MessageBox.Show("File must contain just numbers!", "Oops");
                    break;
                case 0:
                    MessageBox.Show("Matrix has readed (size of " + mMWrapper.Matrix.GetLength(1) + 
                        "). \nChecking matrix for correct: Diagonal elements must be equals by 0, and not diagonal - more, than 0.", "Success");
                    if (mMWrapper.MatrixIsCorrect())
                    {
                        MessageBox.Show("Matrix is correct. Press the OK to start algorithm with chosen settings...","Success");

                        IStrategyGeneration genOperator;
                        IStrategyCrossover crossOperator;
                        IMutator mutOperator;
                        IStrategySelection selOperator;

                        // I need to think right here
                        genOperator = new RouletteGeneration();
                        if (rbCross1.Checked)
                            crossOperator = new OXCrossOver();
                        if (rbMut1.Checked)
                            mutOperator = new SaltationMutator();
                        else if (rbMut2.Checked)
                            mutOperator = new InversionMutator();
                        if (rbSel1.Checked)
                            selOperator = new RouletteSelection();
                        else if (rbSel2.Checked)
                            selOperator = new RouletteSelection();
                    }
                    else
                    {
                        MessageBox.Show("Unfortunately, matrix is uncorrect. Fixed it!", "Oops");
                    }
                    break;
            }
        }

        private void tbFileName_Click(object sender, EventArgs e)
        {
            tbFileName.Text = "";
        }
    }
}
