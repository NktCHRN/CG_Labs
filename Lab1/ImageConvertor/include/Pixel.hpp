#pragma once
#include <stdint.h>

struct Pixel {
    uint8_t r, g, b, a;
    
    Pixel(uint8_t r, uint8_t g, uint8_t b, uint8_t a);
    Pixel(uint8_t val);
    Pixel();
};