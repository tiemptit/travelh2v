using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;
using RecommenderSystem.Core.RS_Core;

namespace RecommenderSystem.Core.Model
{
    public class Item : IEquatable<Item>
    {
        /*
         * Properties
         */
        public int id { get; set; }
        public PlaceCategoryModel place_category { get; set; }
        public string name { get; set; }
        public string imgurl { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string house_number { get; set; }
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
        public Item() { }
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
            DataRow data = DbHelper.RunScriptsWithTable(string.Format("select * from places where id = " + id)).Rows[0];
            this.id = id;
            this.place_category = new PlaceCategoryModel();
            this.place_category.GetDataById(Convert.ToInt32(data[1]));
            this.name = Convert.ToString(data[2]);
            this.imgurl = Convert.ToString(data[3]);
            this.lat = Convert.ToDouble(data[4]);
            this.lng = Convert.ToDouble(data[5]);
            this.house_number = Convert.ToString(data[6]);
            this.street = Convert.ToString(data[7]);
            this.ward = Convert.ToString(data[8]);
            this.district = Convert.ToString(data[9]);
            this.city = Convert.ToString(data[10]);
            this.province = Convert.ToString(data[11]);
            this.country = Convert.ToString(data[12]);
            this.phone_number = Convert.ToString(data[13]);
            this.email = Convert.ToString(data[14]);
            this.website = Convert.ToString(data[15]);
            this.history = Convert.ToString(data[16]);
            this.details = Convert.ToString(data[17]);
            this.sources = Convert.ToString(data[18]);
            this.general_rating = Convert.ToInt32(data[19] == DBNull.Value ? 0 : data[19]);
            this.general_count_rating = Convert.ToInt32(data[20] == DBNull.Value ? 0 : data[20]);
            this.general_sum_rating = Convert.ToInt32(data[21] == DBNull.Value ? 0 : data[21]);
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

        public static List<Item> GetItemsIn(DataTable segment)
        {
            List<Item> result = new List<Item>();
            for (int i = 0; i < segment.Rows.Count; i++)
            {
                Item obj = new Item(Convert.ToInt32(segment.Rows[i][2]));
                bool flag = false;
                foreach (Item item in result)
                {
                    if (obj.id == item.id)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    result.Add(obj);
                }
            }
            return result;
        }

    }
}
