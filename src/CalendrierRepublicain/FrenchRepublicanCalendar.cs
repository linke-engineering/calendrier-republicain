﻿using Sinistrius.CalendrierRepublicain.Extensions;
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

    #region Overridden Properties of the Calendar Class

    /// <inheritdoc/>
    public override CalendarAlgorithmType AlgorithmType => CalendarAlgorithmType.SolarCalendar;


    /// <inheritdoc/>
    public override int[] Eras => new[] { 1 };


    /// <inheritdoc/>
    public override DateTime MaxSupportedDateTime => Constants.MaxSupportedDateTime;


    /// <inheritdoc/>
    public override DateTime MinSupportedDateTime => Constants.MinSupportedDateTime;


    /// <inheritdoc/>
    public override int TwoDigitYearMax => Constants.TwoDigitYearMax;

    #endregion


    #region Overridden Methods of the Calendar Class

    /// <inheritdoc/>
    /// <remarks>
    /// Complementary days will be skipped.
    /// </remarks>
    public override DateTime AddMonths(DateTime time, int months)
    {
        time.Validate();

        if (months == 0)
        {
            return time;
        }

        // Initialize republican date
        FrenchRepublicanDateTime repTime = time.ToRepublican();

        // If within the complementary days, move to begin resp. end of the next month
        if (repTime.IsComplementaryDays)
        {
            if (months < 0)
            {
                repTime = repTime.EndOfPreviousMonth();
            }
            else
            {
                repTime = repTime.StartOfNextYear();
            }
        }

        // Calculate new republican date
        int targetYear = repTime.Year + (repTime.Month + months - (repTime.Month + months > 0 ? 1 : 13)) / 12;
        int targetMonth = ((repTime.Month + months) % 12 <= 0 ? 12 : 0) + (repTime.Month + months) % 12;

        FrenchRepublicanDateTime newRepTime = new(targetYear, targetMonth, repTime.Day, repTime.TimeOfDay);

        // Convert result to Gregorian date.
        return newRepTime.ToGregorian();
    }


    /// <inheritdoc/>
    /// <remarks>
    /// Complementary days will be skipped.
    /// </remarks>
    public override DateTime AddWeeks(DateTime time, int weeks)
    {
        // Validate parameters
        time.Validate();

        if (weeks == 0)
        {
            return time;
        }

        // Initialize republican date
        FrenchRepublicanDateTime repTime = time.ToRepublican();

        // If within the complementary days, move to begin resp. end of the next month
        if (repTime.IsComplementaryDays)
        {
            if (weeks < 0)
            {
                repTime = repTime.EndOfPreviousMonth();
            }
            else
            {
                repTime = repTime.StartOfNextYear();
            }
        }

        // Calculate new republican date
        int currentWeek = 3 * (repTime.Month - 1) + (repTime.Day - 1) / 10 + 1;
        int currentDayOfWeek = (repTime.Day - 1) % 10 + 1;

        int targetYear = repTime.Year + (currentWeek + weeks - (currentWeek + weeks > 0 ? 1 : 37)) / 36;
        int targetWeek = ((currentWeek + weeks) % 36 <= 0 ? 36 : 0) + (currentWeek + weeks) % 36;
        int targetMonth = (targetWeek - 1) / 3 + 1;
        int targetDay = 10 * ((targetWeek - 1) % 3) + currentDayOfWeek;

        FrenchRepublicanDateTime newRepTime = new(targetYear, targetMonth, targetDay, repTime.TimeOfDay);

        // Convert result to Gregorian date
        return newRepTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override DateTime AddYears(DateTime time, int years)
    {
        time.Validate();

        // Nothing to add
        if (years == 0)
        {
            return time;
        }

        // Initialize Republican date
        FrenchRepublicanDateTime repTime = time.ToRepublican();

        // Calculate new republican date
        int targetYear = repTime.Year + years;
        int targetMonth = repTime.Month;
        int targetDay = repTime.Day;

        if (repTime.IsJourDeLaRevolution && !IsLeapYear(targetYear))
        {
            targetYear++;
            targetMonth = 1;
            targetDay = 1;
        }

        FrenchRepublicanDateTime newRepTime = new(targetYear, targetMonth, targetDay, repTime.TimeOfDay);

        // Convert result to Gregorian date
        return newRepTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override int GetDayOfMonth(DateTime time)
    {
        time.Validate();

        FrenchRepublicanDateTime repDateTime = time.ToRepublican();
        return repDateTime.Day;
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

        FrenchRepublicanDateTime repDateTime = time.ToRepublican();
        return 30 * (repDateTime.Month - 1) + repDateTime.Day;
    }


    /// <inheritdoc/>
    public override int GetDaysInMonth(int year, int month)
    {
        return GetDaysInMonth(year, month, 1);
    }


    /// <inheritdoc/>
    public override int GetDaysInMonth(int year, int month, int era)
    {
        this.ValidateMonth(year,month, era);

        if (month == 13)
        {
            return this.IsLeapYear(year) ? 6 : 5;
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
        return GetDaysInYear(year, 1);
    }


    /// <inheritdoc/>
    public override int GetDaysInYear(int year, int era)
    {
        this.ValidateYear(year, era);

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
        return 1;
    }


    /// <inheritdoc/>
    public override int GetMonth(DateTime time)
    {
        time.Validate();

        FrenchRepublicanDateTime repTime = time.ToRepublican();
        return repTime.Month;
    }


    /// <inheritdoc/>
    public override int GetMonthsInYear(int year)
    {
        return GetMonthsInYear(year, 1);
    }


    /// <inheritdoc/>
    public override int GetMonthsInYear(int year, int era)
    {
        this.ValidateYear(year, era);

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
        FrenchRepublicanDateTime repTime = time.ToRepublican();
        return 3 * (repTime.Month - 1) + (repTime.Day - 1) / 10 + 1;
    }


    /// <inheritdoc/>
    public override int GetYear(DateTime time)
    {
        time.Validate();

        FrenchRepublicanDateTime repTime = time.ToRepublican();
        return repTime.Year;
    }


    /// <inheritdoc/>
    public override bool IsLeapDay(int year, int month, int day)
    {
        return IsLeapDay(year, month, day, 1);
    }


    /// <inheritdoc/>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
        this.ValidateDay(year, month, day, era);
        return IsLeapMonth(year, month, era) && day == 6;
    }


    /// <inheritdoc/>
    public override bool IsLeapMonth(int year, int month)
    {
        return IsLeapMonth(year, month, 1);
    }


    /// <inheritdoc/>
    public override bool IsLeapMonth(int year, int month, int era)
    {
        this.ValidateMonth(year, month, era);
        return IsLeapYear(year) && month == 13;
    }


    /// <inheritdoc/>
    public override bool IsLeapYear(int year)
    {
        return IsLeapYear(year, 1);
    }


    /// <inheritdoc/>
    public override bool IsLeapYear(int year, int era)
    {
        this.ValidateYear(year, era);
        return (year + 1) % 4 == 0;
    }


    /// <inheritdoc/>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
        return ToDateTime(year, month, day, hour, minute, second, millisecond, 1);
    }


    /// <inheritdoc/>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
        // Create Republican date time
        FrenchRepublicanDateTime repDateTime = new(year, month, day, hour, minute, second, millisecond);

        // Convert to Gregorian date time
        return repDateTime.ToGregorian();
    }

    #endregion

}
