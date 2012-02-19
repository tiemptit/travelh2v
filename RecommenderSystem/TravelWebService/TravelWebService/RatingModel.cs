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
        public int id_companion { get; set; }
        public int id_budget { get; set; }
        public int id_weather { get; set; }
        public string time { get; set; }
        public float rating { get; set; }

        // insert a rate into DB
        public bool Rate()
        {
            // if user not register, auto register with the email
            int id_user = checkIfUserExisted();
            if (id_user == -1)
            {
                DbHelper.RunScripts(string.Format("insert into users values ('" + username + "','1989-03-01',1)"), "Connection String");
                id_user = checkIfUserExisted();
                string sql = "insert into real_ratings values (" + id_user + "," + id_place + "," + (id_companion == 0 ? "NULL" : id_companion.ToString()) + "," + (id_budget == 0 ? "NULL" : id_companion.ToString()) + "," + (id_weather == 0 ? "NULL" : id_weather.ToString()) + ",'" + (time == "0" ? "NULL" : time) + "'," + rating + ")";

                return DbHelper.RunScripts(string.Format("insert into real_ratings values (" + id_user + "," + id_place + "," + (id_companion == 0 ? "NULL" : id_companion.ToString()) + "," + (id_budget == 0 ? "NULL" : id_budget.ToString()) + "," + (id_weather == 0 ? "NULL" : id_weather.ToString()) + "," + (time == "0" ? "NULL" : "'" + time + "'") + "," + rating + ")"), "Connection String");
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
                    //return DbHelper.RunScripts(string.Format("insert into real_ratings values (" + id_user + "," + id_place + "," + id_companion + "," + id_budget + "," + id_weather + ",'" + time + "'," + rating + ")"), "Connection String");
                    string sql = "insert into real_ratings values (" + id_user + "," + id_place + "," + (id_companion == 0 ? "NULL" : id_companion.ToString()) + "," + (id_budget == 0 ? "NULL" : id_companion.ToString()) + "," + (id_weather == 0 ? "NULL" : id_weather.ToString()) + ",'" + (time == "0" ? "NULL" : time) + "'," + rating + ")";
                    return DbHelper.RunScripts(string.Format("insert into real_ratings values (" + id_user + "," + id_place + "," + (id_companion == 0 ? "NULL" : id_companion.ToString()) + "," + (id_budget == 0 ? "NULL" : id_budget.ToString()) + "," + (id_weather == 0 ? "NULL" : id_weather.ToString()) + "," + (time == "0" ? "NULL" : "'" + time + "'") + "," + rating + ")"), "Connection String");
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
            string sql = "select id from real_ratings where id_user=" + id_user + " and id_place = " + id_place + " and id_companion " + (id_companion == 0 ? "is null" : " = " + id_companion.ToString()) +  " and id_budget " + (id_budget == 0 ? " is null" : " = " + id_budget.ToString()) + " and id_weather" + (id_weather == 0 ? " is null" : " = " + id_weather.ToString()) + " and time " + (time == "0" ? " is null" : "= '" + time + "'");
            DataTable dt = DbHelper.RunScriptsWithTable(string.Format("select id from real_ratings where id_user=" + id_user + " and id_place = " + id_place + " and id_companion " + (id_companion == 0 ? "is null" : " = " + id_companion.ToString()) +  " and id_budget " + (id_budget == 0 ? " is null" : " = " + id_budget.ToString()) + " and id_weather" + (id_weather == 0 ? " is null" : " = " + id_weather.ToString()) + " and time " + (time == "0" ? " is null" : " = '" + time + "'")), "Connection String");
            if (dt.Rows.Count != 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
                return -1;
        }
    }
}