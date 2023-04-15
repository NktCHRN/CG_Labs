#include "ImageReaderWrapper.h"

namespace ImageConverter
{
    ImageReaderWrapper::ImageReaderWrapper()
        : ManagedObject(IC::ImageReader::GetInstance()) {}

    ImageReaderWrapper::ImageReaderWrapper(String^ container_path)
        : ManagedObject(IC::ImageReader::GetInstance(string_to_char_array(container_path))) {}

    MaterialWrapper^ ImageReaderWrapper::Read(String^ file_path)
    {
        auto c_str = string_to_char_array(file_path);
        auto mat = m_Instance->ReadPath(c_str);
        return gcnew MaterialWrapper(mat);
    }

    MaterialWrapper^ ImageReaderWrapper::Read(array<uint8_t>^ data, int width, int height, String^ extension)
    {
        pin_ptr<uint8_t> pinnedArray = &data[0];
        
        auto charExt = string_to_char_array(extension);
        auto mat = m_Instance->ReadData(pinnedArray, width, height, charExt);
        return gcnew MaterialWrapper(mat);
    }
}