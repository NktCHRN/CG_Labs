#pragma once

#include <filesystem>
#include <map>
#include "IC.hpp"
#include "LibHandler.hpp"

namespace fs = std::filesystem;

namespace IC
{

class Material;

class IMG_API ImageReader : public LibHandler
{
public:
    static ImageReader * GetInstance();
    static ImageReader * GetInstance(const char * container_path);

    Material * ReadPath(const char * file_path);
    Material * ReadData(uint8_t * data, int width, int height, const char * extension);

private:
    static ImageReader * instance;

    ImageReader(const char * container_path);
};

}