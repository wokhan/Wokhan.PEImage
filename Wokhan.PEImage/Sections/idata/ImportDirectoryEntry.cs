using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.idata;

/// <summary>
/// Represents an entry in the Import Directory, which contains information about the imported functions in a PE file.
/// This struct includes details such as the import lookup table, timestamp, forwarder chain, DLL name, and import address table.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct ImportDirectoryEntry : INullableDataDirectoryEntry
{
    /// <summary>
    /// Relative Virtual Address (RVA) of the import lookup table, which contains names or ordinals for each import.
    /// </summary>
    public readonly uint ImportLookupTableRVA;

    private readonly uint _timeDateStamp;

    /// <summary>
    /// Timestamp set to zero until the image is bound. After binding, it is set to the DLL's timestamp.
    /// </summary>
    public readonly DateTime TimeDateStamp => DateTime.FromFileTime(_timeDateStamp);

    /// <summary>
    /// Index of the first forwarder reference.
    /// </summary>
    public readonly uint ForwarderChain;

    /// <summary>
    /// Relative address of an ASCII string containing the DLL name.
    /// </summary>
    public readonly uint NameRVA;

    /// <summary>
    /// RVA of the import address table, which mirrors the import lookup table until the image is bound.
    /// </summary>
    public readonly uint ImportAddressTableRVA;

    /// <summary>
    /// Retrieves the imported functions from the import directory.
    /// </summary>
    /// <param name="mapper">The native memory mapper.</param>
    /// <param name="moduleBaseAddress">The base address of the module in memory.</param>
    /// <param name="is64bits">Indicates if the image is 64-bit.</param>
    /// <returns>A collection of tuples containing DLL name, method name, hint, and address.</returns>
    public readonly IEnumerable<(string DllName, string MethodName, ushort Hint, nint Address)> GetImportedFunctions(NativeMapper mapper, nint moduleBaseAddress, bool is64bits)
    {
        var dllName = mapper.ReadStringANSI((nint)NameRVA);

        var addressEntries = mapper.MapArray<ImportAddressTableEntry>((nint)ImportAddressTableRVA, entry => entry.SymbolAddress32 == 0);

        var entries = mapper.MapArray<ImportLookupTableEntry>((nint)ImportLookupTableRVA, entry => entry.IsNull());

        for (var i = 0; i < entries.Length; i++)
        {
            var entry = entries[i];

            var hintNameEntry = mapper.Map<ImportHintNameTableEntry>(entry.HintNameTableRVA);

            string methodName = is64bits && entry.OrdinalNameFlag64 || entry.OrdinalNameFlag32 ? string.Empty : hintNameEntry.Name;

            var address = is64bits ? addressEntries[i].SymbolAddress64 : addressEntries[i].SymbolAddress32;

            yield return (dllName, methodName, hintNameEntry.Hint, (nint)address);
        }
    }

    /// <summary>
    /// Retrieves the name of the import directory entry.
    /// </summary>
    /// <param name="reader">The native memory reader.</param>
    /// <returns>The name of the import directory entry as a string.</returns>
    internal unsafe readonly string GetName(NativeMapper reader)
    {
        return reader.ReadStringANSI((nint)NameRVA);
    }

    /// <summary>
    /// Checks if the import directory entry is null.
    /// </summary>
    /// <returns>True if the import lookup table RVA is 0, otherwise false.</returns>
    public readonly bool IsNull() => ImportLookupTableRVA == 0;
}
