using GbEmu.Components;

namespace GbEmu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var a = 0x0000 - 1;
            Console.WriteLine(Convert.ToString((UInt16)a, toBase: 16));

            var emulator = new Emulator();

            emulator.RunEmu(args);
        }
    }
}