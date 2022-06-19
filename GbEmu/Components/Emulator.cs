namespace GbEmu.Components
{
    internal class Emulator
    {
        private bool Running { get; set; }
        private bool Paused { get; set; }
        private UInt64 Ticks { get; set; }

        internal void RunEmu(string[] args)
        {
            if (args.Count() < 2)
            {
                Console.WriteLine("Usage: emu <rom_file>");
                return;
            }

            var cart = new Cart();
            var cartLoaded = cart.LoadCart(args[1]);

            if (!cartLoaded)
            {
                Console.WriteLine($"Failed to load ROM file: {args[1]}");
                return;
            }

            var cpu = new Cpu(cart);

            Running = true;
            Paused = false;
            Ticks = 0;

            while (Running)
            {
                if (!cpu.CpuStep())
                {
                    Console.WriteLine("CPU Stopped");
                    return;
                }

                Ticks++;
            }
        }
    }
}
