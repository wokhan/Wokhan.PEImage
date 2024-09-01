using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.edata;

[StructLayout(LayoutKind.Sequential)]
public readonly struct ExportDirectory : INullableDataDirectoryEntry
{
    /// <summary>
    /// Reserved, must be 0. 
    /// </summary>
    public readonly uint ExportFlags;

    /// <summary>
    /// The time and date that the export data was created. 
    /// </summary>
    public readonly uint TimeDateStamp;

    /// <summary>
    /// The major version number. The major and minor version numbers can be set by the user. 
    /// </summary>
    public readonly ushort MajorVersion;

    /// <summary>
    /// The minor version number. 
    /// </summary>
    public readonly ushort MinorVersion;

    /// <summary>
    /// The address of the ASCII string that contains the name of the DLL. This address is relative to the image base. 
    /// </summary>
    public readonly uint NameRVA;

    /// <summary>
    /// The starting ordinal number for exports in this image. This field specifies the starting ordinal number for the export address table. It is usually set to 1. 
    /// </summary>
    public readonly uint OrdinalBase;

    /// <summary>
    /// The number of entries in the export address table. 
    /// </summary>
    public readonly uint AddressTableEntries;

    /// <summary>
    /// The number of entries in the name pointer table. This is also the number of entries in the ordinal table. 
    /// </summary>
    public readonly uint NumberOfNamesPointers;

    /// <summary>
    /// The address of the export address table, relative to the image base. 
    /// </summary>
    public readonly uint ExportAddressTableRVA;

    /// <summary>
    /// The address of the export name pointer table, relative to the image base. The table size is given by the Number of Name Pointers field. 
    /// </summary>
    public readonly uint NamePointerRVA;

    /// <summary>
    /// The address of the ordinal table, relative to the image base. 
    /// </summary>
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
