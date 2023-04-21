#include <gtest/gtest.h>
#include <gmock/gmock.h>
#include <filesystem>
#include <iostream>
#include "ImageReader.hpp"
#include "ImageWriter.hpp"
#include "Material.hpp"

void SampleFilter(IC::Material * mat)
{
    auto pixels = mat->GetPixels();
    for (size_t y = 0; y < mat->GetSize().y; y++)
        for (size_t x = 0; x < mat->GetSize().x; x++)
        {
            int new_r = (int)pixels[y][x].r + 90;
            if (new_r <= 255)
                pixels[y][x].r = (uint8_t)new_r;
            else
                pixels[y][x].r = (uint8_t)255;

            int new_g = (int)pixels[y][x].g + 90;
            if (new_g <= 255)
                pixels[y][x].g = (uint8_t)new_g;
            else
                pixels[y][x].g = (uint8_t)255;

            int new_b = (int)pixels[y][x].b + 90;
            if (new_b <= 255)
                pixels[y][x].b = (uint8_t)new_b;
            else
                pixels[y][x].b = (uint8_t)255;
        }
}

namespace fs = std::filesystem;

fs::path ORIGIN_FOLDER_PATH = "../resources/origin/";
fs::path EXPORT_FOLDER_PATH = "../resources/export/";

TEST(ValidateReading, ReadingPPM)
{
    IC::Material * mat = IC::ImageReader::GetInstance()->ReadPath((ORIGIN_FOLDER_PATH / "landscape.ppm").string().c_str());

    EXPECT_EQ(mat->GetSize().x, 640);
    EXPECT_EQ(mat->GetSize().y, 361);
}

TEST(ValidateReading, ReadingBMP)
{
    IC::Material * mat = IC::ImageReader::GetInstance()->ReadPath((ORIGIN_FOLDER_PATH / "bmp_24.bmp").string().c_str());

    EXPECT_EQ(mat->GetSize().x, 200);
    EXPECT_EQ(mat->GetSize().y, 200);
}

TEST(ValidateConversion, PPM2PPM)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "landscape.ppm";
    fs::path export_path = EXPORT_FOLDER_PATH / "landscape_export.ppm";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).string().c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).string().c_str(), ".ppm");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).string().c_str());

    EXPECT_EQ(origin->GetSize(), exported->GetSize());

    fs::remove(export_path);
}

TEST(ValidateConversion, BMP2BMP)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "bmp_24.bmp";
    fs::path export_path = EXPORT_FOLDER_PATH / "bmp_24_export.bmp";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).string().c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).string().c_str(), ".bmp");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).string().c_str());

    EXPECT_EQ(origin->GetSize(), exported->GetSize());

    fs::remove(export_path);
}

TEST(ValidateConversion, PPM2BMP)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "landscape.ppm";
    fs::path export_path = EXPORT_FOLDER_PATH / "landscape_export_ppm.bmp";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).string().c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).string().c_str(), ".bmp");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).string().c_str());

    EXPECT_EQ(origin->GetSize(), exported->GetSize());

    fs::remove(export_path);
}

TEST(ValidateConversion, BMP2PPM)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "bmp_24.bmp";
    fs::path export_path = EXPORT_FOLDER_PATH / "bmp_24_export_bmp.ppm";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).string().c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).string().c_str(), ".ppm");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).string().c_str());

    EXPECT_EQ(origin->GetSize(), exported->GetSize());

    fs::remove(export_path);
}

MATCHER_P2(PixelsEQ, expected, size, "Pixels data equality")
{
    for (size_t y = 0; y < size.y; y++)
        for (size_t x = 0; x < size.x; x++)
        {
            if (arg[y][x] != expected[y][x])
                return false;
        }

    return true;
}

TEST(ValidateConversionPixels, PPM2PPM)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "landscape.ppm";
    fs::path export_path = EXPORT_FOLDER_PATH / "landscape_export.ppm";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).string().c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).string().c_str(), ".ppm");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).string().c_str());

    EXPECT_THAT(exported->GetPixels(), PixelsEQ(origin->GetPixels(), origin->GetSize()));
    fs::remove(export_path);
}

TEST(ValidateConversionPixels, PPM2PPM_False)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "landscape.ppm";
    fs::path export_path = EXPORT_FOLDER_PATH / "landscape_export.ppm";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).string().c_str());
    IC::Material * filtered = IC::ImageReader::GetInstance()->ReadPath((origin_path).string().c_str());
    SampleFilter(filtered);

    IC::ImageWriter::GetInstance()->WriteMat(filtered, (export_path).string().c_str(), ".ppm");
    filtered = IC::ImageReader::GetInstance()->ReadPath((export_path).string().c_str());

    EXPECT_THAT(origin->GetPixels(), testing::Not(PixelsEQ(filtered->GetPixels(), filtered->GetSize())));
    fs::remove(export_path);
}