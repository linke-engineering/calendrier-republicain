# Calendrier Republicain

A .NET 8 library for converting and formatting dates between the Gregorian and French Republican calendars. It provides a drop-in replacement for the standard calendar and formatting infrastructure, enabling seamless integration in .NET applications.

The library is published as a NuGet package and designed for use in other .NET projects.

## Usage

### Restrictions

The library strictly implements the official features of the French Republican calendar, valid only between 1 Vendémiaire I (September 22, 1792) and 10 Nivôse XIV (December 31, 1805). Dates outside this range and decimal time are not supported.

### Creating Republican Dates

You can instantiate a `DateTime` object using the `FrenchRepublicanCalendar` class, which derives from `System.Globalization.Calendar`. This allows you to work with French Republican dates in all standard .NET APIs that support custom calendars.

```cs
using LinkeEngineering.CalendrierRepublicain;

var repCalendar = new FrenchRepublicanCalendar();
var date = new DateTime(8, 2, 18, repCalendar);

Console.WriteLine(date.ToString("d", DateTimeFormatInfo.InvariantInfo));
// Output: 11/9/1799
```

### Writing Dates

You can format French Republican dates using the `FrenchRepublicanDateTimeFormatter` as a format provider with `String.Format`. This enables custom and standard date patterns for output in the Republican calendar.

```cs
using LinkeEngineering.CalendrierRepublicain;

var formatter = new FrenchRepublicanDateTimeFormatter();
var date = new DateTime(8, 2, 18, repCalendar);

Console.WriteLine(String.Format(formatter, "{0:D}", date));
// Output: Octidi, Brumaire 18, VIII

Console.WriteLine(String.Format(formatter, "{0:d MMM. 'an' yyyy}", date));
// Output: 18 Brum. an VIII
```

#### Supported patterns:  

- `"D"`: Long date format (Republican day name, month name, day, year in Roman numerals)
- `"d"`: Short date format (day, abbreviated month, year)
- Custom patterns using:  
  - `d` (day number)
  - `dddd` (Republican day name)
  - `MMM`/`MMMM` (abbreviated/full month name)
  - `yyyy` (year in Roman numerals)
  - `H`/`HH` (hour)
  - `mm` (minute)
  - `ss` (second)

**Remarks:**  
- Only dates created with `FrenchRepublicanCalendar` are supported.
- Formatting outside the supported date range will throw an exception.
- Not all .NET date/time patterns are supported; use only the patterns listed above.

### Working with the Calendar

The `FrenchRepublicanCalendar` class derives from `System.Globalization.Calendar` and provides all standard calendar operations for the French Republican calendar. You can add days, weeks, months, or years, and retrieve calendar-specific information.

```cs
using LinkeEngineering.CalendrierRepublicain;

var calendar = new FrenchRepublicanCalendar();
var formatter = new FrenchRepublicanDateTimeFormatter();

var date = new DateTime(8, 2, 18, calendar);  // 18 Brumaire VIII

// Add 4 Republican weeks (40 days)
date = calendar.AddWeeks(date, 4);
Console.WriteLine(String.Format(formatter, "{0:d MMMM yyyy}", date));
// Output: 28 Frimaire VIII

// Get the Republican year and month
var year = calendar.GetYear(date);   // 8
var month = calendar.GetMonth(date); // 3 (Frimaire)
```

## License

This library is licensed under the MIT License, with additional terms restricting the use of the original package name for modified versions. Please refer to the [license file](https://github.com/linke-engineering/calendrier-republicain/blob/main/assets/License.txt) for details.
