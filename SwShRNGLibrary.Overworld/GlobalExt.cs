using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPRNG.Xoroshiro128p;

namespace SwShRNGLibrary.Overworld
{
    public static class Global
    {
        public static (ulong s0, ulong s1) InitializeNPC(this (ulong s0, ulong s1) state, uint npc, bool isStared = false)
        {
            for (int i = 0; i < npc; i++)
                state.GetRand(91);
            state.Advance();
            state.GetRand(61);
            if (isStared) state.GetRand(360);

            return state;
        }

        public static (ulong s0, ulong s1) InitializeNPC(this (ulong s0, ulong s1) state, uint npc, ref uint index, bool isStared = false)
        {
            for (int i = 0; i < npc; i++)
                state.GetRand(91, ref index);
            state.Advance(ref index);
            state.GetRand(61, ref index);
            if (isStared) state.GetRand(360, ref index);

            return state;
        }

        /// <summary>
        /// O(n)です。使わない方が良いです。
        /// </summary>
        /// <param name="state"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static uint GetIndex(this (ulong s0, ulong s1) state, (ulong s0, ulong s1) init)
        {
            for (uint i = 0; ; i++, init.Advance())
            {
                if (state == init) return i;
            }
        }
    }
}
