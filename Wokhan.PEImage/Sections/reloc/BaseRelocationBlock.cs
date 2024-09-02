using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.reloc;

/// <summary>
/// Represents a block of base relocations in the PE file.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct BaseRelocationBlock : INullableDataDirectoryEntry
{
    /// <summary>
    /// The relative virtual address (RVA) of the page.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint PageRVA;

    /// <summary>
    /// The size of the block, in bytes.
    /// </summary>
    [FieldOffset(4)]
    public readonly uint BlockSize;

    /// <summary>
    /// An array of type-offset entries. Important: The size is arbitrarily set to 16 and should be adjusted as needed.
    /// </summary>
    [FieldOffset(8)]
    private unsafe fixed ushort _typeOffset[16];

    /// <summary>
    /// Retrieves the relocation entries within the block.
    /// </summary>
    /// <param name="mapper">The native mapper used to interpret the entries.</param>
    /// <returns>A collection of tuples containing the relocation type and offset.</returns>
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
