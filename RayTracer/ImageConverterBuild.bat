cd ImageConverter

set "build_folder=build"

if not exist "%build_folder%" (
    mkdir "%build_folder%"
)

cd "%build_folder%"

cmake ..

cd ../
cmake --build "%build_folder%" --config Debug --target all_targets -j12

pause