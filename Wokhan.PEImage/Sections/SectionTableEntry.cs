using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections;


[StructLayout(LayoutKind.Explicit)]
public struct SectionTableEntry
{
    /// <summary>
    /// An 8-byte, null-padded UTF-8 encoded string. If the string is exactly 8 characters long, there is no terminating null. For longer names, this field contains a slash (/) that is followed by an ASCII representation of a decimal number that is an offset into the string table. Executable images do not use a string table and do not support section names longer than 8 characters. Long names in object files are truncated if they are emitted to an executable file.
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
    /// The total size of the section when loaded into memory. If this value is greater than SizeOfRawData, the section is zero-padded. This field is valid only for executable images and should be set to zero for object files.
    /// </summary>
    [FieldOffset(8)]
    public readonly uint VirtualSize;

    /// <summary>
    /// For executable images, the address of the first byte of the section relative to the image base when the section is loaded into memory. For object files, this field is the address of the first byte before relocation is applied; for simplicity, compilers should set this to zero. Otherwise, it is an arbitrary value that is subtracted from offsets during relocation.
    /// </summary>
    [FieldOffset(12)]
    public readonly uint VirtualAddress;

    /// <summary>
    /// The size of the section (for object files) or the size of the initialized data on disk (for image files). For executable images, this must be a multiple of FileAlignment from the optional header. If this is less than VirtualSize, the remainder of the section is zero-filled. Because the SizeOfRawData field is rounded but the VirtualSize field is not, it is possible for SizeOfRawData to be greater than VirtualSize as well. When a section contains only uninitialized data, this field should be zero.
    /// </summary>
    [FieldOffset(16)]
    public readonly uint SizeOfRawData;

    /// <summary>
    /// The file pointer to the first page of the section within the COFF file. For executable images, this must be a multiple of FileAlignment from the optional header. For object files, the value should be aligned on a 4-byte boundary for best performance. When a section contains only uninitialized data, this field should be zero.
    /// </summary>
    [FieldOffset(20)]
    public readonly uint PointerToRawData;

    /// <summary>
    /// The file pointer to the beginning of relocation entries for the section. This is set to zero for executable images or if there are no relocations.
    /// </summary>
    [FieldOffset(24)]
    public readonly uint PointerToRelocations;

    /// <summary>
    /// The file pointer to the beginning of line-number entries for the section. This is set to zero if there are no COFF line numbers. This value should be zero for an image because COFF debugging information is deprecated.
    /// </summary>
    [FieldOffset(28)]
    public readonly uint PointerToLinenumbers;

    /// <summary>
    /// The number of relocation entries for the section. This is set to zero for executable images.
    /// </summary>
    [FieldOffset(32)]
    public readonly ushort NumberOfRelocations;

    /// <summary>
    /// The number of line-number entries for the section. This value should be zero for an image because COFF debugging information is deprecated.
    /// </summary>
    [FieldOffset(34)]
    public readonly ushort NumberOfLinenumbers;

    /// <summary>
    /// The flags that describe the characteristics of the section. For more information, see Section Flags.
    /// </summary>
    [FieldOffset(36)]
    public readonly SectionFlags Characteristics;
}
