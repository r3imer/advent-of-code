using System.Text.Json;
using System.Text.Json.Serialization;

namespace r3im;

public static class String {
    static readonly JsonSerializerOptions s_opts = new() {
        IncludeFields = true,  
    };
    static String() {
        s_opts.Converters.Add(new JsonStringEnumConverter());
    }
    public static string JoinToString(this IEnumerable<string> arr, char c) 
        => string.Join(c, arr);
    public static string JoinToString(this IEnumerable<string> arr, string s) 
        => string.Join(s, arr);
    public static T LogJson<T>(this T x, string before = "", string after = "") {
        Console.WriteLine(before + JsonSerializer.Serialize(x, s_opts) + after);
        return x;
    }
    public static T Log<T>(this T x, string before = "", string after = "") {
        Console.WriteLine(before + x + after);
        return x;
    }
    public static U To<T,U>(this T x, Func<T,U> fn) => fn(x);
}
