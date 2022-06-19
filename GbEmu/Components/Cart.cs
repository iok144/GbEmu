using GbEmu.Extensions;

namespace GbEmu.Components
{
    internal class Cart
    {
        private string Filename { get; set; }
        private byte[] RomData { get; set; }
        private RomHeader Header { get; set; }

        private readonly IReadOnlyList<string> ROM_TYPES = new List<string>()
        {
            "ROM ONLY",
            "MBC1",
            "MBC1+RAM",
            "MBC1+RAM+BATTERY",
            "???",
            "MBC2",
            "MBC2+BATTERY",
            "???",
            "ROM+RAM 1",
            "ROM+RAM+BATTERY 1",
            "???",
            "MMM01",
            "MMM01+RAM",
            "MMM01+RAM+BATTERY",
            "???",
            "MBC3+TIMER+BATTERY",
            "MBC3+TIMER+RAM+BATTERY 2",
            "MBC3",
            "MBC3+RAM 2",
            "MBC3+RAM+BATTERY 2",
            "???",
            "???",
            "???",
            "???",
            "???",
            "MBC5",
            "MBC5+RAM",
            "MBC5+RAM+BATTERY",
            "MBC5+RUMBLE",
            "MBC5+RUMBLE+RAM",
            "MBC5+RUMBLE+RAM+BATTERY",
            "???",
            "MBC6",
            "???",
            "MBC7+SENSOR+RUMBLE+RAM+BATTERY"
        };

        private readonly IReadOnlyDictionary<int, string> LIC_CODES = new Dictionary<int, string>
        {
            [0x00] = "None",
            [0x01] = "Nintendo R&D1",
            [0x08] = "Capcom",
            [0x13] = "Electronic Arts",
            [0x18] = "Hudson Soft",
            [0x19] = "b-ai",
            [0x20] = "kss",
            [0x22] = "pow",
            [0x24] = "PCM Complete",
            [0x25] = "san-x",
            [0x28] = "Kemco Japan",
            [0x29] = "seta",
            [0x30] = "Viacom",
            [0x31] = "Nintendo",
            [0x32] = "Bandai",
            [0x33] = "Ocean/Acclaim",
            [0x34] = "Konami",
            [0x35] = "Hector",
            [0x37] = "Taito",
            [0x38] = "Hudson",
            [0x39] = "Banpresto",
            [0x41] = "Ubi Soft",
            [0x42] = "Atlus",
            [0x44] = "Malibu",
            [0x46] = "angel",
            [0x47] = "Bullet-Proof",
            [0x49] = "irem",
            [0x50] = "Absolute",
            [0x51] = "Acclaim",
            [0x52] = "Activision",
            [0x53] = "American sammy",
            [0x54] = "Konami",
            [0x55] = "Hi tech entertainment",
            [0x56] = "LJN",
            [0x57] = "Matchbox",
            [0x58] = "Mattel",
            [0x59] = "Milton Bradley",
            [0x60] = "Titus",
            [0x61] = "Virgin",
            [0x64] = "LucasArts",
            [0x67] = "Ocean",
            [0x69] = "Electronic Arts",
            [0x70] = "Infogrames",
            [0x71] = "Interplay",
            [0x72] = "Broderbund",
            [0x73] = "sculptured",
            [0x75] = "sci",
            [0x78] = "THQ",
            [0x79] = "Accolade",
            [0x80] = "misawa",
            [0x83] = "lozc",
            [0x86] = "Tokuma Shoten Intermedia",
            [0x87] = "Tsukuda Original",
            [0x91] = "Chunsoft",
            [0x92] = "Video system",
            [0x93] = "Ocean/Acclaim",
            [0x95] = "Varie",
            [0x96] = "Yonezawa/s’pal",
            [0x97] = "Kaneko",
            [0x99] = "Pack in soft",
            [0xA4] = "Konami (Yu-Gi-Oh!)",
        };

        internal Cart()
        {
            Filename = string.Empty;
            Header = new RomHeader();
        }

        private string GetTypeName(byte typeCode)
        {
            if (typeCode <= 0x22)
            {
                return ROM_TYPES[typeCode];
            }

            return "UNKNOWN";
        }

        private string GetLicName(byte licCode)
        {
            if (LIC_CODES.ContainsKey(licCode))
            {
                return LIC_CODES[licCode];
            }

            return "UNKNOWN";
        }

        internal bool LoadCart(string filename)
        {
            try
            {
                RomData = File.ReadAllBytes(filename);
            }
            catch (Exception)
            {
                return false;
            }

            Console.WriteLine("Cart Loaded:\n");

            ReadHeader(RomData);
            var checksum = CalcHeaderChecksum(RomData);

            Console.WriteLine(String.Format("\t Title       : {0}", Header.Title));
            Console.WriteLine(String.Format("\t Type        : {0:X2} {1}", Header.Type, GetTypeName(Header.Type)));
            Console.WriteLine(String.Format("\t ROM Size    : {0} KB", 32 << Header.RomSize));
            Console.WriteLine(String.Format("\t RAM Size    : {0:X2}", Header.RamSize));
            Console.WriteLine(String.Format("\t LIC Code    : {0:X2} {1}", Header.LicCode, GetLicName(Header.LicCode)));
            Console.WriteLine(String.Format("\t ROM Version : {0:X2}", Header.Version));
            Console.WriteLine(String.Format("\t Checksum    : {0:X2} {1}\n",
                                            Header.Checksum,
                                            IsValid(checksum, Header) ? "PASSED" : "FAILED"));

            return true;
        }

        private void ReadHeader(byte[] romData)
        {
            Header.Title = new string(romData.GetCharSubArray(0x134, 0x143));
            Header.Type = romData[0x147];
            Header.RomSize = romData[0x148];
            Header.RamSize = romData[0x149];
            Header.LicCode = romData[0x14B];
            Header.Version = romData[0x14C];
            Header.Checksum = romData[0x14D];
        }

        private UInt16 CalcHeaderChecksum(byte[] romData)
        {
            int checksum = 0;

            for (int i = 0x134; i <= 0x14C; i++)
            {
                checksum = checksum - romData[i] - 1;
            }

            return (UInt16)checksum;
        }

        private bool IsValid(UInt16 checksum, RomHeader header)
        {
            return (checksum & 0xFF) == header.Checksum;
        }

        internal byte ReadCart(UInt16 address)
        {
            //only ROM ONLY type supported
            try
            {
                return RomData[address];
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void WriteCart(UInt16 address, byte value)
        {
            //only ROM ONLY type supported

            RomData[address] = value;
        }
    }
}
