using System;
using System.Diagnostics;

namespace Wokhan.PEImage.Sections;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
[AttributeUsage(AttributeTargets.Field)]
internal class PESectionAttribute : Attribute
{
    private readonly string _sectionCode;

    public PESectionAttribute(string sectionCode)
    {
        _sectionCode = sectionCode;
    }

    private string GetDebuggerDisplay()
    {
        return _sectionCode.ToString();
    }
}