#pragma once

#include <map>
#include <filesystem>

#if _WIN32
    #define WIN32_LEAN_AND_MEAN
    #include <windows.h>
    typedef HMODULE LIB_HANDLER;
    #define LIB_EXT ".dll"
#elif __APPLE__
    #include <dlfcn.h>
    typedef void * LIB_HANDLER;
    #define LIB_EXT ".dylib"
#endif

namespace fs = std::filesystem;

namespace IC
{

class LibHandler
{
public:
    virtual ~LibHandler() {}
protected:
    LibHandler(const char * container_path, const char * lib_type, const char * lib_delimeter);

    std::map<fs::path, fs::path> lib_ext_map;

    const char * lib_type;
    const char * lib_delimeter;

    LIB_HANDLER GetLib(fs::path);
    void FreeLib(LIB_HANDLER);

    template<typename T> T GetFunc(LIB_HANDLER lib_handler, const char * name)
    {
    #ifdef _WIN32
        return reinterpret_cast<T>(GetProcAddress(lib_handler, name));
    #elif __APPLE__
        return reinterpret_cast<T>(dlsym(lib_handler, name));
    #endif
    }
};

}