using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections;

/// <summary>
/// Represents an entry in the section table of a PE image.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct SectionTableEntry
{
    /// <summary>
    /// An 8-byte, null-padded UTF-8 string. If the string is exactly 8 characters, there is no null terminator. For longer names, this field contains a slash (/) followed by an ASCII decimal number offset into the string table. Executable images do not use a string table and do not support section names longer than 8 characters. Long names in object files are truncated if emitted to an executable file.
    /// </summary>
    [FieldOffset(0)]
    private unsafe fixed sbyte _name[8];

    public unsafe string Name
    {
        get
        {
            fixed (sbyte* nameptr = _name)
            {
                return new string(nameptr, 0, 8).TrimEnd('\0');
            }
        }
    }

    /// <summary>
    /// Total size of the section when loaded into memory. If this value exceeds SizeOfRawData, the section is zero-padded. This field is valid only for executable images and should be zero for object files.
    /// </summary>
    [FieldOffset(8)]
    public readonly uint VirtualSize;

    /// <summary>
    /// For executable images, the address of the first byte of the section relative to the image base when loaded into memory. For object files, this is the address of the first byte before relocation is applied; compilers should set this to zero for simplicity. Otherwise, it is an arbitrary value subtracted from offsets during relocation.
    /// </summary>
    [FieldOffset(12)]
    public readonly uint VirtualAddress;

    /// <summary>
    /// Size of the section (for object files) or size of the initialized data on disk (for image files). For executable images, this must be a multiple of FileAlignment from the optional header. If less than VirtualSize, the remainder of the section is zero-filled. Because SizeOfRawData is rounded but VirtualSize is not, SizeOfRawData can be greater than VirtualSize. When a section contains only uninitialized data, this field should be zero.
    /// </summary>
    [FieldOffset(16)]
    public readonly uint SizeOfRawData;

    /// <summary>
    /// File pointer to the first page of the section within the COFF file. For executable images, this must be a multiple of FileAlignment from the optional header. For object files, the value should be aligned on a 4-byte boundary for best performance. When a section contains only uninitialized data, this field should be zero.
    /// </summary>
    [FieldOffset(20)]
    public readonly uint PointerToRawData;

    /// <summary>
    /// File pointer to the beginning of relocation entries for the section. Set to zero for executable images or if there are no relocations.
    /// </summary>
    [FieldOffset(24)]
    public readonly uint PointerToRelocations;

    /// <summary>
    /// File pointer to the beginning of line-number entries for the section. Set to zero if there are no COFF line numbers. This value should be zero for an image because COFF debugging information is deprecated.
    /// </summary>
    [FieldOffset(28)]
    public readonly uint PointerToLinenumbers;

    /// <summary>
    /// Number of relocation entries for the section. Set to zero for executable images.
    /// </summary>
    [FieldOffset(32)]
    public readonly ushort NumberOfRelocations;

    /// <summary>
    /// Number of line-number entries for the section. This value should be zero for an image because COFF debugging information is deprecated.
    /// </summary>
    [FieldOffset(34)]
    public readonly ushort NumberOfLinenumbers;

    /// <summary>
    /// Flags describing the characteristics of the section. See Section Flags for more information.
    /// </summary>
    [FieldOffset(36)]
    public readonly SectionFlags Characteristics;
}
