using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.Model
{
    public class Rating : IEquatable<Rating>
    {
        /*
         * Properties
         */
        public int id {get; set;}
        public int id_user { get; set; }
        public int id_place { get; set; }
        public int id_temperature { get; set; }
        public int id_companion { get; set; }
        public int id_farmiliarity { get; set; }
        public int id_mood { get; set; }
        public int id_budget { get; set; }
        public int id_weather { get; set; }
        public int id_travel_length { get; set; }
        public DateTime time { get; set; }
        public double rating { get; set; }
        /*
         * Constructors
         */
        public Rating(int id)
        {
            GetDataById(id);
        }
        /*
         * IEquatable
         */
        public bool Equals(Rating other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return id.Equals(other.id);
        }
        public override int GetHashCode()
        {
            int hashRatingId = id == null ? 0 : id.GetHashCode();

            return hashRatingId; // ^
        }
        /*
         * Methods
         */

        public void GetDataById(int id)
        {
            DataRow data = DbHelper.RunScriptsWithTable(string.Format("select * from real_ratings where id = " + id)).Rows[0];
            this.id = id;
            this.id_user = Convert.ToInt32(data[1]);
            this.id_place = Convert.ToInt32(data[2]);
            this.id_temperature = Convert.ToInt32(data[3]);
            this.id_companion = Convert.ToInt32(data[4]);
            this.id_farmiliarity = Convert.ToInt32(data[5]);
            this.id_mood = Convert.ToInt32(data[6]);
            this.id_budget = Convert.ToInt32(data[7]);
            this.id_weather = Convert.ToInt32(data[8]);
            this.id_travel_length = Convert.ToInt32(data[9]);
            this.time = Convert.ToDateTime(data[10]);
            this.rating = Convert.ToDouble(data[11]);
        }

        /*
         * Static Methods
         */

        public static DataTable GetFullSegment()
        {
            return DbHelper.RunScriptsWithTable(string.Format("select * from real_ratings"));
        }

        public static List<Rating> GetRatingListByUser(User user, DataTable segment)
        {
            List<Rating> result = new List<Rating>();
            for (int i = 0; i < segment.Rows.Count; i++)
            {
                if (user.id == Convert.ToInt32(segment.Rows[i][1]))
                {
                    result.Add(new Rating(Convert.ToInt32(segment.Rows[i][0])));
                }
            }
            return result;
        }

        public static List<Rating> GetRatingListForItem(Item item, DataTable segment)
        {
            List<Rating> result = new List<Rating>();
            for (int i = 0; i < segment.Rows.Count; i++)
            {
                if (item.id == Convert.ToInt32(segment.Rows[i][2]))
                {
                    result.Add(new Rating(Convert.ToInt32(segment.Rows[i][0])));
                }
            }
            return result;
        }

        public static List<Rating> GetRatingListByUserAndForItem(User user, Item item, DataTable segment)
        {
            List<Rating> result = new List<Rating>();
            for (int i = 0; i < segment.Rows.Count; i++)
            {
                if (user.id == Convert.ToInt32(segment.Rows[i][1]) && item.id == Convert.ToInt32(segment.Rows[i][2]))
                {
                    result.Add(new Rating(Convert.ToInt32(segment.Rows[i][0])));
                }
            }
            return result;
        }
        
    }
}
