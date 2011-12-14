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

            //Test phase
            double sum = 0;
            int count = 0;
            for (int i = 0; i < Evaluation.data.rows; i++)
                for (int j = 0; j < Evaluation.data.cols - 1; j++)
                    if (Evaluation.data[i, j] != 0)
                    {
                        double predict_scrore = Regression.Prediction(Evaluation.user_id[i], Convert.ToInt32(Evaluation.item_id[j].Trim()), Training);
                        sum += Math.Abs(Evaluation.data[i, j] - predict_scrore);
                        count++;
                    }
            return sum / count;
        }
        /*private static double MAE(Matrix Training, Matrix Evaluation)//Mean absolute error
        {
            int k = Recommendation.k;
            Matrix P = Matrix.RandomMatrix(Training.rows, k, 5);
            Matrix Q = Matrix.RandomMatrix(Training.cols, k, 5);
            Matrix R = MatrixFactorization.MatrixFactorize(Training, ref P, ref Q, k);

            double formular = 0;
            for (int i = 0; i < Evaluation.rows; i++)
                for (int j = 0; j < Evaluation.cols; j++)
                    if (Evaluation[i, j] != 0)
                        formular += Math.Abs(R[i, j] - Evaluation[i, j]);


            return formular / (Evaluation.CountCells());
        }*/
        private static void GetRandomEvaluationSet(Matrix root, ref Matrix Eval, ref Matrix Training, ref Matrix Training_root)
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
        }
        public static bool GetStrongSegments()
        {
            /*
            Segment segment = Segment.GetRoot();
            Matrix Evaluation_Matrix = new Matrix(segment.data.rows, segment.data.cols);
            Matrix Training_Segment_Matrix = segment.data.Duplicate();
            Matrix Training_root_Matrix = segment.data.Duplicate();
            GetRandomEvaluationSet(segment.data, ref Evaluation_Matrix, ref Training_Segment_Matrix, ref Training_root_Matrix);
            double performamce_segment = Performance(new Segment(segment, Training_Segment_Matrix), new Segment(segment, Evaluation_Matrix));
            */

            
            List<Segment> AllSegment = Segment.GetAllSegment();
            Segment root = Segment.GetRoot();
            double sum_Correlation_Avg_root = 0;
            int count_Correlation_Avg_root = 0;

            DbHelper.RunScripts("truncate table segments");

            foreach (Segment segment in AllSegment)
            {
                Matrix Evaluation_Matrix = new Matrix(segment.data.rows, segment.data.cols);
                Matrix Training_Segment_Matrix = segment.data.Duplicate();
                Matrix Training_root_Matrix = root.data.Duplicate();
                GetRandomEvaluationSet(segment.data, ref Evaluation_Matrix, ref Training_Segment_Matrix, ref Training_root_Matrix);

                //Get strong segment
                Segment Evaluation_Segment = new Segment(segment, Evaluation_Matrix);
                Segment Training_Segment = new Segment(segment, Training_Segment_Matrix);
                Segment Training_Root = new Segment(root, Training_root_Matrix);

                double performamce_segment = MAE(ref Training_Segment, Evaluation_Segment);
                double performance_root = MAE(ref Training_Root, Evaluation_Segment);

                sum_Correlation_Avg_root += Training_Root.Correlation_Avg;
                count_Correlation_Avg_root += 1;

                if (performamce_segment <= performance_root)
                {
                    //DbHelper.RunScripts(string.Format("pr_insertSegment " + segment.budget.id 
                    //    + ", " + segment.companion.id + ", " + segment.familiarity.id
                    //    + ", " + segment.mood.id + ", " + segment.temperature.id
                    //    + ", " + segment.travelLength.id + ", " + segment.weather.id
                    //    + ", " + performamce_segment));

                    DbHelper.RunScripts(string.Format("pr_insertSegment " + 0
                        + ", " + segment.companion.id + ", " + segment.familiarity.id
                        + ", " + segment.mood.id + ", " + 0
                        + ", " + 0 + ", " + 0
                        + ", " + performamce_segment
                        + ", " + Training_Segment.Correlation_Avg));
                }
            }

            //Remove segment "child" and have performance less than its parents

            Segment[] candidates = Segment.GetCandidates();

            for (int i = 0; i < candidates.Length - 1; i++)
            {
                for (int j = i + 1; j < candidates.Length; j++)
                {
                    if (candidates[j].IsChildOf(candidates[i]))
                    {
                        DbHelper.RunScripts(string.Format("delete from segments where id = " + candidates[j].id));
                    }
                }
            }

            DbHelper.RunScripts(string.Format("pr_insertSegment " + 0
                        + ", " + 0 + ", " + 0
                        + ", " + 0 + ", " + 0
                        + ", " + 0 + ", " + 0
                        + ", " + 9999
                        + ", " + sum_Correlation_Avg_root/count_Correlation_Avg_root));

            return true;
            
        }

        public static bool temp()
        {
            //Remove segment "child" and have performance less than its parents

            Segment[] candidates = Segment.GetCandidates();

            for (int i = 0; i < candidates.Length - 1; i++)
            {
                for (int j = i + 1; j < candidates.Length; j++)
                {
                    if (candidates[i].IsChildOf(candidates[j]))
                    {
                        DbHelper.RunScripts(string.Format("delete from segments where id = " + candidates[i].id));
                    }
                }
            }

            return true;
        }
    }
}
