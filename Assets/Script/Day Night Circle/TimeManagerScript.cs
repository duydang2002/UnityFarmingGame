
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DateTimeNameSpace
{
    public class TimeManagerScript : MonoBehaviour
    {
        [Header("Date & Time Settings")]
        [Range(1, 30)]
        public int dateInMonth;
        [Range(1, 4)]
        public static int season;
        [Range(1, 99)]
        public int year;
        [Range(0, 24)]
        public int hour;
        [Range(0, 60)]
        public int minute;

        private DateTime dateTime;

        [Header("Tick Settings")]
        public int tickSecondsIncrease = 10;
        public float timeBetweenTicks = 1;
        private float currentTimeBetweenTicks = 0;

        public static Action<DateTime> OnDateTimeChanged;

        private void Awake()
        {

            dateTime = new DateTime(1, 0, 1, 12, 0);


            Debug.Log($"Summer Solstice :  {dateTime.SummerSolstice(4)}");
            Debug.Log($"Starting of a season :  {dateTime.StartOfSeason(1, 3)}");
        }

        // Start is called before the first frame update
        void Start()
        {
            OnDateTimeChanged?.Invoke(dateTime);
        }

        // Update is called once per frame
        void Update()
        {
            currentTimeBetweenTicks += Time.deltaTime;
            if (currentTimeBetweenTicks >= timeBetweenTicks)
            {
                currentTimeBetweenTicks = 0;
                Tick();
            }
        }


        void Tick()
        {
            //Debug.Log("Ticked");
            AdvanceTime();
        }

        void AdvanceTime()
        {
            dateTime.AdvanceMinutes(tickSecondsIncrease);

            OnDateTimeChanged?.Invoke(dateTime);
        }
    }

    [System.Serializable]
    public struct DateTime
    {
        #region Fields;
        private Days day;
        private int date;

        private int year;
        private int hour;
        private int minute;

        private Season season;

        private int totalNumDays;
        private int totalNumWeeks;
        #endregion

        #region Properties
        public Days Day => day;
        /* public int Date {
         * get 
         *  {
         *      return date;
         *  }
         * }
         */
        public int Date => date;
        public int Hour => hour;
        public int Minute => minute;
        public Season Season => season;
        public int Year => year;
        public int TotalNumDays => totalNumDays;
        public int TotalNumWeeks => totalNumWeeks;
        public int CurrentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;
        #endregion

        #region Constructors
        public DateTime(int date, int season, int year, int hour, int minutes)
        {
            this.day = (Days)(date % 7);
            if (day == 0)
            {
                day = (Days)7;
            }
            this.date = date;
            this.season = (Season)season;
            this.year = year;
            this.hour = hour;
            this.minute = minutes;

            totalNumDays = date + (28 * (int)this.season * (112 + (year - 1)));

            totalNumWeeks = 1 + totalNumDays / 7;
        }
        #endregion

        #region Time Advancement
        public void AdvanceMinutes(int SecondsToAdvanceBy)
        {
            if (minute + SecondsToAdvanceBy >= 60)
            {
                minute = (minute + SecondsToAdvanceBy) % 60;
                AdvanceHour();
            }
            else
            {
                minute += SecondsToAdvanceBy;
            }
        }

        private void AdvanceHour()
        {
            if (hour == 23)
            {
                hour = 0;
                AdvanceDay();
            }
            else
            {
                hour++;
            }
        }

        private void AdvanceDay()
        {
            if (day + 1 > (Days)7)
            {
                day = (Days)1;
                totalNumWeeks++;
            }
            else
            {
                day++;
            }

            date++;

            if (date % 29 == 0)
            {
                AdvanceSeason();
                date = 1;
            }
            totalNumDays++;

        }


        private void AdvanceSeason()
        {
            if (Season == Season.Winter)
            {
                season = Season.Spring;
                AdvanceYear();
            }
            else season++;
        }

        private void AdvanceYear()
        {
            date = 1;
            year++;
        }

        #endregion

        #region Bool Checks
        public bool IsNight()
        {
            return hour > 18 || hour < 6;
        }
        public bool IsMorning()
        {
            return hour >= 6 && hour <= 12;
        }
        public bool IsAfternoon()
        {
            return hour > 12 && hour < 18;
        }
        public bool IsWeekend()
        {
            return day > Days.Fri ? true : false;
        }
        public bool IsParticularDay(Days _day)
        {
            return day == _day;
        }
        #endregion

        #region Key Dates
        public DateTime NewYearDay(int year)
        {
            if (year == 0) year = 1;
            return new DateTime(1, 0, year, 6, 0);
        }
        public DateTime SummerSolstice(int year)
        {
            if (year == 0) year = 1;
            return new DateTime(28, 1, year, 6, 0);
        }
        public DateTime PumkinMarvest(int year)
        {
            if (year == 0) year = 1;
            return new DateTime(28, 2, year, 6, 0);
        }

        #endregion

        #region Start Of Season

        public DateTime StartOfSeason(int season, int year)
        {
            return new DateTime(date, season, year, 6, 0);
        }
        #endregion
        #region To Strings
        public override string ToString()
        {
            return $"Date {DateToString()} Season: {season} Time: {TimeToString()}" +
                $"\nTotal Days: {TotalNumDays} | Total Weeks: {totalNumWeeks}";
        }
        public string DateToString()
        {
            return $"{Day} {Date} {Year.ToString("D2")}";
        }
        public string TimeToString()
        {
            int adjustedHour = 0;

            if (hour == 0)
            {
                adjustedHour = 12;
            }
            else if (hour == 24)
            {
                adjustedHour = 12;
            }
            else if (hour >= 13)
            {
                adjustedHour = hour - 12;
            }
            else
            {
                adjustedHour = hour;
            }
            string AmPm = hour == 0 || hour < 12 ? "Am" : "PM";
            return $"{adjustedHour.ToString("D2")}: {minute.ToString("D2")} {AmPm}";
        }
        #endregion
    }
    [System.Serializable]
    public enum Days
    {
        NULL = 0,
        Mon = 1,
        Tue = 2,
        Wed = 3,
        Thu = 4,
        Fri = 5,
        Sat = 6,
        Sun = 7
    }

    [System.Serializable]
    public enum Season
    {
        Spring = 0,
        Summer = 1,
        Autumn = 2,
        Winter = 3,
    }
}