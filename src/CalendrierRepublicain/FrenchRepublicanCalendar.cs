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

    #region Local Fields

    /// <summary>
    /// A validator for date and time parts of the Republican calendar.
    /// </summary>
    private readonly RepublicanDateTimeValidator _validator = new();

    #endregion


    #region Overridden Calendar Properties

    /// <inheritdoc/>
    public override CalendarAlgorithmType AlgorithmType
    {
        get
        {
            return CalendarAlgorithmType.SolarCalendar;
        }
    }


    /// <inheritdoc/>
    public override int[] Eras => new[] { 1 };


    /// <inheritdoc/>
    public override DateTime MaxSupportedDateTime => Globals.MaxSupportedDateTime;


    /// <inheritdoc/>
    public override DateTime MinSupportedDateTime => Globals.MinSupportedDateTime;

    #endregion


    #region Overridden Calendar Methods

    /// <inheritdoc/>
    public override DateTime AddMonths(DateTime time, int months)
    {
        // Nothing to add
        if (months == 0)
        {
            return time;
        }

        RepublicanDateTime repDateTime = time.ToRepublican();

        // Initialize number of months still to be added
        int monthsToAdd = months;

        // Determine step direction
        int step = Math.Sign(months);

        while (monthsToAdd != 0)
        {
            // If within the complementary days, go to the nearest day in a regular month
            if (repDateTime.Month == 13)
            {
                repDateTime.Day = (step < 0) ? 30 : 1;
            }

            // Add or subtract one month
            repDateTime.Month += step;

            // Fell below the beginning of the year: Correct to last month of previous year
            if (repDateTime.Month == 0)
            {
                repDateTime.Year--;
                repDateTime.Month = 12;
            }

            // Exceeded the end of the year: Correct to first month of next year
            else if (repDateTime.Month == 13)
            {
                repDateTime.Year++;
                repDateTime.Month = 1;
            }

            // Addition finished
            monthsToAdd -= step;
        }

        // Validate the calculated date
        _validator.ValidateMonth(repDateTime.Year, repDateTime.Month);

        // Convert to Gregorian
        return repDateTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override DateTime AddYears(DateTime time, int years)
    {
        // Nothing to add
        if (years == 0)
        {
            return time;
        }

        RepublicanDateTime repDateTime = time.ToRepublican();

        // Initialize number of years still to be added
        int yearsToAdd = years;

        // Determine step direction
        int step = Math.Sign(years);

        while (yearsToAdd != 0)
        {
            // Add or subtract one year
            repDateTime.Year += step;

            // Add one day if addition ended on invalid leap day.
            if (!Globals.IsLeapYear(repDateTime.Year) && repDateTime.Month == 13 && repDateTime.Day == 6)
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

            // Addition finished
            yearsToAdd -= step;
        }

        // Validate the calculated date
        _validator.ValidateMonth(repDateTime.Year, repDateTime.Month);

        // Convert to Gregorian
        return repDateTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override int GetDayOfMonth(DateTime time)
    {
        RepublicanDateTime repDateTime = time.ToRepublican();
        return repDateTime.Day;
    }


    /// <inheritdoc/>
    public override DayOfWeek GetDayOfWeek(DateTime time)
    {
        throw new InvalidOperationException("The function type is not applicable. The French Republican calendar has ten days per week.");
    }


    /// <inheritdoc/>
    public override int GetDayOfYear(DateTime time)
    {
        RepublicanDateTime repDateTime = time.ToRepublican();
        return 30 * (repDateTime.Month - 1) + repDateTime.Day;
    }


    /// <inheritdoc/>
    public override int GetDaysInMonth(int year, int month, int era)
    {
        throw new NotImplementedException();
    }


    /// <inheritdoc/>
    public override int GetDaysInYear(int year, int era)
    {
        throw new NotImplementedException();
    }


    /// <inheritdoc/>
    public override int GetEra(DateTime time)
    {
        throw new NotImplementedException();
    }


    /// <inheritdoc/>
    public override int GetMonth(DateTime time)
    {
        throw new NotImplementedException();
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
        _validator.ValidateEra(era);
        _validator.ValidateYear(year);
        _validator.ValidateMonth(year, month);
        _validator.ValidateDay(year, month, day);

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
        _validator.ValidateEra(era);
        _validator.ValidateYear(year);
        _validator.ValidateMonth(year, month);

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
        _validator.ValidateEra(era);
        _validator.ValidateYear(year);

        return Globals.IsLeapYear(year);
    }


    /// <inheritdoc/>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
        _validator.ValidateYear(year);
        _validator.ValidateMonth(year, month);
        _validator.ValidateDay(year, month, day);
        _validator.ValidateHour(hour);
        _validator.ValidateMinute(minute);
        _validator.ValidateSecond(second);
        _validator.ValidateMillisecond(millisecond);

        // Create Republican date time
        RepublicanDateTime repDateTime = new(year, month, day, hour, minute, second, millisecond);

        // Convert to Gregorian date time
        return repDateTime.ToGregorian();
    }


    /// <inheritdoc/>
    public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
    {
        _validator.ValidateEra(era);

        return ToDateTime(year, month, day, hour, minute, second, millisecond);
    }

    #endregion


    #region Helper Methods


    #endregion

}
