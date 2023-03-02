using Sinistrius.NumeriRomani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// A formatter 
/// for a <see cref="FrenchRepublicanDateTime"/>.
/// </summary>
internal class FrenchRepublicanDateTimeFormatter : IFormatProvider, ICustomFormatter
{

    /// <inheritdoc/>
    public object? GetFormat(Type? formatType)
    {
        if (formatType == typeof(ICustomFormatter))
        {
            return this;
        }
        else
        {
            return null;
        }
    }


    /// <inheritdoc/>
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        // Validate arguments
        if (format == null)
        {
            throw new ArgumentNullException(nameof(format));
        }

        if (arg == null)
        {
            throw new ArgumentNullException(nameof(arg));
        }

        // Convert to Republican date
        FrenchRepublicanDateTime repTime;

        if (arg.GetType() == typeof(FrenchRepublicanDateTime))
        {
            repTime = (FrenchRepublicanDateTime)arg;
        }
        else if (arg.GetType() == typeof(DateTime))
        {
            repTime = ((DateTime)arg).GetFrenchRepublicanTime();
        }
        else
        {
            throw new ArgumentException($"The argument must be of type {typeof(DateTime).Name} or {typeof(FrenchRepublicanDateTime).Name}.", nameof(arg));
        }

        // Format date according to format string
        switch (format)
        {
            case "D":
                string year = repTime.Year.ToRoman();
                string day = repTime.Day.ToString();
                break;

            default:
                throw new NotSupportedException($"The format string \"{format}\" is not supported.");
        }

        throw new NotImplementedException();
    }

}
