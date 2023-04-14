#include <fstream>
#include <iostream>
#include <sstream>
#include <iomanip>
#include <cstdint>
#include <exception>

#include "Material_BMP.hpp"

extern "C"
{
    IC::Material* ReadPath(const char * file_path)
    {
        std::cout << "Start Read BMP" << std::endl;
        IC::Material * mat = nullptr;
        try {
            mat = new Material_BMP(file_path);
        } catch (const std::exception& e) {
            std::cout << e.what() << std::endl;
        }
        return mat;
    }

    IC::Material* ReadData(uint8_t * data, int width, int height)
    {
        std::cout << "Start Read BMP pixels" << std::endl;
        std::cout << "Width: " << width << " Height: " << height << std::endl;
        IC::Material * mat = nullptr;
        try {
            mat = new Material_BMP(data, width, height);
        } catch (const std::exception& e) {
            std::cout << e.what() << std::endl;
        }
        return mat;
    }
}

extern "C"
{
    bool Write(IC::Material * mat, const char * file_path)
    {
        std::cout << "Start Writing BMP" << std::endl;
        Material_BMP new_mat = Material_BMP(mat->GetPixels(), mat->GetSize().x, mat->GetSize().y);
        new_mat.Export(file_path);
        return true;
    }
}

Material_BMP::Material_BMP(const char* file_path)
    : Material()
{
    std::vector<uint8_t> pixels_data;
    int channels = 0;

    std::ifstream file(file_path, std::ios::binary);
    {
        file.read(reinterpret_cast<char*>(&file_header), sizeof(file_header));
        file.read(reinterpret_cast<char*>(&bmp_info_header), sizeof(bmp_info_header));

        if (file_header.file_type != 0x4D42)
            throw std::runtime_error("File is not BMP type");
        if (bmp_info_header.bits_per_pixel != 24 && bmp_info_header.bits_per_pixel != 32)
            throw std::runtime_error("File has unsupported bits per pixel (" + std::to_string(bmp_info_header.bits_per_pixel) + "). Only 24 and 32 are supported");

        // std::cout << std::setfill('0') << std::setw(8) << std::hex << file_header.file_size << '\n';
        // std::cout << std::setfill('0') << std::setw(8) << std::hex << file_header.offset_data << '\n';

        // std::cout << std::setfill('0') << std::setw(8) << std::hex << bmp_info_header.width << '\n';
        // std::cout << std::setfill('0') << std::setw(8) << std::hex << bmp_info_header.height << '\n';
        
        // std::cout << std::setfill('0') << std::setw(8) << std::hex << bmp_info_header.bits_per_pixel << '\n';

        this->size.x = bmp_info_header.width;
        this->size.y = bmp_info_header.height;

        channels = bmp_info_header.bits_per_pixel / 8;
        const size_t data_size = (bmp_info_header.width * bmp_info_header.height * channels);
        pixels_data.resize(data_size);

        file.seekg(file_header.offset_data);
        file.read(reinterpret_cast<char*>(pixels_data.data()), data_size);
    }
    file.close();

    this->pixels = new IC::Pixel*[this->size.y];
    for (size_t y = 0; y < this->size.y; y++)
        this->pixels[y] = new IC::Pixel[this->size.x];
    
    for (size_t y = 0; y < this->size.y; y++)
    {
        for (size_t x = 0; x < this->size.x; x++)
        {
            int index = (y * bmp_info_header.width + x) * channels;
            this->pixels[y][x].r = pixels_data[index];
            this->pixels[y][x].g = pixels_data[index + 1];
            this->pixels[y][x].b = pixels_data[index + 2];

            if (channels == 4)
                this->pixels[y][x].a = pixels_data[index + 3];
        }
    }
}

Material_BMP::Material_BMP(IC::Pixel** pixels, int width, int height)
    : Material(pixels, width, height)
{
    this->file_header.offset_data = 0x003C; //60

    this->bmp_info_header.width = width;
    this->bmp_info_header.height = height;

    this->bmp_info_header.bits_per_pixel = 24;

    this->file_header.file_size = sizeof(file_header) + sizeof(bmp_info_header) + width * height * 3;
}

void Material_BMP::Export(const char *file_path)
{
    std::ofstream ofs(file_path);
    
    ofs.write(reinterpret_cast<char*>(&file_header), sizeof(file_header));
    ofs.write(reinterpret_cast<char*>(&bmp_info_header), sizeof(bmp_info_header));

    int channels = bmp_info_header.bits_per_pixel / 8;
    const int data_size = (bmp_info_header.width * bmp_info_header.height * channels);
    std::vector<uint8_t> pixels_data(data_size);

    for (size_t y = 0; y < this->size.y; y++)
        for (size_t x = 0; x < this->size.x; x++)
        {
            int index = (y * bmp_info_header.width + x) * channels;
            pixels_data[index    ] = this->pixels[y][x].r;
            pixels_data[index + 1] = this->pixels[y][x].g;
            pixels_data[index + 2] = this->pixels[y][x].b;

            if (channels == 4) 
                pixels_data[index + 3] = this->pixels[y][x].a;
        }

    ofs.seekp(file_header.offset_data);
    ofs.write(reinterpret_cast<char*>(pixels_data.data()), pixels_data.size());
    
    ofs.close();
}

Material_BMP::Material_BMP(uint8_t* data, int width, int height, bool has_alpha)
{
    this->bmp_info_header.bits_per_pixel = (has_alpha ? 32 : 24);
    this->size = Vector_i2(width, height);
    
    this->pixels = new IC::Pixel*[this->size.y];
    for (size_t y = 0; y < this->size.y; y++)
        this->pixels[y] = new IC::Pixel[this->size.x];
    
    int channels = bmp_info_header.bits_per_pixel / 8;

    for (size_t y = 0; y < this->size.y; y++)
    {
        for (size_t x = 0; x < this->size.x; x++)
        {
            int index = (y * bmp_info_header.width + x) * channels;
            this->pixels[y][x].r = data[index];
            this->pixels[y][x].g = data[index + 1];
            this->pixels[y][x].b = data[index + 2];

            if (channels == 4)
                this->pixels[y][x].a = data[index + 3];
        }
    }
}