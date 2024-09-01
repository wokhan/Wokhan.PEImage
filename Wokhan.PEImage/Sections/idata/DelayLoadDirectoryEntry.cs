namespace Wokhan.PEImage.Sections.idata;

/// <summary>
/// https://learn.microsoft.com/en-us/windows/win32/debug/pe-format#the-delay-load-directory-table
/// </summary>
public struct DelayLoadDirectoryEntry : INullableDataDirectoryEntry
{
    public readonly bool IsNull() => true;
}
