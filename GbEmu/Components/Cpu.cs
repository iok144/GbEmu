namespace GbEmu.Components
{
    internal class Cpu
    {
        private CpuContext _context;

        internal Cpu(Cart cart)
        {
            _context = new CpuContext(cart);
        }

        private void FetchInstruction()
        {
            _context.CurrentOpcode = _context.Bus.ReadBus(_context.Regs.PC++);
            _context.CurrentInst = _context.Instructions.GetInstruction(_context.CurrentOpcode);

            if (_context.CurrentInst == null)
            {
                return;
            }
        }

        private void FetchData()
        {
            _context.MemDest = 0;
            _context.DestIsMem = false;

            if (_context.CurrentInst == null)
            {
                return;
            }

            switch (_context.CurrentInst.Mode)
            {
                case AddrMode.AM_IMP:
                    return;
                case AddrMode.AM_R_R:
                    break;
                case AddrMode.AM_MR_R:
                    break;
                case AddrMode.AM_R:
                    _context.FetchedData = _context.Regs.ReadReg(_context.CurrentInst.Reg1);
                    return;
                case AddrMode.AM_R_D8:
                    _context.FetchedData = _context.Bus.ReadBus(_context.Regs.PC);
                    _context.Regs.PC++;
                    return;
                case AddrMode.AM_R_MR:
                    break;
                case AddrMode.AM_R_HLI:
                    break;
                case AddrMode.AM_R_HLD:
                    break;
                case AddrMode.AM_HLI_R:
                    break;
                case AddrMode.AM_HLD_R:
                    _context.DestIsMem = true;
                    _context.MemDest = _context.Regs.ReadReg(_context.CurrentInst.Reg1);
                    _context.FetchedData = _context.Regs.ReadReg(_context.CurrentInst.Reg2);
                    _context.Regs.HL--;
                    return;
                case AddrMode.AM_R_A8:
                    break;
                case AddrMode.AM_A8_R:
                    break;
                case AddrMode.AM_HL_SPR:
                    break;
                case AddrMode.AM_R_D16:
                case AddrMode.AM_D16:
                    var lo = _context.Bus.ReadBus(_context.Regs.PC);
                    var hi = _context.Bus.ReadBus((UInt16)(_context.Regs.PC + 1));

                    _context.FetchedData = (UInt16)(lo | (hi << 8));

                    _context.Regs.PC += 2;
                    return;
                case AddrMode.AM_D8:
                    break;
                case AddrMode.AM_D16_R:
                    break;
                case AddrMode.AM_MR_D8:
                    break;
                case AddrMode.AM_MR:
                    break;
                case AddrMode.AM_A16_R:
                    break;
                case AddrMode.AM_R_A16:
                    break;
                default:
                    break;
            }
        }

        private void Execute()
        {
            var proc = CpuProc.GetInstProcessor(_context.CurrentInst.Type);

            try
            {
                proc(_context);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal bool CpuStep()
        {
            if (!_context.Halted)
            {
                var pc = _context.Regs.PC;

                FetchInstruction();
                FetchData();

                Console.WriteLine(String.Format("Executing instruction: {0:X2} ({1:X2} {2:X2}) PC: {3:X4} AF: {4:X4} BC: {5:X4} HL: {6:X4}",
                    _context.CurrentOpcode,
                    _context.Bus.ReadBus((UInt16)(pc + 1)),
                    _context.Bus.ReadBus((UInt16)(pc + 2)),
                    pc,
                    _context.Regs.AF,
                    _context.Regs.BC,
                    _context.Regs.HL));

                if (_context.CurrentInst == null)
                {
                    Console.WriteLine(String.Format("Unknown instruction {0:X2}", _context.CurrentOpcode));
                    return false;
                }

                Execute();
            }

            return true;
        }
    }
}
