using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.idata;

[StructLayout(LayoutKind.Explicit)]
public struct ImportHintNameTableEntry
{
    [FieldOffset(0)]
    /// <summary>
    /// An index into the export name pointer table. A match is attempted first with this value. If it fails, a binary search is performed on the DLL's export name pointer table. 
    /// </summary>
    public readonly ushort Hint;

    [FieldOffset(2)]
    /// <summary>
    /// An ASCII string that contains the name to import. This is the string that must be matched to the public name in the DLL. This string is case sensitive and terminated by a null byte. 
    /// Note: storing 256 bytes as this is the maximum length for a null terminated string in a PE image, but not all of them will be used.
    /// </summary>
    private unsafe fixed sbyte _name[256];

    public unsafe readonly string Name
    {
        get
        {
            fixed (sbyte* nameptr = _name)
            {
                return new string(nameptr);
            }
        }
    }
}
