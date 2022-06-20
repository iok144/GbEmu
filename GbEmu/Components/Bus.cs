namespace GbEmu.Components
{
    internal class Bus
    {
        private Cart Cart { get; set; }
        private Ram Ram { get; set; }

        internal Bus(Cart cart)
        {
            Cart = cart;
            Ram = new Ram();
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
                return Ram.ReadWRam(address);
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
                Ram.WriteWRam(address, value);
                return;
            }

            Console.WriteLine("NOT IMPLEMENTED");
        }
    }
}
