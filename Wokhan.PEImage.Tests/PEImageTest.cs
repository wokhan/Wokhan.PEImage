using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Tests;

public partial class PEImageTest
{
    private const string EXPORT_LIB_NAME = "Wokhan.PEImage.TestsNE.dll";

    [UnmanagedCallersOnly]
    public static int DummyExport(int _) => 1;

    [UnmanagedCallersOnly]
    public static int DummyExportOther(int _1, int _2) => 1;

    [UnmanagedCallersOnly]
    public static int ThisIsAnExtremelyLongMethodNameThatIsExactlyTwoHundredAndFiftyFiveCharactersLongAndIsDesignedToTestTheLimitsOfWhatIsPossibleWhenCreatingMethodNamesInCSharpSoThatWeCanEnsureThatOurCodeIsRobustAndHandlesEdgeCasesProperlyWithoutAnyIssues() => 1;

    [UnmanagedCallersOnly]
    public static int ThisIsAnExtremelyLongMethodNameThatIsSlightlyMoreThanTwoHundredAndFiftySixCharactersLongAndIsDesignedToTestTheLimitsOfWhatIsPossibleWhenCreatingMethodNamesInCSharpSoThatWeCanEnsureThatOurCodeIsRobustAndHandlesEdgeCasesProperlyWithoutAnyIssuesOrErrors() => 2;

    [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    protected static partial IntPtr LoadLibraryW(string lpFileName);
    
    private readonly SafeHandle CurrentProcessHandle;
    private readonly ProcessModule ExportModule;

    public PEImageTest()
    {
        LoadLibraryW(EXPORT_LIB_NAME);

        var process = Process.GetCurrentProcess();

        CurrentProcessHandle = process.SafeHandle;
        ExportModule = process.Modules.Cast<ProcessModule>().First(mod => mod.ModuleName == EXPORT_LIB_NAME);

    }

    [Fact]
    public void ShouldGiveTheSameResultWithDirectMemoryAccessAndReadMemory()
    {
        var imageRemote = PEImage.CreateForProcessModule(CurrentProcessHandle, ExportModule.BaseAddress);
        var imageSelf = PEImage.CreateForProcessModule(ExportModule.BaseAddress);

        Assert.Equivalent(imageRemote, imageSelf, true);
    }

    [Fact]
    public void ExportsShouldContainTheDummyExportMethod()
    {
        var image = PEImage.CreateForProcessModule(ExportModule.BaseAddress);

        Assert.Contains(image.Exports, meth => meth.MethodName == nameof(DummyExport));
    }


    //[Fact]
    //public void ImportsShouldContainTheDummyExportImport()
    //{
    //    var process = Process.GetCurrentProcess();
    //    var selfmodule = process.Modules.Cast<ProcessModule>().First(mod => mod.ModuleName == "Wokhan.PEImage.Tests.dll");

    //    var image = PortableExecutable.PEImage.CreateForProcessModule(selfmodule.BaseAddress);

    //    Assert.Contains(image.Imports, meth => meth.MethodName == nameof(DummyExport));
    //}
}
