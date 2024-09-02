using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Windows.Win32;

namespace Wokhan.PEImage;

/// <summary>
/// Provides methods to map native structures and arrays from a process's memory.
/// </summary>
public class NativeMapper
{
    /// <summary>
    /// Handle to the process whose memory is being mapped.
    /// </summary>
    readonly SafeHandle? SafeHandle;

    /// <summary>
    /// Base address of the module in the process's memory.
    /// </summary>
    readonly nint ModuleBaseAddress;

    /// <summary>
    /// Initializes a new instance of the <see cref="NativeMapper"/> class.
    /// </summary>
    /// <param name="handle">Handle to the process.</param>
    /// <param name="moduleBaseAddress">Base address of the module.</param>
    internal NativeMapper(SafeHandle? handle, nint moduleBaseAddress)
    {
        ModuleBaseAddress = moduleBaseAddress;
        SafeHandle = handle;
    }

    /// <summary>
    /// Maps a structure from the process's memory.
    /// </summary>
    /// <typeparam name="T">Type of the structure to map.</typeparam>
    /// <param name="rva">Relative virtual address of the structure.</param>
    /// <returns>The mapped structure.</returns>
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
            return Unsafe.Read<T>((void*)(ModuleBaseAddress + rva));
        }
    }

    /// <summary>
    /// Maps an array of structures from the process's memory until a stop condition is met or the maximum length is reached.
    /// </summary>
    /// <typeparam name="TElement">Type of the elements in the array.</typeparam>
    /// <param name="rva">Relative virtual address of the array.</param>
    /// <param name="stopCondition">Function that determines when to stop mapping.</param>
    /// <param name="maxLength">Maximum number of elements to map.</param>
    /// <returns>The mapped array of structures.</returns>
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

    /// <summary>
    /// Maps an array of structures from the process's memory with a specified length.
    /// </summary>
    /// <typeparam name="TElement">Type of the elements in the array.</typeparam>
    /// <param name="rva">Relative virtual address of the array.</param>
    /// <param name="len">Number of elements to map.</param>
    /// <returns>The mapped array of structures.</returns>
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

    /// <summary>
    /// Copies memory from the process's memory to a buffer.
    /// </summary>
    /// <param name="rva">Relative virtual address of the memory to copy.</param>
    /// <param name="buffer">Buffer to copy the memory to.</param>
    /// <param name="len">Number of bytes to copy.</param>
    private unsafe void CopyProcessMemory(nint rva, void* buffer, uint len)
    {
        var bytesRead = new nuint();
        if (!NativeMethods.ReadProcessMemory(SafeHandle, (void*)(ModuleBaseAddress + rva), buffer, len, &bytesRead) || bytesRead != len)
        {
            throw new Win32Exception($"Failed to read process memory at {ModuleBaseAddress + rva:X16}. {bytesRead} byte(s) read over {len}.");
        }
    }

    /// <summary>
    /// Reads an ANSI string from the process's memory.
    /// </summary>
    /// <param name="rva">Relative virtual address of the string.</param>
    /// <param name="maxLength">Maximum length of the string.</param>
    /// <returns>The read string.</returns>
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
