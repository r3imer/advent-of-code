namespace r3im.aof.d07;

public static class Task {

    public static string run(string p, string f) {
        var input = File.ReadAllLines(f);
        return p switch {
            "1" => part1(input),
            "2" => part2(input),
            _ => "",
        };
    }

    record Round(string Deck, int Bid);
    static Round Parse(this string x) {
        return x.Split(' ').To(x => new Round(x[0], int.Parse(x[1])));
    }

    static Hand GetHand(this Round x) {
        var dic = x.Deck
            .GroupBy(x => x)
            // .ToDictionary(x => x.Key, x => x.Count())
            .Select(x => x.Count())
            .OrderDescending()
            .ToArray()
            .LogJson()
            ;

        return dic switch {
            [1,1,1,1,1] => Hand.HighCard,
            [2,1,1,1] => Hand.OnePair,
            [2,2,1] => Hand.TwoPair,
            [3,1,1] => Hand.Three,
            [3,2] => Hand.FullHouse,
            [4,1] => Hand.Four,
            [5] => Hand.Five,
            _ => throw new NotImplementedException(),
        };
    }

    enum Hand {
        HighCard,
        OnePair,
        TwoPair,
        Three,
        FullHouse,
        Four,
        Five,
    }

    public static string part1(string[] input) {
        var output = input
            .Select(x => x.Parse())
            .Select(x => new { round = x, hand = x.GetHand() })
            .LogJson()
            .ToString()
            ;
        return output;
    }

    public static string part2(string[] input) {
        var output = input
            .JoinToString('\n')
            ;
        return output;
    }
}
