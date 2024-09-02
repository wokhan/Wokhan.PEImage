using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.rsrc;

/// <summary>
/// Represents a resource directory string in a PE image.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct ResourceDirectoryString
{
    /// <summary>
    /// Gets the size of the string, excluding the length field itself.
    /// </summary>
    [FieldOffset(0)]
    public readonly ushort Length;

    /// <summary>
    /// Gets the variable-length Unicode string data, aligned to a word boundary.
    /// </summary>
    [FieldOffset(2)]
    public readonly string UnicodeString;
}
