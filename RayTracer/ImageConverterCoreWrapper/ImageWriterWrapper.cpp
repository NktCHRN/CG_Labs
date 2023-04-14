#include "ImageWriterWrapper.h"

namespace ImageConverter
{
    ImageWriterWrapper::ImageWriterWrapper()
        : ManagedObject(IC::ImageWriter::GetInstance()) {}

    ImageWriterWrapper::ImageWriterWrapper(String^ containerPath)
        : ManagedObject(IC::ImageWriter::GetInstance(string_to_char_array(containerPath))) {}

    void ImageWriterWrapper::Write(MaterialWrapper^ mat, String^ file_path, String^ extension)
    {
        auto charPath = string_to_char_array(file_path);
        auto charExt = string_to_char_array(extension);
        m_Instance->Write(mat->GetInstance(), charPath, charExt);
    }

}