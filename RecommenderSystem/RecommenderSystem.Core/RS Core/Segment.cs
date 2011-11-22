using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecommenderSystem.Core.Model;
using Microsoft.AnalysisServices.AdomdClient;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.RS_Core
{
    class Segment //: IEquatable<Item>
    {
        /*
         * Properties
         */
        Budget budget {get; set;}
        Companion companion {get; set;}
        Familiarity familiarity {get; set;}
        Mood mood {get; set;}
        Temperature temperature {get; set;}
        TravelLength travelLength {get; set;}
        Weather weather { get; set; }
        CellSet data { get; set; }

        /*
         * Methods
         */
        public void GetData()
        {
            string mdx = "";
            CellSet result = DbHelper.RunMDX(mdx);

            this.data = result;
        }
        public bool IsContains(Segment other)
        {
            return !(budget.id != 0 && other.budget.id == 0)
                && !(companion.id != 0 && other.companion.id == 0)
                && !(familiarity.id != 0 && other.familiarity.id == 0)
                && !(mood.id != 0 && other.mood.id == 0)
                && !(temperature.id != 0 && other.temperature.id == 0)
                && !(travelLength.id != 0 && other.travelLength.id == 0)
                && !(weather.id != 0 && other.weather.id == 0);
        }

        public bool IsChildOf(Segment other)
        {
            return other.IsContains(this);
        }
        /*
         * Static Methods
         */
        public static List<Segment> GetAllSegment()
        {
            List<Segment> result = new List<Segment>();
            foreach(Budget budget in Budget.GetAllData())
                foreach(Companion companion in Companion.GetAllData())
                    foreach(Familiarity familiarity in Familiarity.GetAllData())
                        foreach(Mood mood in Mood.GetAllData())
                            foreach(Temperature temperature in Temperature.GetAllData())
                                foreach(TravelLength travelLength in TravelLength.GetAllData())
                                    foreach(Weather weather in Weather.GetAllData())
                                    {
                                        Segment segment = new Segment();
                                        segment.budget = budget;
                                        segment.companion = companion;
                                        segment.familiarity = familiarity;
                                        segment.mood = mood;
                                        segment.temperature = temperature;
                                        segment.travelLength = travelLength;
                                        segment.weather = weather;

                                        result.Add(segment);
                                    }
            return result;
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
