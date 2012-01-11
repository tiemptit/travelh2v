using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Model;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.RS_Core
{
    public class Reduction
    {
        private static double Performance(ref Segment Training, Segment Evaluation)
        { 
            //MAE
            return MAE(ref Training, Evaluation);
        }
        private static double MAE(ref Segment Training, Segment Evaluation)//Mean absolute error
        {
            //Training phase
            Training.Correlation_Avg = Regression.Correlation_Avg(Training);
            //Training.Correlation_Avg = 1.036;

            if (Double.IsNaN(Training.Correlation_Avg)) // Ma trận quá thưa thớt
            {
                return 9999;
            }

            //Test phase
            double sum = 0;
            int count = 0;
            for (int i = 0; i < Evaluation.data.rows; i++)
                for (int j = 0; j < Evaluation.data.cols - 1; j++)
                    if (Evaluation.data[i, j] != 0)
                    {
                        double predict_scrore = Regression.Prediction(Evaluation.user_id[i], Convert.ToInt32(Evaluation.item_id[j].Trim()), Training);
                        /*if (Double.IsNaN(predict_scrore))
                        {
                            predict_scrore = Regression.Prediction(Evaluation.user_id[i], Convert.ToInt32(Evaluation.item_id[j].Trim()), Training);
                        }*/
                        if (Double.IsNaN(predict_scrore) && predict_scrore != 0) // Ko có điểm chung
                            continue;
                        sum += Math.Abs(Evaluation.data[i, j] - predict_scrore);
                        count++;
                    }
            return sum / count;
        }
        
        /*private static void GetRandomEvaluationSet(Matrix root, ref Matrix Eval, ref Matrix Training, ref Matrix Training_root)
        {
            Random rand = new Random();
            int need = root.CountCells() / 10;
            int count = 0;
            while (count < need)
            {
                for (int i = 0; i < root.rows; i++)
                {
                    for (int j = 0; j < root.cols; j++)
                        if (Eval[i, j] == 0)
                            if (rand.Next(1, 10) == 5 || rand.Next(1, 10) == 6)
                            {
                                if (root[i, j] != 0)
                                {
                                    Eval[i, j] = root[i, j];
                                    Training[i, j] = 0;
                                    Training_root[i, j] = 0;
                                    count++;
                                }
                                if (count == need)
                                    break;
                            }
                    if (count == need)
                        break;
                }
            }
        }*/
         
        public static void Test()
        {
            //Segment segment = Segment.GetRoot();
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

            //Resampling
            Matrix[] Evaluation_Matrix = new Matrix[10];
            Matrix[] Training_Segment_Matrix = new Matrix[10];

            for (int i = 0; i < 10; i++)
            {
                Evaluation_Matrix[i] = new Matrix(segment.data.rows, segment.data.cols);
                Training_Segment_Matrix[i] = segment.data.Duplicate();
            }

            int count_segment = 0;

            int need = segment.data.CountCells() / 10;
            Random rand = new Random();

            for (int i = 0; i < segment.data.rows; i++)
            {
                for (int j = 0; j < segment.data.cols; j++)
                {
                    if (segment.data[i, j] != 0)
                    {
                        bool isUsed = false;
                        while (!isUsed)
                        {
                            int k = rand.Next(0, 10);
                            if (Evaluation_Matrix[k].CountCells() < need + 1)
                            {
                                isUsed = true;
                                Evaluation_Matrix[k][i, j] = segment.data[i, j];
                                Training_Segment_Matrix[k][i, j] = 0;
                            }
                        }
                    }
                }
            }

            double performamce_segment = 0;

            double correlation_avg_segment = 0;
            for (int i = 0; i < 10; i++)
            {
                //Get strong segment
                Segment Evaluation_Segment = new Segment(segment, Evaluation_Matrix[i]);
                Segment Training_Segment = new Segment(segment, Training_Segment_Matrix[i]);

                performamce_segment += MAE(ref Training_Segment, Evaluation_Segment);
                correlation_avg_segment += Training_Segment.Correlation_Avg;
            }

            
            /*DbHelper.RunScripts(string.Format("pr_insertSegment " + 0 //time
                + ", " + segment.budget.id
                + ", " + segment.companion.id
                + ", " + segment.weather.id
                + ", " + performamce_segment / 10
                + ", " + correlation_avg_segment / 10), "Data Warehouse");
            

            //Remove segment "child" and have performance less than its parents

            
            Segment[] candidates = Segment.GetCandidates();

            for (int i = 0; i < candidates.Length - 1; i++)
            {
                for (int j = i + 1; j < candidates.Length; j++)
                {
                    if (candidates[j].IsChildOf(candidates[i]))
                    {
                        DbHelper.RunScripts(string.Format("delete from segments where id = " + candidates[j].id), "Data Warehouse");
                    }
                }
            }
             */
        }

        public static bool GetStrongSegments()
        {            
            List<Segment> AllSegment = Segment.GetAllSegment();
            Segment root = Segment.GetRoot();
            double sum_Correlation_Avg_root = 0;
            int count_Correlation_Avg_root = 0;

            DbHelper.RunScripts("truncate table segments", "Data Warehouse");

            foreach (Segment segment in AllSegment)
            {
                //Resample segment
                double performamce_segment = 0;
                double performance_root = 0;
                double correlation_avg_segment = 0;
                
                List<Sample> resamples = Sample.Resampling(segment);
                foreach (Sample sample in resamples)
                {
                    double temp_corr_avg_segment = sample.Train();
                    correlation_avg_segment += temp_corr_avg_segment;
                    performamce_segment += sample.Test(temp_corr_avg_segment);

                    Sample sample_root = new Sample(sample.Evaluation, sample.Training_root);
                    sum_Correlation_Avg_root += sample_root.Train();
                    count_Correlation_Avg_root += 1;
                    performance_root += sample_root.Test(sum_Correlation_Avg_root/count_Correlation_Avg_root);
                }

                if (performamce_segment <= performance_root)
                {
                    DbHelper.RunScripts(string.Format("pr_insertSegment " + 0 //time
                        + ", " + segment.budget.id
                        + ", " + segment.companion.id
                        + ", " + segment.weather.id
                        + ", " + performamce_segment / 10
                        + ", " + correlation_avg_segment / 10), "Data Warehouse");
                }
                
                //Remove segment "child" and have performance less than its parents

                Segment[] candidates = Segment.GetCandidates();

                for (int i = 0; i < candidates.Length - 1; i++)
                {
                    for (int j = i + 1; j < candidates.Length; j++)
                    {
                        if (candidates[j].IsChildOf(candidates[i]))
                        {
                            DbHelper.RunScripts(string.Format("delete from segments where id = " + candidates[j].id), "Data Warehouse");
                        }
                    }
                }
            }

            DbHelper.RunScripts(string.Format("pr_insertSegment "
                        + 0 + ", " + 0
                        + ", " + 0 + ", " + 0
                        + ", " + 9999
                        + ", " + sum_Correlation_Avg_root / count_Correlation_Avg_root), "Data Warehouse");

            return true;
            
        }
    }
}
