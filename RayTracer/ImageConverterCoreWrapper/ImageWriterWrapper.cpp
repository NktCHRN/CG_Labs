#include "ImageWriterWrapper.h"

namespace CLI
{
    ImageWriterWrapper::ImageWriterWrapper() : ManagedObject(IC::ImageWriter::GetInstance()) {}

    void ImageWriterWrapper::Write(MaterialWrapper^ mat, String^ file_path)
    {
        auto c_str = string_to_char_array(file_path);
        m_Instance->Write(mat->GetInstance(), c_str, ".bmp");
    }

}