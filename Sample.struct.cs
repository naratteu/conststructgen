using System.Runtime.InteropServices;

namespace Main;

partial struct Sample
{
    public byte head;
    public (float, float, float) body;
    public byte tail;
}

static class SampleExts
{
#if !NETSTANDARD2_0
    public static void Deconstruct(this ref readonly Sample sample, out ReadOnlySpan<char> span)
        => span = MemoryMarshal.Cast<Sample, char>(new ReadOnlySpan<Sample>(in sample));
#endif
}