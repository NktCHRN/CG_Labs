#include "ImageReaderWrapper.h"

namespace CLI
{
    ImageReaderWrapper::ImageReaderWrapper() : ManagedObject(IC::ImageReader::GetInstance()) {}

    MaterialWrapper^ ImageReaderWrapper::Read(String^ file_path)
    {
        auto c_str = string_to_char_array(file_path);
        auto mat = m_Instance->Read(c_str);
        return gcnew MaterialWrapper(mat);
    }
}