#pragma once
#include <stdio.h>

struct Vector2i {
    int x, y;
    
    Vector2i(int x, int y);
    Vector2i(int x);
    Vector2i();

    bool operator==(const Vector2i & other) const;
};
