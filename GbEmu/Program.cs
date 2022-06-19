using GbEmu.Components;

namespace GbEmu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var emulator = new Emulator();

            emulator.RunEmu(args);
        }
    }
}