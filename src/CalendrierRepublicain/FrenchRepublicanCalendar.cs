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

    #endregion


    #region Overridden Methods of the Calendar Class

    /// <inheritdoc/>
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
        RepublicanDateTime repDateTime = time.ToRepublican();

        // Initialize number of months still to be added
        int monthsToAdd = months;

        // Set step size and direction
        int step = Math.Sign(months);

        // The complementary days are not considered as a month
        if (repDateTime.Month == 13)
        {
            // When subtracting, start from the end of the last month of the year
            if (step < 0)
            {
                repDateTime.Month--;
                repDateTime.Day = 30;
            }

            // When adding, start from the first day of the next year
            else
            {
                repDateTime.Year++;
                repDateTime.Month = 1;
                repDateTime.Day = 1;
            }
        }

        while (monthsToAdd != 0)
        {
            // Add or subtract one month
            repDateTime.Month += step;

            // Fell below the beginning of the year: Correct to last month of previous year
            if (repDateTime.Month < 1)
            {
                repDateTime.Year--;
                repDateTime.Month = 12;
            }

            // Exceeded the end of the year: Correct to first month of next year
            else if (repDateTime.Month > 12)
            {
                repDateTime.Year++;
                repDateTime.Month = 1;
            }

            // Validate if resulting date is still in valid range
            if (!repDateTime.IsValid())
            {
                throw new InvalidOperationException("Result would be outside the validity period of the Republican calendar.");
            }

            // Addition finished
            monthsToAdd -= step;
        }

        // Convert to Gregorian
        return repDateTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override DateTime AddWeeks(DateTime time, int weeks)
    {
        if (!time.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }

        // Nothing to add
        if (weeks == 0)
        {
            return time;
        }

        // Initialize Republican date.
        RepublicanDateTime repDateTime = time.ToRepublican();

        // Initialize number of days still to be added
        int daysToAdd = 10 * weeks;

        // Set step size and direction
        int step = 10 * Math.Sign(weeks);

        // The complementary days are not considered as a week
        if (repDateTime.Month == 13)
        {
            // When subtracting, start from the end of the last regular week of the year
            if (step < 0)
            {
                repDateTime.Month--;
                repDateTime.Day = 30;
            }

            // When adding, start from the first day of the next year
            else
            {
                repDateTime.Year++;
                repDateTime.Month = 1;
                repDateTime.Day = 1;
            }
        }

        while (daysToAdd != 0)
        {
            // Add or subtract one week
            repDateTime.Day += step;

            // Fell below the beginning of the month: Correct to previous month
            if (repDateTime.Day < 1)
            {
                repDateTime.Month--;
                repDateTime.Day += 30;

                if (repDateTime.Month < 1)
                {
                    repDateTime.Year--;
                    repDateTime.Month = 12;
                }
            }

            // Exceeded the end of the month: Correct to next month
            else if (repDateTime.Day > 30)
            {
                repDateTime.Month++;
                repDateTime.Day -= 30;

                if (repDateTime.Month > 12)
                {
                    repDateTime.Year++;
                    repDateTime.Month = 1;
                }
            }

            // Validate if resulting date is still in valid range
            if (!repDateTime.IsValid())
            {
                throw new InvalidOperationException("Result would be outside the validity period of the Republican calendar.");
            }

            // Addition finished
            daysToAdd -= step;
        }

        // Convert to Gregorian
        return repDateTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override DateTime AddYears(DateTime time, int years)
    {
        time.IsValid();

        // Nothing to add
        if (years == 0)
        {
            return time;
        }

        // Initialize Republican date
        RepublicanDateTime repDateTime = time.ToRepublican();

        // Initialize number of years still to be added
        int yearsToAdd = years;

        // Set step size and direction
        int step = Math.Sign(years);

        while (yearsToAdd != 0)
        {
            // Add or subtract one year
            repDateTime.Year += step;

            // Add one day if addition ended on invalid leap day.
            if (!repDateTime.Year.IsRepublicanLeapYear() && repDateTime.Month == 13 && repDateTime.Day == 6)
            {
                if (step < 0)
                {
                    repDateTime.Day--;
                }
                else
                {
                    repDateTime.Year++;
                    repDateTime.Month = 1;
                    repDateTime.Day = 1;
                }
            }

            // Validate if resulting date is still in valid range
            if (!repDateTime.IsValid())
            {
                throw new InvalidOperationException("Result would be outside the validity period of the Republican calendar.");
            }

            // Addition finished
            yearsToAdd -= step;
        }

        // Convert to Gregorian
        return repDateTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override int GetDayOfMonth(DateTime time)
    {
        time.IsValid();
        RepublicanDateTime repDateTime = time.ToRepublican();
        return repDateTime.Day;
    }


    /// <inheritdoc/>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
        throw new InvalidOperationException("The function type DayOfWeek is not applicable for the Republican calendar.");
    }


    /// <inheritdoc/>
    public override int GetDayOfYear(DateTime time)
    {
        time.IsValid();
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
        era.IsValidRepublicanCalenderEra();
        year.IsValidRepublicanYear();
        month.IsValidRepublicanMonth(year);

        // TODO: Are complementary days considered as a regular month in this method?
        if (month == 13)
        {
            return year.IsRepublicanLeapYear() ? 6 : 5;
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
        era.IsValidRepublicanCalenderEra();
        year.IsValidRepublicanYear();

        throw new NotImplementedException();
    }


    /// <inheritdoc/>
    public override int GetEra(DateTime time)
    {
        time.IsValid();

        throw new NotImplementedException();
    }


    /// <inheritdoc/>
    public override int GetMonth(DateTime time)
    {
        throw new NotImplementedException();
    }


    /// <inheritdoc/>
    public override int GetMonthsInYear(int year)
    {
        return GetMonthsInYear(year, 1);
    }


    /// <inheritdoc/>
    public override int GetMonthsInYear(int year, int era)
    {
        throw new NotImplementedException();
    }


    /// <inheritdoc/>
    public override int GetYear(DateTime time)
    {
        throw new NotImplementedException();
    }


    /// <inheritdoc/>
    public override bool IsLeapDay(int year, int month, int day)
    {
        return IsLeapDay(year, month, day, 1);
    }


    /// <inheritdoc/>
    public override bool IsLeapDay(int year, int month, int day, int era)
    {
        era.IsValidRepublicanCalenderEra();
        year.IsValidRepublicanYear();
        month.IsValidRepublicanMonth(year);
        day.IsValidRepublicanDay(year, month);

        return IsLeapYear(year, era) && month == 13 && day == 6;
    }


    /// <inheritdoc/>
    public override bool IsLeapMonth(int year, int month)
    {
        return IsLeapMonth(year, month, 1);
    }


    /// <inheritdoc/>
    public override bool IsLeapMonth(int year, int month, int era)
    {
        era.IsValidRepublicanCalenderEra();
        year.IsValidRepublicanYear();
        month.IsValidRepublicanMonth(year);

        return false;
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
        year.IsValidRepublicanYear();
        month.IsValidRepublicanMonth(year);
        day.IsValidRepublicanDay(year, month);
        hour.IsValidHour();
        minute.IsValidMinute();
        second.IsValisSecond();
        millisecond.IsValidMillisecond();

        // Create Republican date time
        RepublicanDateTime repDateTime = new(year, month, day, hour, minute, second, millisecond);

        // Convert to Gregorian date time
        return repDateTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
        era.IsValidRepublicanCalenderEra();

        return ToDateTime(year, month, day, hour, minute, second, millisecond);
    }

    #endregion

}
