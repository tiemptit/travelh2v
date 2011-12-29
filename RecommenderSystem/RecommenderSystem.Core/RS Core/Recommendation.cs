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

        public static List<Recommendation> Recommend(string user_email, int weather, int companion, int budget, string datetime)
        {
            User user = new User(user_email);
            List<Recommendation> result = new List<Recommendation>();
            Segment[] candidates = Segment.GetCandidates();
            Segment chosenOne = null;
            for (int i = 0; i < candidates.Length; i++)
            {
                if (
                    (candidates[i].budget.id == budget || candidates[i].budget.id ==0)
                    && (candidates[i].companion.id == companion || candidates[i].companion.id == 0)
                    //&& (candidates[i].familiarity.id == familiarity || candidates[i].familiarity.id == 0)
                    //&& (candidates[i].mood.id == mood || candidates[i].mood.id == 0)
                    //&& (candidates[i].temperature.id == mood || candidates[i].temperature.id == 0)
                    && (candidates[i].weather.id == weather || candidates[i].weather.id == 0)
                    )
                {
                    candidates[i].GetData();
                    chosenOne = candidates[i];
                    break;
                }
            }


            int min = 100;
            List<Item> ItemFails = new List<Item>();

            for (int i = 0; i < chosenOne.item_id.Length - 1; i++) // Bo unknown
            {
                Item item = new Item(Convert.ToInt32(chosenOne.item_id[i].Trim()));
                double predict = Regression.Prediction(user.id, Convert.ToInt32(chosenOne.item_id[i].Trim()), chosenOne);

                if (!Double.IsNaN(predict) && predict != 0)
                {
                    result.Add(new Recommendation(item, predict));
                }

                else
                {
                    ItemFails.Add(item);
                }
            }

            if (result.Count < min && (chosenOne.budget.id != 0 || chosenOne.companion.id != 0 || chosenOne.weather.id != 0))
            {
                Segment Full = Segment.GetRoot();
                foreach (Item item in ItemFails)
                {
                    double predict = Regression.Prediction(user.id, item.id, Full);
                    if (!Double.IsNaN(predict) && predict != 0)
                    {
                        result.Add(new Recommendation(item, predict));
                    }

                    if (result.Count == min)
                        break;
                }
            }
                return result;
        }
    }
}
