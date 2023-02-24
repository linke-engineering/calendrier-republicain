using Sinistrius.CalendrierRepublicain.Extensions;
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
        // Validate parameters
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        if (months == 0)
        {
            return time;
        }

        // Initialize republican date
        RepublicanDateTime repTime = time.ToRepublican();

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

        RepublicanDateTime newRepTime = new(targetYear, targetMonth, repTime.Day, repTime.TimeOfDay);

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
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        if (weeks == 0)
        {
            return time;
        }

        // Initialize republican date
        RepublicanDateTime repTime = time.ToRepublican();

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

        RepublicanDateTime newRepTime = new(targetYear, targetMonth, targetDay, repTime.TimeOfDay);

        // Convert result to Gregorian date
        return newRepTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override DateTime AddYears(DateTime time, int years)
    {
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        // Nothing to add
        if (years == 0)
        {
            return time;
        }

        // Initialize Republican date
        RepublicanDateTime repTime = time.ToRepublican();

        // Calculate new republican date
        int targetYear = repTime.Year + years;
        int targetMonth = repTime.Month;
        int targetDay = repTime.Day;

        if (repTime.IsJourDeLaRevolution && !targetYear.IsRepublicanLeapYear())
        {
            targetYear++;
            targetMonth = 1;
            targetDay = 1;
        }

        RepublicanDateTime newRepTime = new(targetYear, targetMonth, targetDay, repTime.TimeOfDay);

        // Convert result to Gregorian date
        return newRepTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override int GetDayOfMonth(DateTime time)
    {
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        RepublicanDateTime repDateTime = time.ToRepublican();
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
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        RepublicanDateTime repDateTime = time.ToRepublican();
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
        if (!era.IsValidRepublicanCalenderEra())
        {
            throw new ArgumentOutOfRangeException(nameof(era));
        }

        if (!year.IsValidRepublicanYear())
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (!month.IsValidRepublicanMonth(year))
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        // TODO: Are complementary days considered as a regular month in this method?
        if (month == 13)
        {
            return year.IsRepublicanLeapYear() ? 6 : 5;
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
        if (!era.IsValidRepublicanCalenderEra())
        {
            throw new ArgumentOutOfRangeException(nameof(era));
        }

        if (!year.IsValidRepublicanYear())
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (year == Constants.LastRepublicanYear)
        {
            return 100;
        }
        else if (year.IsRepublicanLeapYear())
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
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        return 1;
    }


    /// <inheritdoc/>
    public override int GetMonth(DateTime time)
    {
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        RepublicanDateTime repTime = time.ToRepublican();
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
        if (!era.IsValidRepublicanCalenderEra())
        {
            throw new ArgumentOutOfRangeException(nameof(era));
        }

        if (!year.IsValidRepublicanYear())
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

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
        RepublicanDateTime repTime = time.ToRepublican();
        return 3 * (repTime.Month - 1) + (repTime.Day - 1) / 10 + 1;
    }


    /// <inheritdoc/>
    public override int GetYear(DateTime time)
    {
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        RepublicanDateTime repTime = time.ToRepublican();
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
        if (!era.IsValidRepublicanCalenderEra())
        {
            throw new ArgumentOutOfRangeException(nameof(era));
        }

        if (!year.IsValidRepublicanYear())
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (!month.IsValidRepublicanMonth(year))
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        if (!day.IsValidRepublicanDay(year, month))
        {
            throw new ArgumentOutOfRangeException(nameof(day));
        }

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
        if (!era.IsValidRepublicanCalenderEra())
        {
            throw new ArgumentOutOfRangeException(nameof(era));
        }

        if (!year.IsValidRepublicanYear())
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (!month.IsValidRepublicanMonth(year))
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        return IsLeapYear(year, era) && month == 13;
    }


    /// <inheritdoc/>
    public override bool IsLeapYear(int year)
    {
        return IsLeapYear(year, 1);
    }


    /// <inheritdoc/>
    public override bool IsLeapYear(int year, int era)
    {
        if (!era.IsValidRepublicanCalenderEra())
        {
            throw new ArgumentOutOfRangeException(nameof(era));
        }

        if (!year.IsValidRepublicanYear())
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        return year.IsRepublicanLeapYear();
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
        RepublicanDateTime repDateTime = new(year, month, day, hour, minute, second, millisecond);

        // Convert to Gregorian date time
        return repDateTime.ToGregorian();
    }

    #endregion

}
