using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ImageConverter
{
    public interface ILibraryLoader
    {
        string LibraryExtension { get; }
        string LibraryType { get; }

        void ValidateExtensionLibraryDictionary();
    }

    abstract public class LibraryLoader : ILibraryLoader
    {
        public virtual string LibraryExtension => ".dll";

        public virtual string LibraryType => "Image";

        protected Dictionary<string, string> NameToLibrary = new Dictionary<string, string>();

        public void ValidateExtensionLibraryDictionary()
        {
            string solutionFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\");
            string libsFolderPath = Path.GetFullPath(Path.Combine(solutionFolderPath, "ImageConvertor\\build\\"));

            foreach (string filePath in Directory.GetFiles(libsFolderPath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string extension = fileInfo.Extension;

                if (extension == LibraryExtension && filePath.Contains(LibraryType))
                {
                    int startIndex = filePath.IndexOf('.');
                    int endIndex = filePath.IndexOf('.', startIndex + 1);

                    if (startIndex != -1 && endIndex != -1)
                    {
                        string libName = filePath.Substring(startIndex, endIndex - startIndex);
                        NameToLibrary.Add(libName.ToLower(), filePath);
                        Console.WriteLine(libName);
                        Console.WriteLine(filePath);
                    }
                }
            }
        }
    }

    static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
    }

}
