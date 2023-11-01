using System.Data;
using Barlines;

namespace Utils;

public static class RecordDisplay
{
    public static void PresentRulesResults(Model.SimpleRecord[] results)
    {
        presentResultStatistics(results);

        presentCountryDistribution(results);
        presentIncorporationDateDistribution(results);
    }

    // ----------------------------------------------------------------------------------------------------
    // Present basic statistics on the execution run
    private static void presentResultStatistics(Model.SimpleRecord[] results)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Execution Results:");
        Console.ResetColor();

        Console.WriteLine("Total Records: {0}", results.Length);
    }

    // ----------------------------------------------------------------------------------------------------
    // Present Country Distribution
    private static void presentCountryDistribution(Model.SimpleRecord[] results)
    {
        var formatter = BarLineFactory.CreateBarLine(BarLineFactory.BarType.Default);
        formatter.LeadStringFormat = "{0, -10} ";
        formatter.FollowingStringFormat = " - {0, 5}/{1}\n";

        var dist = from r in results
                   group r by r.IncorporationCountryCode into cc
                   orderby cc.Key
                   select new
                   {
                       Rule = cc.Key,
                       Matches = cc.Count()
                   };

        Console.WriteLine("\nCountries\n");

        foreach (var d in dist)
        {
            var ruleTotal = (float)results.Length;
            var value = (float)d.Matches / ruleTotal;

            formatter.SetLeadData(d.Rule);
            formatter.SetFollowingData(d.Matches, ruleTotal);
            formatter.DisplayBar(value);
        }
    }

    // ----------------------------------------------------------------------------------------------------
    // Present Incorporation date Distribution
    private static void presentIncorporationDateDistribution(Model.SimpleRecord[] results)
    {
        var distColours = new Dictionary<float, ConsoleColor> {
            {0.0f, ConsoleColor.DarkBlue},
            {0.1f, ConsoleColor.Blue},
            {0.15f, ConsoleColor.Green},
            {0.5f, ConsoleColor.DarkYellow},
            {1.10f, ConsoleColor.DarkRed},
        };

        var formatter = BarLineFactory.CreateBarLine(BarLineFactory.BarType.Colour, colours: distColours);
        formatter.LeadStringFormat = "{0, -10} ";
        formatter.FollowingStringFormat = " - {0, 5}/{1}";

        var dist = from r in results
                   group r by r.IncorporationDate.Year into cc
                   orderby cc.Key
                   select new
                   {
                       Rule = cc.Key,
                       Matches = cc.Count()
                   };

        Console.WriteLine("\nIncorporation Years\n");

        var ruleTotal = (float)results.Length;

        foreach (var d in dist)
        {
            var value = (float)d.Matches / ruleTotal;

            formatter.SetLeadData(d.Rule);
            formatter.SetFollowingData(d.Matches, ruleTotal);

            formatter.DisplayBar(value);
        }
    }
}