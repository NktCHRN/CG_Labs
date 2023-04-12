using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ImageConverter
{
    public class ImageWriter : LibraryLoader
    {
        private static readonly Lazy<ImageWriter> _instance = new Lazy<ImageWriter>(() => new ImageWriter());
        public static ImageWriter Instance => _instance.Value;

        public override string LibraryType => "Writer";

        private ImageWriter()
        {
            ValidateExtensionLibraryDictionary();
        }
    }
}
