#include "Pixel.hpp"

namespace IC
{

Pixel::Pixel(uint8_t r, uint8_t g, uint8_t b, uint8_t a) : r(r), g(g), b(b), a(a) {}
Pixel::Pixel(uint8_t val) : Pixel(val, val, val, 1) {}

Pixel::Pixel() : Pixel(0) {}

}
