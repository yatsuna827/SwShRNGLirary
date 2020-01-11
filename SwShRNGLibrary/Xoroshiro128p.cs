using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{

    public class Xoroshiro128p
    {
        private ulong[] seed;
        public ulong GetRand()
        {
            ulong s0 = seed[0];
            ulong s1 = s0 ^ seed[1];
            ulong res = seed[0] + seed[1];

            seed[0] = s0.RotateLeft(24) ^ s1 ^ (s1 << 16);
            seed[1] = s1.RotateLeft(37);

            return res;
        }
        public ulong GetRand(uint num)
        {
            ulong numtwo = getrandpow2(num);

            ulong result;
            do
            {
                result = GetRand() & numtwo;
            } while (result >= num);
            return result;
        }
        private ulong getrandpow2(uint num)
        {
            if ((num & (num - 1)) == 0) return num - 1;

            ulong res = 1;
            while (res < num)
            {
                res <<= 1;
            }
            return res - 1;
        }

        public string GetSeed()
        {
            return $"{seed[0]:X16}{seed[1]:X16}";
        }
        public void Back()
        {
            ulong s0 = seed[0];
            ulong s1 = seed[1];

            seed[0] = (s0 ^ (s1.RotateLeft(27) << 16)).RotateRight(24) ^ s1.RotateLeft(3);
            seed[1] = (s0 ^ (s1.RotateLeft(27) << 16)).RotateRight(24) ^ s1.RotateLeft(3) ^ s1.RotateLeft(27);
        }
        public Xoroshiro128p(ulong s0, ulong s1)
        {
            seed = new ulong[2] { s0, s1 };
        }
        public Xoroshiro128p(ulong s0)
        {
            seed = new ulong[2] { s0, 0x82A2B175229D6A5B };
        }
    }
    static class RotateExtension
    {
        internal static ulong RotateLeft(this ulong num, int n)
        {
            return (num << n) | (num >> (64 - n));
        }
        internal static ulong RotateRight(this ulong num, int n)
        {
            return (num >> n) | (num << (64 - n));
        }
    }
}
