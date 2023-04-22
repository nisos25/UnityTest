#include <iostream>

#define DLLExport __declspec(dllexport)

struct TwoStrings{
    std::string string1;
    std::string string2;
    std::string concatenated;
};

extern "C" {
    DLLExport void Concatenate(TwoStrings *strings) {
        strings->concatenated = strings->string1 + strings->string2;
    }
}



