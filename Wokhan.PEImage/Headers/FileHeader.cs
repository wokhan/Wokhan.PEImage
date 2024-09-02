using System;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Headers;

[StructLayout(LayoutKind.Sequential)]
public readonly struct FileHeader
{
    /// <summary>
    /// Specifies the type of target machine.
    /// </summary>
    public readonly Machine Machine;

    /// <summary>
    /// Indicates the number of sections, which determines the size of the section table following the headers.
    /// </summary>
    public readonly ushort NumberOfSections;

    private readonly uint _timeDateStamp;

    /// <summary>
    /// Represents the low 32 bits of the seconds elapsed since January 1, 1970 (Unix epoch), indicating the file creation time.
    /// </summary>
    public readonly DateTime TimeDateStamp => DateTime.FromFileTime(_timeDateStamp);

    /// <summary>
    /// Points to the COFF symbol table's file offset, or zero if it doesn't exist. This should be zero for images as COFF debugging info is outdated.
    /// </summary>
    public readonly uint PointerToSymbolTable;

    /// <summary>
    /// Denotes the number of entries in the symbol table, which helps locate the string table that follows. This should be zero for images as COFF debugging info is outdated.
    /// </summary>
    public readonly uint NumberOfSymbols;

    /// <summary>
    /// Defines the size of the optional header, necessary for executables but not for object files. This should be zero for object files. Refer to <see cref="OptionalHeader"/> for the format description (Image Only).
    /// </summary>
    public readonly ushort SizeOfOptionalHeader;

    /// <summary>
    /// Describes the file's attributes through various flags.
    /// </summary>
    public readonly Characteristics Characteristics;
}
