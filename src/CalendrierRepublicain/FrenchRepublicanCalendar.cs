using System.Globalization;
using C = LinkeEngineering.CalendrierRepublicain.Constants;



namespace LinkeEngineering.CalendrierRepublicain;


/// <summary>
/// Represents the French Republican calendar.
/// </summary>
/// <remarks>
/// The French Republican calendar (calendrier républicain) was implemented during the French Revolution and used by the French government for about 13 years from late 1792 to 1805.
/// </remarks>
public class FrenchRepublicanCalendar : Calendar
{

    #region Properties

    /// <summary>
    /// Represents the current era.
    /// </summary>
    public static readonly int FrenchRepublicanEra = 1;

    #endregion


    #region Overridden Properties of the Calendar Class

    /// <inheritdoc/>
    public override CalendarAlgorithmType AlgorithmType => CalendarAlgorithmType.SolarCalendar;


    /// <inheritdoc/>
    public override int[] Eras => [FrenchRepublicanEra];


    /// <inheritdoc/>
    public override DateTime MaxSupportedDateTime => C.MaxSupportedDateTime;


    /// <inheritdoc/>
    public override DateTime MinSupportedDateTime => C.MinSupportedDateTime;


    /// <inheritdoc/>
    public override int TwoDigitYearMax => 99;

    #endregion


    #region Overridden Methods of the Calendar Class

    /// <inheritdoc/>
    public override DateTime AddDays(DateTime time, int days)
    {
        time.Validate();

        return base.AddDays(time, days);
    }


    /// <inheritdoc/>
    /// <remarks>
    /// Complementary days will be skipped.
    /// </remarks>
    public override DateTime AddMonths(DateTime time, int months)
    {
        if (months == 0)
        {
            return time;
        }

        // Initialize republican date
        var repTime = time.ToFrenchRepublicanTime();

        // Reject addition from complementary days
        if (repTime.Month == C.ComplementaryMonth)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        // Calculate new republican date
        var newRepTime = repTime.AddMonths(months);

        // Convert result to Gregorian date
        return ToDateTime(newRepTime.Year, newRepTime.Month, repTime.Day, repTime.TimeOfDay);
    }


    /// <inheritdoc/>
    /// <remarks>
    /// Complementary days will be skipped.
    /// </remarks>
    public override DateTime AddWeeks(DateTime time, int weeks)
    {
        if (weeks == 0)
        {
            return time;
        }

        // Initialize republican date
        var repTime = time.ToFrenchRepublicanTime();

        // Reject addition from complementary days
        if (repTime.Month == C.ComplementaryMonth)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        // Calculate new republican date
        var newRepTime = repTime.AddWeeks(weeks);

        // Convert result to Gregorian date
        return ToDateTime(newRepTime.Year, newRepTime.Month, newRepTime.Day, repTime.TimeOfDay);
    }


    /// <inheritdoc/>
    public override DateTime AddYears(DateTime time, int years)
    {
        // Nothing to add
        if (years == 0)
        {
            return time;
        }

        // Initialize Republican date
        var repTime = time.ToFrenchRepublicanTime();

        // Calculate new republican date
        var newRepTime = repTime.AddYears(years);

        // Convert result to Gregorian date
        return ToDateTime(newRepTime.Year, newRepTime.Month, newRepTime.Day, repTime.TimeOfDay);
    }


    /// <inheritdoc/>
    public override int GetDayOfMonth(DateTime time)
    {
        time.Validate();

        var repTime = time.ToFrenchRepublicanTime();
        return repTime.Day;
    }


    /// <inheritdoc/>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
        throw new NotSupportedException("The function type GetDayOfWeek is not supported for the Republican calendar.");
    }


    /// <inheritdoc/>
    public override int GetDayOfYear(DateTime time)
    {
        time.Validate();

        var repTime = time.ToFrenchRepublicanTime();
        return C.DaysInMonth * (repTime.Month - 1) + repTime.Day;
    }


    /// <inheritdoc/>
    public override int GetDaysInMonth(int year, int month)
    {
        return GetDaysInMonth(year, month, FrenchRepublicanEra);
    }


    /// <inheritdoc/>
    public override int GetDaysInMonth(int year, int month, int era)
    {
        FrenchRepublicanDateTime.ValidateMonth(year,month, era);

        if (month == C.ComplementaryMonth)
        {
            return IsLeapYear(year) ? C.LeapComplementaryDays : C.ComplementaryDays;
        }

        // The Republican calendar was abolished after the 10th of Nivôse XIV.
        if (year == C.LastYear && month == C.LastMonth)
        {
            return C.DaysInLastMonth;
        }

        // Normal month
        return C.DaysInMonth;
    }


    /// <inheritdoc/>
    public override int GetDaysInYear(int year)
    {
        return GetDaysInYear(year, FrenchRepublicanEra);
    }


    /// <inheritdoc/>
    public override int GetDaysInYear(int year, int era)
    {
        FrenchRepublicanDateTime.ValidateYear(year, era);

        if (year == C.LastYear)
        {
            return (C.LastMonth - 1) * C.DaysInMonth + C.DaysInLastMonth;
        }
        else if (IsLeapYear(year))
        {
            return C.MonthsInYear * C.DaysInMonth + C.LeapComplementaryDays;
        }
        else
        {
            return C.MonthsInYear * C.DaysInMonth + C.ComplementaryDays;
        }
    }


    /// <inheritdoc/>
    public override int GetEra(DateTime time)
    {
        time.Validate();
        return FrenchRepublicanEra;
    }


    /// <inheritdoc/>
    public override int GetMonth(DateTime time)
    {
        time.Validate();

        var repTime = time.ToFrenchRepublicanTime();
        return repTime.Month;
    }


    /// <inheritdoc/>
    public override int GetMonthsInYear(int year)
    {
        return GetMonthsInYear(year, FrenchRepublicanEra);
    }


    /// <inheritdoc/>
    public override int GetMonthsInYear(int year, int era)
    {
        FrenchRepublicanDateTime.ValidateYear(year, era);

        if (year == C.LastYear)
        {
            return C.LastMonth;
        }
        else
        {
            return C.ComplementaryMonth;
        }
    }


    /// <inheritdoc/>
    public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        var repTime = time.ToFrenchRepublicanTime();
        return C.WeeksInMonth * (repTime.Month - 1) + (repTime.Day - 1) / C.DaysInWeek + 1;
    }


    /// <inheritdoc/>
    public override int GetYear(DateTime time)
    {
        time.Validate();

        var repTime = time.ToFrenchRepublicanTime();
        return repTime.Year;
    }


    /// <inheritdoc/>
    public override bool IsLeapDay(int year, int month, int day)
    {
        return IsLeapDay(year, month, day, FrenchRepublicanEra);
    }


    /// <inheritdoc/>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
        return FrenchRepublicanDateTime.IsLeapDay(year, month, day, era);
    }


    /// <inheritdoc/>
    public override bool IsLeapMonth(int year, int month)
    {
        return IsLeapMonth(year, month, FrenchRepublicanEra);
    }


    /// <inheritdoc/>
    public override bool IsLeapMonth(int year, int month, int era)
    {
        return FrenchRepublicanDateTime.IsLeapMonth(year, month, era);
    }


    /// <inheritdoc/>
    public override bool IsLeapYear(int year)
    {
        return IsLeapYear(year, FrenchRepublicanEra);
    }


    /// <inheritdoc/>
    public override bool IsLeapYear(int year, int era)
    {
        return FrenchRepublicanDateTime.IsLeapYear(year, era);
    }


    /// <inheritdoc/>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
        return ToDateTime(year, month, day, hour, minute, second, millisecond, FrenchRepublicanEra);
    }


    /// <inheritdoc/>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
        FrenchRepublicanDateTime time = new(year, month, day, hour, minute, second, millisecond, era);
        return time.ToGregorianDateTime();
    }

    #endregion


    #region Helpers

    /// <summary>
    /// Returns a <see cref="DateTime"/> that is set to the specified date and time.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="timeOfDay">The time of the day.</param>
    /// <returns>The <see cref="DateTime"/> that is set to the specified date and time in the current era.</returns>
    private DateTime ToDateTime(int year, int month, int day, TimeSpan timeOfDay)
    {
        return ToDateTime(year, month, day, timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds, timeOfDay.Milliseconds, FrenchRepublicanEra);
    }

    #endregion

}
