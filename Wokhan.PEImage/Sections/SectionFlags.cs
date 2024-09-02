using System;

namespace Wokhan.PEImage.Sections;

/// <summary> 
/// Defines the flags that can be applied to sections within a PE image. 
/// </summary>
[Flags]
public enum SectionFlags : uint
{
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    Reserved0 = 0x00000000,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    Reserved1 = 0x00000001,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    Reserved2 = 0x00000002,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    Reserved3 = 0x00000004,

    /// <summary>
    /// Indicates that the section should not be padded to the next boundary. This flag is obsolete and replaced by IMAGE_SCN_ALIGN_1BYTES. Valid only for object files.
    /// </summary>
    IMAGE_SCN_TYPE_NO_PAD = 0x00000008,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    Reserved4 = 0x00000010,

    /// <summary>
    /// Indicates that the section contains executable code.
    /// </summary>
    IMAGE_SCN_CNT_CODE = 0x00000020,

    /// <summary>
    /// Indicates that the section contains initialized data.
    /// </summary>
    IMAGE_SCN_CNT_INITIALIZED_DATA = 0x00000040,

    /// <summary>
    /// Indicates that the section contains uninitialized data.
    /// </summary>
    IMAGE_SCN_CNT_UNINITIALIZED_DATA = 0x00000080,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    IMAGE_SCN_LNK_OTHER = 0x00000100,

    /// <summary>
    /// Indicates that the section contains comments or other information. The .drectve section has this type. Valid only for object files.
    /// </summary>
    IMAGE_SCN_LNK_INFO = 0x00000200,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    Reserved5 = 0x00000400,

    /// <summary>
    /// Indicates that the section will not become part of the image. Valid only for object files.
    /// </summary>
    IMAGE_SCN_LNK_REMOVE = 0x00000800,

    /// <summary>
    /// Indicates that the section contains COMDAT data. See COMDAT Sections (Object Only) for more details. Valid only for object files.
    /// </summary>
    IMAGE_SCN_LNK_COMDAT = 0x00001000,

    /// <summary>
    /// Indicates that the section contains data referenced through the global pointer (GP).
    /// </summary>
    IMAGE_SCN_GPREL = 0x00008000,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    IMAGE_SCN_MEM_PURGEABLE = 0x00020000,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    IMAGE_SCN_MEM_16BIT = 0x00020000,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    IMAGE_SCN_MEM_LOCKED = 0x00040000,

    /// <summary>
    /// Reserved for future use.
    /// </summary>
    IMAGE_SCN_MEM_PRELOAD = 0x00080000,

    /// <summary>
    /// Aligns data on a 1-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_1BYTES = 0x00100000,

    /// <summary>
    /// Aligns data on a 2-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_2BYTES = 0x00200000,

    /// <summary>
    /// Aligns data on a 4-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_4BYTES = 0x00300000,

    /// <summary>
    /// Aligns data on an 8-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_8BYTES = 0x00400000,

    /// <summary>
    /// Aligns data on a 16-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_16BYTES = 0x00500000,

    /// <summary>
    /// Aligns data on a 32-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_32BYTES = 0x00600000,

    /// <summary>
    /// Aligns data on a 64-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_64BYTES = 0x00700000,

    /// <summary>
    /// Aligns data on a 128-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_128BYTES = 0x00800000,

    /// <summary>
    /// Aligns data on a 256-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_256BYTES = 0x00900000,

    /// <summary>
    /// Aligns data on a 512-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_512BYTES = 0x00A00000,

    /// <summary>
    /// Aligns data on a 1024-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_1024BYTES = 0x00B00000,

    /// <summary>
    /// Aligns data on a 2048-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_2048BYTES = 0x00C00000,

    /// <summary>
    /// Aligns data on a 4096-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_4096BYTES = 0x00D00000,

    /// <summary>
    /// Aligns data on an 8192-byte boundary. Valid only for object files.
    /// </summary>
    IMAGE_SCN_ALIGN_8192BYTES = 0x00E00000,

    /// <summary>
    /// Indicates that the section contains extended relocations.
    /// </summary>
    IMAGE_SCN_LNK_NRELOC_OVFL = 0x01000000,

    /// <summary>
    /// Indicates that the section can be discarded as needed.
    /// </summary>
    IMAGE_SCN_MEM_DISCARDABLE = 0x02000000,

    /// <summary>
    /// Indicates that the section cannot be cached.
    /// </summary>
    IMAGE_SCN_MEM_NOT_CACHED = 0x04000000,

    /// <summary>
    /// Indicates that the section is not pageable.
    /// </summary>
    IMAGE_SCN_MEM_NOT_PAGED = 0x08000000,

    /// <summary>
    /// Indicates that the section can be shared in memory.
    /// </summary>
    IMAGE_SCN_MEM_SHARED = 0x10000000,

    /// <summary>
    /// Indicates that the section can be executed as code.
    /// </summary>
    IMAGE_SCN_MEM_EXECUTE = 0x20000000,

    /// <summary>
    /// Indicates that the section can be read.
    /// </summary>
    IMAGE_SCN_MEM_READ = 0x40000000,

    /// <summary>
    /// Indicates that the section can be written to.
    /// </summary>
    IMAGE_SCN_MEM_WRITE = 0x80000000
}
