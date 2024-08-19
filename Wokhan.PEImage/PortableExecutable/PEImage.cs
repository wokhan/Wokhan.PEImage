using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using Wokhan.PEImage.PortableExecutable.Native;

namespace Wokhan.PEImage.PortableExecutable;
public class PEImage
{
    private readonly NativeMapper reader;

    private readonly nint moduleBaseAddress;

    public DosHeader dosHeaders;
    public NtHeaders ntHeaders;

    public IList<(string DllName, string MethodName, nint RVA, nint Address)> Exports;
    public IList<(string DllName, string MethodName, ushort Hint, nint Address)> Imports;

    private bool is64bits;

    private PEImage(SafeHandle? processHandle, nint moduleBaseAddress)
    {
        this.moduleBaseAddress = moduleBaseAddress;

        reader = new NativeMapper(processHandle, moduleBaseAddress);

        Init();
    }
    public static PEImage CreateForProcessModule(nint moduleBaseAddress)
    {
        return new PEImage(null, moduleBaseAddress);
    }

    public static PEImage CreateForProcessModule(SafeHandle? processHandle, nint moduleBaseAddress)
    {
        return new PEImage(processHandle, moduleBaseAddress);
    }

    private unsafe void Init()
    {
        dosHeaders = reader.Map<DosHeader>(0);
        ntHeaders = reader.Map<NtHeaders>((nint)dosHeaders.e_lfanew);

        if (!ntHeaders.IsValid())
        {
            throw new IOException("The provided image is not a PE image and cannot be parsed.");
        }

        is64bits = ntHeaders.OptionalHeader.ImageMagic == PEMagic.PE32Plus;

        var exportDirectory = ntHeaders.OptionalHeader.DataDirectories.ExportTable.Get(reader);

        var edName = exportDirectory.GetName(reader);

        var importDirectory = ntHeaders.OptionalHeader.DataDirectories.ImportTable.GetEntries(reader);

        Exports = exportDirectory.GetExportedFunctions(reader, moduleBaseAddress).ToList();
        Imports = importDirectory.SelectMany(dir => dir.GetImportedFunctions(reader, moduleBaseAddress, is64bits)).ToList();
    }
}
