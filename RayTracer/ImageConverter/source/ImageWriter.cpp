#include <string>
#include <iostream>
#include <filesystem>
#include <algorithm>
#include "ImageWriter.hpp"

namespace IC
{

ImageWriter * ImageWriter::instance = nullptr;

ImageWriter * ImageWriter::GetInstance()
{
    if (!instance)
        instance = new ImageWriter(".");
    
    return instance;
}

ImageWriter * ImageWriter::GetInstance(const char * container_path)
{
    instance = new ImageWriter(container_path);
    
    return instance;
}

ImageWriter::ImageWriter(const char * container_path)
    : LibHandler(container_path, "Writer", ".") {}

typedef bool (*write_mat_func) (Material * mat, const char * file_path);
typedef bool (*write_data_func) (uint8_t* data, int width, int height, bool has_alpha, const char * file_path);

bool ImageWriter::WriteMat(Material * mat, const char * file_path, const char * extension)
{
    std::cout << "Start writing "<< std::endl;
    std::filesystem::path path(file_path);
    path.replace_extension(extension);

    std::cout << lib_ext_map[extension].string() << std::endl;

    LIB_HANDLER lib_handler = GetLib(path.extension());
    write_mat_func write_func = GetFunc<write_mat_func>(lib_handler, "WriteMat");

    bool is_saved = false;

    if (!write_func)
    {
    #ifdef _WIN32
        std::cout << "ERROR" << std::endl;
        DWORD err = GetLastError();
        LPSTR errMsg;
        FormatMessageA(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
                    nullptr, err, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPSTR)&errMsg, 0, nullptr);
        std::cout << errMsg << std::endl;
        LocalFree(errMsg);
    #elif __APPLE__
        std::cerr << "Failed to get symbol: " << dlerror() << std::endl;
        dlclose(lib_handler);
    #endif
    }
    else
    {
        is_saved = write_func(mat, path.string().c_str());
    }

    FreeLib(lib_handler);
    
    return is_saved;
}

bool ImageWriter::WriteData(uint8_t* data, int width, int height, bool has_alpha, const char * file_path, const char * extension)
{
    std::cout << "Start writing "<< std::endl;
    std::filesystem::path path(file_path);
    path.replace_extension(extension);

    std::cout << lib_ext_map[extension].string() << std::endl;

    LIB_HANDLER lib_handler = GetLib(path.extension());
    write_data_func write_func = GetFunc<write_data_func>(lib_handler, "WriteData");

    bool is_saved = false;

    if (!write_func)
    {
    #ifdef _WIN32
        std::cout << "ERROR" << std::endl;
        DWORD err = GetLastError();
        LPSTR errMsg;
        FormatMessageA(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
                    nullptr, err, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPSTR)&errMsg, 0, nullptr);
        std::cout << errMsg << std::endl;
        LocalFree(errMsg);
    #elif __APPLE__
        std::cerr << "Failed to get symbol: " << dlerror() << std::endl;
        dlclose(lib_handler);
    #endif
    }
    else
    {
        is_saved = write_func(data, width, height, has_alpha, path.string().c_str());
    }

    FreeLib(lib_handler);
    
    return is_saved;
}

}