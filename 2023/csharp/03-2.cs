using System.Text.RegularExpressions;
using Point = (int x, int y);

namespace r3im.aof.d03;

public static class Task2 {

    public static string run() {
        Regex digits = new Regex("\\d+");
        var lines = input.Split('\n');
        var output = lines
            .Select((it, y) => digits.Matches(it).Select(x => new Item(x.Value,(x.Index,y))))
            .SelectMany(x => x)
            .Select(x => (x.value, point: x.getStar(lines)))
            .Where(x => x.point is not null)
            .GroupBy(x => x.point, x => x.value)
            .Select(x => x.Select(x=>int.Parse(x)).ToArray())
            .Where(x => x.Length == 2)
            .Select(x => x[0] * x[1])
            .Sum()
            .ToString()
            ;
            
        return output;
    }

    record Item(string value, Point p);

    static Point? getStar(this Item it, string[] input) {
        int x_max = it.p.x + it.value.Length + 1;
        int X = input[0].Length;
        int Y = input.Length;
        for (int x = it.p.x-1; x<x_max; x++) {
            for (int y = it.p.y-1; y<=it.p.y+1; y++) {
                if(x < 0 || y < 0 || x >= X || y >= Y) {
                    continue;
                } else if(y == it.p.y && x >= it.p.x && x < x_max-1) {
                    continue;
                } 
                if(input[y][x] == '*') {
                    return (x,y);
                }
            }
        }
        return null;
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
