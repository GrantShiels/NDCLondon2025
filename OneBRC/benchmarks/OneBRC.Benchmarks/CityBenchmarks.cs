using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace OneBRC.Benchmarks;

[MemoryDiagnoser]
public class CityBenchmarks
{
    private static readonly ReadOnlyMemory<byte> ThousandText = GetThousandTxt();

    [Benchmark(Baseline = true)]
    public int CountAsStrings()
    {
        var dict = new Dictionary<string, int>();

        var span = ThousandText.Span;
        foreach (var range in span.Split((byte)'\n'))
        {
            var line = span[range];
            var semicolon = line.IndexOf((byte)';');
            var utf8City = line.Slice(0, semicolon);
            var city = Encoding.UTF8.GetString(utf8City);
            dict[city] = dict.TryGetValue(city, out var count) ? count + 1 : 1;
        }

        return dict.Count;
    }

    private static byte[] GetThousandTxt()
    {
        using var s = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("OneBRC.Benchmarks.thousand.txt")!;
        var bytes = new byte[s.Length];
        s.ReadExactly(bytes);
        return bytes;
    }

    public readonly struct CityKey(ulong simple)
    {
        private ulong _simple;
    }
    
    
    public static CityKey Create(ReadOnlySpan<byte> span)
    {
        if (span.Length == 8)
        {
            return new CityKey(MemoryMarshal.Read<ulong>(span));
        }
        
        if (span.Length < 8)
        {
            Span<byte> buffer = stackalloc byte[8];
            span.CopyTo(buffer);
            return new CityKey(MemoryMarshal.Read<ulong>(buffer));
        }

        {
            Span<byte> buffer = stackalloc byte[64];
            span.CopyTo(buffer);
            
        }
    }
}