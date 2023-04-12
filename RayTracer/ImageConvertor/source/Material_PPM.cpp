#include <fstream>
#include <iostream>
#include "Material_PPM.hpp"

extern "C"
{
    IMG_API IC::Material* Read(const char * file_path)
    {
        std::cout << "Start Read PPM" << std::endl;
        IC::Material * mat = nullptr;
        try {
            mat = new Material_PPM(file_path);
        } catch (const std::exception& e) {
            std::cout << e.what() << std::endl;
        }
        return mat;
    }
}

extern "C"
{
    IMG_API bool Write(IC::Material * mat, const char * file_path)
    {
        std::cout << "Start Writing PPM" << std::endl;
        Material_PPM new_mat = Material_PPM(mat->GetPixels(), mat->GetSize().x, mat->GetSize().y);
        new_mat.Export(file_path);
        return true;
    }
}

//In PPM when reading and writing we reverse colors (RGB -> BGR & BGR -> RGB)

Material_PPM::Material_PPM(const char* file_path)
    : Material()
{
    std::ifstream file(file_path);
    
    std::string fileType;
    file >> fileType;
    
    file >> this->size.x;
    file >> this->size.y;
    
    file >> this->rgbMax;
    
    this->pixels = new Pixel*[this->size.y];
    for (size_t i = 0; i < this->size.y; i++)
        this->pixels[i] = new Pixel[this->size.x];
    
    int r, g, b;
    for (size_t y = 0; y < this->size.y; y++)
    {
        for (size_t x = 0; x < this->size.x; x++)
        {
            file >> r;
            file >> g;
            file >> b;
            
            this->pixels[this->size.y - y - 1][x].b = r;
            this->pixels[this->size.y - y - 1][x].g = g;
            this->pixels[this->size.y - y - 1][x].r = b;
        }
    }
}

Material_PPM::Material_PPM(uint8_t* data, int width, int height)
{
    this->size.x = width;
    this->size.y = height;

    this->rgbMax = 255;

    this->pixels = new Pixel*[this->size.y];
    for (size_t i = 0; i < this->size.y; i++)
        this->pixels[i] = new Pixel[this->size.x];

    for (size_t y = 0; y < this->size.y; y++)
    {
        for (size_t x = 0; x < this->size.x; x++)
        {
            size_t index = (x + y) * 3;
            this->pixels[x][y].b = data[index];
            this->pixels[x][y].g = data[index + 1];
            this->pixels[x][y].r = data[index + 2];
        }
    }
}

Material_PPM::Material_PPM(Pixel** pixels, int width, int height)
    : Material(pixels, width, height)
{
    this->rgbMax = 255;
}

void Material_PPM::Export(const char *file_path)
{
    std::ofstream file(file_path);
    
    if (file.is_open())
    {
        file << "P3" << std::endl;
        file << this->size.x << " " << this->size.y << std::endl;
        file << this->rgbMax << std::endl;
        
        for (size_t y = 0; y < this->size.y; y++)
        {
            size_t yf = this->size.y - y - 1;
            for (size_t x = 0; x < this->size.x; x++)
            {
                file << (int)this->pixels[yf][x].b << " ";
                file << (int)this->pixels[yf][x].g << " ";
                file << (int)this->pixels[yf][x].r << " ";
            }
            file << std::endl;
        }
    }
    
    file.close();
}

void Material_PPM::ExportSample(const char* file_path)
{
    srand(time(0));

    this->size = Vector_i2(256);

    for (size_t y = 0; y < size.y; y++)
        for (size_t x = 0; x < size.x; x++)
        {
            this->pixels[y][x].r = rand() % 255;
            this->pixels[y][x].g = rand() % 255;
            this->pixels[y][x].b = rand() % 255;
        }

    this->Export(file_path);
}
