#pragma once
#include <stdio.h>
#include "IC.hpp"
#include "Vector_i2.hpp"
#include "Pixel.hpp"

namespace IC
{

class IMG_API Material {
public:
    Material();
    Material(Pixel** pixels, int width, int height);
    virtual ~Material();
    
    virtual void Export(const char* file_path);
    virtual void ExportSample(const char* file_path);
    
    Vector_i2& GetSize() { return this->size; }
    Pixel** GetPixels() { return this->pixels; }
    
protected:
    Vector_i2 size;
    Pixel** pixels;
    
};

}