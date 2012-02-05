using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RecommenderSystem.Core.RS_Core
{
    public class Statistic
    {
        private DataTable data;
        public Statistic(DataTable data)
        {
            this.data = data;
        }

        public double Coefficient_Of_Variation() // Hệ số biến thiên CV
        {
            double sum_cv = 0;
            int count_cv = 0;

            for (int i = 1; i <= 20; i++)//20 Places
            {
                List<int> l_rating_place = new List<int>();
                foreach (DataRow drow in data.Rows)
                {
                    if (drow["place_key"].ToString() == i.ToString())
                        l_rating_place.Add(Convert.ToInt32(drow["rating"].ToString()));
                }
                if (l_rating_place.Count > 0)
                {
                    double m = 0; // Kỳ vọng
                    int count = 0;
                    foreach (int x in l_rating_place)
                    {
                        m += x;
                        count++;
                    }

                    m = m / count;

                    sum_cv += (Statistic.Standard_Deviation(l_rating_place) / m) * (((double)l_rating_place.Count) / data.Rows.Count);
                    //sum_cv += (Statistic.Standard_Deviation(l_rating_place) / m);
                    //count_cv++;

                }
            }
            //return sum_cv / count_cv;
            return sum_cv;
        }

        public static double Standard_Deviation(List<int> data) // Độ lệch chuẩn
        {
            double m = 0; // Kỳ vọng
            int count = 0;
            foreach (int x in data)
            {
                m += x;
                count++;
            }
            m = m / count;

            double s = 0;//Phương sai
            foreach (int x in data)
            {
                s += Math.Pow(x - m, 2);
            }

            s = s / count;

            return Math.Sqrt(s);// Độ lệch chuẩn
        }

        public double Mutual_Avg()
        {
            double sum = 0;
            int count = 0;

            for (int i = 1; i <= 20; i++)//20 Places
            {
                List<int> l_rating_place = new List<int>();
                foreach (DataRow drow in data.Rows)
                {
                    if (drow["place_key"].ToString() == i.ToString())
                        l_rating_place.Add(Convert.ToInt32(drow["rating"].ToString()));
                }
                if (l_rating_place.Count > 0)
                {
                    sum += l_rating_place.Count;
                    count ++;
                }
            }

            return sum / count;
        }

        public int Count()
        {
            return data.Rows.Count;
        }
    }
}
