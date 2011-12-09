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
        private static double Performance(Matrix Training, Matrix Evaluation)
        { 
            //MAE
            return - MAE(Training, Evaluation);
        }
        private static double MAE(Matrix Training, Matrix Evaluation)//Mean absolute error
        {
            int k = 2;
            Matrix P = Matrix.RandomMatrix(Training.rows, k, 5);
            Matrix Q = Matrix.RandomMatrix(Training.cols, k, 5);
            Matrix R = MatrixFactorization.MatrixFactorize(Training, ref P, ref Q, k);

            double formular = 0;
            for (int i = 0; i < Evaluation.rows; i++)
                for (int j = 0; j < Evaluation.cols; j++)
                    if (Evaluation[i, j] != 0)
                        formular += Math.Abs(R[i, j] - Evaluation[i, j]);


            return formular / (Evaluation.CountCells());
        }
        private static void GetRandomEvaluationSet(Matrix root, ref Matrix Eval, ref Matrix Training)
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
        public static bool GetStrongSegments(Matrix root)
        {
            List<Segment> AllSegment = Segment.GetAllSegment();

            foreach (Segment segment in AllSegment)
            {
                Matrix Evaluation_Segment = new Matrix(segment.data.rows, segment.data.cols);
                Matrix Training_Segment = segment.data.Duplicate();
                GetRandomEvaluationSet(segment.data, ref Evaluation_Segment, ref Training_Segment);

                Matrix Evaluation_root = new Matrix(root.rows, root.cols);
                Matrix Training_root = root.Duplicate();
                GetRandomEvaluationSet(root, ref Evaluation_root, ref Training_root);

                //Get strong segment
                double performamce_segment = Performance(Training_Segment, Evaluation_Segment);
                double performance_root = Performance(Training_root, Evaluation_root);

                //DbHelper.RunScripts("truncate table segments");

                if (performamce_segment >= performance_root)
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
                        + ", " + performamce_segment));
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
