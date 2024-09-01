using System.Runtime.InteropServices;

using Wokhan.PEImage.Sections;
using Wokhan.PEImage.Sections.edata;
using Wokhan.PEImage.Sections.idata;
using Wokhan.PEImage.Sections.pdata;
using Wokhan.PEImage.Sections.reloc;
using Wokhan.PEImage.Sections.rsrc;

namespace Wokhan.PEImage.Headers.Directories;

[StructLayout(LayoutKind.Sequential)]
public readonly struct DataDirectories
{
    /// <summary>
    /// The export table address and size. For more information see .edata Section (Image Only).
    /// </summary>
    [PESection(".edata")]
    public readonly DataDirectory<ExportDirectory> ExportTable;

    /// <summary>
    /// The import table address and size. For more information, see The .idata Section.
    /// </summary>
    [PESection(".idata")]
    public readonly DataDirectory<ImportDirectoryEntry> ImportTable;

    /// <summary>
    /// The resource table address and size. For more information, see The .rsrc Section.
    /// </summary>
    [PESection(".rsrc")]
    public readonly DataDirectory<ResourceDirectoryTable> ResourceTable;

    /// <summary>
    /// The exception table address and size. For more information, see The .pdata Section.
    /// </summary>
    [PESection(".pdata")]
    public readonly DataDirectory<ExceptionsFunctionTableEntry> ExceptionTable;

    /// <summary>
    /// The attribute certificate table address and size. For more information, see The Attribute Certificate Table (Image Only).
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> CertificateTable;

    /// <summary>
    /// The base relocation table address and size. For more information, see The .reloc Section (Image Only).
    /// </summary>
    [PESection(".reloc")]
    public readonly DataDirectory<BaseRelocationBlock> BaseRelocationTable;

    /// <summary>
    /// The debug data starting address and size. For more information, see The .debug Section.
    /// </summary>
    // TODO: implement
    [PESection(".debug")]
    public readonly DataDirectory<ReservedEmptyDirectory> Debug;

    /// <summary>
    /// Reserved, must be 0
    /// </summary>
    public readonly DataDirectory<ReservedEmptyDirectory> Architecture;

    /// <summary>
    /// The RVA of the value to be stored in the global pointer register. The size member of this structure must be set to zero.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> GlobalPtr;

    /// <summary>
    /// The thread local storage (TLS) table address and size. For more information, see The .tls Section.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> TLSTable;

    /// <summary>
    /// The load configuration table address and size. For more information, see The Load Configuration Structure (Image Only).
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> LoadConfigTable;

    /// <summary>
    /// The bound import table address and size.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> BoundImport;

    /// <summary>
    /// The import address table address and size. For more information, see Import Address Table.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> IAT;

    /// <summary>
    /// The delay import descriptor address and size. For more information, see Delay-Load Import Tables (Image Only).
    /// </summary>
    public readonly DataDirectory<DelayLoadDirectoryEntry> DelayImportDescriptor;

    /// <summary>
    /// The CLR runtime header address and size. For more information, see The .cormeta Section (Object Only).
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> CLRRuntimeHeader;

    /// <summary>
    /// Reserved, must be zero
    /// </summary>
    public readonly DataDirectory<ReservedEmptyDirectory> ReservedMustBeZero;

}
