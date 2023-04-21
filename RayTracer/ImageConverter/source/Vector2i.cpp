#include "Vector2i.hpp"

Vector2i::Vector2i(int x, int y)
{
    this->x = x;
    this->y = y;
}

Vector2i::Vector2i(int x) : Vector2i(x, x) {}

Vector2i::Vector2i() : Vector2i(0) {}

bool Vector2i::operator==(const Vector2i & other) const
{
    return this->x == other.x && this->y == other.y;
}