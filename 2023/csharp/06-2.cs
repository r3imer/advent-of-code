using System.Text.RegularExpressions;

namespace r3im.aof.d06;

public static class Task2 {

    public static string run() {
        var regex = new Regex(@"\d+");
        var lines = input.Split('\n');
        var digits = lines
            .Select(x => regex
                .Matches(x)
                .Select(x =>x.Value)
                .JoinToString("")
            )
            .Select(x => long.Parse(x))
            .ToArray()
            ;

        ulong solutions = 0;

        var time = digits[0];
        var dist = digits[1];
        for (int t=1; t<time; t++) {
            if ((time - t) * t > dist) {
                solutions++;
            }
        }

        return solutions.ToString();
    }

    static string input = 
        // "";
        File.ReadAllText("06.txt").Trim();

// """
// Time:      7  15   30
// Distance:  9  40  200
// """;

}
