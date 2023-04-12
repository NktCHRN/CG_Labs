#pragma once

#include "MaterialWrapper.h"
#include "ImageReader.hpp"

namespace CLI
{
    public ref class ImageReaderWrapper : public ManagedObject<IC::ImageReader>
    {
    public:
        ImageReaderWrapper();

        MaterialWrapper^ Read(String^ file_path);
    };
}

