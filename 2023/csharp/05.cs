namespace r3im.aof.d05;

public static class Task {

    public static string run(string p, string f) {
        var input = File.ReadAllLines(f);
        return p switch {
            "1" => part1(input),
            "2" => part2(input),
            _ => "",
        };
    }

    record Map(long Dest, long Source, long length);
    record MapV2(long Dest, Range Source) {
        public override string ToString() 
            => $"{Source}->{Dest}";
    }

    static MapV2 To(this Map x) 
        => new MapV2(x.Dest, new Range(x.Source,x.length));

    static Map Parse(this string x) {
        return x.Split(' ')
            .Select(long.Parse)
            .ToArray()
            .To(x => new Map(x[0],x[1],x[2]));
    }

    static long MapNumber(this Map[] maps, long n) {
        var num = n;
        foreach (var m in maps) {
            var max = m.Source + m.length;
            if (m.Source <= n && n < max) {
                var diff = m.Source - m.Dest;
                // return (n - diff);
                num = n - diff;
            }
        }
        return num;
    }

    public static string part1(string[] lines) {
        var seeds = lines[0].Split(' ').Skip(1).Select(long.Parse).ToArray();

        var output = lines
            .Skip(1)
            .Aggregate(new List<List<Map>>(), (acc,line) => {
                if(line == "") {
                    acc.Add([]);
                } else if (line.Contains(':')) {
                    // line.Log();
                } else {
                    acc.Last().Add(line.Parse());
                }
                return acc;
            })
            .Select(x => x.ToArray())
            .Aggregate(seeds, (acc, maps) => acc
                .Select(maps.MapNumber)
                .ToArray()
            )
            .Min()
            .ToString()
            ;

        return output;
    }

    record Range(long Start, long Length) {
        public override string ToString() 
            => $"[{Start},+{Length})";
    };

    static List<Range> MapRange(this MapV2[] maps, Range range) {
        var list = new List<Range>();
        Range[] todo = [range];
        // maps.LogArray("plans = ");
        foreach (var m in maps) {
            // m.Log("plan = ");
            // todo.LogArray("todo -> ");
            var sections = todo
                .Select(x=>x.Split(m.Source))
                .ToArray()
                // .LogArray("section <- ")
                ;
            var mapped = sections
                .Select(x => x.union!)
                .Where(x => x is not null && x.Length > 0)
                .ToArray()
                // .LogArray("union   = ")
                .Select(x => new Range(x.Start - m.Source.Start + m.Dest, x.Length)) 
                .ToArray()
                // .LogArray("mapped <- ")
                ;
            list.AddRange(mapped);
            // list.ToArray().LogArray($"list = ");
            todo = sections
                .Select(x => new Range?[] {x.left,x.right})
                .SelectMany(x => x)
                .Select(x => x!)
                .Where(x => x is not null && x.Length > 0)
                .ToArray()
                // .LogArray("todo <- ")
                ;
            if (todo.Length == 0) {
                break;
            }
        }
        list.AddRange(todo);
        // list.ToArray().LogArray("map = ");
        return list;
    }

    record Length(long x1, long x2);
    record Section(Range? union, Range? left, Range? right) {
        public override string ToString() 
            => $"u: {union}, l: {left}, r: {right}";
    }

    static bool Finished (this Section x) 
        => x.union is not null && x.left is null && x.right is null;
    static Length ToLength(this Range x) => new Length(x.Start,x.Start+x.Length);
    
    static (char,long)[] ToHelp(this Length x, char help) 
        => [(help,x.x1),(help,x.x2)];

    static Section Split(this Range a, Range b) {
        var A = a.ToLength().ToHelp('a');
        var B = b.ToLength().ToHelp('b');
        var list = A.ToList();
        list.AddRange(B);
        return string.Join("", list.OrderBy(x=>x.Item2).Select(x=>x.Item1)) switch {
            "aabb" => new Section(null,a,null),
            "bbaa" => new Section(null,null,a),
            "baab" => new Section(a,null,null),
            "abba" => new Section(
                b,
                new Range(
                    a.Start, 
                    b.Start - a.Start
                ),
                new Range(
                    b.Start + b.Length, 
                    a.Start + a.Length - b.Start - b.Length
                )
            ),
            "baba" => new Section(
                new Range(a.Start, b.Start + b.Length - a.Start),
                null,
                new Range(b.Start + b.Length, a.Start + a.Length - b.Start - b.Length)
            ),
            "abab" => new Section(
                new Range(b.Start, a.Start + a.Length - b.Start),
                new Range(a.Start, b.Start - a.Start),
                null
            ),
            _ => throw new NotImplementedException(),
        };
    }

    public static string part2(string[] lines) {
        var seeds = lines[0]
            .Split(' ')
            .Skip(1)
            .Select(long.Parse)
            .ToArray()
            ;
        var ranges = new List<Range>();

        for (int i=0; i<seeds.Length-1; i+=2) {
            ranges.Add(new Range(seeds[i],seeds[i+1]));
        }

        // ranges.ToArray().LogArray();

        var mapped = lines
            .Skip(1)
            .Aggregate(new List<List<Map>>(), (acc,line) => {
                if(line == "") {
                    acc.Add([]);
                } else if (line.Contains(':')) {
                    // line.Log();
                } else {
                    acc.Last().Add(line.Parse());
                }
                return acc;
            })
            .Select(x => x.Select(x=>x.To()))
            .Select(x => x.ToArray())
            .ToArray()
            .Aggregate(ranges, (acc, maps) => acc
                .ToArray()
                // .LogArray("acc: ")
                .Select(maps.MapRange)
                .SelectMany(x => x)
                .ToList()
            )
            .ToArray()
            .LogArray("result = ")
            .Select(x => x.Start)
            .ToArray()
            .LogArray("start = ")
            // .Where(x => x > 0)
            ;
        // if (mapped.Length == 0) {
        //     return "";
        // }
        return mapped.Min().ToString();
    }
}
