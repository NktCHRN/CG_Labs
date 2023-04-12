#pragma once
#include "Material.hpp"

class Material_PPM : public IC::Material
{
public:
    Material_PPM(const char* file_path);
    Material_PPM(Pixel** pixels, int width, int height);
    Material_PPM(uint8_t* data, int width, int height);
    
    void Export(const char *file_path) override;
    void ExportSample(const char *file_path) override;
private:
    int rgbMax;
};
