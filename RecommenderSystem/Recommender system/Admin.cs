using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                segment.avgRatingByUser[i] = Convert.ToDouble(result.Rows[i][1] == "" ? 0 : result.Rows[i][1]);
                for (int j = 0; j < segment.data.cols; j++)
                    segment.data[i, j] = Convert.ToDouble(result.Rows[i + 1][j + 2] == "" ? 0 : result.Rows[i + 1][j + 2]);
            }

            List<Sample> resamples = Sample.Resampling(segment);

            int count = 0;
            foreach (Sample sample in resamples)
            {
                count++;
                sample.Export("D:\\Temp\\TestResampling\\" + count + ".xlsx");
            }

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
        }
    }
}
