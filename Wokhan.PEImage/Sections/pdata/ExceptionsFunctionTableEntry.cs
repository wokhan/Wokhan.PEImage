using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.pdata;

/// <summary>
/// Represents an entry in the exception handling function table.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct ExceptionsFunctionTableEntry : INullableDataDirectoryEntry
{
    /// <summary>
    /// The virtual address (VA) of the corresponding function.
    /// </summary>
    [FieldOffset(0)]
    public readonly uint BeginAddress;

    /// <summary>
    /// The virtual address (VA) of the end of the function.
    /// </summary>
    [FieldOffset(4)]
    public readonly uint EndAddress;

    /// <summary>
    /// The relative virtual address (RVA) of the unwind information.
    /// </summary>
    [FieldOffset(8)]
    public readonly uint UnwindInformation;

    /// <summary>
    /// Determines whether the entry is null.
    /// </summary>
    /// <returns>True if the entry is null; otherwise, false.</returns>
    public readonly bool IsNull() => true;
}
