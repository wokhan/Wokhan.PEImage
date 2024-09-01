using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.rsrc;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ResourceDirectoryEntry
{
    /// <summary>
    /// The offset of a string that gives the Type, Name, or Language ID entry, depending on level of table.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint NameOffset;

    /// <summary>
    /// A 32-bit integer that identifies the Type, Name, or Language ID entry.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint IntegerID;

    /// <summary>
    /// High bit 0. Address of a Resource Data entry (a leaf).
    /// </summary>
    [FieldOffset(4)]
    public readonly uint DataEntryOffset;

    /// <summary>
    /// High bit 1. The lower 31 bits are the address of another resource directory table (the next level down).
    /// </summary>
    [FieldOffset(4)]
    public readonly uint SubdirectoryOffset;
}

