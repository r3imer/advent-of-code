using System.Text.RegularExpressions;

namespace r3im.aof.d04;

public static class Task1 {
    static Regex digit = new Regex(@"\d+");

    public static string run() {
        var lines = input.Split('\n');
        var output = lines
            .Select(x => x.Split([':','|']))
            .Select(x => (
                win: x[1].extract(), 
                my:  x[2].extract()
            ))
            .Select(x => x.win.Select(w=>x.my.Contains(w)).Where(x=>x is true).Count())
            .Select(x => x > 0 ? Math.Pow(2,x-1) : 0)
            .Sum()
            .ToString()
            ;
            
        return output;
    }

    static string[] extract(this string x) {
        return digit.Matches(x).Select(x=>x.Value).ToArray();
    }

    static string input = 
        // "";
        File.ReadAllText("04.txt").Trim();

// """
// Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
// Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
// Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
// Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
// Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
// Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
// """;

}
