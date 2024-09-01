using System;

namespace Wokhan.PEImage.Sections;
internal class PESectionAttribute : Attribute
{
    private string _sectionCode;

    public PESectionAttribute(string sectionCode)
    {
        _sectionCode = sectionCode;
    }
}