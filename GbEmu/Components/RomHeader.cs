namespace GbEmu.Components
{
    internal class RomHeader
    {
        internal RomHeader()
        {
            Entry = new byte[4];
            Logo = new byte[0x30];
            Title = String.Empty;
        }

        internal byte[] Entry { get; set; }
        internal byte[] Logo { get; set; }
        
        internal string Title { get; set; }
        internal UInt16 NewLicenseeCode { get; set; }
        internal byte SgbFlag { get; set; }
        internal byte Type { get; set; }
        internal byte RomSize { get; set; }
        internal byte RamSize { get; set; }
        internal byte DestinationCode { get; set; }
        internal byte LicCode { get; set; }
        internal byte Version { get; set; }
        internal byte Checksum { get; set; }
        internal ushort GlobalChecksum { get; set; }
    }
}
