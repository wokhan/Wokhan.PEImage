using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using Wokhan.PEImage.Headers;
using Wokhan.PEImage.Sections;

using ReflectionPE = System.Reflection.PortableExecutable;

namespace Wokhan.PEImage
{
    /// <summary>
    /// Represents a Portable Executable (PE) image.
    /// </summary>
    public class PEImage
    {
        /// <summary>
        /// Maps native memory to managed structures.
        /// </summary>
        private readonly NativeMapper mapper;

        /// <summary>
        /// Base address of the module in memory.
        /// </summary>
        private readonly nint moduleBaseAddress;

        /// <summary>
        /// Gets the DOS header of the PE image.
        /// </summary>
        public DosHeader DosHeaders { get; private set; }

        /// <summary>
        /// Gets the NT headers of the PE image.
        /// </summary>
        public PEHeader PEHeaders { get; private set; }

        /// <summary>
        /// Gets the section table entries of the PE image.
        /// </summary>
        public SectionTableEntry[] Sections { get; private set; }


        private IList<(string DllName, string MethodName, nint RVA, nint Address)>? _exports = null;

        /// <summary>
        /// Gets the list of exported functions from the PE image.
        /// </summary>
        public IList<(string DllName, string MethodName, nint RVA, nint Address)> Exports => _exports ??= LoadExports();


        public IList<(string DllName, string MethodName, ushort Hint, nint Address)>? _imports { get; private set; }

        /// <summary>
        /// Gets the list of imported functions from the PE image.
        /// </summary>
        public IList<(string DllName, string MethodName, ushort Hint, nint Address)> Imports => _imports ??= LoadImports();


        /// <summary>
        /// Indicates whether the PE image is 64-bit.
        /// </summary>
        private bool is64bits;

        /// <summary>
        /// Initializes a new instance of the <see cref="PEImage"/> class.
        /// </summary>
        /// <param name="processHandle">Handle to the process containing the module.</param>
        /// <param name="moduleBaseAddress">Base address of the module in memory.</param>
        private PEImage(SafeHandle? processHandle, nint moduleBaseAddress)
        {
            this.moduleBaseAddress = moduleBaseAddress;
            mapper = new NativeMapper(processHandle, moduleBaseAddress);
            Init();
        }

        /// <summary>
        /// Creates a <see cref="PEImage"/> for a process module loaded by the current process, with the specified base address.
        /// </summary>
        /// <param name="moduleBaseAddress">Base address of the module in memory.</param>
        /// <returns>A new instance of the <see cref="PEImage"/> class.</returns>
        public static PEImage CreateForProcessModule(nint moduleBaseAddress)
        {
            return new PEImage(null, moduleBaseAddress);
        }

        /// <summary>
        /// Creates a <see cref="PEImage"/> for a process module with the specified process handle and base address.
        /// </summary>
        /// <param name="processHandle">Handle to the process containing the module.</param>
        /// <param name="moduleBaseAddress">Base address of the module in memory.</param>
        /// <returns>A new instance of the <see cref="PEImage"/> class.</returns>
        public static PEImage CreateForProcessModule(SafeHandle? processHandle, nint moduleBaseAddress)
        {
            return new PEImage(processHandle, moduleBaseAddress);
        }

        /// <summary>
        /// Initializes the PE image by mapping its headers and sections.
        /// </summary>
        private unsafe void Init()
        {
            DosHeaders = mapper.Map<DosHeader>(0);
            PEHeaders = mapper.Map<PEHeader>((nint)DosHeaders.PEHeaderAddress);

            if (!PEHeaders.IsValid())
            {
                throw new IOException("The provided image is not a PE image and cannot be parsed.");
            }

            Sections = mapper.MapArray<SectionTableEntry>((nint)DosHeaders.PEHeaderAddress + sizeof(PEHeader), PEHeaders.FileHeader.NumberOfSections);
            
            var resourceDirectory = PEHeaders.OptionalHeader.DataDirectories.ResourceTable.GetSingle(mapper);

            is64bits = PEHeaders.OptionalHeader.ImageMagic == ReflectionPE.PEMagic.PE32Plus;
        }


        private IList<(string DllName, string MethodName, nint RVA, nint Address)> LoadExports()
        {
            var exportDirectory = PEHeaders.OptionalHeader.DataDirectories.ExportTable.GetSingle(mapper);
            return exportDirectory.GetExportedFunctions(mapper, moduleBaseAddress).ToList();
        }


        private IList<(string DllName, string MethodName, ushort Hint, nint Address)> LoadImports()
        {
            var importDirectoryEntries = PEHeaders.OptionalHeader.DataDirectories.ImportTable.GetAllEntries(mapper);
            return importDirectoryEntries.SelectMany(dir => dir.GetImportedFunctions(mapper, moduleBaseAddress, is64bits)).ToList();
        }
    }
}
