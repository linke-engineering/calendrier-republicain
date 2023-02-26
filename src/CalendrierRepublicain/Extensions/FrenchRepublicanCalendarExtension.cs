namespace Sinistrius.CalendrierRepublicain.Extensions
{

    /// <summary>
    /// Extends the <see cref="FrenchRepublicanCalendar"/> class.
    /// </summary>
    internal static class FrenchRepublicanCalendarExtension
    {

        #region Date/Time Part Validation

        /// <summary>
        /// Validates an era in the Republican calendar.
        /// </summary>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the era is invalid.</exception>
        internal static void ValidateEra(this FrenchRepublicanCalendar _, int era) 
        {
            if (era != 1)
            {
                throw new ArgumentOutOfRangeException(nameof(era));
            }
        }


        /// <summary>
        /// Validates a year in the Republican calendar.
        /// </summary>
        /// <param name="calendar">The Republican calendar.</param>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the year is invalid.</exception>
        internal static void ValidateYear(this FrenchRepublicanCalendar calendar, int year, int era = 1)
        {
            calendar.ValidateEra(era);

            if (!IsInRange(year, 1, Constants.LastRepublicanYear))
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }
        }


        /// <summary>
        /// Validates a month in the Republican calendar.
        /// </summary>
        /// <param name="calendar">The Republican calendar.</param>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">An integer that represents the month.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the month is invalid.</exception>
        internal static void ValidateMonth(this FrenchRepublicanCalendar calendar, int year, int month, int era = 1)
        {
            calendar.ValidateYear(year, era);
            
            if // The complementary days at the end of each year are considered as 13th month:
               (!IsInRange(month, 1, 13) ||
               // The Republican calender was abolished in the 4th month of the 14th year:
               (year == Constants.LastRepublicanYear && month > Constants.LastRepublicanMonth))
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }
        }


        /// <summary>
        /// Validates a day in the Republican calendar.
        /// </summary>
        /// <param name="calendar">The Republican calendar.</param>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">An integer that represents the month.</param>
        /// <param name="day">An integer that represents the day.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the day is invalid.</exception>
        internal static void ValidateDay(this FrenchRepublicanCalendar calendar, int year, int month, int day, int era = 1)
        {
            calendar.ValidateMonth(year, month, era);

            if // Each month has 30 days:
               (!IsInRange(day, 1, 30) ||
               // The Republican calender was abolished on the 10th day of the 4th month of the 14th year:
               (year == Constants.LastRepublicanYear && month == Constants.LastRepublicanMonth && day > 10) ||
               // In a non-leap year there are 5 complementary days in the assumed 13th month:
               (!calendar.IsLeapYear(year) && month == 13 && day > 5) ||
               // In a leap year there are 6 complementary days in the assumed 13th month:
               (calendar.IsLeapMonth(year, month) && day > 6))
            {
                throw new ArgumentOutOfRangeException(nameof(day));
            }
        }


        /// <summary>
        /// Validates a time value.
        /// </summary>
        /// <param name="hour">An integer that represents the hour.</param>
        /// <param name="minute">An integer that represents the minute.</param>
        /// <param name="second">An integer that represents the second.</param>
        /// <param name="millisecond">An integer that represents the millisecond.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the time value is invalid.</exception>
        internal static void ValidateTime(this FrenchRepublicanCalendar _, int hour, int minute, int second, int millisecond) 
        {
            if (!IsInRange(hour, 0, 23))
            {
                throw new ArgumentOutOfRangeException(nameof(hour));
            }

            if (!IsInRange(minute, 0, 59))
            {
                throw new ArgumentOutOfRangeException(nameof(minute));
            }

            if (!IsInRange(second, 0, 59))
            {
                throw new ArgumentOutOfRangeException(nameof(second));
            }

            if (!IsInRange(millisecond, 0, 999))
            {
                throw new ArgumentOutOfRangeException(nameof(millisecond));
            }
        }

        #endregion


        #region Helper

        /// <summary>
        /// Determines whether a specified value is inside the specified range.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns></returns>
        private static bool IsInRange(int value, int lowerBound, int upperBound)
        {
            return value >= lowerBound && value <= upperBound;
        }

        #endregion

    }

}
