using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using Main;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

[Generator]
public class SampleGen : IIncrementalGenerator
{
    struct U16(UInt16 u) { public override string ToString() => $"\\x{u:X4}"; }
    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        string raw;
        string Summary(Sample expression, [CallerArgumentExpression(nameof(expression))] string summary = "")
        {
            raw = string.Concat([.. MemoryMarshal.Cast<Sample, U16>([expression])]);
            return summary;
        }
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Sample.g.cs", SourceText.From($$"""
        using System;
        namespace Main;

        partial struct Sample
        {
            ///<summary>{{Summary(new Sample { head = 1, body = (0, 0, 0), tail = 2 })}}</summary>
            public const string TYPE1 = "{{raw}}";
            
            ///<summary>{{Summary(new Sample { head = 1, body = (9, 9, 9), tail = 2 })}}</summary>
            public const string TYPE2 = "{{raw}}";
        }
        """, Encoding.UTF8)));
    }
}