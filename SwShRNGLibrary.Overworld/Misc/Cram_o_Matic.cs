using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPRNG.Xoroshiro128p;

namespace SwShRNGLibrary.Overworld.Misc
{
    public class CramOMaticGenerator : IGeneratable<CramOMaticResult>, IGeneratableEffectful<CramOMaticResult>
    {
        public CramOMaticResult Generate((ulong s0, ulong s1) state)
        {
            var itemRoll = (byte)state.GetRand(4);
            var ballRoll = (byte)state.GetRand(100);
            var rare = state.GetRand(1000) == 0;
            var bonus = (ballRoll == 99 || rare) ? state.GetRand(1000) == 0 : state.GetRand(100) == 0;

            return new CramOMaticResult(itemRoll, ballRoll, rare, bonus);
        }
        public CramOMaticResult Generate(ref (ulong s0, ulong s1) state)
        {
            var itemRoll = (byte)state.GetRand(4);
            var ballRoll = (byte)state.GetRand(100);
            var rare = state.GetRand(1000) == 0;
            var bonus = (ballRoll == 99 || rare) ? state.GetRand(1000) == 0 : state.GetRand(100) == 0;

            return new CramOMaticResult(itemRoll, ballRoll, rare, bonus);
        }
        public CramOMaticResult Generate((ulong s0, ulong s1) state, ref uint index)
        {
            var itemRoll = (byte)state.GetRand(4, ref index);
            var ballRoll = (byte)state.GetRand(100, ref index);
            var rare = state.GetRand(1000, ref index) == 0;
            var bonus = (ballRoll == 99 || rare) ? state.GetRand(1000, ref index) == 0 : state.GetRand(100, ref index) == 0;

            return new CramOMaticResult(itemRoll, ballRoll, rare, bonus);
        }
    }
    public readonly struct CramOMaticResult
    {
        public byte ItemRoll { get; }
        public byte BallRoll { get; }
        public bool Rare { get; }
        public bool Bonus { get; }

        public CramOMaticResult(byte itemRoll, byte ballRoll, bool rare, bool bonus)
        {
            ItemRoll = itemRoll;
            BallRoll = ballRoll;
            Rare = rare;
            Bonus = bonus;
        }
    }

    public static class _CramOMaticResultExt
    {
        private static readonly Ball[] commonShopBalls =
            new Ball[] { Ball.UltraBall, Ball.NetBall, Ball.UltraBall, Ball.UltraBall, Ball.UltraBall, Ball.PremierBall, Ball.Finsterball };
        private static readonly Ball[] rareShopBalls =
            new Ball[] { Ball.RepeatBall, Ball.DiveBall, Ball.QuickBall, Ball.NestBall, Ball.HealBall, Ball.TimerBall, Ball.LuxuryBall };
        private static readonly Ball[] apricornBalls =
            new Ball[] { Ball.LevelBall, Ball.LureBall, Ball.MoonBall, Ball.FriendBall, Ball.LoveBall, Ball.FastBall, Ball.HeavyBall };

        public static Ball FinishedBall(this in CramOMaticResult result)
        {
            // すべて同じぼんぐりならサファリ、2種以上混合ならコンペ
            if (result.Rare)
                return Ball.FacilityBalls;

            if (result.BallRoll < 25) return Ball.PokeBall;
            if (result.BallRoll < 50) return Ball.GreatBall;
            if (result.BallRoll < 75) return Ball.ShopBalls1;
            if (result.BallRoll < 99) return Ball.ShopBalls2;
            return Ball.ApricornBalls;
        }
        public static Ball FinishedBall(this in CramOMaticResult result, Apricorn first, Apricorn second, Apricorn third, Apricorn fourth)
        {
            // すべて同じぼんぐりならサファリ、2種以上混合ならコンペ
            if (result.Rare) 
                return (first == second && second == third && third == fourth) ? Ball.SafariBall : Ball.SportBall;

            // ボールの決定に使われるぼんぐりを決める
            // 判定値と逆順
            var usedApricorn =
                (result.ItemRoll & 1) == 0 ?
                    (result.ItemRoll >> 1) == 0 ? fourth : second
                :
                    (result.ItemRoll >> 1) == 0 ? third : first;

            if (result.BallRoll < 25) return Ball.PokeBall;
            if (result.BallRoll < 50) return Ball.GreatBall;
            if (result.BallRoll < 75) return commonShopBalls[(int)usedApricorn];
            if (result.BallRoll < 99) return rareShopBalls[(int)usedApricorn];
            return apricornBalls[(int)usedApricorn];
        }
        public static int BallCount(this in CramOMaticResult result)
            => result.Bonus ? 5 : 1;
    }
}
