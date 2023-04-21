#include "Pixel.hpp"

namespace IC
{

Pixel::Pixel(uint8_t r, uint8_t g, uint8_t b, uint8_t a) : r(r), g(g), b(b), a(a) {}
Pixel::Pixel(uint8_t val) : Pixel(val, val, val, 1) {}

Pixel::Pixel() : Pixel(0) {}

bool Pixel::operator==(const Pixel &other) const
{
    return this->r == other.r && this->g == other.g && this->b == other.b;
}

bool Pixel::operator!=(const Pixel &other) const
{
    return !(*this == other);
}

std::ostream &operator<<(std::ostream &os, const Pixel &p)
{
    return os << (int)p.r << " " << (int)p.g << " " << (int)p.b << std::endl;
}

}
