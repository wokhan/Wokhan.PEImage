using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

using Wokhan.PEImage.Headers.Directories;

namespace Wokhan.PEImage.Headers;


[StructLayout(LayoutKind.Explicit, Pack = 1)]
public readonly unsafe struct OptionalHeader
{
    /// <summary>
    /// Identifies the state of the image file with an unsigned integer. Common values are 0x10B for a normal executable, 0x107 for a ROM image, and 0x20B for a PE32+ executable.
    /// </summary>
    [FieldOffset(0)]
    public readonly PEMagic ImageMagic;

    /// <summary>
    /// Major version number of the linker.
    /// </summary>
    [FieldOffset(2)]
    public readonly sbyte MajorLinkerVersion;

    /// <summary>
    /// Minor version number of the linker.
    /// </summary>
    [FieldOffset(3)]
    public readonly sbyte MinorLinkerVersion;

    /// <summary>
    /// Size of the code (text) section, or the total size of all code sections if there are multiple.
    /// </summary>
    [FieldOffset(4)]
    public readonly uint SizeOfCode;

    /// <summary>
    /// Size of the initialized data section, or the total size of all such sections if there are multiple.
    /// </summary>
    [FieldOffset(8)]
    public readonly uint SizeOfInitializedData;

    /// <summary>
    /// Size of the uninitialized data section (BSS), or the total size of all such sections if there are multiple.
    /// </summary>
    [FieldOffset(12)]
    public readonly uint SizeOfUninitializedData;

    /// <summary>
    /// Entry point address relative to the image base when loaded into memory. For programs, this is the starting address. For drivers, this is the initialization function address. Optional for DLLs; if absent, this field is zero.
    /// </summary>
    [FieldOffset(16)]
    public readonly uint AddressOfEntryPoint;

    /// <summary>
    /// Address relative to the image base of the beginning of the code section when loaded into memory.
    /// </summary>
    [FieldOffset(20)]
    public readonly uint BaseOfCode;

    /// <summary>
    /// PE32 Only: Address relative to the image base of the beginning of the data section when loaded into memory.
    /// </summary>
    [FieldOffset(24)]
    private readonly uint _baseOfData32;

    public readonly uint BaseOfData => ImageMagic == PEMagic.PE32 ? _baseOfData32 : 0;

    #region Windows specific

    [FieldOffset(24)]
    private readonly ulong _imageBase64;

    [FieldOffset(28)]
    private readonly uint _imageBase32;

    /// <summary>
    /// Preferred address of the first byte of the image when loaded into memory; must be a multiple of 64 K. Defaults: 0x10000000 for DLLs, 0x00010000 for Windows CE EXEs, and 0x00400000 for other Windows versions.
    /// </summary>
    public readonly ulong ImageBase => ImageMagic == PEMagic.PE32 ? _imageBase32 : _imageBase64;

    /// <summary>
    /// Alignment (in bytes) of sections when loaded into memory. Must be greater than or equal to FileAlignment. Default is the architecture's page size.
    /// </summary>
    [FieldOffset(32)]
    public readonly uint SectionAlignment;

    /// <summary>
    /// Alignment factor (in bytes) for raw data of sections in the image file. Should be a power of 2 between 512 and 64 K, inclusive. Default is 512. If SectionAlignment is less than the architecture's page size, FileAlignment must match SectionAlignment.
    /// </summary>
    [FieldOffset(36)]
    public readonly ulong FileAlignment;

    /// <summary>
    /// Major version number of the required operating system.
    /// </summary>
    [FieldOffset(40)]
    public readonly ushort MajorOperatingSystemVersion;

    /// <summary>
    /// Minor version number of the required operating system.
    /// </summary>
    [FieldOffset(42)]
    public readonly ushort MinorOperatingSystemVersion;

    /// <summary>
    /// Major version number of the image.
    /// </summary>
    [FieldOffset(44)]
    public readonly ushort MajorImageVersion;

    /// <summary>
    /// Minor version number of the image.
    /// </summary>
    [FieldOffset(46)]
    public readonly ushort MinorImageVersion;

    /// <summary>
    /// Major version number of the subsystem.
    /// </summary>
    [FieldOffset(48)]
    public readonly ushort MajorSubsystemVersion;

    /// <summary>
    /// PE32: Minor version number of the subsystem.
    /// </summary>
    [FieldOffset(50)]
    public readonly ushort MinorSubsystemVersion;

    /// <summary>
    /// Reserved field, must be zero.
    /// </summary>
    [FieldOffset(52)]
    public readonly ulong Win32VersionValue;

    /// <summary>
    /// Size (in bytes) of the image, including all headers, as loaded in memory. Must be a multiple of SectionAlignment.
    /// </summary>
    [FieldOffset(56)]
    public readonly uint SizeOfImage;

    /// <summary>
    /// Combined size of MS-DOS stub, PE header, and section headers, rounded up to a multiple of FileAlignment.
    /// </summary>
    [FieldOffset(60)]
    public readonly uint SizeOfHeaders;

    /// <summary>
    /// Image file checksum. Computed using an algorithm in IMAGHELP.DLL. Validated at load time for all drivers, any DLL loaded at boot time, and any DLL loaded into a critical Windows process.
    /// </summary>
    [FieldOffset(64)]
    public readonly ulong CheckSum;

    /// <summary>
    /// Subsystem required to run this image. See Windows Subsystem for more details.
    /// </summary>
    [FieldOffset(68)]
    public readonly Subsystem Subsystem;

    /// <summary>
    /// See DLL Characteristics for more details.
    /// </summary>
    [FieldOffset(70)]
    public readonly DllCharacteristics DllCharacteristics;

    /// <summary>
    /// Size of the stack to reserve. Only SizeOfStackCommit is committed; the rest is made available one page at a time until the reserve size is reached.
    /// </summary>
    [FieldOffset(72)]
    public readonly uint SizeOfStackReserve;

    [FieldOffset(76)]
    private readonly uint _sizeOfStackCommit32;

    [FieldOffset(80)]
    private readonly ulong _sizeOfStackCommit64;

    /// <summary>
    /// Size of the stack to commit.
    /// </summary>
    public readonly ulong SizeOfStackCommit => ImageMagic == PEMagic.PE32 ? _sizeOfStackCommit32 : _sizeOfStackCommit64;

    [FieldOffset(80)]
    private readonly uint _sizeOfHeapReserve32;

    [FieldOffset(88)]
    private readonly ulong _sizeOfHeapReserve64;

    /// <summary>
    /// Size of the local heap space to reserve. Only SizeOfHeapCommit is committed; the rest is made available one page at a time until the reserve size is reached.
    /// </summary>
    public readonly ulong SizeOfHeapReserve => ImageMagic == PEMagic.PE32 ? _sizeOfHeapCommit32 : _sizeOfHeapCommit64;

    [FieldOffset(84)]
    private readonly uint _sizeOfHeapCommit32;

    [FieldOffset(96)]
    private readonly ulong _sizeOfHeapCommit64;

    /// <summary>
    /// Size of the local heap space to commit.
    /// </summary>
    public readonly ulong SizeOfHeapCommit => ImageMagic == PEMagic.PE32 ? _sizeOfHeapCommit32 : _sizeOfHeapCommit64;

    [FieldOffset(88)]
    private readonly uint _loaderFlags32;

    [FieldOffset(104)]
    private readonly uint _loaderFlags64;

    /// <summary>
    /// Reserved field, must be zero.
    /// </summary>
    public readonly uint LoaderFlags => ImageMagic == PEMagic.PE32 ? _loaderFlags32 : _loaderFlags64;

    [FieldOffset(92)]
    private readonly uint _numberOfRvaAndSizes32;

    [FieldOffset(108)]
    private readonly uint _numberOfRvaAndSizes64;

    /// <summary>
    /// PE32: Number of data-directory entries in the remainder of the optional header. Each entry describes a location and size.
    /// </summary>
    public readonly uint NumberOfRvaAndSizes => ImageMagic == PEMagic.PE32 ? _numberOfRvaAndSizes32 : _numberOfRvaAndSizes64;

    #endregion

    [FieldOffset(96)]
    private readonly DataDirectories _dataDirectories32;

    [FieldOffset(112)]
    private readonly DataDirectories _dataDirectories64;

    public readonly DataDirectories DataDirectories => ImageMagic == PEMagic.PE32 ? _dataDirectories32 : _dataDirectories64;
}
