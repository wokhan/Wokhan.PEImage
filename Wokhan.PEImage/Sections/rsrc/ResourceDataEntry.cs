using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.rsrc;

/// <summary>
/// Represents an entry of resource data in the PE file.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct ResourceDataEntry
{
    /// <summary>
    /// The relative virtual address (RVA) of a unit of resource data in the Resource Data area.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint DataRVA;

    /// <summary>
    /// The size, in bytes, of the resource data that is pointed to by the DataRVA field.
    /// </summary>
    [FieldOffset(4)]
    public readonly uint Size;

    /// <summary>
    /// The code page used to decode code point values within the resource data. Typically, this would be the Unicode code page.
    /// </summary>
    [FieldOffset(8)]
    public readonly uint Codepage;

    /// <summary>
    /// Reserved, must be zero.
    /// </summary>
    [FieldOffset(12)]
    public readonly uint Reserved;
}
