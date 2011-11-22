using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.Model
{
    public class TravelLength
    {
        /*
         * Properties
         */
        public int id { get; set; }
        public string description { get; set; }
        /*
         * Override Methods
         */
        public override int GetHashCode()
        {
            int hashDescription = description == null ? 0 : description.GetHashCode();
            int hashId = id.GetHashCode();

            return hashId ^ hashDescription;
        }
        /*
         * Static Methods
         */
        public static List<TravelLength> GetAllData()
        {
            DataTable data = DbHelper.RunScriptsWithTable(string.Format("select * from TravelLength"));
            List<TravelLength> result = new List<TravelLength>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                TravelLength obj = new TravelLength();
                obj.id = Convert.ToInt32(data.Rows[i][0]);
                obj.description = Convert.ToString(data.Rows[i][1]);

                result.Add(obj);
            }
            return result;
        }
    }
}
