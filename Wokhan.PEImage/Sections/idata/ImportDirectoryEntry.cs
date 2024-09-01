using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.idata;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct ImportDirectoryEntry : INullableDataDirectoryEntry
{
    /// <summary>
    /// The RVA of the import lookup table. This table contains a name or ordinal for each import. (The name "Characteristics" is used in Winnt.h, but no longer describes this field.) 
    /// </summary>
    public readonly uint ImportLookupTableRVA;

    private readonly uint _timeDateStamp;

    /// <summary>
    /// The stamp that is set to zero until the image is bound. After the image is bound, this field is set to the time/data stamp of the DLL. 
    /// </summary>
    public readonly DateTime TimeDateStamp => DateTime.FromFileTime(_timeDateStamp);

    /// <summary>
    /// The index of the first forwarder reference. 
    /// </summary>
    public readonly uint ForwarderChain;

    /// <summary>
    /// The address of an ASCII string that contains the name of the DLL. This address is relative to the image base. 
    /// </summary>
    public readonly uint NameRVA;

    /// <summary>
    /// The RVA of the import address table. The contents of this table are identical to the contents of the import lookup table until the image is bound. 
    /// </summary>
    public readonly uint ImportAddressTableRVA;

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

    internal unsafe readonly string GetName(NativeMapper reader)
    {
        return reader.ReadStringANSI((nint)NameRVA);
    }

    public readonly bool IsNull() => ImportLookupTableRVA == 0;
}
