using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.rsrc;

/// <summary>
/// Represents a resource directory table in a PE image.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct ResourceDirectoryTable : INullableDataDirectoryEntry
{
    /// <summary>
    /// Gets the resource flags. This field is reserved for future use and is currently set to zero.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint Characteristics;

    /// <summary>
    /// Gets the time that the resource data was created by the resource compiler.
    /// </summary>
    [FieldOffset(4)]
    public readonly uint TimeDateStamp;

    /// <summary>
    /// Gets the major version number, set by the user.
    /// </summary>
    [FieldOffset(8)]
    public readonly ushort MajorVersion;

    /// <summary>
    /// Gets the minor version number, set by the user.
    /// </summary>
    [FieldOffset(10)]
    public readonly ushort MinorVersion;

    /// <summary>
    /// Gets the number of directory entries immediately following the table that use strings to identify Type, Name, or Language entries, depending on the level of the table.
    /// </summary>
    [FieldOffset(12)]
    public readonly ushort NumberOfNameEntries;

    /// <summary>
    /// Gets the number of directory entries immediately following the Name entries that use numeric IDs for Type, Name, or Language entries.
    /// </summary>
    [FieldOffset(14)]
    public readonly ushort NumberOfIDEntries;

    /// <summary>
    /// Determines whether the data directory entry is null.
    /// </summary>
    public bool IsNull() => false;
}
