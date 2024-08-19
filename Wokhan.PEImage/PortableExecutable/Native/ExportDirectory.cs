using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.PortableExecutable.Native;

[StructLayout(LayoutKind.Sequential)]
public readonly struct ExportDirectory : INullableDataDirectoryEntry
{
    public readonly uint ExportFlags;
    public readonly uint TimeDateStamp;
    public readonly ushort MajorVersion;
    public readonly ushort MinorVersion;
    public readonly uint NameRVA;
    public readonly uint OrdinalBase;
    public readonly uint AddressTableEntries;
    public readonly uint NumberOfNamesPointers;
    public readonly uint ExportAddressTableRVA;
    public readonly uint NamePointerRVA;
    public readonly uint OrdinalTableRVA;

    public bool IsNull() => false;

    internal readonly IEnumerable<(string DllName, string MethodName, nint RVA, nint Address)> GetExportedFunctions(NativeMapper reader, nint moduleBaseAddress)
    {
        var dllName = reader.ReadStringANSI((nint)NameRVA, 256);

        var namesPointers = reader.MapArray<uint>((nint)NamePointerRVA, NumberOfNamesPointers);
        var ordinals = reader.MapArray<ushort>((nint)OrdinalTableRVA, NumberOfNamesPointers);
        var addresses = reader.MapArray<uint>((nint)ExportAddressTableRVA, AddressTableEntries);

        var localOrdinalBase = OrdinalBase;

        //TODO: check if +1 is necessary
        return namesPointers.Zip(ordinals.Select(ordinal => addresses[ordinal - localOrdinalBase + 1]), 
                                 (nameAddress, functionAddress) => (dllName, reader.ReadStringANSI((nint)nameAddress), (nint)functionAddress, moduleBaseAddress + (nint)functionAddress));
    }

    internal unsafe readonly string GetName(NativeMapper reader)
    {
        return reader.ReadStringANSI((nint)NameRVA);
    }
}
