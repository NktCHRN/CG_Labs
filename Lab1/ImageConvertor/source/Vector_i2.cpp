#include "Vector_i2.hpp"

Vector_i2::Vector_i2(int x, int y)
{
    this->x = x;
    this->y = y;
}

Vector_i2::Vector_i2(int x) : Vector_i2(x, x) {}

Vector_i2::Vector_i2() : Vector_i2(0) {}

