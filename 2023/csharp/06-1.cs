using System.Text.RegularExpressions;

namespace r3im.aof.d06;

public static class Task1 {

    public static string run() {
        var regex = new Regex(@"\d+");
        var lines = input.Split('\n');
        var digits = lines
            .Select(x => regex.Matches(x).Select(x =>int.Parse(x.Value)).ToArray())
            .ToArray()
            ;

        var length = digits[0].Length;
        long[] solutions = new long[length];

        for (int i=0; i<length; i++) {
            var time = digits[0][i];
            var dist = digits[1][i];
            for (int t=1; t<time; t++) {
                if ((time - t) * t > dist) {
                    solutions[i]++;
                }
            }
        }

        return solutions.Aggregate((acc,x)=>acc*x).ToString();
    }

    static string input = 
        // "";
        File.ReadAllText("06.txt").Trim();

// """
// Time:      7  15   30
// Distance:  9  40  200
// """;

}
