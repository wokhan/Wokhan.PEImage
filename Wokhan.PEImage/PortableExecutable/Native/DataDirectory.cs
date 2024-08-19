using System;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.PortableExecutable.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct DataDirectory<T> where T : unmanaged, INullableDataDirectoryEntry
{
    public readonly uint VirtualAddress;
    public readonly uint Size;

    public readonly T Get(NativeMapper reader) 
    {
        return reader.Map<T>((nint)VirtualAddress);
    }

    public readonly T[] GetEntries(NativeMapper reader)
    {
        return reader.MapArray<T>((nint)VirtualAddress, dir => dir.IsNull());
    }
}
