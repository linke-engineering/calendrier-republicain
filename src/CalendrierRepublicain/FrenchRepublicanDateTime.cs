namespace LinkeEngineering.CalendrierRepublicain;


/// <summary>
/// Represents an instance in time expressed in the Republican calendar.
/// </summary>
internal class FrenchRepublicanDateTime : IFormattable
{

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FrenchRepublicanDateTime"/> class to the specified Republican year, month and day.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="day">An integer that represents the day.</param>
    /// <param name="era">An integer that represents the era.</param>
    internal FrenchRepublicanDateTime(int year, int month, int day, int era)
    {
        ValidateDay(year, month, day, era);

        Era = era;
        Year = year;
        Month = month;
        Day = day;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="FrenchRepublicanDateTime"/> class to the specified Republican year, month, day, hour, minute, second and millisecond.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="day">An integer that represents the day.</param>
    /// <param name="hour">An integer that represents the hour.</param>
    /// <param name="minute">An integer that represents the minute.</param>
    /// <param name="second">An integer that represents the second.</param>
    /// <param name="millisecond">An integer that represents the millisecond.</param>
    /// <param name="era">An integer that represents the era.</param>
    internal FrenchRepublicanDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era) : this(year, month, day, era)
    {
        ValidateTime(hour, minute, second, millisecond);

        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
        TimeOfDay = new TimeSpan(0, hour, minute, second, millisecond);
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="FrenchRepublicanDateTime"/> class to the specified Republican year, month, day and time.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="day">An integer that represents the day.</param>
    /// <param name="era">An integer that represents the era.</param>
    /// <param name="timeOfDay">The time of the day.</param>
    internal FrenchRepublicanDateTime(int year, int month, int day, TimeSpan timeOfDay, int era) : this(year, month, day, era)
    {
        ValidateTime(timeOfDay);

        TimeOfDay = timeOfDay;
        Hour = timeOfDay.Hours;
        Minute = timeOfDay.Minutes;
        Second = timeOfDay.Seconds;
        Millisecond = timeOfDay.Milliseconds;
    }

    #endregion


    #region Properties

    /// <summary>
    /// The era.
    /// </summary>
    internal int Era { get; }


    /// <summary>
    /// The year.
    /// </summary>
    internal int Year { get; }


    /// <summary>
    /// The month.
    /// </summary>
    internal int Month { get; }


    /// <summary>
    /// The day.
    /// </summary>
    internal int Day { get; }


    /// <summary>
    /// The hour.
    /// </summary>
    internal int Hour { get; }


    /// <summary>
    /// The minute.
    /// </summary>
    internal int Minute { get; }


    /// <summary>
    /// The second.
    /// </summary>
    internal int Second { get; }


    /// <summary>
    /// The millisecond.
    /// </summary>
    internal int Millisecond { get; }


    /// <summary>
    /// The time of day for this instance.
    /// </summary>
    internal TimeSpan TimeOfDay { get; } = new();

    #endregion


    #region Methods

    /// <summary>
    /// Adds months to the current <see cref="FrenchRepublicanDateTime"/>.
    /// </summary>
    /// <param name="months">An integer that represents the number of months to add.</param>
    /// <returns>The <see cref="FrenchRepublicanDateTime"/> after the addition.</returns>
    internal FrenchRepublicanDateTime AddMonths(int months)
    {
        if (months == 0)
        {
            return this;
        }

        if (Month == 13)
        {
            throw new InvalidOperationException("Addition of months starting from complementary days is not supported.");
        }

        int newYear = Year + (Month + months - (months > 0 ? 1 : 12)) / 12;
        int newMonth = ((Month + months) % 12 <= 0 ? 12 : 0) + (Month + months) % 12;

        return new FrenchRepublicanDateTime(newYear, newMonth, Day, TimeOfDay, Era);
    }


    /// <summary>
    /// Adds weeks to the current <see cref="FrenchRepublicanDateTime"/>.
    /// </summary>
    /// <param name="weeks">An integer that represents the number of weeks to add.</param>
    /// <returns>The <see cref="FrenchRepublicanDateTime"/> after the addition.</returns>
    internal FrenchRepublicanDateTime AddWeeks(int weeks)
    {
        if (weeks == 0)
        {
            return this;
        }

        if (Month == 13)
        {
            throw new InvalidOperationException("Addition of weeks starting from complementary days is not supported.");
        }

        int currentWeekOfYear = 3 * (Month - 1) + (Day - 1) / 10 + 1;
        int currentDayOfWeek = (Day - 1) % 10 + 1;
        int newWeekOfYear = ((currentWeekOfYear + weeks) % 36 <= 0 ? 36 : 0) + (currentWeekOfYear + weeks) % 36;
        int newYear = Year + (currentWeekOfYear + weeks - (weeks > 0 ? 1 : 36)) / 36;
        int newMonth = (newWeekOfYear - 1) / 3 + 1;
        int newDay = 10 * ((newWeekOfYear - 1) % 3) + currentDayOfWeek;

        return new FrenchRepublicanDateTime(newYear, newMonth, newDay, TimeOfDay, Era);
    }


    /// <summary>
    /// Adds years to the current <see cref="FrenchRepublicanDateTime"/>.
    /// </summary>
    /// <param name="years">An integer that represents the number of years to add.</param>
    /// <returns>The <see cref="FrenchRepublicanDateTime"/> after the addition.</returns>
    internal FrenchRepublicanDateTime AddYears(int years)
    {
        if (years == 0)
        {
            return this;
        }

        int newYear = Year + years;
        int newMonth = Month;
        int newDay = Day;

        if (newMonth == 13 && newDay == 6 && !IsLeapYear(newYear, Era))
        {
            newYear++;
            newMonth = 1;
            newDay = 1;
        }

        return new FrenchRepublicanDateTime(newYear, newMonth, newDay, TimeOfDay, Era);
    }


    /// <summary>
    /// Determines whether the provided day is a leap day.
    /// </summary>
    /// <param name="year">An integer that represents a year in the Republican calendar.</param>
    /// <param name="month">An integer that represents a month in the Republican calendar.</param>
    /// <param name="day">An integer that represents a day in the Republican calendar.</param>
    /// <param name="era">An integer that represents an era in the Republican calendar.</param>
    /// <returns>True if the day is a leap day, otherwise false.</returns>
    internal static bool IsLeapDay(int year, int month, int day, int era)
    {
        ValidateDay(year, month, day, era);
        return IsLeapMonth(year, month, era) && day == 6;
    }


    /// <summary>
    /// Determines whether the provided month is a leap month.
    /// </summary>
    /// <param name="year">An integer that represents a year in the Republican calendar.</param>
    /// <param name="month">An integer that represents a month in the Republican calendar.</param>
    /// <param name="era">An integer that represents an era in the Republican calendar.</param>
    /// <returns>True if the month is a leap month, otherwise false.</returns>
    internal static bool IsLeapMonth(int year, int month, int era)
    {
        ValidateMonth(year, month, era);
        return IsLeapYear(year, era) && month == 13;
    }


    /// <summary>
    /// Determines whether the provided year is a leap year.
    /// </summary>
    /// <param name="year">An integer that represents a year in the Republican calendar.</param>
    /// <param name="era">An integer that represents an era in the Republican calendar.</param>
    /// <returns>True if the year is a leap year, otherwise false.</returns>
    internal static bool IsLeapYear(int year, int era)
    {
        ValidateYear(year, era);
        return (year + 1) % 4 == 0;
    }


    /// <summary>
    /// Converts the <see cref="FrenchRepublicanDateTime"/> to a <see cref="DateTime"/> in the Gregorian calendar.
    /// </summary>
    /// <returns>The <see cref="DateTime"/> in the Gregorian Calendar.</returns>
    internal DateTime ToGregorianDateTime()
    {
        int daysSinceEpoch = 365 * (Year - 1) + Year / 4 + 30 * Month + Day - 31;
        TimeSpan timeSpan = new(daysSinceEpoch, Hour, Minute, Second, Millisecond);
        return Constants.MinSupportedDateTime.Add(timeSpan);
    }


    /// <summary>
    /// Validates an era in the Republican calendar.
    /// </summary>
    /// <param name="era">An integer that represents the era.</param>
    /// <exception cref="ArgumentOutOfRangeException">The era is invalid.</exception>
    internal static void ValidateEra(int era)
    {
        ValidateValueInsideRange(nameof(era), era, FrenchRepublicanCalendar.FrenchRepublicanEra, FrenchRepublicanCalendar.FrenchRepublicanEra);
    }


    /// <summary>
    /// Validates a year in the Republican calendar.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="era">An integer that represents the era.</param>
    /// <exception cref="ArgumentOutOfRangeException">The year is invalid.</exception>
    internal static void ValidateYear(int year, int era)
    {
        ValidateEra(era);
        ValidateValueInsideRange(nameof(year), year, 1, 14);
    }


    /// <summary>
    /// Validates a month in the Republican calendar.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="era">An integer that represents the era.</param>
    /// <exception cref="ArgumentOutOfRangeException">The month is invalid.</exception>
    internal static void ValidateMonth(int year, int month, int era)
    {
        ValidateYear(year, era);

        if (year == 14)
        {
            ValidateValueInsideRange(nameof(month), month, 1, 4);
        }
        else
        {
            ValidateValueInsideRange(nameof(month), month, 1, 13);
        }
    }


    /// <summary>
    /// Validates a day in the Republican calendar.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="day">An integer that represents the day.</param>
    /// <param name="era">An integer that represents the era.</param>
    /// <exception cref="ArgumentOutOfRangeException">The day is invalid.</exception>
    internal static void ValidateDay(int year, int month, int day, int era)
    {
        ValidateMonth(year, month, era);

        if (IsLeapMonth(year, month, era))
        {
            ValidateValueInsideRange(nameof(day), day, 1, 6);
        }
        else if (month == 13)
        {
            ValidateValueInsideRange(nameof(day), day, 1, 5);
        }
        else if (year == 14 && month == 4)
        {
            ValidateValueInsideRange(nameof(day), day, 1, 10);
        }
        else
        {
            ValidateValueInsideRange(nameof(day), day, 1, 30);
        }
    }


    /// <summary>
    /// Validates a time value.
    /// </summary>
    /// <param name="hour">An integer that represents the hour.</param>
    /// <param name="minute">An integer that represents the minute.</param>
    /// <param name="second">An integer that represents the second.</param>
    /// <param name="millisecond">An integer that represents the millisecond.</param>
    /// <exception cref="ArgumentOutOfRangeException">The time value is invalid.</exception>
    internal static void ValidateTime(int hour, int minute, int second, int millisecond)
    {
        ValidateValueInsideRange(nameof(hour), hour, 0, 23);
        ValidateValueInsideRange(nameof(minute), minute, 0, 59);
        ValidateValueInsideRange(nameof(second), second, 0, 59);
        ValidateValueInsideRange(nameof(millisecond), millisecond, 0, 999);
    }


    /// <summary>
    /// Validates a time value.
    /// </summary>
    /// <param name="timeOfDay">The time of the day.</param>
    /// <exception cref="ArgumentOutOfRangeException">The time value is invalid.</exception>
    private static void ValidateTime(TimeSpan timeOfDay)
    {
        ValidateTime(timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds, timeOfDay.Milliseconds);
    }


    /// <summary>
    /// Determines whether a specified value is within the specified range.
    /// </summary>
    /// <param name="paramName">A string that represents the name of the parameter to check.</param>
    /// <param name="value">An integer that represents the value to check.</param>
    /// <param name="lowerBound">An integer that represents the lower bound.</param>
    /// <param name="upperBound">An integer that represents the upper bound.</param>
    /// <exception cref="ArgumentOutOfRangeException">The value is not within the specified range.</exception>
    private static void ValidateValueInsideRange(string paramName, int value, int lowerBound, int upperBound)
    {
        if (value < lowerBound || value > upperBound)
        {
            throw new ArgumentOutOfRangeException(paramName, value, $"The value must be between {lowerBound} and {upperBound}.");
        }
    }

    #endregion


    #region Implementation of IFormattable

    /// <inheritdoc/>
    public String ToString(string? format, IFormatProvider? formatProvider)
    {
        // Set default formatting string
        if (String.IsNullOrEmpty(format))
        {
            format = "G";
        }

        // Use provided formatter
        if ((formatProvider != null) && (formatProvider.GetFormat(GetType()) is ICustomFormatter formatter))
        {
            return formatter.Format(format, this, formatProvider);
        }

        throw new NotImplementedException();
    }

    #endregion

}
