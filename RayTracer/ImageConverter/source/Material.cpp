#include <iostream>
#include <fstream>
#include "Material.hpp"

namespace IC
{

Material::Material()
{
    this->pixels = nullptr;
    this->size = Vector2i();
}

Material::Material(Pixel** pixels, int width, int height)
{
    this->size = Vector2i(width, height);

    this->pixels = new Pixel*[this->size.y];
    for (size_t y = 0; y < this->size.y; y++)
        this->pixels[y] = new Pixel[this->size.x];

    for (size_t y = 0; y < this->size.y; y++)
        for (size_t x = 0; x < this->size.x; x++)
        {
            this->pixels[y][x].r = pixels[y][x].r;
            this->pixels[y][x].g = pixels[y][x].g;
            this->pixels[y][x].b = pixels[y][x].b;
        }
}

void Material::Export(const char* file_path) {}
void Material::ExportSample(const char* file_path) {}

Material::~Material()
{
    if (!this->pixels)
        return;
    
    for (size_t y = 0; y < this->size.y; y++)
        if (this->pixels[y])
            delete[] this->pixels[y];
    
    delete[] pixels;
}

}