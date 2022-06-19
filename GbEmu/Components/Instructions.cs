namespace GbEmu.Components
{
    internal class Instructions
    {
        private readonly IDictionary<byte, Instruction> _instructions = new Dictionary<byte, Instruction>()
        {
            [0x00] = new Instruction() { Type = InType.IN_NOP, Mode = AddrMode.AM_IMP },
            [0x06] = new Instruction() { Type = InType.IN_LD, Mode = AddrMode.AM_R_D8, Reg1 = RegType.RT_B},
            [0x0E] = new Instruction() { Type = InType.IN_LD, Mode = AddrMode.AM_R_D8, Reg1 = RegType.RT_C },
            [0x21] = new Instruction() { Type = InType.IN_LD, Mode = AddrMode.AM_R_D16, Reg1 = RegType.RT_HL },
            [0x32] = new Instruction() { Type = InType.IN_LD, Mode = AddrMode.AM_HLD_R, Reg1 = RegType.RT_HL, Reg2 = RegType.RT_A },
            [0xC3] = new Instruction() { Type = InType.IN_JP, Mode = AddrMode.AM_D16 },
            [0xAF] = new Instruction() { Type = InType.IN_XOR, Mode = AddrMode.AM_R, Reg1 = RegType.RT_A}
        };

        internal Instruction? GetInstruction(byte opcode)
        {
            if (_instructions.ContainsKey(opcode))
            {
                return _instructions[opcode];
            }
            else
            {
                return null;
            }
        }
    }
}
