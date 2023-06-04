using System.Diagnostics.CodeAnalysis;

namespace ImageConverter.Core;
public sealed class ConversionResult
{
    public FileInfo? Result { get; private init; }
    public string? Error { get; private init; }

    [MemberNotNullWhen(true, nameof(Result))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccessful { get; private init; }

    internal static ConversionResult FromResult(FileInfo result) => new() { Result = result, IsSuccessful = true };
    internal static ConversionResult FromError(string error) => new() { Error = error, IsSuccessful = false };
}
