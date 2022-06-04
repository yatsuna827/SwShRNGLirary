using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPRNG.Xoroshiro128p;

namespace SwShRNGLibrary.OverWorldRNG
{
    public class IdLotteryGenerator
    {
        private readonly int npc;
        public uint Generate((ulong s0, ulong s1) state)
        {
            for (int i = 0; i < npc; i++) state.GetRand(91);
            state.Advance();
            state.GetRand(60);

            var number = 0u;
            for (uint digit = 10000; digit > 0; digit /= 10)
                number += (uint)state.GetRand(10) * digit;

            return number;
        }

        /// <summary>
        /// npc: 主人公を除いたNPCの数
        /// </summary>
        /// <param name="npc"></param>
        public IdLotteryGenerator(int npc)
            => this.npc = npc + 1;
    }
}
