using System;

using Wokhan.PEImage.Headers.Directories;

namespace Wokhan.PEImage;

public interface INullableDataDirectoryEntry
{
    bool IsNull() { throw new Exception($"{nameof(IsNull)} method is not supposed to be called (use GetSingle instead or GetEntries) or you forgot to implement it."); }
}