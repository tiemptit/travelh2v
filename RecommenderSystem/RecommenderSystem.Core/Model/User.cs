using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;
using RecommenderSystem.Core.RS_Core;

namespace RecommenderSystem.Core.Model
{
    public class User
    {
        /*
         * Properties
         */
        public int id { get; set; }
        public string email { get; set; }
        public DateTime birthday { get; set; }
        public int gender { get; set; }
        /*
         * Constructors
         */
        public User (int id)
        {
            GetDataById(id);
        }
        public User(string email)
        {
            DataRow data = DbHelper.RunScriptsWithTable(string.Format("select * from dim_user where email = " + email), "Data Warehouse").Rows[0];
            this.id = Convert.ToInt32(data[0]);
            this.email = Convert.ToString(data[1]);
            //this.birthday = Convert.ToDateTime(data[2]);
            //this.gender = Convert.ToInt32(data[3]);
        }
        /*
         * Methods
         */
        public void GetDataById(int id)
        {
            DataRow data = DbHelper.RunScriptsWithTable(string.Format("select * from dim_user where user_key = " + id), "Data Warehouse").Rows[0];
            this.id = id;
            this.email = Convert.ToString(data[1]);
            this.birthday = Convert.ToDateTime(data[2]);
            this.gender = Convert.ToInt32(data[3]);
        }
        /*public double GetAverageRating(DataTable segment)
        {
            List<Rating> ratings = Rating.GetRatingListByUser(this, segment);
            double sum = 0;
            foreach (Rating rating in ratings)
            {
                sum += rating.rating;
            }
            return sum / ratings.Count;
        }*/
        /*
         * Static Methods
         */
        /*public static List<User> GetUsersRateItem(Item item, DataTable segment)
        {
            List<User> result = new List<User>();
            for (int i = 0; i < segment.Rows.Count; i++)
            {
                if (item.id == Convert.ToInt32(segment.Rows[i][2]))
                {
                    result.Add(new User(Convert.ToInt32(segment.Rows[i][1])));
                }
            }

            return result;
        }
        public static List<int> GetUserIdsRatedItemId(int item_id, Segment segment)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < segment.user_id.Length; i++)
                if (segment.data[i, item_id] != 0)
                    result.Add(i);
            return result;
        }*/
    }
}
