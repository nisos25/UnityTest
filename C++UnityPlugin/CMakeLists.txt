cmake_minimum_required(VERSION 3.25)
project(UnityPlugin)

set(CMAKE_CXX_STANDARD 17)

add_library(UnityPlugin SHARED library.cpp)

set_target_properties(UnityPlugin PROPERTIES PREFIX "")
set_target_properties(UnityPlugin PROPERTIES OUTPUT_NAME "UnityPlugin")

set_target_properties(UnityPlugin PROPERTIES SUFFIX ".dll")


