using System.Runtime.InteropServices;

namespace Wokhan.PEImage.Sections.rsrc;

[StructLayout(LayoutKind.Explicit)]
public readonly struct ResourceDirectoryString
{
    /// <summary>
    /// The size of the string, not including length field itself.
    /// </summary>
    [FieldOffset(0)]
    public readonly ushort Length;

    /// <summary>
    /// The variable-length Unicode string data, word-aligned.
    /// </summary>
    [FieldOffset(2)]
    public readonly string UnicodeString;
}

