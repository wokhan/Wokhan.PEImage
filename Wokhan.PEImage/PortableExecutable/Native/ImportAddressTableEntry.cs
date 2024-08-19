using System.Runtime.InteropServices;

namespace Wokhan.PEImage.PortableExecutable.Native;

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public readonly struct ImportAddressTableEntry
{
    [FieldOffset(0)]
    public readonly uint SymbolAddress32;

    [FieldOffset(0)]
    public readonly ulong SymbolAddress64;
}
