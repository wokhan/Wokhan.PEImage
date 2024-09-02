namespace Wokhan.PEImage.Sections.reloc;

/// <summary>
/// Specifies the types of base relocations that can be applied to a PE file.
/// </summary>
public enum BaseRelocationType : ushort
{
    /// <summary>
    /// The base relocation is skipped. This type can be used to pad a block.
    /// </summary>
    IMAGE_REL_BASED_ABSOLUTE = 0,

    /// <summary>
    /// Adds the high 16 bits of the difference to the 16-bit field at offset. The 16-bit field represents the high value of a 32-bit word.
    /// </summary>
    IMAGE_REL_BASED_HIGH = 1,

    /// <summary>
    /// Adds the low 16 bits of the difference to the 16-bit field at offset. The 16-bit field represents the low half of a 32-bit word.
    /// </summary>
    IMAGE_REL_BASED_LOW = 2,

    /// <summary>
    /// Applies all 32 bits of the difference to the 32-bit field at offset.
    /// </summary>
    IMAGE_REL_BASED_HIGHLOW = 3,

    /// <summary>
    /// Adds the high 16 bits of the difference to the 16-bit field at offset. The 16-bit field represents the high value of a 32-bit word. The low 16 bits of the 32-bit value are stored in the 16-bit word that follows this base relocation, occupying two slots.
    /// </summary>
    IMAGE_REL_BASED_HIGHADJ = 4,

    /// <summary>
    /// Applies to a MIPS jump instruction. The interpretation is dependent on the machine type.
    /// </summary>
    IMAGE_REL_BASED_MIPS_JMPADDR = 5,

    /// <summary>
    /// Applies the 32-bit address of a symbol across a consecutive MOVW/MOVT instruction pair. This is meaningful only for ARM or Thumb machine types.
    /// </summary>
    IMAGE_REL_BASED_ARM_MOV32 = 5,

    /// <summary>
    /// Applies to the high 20 bits of a 32-bit absolute address. This is meaningful only for RISC-V machine types.
    /// </summary>
    IMAGE_REL_BASED_RISCV_HIGH20 = 5,

    /// <summary>
    /// Reserved, must be zero.
    /// </summary>
    Reserved = 6,

    /// <summary>
    /// Applies the 32-bit address of a symbol to a consecutive MOVW/MOVT instruction pair. This is meaningful only for Thumb machine types.
    /// </summary>
    IMAGE_REL_BASED_THUMB_MOV32 = 7,

    /// <summary>
    /// Applies to the low 12 bits of a 32-bit absolute address formed in RISC-V I-type instruction format. This is meaningful only for RISC-V machine types.
    /// </summary>
    IMAGE_REL_BASED_RISCV_LOW12I = 7,

    /// <summary>
    /// Applies to the low 12 bits of a 32-bit absolute address formed in RISC-V S-type instruction format. This is meaningful only for RISC-V machine types.
    /// </summary>
    IMAGE_REL_BASED_RISCV_LOW12S = 8,

    /// <summary>
    /// Applies to a 32-bit absolute address formed in two consecutive instructions. This is meaningful only for LoongArch 32-bit machine types.
    /// </summary>
    IMAGE_REL_BASED_LOONGARCH32_MARK_LA = 8,

    /// <summary>
    /// Applies to a 64-bit absolute address formed in four consecutive instructions. This is meaningful only for LoongArch 64-bit machine types.
    /// </summary>
    IMAGE_REL_BASED_LOONGARCH64_MARK_LA = 8,

    /// <summary>
    /// Applies to a MIPS16 jump instruction. This is meaningful only for MIPS machine types.
    /// </summary>
    IMAGE_REL_BASED_MIPS_JMPADDR16 = 9,

    /// <summary>
    /// Applies the difference to the 64-bit field at offset.
    /// </summary>
    IMAGE_REL_BASED_DIR64 = 10
}
