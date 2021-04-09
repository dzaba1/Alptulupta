using System.Collections.Generic;

namespace Alptulupta.Contracts
{
    public static class Constants
    {
        public static readonly ISet<int> VkCodesToBlock = new HashSet<int>
        {
            91, 160, 9, 20, 162, 164, 93, 163, 161, 44, 145, 19, 144, 92
        };
    }
}
