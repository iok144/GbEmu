namespace GbEmu.Components
{
    internal class Bus
    {
        private Cart Cart { get; set; }

        internal Bus(Cart cart)
        {
            Cart = cart;
        }

        internal byte ReadBus(UInt16 address)
        {
            if (address < 0x8000)
            {
                //ROM Data
                return Cart.ReadCart(address);
            }

            if (address < 0xA000)
            {
                //Char/Map Data
                Console.WriteLine("(Char/Map Data) NOT IMPLEMENTED");
                throw new Exception();
            }

            if (address < 0xC000)
            {
                //Cartridge RAM
                return Cart.ReadCart(address);
            }

            if (address < 0xE000)
            {
                //WRAM
                return ReadWRam(address);
            }

            Console.WriteLine("NOT IMPLEMENTED");
            throw new Exception();
        }

        internal void WriteBus(UInt16 address, byte value)
        {
            if (address < 0x8000)
            {
                //ROM Data
                Cart.WriteCart(address, value);
                return;
            }

            if (address < 0xA000)
            {
                //Char/Map Data
                Console.WriteLine("(Char/Map Data) NOT IMPLEMENTED");
            }

            if (address < 0xC000)
            {
                //Cartridge RAM
                Cart.WriteCart(address, value);
                return;
            }

            if (address < 0xE000)
            {
                //WriteWRam(address, value);
            }

            Console.WriteLine("NOT IMPLEMENTED");
        }

        private byte ReadWRam(UInt16 address)
        {
            return 0;
        }
    }
}
