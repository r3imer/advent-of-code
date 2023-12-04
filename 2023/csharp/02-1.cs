using System.Text.RegularExpressions;

namespace r3im.aof.d02;

public static class Task1 {

    public static string run() {
        var blue = new Regex(@"(\d+) blue");
        var red = new Regex(@"(\d+) red");
        var green = new Regex(@"(\d+) green");
        var lines = input.Split('\n');
        var output = lines
            .Select((x,i) => new {
                id    = i + 1,
                blue  = blue.Matches(x).max(), 
                red   = red.Matches(x).max(), 
                green = green.Matches(x).max(),
            })
            .Where(x=>x.red <= 12 && x.green <= 13 && x.blue <= 14)
            .Select(x => x.id)
            .Sum()
            .ToString()
            ;
            
        return output;
    }

    static int max(this MatchCollection x) 
        => x.Select(x=>x.Groups.Values.Select(x=>x.Value).Last()).Select(x=>int.Parse(x)).Max();

    static string input = 
        File.ReadAllText("02.txt").Trim();

// """
// Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
// Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
// Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
// Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
// Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
// """;

}
