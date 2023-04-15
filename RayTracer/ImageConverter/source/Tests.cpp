#include <gtest/gtest.h>
#include <filesystem>
#include <iostream>
#include "ImageReader.hpp"
#include "ImageWriter.hpp"
#include "Material.hpp"

namespace fs = std::filesystem;

fs::path ORIGIN_FOLDER_PATH = "../resources/origin/";
fs::path EXPORT_FOLDER_PATH = "../resources/export/";

TEST(ValidateReading, ReadingPPM)
{
    IC::Material * mat = IC::ImageReader::GetInstance()->ReadPath((ORIGIN_FOLDER_PATH / "landscape.ppm").c_str());

    EXPECT_EQ(mat->GetSize().x, 640);
    EXPECT_EQ(mat->GetSize().y, 361);
}

TEST(ValidateReading, ReadingBMP)
{
    IC::Material * mat = IC::ImageReader::GetInstance()->ReadPath((ORIGIN_FOLDER_PATH / "bmp_24.bmp").c_str());

    EXPECT_EQ(mat->GetSize().x, 200);
    EXPECT_EQ(mat->GetSize().y, 200);
}

TEST(ValidateConversion, PPM2PPM)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "landscape.ppm";
    fs::path export_path = EXPORT_FOLDER_PATH / "landscape_export.ppm";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).c_str(), ".ppm");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).c_str());

    EXPECT_EQ(origin->GetSize(), exported->GetSize());

    fs::remove(export_path);
}

TEST(ValidateConversion, BMP2BMP)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "bmp_24.bmp";
    fs::path export_path = EXPORT_FOLDER_PATH / "bmp_24_export.bmp";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).c_str(), ".bmp");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).c_str());

    EXPECT_EQ(origin->GetSize(), exported->GetSize());

    fs::remove(export_path);
}

TEST(ValidateConversion, PPM2BMP)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "landscape.ppm";
    fs::path export_path = EXPORT_FOLDER_PATH / "landscape_export_ppm.bmp";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).c_str(), ".bmp");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).c_str());

    EXPECT_EQ(origin->GetSize(), exported->GetSize());

    fs::remove(export_path);
}

TEST(ValidateConversion, BMP2PPM)
{
    fs::path origin_path = ORIGIN_FOLDER_PATH / "bmp_24.bmp";
    fs::path export_path = EXPORT_FOLDER_PATH / "bmp_24_export_bmp.ppm";

    IC::Material * origin = IC::ImageReader::GetInstance()->ReadPath((origin_path).c_str());
    IC::ImageWriter::GetInstance()->WriteMat(origin, (export_path).c_str(), ".ppm");

    IC::Material * exported = IC::ImageReader::GetInstance()->ReadPath((export_path).c_str());

    EXPECT_EQ(origin->GetSize(), exported->GetSize());

    fs::remove(export_path);
}