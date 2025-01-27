using System.Text;
using BenchmarkDotNet.Attributes;

namespace OneBRC.Benchmarks;

[MemoryDiagnoser]
public class MultiFloatParsingBenchmarks
{
    private const byte Semicolon = (byte)';';
    private const byte Newline = (byte)'\n';
    
    private static readonly string Floats = Enumerable.Range(0, 1000)
        .Select(_ => Random.Shared.NextSingle().ToString("0.000"))
        .Aggregate((a, b) => a + "\n" + b);

    private static readonly ReadOnlyMemory<byte> Utf8Floats = Encoding.UTF8.GetBytes(Floats);

}