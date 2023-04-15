#pragma once

#include <vector>
#include "Material.hpp"

#pragma pack(push, 1)

struct BMPFileHeader {
    uint16_t file_type { 0x4D42 };     // File type always BM which is 0x4D42
    uint32_t file_size { 0 };     // Size of the file (in bytes)
    uint16_t reserved1 { 0 };     // Reserved, always 0
    uint16_t reserved2 { 0 };     // Reserved, always 0
    uint32_t offset_data { 54 };   // Start position of pixel data (bytes from the beginning of the file)
};

#pragma pack(pop) 

#pragma pack(push, 1)

struct BMPInfoHeader {
    uint32_t header_size { 40 };                  // Size of this header (in bytes)
    uint32_t width { 0 };                 // width of bitmap in pixels
    uint32_t height { 0 };                // width of bitmap in pixels
                                    // (if positive, bottom-up, with origin in lower left corner)
                                    // (if negative, top-down, with origin in upper left corner)
    uint16_t planes { 1 };                // No. of planes for the target device, this is always 1
    uint16_t bits_per_pixel { 24 };        // No. of bits per pixel
    uint32_t compression { 0 };           // 0 or 3 - uncompressed. THIS PROGRAM CONSIDERS ONLY UNCOMPRESSED BMP images
    uint32_t img_size_bytes { 0 };        // 0 - for uncompressed images
    uint32_t x_pixels_per_meter { 320 };
    uint32_t y_pixels_per_meter { 320 };
    uint32_t colors_used { 0 };           // No. color indexes in the color table. Use 0 for the max number of colors allowed by bit_count
    uint32_t colors_important { 0 };      // No. of colors used for displaying the bitmap. If 0 all colors are required
};

#pragma pack(pop)

class Material_BMP: public IC::Material
{
public:
    Material_BMP(const char* file_path);
    Material_BMP(IC::Pixel** pixels, int width, int height);
    Material_BMP(uint8_t* data, int width, int height, bool has_alpha = false);
    
    void Export(const char *file_path) override;
    void ExportSample(const char *file_path) override {};

private:
    BMPFileHeader file_header;
    BMPInfoHeader bmp_info_header;

};