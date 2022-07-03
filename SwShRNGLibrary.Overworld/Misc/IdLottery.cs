using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPRNG.Xoroshiro128p;

namespace SwShRNGLibrary.Overworld.Misc
{
    public class IdLotteryGenerator : IGeneratable<uint>
    {
        public uint Generate((ulong s0, ulong s1) state)
        {
            var number = 0u;
            for (uint digit = 10000; digit > 0; digit /= 10)
                number += (uint)state.GetRand(10) * digit;

            return number;
        }
    }
}
