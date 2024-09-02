using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.idata;

/// <summary>
/// Represents an entry in the Import Address Table (IAT).
/// This struct can hold either a 32-bit or a 64-bit symbol address, depending on the target architecture.
/// </summary>
[StructLayout(LayoutKind.Explicit, Pack = 1)]
public readonly struct ImportAddressTableEntry
{
    /// <summary>
    /// The 32-bit symbol address.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint SymbolAddress32;

    /// <summary>
    /// The 64-bit symbol address.
    /// </summary>
    [FieldOffset(0)]
    public readonly ulong SymbolAddress64;
}
