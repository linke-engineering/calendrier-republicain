using System.Collections.Generic;


namespace LinkeEngineering.CalendrierRepublicain.UnitTests;


/// <summary>
/// Defines a collection of test cases for verifying the correct formatting of date and time values in the French 
/// Republican calendar, including standard and custom format strings across different cultures.
/// </summary>
/// <remarks>
/// Save this file in "Unicode (UTF-8 with signature)" encoding to avoid issues with accented characters in the CI 
/// workflow.
/// </remarks>
internal class TestData
{

    /// <summary>
    /// Represents the minimum supported date and time value for the French Republican calendar.
    /// </summary>
    private static readonly DateTime Time1 = new(1, 1, 1, 0, 0, 0, new FrenchRepublicanCalendar());


    /// <summary>
    /// Represents a leap day in the French Republican calendar.
    /// </summary>
    private static readonly DateTime Time2 = new(3, 13, 6, 14, 25, 36, new FrenchRepublicanCalendar());


    /// <summary>
    /// Represents the maximum supported date and time value for the French Republican calendar.
    /// </summary>
    private static readonly DateTime Time3 = new(14, 4, 10, 23, 59, 59, new FrenchRepublicanCalendar());


    /// <summary>
    /// Represents the culture name for French (France).
    /// </summary>
    private static readonly string FR = "fr-FR";


    /// <summary>
    /// Represents the culture name for German (Germany).
    /// </summary>
    private static readonly string DE = "de-DE";


    /// <summary>
    /// Represents the culture name for United States English.
    /// </summary>
    private static readonly string US = "en-US";


    /// <summary>
    /// The collection of test cases for date formatting.
    /// </summary>
    internal static IReadOnlyList<(string FormatString, DateTime Time, string Culture, string Expected)> Data { get; } =
    [

        #region Standard format strings

        #region Standard format string "d"

        ("d", Time1, FR, "01/01/01"),
        ("d", Time1, DE, "01.01.01"),
        ("d", Time1, US, "1/1/01"),
        ("d", Time2, FR, "06/13/03"),
        ("d", Time2, DE, "06.13.03"),
        ("d", Time2, US, "13/6/03"),
        ("d", Time3, FR, "10/04/14"),
        ("d", Time3, DE, "10.04.14"),
        ("d", Time3, US, "4/10/14"),

        #endregion

        #region Standard format string "D"

        ("D", Time1, FR, "Primidi 1 Vendémiaire I"),
        ("D", Time1, DE, "Primidi, 1. Vendémiaire I"),
        ("D", Time1, US, "Primidi, Vendémiaire 1, I"),
        ("D", Time2, FR, "Jour de la révolution III"),
        ("D", Time2, DE, "Jour de la révolution III"),
        ("D", Time2, US, "Jour de la révolution, III"),
        ("D", Time3, FR, "Décadi 10 Nivôse XIV"),
        ("D", Time3, DE, "Décadi, 10. Nivôse XIV"),
        ("D", Time3, US, "Décadi, Nivôse 10, XIV"),

        #endregion

        #region Standard format string "f"

        ("f", Time1, FR, "Primidi 1 Vendémiaire I 00:00"),
        ("f", Time1, DE, "Primidi, 1. Vendémiaire I 00:00"),
        ("f", Time1, US, "Primidi, Vendémiaire 1, I 0:00"),
        ("f", Time2, FR, "Jour de la révolution III 14:25"),
        ("f", Time2, DE, "Jour de la révolution III 14:25"),
        ("f", Time2, US, "Jour de la révolution, III 14:25"),
        ("f", Time3, FR, "Décadi 10 Nivôse XIV 23:59"),
        ("f", Time3, DE, "Décadi, 10. Nivôse XIV 23:59"),
        ("f", Time3, US, "Décadi, Nivôse 10, XIV 23:59"),

        #endregion

        #region Standard format string "F"

        ("F", Time1, FR, "Primidi 1 Vendémiaire I 00:00:00"),
        ("F", Time1, DE, "Primidi, 1. Vendémiaire I 00:00:00"),
        ("F", Time1, US, "Primidi, Vendémiaire 1, I 0:00:00"),
        ("F", Time2, FR, "Jour de la révolution III 14:25:36"),
        ("F", Time2, DE, "Jour de la révolution III 14:25:36"),
        ("F", Time2, US, "Jour de la révolution, III 14:25:36"),
        ("F", Time3, FR, "Décadi 10 Nivôse XIV 23:59:59"),
        ("F", Time3, DE, "Décadi, 10. Nivôse XIV 23:59:59"),
        ("F", Time3, US, "Décadi, Nivôse 10, XIV 23:59:59"),

        #endregion

        #region Standard format string "g"

        ("g", Time1, FR, "01/01/01 00:00"),
        ("g", Time1, DE, "01.01.01 00:00"),
        ("g", Time1, US, "1/1/01 0:00"),
        ("g", Time2, FR, "06/13/03 14:25"),
        ("g", Time2, DE, "06.13.03 14:25"),
        ("g", Time2, US, "13/6/03 14:25"),
        ("g", Time3, FR, "10/04/14 23:59"),
        ("g", Time3, DE, "10.04.14 23:59"),
        ("g", Time3, US, "4/10/14 23:59"),

#endregion

        #region Standard format string "G"

        ("G", Time1, FR, "01/01/01 00:00:00"),
        ("G", Time1, DE, "01.01.01 00:00:00"),
        ("G", Time1, US, "1/1/01 0:00:00"),
        ("G", Time2, FR, "06/13/03 14:25:36"),
        ("G", Time2, DE, "06.13.03 14:25:36"),
        ("G", Time2, US, "13/6/03 14:25:36"),
        ("G", Time3, FR, "10/04/14 23:59:59"),
        ("G", Time3, DE, "10.04.14 23:59:59"),
        ("G", Time3, US, "4/10/14 23:59:59"),

        #endregion

        #region Standard format string "m" or "M"

        ("m", Time1, FR, "1 Vendémiaire"),
        ("m", Time1, DE, "1. Vendémiaire"),
        ("m", Time1, US, "Vendémiaire 1"),
        ("m", Time2, FR, "Jour de la révolution"),
        ("m", Time2, DE, "Jour de la révolution"),
        ("m", Time2, US, "Jour de la révolution"),
        ("m", Time3, FR, "10 Nivôse"),
        ("m", Time3, DE, "10. Nivôse"),
        ("m", Time3, US, "Nivôse 10"),

        ("M", Time1, FR, "1 Vendémiaire"),
        ("M", Time1, DE, "1. Vendémiaire"),
        ("M", Time1, US, "Vendémiaire 1"),
        ("M", Time2, FR, "Jour de la révolution"),
        ("M", Time2, DE, "Jour de la révolution"),
        ("M", Time2, US, "Jour de la révolution"),
        ("M", Time3, FR, "10 Nivôse"),
        ("M", Time3, DE, "10. Nivôse"),
        ("M", Time3, US, "Nivôse 10"),

        #endregion

        #region Standard format string "t"

        ("t", Time1, FR, "00:00"),
        ("t", Time1, DE, "00:00"),
        ("t", Time1, US, "0:00"),
        ("t", Time2, FR, "14:25"),
        ("t", Time2, DE, "14:25"),
        ("t", Time2, US, "14:25"),
        ("t", Time3, FR, "23:59"),
        ("t", Time3, DE, "23:59"),
        ("t", Time3, US, "23:59"),

        #endregion

        #region Standard format string "T"

        ("T", Time1, FR, "00:00:00"),
        ("T", Time1, DE, "00:00:00"),
        ("T", Time1, US, "0:00:00"),
        ("T", Time2, FR, "14:25:36"),
        ("T", Time2, DE, "14:25:36"),
        ("T", Time2, US, "14:25:36"),
        ("T", Time3, FR, "23:59:59"),
        ("T", Time3, DE, "23:59:59"),
        ("T", Time3, US, "23:59:59"),

        #endregion

        #region Standard format string "y" or "Y"

        ("y", Time1, FR, "Vendémiaire I"),
        ("y", Time1, DE, "Vendémiaire I"),
        ("y", Time1, US, "Vendémiaire I"),
        ("y", Time2, FR, "Jours complémentaires III"),
        ("y", Time2, DE, "Jours complémentaires III"),
        ("y", Time2, US, "Jours complémentaires III"),
        ("y", Time3, FR, "Nivôse XIV"),
        ("y", Time3, DE, "Nivôse XIV"),
        ("y", Time3, US, "Nivôse XIV"),

        ("Y", Time1, FR, "Vendémiaire I"),
        ("Y", Time1, DE, "Vendémiaire I"),
        ("Y", Time1, US, "Vendémiaire I"),
        ("Y", Time2, FR, "Jours complémentaires III"),
        ("Y", Time2, DE, "Jours complémentaires III"),
        ("Y", Time2, US, "Jours complémentaires III"),
        ("Y", Time3, FR, "Nivôse XIV"),
        ("Y", Time3, DE, "Nivôse XIV"),
        ("Y", Time3, US, "Nivôse XIV"),

        #endregion

        #endregion

        #region Custom format strings

        #region Custom format string "yyyyy"

        ("yyyyy", Time1, FR, "I"),
        ("yyyyy", Time1, DE, "I"),
        ("yyyyy", Time1, US, "I"),
        ("yyyyy", Time2, FR, "III"),
        ("yyyyy", Time2, DE, "III"),
        ("yyyyy", Time2, US, "III"),
        ("yyyyy", Time3, FR, "XIV"),
        ("yyyyy", Time3, DE, "XIV"),
        ("yyyyy", Time3, US, "XIV"),

        #endregion

        #region Custom format string "yyyy"

        ("yyyy", Time1, FR, "I"),
        ("yyyy", Time1, DE, "I"),
        ("yyyy", Time1, US, "I"),
        ("yyyy", Time2, FR, "III"),
        ("yyyy", Time2, DE, "III"),
        ("yyyy", Time2, US, "III"),
        ("yyyy", Time3, FR, "XIV"),
        ("yyyy", Time3, DE, "XIV"),
        ("yyyy", Time3, US, "XIV"),

        #endregion

        #region Custom format string "yyy"

        ("yyy", Time1, FR, "I"),
        ("yyy", Time1, DE, "I"),
        ("yyy", Time1, US, "I"),
        ("yyy", Time2, FR, "III"),
        ("yyy", Time2, DE, "III"),
        ("yyy", Time2, US, "III"),
        ("yyy", Time3, FR, "XIV"),
        ("yyy", Time3, DE, "XIV"),
        ("yyy", Time3, US, "XIV"),

        #endregion

        #region Custom format string "yy"
        
        ("yy", Time1, FR, "01"),
        ("yy", Time1, DE, "01"),
        ("yy", Time1, US, "01"),
        ("yy", Time2, FR, "03"),
        ("yy", Time2, DE, "03"),
        ("yy", Time2, US, "03"),
        ("yy", Time3, FR, "14"),
        ("yy", Time3, DE, "14"),
        ("yy", Time3, US, "14"),

        #endregion

        #region Custom format string "y"

        ("%y", Time1, FR, "1"),
        ("%y", Time1, DE, "1"),
        ("%y", Time1, US, "1"),
        ("%y", Time2, FR, "3"),
        ("%y", Time2, DE, "3"),
        ("%y", Time2, US, "3"),
        ("%y", Time3, FR, "14"),
        ("%y", Time3, DE, "14"),
        ("%y", Time3, US, "14"),

        #endregion

        #region Custom format string "MMMM"

        ("MMMM", Time1, FR, "Vendémiaire"),
        ("MMMM", Time1, DE, "Vendémiaire"),
        ("MMMM", Time1, US, "Vendémiaire"),
        ("MMMM", Time2, FR, "Jours complémentaires"),
        ("MMMM", Time2, DE, "Jours complémentaires"),
        ("MMMM", Time2, US, "Jours complémentaires"),
        ("MMMM", Time3, FR, "Nivôse"),
        ("MMMM", Time3, DE, "Nivôse"),
        ("MMMM", Time3, US, "Nivôse"),

        #endregion

        #region Custom format string "MMM"

        ("MMM", Time1, FR, "Vendémiaire"),
        ("MMM", Time1, DE, "Vendémiaire"),
        ("MMM", Time1, US, "Vendémiaire"),
        ("MMM", Time2, FR, "Jours complémentaires"),
        ("MMM", Time2, DE, "Jours complémentaires"),
        ("MMM", Time2, US, "Jours complémentaires"),
        ("MMM", Time3, FR, "Nivôse"),
        ("MMM", Time3, DE, "Nivôse"),
        ("MMM", Time3, US, "Nivôse"),

        #endregion

        #region Custom format string "MM"

        ("MM", Time1, FR, "01"),
        ("MM", Time1, DE, "01"),
        ("MM", Time1, US, "01"),
        ("MM", Time2, FR, "13"),
        ("MM", Time2, DE, "13"),
        ("MM", Time2, US, "13"),
        ("MM", Time3, FR, "04"),
        ("MM", Time3, DE, "04"),
        ("MM", Time3, US, "04"),

        #endregion

        #region Custom format string "M"

        ("%M", Time1, FR, "1"),
        ("%M", Time1, DE, "1"),
        ("%M", Time1, US, "1"),
        ("%M", Time2, FR, "13"),
        ("%M", Time2, DE, "13"),
        ("%M", Time2, US, "13"),
        ("%M", Time3, FR, "4"),
        ("%M", Time3, DE, "4"),
        ("%M", Time3, US, "4"),

        #endregion

        #region Custom format string "dddd"
        
        ("dddd", Time1, FR, "Primidi"),
        ("dddd", Time1, DE, "Primidi"),
        ("dddd", Time1, US, "Primidi"),
        ("dddd", Time2, FR, String.Empty),
        ("dddd", Time2, DE, String.Empty),
        ("dddd", Time2, US, String.Empty),
        ("dddd", Time3, FR, "Décadi"),
        ("dddd", Time3, DE, "Décadi"),
        ("dddd", Time3, US, "Décadi"),

        #endregion

        #region Custom format string "ddd"
        
        ("ddd", Time1, FR, "Primidi"),
        ("ddd", Time1, DE, "Primidi"),
        ("ddd", Time1, US, "Primidi"),
        ("ddd", Time2, FR, String.Empty),
        ("ddd", Time2, DE, String.Empty),
        ("ddd", Time2, US, String.Empty),
        ("ddd", Time3, FR, "Décadi"),
        ("ddd", Time3, DE, "Décadi"),
        ("ddd", Time3, US, "Décadi"),

        #endregion

        #region Custom format string "dd"

        ("dd", Time1, FR, "01"),
        ("dd", Time1, DE, "01"),
        ("dd", Time1, US, "01"),
        ("dd", Time2, FR, "06"),
        ("dd", Time2, DE, "06"),
        ("dd", Time2, US, "06"),
        ("dd", Time3, FR, "10"),
        ("dd", Time3, DE, "10"),
        ("dd", Time3, US, "10"),

        #endregion

        #region Custom format string "d"

        ("%d", Time1, FR, "1"),
        ("%d", Time1, DE, "1"),
        ("%d", Time1, US, "1"),
        ("%d", Time2, FR, "6"),
        ("%d", Time2, DE, "6"),
        ("%d", Time2, US, "6"),
        ("%d", Time3, FR, "10"),
        ("%d", Time3, DE, "10"),
        ("%d", Time3, US, "10"),

        #endregion

        #region Custom format string "hh"

        ("hh", Time1, FR, "00"),
        ("hh", Time1, DE, "00"),
        ("hh", Time1, US, "00"),
        ("hh", Time2, FR, "14"),
        ("hh", Time2, DE, "14"),
        ("hh", Time2, US, "14"),
        ("hh", Time3, FR, "23"),
        ("hh", Time3, DE, "23"),
        ("hh", Time3, US, "23"),

        #endregion

        #region Custom format string "h"

        ("%h", Time1, FR, "0"),
        ("%h", Time1, DE, "0"),
        ("%h", Time1, US, "0"),
        ("%h", Time2, FR, "14"),
        ("%h", Time2, DE, "14"),
        ("%h", Time2, US, "14"),
        ("%h", Time3, FR, "23"),
        ("%h", Time3, DE, "23"),
        ("%h", Time3, US, "23"),

        #endregion

        #region Custom format string "HH"

        ("HH", Time1, FR, "00"),
        ("HH", Time1, DE, "00"),
        ("HH", Time1, US, "00"),
        ("HH", Time2, FR, "14"),
        ("HH", Time2, DE, "14"),
        ("HH", Time2, US, "14"),
        ("HH", Time3, FR, "23"),
        ("HH", Time3, DE, "23"),
        ("HH", Time3, US, "23"),
        #endregion

        #region Custom format string "H"

        ("%H", Time1, FR, "0"),
        ("%H", Time1, DE, "0"),
        ("%H", Time1, US, "0"),
        ("%H", Time2, FR, "14"),
        ("%H", Time2, DE, "14"),
        ("%H", Time2, US, "14"),
        ("%H", Time3, FR, "23"),
        ("%H", Time3, DE, "23"),
        ("%H", Time3, US, "23"),

        #endregion

        #region Custom format string "mm"

        ("mm", Time1, FR, "00"),
        ("mm", Time1, DE, "00"),
        ("mm", Time1, US, "00"),
        ("mm", Time2, FR, "25"),
        ("mm", Time2, DE, "25"),
        ("mm", Time2, US, "25"),
        ("mm", Time3, FR, "59"),
        ("mm", Time3, DE, "59"),
        ("mm", Time3, US, "59"),
        
        #endregion

        #region Custom format string "m"

        ("%m", Time1, FR, "0"),
        ("%m", Time1, DE, "0"),
        ("%m", Time1, US, "0"),
        ("%m", Time2, FR, "25"),
        ("%m", Time2, DE, "25"),
        ("%m", Time2, US, "25"),
        ("%m", Time3, FR, "59"),
        ("%m", Time3, DE, "59"),
        ("%m", Time3, US, "59"),

        #endregion

        #region Custom format string "ss"

        ("ss", Time1, FR, "00"),
        ("ss", Time1, DE, "00"),
        ("ss", Time1, US, "00"),
        ("ss", Time2, FR, "36"),
        ("ss", Time2, DE, "36"),
        ("ss", Time2, US, "36"),
        ("ss", Time3, FR, "59"),
        ("ss", Time3, DE, "59"),
        ("ss", Time3, US, "59"),

        #endregion

        #region Custom format string "s"

        ("%s", Time1, FR, "0"),
        ("%s", Time1, DE, "0"),
        ("%s", Time1, US, "0"),
        ("%s", Time2, FR, "36"),
        ("%s", Time2, DE, "36"),
        ("%s", Time2, US, "36"),
        ("%s", Time3, FR, "59"),
        ("%s", Time3, DE, "59"),
        ("%s", Time3, US, "59"),

        #endregion

        #endregion

        #region Freely composed custom format strings

        ("MMMM an yyyy", Time1, FR, "Vendémiaire an I"),
        ("MMMM an yyyy", Time2, DE, "Jours complémentaires an III"),
        ("MMMM an yyyy", Time3, US, "Nivôse an XIV"),
        ("ddd, dd. MMMM yyyy", Time2, FR, "Jour de la révolution III"),
        ("'Born' d MMM., 'year' yyyy", Time3, US, "Born 10 Nivôse, year XIV"),
        ("dddd \\y hh:mm tt", Time3, FR, "Décadi y 23:59"),

        #endregion

    ];

}
