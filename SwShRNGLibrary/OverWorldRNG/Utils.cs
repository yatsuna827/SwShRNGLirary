using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPRNG.Xoroshiro128p;

namespace SwShRNGLibrary.OverWorldRNG
{
    public static class Utils
    {
        /// <summary>
        /// npc: 主人公を除いたNPCの数
        /// </summary>
        /// <param name="npc"></param>
        public static (ulong s0, ulong s1) InitializeNPC(this (ulong s0, ulong s1) state, uint npc)
        {
            for (int i = 0; i < npc + 1; i++) 
                state.GetRand(91);
            state.Advance();
            state.GetRand(61);

            return state;
        }

        public static (ulong s0, ulong s1) InitializeNPC(this (ulong s0, ulong s1) state, uint npc, ref uint index)
        {
            for (int i = 0; i < npc + 1; i++)
                state.GetRand(91, ref index);
            state.Advance(ref index);
            state.GetRand(61, ref index);

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
            for (uint i=0; ; i++, init.Advance())
            {
                if (state == init) return i;
            }
        }
    }
}
