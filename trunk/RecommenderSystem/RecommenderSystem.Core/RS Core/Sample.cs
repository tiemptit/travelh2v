using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.RS_Core
{
    public class Sample
    {
        public Segment Evaluation;
        public Segment Training;
        public Segment Training_root;

        public Sample(Segment Eval, Segment Training)
        {
            this.Evaluation = Eval;
            this.Training = Training;
        }

        public Sample(DataTable EvalTable, DataTable TrainingTable)
        {
            Segment EvalSegment = new Segment();
            EvalSegment.data = new Matrix(EvalTable.Rows.Count - 1, EvalTable.Columns.Count - 2);
            EvalSegment.user_id = new int[EvalTable.Rows.Count - 1];
            EvalSegment.item_id = new string[EvalTable.Columns.Count - 2];
            EvalSegment.avgRatingByItem = new double[EvalTable.Columns.Count - 2];
            EvalSegment.avgRatingByUser = new double[EvalTable.Rows.Count - 1];

            //item
            for (int i = 0; i < EvalSegment.item_id.Length; i++)
            {
                EvalSegment.item_id[i] = EvalTable.Columns[i + 2].ColumnName.Trim();
                EvalSegment.avgRatingByItem[i] = Convert.ToDouble(EvalTable.Rows[0][i + 2] == "" ? 0 : EvalTable.Rows[0][i + 2]);
            }

            for (int i = 0; i < EvalSegment.data.rows; i++)
            {
                EvalSegment.user_id[i] = Convert.ToInt32(EvalTable.Rows[i + 1][0]);
                EvalSegment.avgRatingByUser[i] = Convert.ToDouble(EvalTable.Rows[i + 1][1] == "" ? 0 : EvalTable.Rows[i + 1][1]);
                for (int j = 0; j < EvalSegment.data.cols; j++)
                    EvalSegment.data[i, j] = Convert.ToDouble(EvalTable.Rows[i + 1][j + 2] == "" ? 0 : EvalTable.Rows[i + 1][j + 2]);
            }

            Segment TrainingSegment = new Segment();
            TrainingSegment.data = new Matrix(TrainingTable.Rows.Count - 1, TrainingTable.Columns.Count - 2);
            TrainingSegment.user_id = new int[TrainingTable.Rows.Count - 1];
            TrainingSegment.item_id = new string[TrainingTable.Columns.Count - 2];
            TrainingSegment.avgRatingByItem = new double[TrainingTable.Columns.Count - 2];
            TrainingSegment.avgRatingByUser = new double[TrainingTable.Rows.Count - 1];

            //item
            for (int i = 0; i < TrainingSegment.item_id.Length; i++)
            {
                TrainingSegment.item_id[i] = TrainingTable.Columns[i + 2].ColumnName.Trim();
                TrainingSegment.avgRatingByItem[i] = Convert.ToDouble(TrainingTable.Rows[0][i + 2] == "" ? 0 : TrainingTable.Rows[0][i + 2]);
            }

            for (int i = 0; i < TrainingSegment.data.rows; i++)
            {
                TrainingSegment.user_id[i] = Convert.ToInt32(TrainingTable.Rows[i + 1][0]);
                TrainingSegment.avgRatingByUser[i] = Convert.ToDouble(TrainingTable.Rows[i + 1][1] == "" ? 0 : TrainingTable.Rows[i + 1][1]);
                for (int j = 0; j < TrainingSegment.data.cols; j++)
                    TrainingSegment.data[i, j] = Convert.ToDouble(TrainingTable.Rows[i + 1][j + 2] == "" ? 0 : TrainingTable.Rows[i + 1][j + 2]);
            }

            this.Evaluation = EvalSegment;
            this.Training = TrainingSegment;
        }

        public static List<Sample> Resampling (Segment segment)
        {
            
            List<Sample> result = new List<Sample>();

            Matrix[] Evaluation_Matrix = new Matrix[10];
            Matrix[] Training_Segment_Matrix = new Matrix[10];
            Matrix[] Training_Root_Matrix = new Matrix[10];

            for (int i = 0; i < 10; i++)
            {
                Evaluation_Matrix[i] = new Matrix(segment.data.rows, segment.data.cols);
                Training_Segment_Matrix[i] = segment.data.Duplicate();
                Training_Root_Matrix[i] = segment.data.Duplicate();
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
                                Training_Root_Matrix[k][i, j] = 0;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                Segment Evaluation_Segment = new Segment(segment, Evaluation_Matrix[i]);
                Segment Training_Segment = new Segment(segment, Training_Segment_Matrix[i]);
                
                Sample sample = new Sample(Evaluation_Segment, Training_Segment);
                sample.Training_root = new Segment(segment, Training_Root_Matrix[i]);

                result.Add(sample);
            }
            return result;
        }

        public double Train()
        {
            //Training phase
            //Training.Correlation_Avg = Regression.Correlation_Avg(Training);
            //return Training.Correlation_Avg;
            return Regression.Correlation_Avg(Training);
        }

        public double Test(double Correlation_Avg)
        {
            Training.Correlation_Avg = Correlation_Avg;
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
                        if (Double.IsNaN(predict_scrore) || predict_scrore == 0) // Ko có điểm chung
                            continue;
                        sum += Math.Abs(Evaluation.data[i, j] - predict_scrore);
                        count++;
                    }
            return sum / count;
        }

        public double Test(double Correlation_Avg, ref DataTable result)
        {
            Training.Correlation_Avg = Correlation_Avg;
            if (Double.IsNaN(Training.Correlation_Avg)) // Ma trận quá thưa thớt
            {
                return 9999;
            }
            result.Columns.Add("...");
            result.Columns.Add("All");
            for (int i = 0; i < Evaluation.item_id.Length; i++)
                result.Columns.Add(Evaluation.item_id[i]);

            DataRow dRow0 = result.NewRow();
            dRow0[0] = "All";
            dRow0[1] = "...";
            for (int i = 0; i < Evaluation.avgRatingByItem.Length; i++)
            {
                dRow0[i + 2] = "...";
            }
            result.Rows.Add(dRow0);

            //Test phase
            double sum = 0;
            int count = 0;
            for (int i = 0; i < Evaluation.data.rows; i++)
            {
                DataRow dRow = result.NewRow();
                dRow[0] = Evaluation.user_id[i];
                dRow[1] = "...";

                for (int j = 0; j < Evaluation.data.cols - 1; j++)
                    if (Evaluation.data[i, j] != 0)
                    {
                        double predict_scrore = Regression.Prediction(Evaluation.user_id[i], Convert.ToInt32(Evaluation.item_id[j].Trim()), Training);
                        dRow[j + 2] = predict_scrore;
                        /*if (Double.IsNaN(predict_scrore))
                        {
                            predict_scrore = Regression.Prediction(Evaluation.user_id[i], Convert.ToInt32(Evaluation.item_id[j].Trim()), Training);
                        }*/
                        if (Double.IsNaN(predict_scrore) || predict_scrore == 0) // Ko có điểm chung
                            continue;
                        sum += Math.Abs(Evaluation.data[i, j] - predict_scrore);
                        count++;
                    }
                    else
                    {
                        dRow[j + 2] = "###";
                    }
                result.Rows.Add(dRow);
            }
            return sum / count;
        }

        public void Export(string path)
        {
            DataTable dataTrain = new DataTable();
            dataTrain.Columns.Add("...");
            dataTrain.Columns.Add("All");
            for (int i = 0; i < Training.item_id.Length; i++ )
                dataTrain.Columns.Add(Training.item_id[i]);

            DataRow dRow0 = dataTrain.NewRow();
            dRow0[0] = "All";
            dRow0[1] = "...";
            for (int i = 0; i < Training.avgRatingByItem.Length; i++)
            {
                dRow0[i + 2] = Training.avgRatingByItem[i];
            }
            dataTrain.Rows.Add(dRow0);

            for (int i = 0; i < Training.data.rows; i++)
            {
                DataRow dRow = dataTrain.NewRow();
                dRow[0] = Training.user_id[i];
                dRow[1] = Training.avgRatingByUser[i];
                for (int j = 0; j < Training.data.cols; j++)
                {
                    dRow[j + 2] = Training.data[i, j];
                }
                dataTrain.Rows.Add(dRow);
            }

            ExcelHelper.SetDataToExcel(path, "Training", dataTrain);

            //Eval

            DataTable dataEval = new DataTable();
            dataEval.Columns.Add("...");
            dataEval.Columns.Add("All");
            for (int i = 0; i < Evaluation.item_id.Length; i++)
                dataEval.Columns.Add(Evaluation.item_id[i]);

            DataRow dRow_0 = dataEval.NewRow();
            dRow_0[0] = "All";
            dRow_0[1] = "...";
            for (int i = 0; i < Evaluation.avgRatingByItem.Length; i++)
            {
                dRow_0[i + 2] = Evaluation.avgRatingByItem[i];
            }
            dataEval.Rows.Add(dRow_0);

            for (int i = 0; i < Evaluation.data.rows; i++)
            {
                DataRow dRow = dataEval.NewRow();
                dRow[0] = Evaluation.user_id[i];
                dRow[1] = Evaluation.avgRatingByUser[i];
                for (int j = 0; j < Evaluation.data.cols; j++)
                {
                    dRow[j + 2] = Evaluation.data[i, j];
                }
                dataEval.Rows.Add(dRow);
            }

            ExcelHelper.SetDataToExcel(path, "Eval", dataEval);
        }
    }
}
