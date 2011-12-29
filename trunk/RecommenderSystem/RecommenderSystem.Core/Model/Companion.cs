using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.Model
{
    public class Companion
    {
        /*
         * Properties
         */
        public int id { get; set; }
        public string description { get; set; }

        public Companion()
        { 
            
        }
        
        public Companion(int id)
        {
            this.id = id;
        }
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
        public static List<Companion> GetAllData()
        {
            DataTable data = DbHelper.RunScriptsWithTable(string.Format("select * from dim_companion"), "Data Warehouse");
            List<Companion> result = new List<Companion>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                Companion obj = new Companion();
                obj.id = Convert.ToInt32(data.Rows[i][0]);
                obj.description = Convert.ToString(data.Rows[i][1]);

                result.Add(obj);
            }
            //Add Null record
            result.Add(new Companion());
            return result;
        }
    }
}
