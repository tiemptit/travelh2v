using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecommenderSystem.Core.Helper;

namespace TravelWebService
{
    public class RatingModel
    {
        public int id_user { get; set; }
        public int id_place { get; set; }
        public int id_temperature { get; set; }
        public int id_companion { get; set; }
        public int id_farmiliarity { get; set; }
        public int id_mood { get; set; }
        public int id_budget { get; set; }
        public int id_weather { get; set; }
        public int id_travel_length { get; set; }
        public string time { get; set; }
        public float rating { get; set; }

        // insert a rate into DB
        public bool Rate()
        {
            return DbHelper.RunScripts("insert into real_ratings values ('',id_user,id_place,id_temperature,id_companion,id_farmiliarity,id_mood,id_budget,id_budget,id_weather,id_travel_length,time,rating)");
        }
    }
}