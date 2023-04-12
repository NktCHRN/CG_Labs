#pragma once

#include "ManagedObject.h"
#include "Material.hpp"

using namespace System;

namespace CLI
{
    public ref class MaterialWrapper : public ManagedObject<IC::Material>
    {
    public:
        MaterialWrapper(IC::Material * mat);
    };
}