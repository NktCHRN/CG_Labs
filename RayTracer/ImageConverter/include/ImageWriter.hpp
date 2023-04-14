#pragma once

#include <filesystem>
#include <map>
#include "IC.hpp"
#include "LibHandler.hpp"

namespace fs = std::filesystem;

namespace IC
{

class Material;

class IMG_API ImageWriter : public LibHandler
{
public:
    static ImageWriter * GetInstance();
    static ImageWriter * GetInstance(const char * container_path);

    bool Write(Material * mat, const char * file_path, const char * extension);

private:
    static ImageWriter* instance;
    
    ImageWriter(const char * container_path);
};

}