namespace r3im.aof.d00;

public static class Task1 {

    public static string run() {
        var lines = input.Split('\n');
        var output = lines
            .Select(x => x)
            .JoinToString('\n')
            ;
            
        return output;
    }

    static string input = 
        "";
        // File.ReadAllText("00.txt").Trim();

// """
// """;

}
