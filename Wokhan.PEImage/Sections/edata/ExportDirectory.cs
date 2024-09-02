using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.edata;

/// <summary> 
/// Represents the Export Directory, which contains information about the functions and data exported by a DLL. 
/// This includes details such as the DLL name, function names, and their addresses. 
///     </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ExportDirectory : INullableDataDirectoryEntry
{
    /// <summary>
    /// Reserved field, must be set to 0.
    /// </summary>
    public readonly uint ExportFlags;

    /// <summary>
    /// Timestamp indicating when the export data was generated.
    /// </summary>
    public readonly uint TimeDateStamp;

    /// <summary>
    /// Major version number, user-defined.
    /// </summary>
    public readonly ushort MajorVersion;

    /// <summary>
    /// Minor version number, user-defined.
    /// </summary>
    public readonly ushort MinorVersion;

    /// <summary>
    /// Relative address of the ASCII string containing the DLL name.
    /// </summary>
    public readonly uint NameRVA;

    /// <summary>
    /// Starting ordinal number for exports, typically set to 1.
    /// </summary>
    public readonly uint OrdinalBase;

    /// <summary>
    /// Number of entries in the export address table.
    /// </summary>
    public readonly uint AddressTableEntries;

    /// <summary>
    /// Number of entries in the name pointer table, which matches the number of entries in the ordinal table.
    /// </summary>
    public readonly uint NumberOfNamesPointers;

    /// <summary>
    /// Relative address of the export address table.
    /// </summary>
    public readonly uint ExportAddressTableRVA;

    /// <summary>
    /// Relative address of the export name pointer table, with size specified by NumberOfNamesPointers.
    /// </summary>
    public readonly uint NamePointerRVA;

    /// <summary>
    /// Relative address of the ordinal table.
    /// </summary>
    public readonly uint OrdinalTableRVA;

    /// <summary>
    /// Checks if the directory entry is null.
    /// </summary>
    /// <returns>False, indicating the entry is not null.</returns>
    public bool IsNull() => false;

    /// <summary>
    /// Retrieves the exported functions from the export directory.
    /// </summary>
    /// <param name="reader">The native memory reader.</param>
    /// <param name="moduleBaseAddress">The base address of the module in memory.</param>
    /// <returns>A collection of tuples containing DLL name, method name, relative virtual address (RVA), and absolute address.</returns>
    internal readonly IEnumerable<(string DllName, string MethodName, nint RVA, nint Address)> GetExportedFunctions(NativeMapper reader, nint moduleBaseAddress)
    {
        var dllName = reader.ReadStringANSI((nint)NameRVA, 256);

        var namesPointers = reader.MapArray<uint>((nint)NamePointerRVA, NumberOfNamesPointers);
        var ordinals = reader.MapArray<ushort>((nint)OrdinalTableRVA, NumberOfNamesPointers);
        var addresses = reader.MapArray<uint>((nint)ExportAddressTableRVA, AddressTableEntries);

        var localOrdinalBase = OrdinalBase;

        //TODO: verify if +1 adjustment is necessary
        return namesPointers.Zip(ordinals.Select(ordinal => addresses[ordinal - localOrdinalBase + 1]),
                                 (nameAddress, functionAddress) => (dllName, reader.ReadStringANSI((nint)nameAddress), (nint)functionAddress, moduleBaseAddress + (nint)functionAddress));
    }

    /// <summary>
    /// Retrieves the name of the export directory.
    /// </summary>
    /// <param name="reader">The native memory reader.</param>
    /// <returns>The name of the export directory as a string.</returns>
    internal unsafe readonly string GetName(NativeMapper reader)
    {
        return reader.ReadStringANSI((nint)NameRVA);
    }
}
