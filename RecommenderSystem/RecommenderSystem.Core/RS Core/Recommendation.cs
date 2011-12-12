using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Model;

namespace RecommenderSystem.Core.RS_Core
{
    public class Recommendation
    {
        public Item item;
        public double ratingEstimated;

        public static int k = 5;

        public Recommendation (Item item, double ratingEstimated)
        {
            this.item = item;
            this.ratingEstimated = ratingEstimated;
        }

        public static List<Recommendation> Recommend(string user_email, int companion, int familiarity, int mood)
        {
            User user = new User(user_email);
            List<Recommendation> result = new List<Recommendation>();
            Segment[] candidates = Segment.GetCandidates();
            Matrix chosenOne = null;
            for (int i = 0; i < candidates.Length; i++)
            {
                if (candidates[i].companion.id == companion
                    && candidates[i].familiarity.id == familiarity
                    && candidates[i].mood.id == mood)
                {
                    candidates[i].GetData();
                    chosenOne = candidates[i].data;
                    break;
                }

                if (i == candidates.Length - 1)
                {
                    chosenOne = Rating.GetFullSegment();
                }
            }

            int limit = 10;

            Matrix P = Matrix.RandomMatrix(chosenOne.rows, k, 5);
            Matrix Q = Matrix.RandomMatrix(chosenOne.cols, k, 5);
            Matrix R = MatrixFactorization.MatrixFactorize(chosenOne, ref P, ref Q, k);

            for (int i = 0; i < R.rows; i++)
            { 
                
            }

                /*foreach (Item item in Item.GetItemsIn(chosenOne))
                {
                    double estimate_rating = CollaborativeFiltering.EstimateRating(item, user, chosenOne);
                    if (!Double.IsNaN(estimate_rating))
                    {
                        Recommendation r = new Recommendation(item, estimate_rating);
                        result.Add(r);
                    }
                    if (result.Count == limit)
                        break;
                }*/

                return result;
        }
    }
}
