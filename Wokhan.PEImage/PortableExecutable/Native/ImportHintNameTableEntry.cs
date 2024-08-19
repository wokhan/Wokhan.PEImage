using System.Runtime.InteropServices;

namespace Wokhan.PEImage.PortableExecutable.Native;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ImportHintNameTableEntry
{
    [FieldOffset(0)]
    /// <summary>
    /// An index into the export name pointer table. A match is attempted first with this value. If it fails, a binary search is performed on the DLL's export name pointer table. 
    /// </summary>
    public readonly ushort Hint;

    [FieldOffset(2)]
    /// <summary>
    /// An ASCII string that contains the name to import. This is the string that must be matched to the public name in the DLL. This string is case sensitive and terminated by a null byte. 
    /// </summary>
    private readonly sbyte _nameFirstByte;

    public unsafe readonly string GetName()
    {
        fixed (sbyte* nameptr = &_nameFirstByte)
        {
            return new string(nameptr);
        }
    }
}
