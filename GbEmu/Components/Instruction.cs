namespace GbEmu.Components
{
    internal class Instruction
    {
        internal InType Type { get; set; }
        internal AddrMode Mode { get; set; }
        internal RegType Reg1 { get; set; }
        internal RegType Reg2 { get; set; }
        internal CondType CondType { get; set; }
        internal byte Param { get; set; }
    }
}
