using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Windows.Win32;

namespace Wokhan.PEImage;

public class NativeMapper
{
    readonly SafeHandle? SafeHandle;
    readonly nint ModuleBaseAddress;

    internal NativeMapper(SafeHandle? handle, nint moduleBaseAddress)
    {
        ModuleBaseAddress = moduleBaseAddress;
        SafeHandle = handle;
    }

    internal unsafe T Map<T>(nint rva) where T : unmanaged
    {
        if (SafeHandle is not null)
        {
            var ret = new T();

            CopyProcessMemory(rva, &ret, (uint)sizeof(T));

            return ret;
        }
        else
        {
            return Unsafe.Read<T>((void*)(ModuleBaseAddress + rva)); //*(T*)(ModuleBaseAddress + rva);
        }
    }

    internal unsafe TElement[] MapArray<TElement>(nint rva, Func<TElement, bool> stopCondition, int maxLength = 256) where TElement : unmanaged
    {
        List<TElement> arrayItems = new();

        uint cpt = 0;
        bool keepGoing;

        do
        {
            var item = Map<TElement>(rva);

            keepGoing = !stopCondition(item);

            if (keepGoing)
            {
                cpt++;

                arrayItems.Add(item);
                rva = rva + sizeof(TElement);
            }
        } while (keepGoing && cpt < maxLength);

        return arrayItems.ToArray();
    }

    internal unsafe TElement[] MapArray<TElement>(nint rva, uint len) where TElement : unmanaged
    {
        if (SafeHandle is not null)
        {
            var array = new TElement[len];
            fixed (TElement* ptr = array)
            {
                CopyProcessMemory(rva, ptr, (uint)(len * sizeof(TElement)));
            }
            return array;
        }
        else
        {
            return new ReadOnlySpan<TElement>((void*)(ModuleBaseAddress + rva), (int)len).ToArray();
        }
    }

    private unsafe void CopyProcessMemory(nint rva, void* buffer, uint len)
    {
        var bytesRead = new nuint();
        if (!NativeMethods.ReadProcessMemory(SafeHandle, (void*)(ModuleBaseAddress + rva), buffer, len, &bytesRead) || bytesRead != len)
        {
            throw new Win32Exception($"Failed to read process memory at {ModuleBaseAddress + rva:X16}. {bytesRead} byte(s) read over {len}.");
        }
    }

    internal unsafe string ReadStringANSI(nint rva, int maxLength = 4096)
    {
        sbyte* address;
        if (SafeHandle is not null)
        {
            // We have to copy the memory buffer before reading (to avoid memory access exceptions).
            fixed (sbyte* bufferPtr = new sbyte[maxLength])
            {
                CopyProcessMemory(rva, bufferPtr, (uint)maxLength);
                address = bufferPtr;
            }
        }
        else
        {
            address = (sbyte*)(ModuleBaseAddress + rva);
        }

        return new string(address);
    }
}
