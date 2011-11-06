using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RecommenderSystem.Core.Model
{
    class Item : IEquatable<Item>
    {
        /*
         * Properties
         */
        public int id;
        /*
         * Constructor
         */
        public Item(int id)
        {
            this.id = id;
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
        /*
         * Static Method
         */
        public static List<Item> GetItemRatedByUser(User user, DataTable data)
        {
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
    }
}
