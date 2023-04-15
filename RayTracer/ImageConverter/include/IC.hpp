#pragma once

#ifdef _WIN32
    #ifdef IMG_EXPORT
        #define IMG_API __declspec(dllexport)
    #else
        #define IMG_API __declspec(dllimport)
    #endif
#else
    #define IMG_API
#endif