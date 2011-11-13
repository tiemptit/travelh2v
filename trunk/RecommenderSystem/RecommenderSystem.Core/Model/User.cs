using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.Model
{
    public class User
    {
        /*
         * Properties
         */
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int year_of_birth { get; set; }
        public int gender { get; set; }
        /*
         * Constructors
         */
        public User (int id)
        {
            GetDataById(id);
        }
        /*
         * Methods
         */
        public void GetDataById(int id)
        {
            DataRow data = DbHelper.RunScriptsWithTable(string.Format("select * from users where id = " + id)).Rows[0];
            this.id = id;
            this.email = Convert.ToString(data[1]);
            this.password = Convert.ToString(data[2]);
            this.year_of_birth = Convert.ToInt32(data[3]);
            this.gender = Convert.ToInt32(data[4]);
        }
        public double GetAverageRating(DataTable segment)
        {
            List<Rating> ratings = Rating.GetRatingListByUser(this, segment);
            double sum = 0;
            foreach (Rating rating in ratings)
            {
                sum += rating.rating;
            }
            return sum / ratings.Count;
        }
        /*
         * Static Methods
         */
        public static List<User> GetUsersRateItem(Item item, DataTable segment)
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
    }
}
