using System.Reflection;
using System.Runtime.InteropServices;

namespace Lab2.ImageConverter
{
    public class ImageReader : LibraryLoader
    {
        private static readonly Lazy<ImageReader> _instance = new Lazy<ImageReader>(() => new ImageReader());
        public static ImageReader Instance => _instance.Value;

        public override string LibraryType => "Reader";

        private ImageReader()
        {
            ValidateExtensionLibraryDictionary();
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate CLI.Material readPtr(char[] file_path);

        public CLI.Material Read(string file_path)
        {
            string name = new FileInfo(file_path).Extension;
            string dllPath = NameToLibrary[name];

            IntPtr handler = NativeMethods.LoadLibrary(dllPath);
            IntPtr pAddressOfFunctionToCall = NativeMethods.GetProcAddress(handler, "Read");

            if (pAddressOfFunctionToCall != IntPtr.Zero)
            {
                readPtr read = (readPtr)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(readPtr));
                return read(file_path.ToCharArray());
            }

            return null;
        } 
    }
}