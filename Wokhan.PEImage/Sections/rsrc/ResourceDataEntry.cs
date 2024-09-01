using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.rsrc;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ResourceDataEntry
{
    /// <summary>
    /// The address of a unit of resource data in the Resource Data area.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint DataRVA;

    /// <summary>
    /// The size, in bytes, of the resource data that is pointed to by the Data RVA field.
    /// </summary>
    [FieldOffset(4)]
    public readonly uint Size;

    /// <summary>
    /// The code page that is used to decode code point values within the resource data. Typically, the code page would be the Unicode code page.
    /// </summary>
    [FieldOffset(8)]
    public readonly uint Codepage;

    /// <summary>
    /// Reserved, must be 0.
    /// </summary>
    [FieldOffset(12)]
    public readonly uint Reserved;
}

