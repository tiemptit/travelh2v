using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RecommenderSystem.Core.Model
{
    class Rating : IEquatable<Rating>
    {
        /*
         * Attributes
         */
        public int id {get; set;}
        /*
         * Constructors
         */
        public Rating(int id)
        {
            this.id = id;
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
         * Static Methods
         */
        public static List<Rating> GetRatingListByUser(User user, DataTable data)
        {
            List<Rating> result = new List<Rating>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (user.id == Convert.ToInt32(data.Rows[i][1]))
                { 
                    result.Add(new Rating(Convert.ToInt32(data.Rows[i][0])));
                }
            }
            return result;
        }

        public static List<Rating> GetRatingListByUser(User user, DataTable data)
        {
            List<Rating> result = new List<Rating>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (user.id == Convert.ToInt32(data.Rows[i][1]))
                {
                    result.Add(new Rating(Convert.ToInt32(data.Rows[i][0])));
                }
            }
            return result;
        }

        public static List<Rating> GetRatingListByUserAndForItem(User user, Item item, DataTable data)
        {
            List<Rating> result = new List<Rating>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (user.id == Convert.ToInt32(data.Rows[i][1]) && item.id == Convert.ToInt32(data.Rows[i][2]))
                {
                    result.Add(new Rating(Convert.ToInt32(data.Rows[i][0])));
                }
            }
            return result;
        }
        
    }
}
