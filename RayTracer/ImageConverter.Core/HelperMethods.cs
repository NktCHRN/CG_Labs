namespace ImageConverter.Core;
public static class HelperMethods
{
    public static string FormatFileName(string fileName) => fileName.Trim();

    public static string FormatFileExtension(string fileExtension)
    {
        fileExtension = fileExtension.Trim();

        if (fileExtension.StartsWith('.'))
        {
            fileExtension = fileExtension[1..];
        }

        return fileExtension.ToLower();
    }

    public static string ChangeFileExtension(string oldFileName, string oldExtension, string newExtension)
    {
        if (oldFileName.EndsWith(oldExtension, StringComparison.OrdinalIgnoreCase))
        {
            oldFileName = oldFileName.Remove(oldFileName.LastIndexOf(oldExtension, StringComparison.OrdinalIgnoreCase));
        }

        return $"{oldFileName}.{newExtension}";
    }
}
