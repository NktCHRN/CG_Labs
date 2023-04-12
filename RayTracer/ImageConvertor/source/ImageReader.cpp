#include <string>
#include <iostream>
#include <algorithm>
#include "ImageReader.hpp"
#include "Material.hpp"

#if _WIN32
    #include <windows.h>
    #define LIB_EXT ".dll"
#elif __APPLE__
    #include <dlfcn.h>
    #define LIB_EXT ".dylib"
#endif

#define LIB_TYPE "Reader"
#define LIB_EXT_DELIM '.'

namespace IC
{

ImageReader *ImageReader::instance = nullptr;

ImageReader *ImageReader::GetInstance()
{
    if (!instance)
        instance = new ImageReader();
    
    return instance;
}

ImageReader::ImageReader()
{
    std::cout << "IMG READER" << std::endl;
    for (const auto & entry : std::filesystem::directory_iterator("../../../../ImageConvertor/build/"))
    {
        std::cout << entry.path() << std::endl;
        auto ext = entry.path().extension();
        auto file_name_str = entry.path().filename().string();
        if (ext == LIB_EXT && file_name_str.find(LIB_TYPE) != std::string::npos)
        {
            size_t startPos = file_name_str.find(LIB_EXT_DELIM);
            size_t endPos = file_name_str.find(LIB_EXT_DELIM, startPos + 1);

            if (startPos != std::string::npos && endPos != std::string::npos)
            {
                std::string sub_str = file_name_str.substr(startPos, endPos - startPos);
                std::transform(sub_str.begin(), sub_str.end(), sub_str.begin(), [](unsigned char c) { return std::tolower(c); });
                lib_ext_map.emplace(sub_str, entry.path());
                std::cout << entry.path() << std::endl;
            }
        }
    }
}

typedef Material* (*read_func) (const char *);
Material * ImageReader::Read(const char *file_path)
{
    std::filesystem::path path(file_path);
    auto extension = path.extension();

    Material * mat = nullptr;

    std::cout << lib_ext_map[extension].c_str() << std::endl;

#ifdef _WIN32
    HMODULE lib_handler = LoadLibrary(lib_ext_map[extension].string().c_str());  
#elif __APPLE__
    void * lib_handler = dlopen(lib_ext_map[extension].c_str(), RTLD_LAZY);
#endif

    if (lib_handler == NULL)
    {
        perror("dlopen");
        std::cout << extension << " reader plugin are not installed" << std::endl;
        return mat;
    }

#ifdef _WIN32
    read_func read_func_ptr = reinterpret_cast<read_func>(GetProcAddress(lib_handler, "Read"));  
#elif __APPLE__
    read_func read_func_ptr = reinterpret_cast<read_func>(dlsym(lib_handler, "Read"));
#endif

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
        dlclose(lib_handler);
    #endif

        return mat;
    }

    mat = read_func_ptr(file_path);

#ifdef _WIN32
    FreeLibrary(lib_handler);  
#elif __APPLE__
    dlclose(lib_handler);
#endif

    return mat;
}

}