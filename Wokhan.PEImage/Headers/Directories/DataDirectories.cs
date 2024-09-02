using Wokhan.PEImage.Sections;
using Wokhan.PEImage.Sections.edata;
using Wokhan.PEImage.Sections.idata;
using Wokhan.PEImage.Sections.pdata;
using Wokhan.PEImage.Sections.reloc;
using Wokhan.PEImage.Sections.rsrc;

namespace Wokhan.PEImage.Headers.Directories;

public readonly struct DataDirectories
{
    /// <summary>
    /// Address and size of the export table. See .edata Section (Image Only) for more details.
    /// </summary>
    [PESection(".edata")]
    public readonly DataDirectory<ExportDirectory> ExportTable;

    /// <summary>
    /// Address and size of the import table. See The .idata Section for more details.
    /// </summary>
    [PESection(".idata")]
    public readonly DataDirectory<ImportDirectoryEntry> ImportTable;

    /// <summary>
    /// Address and size of the resource table. See The .rsrc Section for more details.
    /// </summary>
    [PESection(".rsrc")]
    public readonly DataDirectory<ResourceDirectoryTable> ResourceTable;

    /// <summary>
    /// Address and size of the exception table. See The .pdata Section for more details.
    /// </summary>
    [PESection(".pdata")]
    public readonly DataDirectory<ExceptionsFunctionTableEntry> ExceptionTable;

    /// <summary>
    /// Address and size of the attribute certificate table. See The Attribute Certificate Table (Image Only) for more details.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> CertificateTable;

    /// <summary>
    /// Address and size of the base relocation table. See The .reloc Section (Image Only) for more details.
    /// </summary>
    [PESection(".reloc")]
    public readonly DataDirectory<BaseRelocationBlock> BaseRelocationTable;

    /// <summary>
    /// Starting address and size of the debug data. See The .debug Section for more details.
    /// </summary>
    // TODO: implement
    [PESection(".debug")]
    public readonly DataDirectory<ReservedEmptyDirectory> Debug;

    /// <summary>
    /// Reserved field, must be zero.
    /// </summary>
    public readonly DataDirectory<ReservedEmptyDirectory> Architecture;

    /// <summary>
    /// RVA of the value to be stored in the global pointer register. The size member must be zero.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> GlobalPtr;

    /// <summary>
    /// Address and size of the thread local storage (TLS) table. See The .tls Section for more details.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> TLSTable;

    /// <summary>
    /// Address and size of the load configuration table. See The Load Configuration Structure (Image Only) for more details.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> LoadConfigTable;

    /// <summary>
    /// Address and size of the bound import table.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> BoundImport;

    /// <summary>
    /// Address and size of the import address table. See Import Address Table for more details.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> IAT;

    /// <summary>
    /// Address and size of the delay import descriptor. See Delay-Load Import Tables (Image Only) for more details.
    /// </summary>
    public readonly DataDirectory<DelayLoadDirectoryEntry> DelayImportDescriptor;

    /// <summary>
    /// Address and size of the CLR runtime header. See The .cormeta Section (Object Only) for more details.
    /// </summary>
    // TODO: implement
    public readonly DataDirectory<ReservedEmptyDirectory> CLRRuntimeHeader;

    /// <summary>
    /// Reserved field, must be zero.
    /// </summary>
    public readonly DataDirectory<ReservedEmptyDirectory> ReservedMustBeZero;
}
