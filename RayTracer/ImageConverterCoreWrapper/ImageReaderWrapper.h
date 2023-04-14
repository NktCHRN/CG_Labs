#pragma once

#include "MaterialWrapper.h"
#include "ImageReader.hpp"

namespace ImageConverter
{
    public ref class ImageReaderWrapper : public ManagedObject<IC::ImageReader>
    {
    public:
        ImageReaderWrapper();
        ImageReaderWrapper(String^ containerPath);

        MaterialWrapper^ Read(String^ file_path);
        MaterialWrapper^ ReadData(array<uint8_t>^ data, int width, int height);
    };
}