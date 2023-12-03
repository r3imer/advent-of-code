using System.Text.Json;

namespace r3im;

public static class String {
    public static string JoinToString(this IEnumerable<string> arr, char c) 
        => string.Join(c, arr);
    public static string JoinToString(this IEnumerable<string> arr, string s) 
        => string.Join(s, arr);
    public static string Log<T>(this T x) 
        => JsonSerializer.Serialize(x);
}
