using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPRNG.Xoroshiro128p;

namespace SwShRNGLibrary.OverWorldRNG
{
    public enum Apricorn
    {
        Red,
        Blue,
        Yallow,
        Green,
        Pink,
        White,
        Black,
    }

    [Flags]
    public enum Ball
    {
        None        = 0,

        PokeBall    = 1 << 0, // モンスター
        GreatBall   = 1 << 1, // スーパー
        UltraBall   = 1 << 2, // ハイパー
        MasterBall  = 1 << 3, // マスター

        PremierBall = 1 << 4, // プレミア
        HealBall    = 1 << 5, // ヒール
        NetBall     = 1 << 6, // ネット
        NestBall    = 1 << 7, // ネスト
        DiveBall    = 1 << 8, // ダイブ
        Finsterball = 1 << 9, // ダーク
        TimerBall   = 1 << 10, // タイマー
        QuickBall   = 1 << 11, // クイック
        RepeatBall  = 1 << 12, // リピート
        LuxuryBall  = 1 << 13, // ゴージャス

        FastBall    = 1 << 14, // スピード
        FriendBall  = 1 << 15, // フレンド
        LureBall    = 1 << 16, // ルアー
        LevelBall   = 1 << 17, // レベル
        HeavyBall   = 1 << 18, // ヘビー
        LoveBall    = 1 << 19, // ラブラブ
        MoonBall    = 1 << 20, // ムーン

        DreamBall   = 1 << 21, // ドリーム
        SportBall   = 1 << 22, // コンペ
        SafariBall  = 1 << 23, // サファリ
        BeastBall   = 1 << 24, // ウルトラ

        CherishBall = 1 << 25, // プレシャス

        ShopBalls1      = UltraBall | PremierBall | Finsterball | NetBall,
        ShopBalls2      = HealBall | NestBall | DiveBall | TimerBall | QuickBall | RepeatBall | LuxuryBall,
        ApricornBalls   = FastBall | FriendBall | LureBall | LevelBall | HeavyBall | LoveBall | MoonBall,
        FacilityBalls   = SafariBall | SportBall,
        NFSBalls        = ApricornBalls | FacilityBalls | DreamBall | BeastBall | CherishBall
    }

    public class CramOMaticGenerator
    {
        private readonly int npc;
        public CramOMaticResult Generate((ulong s0, ulong s1) state)
        {
            for (int i = 0; i < npc; i++)
                state.GetRand(91);
            state.Advance();
            state.GetRand(60);

            var itemRoll = (byte)state.GetRand(4);
            var ballRoll = (byte)state.GetRand(100);
            var rare = state.GetRand(1000) == 0;
            var bonus = (ballRoll == 99 || rare) ? state.GetRand(1000) == 0 : state.GetRand(100) == 0;

            return new CramOMaticResult(itemRoll, ballRoll, rare, bonus);
        }

        /// <summary>
        /// npc: 主人公を除いたNPCの数
        /// </summary>
        /// <param name="npc"></param>
        public CramOMaticGenerator(int npc = 20)
            => this.npc = npc + 1;
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

    public static class _CramOMaticExt
    {
        private static readonly Ball[] commonShopBalls =
            new Ball[] { Ball.UltraBall, Ball.NetBall, Ball.UltraBall, Ball.UltraBall, Ball.UltraBall, Ball.PremierBall, Ball.Finsterball };
        private static readonly Ball[] rareShopBalls =
            new Ball[] { Ball.RepeatBall, Ball.DiveBall, Ball.QuickBall, Ball.NestBall, Ball.HealBall, Ball.TimerBall, Ball.LuxuryBall };
        private static readonly Ball[] apricornBalls =
            new Ball[] { Ball.LevelBall, Ball.LureBall, Ball.MoonBall, Ball.FriendBall, Ball.LoveBall, Ball.FastBall, Ball.HeavyBall };
        private static readonly IReadOnlyDictionary<Ball, string> ballJp = new Dictionary<Ball, string>() 
        {
            { Ball.PokeBall, "モンスターボール" },
            { Ball.GreatBall, "スーパーボール" },
            { Ball.UltraBall, "ハイパーボール" },
            { Ball.MasterBall, "マスターボール" },

            { Ball.PremierBall, "プレミアボール" },
            { Ball.HealBall, "ヒールボール" },
            { Ball.NetBall, "ネットボール" },
            { Ball.NestBall, "ネストボール" },
            { Ball.DiveBall, "ダイブボール" },
            { Ball.Finsterball, "ダークボール" },
            { Ball.TimerBall, "タイマーボール" },
            { Ball.QuickBall, "クイックボール" },
            { Ball.RepeatBall, "リピートボール" },
            { Ball.LuxuryBall, "ゴージャスボール" },

            { Ball.FastBall, "スピードボール" },
            { Ball.FriendBall, "フレンドボール" },
            { Ball.LureBall, "ルアーボール" },
            { Ball.LevelBall, "レベルボール" },
            { Ball.HeavyBall, "ヘビーボール" },
            { Ball.LoveBall, "ラブラブボール" },
            { Ball.MoonBall, "ムーンボール" },

            { Ball.DreamBall, "ドリームボール" },
            { Ball.SportBall, "コンペボール" },
            { Ball.SafariBall, "サファリボール" },
            { Ball.BeastBall, "ウルトラボール" },

            { Ball.CherishBall, "プレシャスボール" },

            { Ball.ShopBalls1, "店売りボール1" },
            { Ball.ShopBalls2, "店売りボール2" },
            { Ball.ApricornBalls, "ガンテツボール" },
            { Ball.FacilityBalls, "施設限定ボール" },
            { Ball.NFSBalls, "非売品ボール" },
        };
        private static readonly IReadOnlyList<string> apricornJp = 
            new[] { "あかぼんぐり", "あおぼんぐり", "きぼんぐり", "みどぼんぐり", "ももぼんぐり", "しろぼんぐり", "くろぼんぐり" };

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

        public static string ToJapanese(this Ball ball) => ballJp[ball];
        public static string ToJapanese(this Apricorn apricorn) => apricornJp[(int)apricorn];
    }
}
