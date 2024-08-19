# Wokhan.PEImage: an in-memory PortableExecultable (PE) image "reader"

The goal of this library is to provide a light and efficient way to read PEImage without allocating anything (directly mapping to blittable structs). There are other parsers out there (even one in System.Reflection.PortableExecutable I used some enums from), but I wanted to play around with direct memory access as I really like that subject ;)

You can see this project as a hobby (while I'm using it in at least one app and it should work anyway).

> :warning: Known limitation: when parsing the PE image of a module *in the current process only*, it directly maps objects to in-memory data: if the memory is modified for the target module, result will be unpredictable, so structures should be used as soon as loaded in this case.
> \
> Anyway the main goal of this lib is to parse module for another process: then it's not an issue as I'm calling ReadProcessMemory first to copy the corresponding data in the current process' memory.

> :information_source: All structures and descriptions themselves comes directly from the Microsoft PE Format specification available at 
https://learn.microsoft.com/en-us/windows/win32/debug/pe-format

