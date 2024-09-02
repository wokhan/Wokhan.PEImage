using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.rsrc;

/// <summary>
/// Represents an entry in the resource directory of the PE file.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct ResourceDirectoryEntry
{
    /// <summary>
    /// The offset of a string that specifies the Type, Name, or Language ID entry, depending on the level of the table.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint NameOffset;

    /// <summary>
    /// A 32-bit integer that identifies the Type, Name, or Language ID entry.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint IntegerID;

    /// <summary>
    /// If the high bit is 0, this is the address of a Resource Data entry (a leaf node).
    /// </summary>
    [FieldOffset(4)]
    public readonly uint DataEntryOffset;

    /// <summary>
    /// If the high bit is 1, the lower 31 bits are the address of another resource directory table (the next level down).
    /// </summary>
    [FieldOffset(4)]
    public readonly uint SubdirectoryOffset;
}
