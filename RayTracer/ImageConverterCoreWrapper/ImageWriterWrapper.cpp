#include "ImageWriterWrapper.h"

namespace ImageConverter
{
    ImageWriterWrapper::ImageWriterWrapper()
        : ManagedObject(IC::ImageWriter::GetInstance()) {}

    ImageWriterWrapper::ImageWriterWrapper(String^ container_path)
        : ManagedObject(IC::ImageWriter::GetInstance(string_to_char_array(container_path))) {}

    void ImageWriterWrapper::Write(MaterialWrapper^ mat, String^ file_path, String^ extension)
    {
        auto char_path = string_to_char_array(file_path);
        auto char_ext = string_to_char_array(extension);
        m_Instance->WriteMat(mat->GetInstance(), char_path, char_ext);
    }

    void ImageWriterWrapper::Write(array<uint8_t>^ data, int width, int height, String^ file_path, String^ file_ext)
    {
        pin_ptr<uint8_t> pinnedArray = &data[0];

        auto char_path = string_to_char_array(file_path);
        auto char_ext = string_to_char_array(file_ext);
        bool result = m_Instance->WriteData(pinnedArray, width, height, false, char_path, char_ext);
    }

}