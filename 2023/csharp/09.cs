namespace r3im.aof.d09;

public static class Task {

    public static string run(string p, string f) {
        var input = File.ReadAllLines(f);
        return p switch {
            "1" => part1(input),
            "2" => part2(input),
            _ => "",
        };
    }

    static int[] Diff(this int[] line) {
        var diff = new int[line.Length - 1]; 
        for (int i=0; i<line.Length-1; i++) {
            diff[i] = line[i+1] - line[i];
        }
        return diff;
    }

    static int RecDiff(int[] line) {
        if (line.All(x => x == 0)) {
            return 0;
        }
        return line.Last() + RecDiff(line.Diff()); 
    }

    static int RecDiffFirst(int[] line) {
        if (line.All(x => x == 0)) {
            return 0;
        }
        return line.First() - RecDiffFirst(line.Diff());
    }

    public static string part1(string[] input) {
        var output = input
            .Select(x => x.Split(' ')
                .Select(x => int.Parse(x))
                .ToArray()
            )
            .Select(x => RecDiff(x))
            .Sum()
            .ToString()
            ;
        return output;
    }

    public static string part2(string[] input) {
        var output = input
            .Select(x => x.Split(' ')
                .Select(x => int.Parse(x))
                .ToArray()
            )
            .Select(x => RecDiffFirst(x))
            .Sum()
            .ToString()
            ;
        return output;
    }
}
