using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.PortableExecutable.Native;

[StructLayout(LayoutKind.Sequential)]
public readonly struct DataDirectories
{
    /// <summary>
    /// The export table address and size. For more information see .edata Section (Image Only).
    /// </summary>
    public readonly DataDirectory<ExportDirectory> ExportTable;

    /// <summary>
    /// The import table address and size. For more information, see The .idata Section.
    /// </summary>
    public readonly DataDirectory<ImportDirectory> ImportTable;

    /// <summary>
    /// The resource table address and size. For more information, see The .rsrc Section.
    /// </summary>
    public readonly DataDirectory<ExportDirectory> ResourceTable;

    /// <summary>
    /// The exception table address and size. For more information, see The .pdata Section.
    /// </summary>
    public readonly DataDirectory<ExportDirectory> ExceptionTable;

    /// <summary>
    /// The attribute certificate table address and size. For more information, see The Attribute Certificate Table (Image Only).
    /// </summary>
    public readonly DataDirectory<ExportDirectory> CertificateTable;

    /// <summary>
    /// The base relocation table address and size. For more information, see The .reloc Section (Image Only).
    /// </summary>
    public readonly DataDirectory<ExportDirectory> BaseRelocationTable;

    /// <summary>
    /// The debug data starting address and size. For more information, see The .debug Section.
    /// </summary>
    public readonly DataDirectory<ExportDirectory> Debug;

    /// <summary>
    /// Reserved, must be 0
    /// </summary>
    public readonly DataDirectory<ExportDirectory> Architecture;

    /// <summary>
    /// The RVA of the value to be stored in the global pointer register. The size member of this structure must be set to zero.
    /// </summary>
    public readonly DataDirectory<ExportDirectory> GlobalPtr;

    /// <summary>
    /// The thread local storage (TLS) table address and size. For more information, see The .tls Section.
    /// </summary>
    public readonly DataDirectory<ExportDirectory> TLSTable;

    /// <summary>
    /// The load configuration table address and size. For more information, see The Load Configuration Structure (Image Only).
    /// </summary>
    public readonly DataDirectory<ExportDirectory> LoadConfigTable;

    /// <summary>
    /// The bound import table address and size.
    /// </summary>
    public readonly DataDirectory<ExportDirectory> BoundImport;

    /// <summary>
    /// The import address table address and size. For more information, see Import Address Table.
    /// </summary>
    public readonly DataDirectory<ExportDirectory> IAT;

    /// <summary>
    /// The delay import descriptor address and size. For more information, see Delay-Load Import Tables (Image Only).
    /// </summary>
    public readonly DataDirectory<ExportDirectory> DelayImportDescriptor;

    /// <summary>
    /// The CLR runtime header address and size. For more information, see The .cormeta Section (Object Only).
    /// </summary>
    public readonly DataDirectory<ExportDirectory> CLRRuntimeHeader;

    /// <summary>
    /// Reserved, must be zero
    /// </summary>
    public readonly DataDirectory<ExportDirectory> ReservedMustBeZero;

}
