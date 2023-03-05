using System;
using System.Globalization;


namespace Sinistrius.CalendrierRepublicain;


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
    public override int[] Eras => new[] { FrenchRepublicanEra };


    /// <inheritdoc/>
    public override DateTime MaxSupportedDateTime => Constants.MaxSupportedDateTime;


    /// <inheritdoc/>
    public override DateTime MinSupportedDateTime => Constants.MinSupportedDateTime;


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
        FrenchRepublicanDateTime repTime = time.ToFrenchRepublicanTime();

        // Reject addition from complementary days
        if (repTime.Month == 13)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        // Calculate new republican date
        FrenchRepublicanDateTime newRepTime = repTime.AddMonths(months);

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
        FrenchRepublicanDateTime repTime = time.ToFrenchRepublicanTime();

        // Reject addition from complementary days
        if (repTime.Month == 13)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        // Calculate new republican date
        FrenchRepublicanDateTime newRepTime = repTime.AddWeeks(weeks);

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
        FrenchRepublicanDateTime repTime = time.ToFrenchRepublicanTime();

        // Calculate new republican date
        FrenchRepublicanDateTime newRepTime = repTime.AddYears(years);

        // Convert result to Gregorian date
        return ToDateTime(newRepTime.Year, newRepTime.Month, newRepTime.Day, repTime.TimeOfDay);
    }


    /// <inheritdoc/>
    public override int GetDayOfMonth(DateTime time)
    {
        time.Validate();

        FrenchRepublicanDateTime repTime = time.ToFrenchRepublicanTime();
        return repTime.Day;
    }


    /// <inheritdoc/>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
        throw new InvalidOperationException("The function type GetDayOfWeek is not applicable for the Republican calendar.");
    }


    /// <inheritdoc/>
    public override int GetDayOfYear(DateTime time)
    {
        time.Validate();

        FrenchRepublicanDateTime repTime = time.ToFrenchRepublicanTime();
        return 30 * (repTime.Month - 1) + repTime.Day;
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

        if (month == 13)
        {
            return IsLeapYear(year) ? 6 : 5;
        }

        // The Republican calendar was abolished after the 10th of Nivôse XIV.
        if (year == Constants.LastRepublicanYear && month == Constants.LastRepublicanMonth)
        {
            return 10;
        }

        // Normal month
        return 30;
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

        if (year == Constants.LastRepublicanYear)
        {
            return 100;
        }
        else if (IsLeapYear(year))
        {
            return 366;
        }
        else
        {
            return 365;
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

        FrenchRepublicanDateTime repTime = time.ToFrenchRepublicanTime();
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

        if (year == Constants.LastRepublicanYear)
        {
            return Constants.LastRepublicanMonth;
        }
        else
        {
            return 13;
        }
    }


    /// <inheritdoc/>
    public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
    {
        FrenchRepublicanDateTime repTime = time.ToFrenchRepublicanTime();
        return 3 * (repTime.Month - 1) + (repTime.Day - 1) / 10 + 1;
    }


    /// <inheritdoc/>
    public override int GetYear(DateTime time)
    {
        time.Validate();

        FrenchRepublicanDateTime repTime = time.ToFrenchRepublicanTime();
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
