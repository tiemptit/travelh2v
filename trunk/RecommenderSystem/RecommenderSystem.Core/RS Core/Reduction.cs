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
        private static double Performance(DataTable Training, DataTable Evaluation)
        { 
            //MAE
            return - MAE(Training, Evaluation);
        }
        private static double MAE(DataTable Training, DataTable Evaluation)//Mean absolute error
        {
            double formular = 0;
            for (int i = 0; i < Evaluation.Rows.Count; i++)
            {
                Item item = new Item(Convert.ToInt32(Evaluation.Rows[i]["id_place"]));
                User user = new User(Convert.ToInt32(Evaluation.Rows[i]["id_user"]));
                double dR = Convert.ToDouble(Evaluation.Rows[i]["rating"]);
                double dR_Training = CollaborativeFiltering.EstimateRating(item, user, Training);

                formular += Math.Abs(dR_Training - dR);
            }
            return formular / (Evaluation.Rows.Count);
        }
        private static DataTable GetRandomEvaluationSet(DataTable root)
        {
            DataTable result = new DataTable();
            result = root.Clone();
            Random rand = new Random();
            int need = root.Rows.Count / 10;
            List<int> choose = new List<int>();
            while (choose.Count < need)
            {
                foreach (DataRow dr in root.Rows)
                {
                    if (choose.Count == need)
                        break;
                    int r = rand.Next(1, 5);
                    if (r == 3)
                    {
                        if (!choose.Contains(Convert.ToInt32(dr[0])))
                        {
                            DataRow newRow = result.NewRow();
                            newRow.ItemArray = dr.ItemArray;

                            result.Rows.Add(newRow);
                            choose.Add(Convert.ToInt32(dr[0]));
                        }
                    }
                }
            }
            return result;
        }
        public static bool GetStrongSegments(DataTable root)
        {
            List<Segment> AllSegment = Segment.GetAllSegment();

            foreach (Segment segment in AllSegment)
            {
                DataTable Evaluation = GetRandomEvaluationSet(segment.data);
                
                //Get Training set on segment

                DataTable Training_Segment = new DataTable();
                Training_Segment = root.Clone();
                List<int> list_eval_id = new List<int>();
                for (int i = 0; i < Evaluation.Rows.Count; i++)
                {
                    list_eval_id.Add(Convert.ToInt32(Evaluation.Rows[i][0]));
                }
                for (int i = 0; i < segment.data.Rows.Count; i++)
                {
                    if (!list_eval_id.Contains(Convert.ToInt32(segment.data.Rows[i][0])))
                    {
                        DataRow newRow = Training_Segment.NewRow();
                        newRow.ItemArray = segment.data.Rows[i].ItemArray;
                        Training_Segment.Rows.Add(newRow);
                    }
                }

                //Get Training set on root

                DataTable Training_root = new DataTable();
                Training_root = root.Clone();
                
                for (int i = 0; i < root.Rows.Count; i++)
                {
                    if (!list_eval_id.Contains(Convert.ToInt32(root.Rows[i][0])))
                    {
                        DataRow newRow = Training_root.NewRow();
                        newRow.ItemArray = root.Rows[i].ItemArray;
                        Training_root.Rows.Add(newRow);
                    }
                }

                //Get strong segment
                double performamce_segment = Performance(Training_Segment, Evaluation);
                double performance_root = Performance(Training_root, Evaluation);
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
                    if (candidates[i].IsChildOf(candidates[j]))
                    {
                        DbHelper.RunScripts(string.Format("delete from segments where id = " + candidates[i].id));
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
