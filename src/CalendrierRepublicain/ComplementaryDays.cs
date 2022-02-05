using System.ComponentModel;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// The names of the complementary days at the end of each year.
/// </summary>
internal enum ComplementaryDays
{

    [Description("jour de la vertu")]
    JourDeLaVertu,

    [Description("jour du génie")]
    JourDuGenie,

    [Description("jour du travail")]
    JourDuTravail,

    [Description("jour de l’opinion")]
    JourDeLOpinion,

    [Description("jour des récompenses")]
    JourDesRecompenses,

    [Description("jour de la Révolution")]
    JourDeLaRevolution

}
