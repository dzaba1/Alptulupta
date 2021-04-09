using Alptulupta.Contracts;

namespace Alptulupta.Core
{
    internal sealed class Random : IRandom
    {
        private static System.Random rand = new System.Random();

        public int Next(int minValue, int maxValue)
        {
            return rand.Next(minValue, maxValue);
        }
    }
}
