#include <string>
#include <iostream>
#include <algorithm>
#include "ImageReader.hpp"
#include "Material.hpp"

namespace IC
{

ImageReader *ImageReader::instance = nullptr;

ImageReader *ImageReader::GetInstance()
{
    if (!instance)
        instance = new ImageReader();
    
    return instance;
}

ImageReader::ImageReader() : LibHandler(".dll", "Reader", ".") {}

typedef Material* (*read_file_func) (const char *);
typedef Material* (*read_data_func) (uint8_t * data, int width, int height);

Material * ImageReader::ReadPath(const char *file_path)
{
    Material * mat = nullptr;

    fs::path path(file_path);
    
    LIB_HANDLER lib_handler = GetLib(path.extension());
    read_file_func read_func_ptr = GetFunc<read_file_func>(lib_handler, "ReadPath");

    if (!read_func_ptr) {
    #ifdef _WIN32
        DWORD err = GetLastError();
        LPSTR errMsg;
        FormatMessageA(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
                    nullptr, err, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPSTR)&errMsg, 0, nullptr);
        std::cout << errMsg << std::endl;
        LocalFree(errMsg);
    #elif __APPLE__
        std::cerr << "Failed to get symbol: " << dlerror() << std::endl;
    #endif

        return mat;
    }

    mat = read_func_ptr(file_path);

    FreeLib(lib_handler);

    return mat;
}

Material * ImageReader::ReadData(uint8_t * data, int width, int height, const char * extension)
{
    Material * mat = nullptr;

    LIB_HANDLER lib_handler = GetLib(std::filesystem::path(extension));
    read_data_func read_func_ptr = GetFunc<read_data_func>(lib_handler, "ReadData");

    if (!read_func_ptr) {
    #ifdef _WIN32
        DWORD err = GetLastError();
        LPSTR errMsg;
        FormatMessageA(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
                    nullptr, err, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPSTR)&errMsg, 0, nullptr);
        std::cout << errMsg << std::endl;
        LocalFree(errMsg);
    #elif __APPLE__
        std::cerr << "Failed to get symbol: " << dlerror() << std::endl;
    #endif
    }
    else
    {
        mat = read_func_ptr(data, width, height);
    }

    FreeLib(lib_handler);

    return mat;
}

}