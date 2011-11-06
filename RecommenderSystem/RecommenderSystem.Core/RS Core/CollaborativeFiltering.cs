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
        private double CosineSimilarity(User a, User b, DataTable data)
        {
            //Rate nhiều lần cho 1 place? 

            List<Item> itemRatedByA = Item.GetItemRatedByUser(a, data);
            List<Item> itemRatedByB = Item.GetItemRatedByUser(b, data);
            List<Item> itemRatedByAB = itemRatedByA.Intersect<Item>(itemRatedByB).ToList<Item>();

            foreach (Item item in itemRatedByAB)
            {
                List<Rating> lri_a = Rating.GetRatingListByUserAndForItem(a, item, data);
                List<Rating> lri_b = Rating.GetRatingListByUserAndForItem(b, item, data);

                int ra_agg = lri_a.Aggregate;
                int rb_agg = 0;

                foreach (Rating ri_a in lri_a)
                { 
                    ra
                }
            }
        }
    }
}
