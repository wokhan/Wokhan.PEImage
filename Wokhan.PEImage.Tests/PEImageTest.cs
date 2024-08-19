using DNNE;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Tests;

public class PEImageTest
{
    private const string EXPORT_LIB_NAME = "Wokhan.PEImage.TestsNE.dll";

    [UnmanagedCallersOnly]
    public static int DummyExport(int _) => 1;

    [UnmanagedCallersOnly]
    public static int DummyExportOther(int _1, int _2) => 1;

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr LoadLibrary(string lpFileName);

    [Fact]
    public void ShouldParseImageWhenUsingReadMemory()
    {
        LoadLibrary(EXPORT_LIB_NAME);

        var process = Process.GetCurrentProcess();

        var selfmodule = process.Modules.Cast<ProcessModule>().First(mod => mod.ModuleName == EXPORT_LIB_NAME);
        var image = PortableExecutable.PEImage.CreateForProcessModule(process.SafeHandle, selfmodule.BaseAddress);

        var exportedFunctions = image.Exports;

        var foundMeth = exportedFunctions.First(meth => meth.MethodName == nameof(DummyExport));

        var importedFunctions = image.Imports;
    }

    [Fact]
    public void ShouldParseImageWhenUsingDirectMemoryAccess()
    {
        LoadLibrary(EXPORT_LIB_NAME);

        var process = Process.GetCurrentProcess();

        var selfmodule = process.Modules.Cast<ProcessModule>().First(mod => mod.ModuleName == EXPORT_LIB_NAME);
        var image = PortableExecutable.PEImage.CreateForProcessModule(selfmodule.BaseAddress);

        var exportedFunctions = image.Exports;
        var importedFunctions = image.Imports;
    }
}
