using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RecommenderSystem.Core.Model;
using Microsoft.AnalysisServices.AdomdClient;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.RS_Core
{
    public class Segment //: IEquatable<Item>
    {
        /*
         * Properties
         */
        public int id { get; set; }
        public Budget budget { get; set; }
        public Companion companion { get; set; }
        public Familiarity familiarity { get; set; }
        public Mood mood { get; set; }
        public Temperature temperature { get; set; }
        public TravelLength travelLength { get; set; }
        public Weather weather { get; set; }
        public double Performance {get; set;}
        //public DataTable data { get; set; }
        public Matrix data { get; set; }

        /*
         * Methods
         */
        public void GetData()
        {
            /*string sql = "pr_getSegment " + budget.id
                    + ", " + companion.id + ", " + familiarity.id + ", " + mood.id + ", " + temperature.id
                    + ", " + travelLength.id + ", " + weather.id;*/

            /*string sql = "pr_getSegment " + 0
                    + ", " + companion.id + ", " + familiarity.id + ", " + mood.id + ", " + 0
                    + ", " + 0 + ", " + 0;
            DataTable result = DbHelper.RunScriptsWithTable(string.Format(sql));*/

            /*
             * OLAP CUBE
             */

            string mdx = "with member Measures.[Avg_Ratings] as "
                        + "([Measures].[Sum_Ratings]/[Measures].[Count_Ratings]) "
                        + "select "
                        + "[Dim Place].[Place Key].Members on columns, "
                        + "[Dim User].[User Key].Members on rows "
                        + "from [Travel H2V DW]"
                        + "where (Measures.[Avg_Ratings]";
            /*if (budget.id != 0)
                mdx += ", [Dim Budget].[Budget Key].&[" + budget.id + "]";*/
            if (companion.id != 0)
                mdx += ", [Dim Companion].[Companion Key].&[" + companion.id + "]";
            if (familiarity.id != 0)
                mdx += ", [Dim Familiarity].[Familiarity Key].&[" + familiarity.id + "]";
            if (mood.id != 0)
                mdx += ", [Dim Mood].[Mood Key].&[" + mood.id + "]";
            /*if (temperature.id != 0)
                mdx += ", [Dim Temperature].[Temperature Key].&[" + temperature.id + "]";
            if (travelLength.id != 0)
                mdx += ", [Dim Travel Length].[Travellength Key].&[" + travelLength.id + "]";
            if (weather.id != 0)
                mdx += ", [Dim Weather].[Weather Key].&[" + weather.id + "]";*/
                        
            mdx += ")";

            DataTable result = DbHelper.RunMDXWithDataTable(mdx);
            this.data = new Matrix(result.Rows.Count - 1, result.Columns.Count - 2);
            for (int i = 0; i < data.rows; i++)
                for (int j = 0; j < data.cols; j++)
                    data[i, j] = Convert.ToDouble(result.Rows[i + 1][j + 2] == "" ? 0 : result.Rows[i + 1][j + 2]);
        }

        public bool IsChildOf(Segment other)
        {
            return (budget.id == other.budget.id ||  other.budget.id == 0)
                && (companion.id == other.companion.id || other.companion.id == 0)
                && (familiarity.id == other.familiarity.id || other.familiarity.id == 0)
                && (mood.id == other.mood.id || other.mood.id == 0)
                && (temperature.id == other.temperature.id || other.temperature.id == 0)
                && (travelLength.id == other.travelLength.id || other.travelLength.id == 0)
                && (weather.id == other.weather.id || other.weather.id == 0);
        }
        /*
         * Static Methods
         */
        public static List<Segment> GetAllSegment()
        {
            List<Segment> result = new List<Segment>();
            //foreach(Budget budget in Budget.GetAllData())
                foreach(Companion companion in Companion.GetAllData())
                    foreach(Familiarity familiarity in Familiarity.GetAllData())
                        foreach(Mood mood in Mood.GetAllData())
                            //foreach(Temperature temperature in Temperature.GetAllData())
                                //foreach(TravelLength travelLength in TravelLength.GetAllData())
                                    //foreach(Weather weather in Weather.GetAllData())
                                    {
                                        Segment segment = new Segment();
                                        //segment.budget = budget;
                                        segment.companion = companion;
                                        segment.familiarity = familiarity;
                                        segment.mood = mood;
                                        //segment.temperature = temperature;
                                        //segment.travelLength = travelLength;
                                        //segment.weather = weather;
                                        segment.GetData();
                                        if (segment.data.CountCells() > 10)
                                            result.Add(segment);
                                    }
            return result;
        }

        public static Segment[] GetCandidates()
        {
            DataTable data = DbHelper.RunScriptsWithTable(string.Format("select * from segments order by performance desc"));
            Segment[] candidates = new Segment[data.Rows.Count];
            for (int i = 0; i < data.Rows.Count; i++)
            {
                Segment obj = new Segment();
                obj.id = Convert.ToInt32(data.Rows[i][0]);
                obj.budget = new Budget(Convert.ToInt32(data.Rows[i][1]));
                obj.companion = new Companion(Convert.ToInt32(data.Rows[i][2]));
                obj.familiarity = new Familiarity(Convert.ToInt32(data.Rows[i][3]));
                obj.mood = new Mood(Convert.ToInt32(data.Rows[i][4]));
                obj.temperature = new Temperature(Convert.ToInt32(data.Rows[i][5]));
                obj.travelLength = new TravelLength(Convert.ToInt32(data.Rows[i][6]));
                obj.weather = new Weather(Convert.ToInt32(data.Rows[i][7]));
                obj.Performance = Convert.ToDouble(data.Rows[i][8]);

                candidates[i] = obj;
            }

            return candidates;
            
        }

        /*
         * IEquatable
         */
        public bool Equals(Segment other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return budget.id.Equals(other.budget.id) 
                && companion.id.Equals(other.companion.id)
                && familiarity.id.Equals(other.familiarity.id)
                && mood.id.Equals(other.mood.id)
                && temperature.id.Equals(other.temperature.id)
                && travelLength.id.Equals(other.travelLength.id)
                && weather.id.Equals(other.weather.id);
        }
        public override int GetHashCode()
        {   
            return budget.GetHashCode() ^ companion.GetHashCode() ^ familiarity.GetHashCode()
                    ^ mood.GetHashCode() ^ temperature.GetHashCode()
                    ^ travelLength.GetHashCode() ^ weather.GetHashCode();
        }
    }
}
