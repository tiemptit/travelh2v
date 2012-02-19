using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecommenderSystem.Core.Helper;

namespace RecommenderSystem.Core.Model
{
    public class Time
    {
        public enum Period_Of_Day
        {
            All = 0,
            Morning = 1,
            Afternoon = 2,
            Night = 3
        }

        public enum Period_Of_Week
        {
            All = 0,
            Weekday = 1,
            Weekend = 2
        }

        public enum Season
        {
            All = 0,
            Spring = 1,
            Summer = 2,
            Autumn = 3,
            Winter = 4
        }

        /*
         * Properties
         */
        public Period_Of_Day period_of_day { get; set; }
        public Period_Of_Week period_of_week { get; set; }
        public Season season { get; set; }

        public Time()
        {
            period_of_day = Period_Of_Day.All;
            period_of_week = Period_Of_Week.All;
            season = Season.All;
        }

        public Time(string pod, string pow, string ss)
        {
            if (pod == "Morning")
                period_of_day = Time.Period_Of_Day.Morning;
            else if (pod == "Afternoon")
                period_of_day = Time.Period_Of_Day.Afternoon;
            else if (pod == "Night")
                period_of_day = Time.Period_Of_Day.Night;
            else
                period_of_day = Time.Period_Of_Day.All;

            
            if (pow == "Weekday")
                period_of_week = Time.Period_Of_Week.Weekday;
            else if (pow == "Weekend")
                period_of_week = Time.Period_Of_Week.Weekend;
            else
                period_of_week = Time.Period_Of_Week.All;

            
            if (ss == "Spring")
                season = Time.Season.Spring;
            else if (ss == "Summer")
                season = Time.Season.Summer;
            else if (ss == "Autumn")
                season = Time.Season.Autumn;
            else if (ss == "Winter")
                season = Time.Season.Winter;
            else
                season = Time.Season.All;
        }

        public Time(Period_Of_Day period_of_day, Period_Of_Week period_of_week, Season season)
        {
            this.period_of_day = period_of_day;
            this.period_of_week = period_of_week;
            this.season = season;
        }

        public Time(DateTime dt)
        {
            if (dt.Hour >= 5 && dt.Hour <= 11)
                this.period_of_day = Period_Of_Day.Morning;
            else if (dt.Hour > 11 && dt.Hour <= 17)
                this.period_of_day = Period_Of_Day.Afternoon;
            else
                this.period_of_day = Period_Of_Day.Night;

            if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                this.period_of_week = Period_Of_Week.Weekend;
            else
                this.period_of_week = Period_Of_Week.Weekday;

            if (dt.Month >= 1 && dt.Month <= 3)
                this.season = Season.Spring;
            else if (dt.Month >= 4 && dt.Month <= 6)
                this.season = Season.Summer;
            else if (dt.Month >= 7 && dt.Month <= 9)
                this.season = Season.Autumn;
            else if (dt.Month >= 10 && dt.Month <= 12)
                this.season = Season.Winter;
        }

        public static List<Time> GetAll()
        {
            List<Time> result = new List<Time>();
            foreach (Period_Of_Day pod in Enum.GetValues(typeof(Period_Of_Day)))
            {
                foreach (Period_Of_Week pow in Enum.GetValues(typeof(Period_Of_Week)))
                {
                    foreach (Season ss in Enum.GetValues(typeof(Season)))
                    {
                        Time obj = new Time(pod, pow, ss);
                        result.Add(obj);
                    }
                }
            }
            return result;
        }
    }
}
