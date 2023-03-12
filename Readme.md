# Sinistrius.CalendrierRepublicain

A .NET library which integrates the French Republican calendar


## Installation

Use the NuGet package manager to install the package.


## Usage

### Restrictions

The library sticks with the features that were officially adopted in France between 1 Vendémiaire I (i.e. September 22, 1792) and 10 Nivôse XIV (i.e. December 31, 1805). It therefore doesn't work for dates beyond that validity period, and likewise it doesn't support decimal time.


### Creating Dates

Instantiate a *DateTime* object with Republican date values using the calendar object:

```cs
using Sinistrius.CalendrierRepublicain;

FrenchRepublicanCalendar() repCalendar = new();
DateTime date = new(8, 2, 18, repCalendar);
Console.WriteLine(date.ToString("d", DateTimeFormatInfo.InvariantInfo));
// Displays 11/9/1799
```


### Writing Dates

Get a string representation of a Republican date by using the *String.Format()* method with a format provider and format string. Please note that only a few date and time patterns are supported.

```cs
using Sinistrius.CalendrierRepublicain;

FrenchRepublicanDateTimeFormatter provider = new();
DateTime date = new(8, 2, 18, repCalendar);

Console.WriteLine(String.Format(provider, "{0:D}", date));
// Displays Octidi, Brumaire 18, VIII

Console.WriteLine(String.Format(provider, "{0:d MMM. 'an' yyyy}", date));
// Displays 18 Brum. an VIII
```


### Working with the Calendar

The *FrenchRepublicanCalendar* class is derived from *System.Globalization.Calendar* and provides the same functionality.

```cs
using Sinistrius.CalendrierRepublicain;

FrenchRepublicanCalendar calendar = new();
FrenchRepublicanDateTimeFormatter provider = new();

DateTime date = new(8, 2, 18, calendar);  // 18 Brumaire VIII
int weeks = 4;                            // 4 Republican weeks (40 days) to add

date = calendar.AddWeeks(date, weeks);
Console.WriteLine(String.Format(provider, "{0:d MMMM yyyy}", date));
// Displays 28 Frimaire VIII
```


## License

This work is licensed under a [Creative Commons Attribution-NoDerivatives 4.0 International License](http://creativecommons.org/licenses/by-nd/4.0/).
