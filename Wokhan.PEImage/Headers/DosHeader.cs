using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Headers;

/// <summary>
/// Represents the DOS Header of a Portable Executable (PE) file.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct DosHeader
{
    /// <summary>
    /// Magic number that identifies the file as a DOS executable.
    /// Commonly set to 0x5A4D ('MZ'). (Original: e_magic)
    /// </summary>
    public readonly ushort MagicNumber;

    /// <summary>
    /// Number of bytes on the last page of the file.
    /// Indicates how much of the last page is used. (Original: e_cblp)
    /// </summary>
    public readonly ushort LastPageBytes;

    /// <summary>
    /// Total number of pages in the file.
    /// Helps in calculating the file size. (Original: e_cp)
    /// </summary>
    public readonly ushort TotalPages;

    /// <summary>
    /// Number of relocation entries.
    /// Used for adjusting addresses when the program is loaded. (Original: e_crlc)
    /// </summary>
    public readonly ushort RelocationEntries;

    /// <summary>
    /// Size of the header in 16-byte paragraphs.
    /// Defines the length of the header. (Original: e_cparhdr)
    /// </summary>
    public readonly ushort HeaderSizeInParagraphs;

    /// <summary>
    /// Minimum number of paragraphs required above the program.
    /// Ensures the program has enough memory. (Original: e_minalloc)
    /// </summary>
    public readonly ushort MinExtraParagraphs;

    /// <summary>
    /// Maximum number of paragraphs the program can allocate.
    /// Limits the memory usage of the program. (Original: e_maxalloc)
    /// </summary>
    public readonly ushort MaxExtraParagraphs;

    /// <summary>
    /// Initial value of the SS (Stack Segment) register.
    /// Sets up the stack segment. (Original: e_ss)
    /// </summary>
    public readonly ushort InitialSS;

    /// <summary>
    /// Initial value of the SP (Stack Pointer) register.
    /// Points to the top of the stack. (Original: e_sp)
    /// </summary>
    public readonly ushort InitialSP;

    /// <summary>
    /// Checksum of the executable.
    /// Used to verify the integrity of the file. (Original: e_csum)
    /// </summary>
    public readonly ushort Checksum;

    /// <summary>
    /// Initial value of the IP (Instruction Pointer) register.
    /// Points to the entry point of the program. (Original: e_ip)
    /// </summary>
    public readonly ushort InitialIP;

    /// <summary>
    /// Initial value of the CS (Code Segment) register.
    /// Sets up the code segment. (Original: e_cs)
    /// </summary>
    public readonly ushort InitialCS;

    /// <summary>
    /// File address of the relocation table.
    /// Points to the table used for address adjustments. (Original: e_lfarlc)
    /// </summary>
    public readonly ushort RelocationTableAddress;

    /// <summary>
    /// Overlay number.
    /// Used for managing multiple overlays. (Original: e_ovno)
    /// </summary>
    public readonly ushort OverlayNumber;

    /// <summary>
    /// Reserved words, should be set to zero. (Original: e_res1)
    /// </summary>
    public fixed ushort Reserved1[4];

    /// <summary>
    /// OEM identifier: specifies the OEM that created the file. (Original: e_oemid)
    /// </summary>
    public readonly ushort OemId;

    /// <summary>
    /// Additional data defined by the OEM. (Original: e_oeminfo)
    /// </summary>
    public readonly ushort OemInfo;

    /// <summary>
    /// Reserved words, should be set to zero. (Original: e_res2)
    /// </summary>
    public fixed ushort Reserved2[10];

    /// <summary>
    /// Points to the PE header, used by Windows to load the executable. (Original: e_lfanew)
    /// </summary>
    public readonly uint PEHeaderAddress;
}
