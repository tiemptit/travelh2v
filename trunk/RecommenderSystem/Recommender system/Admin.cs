using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using RecommenderSystem.Core.RS_Core;
using RecommenderSystem.Core.Helper;

namespace Recommender_system
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {

        }

        private void btnTestML_Click(object sender, EventArgs e)
        {
            Stopwatch swglobal = new Stopwatch();
            Stopwatch swtemp = new Stopwatch();
            swglobal.Start();
            swtemp.Start();
            txtLogResample.AppendText("Process started: " + DateTime.Now);
            txtLogResample.AppendText("\r\nReading Data from Cube ... ");
            //Get data
            Segment segment = new Segment();
            string mdx = "with "
                       + "member Measures.[Ratings AVG] as"
                       + "( "
                       + "[Measures].[Rating]/[Measures].[Ratings100k Count]"
                       + ")"
                       + "select "
                       + "[User100k].[Id].Members on rows, "
                       + "[Movies100k].[Id].Members on columns "
                       + "from Movielens "
                       + "where Measures.[Ratings AVG]";
            DataTable result = DbHelper.RunMDXWithDataTable(mdx);
            segment.data = new Matrix(result.Rows.Count - 1, result.Columns.Count - 2);
            segment.user_id = new int[result.Rows.Count - 1];
            segment.item_id = new string[result.Columns.Count - 2];
            segment.avgRatingByItem = new double[result.Columns.Count - 2];
            segment.avgRatingByUser = new double[result.Rows.Count - 1];

            //item
            for (int i = 0; i < segment.item_id.Length; i++)
            {
                segment.item_id[i] = result.Columns[i + 2].ColumnName.Trim();
                segment.avgRatingByItem[i] = Convert.ToDouble(result.Rows[0][i + 2] == "" ? 0 : result.Rows[0][i + 2]);
            }

            for (int i = 0; i < segment.data.rows - 1; i++)
            {
                segment.user_id[i] = Convert.ToInt32(result.Rows[i + 1][0]);
                segment.avgRatingByUser[i] = Convert.ToDouble(result.Rows[i + 1][1] == "" ? 0 : result.Rows[i + 1][1]);
                for (int j = 0; j < segment.data.cols; j++)
                    segment.data[i, j] = Convert.ToDouble(result.Rows[i + 1][j + 2] == "" ? 0 : result.Rows[i + 1][j + 2]);
            }

            swtemp.Stop();
            txtLogResample.AppendText(swtemp.Elapsed.ToString());
            txtLogResample.AppendText("\r\nStart resampling (this may take a long time) ... ");
            swtemp.Start();
            List<Sample> resamples = Sample.Resampling(segment);
            swtemp.Stop();
            txtLogResample.AppendText(swtemp.Elapsed.ToString());
            txtLogResample.AppendText("\r\nExporting result ...");
            swtemp.Start();
            int count = 0;
            foreach (Sample sample in resamples)
            {
                count++;
                sample.Export("D:\\Temp\\TestResampling\\" + count + ".xlsx");
            }
            swtemp.Stop();
            txtLogResample.AppendText(swtemp.Elapsed.ToString());
            swglobal.Stop();
            txtLogResample.AppendText("\r\nProcess ended: " + DateTime.Now + " - Total time: " + swglobal.Elapsed.ToString());


            /*
            //Evaluate
            double performamce_segment = 0;
            double correlation_avg_segment = 0;

            foreach (Sample sample in resamples)
            {
                double temp_corr_avg_segment = sample.Train();
                correlation_avg_segment += temp_corr_avg_segment;
                performamce_segment += sample.Test(temp_corr_avg_segment);
            }

            MessageBox.Show("Done");

            txtPerformance.Text = Convert.ToString(performamce_segment / 10);
            txtCorrelation.Text = Convert.ToString(correlation_avg_segment / 10);
            */
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.xlsx|*.xlsx";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtInput.Text = dlg.FileName;
            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (txtInput.Text == "")
            {
                MessageBox.Show("Choose a file!");
                return;
            }
            Stopwatch swglobal = new Stopwatch();
            Stopwatch swtemp = new Stopwatch();
            txtLogML.AppendText("Process starts: " + DateTime.Now);
            swglobal.Start();
            txtLogML.AppendText("\r\nReading Evaluation Matrix ... ");
            swtemp.Start();
            DataTable EvalTable = ExcelHelper.GetDataFromExcel(txtInput.Text, "Eval");
            swtemp.Stop();
            txtLogML.AppendText(swtemp.Elapsed.ToString());
            txtLogML.AppendText("\r\nReading Training Matrix ... ");
            swtemp.Start();
            DataTable TrainingTable = ExcelHelper.GetDataFromExcel(txtInput.Text, "Training");
            swtemp.Stop();
            txtLogML.AppendText(swtemp.Elapsed.ToString());
            txtLogML.AppendText("\r\nStarting evaluation");
            Sample sample = new Sample(EvalTable, TrainingTable);
            double performamce_segment = 0;
            double correlation_avg_segment = 0;
            txtLogML.AppendText("\r\nTraining data ... ");
            swtemp.Start();
            correlation_avg_segment = sample.Train();
            swtemp.Stop();
            txtLogML.AppendText(swtemp.Elapsed.ToString());
            txtLogML.AppendText("\r\nTesting data (This may take a long time) ... ");
            swtemp.Start();
            DataTable result = new DataTable();
            performamce_segment = sample.Test(correlation_avg_segment, ref result);
            swtemp.Stop();
            
            txtLogML.AppendText(swtemp.Elapsed.ToString());
            txtLogML.AppendText("\r\nDone evaluate sample:");
            txtLogML.AppendText("\r\nMAE: " + performamce_segment);
            txtLogML.AppendText("\r\nCorreation average: " + correlation_avg_segment);
            txtLogML.AppendText("\r\nProcess finishs: " + DateTime.Now);
            txtLogML.AppendText("\r\nStarting export predict ... ");
            swtemp.Start();
            ExcelHelper.SetDataToExcel(txtInput.Text, "Prediction", result);
            swtemp.Stop();
            txtLogML.AppendText(swtemp.Elapsed.ToString());
            swglobal.Stop();
            txtLogML.AppendText("Process ended " + DateTime.Now + " - Total time: " + swglobal.Elapsed.ToString());
            MessageBox.Show("Done!");
        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            Segment root = Segment.GetRoot();
            Sample sample = new Sample(root, root);
            sample.Export("D:\\haizz.xlsx");

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Reduction.GetStrongSegments())
                MessageBox.Show("Done!");
        }

        private void btnCV_Click(object sender, EventArgs e)
        {
            DataTable data = DbHelper.RunScriptsWithTable(txtSQL.Text, "Data Warehouse");

            Statistic statistic = new Statistic(data);

            double cv = statistic.Coefficient_Of_Variation();

            txtCV.Text = cv.ToString();

            txtMutual_avg.Text = statistic.Mutual_Avg().ToString();

            txtStatisticCount.Text = statistic.Count().ToString();

            MessageBox.Show("Done");
        }

        private void btnRecommend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string time = dtpTime.Value.ToString();
            int budget = cbBudget.SelectedIndex;
            int companion = cbCompanion.SelectedIndex;
            int weather = cbWeather.SelectedIndex;

            List<Recommendation> l_rs = Recommendation.Recommend(email, weather, companion, budget, time);

            foreach (Recommendation rs in l_rs)
            {
                txtRecommend.AppendText("\r\n" + rs.item.name + "----" + rs.ratingEstimated.ToString());
            }

            MessageBox.Show("Done");
        }


    }
}
