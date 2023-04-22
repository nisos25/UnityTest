#include <string>

#ifndef UNITYPLUGIN_LIBRARY_H
#define UNITYPLUGIN_LIBRARY_H

#define DLLExport __declspec(dllexport)

struct TwoStrings {
    std::string string1;
    std::string string2;
    std::string concatenated;
};

extern "C"
{
    DLLExport void Concatenate(TwoStrings* strings);
}

#endif //UNITYPLUGIN_LIBRARY_H
