cmake_minimum_required(VERSION 3.24)

project(ImageConverter)

enable_language(C)
enable_language(CXX)

set(CMAKE_CXX_STANDARD 17)

# find_package(zlib REQUIRED)

include_directories(include)

file(GLOB BASE_SRC
    "IC.hpp"
    "source/Vector_i2.cpp" "include/Vector_i2.hpp"
    "source/Pixel.cpp" "include/Pixel.hpp" 
    "source/Material.cpp" "include/Material.hpp"
)

add_definitions(-D IMG_EXPORT)

add_library(ImageReader.PPM SHARED ${BASE_SRC} "source/Material_PPM.cpp" "include/Material_PPM.hpp")
add_library(ImageWriter.PPM SHARED ${BASE_SRC} "source/Material_PPM.cpp" "include/Material_PPM.hpp")

# add_library(ImageReader.BMP SHARED ${BASE_SRC} "source/Material_BMP.cpp" "include/Material_BMP.hpp")
# add_library(ImageWriter.BMP SHARED ${BASE_SRC} "source/Material_BMP.cpp" "include/Material_BMP.hpp")

# add_library(ImageReader.PNG SHARED ${BASE_SRC} "source/Material_PNG.cpp" "include/Material_PNG.hpp")
# add_library(ImageWriter.PNG SHARED ${BASE_SRC} "source/Material_PNG.cpp" "include/Material_PNG.hpp")

# include_directories(ZLIB_INCLUDE_DIRS)
# target_link_libraries(ImageReader.PNG ZLIB::ZLIB)

add_library(${PROJECT_NAME}_LIB STATIC ${BASE_SRC}
    "source/ImageReader.cpp" "include/ImageReader.hpp"
    "source/ImageWriter.cpp" "include/ImageWriter.hpp"
)

add_executable(${PROJECT_NAME}
    ${BASE_SRC}
    "source/ImageReader.cpp" "include/ImageReader.hpp"
    "source/ImageWriter.cpp" "include/ImageWriter.hpp"
    "source/main.cpp"
)