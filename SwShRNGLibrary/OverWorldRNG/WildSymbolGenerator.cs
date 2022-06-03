using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPRNG.Xoroshiro128p;

namespace SwShRNGLibrary
{
    class WildSymbolGenerator
    {
        public void Generate((ulong s0, ulong s1) seed)
        {
            var range = 6u;

            seed.Advance();
            seed.GetRand(100);
            seed.GetRand(100);
            var slot = seed.GetRand(100);
            var lv = seed.GetRand(range);
            var mark = 0;
            var brilliant = seed.GetRand(1000);

            var mockPID = (uint)seed.GetRand(); // TSVを使って色判定.

            seed.Advance();
            var nature = seed.GetRand(25);
            var ability = seed.GetRand(2);

            // 持ち物持っている可能性があるときだけ
            var item = seed.GetRand(100);

        }
    }

    public class StaticSymbolGenerator
    {
        public void Generate((ulong s0, ulong s1) seed)
        {
            var range = 6u;

            seed.Advance();
            seed.GetRand(100);
            seed.GetRand(100);
            var slot = seed.GetRand(100);
            var lv = seed.GetRand(range);
            var mark = 0;
            var brilliant = seed.GetRand(1000);

            var mockPID = (uint)seed.GetRand(); // TSVを使って色判定.

            seed.Advance();
            var nature = seed.GetRand(25);
            var ability = seed.GetRand(2);

            // 持ち物持っている可能性があるときだけ
            var item = seed.GetRand(100);

        }

        public static string GenerateIndiv(uint init)
        {
            var seed = ((ulong)init, Xoroshiro128p.FIXSEED);
            seed.Advance();
            seed.Advance();
            var ivs = Enumerable.Range(0, 6).Select(_ => seed.GetRand(32)).ToArray();
            
            return string.Join("-", ivs);
        }
    }
}
