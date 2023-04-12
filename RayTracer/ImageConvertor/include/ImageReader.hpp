#pragma once

#include <filesystem>
#include <map>
#include "IC.hpp"

namespace fs = std::filesystem;

namespace IC
{

class Material;

class IMG_API ImageReader
{
public:
    static ImageReader * GetInstance();

    Material * Read(const char * path);

private:
    static ImageReader * instance;

    std::map<fs::path, fs::path> lib_ext_map;

    ImageReader();
};

}