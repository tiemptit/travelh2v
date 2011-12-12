using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecommenderSystem.Core.Model;

namespace RecommenderSystem.Core.RS_Core
{
    public class Regression
    {
        int i, j;
        int[] Uij {get; set;}
        public Regression(int i, int j, Segment segment)
        {
            this.i = i;
            this.j = j;
            List<int> Ui = User.GetUserIdsRatedItemId(i, segment);
            List<int> Uj = User.GetUserIdsRatedItemId(j, segment);
            this.Uij = Ui.Intersect<int>(Uj).ToArray();
        }

        public double ExpertScore(double r_aj, Segment segment)// i: item cần tính, j: item mà active user đã rate với giá trị x
        {
            double alpha = 0;
            double beta = 0;
            double formular_alpha_1 = 0, formular_alpha_2 = 0;

            List<int> Ui = User.GetUserIdsRatedItemId(i, segment);
            List<int> Uj = User.GetUserIdsRatedItemId(j, segment);
            int[] Uij = Ui.Intersect<int>(Uj).ToArray();

            for (int u = 0; u < Uij.Length; u++)
            {
                formular_alpha_1 += (segment.data[u, i] - segment.avgRatingByItem[i]) * (segment.data[u, j] - segment.avgRatingByItem[j]);
                formular_alpha_2 += Math.Pow(segment.data[u, i] - segment.avgRatingByItem[i], 2);
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
                sum += Math.Pow(segment.data[u, j] - ExpertScore(segment.data[u, i], segment), 2);
            }
            return sum / Uij.Length;
        }
        /*public static double C(int i, int j, int k, Segment segment)
        {
            Regression r = new Regression(i, j, segment);

            if (k == j)
                return r.MSE();
            else
            {

                double sum = 0;
                for (int u = 0; u < r.Uij.Length; u++)
                {
                    sum += (segment.data[u, j] - r.ExpertScore(segment.data[u, i])) * (segment.data[u, k] - r.ExpertScore(segment.data[u, i]));
                }
                return sum / r.Uij.Length;
            }
        }*/
        public static double Correlation(int i, int j, int k, double r_uj, double r_uk, Segment segment)
        {
            Regression R_ij = new Regression(i, j, segment);
            Regression R_ik = new Regression(i, k, segment);
            Regression R_jk = new Regression(j, k, segment);

            double formular1 = 0, formular2 = 0;
            double Cijj = R_ij.MSE(segment);
            double Cikk = R_ik.MSE(segment);

            formular1 = Cijj + Cikk - (Math.Pow(R_ij.ExpertScore(r_uj, segment) - R_ik.ExpertScore(r_uk, segment), 2) / R_jk.Uij.Length);
            formular2 = 2 * Math.Sqrt(Cijj * Cikk);

            return formular1 / formular2;
        }
    }
}
