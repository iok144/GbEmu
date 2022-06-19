namespace GbEmu.Components
{
    internal static class CpuProc
    {
        private static void ProcNOP(CpuContext context) { }

        private static void ProcJP(CpuContext context)
        {
            context.Regs.PC = context.FetchedData;
        }

        private static void ProcXOR(CpuContext context)
        {
            context.Regs.A = (byte)(context.Regs.A ^ context.FetchedData);

            context.Regs.NFlag = 0;
            context.Regs.HFlag = 0;
            context.Regs.CFlag = 0;
        }

        private static void ProcLD(CpuContext context)
        {
            if (context.DestIsMem && context.CurrentInst.Mode == AddrMode.AM_HLD_R)
            {
                if (context.MemDest.HasValue)
                {
                    context.Bus.WriteBus(context.MemDest.Value, (byte)context.FetchedData);
                }
            }
            else
            {
                context.Regs.SetReg(context.CurrentInst.Reg1, context.FetchedData);
            }
        }

        private static readonly Dictionary<InType, Action<CpuContext>> processes = new Dictionary<InType, Action<CpuContext>>
        {
            [InType.IN_JP] = ProcJP,
            [InType.IN_NOP] = ProcNOP,
            [InType.IN_XOR] = ProcXOR,
            [InType.IN_LD] = ProcLD
        };

        internal static Action<CpuContext>? GetInstProcessor(InType inType)
        {
            if (processes.ContainsKey(inType))
            {
                return processes[inType];
            }
            else
            {
                return null;
            }
        }
    }
}
