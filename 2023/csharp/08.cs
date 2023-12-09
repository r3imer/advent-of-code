using static r3im.aof.d08.Task;

namespace r3im.aof.d08;

using Network = System.Collections.Generic.Dictionary<string, Node>;

public static class Task {

    public static string run(string p, string f) {
        var input = File.ReadAllLines(f);
        return p switch {
            "1" => part1(input),
            "2" => part2(input),
            _ => "",
        };
    }

    public record Node(string Left, string Right);

    static string? GetNode(this Node x, char c) {
        return c switch {
            'L' => x.Left,
            'R' => x.Right,
            _ => null,
        };
    }

    static Network Parse(this string[] lines) {
        return lines
            .Select(x => x.Split([' ', '(', ')', ',']))
            // .LogJson()
            .ToDictionary(x => x[0], x => new Node(x[3],x[5]))
            ;
    }

    static bool Finish(this string nodes) {
        return nodes.Last() == 'Z';
    }

    static int CountSteps(string start, char[] cmds, Network network) {
        string search = start;
        int count = 0;
        int c = -1;
        int length = cmds.Length;
        while (!search.Finish()) {
            c = (c >= length - 1) ? 0 : c + 1;
            count++;
            search = network[search].GetNode(cmds[c])!;
        }

        return count;
    }

    static long gcf(long a, long b) {
        while (b != 0) {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static long lcm(long a, long b) {
        return (a / gcf(a, b)) * b;
    } 


    public static string part1(string[] input) {
        var cmds = input[0].ToCharArray();
        var network = input[2..].Parse();
        string[] start = ["AAA"];

        var count = CountSteps(start[0], cmds, network);

        return count.ToString();
    }


    public static string part2(string[] input) {
        var cmds = input[0].ToCharArray();
        var network = input[2..].Parse();
        var output = network
            .Select(kv => kv.Key)
            .Where(x => x.Last() == 'A')
            .ToArray()
            // .LogJson("start: ")
            .Select(x => CountSteps(x, cmds, network))
            .Select(x => (long)x)
            .ToArray()
            // .LogJson("counts: ")
            .Aggregate((acc,x) => lcm(acc,x)) 
            .ToString()
            ;

        return output;
    }
}
