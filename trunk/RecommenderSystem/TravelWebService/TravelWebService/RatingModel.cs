using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace TravelWebService
{
    public class RatingModel
    {
        public string username { get; set; }
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
            // if user not register, auto register with the email
            int id_user = checkIfUserExisted();
            if (id_user == -1)
            {
                DbHelper.RunScripts(string.Format("insert into users values ('" + username + "','123',1989,1)"), "Connection String");
                id_user = checkIfUserExisted();
                return DbHelper.RunScripts(string.Format("insert into real_ratings values (" + id_user + "," + id_place + "," + id_temperature + "," + id_companion + "," + id_farmiliarity + "," + id_mood + "," + id_budget + "," + id_weather + "," + id_travel_length + "," + time + "," + rating + ")"), "Connection String");
            }
            else 
            { 
                //if user rated before, update
                int id_real_rating = checkIfRatingExisted();
                if (id_real_rating != -1) 
                {
                    return DbHelper.RunScripts(string.Format("update real_ratings set rating = " + rating + " where id = " + id_real_rating), "Connection String");
                } // else, insert new rating
                else 
                {
                    return DbHelper.RunScripts(string.Format("insert into real_ratings values (" + id_user + "," + id_place + "," + id_temperature + "," + id_companion + "," + id_farmiliarity + "," + id_mood + "," + id_budget + "," + id_weather + "," + id_travel_length + "," + time + "," + rating + ")"), "Connection String");
                } 
            }        
        }

        public int checkIfUserExisted()
        {
            DataTable dt = DbHelper.RunScriptsWithTable(string.Format("select id from users where email ='" + username + "'"), "Connection String");
            if (dt.Rows.Count != 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
                return -1;
        }

        public int checkIfRatingExisted()
        {
            int id_user = checkIfUserExisted();

            DataTable dt = DbHelper.RunScriptsWithTable(string.Format("select id from real_ratings where id_user=" + id_user + " and id_place = " + id_place + " and id_temperature = " + id_temperature + " and id_companion = " + id_companion + " and id_farmiliarity = " + id_farmiliarity + " and id_mood = " + id_mood + " and id_budget = " + id_budget + " and id_weather = " + id_weather + " and id_travel_length = " + id_travel_length), "Connection String");
            if (dt.Rows.Count != 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
                return -1;
        }
    }
}