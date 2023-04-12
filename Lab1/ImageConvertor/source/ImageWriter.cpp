#include <string>
#include <iostream>
#include <filesystem>
#include <algorithm>
#include "ImageWriter.hpp"

#if _WIN32
    #include <windows.h>
    #define LIB_EXT ".dll"
#elif __APPLE__
    #include <dlfcn.h>
    #define LIB_EXT ".dylib"
#endif

#define LIB_TYPE "Writer"
#define LIB_EXT_DELIM '.'

namespace IC
{

ImageWriter *ImageWriter::instance = nullptr;

ImageWriter *ImageWriter::GetInstance()
{
    if (!instance)
        instance = new ImageWriter();
    
    return instance;
}

ImageWriter::ImageWriter()
{
    for (const auto & entry : std::filesystem::directory_iterator("../../../../ImageConvertor/build/"))
    {
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

typedef bool (*write_func) (Material *, const char *);
bool ImageWriter::Write(Material * mat, const char * file_path, const char * extension)
{
    std::cout << "Start writing "<< std::endl;
    std::filesystem::path path(file_path);
    path.replace_extension(extension);

    std::cout << lib_ext_map[extension].string() << std::endl;

#ifdef _WIN32
    HMODULE lib_handler = LoadLibrary(lib_ext_map[extension].string().c_str());  
#elif __APPLE__
    void * lib_handler = dlopen(lib_ext_map[extension].c_str(), RTLD_LAZY);
#endif

    if (lib_handler == NULL)
    {
        perror("dlopen");
        std::cout << extension << " writer plugin are not installed" << std::endl;
        return false;
    }

#ifdef _WIN32
    write_func write_func_ptr = reinterpret_cast<write_func>(GetProcAddress(lib_handler, "Write"));  
#elif __APPLE__
    write_func write_func_ptr = reinterpret_cast<write_func>(dlsym(lib_handler, "Write"));
#endif

    if (!write_func_ptr)
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
        
        return false;
    }

    std::cout << "before call func" << std::endl;
    bool is_saved = write_func_ptr(mat, path.string().c_str());

#ifdef _WIN32
    FreeLibrary(lib_handler);  
#elif __APPLE__
    dlclose(lib_handler);
#endif
    
    return is_saved;
}

}