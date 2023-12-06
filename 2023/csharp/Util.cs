using System.Text.Json;

namespace r3im;

public static class String {
    public static string JoinToString(this IEnumerable<string> arr, char c) 
        => string.Join(c, arr);
    public static string JoinToString(this IEnumerable<string> arr, string s) 
        => string.Join(s, arr);
    public static T LogJson<T>(this T x, string before = "", string after = "") {
        Console.WriteLine(before + JsonSerializer.Serialize(x) + after);
        return x;
    }
    public static T Log<T>(this T x, string before = "", string after = "") {
        Console.WriteLine(before + x + after);
        return x;
    }
}
