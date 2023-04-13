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

    Material * ImageReader::ReadPath(const char * file_path);
    Material * ImageReader::ReadData(uint8_t * data, int width, int height, const char * extension);

private:
    static ImageReader * instance;

    ImageReader();
};

}