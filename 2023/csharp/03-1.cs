using System.Text.RegularExpressions;

namespace r3im.aof.d03;

public static class Task1 {

    public static string run() {
        Regex digits = new Regex("\\d+");
        var lines = input.Split('\n');
        var w = lines[0].Length;
        var h = lines.Length;
        var output = lines
            .Select((it, y) => digits.Matches(it).Select(x => new Item(x.Value,x.Index,y)))
            .SelectMany(x => x)
            .Where(x => x.adjecentSymbol(lines))
            .Select(x => int.Parse(x.value))
            .Sum()
            .ToString()
            ;
            
        return output;
    }

    record Item(string value, int x, int y);

    static bool adjecentSymbol(this Item it, string[] input) {

        int x_max = it.x + it.value.Length + 1;
        int X = input[0].Length;
        int Y = input.Length;
        for (int x = it.x-1; x<x_max; x++) {
            for (int y = it.y-1; y<=it.y+1; y++) {

                // only check valid boarder elements
                if(x < 0 || y < 0 || x >= X || y >= Y) {
                    continue;
                } else if(y == it.y && x >= it.x && x < x_max-1) {
                    continue;
                } 

                if(input[y][x] != '.') {
                    return true;
                }
            }
        }
        return false;
    }

    static string input = 
        File.ReadAllText("03.txt").Trim();

// """
// 467..114..
// ...*......
// ..35..633.
// ......#...
// 617*......
// .....+.58.
// ..592.....
// ......755.
// ...$.*....
// .664.598..
// """;

}
