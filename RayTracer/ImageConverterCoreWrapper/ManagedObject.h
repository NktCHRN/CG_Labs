#pragma once

using namespace System;
using namespace System::Runtime::InteropServices;

namespace ImageConverter
{

    static const char* string_to_char_array(String^ string)
    {
        IntPtr ptrToNativeString = Marshal::StringToHGlobalAnsi(string);
        return static_cast<char*>(ptrToNativeString.ToPointer());
    }

    template<class T>
    public ref class ManagedObject
    {
    protected:
        T* m_Instance;
    public:
        ManagedObject(T* instance)
            : m_Instance(instance)
        {
        }
        virtual ~ManagedObject()
        {
            if (m_Instance != nullptr)
            {
                delete m_Instance;
            }
        }
        !ManagedObject()
        {
            if (m_Instance != nullptr)
            {
                delete m_Instance;
            }
        }
        T* GetInstance()
        {
            return m_Instance;
        }
    };
}