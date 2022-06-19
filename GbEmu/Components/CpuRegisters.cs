namespace GbEmu.Components
{
    internal class CpuRegisters
    {
        internal byte A { get; set; }
        internal byte F { get; set; }
        internal byte B { get; set; }
        internal byte C { get; set; }
        internal byte D { get; set; }
        internal byte E { get; set; }
        internal byte H { get; set; }
        internal byte L { get; set; }
        internal UInt16 PC { get; set; }
        internal UInt16 SP { get; set; }

        internal int ZFlag { get { return (F >> 7) & 1; } set { SetBit(value, 7); } }
        internal int NFlag { get { return (F >> 6) & 1; } set { SetBit(value, 6); } }
        internal int HFlag { get { return (F >> 5) & 1; } set { SetBit(value, 5); } }
        internal int CFlag { get { return (F >> 4) & 1; } set { SetBit(value, 4); } }

        internal UInt16 AF { get { return (UInt16)(A << 8 | F); } set { A = (byte)(value >> 8); F = (byte)(value & 0xFF); } }
        internal UInt16 BC { get { return (UInt16)(B << 8 | C); } set { B = (byte)(value >> 8); C = (byte)(value & 0xFF); } }
        internal UInt16 DE { get { return (UInt16)(D << 8 | E); } set { D = (byte)(value >> 8); E = (byte)(value & 0xFF); } }
        internal UInt16 HL { get { return (UInt16)(H << 8 | L); } set { H = (byte)(value >> 8); L = (byte)(value & 0xFF); } }

        internal UInt16 ReadReg(RegType regType)
        {
            switch (regType)
            {
                case RegType.RT_NONE:
                case RegType.RT_A: return A;
                case RegType.RT_F: return F;
                case RegType.RT_B: return B;
                case RegType.RT_C: return C;
                case RegType.RT_D: return D;
                case RegType.RT_E: return E;
                case RegType.RT_H: return H;
                case RegType.RT_L: return L;
                case RegType.RT_AF: return AF;
                case RegType.RT_BC: return BC;
                case RegType.RT_DE: return DE;
                case RegType.RT_HL: return HL;
                case RegType.RT_SP: return SP;
                case RegType.RT_PC: return PC;
                default: return 0;
            }
        }

        internal void SetReg(RegType regType, UInt16 value)
        {
            switch (regType)
            {
                case RegType.RT_A: A = (byte)(value & 0xFF); break;
                case RegType.RT_F: F = (byte)(value & 0xFF); break;
                case RegType.RT_B: B = (byte)(value & 0xFF); break;
                case RegType.RT_C: C = (byte)(value & 0xFF); break;
                case RegType.RT_D: D = (byte)(value & 0xFF); break;
                case RegType.RT_E: E = (byte)(value & 0xFF); break;
                case RegType.RT_H: H = (byte)(value & 0xFF); break;
                case RegType.RT_L: L = (byte)(value & 0xFF); break;
                case RegType.RT_AF: AF = value; break;
                case RegType.RT_BC: BC = value; break;
                case RegType.RT_DE: DE = value; break;
                case RegType.RT_HL: HL = value; break;
                case RegType.RT_SP: SP = value; break;
                case RegType.RT_PC: PC = value; break;
            }
        }

        private void SetBit(int value, int bitPosition)
        {
            if (value == 1)
            {
                F |= (byte)(1 << bitPosition);
            }
            else
            {
                F &= (byte)~(1 << bitPosition);
            }
        }
    }
}
