#pragma once

#include "MaterialWrapper.h"
#include "ImageWriter.hpp"

namespace ImageConverter
{
    public ref class ImageWriterWrapper : public ManagedObject<IC::ImageWriter>
    {
    public:
        ImageWriterWrapper();
        ImageWriterWrapper(String^ containerPath);

        void Write(MaterialWrapper^ mat, String^ file_path, String^ extension);
    };
}
