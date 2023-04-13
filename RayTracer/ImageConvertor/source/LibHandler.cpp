#include <iostream>
#include <algorithm>
#include "LibHandler.hpp"

IC::LibHandler::LibHandler(const char * lib_ext, const char * lib_type, const char * lib_delimeter)
    : lib_ext(lib_ext), lib_type(lib_type), lib_delimeter(lib_delimeter)
{
    for (const auto & entry : fs::directory_iterator("../../../../ImageConvertor/build/"))
    {
        auto ext = entry.path().extension();
        auto file_name_str = entry.path().filename().string();
        if (ext == lib_ext && file_name_str.find(lib_type) != std::string::npos)
        {
            size_t startPos = file_name_str.find(lib_delimeter);
            size_t endPos = file_name_str.find(lib_delimeter, startPos + 1);
    
            if (startPos != std::string::npos && endPos != std::string::npos)
            {
                std::string sub_str = file_name_str.substr(startPos, endPos - startPos);
                std::cout << sub_str << std::endl;
                std::transform(sub_str.begin(), sub_str.end(), sub_str.begin(), [](unsigned char c) { return std::tolower(c); });
                lib_ext_map.emplace(sub_str, entry.path());
                std::cout << entry.path() << std::endl;
            }
        }
    }
}

LIB_HANDLER IC::LibHandler::GetLib(fs::path extension)
{
    std::cout << lib_ext_map[extension].c_str() << std::endl;

#ifdef _WIN32
    LIB_HANDLER lib_handler = LoadLibrary(lib_ext_map[extension].string().c_str());  
#elif __APPLE__
    LIB_HANDLER lib_handler = dlopen(lib_ext_map[extension].c_str(), RTLD_LAZY);
#endif

    if (lib_handler == NULL)
    {
        perror("dlopen");
        std::cout << extension << " reader plugin are not installed" << std::endl;
    }

    return lib_handler;
}

void IC::LibHandler::FreeLib(LIB_HANDLER lib_handler)
{
#ifdef _WIN32
    FreeLibrary(lib_handler);  
#elif __APPLE__
        dlclose(lib_handler);
#endif
}
