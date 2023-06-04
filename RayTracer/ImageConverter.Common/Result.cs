using System.Diagnostics.CodeAnalysis;

namespace ImageConverter.Common;
public sealed record Result<TResult, KError>
{
    public TResult? Value { get; private init; }
    public KError? Error { get; private init; }

    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccessful { get; private init; }

    public static Result<TResult, KError> Success(TResult result) => new() { Value = result, IsSuccessful = true };
    public static Result<TResult, KError> Failure(KError error) => new() { Error = error, IsSuccessful = false };
}
