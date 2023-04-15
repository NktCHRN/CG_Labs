#pragma once

#include "MaterialWrapper.h"
#include "ImageWriter.hpp"

namespace ImageConverter
{
    public ref class ImageWriterWrapper : public ManagedObject<IC::ImageWriter>
    {
    public:
        ImageWriterWrapper();
        ImageWriterWrapper(String^ container_path);

        void Write(MaterialWrapper^ mat, String^ file_path, String^ extension);
        void Write(array<uint8_t>^ data, int width, int height, String^ file_path, String^ file_ext);
    };
}
