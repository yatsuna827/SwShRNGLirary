using System;
using System.Collections.Generic;

namespace SwShRNGLibrary
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

    public static class _CramOMaticExt
    {
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

        public static string ToJapanese(this Ball ball) => ballJp[ball];
        public static string ToJapanese(this Apricorn apricorn) => apricornJp[(int)apricorn];
    }
}
