using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.idata;

/// <summary>
/// Represents an entry in the Import Hint/Name Table, which provides a hint and name for resolving imported functions in a PE file.
/// This struct includes both the hint index and the ASCII name string.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct ImportHintNameTableEntry
{
    [FieldOffset(0)]
    /// <summary>
    /// Index into the export name pointer table. This value is used for an initial match attempt. If it fails, a binary search is performed on the DLL's export name pointer table.
    /// </summary>
    public readonly ushort Hint;

    [FieldOffset(2)]
    /// <summary>
    /// ASCII string containing the name to import. This string must match the public name in the DLL, is case-sensitive, and is null-terminated.
    /// Note: Allocates 256 bytes as the maximum length for a null-terminated string in a PE image, though not all bytes may be used.
    /// </summary>
    private unsafe fixed sbyte _name[256];

    /// <summary>
    /// Gets the imported method name.
    /// </summary>
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
