using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.Model
{
    public class PlaceCategoryModel
    {
        public int id { get; set; }
        public string place_category { get; set; }

        public void GetDataById(int id)
        {
            DataTable temp = DbHelper.RunScriptsWithTable(string.Format("Select place_category from place_categories where id = '" + id.ToString() + "'"), "Connection String");
            this.id = id;
            place_category = Convert.ToString(temp.Rows[0][0]);
        }

        public static List<PlaceCategoryModel> GetAllPlaceCat(DataTable data)
        {
            List<PlaceCategoryModel> result = new List<PlaceCategoryModel>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                PlaceCategoryModel place_cat = new PlaceCategoryModel();
                place_cat.id = Convert.ToInt32(data.Rows[i][0]);
                place_cat.place_category = Convert.ToString(data.Rows[i][1]);

                result.Add(place_cat);
            }

            return result;
        }
    }
}