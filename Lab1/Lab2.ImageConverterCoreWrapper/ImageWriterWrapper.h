#pragma once

#include "MaterialWrapper.h"
#include "ImageWriter.hpp"

namespace CLI
{
    public ref class ImageWriterWrapper : public ManagedObject<IC::ImageWriter>
    {
    public:
        ImageWriterWrapper();

        void Write(MaterialWrapper^ mat, String^ file_path);
    };
}
