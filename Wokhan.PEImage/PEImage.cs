using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReflectionPE = System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using Wokhan.PEImage.Headers;
using Wokhan.PEImage.Sections;
using System.Runtime.Versioning;

namespace Wokhan.PEImage;
public class PEImage
{
    private readonly NativeMapper mapper;

    private readonly nint moduleBaseAddress;

    public DosHeader DosHeaders { get; private set; }
    public PEHeader NTHeaders { get; private set; }
    public SectionTableEntry[] Sections { get; private set; }

    public IList<(string DllName, string MethodName, nint RVA, nint Address)> Exports { get; private set; }
    public IList<(string DllName, string MethodName, ushort Hint, nint Address)> Imports { get; private set; }

    private bool is64bits;

    private PEImage(SafeHandle? processHandle, nint moduleBaseAddress)
    {
        this.moduleBaseAddress = moduleBaseAddress;

        mapper = new NativeMapper(processHandle, moduleBaseAddress);

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
        DosHeaders = mapper.Map<DosHeader>(0);
        NTHeaders = mapper.Map<PEHeader>((nint)DosHeaders.e_lfanew);
        Sections = mapper.MapArray<SectionTableEntry>((nint)DosHeaders.e_lfanew + sizeof(PEHeader), NTHeaders.FileHeader.NumberOfSections);

        if (!NTHeaders.IsValid())
        {
            throw new IOException("The provided image is not a PE image and cannot be parsed.");
        }

        var exportDirectory = NTHeaders.OptionalHeader.DataDirectories.ExportTable.GetSingle(mapper);
        var importDirectoryEntries = NTHeaders.OptionalHeader.DataDirectories.ImportTable.GetAllEntries(mapper);
        var resourceDirectory = NTHeaders.OptionalHeader.DataDirectories.ResourceTable.GetSingle(mapper);

        Exports = exportDirectory.GetExportedFunctions(mapper, moduleBaseAddress).ToList();

        is64bits = NTHeaders.OptionalHeader.ImageMagic == ReflectionPE.PEMagic.PE32Plus;
        Imports = importDirectoryEntries.SelectMany(dir => dir.GetImportedFunctions(mapper, moduleBaseAddress, is64bits)).ToList();
    }
}
