#pragma once

#include "MaterialWrapper.h"
#include "ImageReader.hpp"

namespace ImageConverter
{
    public ref class ImageReaderWrapper : public ManagedObject<IC::ImageReader>
    {
    public:
        ImageReaderWrapper();
        ImageReaderWrapper(String^ container_path);

        MaterialWrapper^ Read(String^ file_path);
        MaterialWrapper^ Read(array<uint8_t>^ data, int width, int height, String^ extension);
    };
}