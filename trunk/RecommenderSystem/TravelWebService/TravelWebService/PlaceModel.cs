using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace TravelWebService
{
    public class PlaceModel
    {
        public int id { get; set; }
        public PlaceCategoryModel place_category_obj { get; set; }
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

        public static List<PlaceModel> GetAllPlaces()
        {
            return GetAllFromDataTable(DbHelper.RunScriptsWithTable(string.Format("Select * from places order by name")));
        }

        public static List<PlaceModel> GetAllFromDataTable(DataTable data)
        {
            List<PlaceModel> result = new List<PlaceModel>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                PlaceModel place = new PlaceModel();
                place.id = Convert.ToInt32(data.Rows[i][0]);
                PlaceCategoryModel place_cat = new PlaceCategoryModel();
                place_cat.GetDataById(Convert.ToInt32(data.Rows[i][1]));
                place.place_category_obj = place_cat;
                place.name = Convert.ToString(data.Rows[i][2]);
                place.imgurl = Convert.ToString(data.Rows[i][3]);
                place.lat = Convert.ToDouble(data.Rows[i][4]);
                place.lng = Convert.ToDouble(data.Rows[i][5]);
                place.house_number = Convert.ToString(data.Rows[i][6]);
                place.street = Convert.ToString(data.Rows[i][7]);
                place.ward = Convert.ToString(data.Rows[i][8]);
                place.district = Convert.ToString(data.Rows[i][9]);
                place.city = Convert.ToString(data.Rows[i][10]);
                place.province = Convert.ToString(data.Rows[i][11]);
                place.country = Convert.ToString(data.Rows[i][12]);
                place.phone_number = Convert.ToString(data.Rows[i][13]);
                place.email = Convert.ToString(data.Rows[i][14]);
                place.website = Convert.ToString(data.Rows[i][15]);
                place.history = Convert.ToString(data.Rows[i][16]);
                place.details = Convert.ToString(data.Rows[i][17]);
                place.sources = Convert.ToString(data.Rows[i][18]);
                place.general_rating = Convert.ToInt32(data.Rows[i][19] == DBNull.Value ? 0 : data.Rows[i][19]);
                place.general_count_rating = Convert.ToInt32(data.Rows[i][20] == DBNull.Value ? 0 : data.Rows[i][20]);
                place.general_sum_rating = Convert.ToInt32(data.Rows[i][21] == DBNull.Value ? 0 : data.Rows[i][21]);

                result.Add(place);

            }
            return result;
        }
    }
}