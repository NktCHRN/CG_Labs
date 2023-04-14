#include "ImageReaderWrapper.h"

namespace ImageConverter
{
    ImageReaderWrapper::ImageReaderWrapper() : ManagedObject(IC::ImageReader::GetInstance()) {}

    MaterialWrapper^ ImageReaderWrapper::Read(String^ file_path)
    {
        auto c_str = string_to_char_array(file_path);
        auto mat = m_Instance->ReadPath(c_str);
        return gcnew MaterialWrapper(mat);
    }

    MaterialWrapper^ ImageReaderWrapper::ReadData(array<uint8_t>^ data, int width, int height)
    {
        pin_ptr<uint8_t> pinnedArray = &data[0];
        
        auto mat = m_Instance->ReadData(pinnedArray, width, height, ".ppm");
        return gcnew MaterialWrapper(mat);
    }
}