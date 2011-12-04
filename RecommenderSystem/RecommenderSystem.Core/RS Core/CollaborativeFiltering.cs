using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Model;

namespace RecommenderSystem.Core.RS_Core
{
    public class CollaborativeFiltering
    {
        private static double CosineSimilarity(User a, User b, DataTable segment)
        {

            List<Item> itemRatedByA = Item.GetItemRatedByUser(a, segment);
            List<Item> itemRatedByB = Item.GetItemRatedByUser(b, segment);
            List<Item> itemRatedByAB = itemRatedByA.Intersect<Item>(itemRatedByB).ToList<Item>();

            double formula1 = 0, formula2 = 0, formula3 = 0;

            foreach (Item item in itemRatedByAB)
            {
                List<Rating> lri_a = Rating.GetRatingListByUserAndForItem(a, item, segment);
                List<Rating> lri_b = Rating.GetRatingListByUserAndForItem(b, item, segment);

                //Rate nhiều lần cho 1 place? ~~

                Rating ri_a = lri_a[0];
                Rating ri_b = lri_b[0];

                formula1 += ri_a.rating * ri_b.rating;
                formula2 += ri_a.rating * ri_a.rating;
                formula3 += ri_b.rating * ri_b.rating;
            }
            if (formula2 == 0 || formula3 == 0)
                return 0;
            return formula1 / (Math.Sqrt(formula2) * Math.Sqrt(formula3));
        }

        public static double EstimateRating(Item item, User user, DataTable segment)
        {
            DataTable FullSegment = Rating.GetFullSegment();
            double avg_ru = user.GetAverageRating(FullSegment);
            double formula1 = 0;
            double formula2 = 0;
            List<Rating> rating_for_item_before = Rating.GetRatingListForItem(item, segment);
            foreach (Rating rating in rating_for_item_before)
            {
                User other = new User(rating.id_user);
                double sim = CosineSimilarity(user, other, FullSegment);
                double avg_ro = other.GetAverageRating(FullSegment);

                formula1 += sim * (rating.rating - avg_ro);
                formula2 += sim;
            }

            return avg_ru + (1 / formula2) * formula1;
        }
    }

    
}
