using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecommenderSystem.Core.Model;

namespace RecommenderSystem.Core.RS_Core
{
    public class Regression
    {
        int i, j; // item index in segment
        int[] Uij {get; set;} //u_index (not id)
        public Regression(int i, int j, Segment segment)
        {
            this.i = i;
            this.j = j;
            /*int i_index = -1, j_index = -1;
            for (int index = 0; index < segment.item_id.Length; index++)
            {
                if (Convert.ToInt32(segment.item_id[index].Trim()) == i)
                    i_index = index;
                if (Convert.ToInt32(segment.item_id[index].Trim()) == j)
                    j_index = index;
                if (i_index != -1 && j_index != -1)
                    break;
            }*/
            List<int> L_Uij = new List<int>();
            for (int index = 0; index < segment.data.rows; index++)
            {
                //if (segment.data[index, i_index] != 0 && segment.data[index, j_index] != 0)
                if (segment.data[index, i] != 0 && segment.data[index, j] != 0)
                    L_Uij.Add(index);
            }

            this.Uij = L_Uij.ToArray();
        }

        public double ExpertScore(Segment segment)// i: item cần tính, j: item expert
        {
            double alpha = 0;
            double beta = 0;
            double formular_alpha_1 = 0, formular_alpha_2 = 0;

            double r_uj = 0;//trung bình rating cho item j của các user trên tập giao của i, j

            for (int u = 0; u < Uij.Length; u++)
            {
                formular_alpha_1 += (segment.data[u, i] - segment.avgRatingByItem[i]) * (segment.data[u, j] - segment.avgRatingByItem[j]);
                formular_alpha_2 += Math.Pow(segment.data[u, i] - segment.avgRatingByItem[i], 2);
                r_uj += segment.data[u, j];
            }

            r_uj = r_uj / Uij.Length;
            alpha = formular_alpha_1 / formular_alpha_2;
            beta = segment.avgRatingByItem[i] - alpha * segment.avgRatingByItem[j];

            return alpha * r_uj + beta;
        }

        public double ExpertScore(double r_aj, Segment segment)// i: item cần tính, j: item mà active user đã rate với giá trị x
        {
            double alpha = 0;
            double beta = 0;
            double formular_alpha_1 = 0, formular_alpha_2 = 0;

            for (int u = 0; u < Uij.Length; u++)
            {
                formular_alpha_1 += (segment.data[Uij[u], i] - segment.avgRatingByItem[i]) * (segment.data[Uij[u], j] - segment.avgRatingByItem[j]);
                formular_alpha_2 += Math.Pow(segment.data[Uij[u], i] - segment.avgRatingByItem[i], 2);
            }

            alpha = formular_alpha_1 / formular_alpha_2;
            beta = segment.avgRatingByItem[i] - alpha * segment.avgRatingByItem[j];

            return alpha * r_aj + beta;
        }

        public double MSE(Segment segment)
        {
            double sum = 0;
            for (int u = 0; u < Uij.Length; u++)
            {
                sum += Math.Pow(segment.data[Uij[u], j] - ExpertScore(segment.data[Uij[u], j], segment), 2);
            }

            //sum = 0
            if (sum == 0)
                return 0.01;
            else
            return sum / Uij.Length;
        }

        public static double Correlation(int i, int j, int k, Segment segment)
        {
            Regression R_ij = new Regression(i, j, segment);
            Regression R_ik = new Regression(i, k, segment);
            Regression R_jk = new Regression(j, k, segment);

            double formular1 = 0, formular2 = 0;
            double Cijj = R_ij.MSE(segment);
            double Cikk = R_ik.MSE(segment);

            double temp = 0; //EU_jk[(F_ji - F_ki)^2]
            int count_temp = 0;
            for (int u = 0; u < R_jk.Uij.Length; u++)
            {
                int u_index = R_jk.Uij[u];
                if (segment.data[u_index, i] != 0)
                {
                    temp += Math.Pow(R_ij.ExpertScore(segment.data[u_index, i], segment) - R_ik.ExpertScore(segment.data[u_index, i], segment), 2);
                    count_temp++;
                }
            }
            //temp = temp / R_jk.Uij.Length;
            temp = temp / count_temp;

            formular1 = Cijj + Cikk - temp;
            formular2 = 2 * Math.Sqrt(Cijj * Cikk);

            return formular1 / formular2;
        }

        public static double Correlation_Avg(Segment segment) // Training Phase
        {
            double sum = 0;
            int count = 0;
            
            for (int i = 0; i < segment.item_id.Length - 3; i++)
                for (int j = i + 1; j < segment.item_id.Length - 2; j++)
                    for (int k = j + 1; k < segment.item_id.Length - 1; k++)
                    {
                        double temp = Correlation(i, j, k, segment);
                        if (Double.IsInfinity(temp))
                            temp = Correlation(i, j, k, segment);
                        if (!Double.IsNaN(temp) && !Double.IsInfinity(temp))
                        {
                            sum += temp;
                            count++;
                        }
                    }
            /*
            for (int s = 0; s < segment.item_id.Length; s++)
            {
                Random rand = new Random();
                int i = 1, j = 2, k = 3;
                while (i != j && i != k && j != k)
                {
                    i = rand.Next(0, segment.item_id.Length - 1);
                    j = rand.Next(0, segment.item_id.Length - 1);
                    k = rand.Next(0, segment.item_id.Length - 1);
                }

                double temp = Correlation(i, j, k, segment);
                if (Double.IsInfinity(temp))
                    temp = Correlation(i, j, k, segment);
                if (!Double.IsNaN(temp) && !Double.IsInfinity(temp))
                {
                    sum += temp;
                    count++;
                }
            }*/

            return sum / count;
        }

        public static double Prediction(int active_user_id, int item_id, Segment segment)
        { 
            //index of item_id in segment data
            int item_id_index = -1;
            for (int i = 0; i < segment.item_id.Length; i++)
            {
                if (Convert.ToInt32(segment.item_id[i].Trim()) == item_id)
                {
                    item_id_index = i;
                    break;
                }
            }

            for(int a = 0; a < segment.data.rows; a++)
            {
                if (segment.user_id[a] == active_user_id)
                {
                    List<double> L_MSE_ij = new List<double>(); // i,i: item_id_index
                    List<int> Ia = new List<int>();// Item id index của active user a
                    double MSE_Min = -9999;
                    for (int j = 0; j < segment.data.cols - 1; j++) //Bỏ unknown item
                    {
                        if (segment.data[a, j] != 0)
                        {
                            Ia.Add(j);
                            Regression r = new Regression(item_id_index, j, segment);
                            double MSE_ij = r.MSE(segment);
                            L_MSE_ij.Add(MSE_ij);

                            if (MSE_Min == -9999 || MSE_ij < MSE_Min)
                                MSE_Min = MSE_ij;
                        }
                    }

                    List<double> L_MSE_Diag = new List<double>(); // {C*_i}_jj
                    double Inv_Sum_MSE_Diag = 0;
                    foreach (double MSE_ij in L_MSE_ij)
                    {
                        double MSE_Diag = MSE_ij - segment.Correlation_Avg * MSE_Min;
                        L_MSE_Diag.Add(MSE_Diag);

                        Inv_Sum_MSE_Diag += 1 / MSE_Diag;
                    }

                    int[] _Ia = Ia.ToArray();
                    double[] _L_MSE_Diag = L_MSE_Diag.ToArray();

                    double predict_ai = 0;
                    for(int j = 0; j < _Ia.Length; j++)
                    {
                        double wji = 1 / (_L_MSE_Diag[j] * Inv_Sum_MSE_Diag);
                        Regression r = new Regression(item_id_index, _Ia[j], segment);
                        predict_ai += wji * r.ExpertScore(segment.data[a, _Ia[j]], segment);
                    }

                    return predict_ai;
                }

            }

            return 0;
        }
    }
}
