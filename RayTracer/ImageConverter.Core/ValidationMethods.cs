using System.Diagnostics.CodeAnalysis;

namespace ImageConverter.Core;
public static class ValidationMethods
{
    public static void ValidateFileName([NotNull] string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName), "Provided file name was null or whitespace");
        }
    }

    public static void ValidateFileExtension([NotNull] string? fileExtension)
    {
        if (string.IsNullOrWhiteSpace(fileExtension))
        {
            throw new ArgumentNullException(nameof(fileExtension), "Provided file extension was null or whitespace");
        }
        else if (!fileExtension.Any(c => char.IsLetterOrDigit(c)))
        {
            throw new ArgumentException("Provided file extension does not contain any characters or digits");
        }
    }
}
