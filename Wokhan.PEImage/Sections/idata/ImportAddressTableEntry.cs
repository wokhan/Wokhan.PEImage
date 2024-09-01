using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.idata;

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public readonly struct ImportAddressTableEntry
{
    [FieldOffset(0)]
    public readonly uint SymbolAddress32;

    [FieldOffset(0)]
    public readonly ulong SymbolAddress64;
}
