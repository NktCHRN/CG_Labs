#include <gtest/gtest.h>
#include "ImageReader.hpp"
#include "Material.hpp"

TEST(ReadingPPM, ValidateReading)
{
    IC::Material * mat = IC::ImageReader::GetInstance()->ReadPath("../resources/origin/landscape.ppm");

    EXPECT_EQ(mat->GetSize().x, 640);
    EXPECT_EQ(mat->GetSize().y, 361);
}

TEST(ReadingBMP, ValidateReading)
{
    IC::Material * mat = IC::ImageReader::GetInstance()->ReadPath("../resources/origin/bmp_24.bmp");

    EXPECT_EQ(mat->GetSize().x, 200);
    EXPECT_EQ(mat->GetSize().y, 200);
}