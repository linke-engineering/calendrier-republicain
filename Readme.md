# Calendrier Républicain

A modern .NET library for converting and formatting dates between the Gregorian and French Republican calendars. It provides a drop-in replacement for the standard calendar and formatting infrastructure, enabling seamless integration into .NET applications.

The library is published as a NuGet package and designed for use in other .NET projects.

## Installation

To install the library via NuGet, use the following command:

```bash
# Using the .NET CLI
> dotnet add package LinkeEngineering.CalendrierRepublicain --version 2.0.0
```

Alternatively, you can search for `LinkeEngineering.CalendrierRepublicain` in the NuGet Package Manager in Visual Studio.

## Compatibility

This library targets `.NET 8` and `.NET 10`. Ensure your project is compatible with one of these frameworks before integrating the library.

## Dependencies

The library depends on the `LinkeEngineering.NumeriRomani` package (version 1.1.5) for handling Roman numeral formatting. This dependency is automatically resolved when you install the package.

## Usage

### Restrictions

The library strictly implements the official features of the French Republican calendar, valid only between Vendémiaire 1, I (September 22, 1792) and Nivôse 10, XIV (December 31, 1805). Dates outside this range and decimal time are not supported.

### Creating Republican Dates

You can create a `DateTime` object using the `FrenchRepublicanCalendar` class, which derives from `System.Globalization.Calendar`. This allows you to work with French Republican dates in all standard .NET APIs that support custom calendars.

```csharp
using LinkeEngineering.CalendrierRepublicain;

var repCalendar = new FrenchRepublicanCalendar();
var date = new DateTime(8, 2, 18, repCalendar);

Console.WriteLine(date.ToString("d", DateTimeFormatInfo.InvariantInfo));
// Output: 11/9/1799
```

### Working with the Calendar

The `FrenchRepublicanCalendar` provides all standard calendar operations for the French Republican calendar. You can add days, weeks, months, or years, and retrieve calendar-specific information.

```csharp
using LinkeEngineering.CalendrierRepublicain;

var repCalendar = new FrenchRepublicanCalendar();
var date = new DateTime(8, 2, 18, repCalendar);  // 18 Brumaire VIII

// Add 4 Republican weeks (40 days)
date = repCalendar.AddWeeks(date, 4);  // 28 Frimaire VIII

// Get the Republican year and month
var year = repCalendar.GetYear(date);    // 8
var month = repCalendar.GetMonth(date);  // 3 (i.e. Frimaire)
```

### Formatting Dates

You can format French Republican dates using the `FrenchRepublicanDateTimeFormatter` as a format provider with `String.Format`. This enables both standard and custom date/time format strings for output in the Republican calendar. As with other .NET calendars, the culture determines the order of elements in standard format strings, while custom format strings allow you to specify the exact format.

```csharp
using LinkeEngineering.CalendrierRepublicain;

var repCalendar = new FrenchRepublicanCalendar();
var repFormatter = new FrenchRepublicanDateTimeFormatter();
var date = new DateTime(8, 2, 18, repCalendar);

Console.WriteLine(String.Format(repFormatter, "{0:D}", date));
// Output: Octidi 18 Brumaire VIII

Console.WriteLine(String.Format(repFormatter, "{0:dddd, dd MM yy}", date));
// Output: Octidi, 18 02 08

var complementaryDate = new DateTime(3, 13, 6, repCalendar);
Console.WriteLine(String.Format(repFormatter, "{0:dddd, dd MMMM 'an' yyyy}", complementaryDate));
// Output: Jour de la révolution an III
```

**Remarks:**  
- Format strings that handle parts of seconds, time zones, or eras are not supported, as these concepts did not exist in the French Republican calendar. Using an unsupported standard format string like `O` results in a `FormatException`, while unsupported custom format strings like `tt` will be ignored in the output.
- The names of months and days are not translated, as they are specific to the calendar and do not have equivalents in other languages. They will always be displayed in their original French form, regardless of the culture used for formatting.
- The names of months and days are never abbreviated and are always displayed in their full form.
- Dates that fall in the complementary days (Sansculottides) will be formatted as a 13th month in numeric representation, and with the festival name in textual representation, e.g., `13/1/4` or `Jour de la vertu, IV`. There are no weekday names for these days.
- Time is always represented in 24-hour format, as the concept of AM/PM did not exist. Format specifiers for 12-hour time and AM/PM will be transformed to their 24-hour equivalents, e.g., `h` will be treated as `H`, and `tt` will be ignored.

#### Supported Standard Format Strings

| Format specifier | Description             | Examples                                                                                  |
|------------------|-------------------------|-------------------------------------------------------------------------------------------|
| `d`              | Short date pattern      | fr-FR: `18/02/08`<br>en-US: `2/18/08`                                                     |
| `D`              | Long date pattern       | fr-FR: `Octidi 18 Brumaire VIII`<br>en-US: `Octidi, Brumaire 18, VIII`                    |
| `f`              | Long date + short time  | fr-FR: `Octidi, 18 Brumaire VIII 14:30`<br>en-US: `Octidi, Brumaire 18, VIII 14:30`       |
| `F`              | Full date/time pattern  | fr-FR: `Octidi, 18 Brumaire VIII 14:30:00`<br>en-US: `Octidi, Brumaire 18, VIII 14:30:00` |
| `g`              | Short date + short time | fr-FR: `18/02/08 14:30`<br>en-US: `2/18/08 14:30`                                         |
| `G`              | Short date + long time  | fr-FR: `18/02/08 14:30:00`<br>en-US: `2/18/08 14:30:00`                                   |
| `m`, `M`         | Month/day pattern       | fr-FR: `18 Brumaire`<br>en-US: `Brumaire 18`                                              |
| `t`              | Short time pattern      | fr-FR: `14:30`<br>en-US: `14:30`                                                          |
| `T`              | Long time pattern       | fr-FR: `14:30:00`<br>en-US: `14:30:00`                                                    |
| `y`, `Y`         | Year/month pattern      | fr-FR: `Nivôse XIV`<br>en-US: `Nivôse XIV`                                                |

#### Supported Custom Format Strings

| Format specifier       | Description                          | Examples       |
|------------------------|--------------------------------------|----------------|
| `d`                    | Day of month (1 or 2 digits)         | `1`, `18`      |
| `dd`                   | Day of month (2 digits)              | `01`, `18`     |
| `ddd`, `dddd`          | Name of the day of the decade        | `Octidi`       |
| `h`, `H`               | Hour, 24-hour format (1 or 2 digits) | `0`, `14`      |
| `hh`, `HH`             | Hour, 24-hour format (2 digits)      | `00`, `14`     |
| `m`                    | Minute (1 or 2 digits)               | `0`, `30`      |
| `mm`                   | Minute (2 digits)                    | `00`, `30`     |
| `M`                    | Month of year (1 or 2 digits)        | `2`, `10`      |
| `MM`                   | Month of year (2 digits)             | `02`, `10`     |
| `MMM`, `MMMM`          | Name of the month                    | `Brumaire`     |
| `s`                    | Second (1 or 2 digits)               | `0`, `45`      |
| `ss`                   | Second (2 digits)                    | `00`, `45`     |
| `y`                    | Year (1 or 2 digits)                 | `8`, `12`      |
| `yy`                   | Year (2 digits)                      | `08`, `12`     |
| `yyy`, `yyyy`, `yyyyy` | Year (Roman numerals)                | `VIII`         |

## License

This library is licensed under the MIT License, with additional terms restricting the use of the original package name for modified versions. Please refer to the [license file](https://github.com/linke-engineering/calendrier-republicain/blob/main/assets/License.txt) for details.
