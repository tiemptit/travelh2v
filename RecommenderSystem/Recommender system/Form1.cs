using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecommenderSystem.Core;
using RecommenderSystem.Core.Helper;
using RecommenderSystem.Core.RS_Core;
using RecommenderSystem.Core.Model;


namespace Recommender_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (DbHelper.Test())
                MessageBox.Show("OK");
            else
                MessageBox.Show("Oops!");
        }

        private void btnImportExcelData_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.xlsx|*.xlsx";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataSync dsync = new DataSync();
                bool result = dsync.ImportExcelData(dlg.FileName);

                if (result == true)
                {
                    MessageBox.Show("Excel Data import OK");
                }
                else
                {
                    MessageBox.Show("Problem import Excel data, please check log file for details");
                }
            }
        }

        private void btnEstimate_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(txtUser.Text);
            int item_id = Convert.ToInt32(txtItem.Text);
            //DataTable FullSegment = Rating.GetFullSegment();
            //double estimate = CollaborativeFiltering.EstimateRating(new Item(item_id), new User(user_id), FullSegment);
            //MessageBox.Show(estimate.ToString());
        }

        private void btnTestAdomd_Click(object sender, EventArgs e)
        {
            if (Reduction.GetStrongSegments())
                MessageBox.Show("Done");
        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            if (Reduction.temp())
                MessageBox.Show("Done");
        }

        private void btnEstimate_Context_Click(object sender, EventArgs e)
        {
            Recommendation.Recommend("10008", 1, 0, 3, "2");
        }

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            Reduction.Test();
        }

        /*private void btnTestMatrix_Click(object sender, EventArgs e)
        {

            double[,] test = new double[5, 4] { { 5, 3, 0, 1 }, { 4, 0, 0, 1 }, { 1, 1, 0, 5 }, { 1, 0, 0, 4 }, { 0, 1, 5, 4 } };
            Matrix R = new Matrix(5, 4);
            R.mat = test;

            //Matrix R = Matrix.RandomMatrix(5, 4, 5);
            Matrix P = Matrix.RandomMatrix(5, 2, 5);
            Matrix Q = Matrix.RandomMatrix(4, 2, 5);

            MatrixFactorization.testc(R, P, Q, 10);
        }*/

    }
}
