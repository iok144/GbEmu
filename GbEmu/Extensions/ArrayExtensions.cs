namespace GbEmu.Extensions
{
    internal static class ArrayExtensions
    {
        internal static byte[] GetSubArray(this byte[] data, int startIndex, int endIndex)
        {
            var result = new byte[endIndex - startIndex + 1];
            int j = 0;

            for (int i = startIndex; i <= endIndex; i++)
            {
                result[j++] = data[i];
            }

            return result;
        }

        internal static char[] GetCharSubArray(this byte[] data, int startIndex, int endIndex)
        {
            var result = new char[endIndex - startIndex + 1];
            int j = 0;

            for (int i = startIndex; i <= endIndex; i++)
            {
                result[j++] = (char)data[i];
            }

            return result;
        }
    }
}
