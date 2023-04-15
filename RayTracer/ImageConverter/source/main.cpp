#include <iostream>
#include <unordered_map>
#include "Material.hpp"
#include "ImageReader.hpp"
#include "ImageWriter.hpp"

void SampleFilter(IC::Material& mat)
{
    auto pixels = mat.GetPixels();
    for (size_t y = 0; y < mat.GetSize().y; y++)
        for (size_t x = 0; x < mat.GetSize().x; x++)
        {
            int new_value = (int)pixels[y][x].b + 90;
            if (new_value <= 255)
                pixels[y][x].b = (uint8_t)new_value;
            else
                pixels[y][x].b = (uint8_t)255;
        }
}

int main(int argc, const char * argv[])
{
    if (argc < 4)
    {
        std::cout << "You forget to pass some of the parameters. Parameters are: --source, --output, --goal-format" << std::endl;
        exit(1);
    }

    std::unordered_map<std::string, std::string> options;
    for (int i = 1; i < argc; ++i)
    {
        std::string arg = argv[i];
        if (arg.substr(0, 2) == "--")
        {
            std::string::size_type pos = arg.find('=');
            if (pos != std::string::npos)
            {
                std::string key = arg.substr(2, pos - 2);
                std::string value = arg.substr(pos + 1);
                options[key] = value;
            }
        }
    }

    std::string src;
    std::string dst;
    std::string dst_ext;

    if (options.count("source"))
        src = options["source"];

    if (options.count("output"))
        dst = options["output"];

    if (options.count("goal-format"))
        dst_ext = options["goal-format"];

    std::cout << "Read from " << src << " write to " << dst << std::endl;

    auto reader = IC::ImageReader::GetInstance();
    auto writer = IC::ImageWriter::GetInstance();

    auto mat = reader->ReadPath(src.c_str());
    if (mat)
    {
        // SampleFilter(*mat);
        std::cout << "Mat info: " << std::endl;
        std::cout << mat->GetSize().x << " " << mat->GetSize().y << std::endl;
        writer->WriteMat(mat, dst.c_str(), dst_ext.c_str());
    }

    return 0;
}
