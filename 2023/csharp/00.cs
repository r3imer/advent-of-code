namespace r3im.aof.d00;

public static class Task {

    public static string run(string p, string f) {
        var input = File.ReadAllLines(f);
        return p switch {
            "1" => part1(input),
            "2" => part2(input),
            _ => "",
        };
    }

    public static string part1(string[] lines) {
        var output = lines
            .JoinToString('\n')
            ;
        return output;
    }

    public static string part2(string[] lines) {
        var output = lines
            .JoinToString('\n')
            ;
        return output;
    }
}
