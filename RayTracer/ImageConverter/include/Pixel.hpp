#pragma once
#include <stdint.h>
#include <iostream>

namespace IC
{

struct Pixel {
    uint8_t r, g, b, a;
    
    Pixel(uint8_t r, uint8_t g, uint8_t b, uint8_t a);
    Pixel(uint8_t val);
    Pixel();

    bool operator==(const Pixel & other) const;
    bool operator!=(const Pixel & other) const;
    friend std::ostream &operator<<(std::ostream &os, const Pixel &p);
};

}