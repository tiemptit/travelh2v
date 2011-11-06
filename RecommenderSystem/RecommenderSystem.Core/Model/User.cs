using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RecommenderSystem.Core.Model
{
    class User
    {
        public int id;
        public User (int id)
        {
            this.id = id;
        }
        public static List<User> GetUsersRateItem(Item item, DataTable data)
        {
            List<User> result = new List<User>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (item.id == Convert.ToInt32(data.Rows[i][2]))
                {
                    result.Add(new User(Convert.ToInt32(data.Rows[i][1])));
                }
            }

            return result;
        }
    }
}
