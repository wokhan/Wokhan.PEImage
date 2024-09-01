using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Headers.Directories;

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct DataDirectory<T> where T : unmanaged, INullableDataDirectoryEntry
{
    public readonly uint VirtualAddress;
    public readonly uint Size;

    public readonly T GetSingle(NativeMapper mapper)
    {
        return mapper.Map<T>((nint)VirtualAddress);
    }

    public readonly T[] GetAllEntries(NativeMapper mapper)
    {
        return mapper.MapArray<T>((nint)VirtualAddress, dir => dir.IsNull());
    }
}
