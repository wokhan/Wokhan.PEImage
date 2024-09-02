namespace Wokhan.PEImage.Sections.idata;

/// <summary>
/// Represents the Delay-Load Directory Table, which contains information for delay-loading DLLs.
/// Delay-loading allows an application to defer the loading of a DLL until it is actually needed,
/// which can improve startup performance and reduce memory usage.
/// </summary>
public struct DelayLoadDirectoryEntry : INullableDataDirectoryEntry
{
    public readonly bool IsNull() => true;
}
