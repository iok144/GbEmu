namespace GbEmu.Components
{
    internal class Ram
    {
        internal byte[] WRam { get;set; }
        internal byte[] HRam { get;set; }

        internal Ram()
        {
            WRam = new byte[0x2000];
            HRam = new byte[0x80];
        }

        internal byte ReadWRam(UInt16 address)
        {
            address -= (UInt16)0xC000;

            if (address >= 0x2000)
            {
                throw new Exception(String.Format("INVALID WRAM ADDR {0:X4}", address));
            }

            return WRam[address];
        }

        internal void WriteWRam(UInt16 address, byte value)
        {
            address -= (UInt16)0xC000;

            WRam[address] = value;
        }

        internal byte ReadHRam(UInt16 address)
        {
            address -= (UInt16)0xFF80;

            return HRam[address];
        }

        internal void WriteHRam(UInt16 address, byte value)
        {
            address -= 0xFF80;

            HRam[address] = value;
        }
    }
}
