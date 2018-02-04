using System;
using static System.Math;

namespace Infrastructure.CrossCutting.Date
{
    /// <summary>
    /// Julian datetime class
    /// </summary>
    public static class JulianDate
    {
        /// <summary>
        /// Convert Julian day to centuries since J2000.0
        /// </summary>
        /// <param name="dblJulianDay">Number of Julian centuries since J2000.0</param>
        /// <returns>Julian Day corresponding to the centuries.</returns>
        public static double CalculateJulianCenturies(double julianDay)
        {
            double julianCent = (julianDay - 2451545) / 36525;
            return (double.IsNaN(julianCent) ? 0.1 : julianCent);
        }

        /// <summary>
        /// Julian day from calendar day
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>The Julian day corresponding to the date</returns>
        public static double CalculateJulianDay(System.DateTime date)
        {
            int intYear = date.Year;
            int intMonth = date.Month;
            int intDay = date.Day;
            double dblA = 0;
            double dblB = 0;

            if (intMonth <= 2)
            {
                intYear = intYear - 1;
                intMonth = intMonth + 12;
            }

            dblA = Floor((double)(date.Year / 100));
            dblB = 2 - dblA + Floor(Convert.ToDouble(dblA / 4));

            return Floor(365.25 * (intYear + 4716)) + Floor(30.6001 * (intMonth + 1)) + intDay + dblB - 1524.5;
        }

        /// <summary>Convert centuries since J2000.0 to Julian Day</summary>
        /// <param name="julianCent">Number of Julian centuries since J2000.0</param>
        /// <returns>Julian Day corresponding to the centuries</returns>
        public static double CalculateJulianDayFromJulianCentury(double julianCent)
        {
            return julianCent * 36525.0 + 2451545.0;
        }
    }
}
