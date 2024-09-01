namespace Wokhan.PEImage.Headers.Directories;

public readonly struct ReservedEmptyDirectory : INullableDataDirectoryEntry
{
    public readonly bool IsNull() => false;
}
