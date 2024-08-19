﻿using System.Runtime.InteropServices;

namespace Wokhan.PEImage.PortableExecutable.Native;

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public unsafe readonly struct ImportLookupTableEntry : INullableDataDirectoryEntry
{
    // The structure is a bit field, so we have to parse each field according to the specification
    // Exemple with the uint 345237 with the OrdinalNameFlag set to 1 (true):
    // OrdinalNameFlag (1 bit)
    // 1010 1001 0010 0010 1010 0000 0000 0001
    // ^                                      
    // OrdinalNumber (16 bits)
    // 1010 1001 0010 0010 1010 0000 0000 0001
    //                     ^^^^ ^^^^ ^^^^ ^^^^
    // HintNameTableRVA (31 bits)
    // 1010 1001 0010 0010 1010 0000 0000 0001
    //  ^^^^ ^^^^ ^^^^ ^^^^ ^^^^ ^^^^ ^^^^ ^^^

    [FieldOffset(0)]
    private readonly uint _data32;

    [FieldOffset(0)]
    private readonly ulong _data64;

    ///// <summary>
    ///// PE32 only: If this bit is set, import by ordinal. Otherwise, import by name. Bit is masked as 0x80000000 for PE32, 0x8000000000000000 for PE32+. 
    ///// For PE32+ bits 62-31 must be zero. 
    ///// </summary>
    public readonly bool OrdinalNameFlag32 => (_data32 >> 31) != 0;

    ///// <summary>
    ///// PE32Plus only: If this bit is set, import by ordinal. Otherwise, import by name. Bit is masked as 0x80000000 for PE32, 0x8000000000000000 for PE32+. 
    ///// </summary>
    public readonly bool OrdinalNameFlag64 => (_data64 >> 63) != 0;

    ///// <summary>
    ///// A 16-bit ordinal number. This field is used only if the Ordinal/Name Flag bit field is 1 (import by ordinal). Bits 30-15 or 62-15 must be 0. 
    ///// </summary>
    public readonly ushort OrdinalNumber => (ushort)(_data32 << 16 >> 16); // Same as _data32 & 0xFFFF, probably insignificantly "faster"...

    ///// <summary>
    ///// A 31-bit RVA of a hint/name table entry. This field is used only if the Ordinal/Name Flag bit field is 0 (import by name). 
    ///// </summary>
    ///// Note: the address is stored on 31bits only: get rid of the last one by shifting right once
    public readonly nint HintNameTableRVA => (nint)(_data32 << 1 >> 1);

    public bool IsNull() => _data32 == 0;
}
