using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.reloc;

[StructLayout(LayoutKind.Explicit)]
public struct BaseRelocationBlock : INullableDataDirectoryEntry
{
    [FieldOffset(0)]
    public readonly uint PageRVA;

    [FieldOffset(4)]
    public readonly uint BlockSize;

    //TODO: shouldn't use arbitrary size here.
    [FieldOffset(8)]
    private unsafe fixed ushort _typeOffset[16];

    public unsafe IEnumerable<(BaseRelocationType RelocationType, ushort Offset)> GetBlocks(NativeMapper mapper)
    {
        List<(BaseRelocationType RelocationType, ushort Offset)> ret = new();
        var numberOfEntries = (BlockSize - sizeof(uint) - sizeof(uint)) / sizeof(ushort);
        fixed (ushort* typeOffsetPtr = _typeOffset)
        {
            for (var i = 0; i < numberOfEntries; i++)
            {
                var entry = typeOffsetPtr[i * 16];
                ret.Add(((BaseRelocationType)(entry >> 12), (ushort)(entry << 4 >> 4)));
            }
        }
        return ret;
    }
}
