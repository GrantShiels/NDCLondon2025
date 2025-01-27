using System.Reflection;

namespace OneBRC.Benchmarks;

public class CityBenchmarks
{
    private static readonly ReadOnlyMemory<byte> ThousandTxt = GetThousandTxt();

    public static int Thing()
    {
        return ThousandTxt.Length;
    }

    private static byte[] GetThousandTxt()
    {
        using var s = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("OneBRC.Benchmarks.thousand.txt")!;
        var bytes = new byte[s.Length];
        s.ReadExactly(bytes);
        return bytes;
    }
}