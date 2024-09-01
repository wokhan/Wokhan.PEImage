using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.rsrc;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ResourceDirectoryTable : INullableDataDirectoryEntry
{
    /// <summary>
    /// Resource flags. This field is reserved for future use. It is currently set to zero.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint Characteristics;

    /// <summary>
    /// The time that the resource data was created by the resource compiler.
    /// </summary>
    [FieldOffset(4)]
    public readonly uint TimeDateStamp;

    /// <summary>
    /// The major version number, set by the user.
    /// </summary>
    [FieldOffset(8)]
    public readonly ushort MajorVersion;

    /// <summary>
    /// The minor version number, set by the user.
    /// </summary>
    [FieldOffset(10)]
    public readonly ushort MinorVersion;

    /// <summary>
    /// The number of directory entries immediately following the table that use strings to identify Type, Name, or Language entries (depending on the level of the table).
    /// </summary>
    [FieldOffset(12)]
    public readonly ushort NumberOfNameEntries;

    /// <summary>
    /// The number of directory entries immediately following the Name entries that use numeric IDs for Type, Name, or Language entries.
    /// </summary>
    [FieldOffset(14)]
    public readonly ushort NumberOfIDEntries;

    public bool IsNull() => false;
}

