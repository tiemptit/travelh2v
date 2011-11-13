using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.Model
{
    public class Item : IEquatable<Item>
    {
        /*
         * Properties
         */
        public int id { get; set; }
        public int id_place_category { get; set; }
        public string name { get; set; }
        public string imgurl { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int house_number { get; set; }
        public string street { get; set; }
        public string ward { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string history { get; set; }
        public string details { get; set; }
        public string sources { get; set; }
        public double general_rating { get; set; }
        public double general_count_rating { get; set; }
        public double general_sum_rating { get; set; }
        /*
         * Constructor
         */
        public Item(int id)
        {
            GetDataById(id);
        }
        /*
         * IEquatable
         */
        public bool Equals(Item other)
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
         * Method
         */

        public void GetDataById(int id)
        {
            this.id = id;
            //To be continue
        }

        /*
         * Static Method
         */
        public static List<Item> GetItemRatedByUser(User user)
        {
            DataTable data = DbHelper.RunScriptsWithTable(string.Format("select * from real_ratings where id_user = " + user.id));

            List<Item> result = new List<Item>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (user.id == Convert.ToInt32(data.Rows[i][1]))
                {
                    result.Add(new Item(Convert.ToInt32(data.Rows[i][2])));
                }
            }
            return result;
        }

        public static List<Item> GetItemRatedByUser(User user, DataTable segment)
        {
            List<Item> result = new List<Item>();
            for (int i = 0; i < segment.Rows.Count; i++)
            {
                if (user.id == Convert.ToInt32(segment.Rows[i][1]))
                { 
                    result.Add(new Item(Convert.ToInt32(segment.Rows[i][2])));
                }
            }
            return result;
        }

    }
}
