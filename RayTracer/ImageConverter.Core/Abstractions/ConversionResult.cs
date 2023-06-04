namespace ImageConverter.Core.Abstractions;
public sealed class ConversionResult
{
    public Stream? Result { get; private init; }
    public string? Error { get; private init; }

    internal static ConversionResult FromResult(Stream result) => new() { Result = result };
    internal static ConversionResult FromError(string error) => new() { Error = error };
}
