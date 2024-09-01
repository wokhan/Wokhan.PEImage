using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.pdata;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ExceptionsFunctionTableEntry : INullableDataDirectoryEntry
{
    /// <summary>
    /// The VA of the corresponding function.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint BeginAddress;

    /// <summary>
    /// The VA of the end of the function.
    /// </summary>
    [FieldOffset(4)]
    public readonly uint EndAddress;

    /// <summary>
    /// The RVA of the unwind information.
    /// </summary>
    [FieldOffset(8)]
    public readonly uint UnwindInformation;

    public readonly bool IsNull() => true;
}
