namespace GbEmu.Components
{
    internal class CpuContext
    {
        internal CpuRegisters Regs { get; }
        internal Bus Bus { get; }
        internal Instructions Instructions { get; }

        internal UInt16 FetchedData { get; set; }
        internal UInt16? MemDest { get; set; }
        internal byte CurrentOpcode { get; set; }
        internal bool Halted { get; set; }
        internal bool Stepping { get; set; }
        internal bool DestIsMem { get; set; }

        internal Instruction? CurrentInst { get; set; }

        internal CpuContext(Cart cart)
        {
            Regs = new CpuRegisters()
            {
                A = 0x01,
                F = 0xB0,
                B = 0x00,
                C = 0x13,
                D = 0x00,
                E = 0xD8,
                H = 0x01,
                L = 0x4D,
                SP = 0xFFFE,
                PC = 0x0100
            };

            Bus = new Bus(cart);

            Instructions = new Instructions();
        }
    }
}
