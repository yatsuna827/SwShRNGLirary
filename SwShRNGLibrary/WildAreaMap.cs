using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{
    public class RaidBattleSlot
    {
        public readonly Pokemon.Species pokemon;
        public readonly bool allowHiddenAbility;
        public readonly Gender FixGender = Gender.Genderless; // Genderless == 固定なし.
        public readonly Nature FixNature = Nature.other;
        public readonly bool ForceShiny;
        public readonly int FlawlessIVs;
        public string Label { get; internal set; }

        internal RaidBattleSlot(string name, bool allowHA, int fix, Gender fixGender = Gender.Genderless, Nature fixNature = Nature.other)
        {
            pokemon = Pokemon.GetPokemon(name);
            allowHiddenAbility = allowHA;
            FlawlessIVs = fix;
            FixGender = fixGender;
            FixNature = fixNature;
        }
        internal RaidBattleSlot(string name, string form, bool allowHA, int fix, Gender fixGender = Gender.Genderless, Nature fixNature = Nature.other)
        {
            pokemon = Pokemon.GetPokemon(name, form);
            allowHiddenAbility = allowHA;
            FlawlessIVs = fix;
            FixGender = fixGender;
            FixNature = fixNature;
        }
    }
    public class CommonSlot1 : RaidBattleSlot
    {
        internal CommonSlot1(string name, Rom limited = Rom.empty) : base(name, false, 1)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
        internal CommonSlot1(string name, string form, Rom limited = Rom.empty) : base(name, form, false, 1)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
    }
    public class CommonSlot2 : RaidBattleSlot
    {
        internal CommonSlot2(string name, Rom limited = Rom.empty) : base(name, false, 2)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
        internal CommonSlot2(string name, string form, Rom limited = Rom.empty) : base(name, form, false, 2)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
    }
    public class CommonSlot3 : RaidBattleSlot
    {
        internal CommonSlot3(string name, Rom limited = Rom.empty) : base(name, false, 3)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
        internal CommonSlot3(string name, string form, Rom limited = Rom.empty) : base(name, form, false, 3)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
    }
    public class CommonSlot4 : RaidBattleSlot
    {
        internal CommonSlot4(string name, Rom limited = Rom.empty) : base(name, false, 4)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
        internal CommonSlot4(string name, string form, Rom limited = Rom.empty) : base(name, form, false, 4)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
    }
    public class CommonSlot5 : RaidBattleSlot
    {
        internal CommonSlot5(string name, Rom limited = Rom.empty) : base(name, true, 4)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
        internal CommonSlot5(string name, string form, Rom limited = Rom.empty) : base(name, form, true, 4)
        {
            Label = limited != Rom.empty ? $"({limited.ToKanji()})" : "";
        }
    }

    public class WildAreaMap
    {
        public readonly string MapName;
        public readonly　RaidBattleSlot[][] RaidTable;

        internal WildAreaMap(string name, RaidBattleSlot[][] table)
        {
            MapName = name;
            RaidTable = table;
        }

        public static readonly WildAreaMap[] MapList;
        static WildAreaMap()
        {
            var mapList = new List<WildAreaMap>();

            var dummyTable = new RaidBattleSlot[][] {
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", false, 1), new RaidBattleSlot("ヌオー", false, 2), new RaidBattleSlot("ヌオー", false, 3), new RaidBattleSlot("ヌオー", false, 4), new RaidBattleSlot("ヌオー", true, 4) },
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", false, 1), new RaidBattleSlot("ヌオー", false, 2), new RaidBattleSlot("ヌオー", false, 3), new RaidBattleSlot("ヌオー", false, 4), new RaidBattleSlot("ヌオー", true, 4) },
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", false, 1), new RaidBattleSlot("ヌオー", false, 2), new RaidBattleSlot("ヌオー", false, 3), new RaidBattleSlot("ヌオー", false, 4), new RaidBattleSlot("ヌオー", true, 4) },
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", false, 1), new RaidBattleSlot("ヌオー", false, 2), new RaidBattleSlot("ヌオー", false, 3), new RaidBattleSlot("ヌオー", false, 4), new RaidBattleSlot("ヌオー", true, 4) },
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", false, 1), new RaidBattleSlot("ヌオー", false, 2), new RaidBattleSlot("ヌオー", false, 3), new RaidBattleSlot("ヌオー", false, 4), new RaidBattleSlot("ヌオー", true, 4) },
            };
            var dummyTable2 = new RaidBattleSlot[][] {
                new RaidBattleSlot[] { new RaidBattleSlot("ネクロズマ","たそがれ", false, 1), new RaidBattleSlot("ヒヒダルマ","ガラルダルマ", false, 2), new RaidBattleSlot("バスラオ", false, 3), new RaidBattleSlot("ザマゼンタ", false, 4), new RaidBattleSlot("ザシアン","剣の王", true, 4) },
                new RaidBattleSlot[] { new RaidBattleSlot("メタモン", false, 1), new RaidBattleSlot("メタモン", false, 2), new RaidBattleSlot("メタモン", false, 3), new RaidBattleSlot("メタモン", false, 4), new RaidBattleSlot("メタモン", true, 4) },
                new RaidBattleSlot[] { new RaidBattleSlot("メタモン", false, 1), new RaidBattleSlot("メタモン", false, 2), new RaidBattleSlot("メタモン", false, 3), new RaidBattleSlot("メタモン", false, 4), new RaidBattleSlot("メタモン", true, 4) },
                new RaidBattleSlot[] { new RaidBattleSlot("メタモン", false, 1), new RaidBattleSlot("メタモン", false, 2), new RaidBattleSlot("メタモン", false, 3), new RaidBattleSlot("メタモン", false, 4), new RaidBattleSlot("メタモン", true, 4) },
                new RaidBattleSlot[] { new RaidBattleSlot("メタモン", false, 1), new RaidBattleSlot("メタモン", false, 2), new RaidBattleSlot("メタモン", false, 3), new RaidBattleSlot("メタモン", false, 4), new RaidBattleSlot("メタモン", true, 4) },
            };

            #region Tables
            #region Normal
            var normalA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ホルビー"),
                    new CommonSlot1("ホーホー"),
                    new CommonSlot1("マメパト"),
                    new CommonSlot1("チラーミィ")
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("マメパト"),
                    new CommonSlot1("チラーミィ"),
                    new CommonSlot2("エリキテル"),
                    new CommonSlot2("ヌイコグマ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ヌイコグマ"),
                    new CommonSlot3("ホルード"),
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ケンホロウ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ホルード"),
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ケンホロウ"),
                    new CommonSlot5("エレザード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ケンホロウ"),
                    new CommonSlot5("エレザード"),
                    new CommonSlot5("チラチーノ"),
                    new CommonSlot5("キテルグマ"),
                }
            };
            var normalB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ホシガリス"),
                    new CommonSlot1("ウールー"),
                    new CommonSlot1("ジグザグマ", "ガラル"),
                    new CommonSlot1("ゴンベ")
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ジグザグマ", "ガラル"),
                    new CommonSlot1("ゴンベ"),
                    new CommonSlot2("ヨクバリス"),
                    new CommonSlot2("イエッサン", "♂", Rom.Sword),
                    new CommonSlot2("イエッサン", "♀", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ヨクバリス"),
                    new CommonSlot3("マッスグマ", "ガラル"),
                    new CommonSlot4("ヨクバリス"),
                    new CommonSlot4("バイウールー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("マッスグマ", "ガラル"),
                    new CommonSlot4("ヨクバリス"),
                    new CommonSlot4("バイウールー"),
                    new CommonSlot5("ホルード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヨクバリス"),
                    new CommonSlot4("バイウールー"),
                    new CommonSlot5("ホルード"),
                    new CommonSlot5("カビゴン"),
                    new CommonSlot5("ウォーグル", Rom.Sword),
                    new CommonSlot5("ヤレユータン", Rom.Shield),
                }
            };
            var normalC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ホルビー"),
                    new CommonSlot1("マメパト"),
                    new CommonSlot1("ホシガリス"),
                    new CommonSlot1("イーブイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ホシガリス"),
                    new CommonSlot1("イーブイ"),
                    new CommonSlot2("ハトーボー"),
                    new CommonSlot2("ウールー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ハトーボー"),
                    new CommonSlot2("ウールー"),
                    new CommonSlot4("ケンホロウ"),
                    new CommonSlot4("バイウールー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ケンホロウ"),
                    new CommonSlot4("バイウールー"),
                    new CommonSlot5("ウォーグル", Rom.Sword),
                    new CommonSlot5("ヤレユータン", Rom.Shield),
                    new CommonSlot5("イエッサン","♂", Rom.Sword),
                    new CommonSlot5("イエッサン","♀", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ケンホロウ"),
                    new CommonSlot4("バイウールー"),
                    new CommonSlot5("イーブイ"),
                    new CommonSlot5("カビゴン"),
                    new CommonSlot5("ウォーグル", Rom.Sword),
                    new CommonSlot5("ヤレユータン", Rom.Shield),
                    new CommonSlot5("イエッサン","♂", Rom.Sword),
                    new CommonSlot5("イエッサン","♀", Rom.Shield),
                }
            };
            var normalD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("メタモン"),
                    new CommonSlot2("メタモン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("メタモン"),
                    new CommonSlot3("メタモン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("メタモン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("メタモン"),
                    new CommonSlot5("メタモン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot5("メタモン"),
                }
            };
            #endregion

            #region Fire
            var fireA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤクデ"),
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ダルマッカ", "ガラル", Rom.Sword),
                    new CommonSlot1("クイタラン", Rom.Shield),
                    new CommonSlot2("エンニュート", Rom.Sword),
                    new CommonSlot2("ランプラー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("エンニュート", Rom.Sword),
                    new CommonSlot2("ランプラー", Rom.Shield),
                    new CommonSlot3("ランプラー", Rom.Sword),
                    new CommonSlot3("エンニュート", Rom.Shield),
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ランプラー", Rom.Sword),
                    new CommonSlot3("エンニュート", Rom.Shield),
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("マルヤクデ", Rom.Sword),
                    new CommonSlot5("クイタラン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("マルヤクデ"),
                    new CommonSlot5("クイタラン"),
                    new CommonSlot5("ヒヒダルマ","ガラル", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                }
            };
            var fireB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                    new CommonSlot2("エンニュート", Rom.Sword),
                    new CommonSlot2("ランプラー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("エンニュート", Rom.Sword),
                    new CommonSlot2("ランプラー", Rom.Shield),
                    new CommonSlot3("ランプラー", Rom.Sword),
                    new CommonSlot3("エンニュート", Rom.Shield),
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス", Rom.Sword),
                    new CommonSlot4("エンニュート", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ランプラー", Rom.Sword),
                    new CommonSlot3("エンニュート", Rom.Shield),
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス", Rom.Sword),
                    new CommonSlot4("エンニュート", Rom.Shield),
                    new CommonSlot5("エンニュート", Rom.Sword),
                    new CommonSlot5("コータス", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot5("シャンデラ"),
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス", Rom.Sword),
                    new CommonSlot4("エンニュート", Rom.Shield),
                    new CommonSlot5("エンニュート", Rom.Sword),
                    new CommonSlot5("コータス", Rom.Shield),
                    new CommonSlot5("バクガメス", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                }
            };
            var fireC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤクデ"),
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ダルマッカ", "ガラル", Rom.Sword),
                    new CommonSlot1("コータス", Rom.Shield),
                    new CommonSlot2("エンニュート", Rom.Sword),
                    new CommonSlot2("トロッゴン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("エンニュート", Rom.Sword),
                    new CommonSlot2("トロッゴン", Rom.Shield),
                    new CommonSlot3("トロッゴン", Rom.Sword),
                    new CommonSlot3("エンニュート", Rom.Shield),
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("トロッゴン", Rom.Sword),
                    new CommonSlot3("エンニュート", Rom.Shield),
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("マルヤクデ", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("キュウコン", Rom.Sword),
                    new CommonSlot4("ウインディ", Rom.Shield),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("マルヤクデ"),
                    new CommonSlot5("セキタンザン"),
                    new CommonSlot5("ヒヒダルマ","ガラル", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                }
            };
            var fireD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤクデ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ヒトカゲ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ヒトカゲ"),
                    new CommonSlot2("リザード"),
                    new CommonSlot2("キュウコン", Rom.Sword),
                    new CommonSlot2("クイタラン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("リザード"),
                    new CommonSlot2("キュウコン", Rom.Sword),
                    new CommonSlot2("クイタラン", Rom.Shield),
                    new CommonSlot3("クイタラン"),
                    new CommonSlot4("コータス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クイタラン"),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("エンニュート"),
                    new CommonSlot5("キュウコン", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("コータス"),
                    new CommonSlot5("エンニュート"),
                    new CommonSlot5("キュウコン", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                    new CommonSlot5("マルヤクデ"),
                    new CommonSlot5("リザードン"),
                }
            };
            var fireE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤクデ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot2("トロッゴン"),
                    new CommonSlot2("ランプラー", Rom.Sword),
                    new CommonSlot2("クイタラン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トロッゴン"),
                    new CommonSlot2("ランプラー", Rom.Sword),
                    new CommonSlot2("クイタラン", Rom.Shield),
                    new CommonSlot3("クイタラン", Rom.Sword),
                    new CommonSlot3("ランプラー", Rom.Shield),
                    new CommonSlot4("コータス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クイタラン", Rom.Sword),
                    new CommonSlot3("ランプラー", Rom.Shield),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("キュウコン", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                    new CommonSlot5("シャンデラ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("コータス"),
                    new CommonSlot5("キュウコン", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                    new CommonSlot5("シャンデラ"),
                    new CommonSlot5("セキタンザン"),
                    new CommonSlot5("バクガメス", Rom.Sword),
                    new CommonSlot5("エンニュート", Rom.Shield),
                }
            };
            var fireF = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤクデ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                    new CommonSlot2("トロッゴン"),
                    new CommonSlot2("ランプラー", Rom.Sword),
                    new CommonSlot2("クイタラン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トロッゴン"),
                    new CommonSlot2("ランプラー", Rom.Sword),
                    new CommonSlot2("クイタラン", Rom.Shield),
                    new CommonSlot3("クイタラン", Rom.Sword),
                    new CommonSlot3("ランプラー", Rom.Shield),
                    new CommonSlot4("コータス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クイタラン", Rom.Sword),
                    new CommonSlot3("ランプラー", Rom.Shield),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("シャンデラ"),
                    new CommonSlot5("ウインディ"),
                    new CommonSlot5("キュウコン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("コータス"),
                    new CommonSlot5("シャンデラ"),
                    new CommonSlot5("ウインディ"),
                    new CommonSlot5("キュウコン"),
                    new CommonSlot5("ブースター"),
                }
            };
            var fireG = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤクデ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ヒトカゲ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ヒトカゲ"),
                    new CommonSlot2("リザード"),
                    new CommonSlot2("キュウコン", Rom.Sword),
                    new CommonSlot2("クイタラン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("リザード"),
                    new CommonSlot2("キュウコン", Rom.Sword),
                    new CommonSlot2("クイタラン", Rom.Shield),
                    new CommonSlot3("クイタラン"),
                    new CommonSlot4("コータス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クイタラン"),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("エンニュート"),
                    new CommonSlot5("キュウコン", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("コータス"),
                    new CommonSlot5("エンニュート"),
                    new CommonSlot5("キュウコン", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                    new CommonSlot5("マルヤクデ"),
                    new CommonSlot5("リザードン", "キョダイ"),
                }
            };
            var fireH = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤクデ"),
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot1("ロコン", Rom.Sword),
                    new CommonSlot1("ガーディ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒトモシ"),
                    new CommonSlot1("ヤトウモリ"),
                    new CommonSlot2("トロッゴン"),
                    new CommonSlot2("ランプラー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トロッゴン"),
                    new CommonSlot2("ランプラー"),
                    new CommonSlot3("クイタラン"),
                    new CommonSlot4("コータス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クイタラン"),
                    new CommonSlot4("コータス"),
                    new CommonSlot5("キュウコン", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                    new CommonSlot5("シャンデラ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("コータス"),
                    new CommonSlot5("キュウコン", Rom.Sword),
                    new CommonSlot5("ウインディ", Rom.Shield),
                    new CommonSlot5("シャンデラ"),
                    new CommonSlot5("セキタンザン"),
                    new CommonSlot5("マルヤクデ","キョダイ"),
                }
            };
            #endregion

            #region Water
            var waterA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("コイキング"),
                    new CommonSlot1("タマンタ"),
                    new CommonSlot1("テッポウオ"),
                    new CommonSlot1("チョンチー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("テッポウオ"),
                    new CommonSlot1("チョンチー"),
                    new CommonSlot2("ホエルコ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ホエルコ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                    new CommonSlot3("オクタン"),
                    new CommonSlot4("マンタイン"),
                    new CommonSlot4("ランターン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("オクタン"),
                    new CommonSlot4("マンタイン"),
                    new CommonSlot4("ランターン"),
                    new CommonSlot5("ホエルオー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("マンタイン"),
                    new CommonSlot4("ランターン"),
                    new CommonSlot5("ホエルオー"),
                    new CommonSlot5("ヨワシ"),
                    new CommonSlot5("ギャラドス"),
                }
            };
            var waterB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("カムカメ"),
                    new CommonSlot1("サシカマス"),
                    new CommonSlot1("カラナクシ"),
                    new CommonSlot1("シズクモ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("カラナクシ"),
                    new CommonSlot1("シズクモ"),
                    new CommonSlot2("ホエルコ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ホエルコ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                    new CommonSlot3("ヨワシ"),
                    new CommonSlot4("カジリガメ"),
                    new CommonSlot4("カマスジョー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ヨワシ"),
                    new CommonSlot4("カジリガメ"),
                    new CommonSlot4("カマスジョー"),
                    new CommonSlot5("オニシズクモ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("カジリガメ"),
                    new CommonSlot4("カマスジョー"),
                    new CommonSlot5("オニシズクモ"),
                    new CommonSlot5("トリトドン"),
                    new CommonSlot5("ホエルオー"),
                }
            };
            var waterC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("カムカメ"),
                    new CommonSlot1("ウパー"),
                    new CommonSlot1("オタマロ"),
                    new CommonSlot1("ヘイガニ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("オタマロ"),
                    new CommonSlot1("ヘイガニ"),
                    new CommonSlot2("ガマガル"),
                    new CommonSlot2("シェルダー", Rom.Sword),
                    new CommonSlot1("シェルダー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ガマガル"),
                    new CommonSlot3("カジリガメ"),
                    new CommonSlot4("ヌオー"),
                    new CommonSlot4("ナマコブシ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("カジリガメ"),
                    new CommonSlot4("ヌオー"),
                    new CommonSlot4("ナマコブシ"),
                    new CommonSlot5("パルシェン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヌオー"),
                    new CommonSlot4("ナマコブシ"),
                    new CommonSlot5("パルシェン"),
                    new CommonSlot5("ガマゲロゲ"),
                    new CommonSlot5("シザリガー"),
                }
            };
            var waterD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("オタマロ"),
                    new CommonSlot1("シェルダー"),
                    new CommonSlot1("チョンチー"),
                    new CommonSlot1("ヒドイデ", Rom.Sword),
                    new CommonSlot1("ガマガル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("チョンチー"),
                    new CommonSlot2("サシカマス"),
                    new CommonSlot1("ヒドイデ", Rom.Sword),
                    new CommonSlot1("ガマガル", Rom.Shield),
                    new CommonSlot2("ガマガル", Rom.Sword),
                    new CommonSlot2("ヒドイデ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("サシカマス"),
                    new CommonSlot3("パルシェン", Rom.Sword),
                    new CommonSlot3("ドヒドイデ", Rom.Shield),
                    new CommonSlot4("ヨワシ", Rom.Sword),
                    new CommonSlot4("パルシェン", Rom.Shield),
                    new CommonSlot4("ランターン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("パルシェン", Rom.Sword),
                    new CommonSlot3("ドヒドイデ", Rom.Shield),
                    new CommonSlot4("ヨワシ", Rom.Sword),
                    new CommonSlot4("パルシェン", Rom.Shield),
                    new CommonSlot4("ランターン"),
                    new CommonSlot5("ガマゲロゲ", Rom.Sword),
                    new CommonSlot5("ヨワシ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヨワシ", Rom.Sword),
                    new CommonSlot4("パルシェン", Rom.Shield),
                    new CommonSlot4("ランターン"),
                    new CommonSlot5("ガマゲロゲ"),
                    new CommonSlot5("ドヒドイデ", Rom.Sword),
                    new CommonSlot5("ヨワシ", Rom.Shield),
                }
            };
            var waterE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("カラナクシ"),
                    new CommonSlot1("クラブ"),
                    new CommonSlot1("ヘイガニ"),
                    new CommonSlot1("カムカメ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヘイガニ"),
                    new CommonSlot1("カムカメ"),
                    new CommonSlot2("カメテテ"),
                    new CommonSlot2("ナマコブシ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ナマコブシ"),
                    new CommonSlot3("キングラー"),
                    new CommonSlot4("シザリガー"),
                    new CommonSlot4("ガメノデス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("キングラー"),
                    new CommonSlot4("シザリガー"),
                    new CommonSlot4("ガメノデス"),
                    new CommonSlot5("トリトドン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("シザリガー"),
                    new CommonSlot4("ガメノデス"),
                    new CommonSlot5("トリトドン"),
                    new CommonSlot5("ブルンゲル"),
                    new CommonSlot5("カジリガメ"),
                }
            };
            var waterF = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("コイキング"),
                    new CommonSlot1("ヒンバス"),
                    new CommonSlot1("サシカマス"),
                    new CommonSlot1("カムカメ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("サシカマス"),
                    new CommonSlot1("カムカメ"),
                    new CommonSlot2("ヒドイデ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ヒドイデ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                    new CommonSlot3("ハリーセン"),
                    new CommonSlot4("ドヒドイデ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ハリーセン"),
                    new CommonSlot4("ドヒドイデ"),
                    new CommonSlot5("ナマコブシ"),
                    new CommonSlot5("ギャラドス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ドヒドイデ"),
                    new CommonSlot5("ナマコブシ"),
                    new CommonSlot5("ギャラドス"),
                    new CommonSlot5("ラプラス"),
                    new CommonSlot5("ミロカロス"),
                }
            };
            var waterG = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("タマンタ"),
                    new CommonSlot1("ヘイガニ"),
                    new CommonSlot1("サシカマス"),
                    new CommonSlot1("カムカメ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("サシカマス"),
                    new CommonSlot1("カムカメ"),
                    new CommonSlot2("ヒドイデ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ヒドイデ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                    new CommonSlot3("シザリガー"),
                    new CommonSlot4("ドヒドイデ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("シザリガー"),
                    new CommonSlot4("ドヒドイデ"),
                    new CommonSlot5("ナマコブシ"),
                    new CommonSlot5("マンタイン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ドヒドイデ"),
                    new CommonSlot5("ナマコブシ"),
                    new CommonSlot5("マンタイン"),
                    new CommonSlot5("ラプラス"),
                    new CommonSlot5("シャワーズ"),
                }
            };
            var waterH = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("コイキング"),
                    new CommonSlot1("シズクモ"),
                    new CommonSlot1("ウパー"),
                    new CommonSlot1("ドジョッチ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ウパー"),
                    new CommonSlot1("ドジョッチ"),
                    new CommonSlot2("クラブ"),
                    new CommonSlot2("ヨワシ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("クラブ"),
                    new CommonSlot2("ヨワシ"),
                    new CommonSlot3("キングラー"),
                    new CommonSlot4("ナマズン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("キングラー"),
                    new CommonSlot4("ナマズン"),
                    new CommonSlot5("ハリーセン"),
                    new CommonSlot5("ヌオー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ナマズン"),
                    new CommonSlot5("ハリーセン"),
                    new CommonSlot5("ヌオー"),
                    new CommonSlot5("オニシズクモ"),
                    new CommonSlot5("ギャラドス"),
                }
            };
            var waterI = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("タマンタ"),
                    new CommonSlot1("テッポウオ"),
                    new CommonSlot1("ホエルコ"),
                    new CommonSlot1("カメテテ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ホエルコ"),
                    new CommonSlot1("カメテテ"),
                    new CommonSlot2("クラブ"),
                    new CommonSlot2("ナマコブシ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("クラブ"),
                    new CommonSlot2("ナマコブシ"),
                    new CommonSlot3("キングラー"),
                    new CommonSlot4("バスラオ", "あか", Rom.Sword),
                    new CommonSlot4("バスラオ", "あお", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("キングラー"),
                    new CommonSlot4("バスラオ", "あか", Rom.Sword),
                    new CommonSlot4("バスラオ", "あお", Rom.Shield),
                    new CommonSlot5("ハリーセン"),
                    new CommonSlot5("オクタン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("バスラオ", "あか", Rom.Sword),
                    new CommonSlot4("バスラオ", "あお", Rom.Shield),
                    new CommonSlot5("ハリーセン"),
                    new CommonSlot5("オクタン"),
                    new CommonSlot5("ホエルオー"),
                    new CommonSlot5("マンタイン"),
                }
            };
            var waterJ = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("コイキング"),
                    new CommonSlot1("サシカマス"),
                    new CommonSlot1("カムカメ"),
                    new CommonSlot1("クラブ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("カムカメ"),
                    new CommonSlot1("クラブ"),
                    new CommonSlot2("ナマコブシ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ナマコブシ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                    new CommonSlot3("ハリーセン"),
                    new CommonSlot4("キングラー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ハリーセン"),
                    new CommonSlot4("キングラー"),
                    new CommonSlot5("ヨワシ"),
                    new CommonSlot5("ギャラドス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("キングラー"),
                    new CommonSlot5("ヨワシ"),
                    new CommonSlot5("ギャラドス"),
                    new CommonSlot5("トリトドン"),
                    new CommonSlot5("カジリガメ", "キョダイ"),
                }
            };
            var waterK = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヘイガニ"),
                    new CommonSlot1("クラブ"),
                    new CommonSlot1("サシカマス"),
                    new CommonSlot1("カムカメ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("サシカマス"),
                    new CommonSlot1("カムカメ"),
                    new CommonSlot2("ヒドイデ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                    new CommonSlot5("キングラー", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ヒドイデ"),
                    new CommonSlot2("バスラオ", "あか", Rom.Sword),
                    new CommonSlot2("バスラオ", "あお", Rom.Shield),
                    new CommonSlot3("シザリガー"),
                    new CommonSlot4("ドヒドイデ"),
                    new CommonSlot5("キングラー", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("シザリガー"),
                    new CommonSlot4("ドヒドイデ"),
                    new CommonSlot5("ナマコブシ"),
                    new CommonSlot5("ギャラドス"),
                    new CommonSlot5("キングラー", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ドヒドイデ"),
                    new CommonSlot5("ナマコブシ"),
                    new CommonSlot5("ギャラドス"),
                    new CommonSlot5("ラプラス"),
                    new CommonSlot5("キングラー", "キョダイ"),
                }
            };
            #endregion

            #region Grass
            var grassA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("スボミー"),
                    new CommonSlot1("アマカジ"),
                    new CommonSlot1("ナゾノクサ"),
                    new CommonSlot1("タネボー", Rom.Sword),
                    new CommonSlot1("ハスボー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("アマカジ"),
                    new CommonSlot1("ナゾノクサ"),
                    new CommonSlot2("コノハナ", Rom.Sword),
                    new CommonSlot2("ハスブレロ", Rom.Shield),
                    new CommonSlot3("ロゼリア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ロゼリア"),
                    new CommonSlot3("クサイハナ"),
                    new CommonSlot4("アママイコ"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クサイハナ"),
                    new CommonSlot4("アママイコ"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                    new CommonSlot5("アマージョ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("アママイコ"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                    new CommonSlot5("アマージョ"),
                    new CommonSlot5("ラフレシア"),
                    new CommonSlot5("キレイハナ"),
                }
            };
            var grassB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("スボミー"),
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("モンメン"),
                    new CommonSlot1("カジッチュ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("モンメン"),
                    new CommonSlot1("カジッチュ"),
                    new CommonSlot2("チェリンボ"),
                    new CommonSlot2("ロゼリア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("チェリンボ"),
                    new CommonSlot2("ロゼリア"),
                    new CommonSlot3("テッシード"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot4("チェリム"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("テッシード"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot4("チェリム"),
                    new CommonSlot5("ワタシラガ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot4("チェリム"),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("エルフーン"),
                    new CommonSlot5("アップリュー", Rom.Sword),
                    new CommonSlot5("タルップル", Rom.Shield),
                }
            };
            var grassC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("バケッチャ", "小"),
                    new CommonSlot1("ボクレー"),
                    new CommonSlot1("バケッチャ", "普通"),
                    new CommonSlot1("ネマシュ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("バケッチャ", "普通"),
                    new CommonSlot1("ネマシュ"),
                    new CommonSlot2("バケッチャ", "大"),
                    new CommonSlot2("ロゼリア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ロゼリア"),
                    new CommonSlot3("マシェード"),
                    new CommonSlot4("マラカッチ"),
                    new CommonSlot4("オーロット"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("マシェード"),
                    new CommonSlot4("マラカッチ"),
                    new CommonSlot4("オーロット"),
                    new CommonSlot5("パンプジン", "普通"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("マラカッチ"),
                    new CommonSlot4("オーロット"),
                    new CommonSlot5("パンプジン", "普通"),
                    new CommonSlot5("ダダリン"),
                    new CommonSlot5("バケッチャ", "特大"),
                }
            };
            var grassD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("スボミー"),
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("テッシード"),
                    new CommonSlot1("タネボー", Rom.Sword),
                    new CommonSlot1("ハスボー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("テッシード"),
                    new CommonSlot2("カジッチュ"),
                    new CommonSlot2("コノハナ", Rom.Sword),
                    new CommonSlot2("ハスブレロ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("カジッチュ"),
                    new CommonSlot2("コノハナ", Rom.Sword),
                    new CommonSlot2("ハスブレロ", Rom.Shield),
                    new CommonSlot4("ロゼリア"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ロゼリア"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("ナットレイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ロゼリア"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("ナットレイ"),
                    new CommonSlot5("ロズレイド"),
                    new CommonSlot5("アップリュー", Rom.Sword),
                    new CommonSlot5("タルップル", Rom.Shield),
                }
            };
            var grassE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("チェリンボ"),
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("モンメン"),
                    new CommonSlot1("タネボー", Rom.Sword),
                    new CommonSlot1("ハスボー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("モンメン"),
                    new CommonSlot2("ネマシュ"),
                    new CommonSlot2("コノハナ", Rom.Sword),
                    new CommonSlot2("ハスブレロ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ネマシュ"),
                    new CommonSlot2("コノハナ", Rom.Sword),
                    new CommonSlot2("ハスブレロ", Rom.Shield),
                    new CommonSlot4("チェリム"),
                    new CommonSlot4("マシェード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("チェリム"),
                    new CommonSlot4("マシェード"),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("エルフーン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("チェリム"),
                    new CommonSlot4("マシェード"),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("エルフーン"),
                    new CommonSlot5("ダダリン"),
                    new CommonSlot5("ダーテング", Rom.Sword),
                    new CommonSlot5("ルンパッパ", Rom.Shield),
                }
            };
            var grassF = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("チェリンボ"),
                    new CommonSlot1("アマカジ"),
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("モンメン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("モンメン"),
                    new CommonSlot2("アママイコ"),
                    new CommonSlot2("テッシード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("アママイコ"),
                    new CommonSlot2("テッシード"),
                    new CommonSlot4("チェリム"),
                    new CommonSlot4("ナットレイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("チェリム"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("アマージョ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("チェリム"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("アマージョ"),
                    new CommonSlot5("エルフーン"),
                    new CommonSlot5("リーフィア"),
                }
            };
            var grassG = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("スボミー"),
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("テッシード"),
                    new CommonSlot1("タネボー", Rom.Sword),
                    new CommonSlot1("ハスボー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒメンカ"),
                    new CommonSlot1("テッシード"),
                    new CommonSlot2("カジッチュ"),
                    new CommonSlot2("コノハナ", Rom.Sword),
                    new CommonSlot2("ハスブレロ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("カジッチュ"),
                    new CommonSlot2("コノハナ", Rom.Sword),
                    new CommonSlot2("ハスブレロ", Rom.Shield),
                    new CommonSlot4("ロゼリア"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ロゼリア"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("ナットレイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ロゼリア"),
                    new CommonSlot4("ダーテング", Rom.Sword),
                    new CommonSlot4("ルンパッパ", Rom.Shield),
                    new CommonSlot5("ワタシラガ"),
                    new CommonSlot5("ナットレイ"),
                    new CommonSlot5("ロズレイド"),
                    new CommonSlot5("アップリュー", "キョダイ", Rom.Sword),
                    new CommonSlot5("タルップル", "キョダイ", Rom.Shield),
                }
            };
            #endregion

            #region Electric
            var electricA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ピチュー"),
                    new CommonSlot1("ラクライ"),
                    new CommonSlot1("バチュル"),
                    new CommonSlot1("チョンチー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("バチュル"),
                    new CommonSlot1("チョンチー"),
                    new CommonSlot2("デンヂムシ"),
                    new CommonSlot3("ピカチュウ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ピカチュウ"),
                    new CommonSlot4("ランターン"),
                    new CommonSlot4("ライボルト"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ピカチュウ"),
                    new CommonSlot4("ランターン"),
                    new CommonSlot4("ライボルト"),
                    new CommonSlot5("デンチュラ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ランターン"),
                    new CommonSlot4("ライボルト"),
                    new CommonSlot5("デンチュラ"),
                    new CommonSlot5("クワガノン"),
                    new CommonSlot5("ライチュウ"),
                }
            };
            var electricB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ワンパチ"),
                    new CommonSlot1("エリキテル"),
                    new CommonSlot1("エレズン"),
                    new CommonSlot1("チョンチー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("エレズン"),
                    new CommonSlot1("チョンチー"),
                    new CommonSlot2("ピカチュウ"),
                    new CommonSlot3("ランターン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ランターン"),
                    new CommonSlot3("パルスワン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot4("エレザード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("パルスワン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot4("エレザード"),
                    new CommonSlot5("バチンウニ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot4("エレザード"),
                    new CommonSlot5("バチンウニ"),
                    new CommonSlot5("モルペコ"),
                    new CommonSlot5("トゲデマル"),
                }
            };
            var electricC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ワンパチ"),
                    new CommonSlot1("エレズン"),
                    new CommonSlot1("ピカチュウ"),
                    new CommonSlot1("バチュル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ピカチュウ"),
                    new CommonSlot1("バチュル"),
                    new CommonSlot2("チョンチー"),
                    new CommonSlot2("ランターン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("チョンチー"),
                    new CommonSlot2("ランターン"),
                    new CommonSlot4("パルスワン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("パルスワン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot5("バチンウニ"),
                    new CommonSlot5("デンチュラ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("パルスワン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot5("バチンウニ"),
                    new CommonSlot5("デンチュラ"),
                    new CommonSlot5("トゲデマル"),
                    new CommonSlot5("モルペコ"),
                }
            };
            var electricD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ピチュー"),
                    new CommonSlot1("ラクライ"),
                    new CommonSlot1("エレズン"),
                    new CommonSlot1("エリキテル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("エレズン"),
                    new CommonSlot1("エリキテル"),
                    new CommonSlot2("バチュル"),
                    new CommonSlot2("ピカチュウ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("バチュル"),
                    new CommonSlot2("ピカチュウ"),
                    new CommonSlot4("ピカチュウ"),
                    new CommonSlot4("ロトム", "カット"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ピカチュウ"),
                    new CommonSlot4("ロトム", "カット"),
                    new CommonSlot5("ロトム", "フロスト"),
                    new CommonSlot5("ロトム", "スピン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ピカチュウ"),
                    new CommonSlot4("ロトム", "カット"),
                    new CommonSlot5("ロトム", "フロスト"),
                    new CommonSlot5("ロトム", "スピン"),
                    new CommonSlot5("ロトム", "ウォッシュ"),
                    new CommonSlot5("ロトム", "ヒート"),
                }
            };
            var electricE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ワンパチ"),
                    new CommonSlot1("エレズン"),
                    new CommonSlot1("ピカチュウ"),
                    new CommonSlot1("エリキテル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ピカチュウ"),
                    new CommonSlot1("エリキテル"),
                    new CommonSlot2("チョンチー"),
                    new CommonSlot2("ランターン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("チョンチー"),
                    new CommonSlot2("ランターン"),
                    new CommonSlot4("パルスワン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("パルスワン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot5("エレザード"),
                    new CommonSlot5("クワガノン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("パルスワン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot5("エレザード"),
                    new CommonSlot5("クワガノン"),
                    new CommonSlot5("ピカチュウ"),
                    new CommonSlot5("サンダース"),
                }
            };
            #endregion

            #region Electric
            var rockA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ウソハチ"),
                    new CommonSlot1("イシズマイ"),
                    new CommonSlot1("カメテテ"),
                    new CommonSlot1("ダンゴロ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("イシズマイ"),
                    new CommonSlot1("カメテテ", Rom.Sword),
                    new CommonSlot1("ダンゴロ", Rom.Shield),
                    new CommonSlot2("サイホーン"),
                    new CommonSlot2("ガントル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("サイホーン"),
                    new CommonSlot2("ガントル"),
                    new CommonSlot3("ガメノデス"),
                    new CommonSlot4("サイドン"),
                    new CommonSlot4("ウソッキー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ガメノデス"),
                    new CommonSlot4("サイドン"),
                    new CommonSlot4("ウソッキー"),
                    new CommonSlot5("イワパレス", Rom.Sword),
                    new CommonSlot5("ギガイアス", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("サイドン"),
                    new CommonSlot4("ウソッキー"),
                    new CommonSlot5("イワパレス"),
                    new CommonSlot5("ギガイアス"),
                    new CommonSlot5("ツボツボ"),
                }
            };
            var rockB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("タンドン"),
                    new CommonSlot1("カメテテ"),
                    new CommonSlot1("ダンゴロ"),
                    new CommonSlot1("イシズマイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("カメテテ"),
                    new CommonSlot1("イシズマイ", Rom.Sword),
                    new CommonSlot1("ダンゴロ", Rom.Shield),
                    new CommonSlot1("トロッゴン"),
                    new CommonSlot2("ガントル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ガントル"),
                    new CommonSlot3("イワパレス"),
                    new CommonSlot3("イワーク", Rom.Sword),
                    new CommonSlot3("セキタンザン", Rom.Shield),
                    new CommonSlot4("ガメノデス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("イワパレス"),
                    new CommonSlot4("ガメノデス"),
                    new CommonSlot4("イワーク", Rom.Sword),
                    new CommonSlot4("セキタンザン", Rom.Shield),
                    new CommonSlot5("セキタンザン", Rom.Sword),
                    new CommonSlot5("ギガイアス", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ガメノデス"),
                    new CommonSlot4("イワーク", Rom.Sword),
                    new CommonSlot4("セキタンザン", Rom.Shield),
                    new CommonSlot5("ギガイアス"),
                    new CommonSlot5("ドサイドン"),
                    new CommonSlot5("セキタンザン", Rom.Sword),
                    new CommonSlot5("イワーク", Rom.Shield),
                }
            };
            var rockC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("イシズマイ"),
                    new CommonSlot1("ウソハチ"),
                    new CommonSlot1("タンドン"),
                    new CommonSlot1("カメテテ", Rom.Sword),
                    new CommonSlot1("ダンゴロ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("タンドン"),
                    new CommonSlot1("カメテテ", Rom.Sword),
                    new CommonSlot1("ダンゴロ", Rom.Shield),
                    new CommonSlot2("トロッゴン"),
                    new CommonSlot2("ウソッキー", Rom.Sword),
                    new CommonSlot2("ヨーギラス", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トロッゴン"),
                    new CommonSlot2("ウソッキー", Rom.Sword),
                    new CommonSlot2("ヨーギラス", Rom.Shield),
                    new CommonSlot3("イワーク"),
                    new CommonSlot3("ガメノデス", Rom.Sword),
                    new CommonSlot3("サナギラス", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ガメノデス", Rom.Sword),
                    new CommonSlot3("サナギラス", Rom.Shield),
                    new CommonSlot4("イワーク"),
                    new CommonSlot5("セキタンザン"),
                    new CommonSlot5("イワパレス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("イワーク"),
                    new CommonSlot5("イワパレス"),
                    new CommonSlot5("ハガネール"),
                    new CommonSlot5("イシヘンジン", Rom.Sword),
                    new CommonSlot5("バンギラス", Rom.Shield),
                }
            };
            var rockD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("イシズマイ", Rom.Sword),
                    new CommonSlot1("ウソハチ", Rom.Sword),
                    new CommonSlot1("タンドン", Rom.Sword),
                    new CommonSlot1("カメテテ", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("タンドン", Rom.Sword),
                    new CommonSlot1("カメテテ", Rom.Sword),
                    new CommonSlot2("トロッゴン", Rom.Sword),
                    new CommonSlot2("ウソッキー", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トロッゴン", Rom.Sword),
                    new CommonSlot2("ウソッキー", Rom.Sword),
                    new CommonSlot3("ガメノデス", Rom.Sword),
                    new CommonSlot4("イワーク", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ガメノデス", Rom.Sword),
                    new CommonSlot4("イワーク", Rom.Sword),
                    new CommonSlot5("イワパレス", Rom.Sword),
                    new CommonSlot5("ハガネール", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("イワーク", Rom.Sword),
                    new CommonSlot5("イワパレス", Rom.Sword),
                    new CommonSlot5("ハガネール", Rom.Sword),
                    new CommonSlot5("イシヘンジン", Rom.Sword),
                    new CommonSlot5("セキタンザン", "キョダイ", Rom.Sword),
                }
            };
            #endregion

            #region Electric
            var groundA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ディグダ"),
                    new CommonSlot1("ドロバンコ"),
                    new CommonSlot1("ツチニン"),
                    new CommonSlot1("モグリュー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ツチニン"),
                    new CommonSlot1("モグリュー"),
                    new CommonSlot1("イワーク"),
                    new CommonSlot2("ドジョッチ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ドジョッチ"),
                    new CommonSlot3("ハガネール"),
                    new CommonSlot4("ナマズン"),
                    new CommonSlot4("ホルード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ハガネール"),
                    new CommonSlot4("ナマズン"),
                    new CommonSlot4("ホルード"),
                    new CommonSlot5("ダグトリオ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ナマズン"),
                    new CommonSlot4("ホルード"),
                    new CommonSlot5("ダグトリオ"),
                    new CommonSlot5("ドリュウズ"),
                    new CommonSlot5("バンバドロ"),
                }
            };
            var groundB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("スナヘビ"),
                    new CommonSlot1("デスマス", "ガラル"),
                    new CommonSlot1("ヒポポタス"),
                    new CommonSlot1("ウリムー", Rom.Sword),
                    new CommonSlot1("ナックラー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒポポタス"),
                    new CommonSlot1("ウリムー"),
                    new CommonSlot1("ナックラー"),
                    new CommonSlot2("イノムー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("イノムー"),
                    new CommonSlot3("ビブラーバ", Rom.Sword),
                    new CommonSlot3("マッギョ", "ガラル", Rom.Shield),
                    new CommonSlot4("マッギョ", "ガラル", Rom.Sword),
                    new CommonSlot4("ビブラーバ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ビブラーバ", Rom.Sword),
                    new CommonSlot3("マッギョ", "ガラル", Rom.Shield),
                    new CommonSlot4("マッギョ", "ガラル", Rom.Sword),
                    new CommonSlot4("ビブラーバ", Rom.Shield),
                    new CommonSlot5("カバルドン", Rom.Sword),
                    new CommonSlot5("フライゴン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("マッギョ", "ガラル", Rom.Sword),
                    new CommonSlot4("ビブラーバ", Rom.Shield),
                    new CommonSlot4("デスバーン"),
                    new CommonSlot5("サダイジャ"),
                    new CommonSlot5("カバルドン"),
                    new CommonSlot5("フライゴン"),
                }
            };
            var groundC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ウパー"),
                    new CommonSlot1("ドジョッチ"),
                    new CommonSlot1("デスマス", "ガラル"),
                    new CommonSlot1("ゴビット"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("デスマス", "ガラル"),
                    new CommonSlot1("ゴビット"),
                    new CommonSlot2("ガマガル"),
                    new CommonSlot2("ヌオー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ガマガル"),
                    new CommonSlot2("ヌオー"),
                    new CommonSlot3("マッギョ", "ガラル"),
                    new CommonSlot4("ゴルーグ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("マッギョ", "ガラル"),
                    new CommonSlot4("ゴルーグ"),
                    new CommonSlot5("トリトドン"),
                    new CommonSlot5("ガマゲロゲ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ゴルーグ"),
                    new CommonSlot5("トリトドン"),
                    new CommonSlot5("ガマゲロゲ"),
                    new CommonSlot5("デスバーン"),
                    new CommonSlot5("ドサイドン"),
                }
            };
            var groundD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ウパー"),
                    new CommonSlot1("ドジョッチ"),
                    new CommonSlot1("デスマス", "ガラル"),
                    new CommonSlot1("ゴビット"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("デスマス", "ガラル"),
                    new CommonSlot1("ゴビット"),
                    new CommonSlot2("ガマガル"),
                    new CommonSlot2("ヌオー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ガマガル"),
                    new CommonSlot2("ヌオー"),
                    new CommonSlot3("マッギョ", "ガラル"),
                    new CommonSlot4("ゴルーグ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("マッギョ", "ガラル"),
                    new CommonSlot4("ゴルーグ"),
                    new CommonSlot5("トリトドン"),
                    new CommonSlot5("ガマゲロゲ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ゴルーグ"),
                    new CommonSlot5("トリトドン"),
                    new CommonSlot5("ガマゲロゲ"),
                    new CommonSlot5("ドサイドン"),
                    new CommonSlot5("サダイジャ", "キョダイ"),
                }
            };
            #endregion

            #region Electric
            var iceA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("バニプッチ"),
                    new CommonSlot1("ウリムー"),
                    new CommonSlot1("ユキカブリ"),
                    new CommonSlot1("カチコール"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ユキカブリ"),
                    new CommonSlot1("カチコール"),
                    new CommonSlot1("デリバード"),
                    new CommonSlot2("バニリッチ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("バニリッチ"),
                    new CommonSlot3("イノムー"),
                    new CommonSlot4("クレベース"),
                    new CommonSlot4("ユキノオー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("イノムー"),
                    new CommonSlot4("クレベース"),
                    new CommonSlot4("ユキノオー"),
                    new CommonSlot5("パルシェン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("クレベース"),
                    new CommonSlot4("ユキノオー"),
                    new CommonSlot5("パルシェン"),
                    new CommonSlot5("バイバニラ"),
                    new CommonSlot5("ラプラス"),
                }
            };
            var iceB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ウリムー"),
                    new CommonSlot1("クマシュン"),
                    new CommonSlot1("ユキハミ"),
                    new CommonSlot1("ニューラ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ユキハミ"),
                    new CommonSlot1("ニューラ"),
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot2("イノムー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("イノムー"),
                    new CommonSlot3("パルシェン"),
                    new CommonSlot4("バリコオル"),
                    new CommonSlot4("ツンベアー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("パルシェン"),
                    new CommonSlot4("バリコオル"),
                    new CommonSlot4("ツンベアー"),
                    new CommonSlot5("マンムー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("バリコオル"),
                    new CommonSlot4("ツンベアー"),
                    new CommonSlot5("マンムー"),
                    new CommonSlot5("モスノウ"),
                    new CommonSlot5("マニューラ"),
                }
            };
            var iceC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ユキワラシ"),
                    new CommonSlot1("ユキハミ"),
                    new CommonSlot1("ニューラ"),
                    new CommonSlot1("ダルマッカ", "ガラル", Rom.Sword),
                    new CommonSlot1("デリバード", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ニューラ"),
                    new CommonSlot1("ダルマッカ", "ガラル", Rom.Sword),
                    new CommonSlot1("デリバード", Rom.Shield),
                    new CommonSlot2("バリヤード", "ガラル"),
                    new CommonSlot3("ユキカブリ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ユキカブリ"),
                    new CommonSlot3("ユキノオー"),
                    new CommonSlot4("バリコオル"),
                    new CommonSlot4("オニゴーリ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ユキノオー"),
                    new CommonSlot4("バリコオル"),
                    new CommonSlot4("オニゴーリ"),
                    new CommonSlot5("モスノウ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("バリコオル"),
                    new CommonSlot4("オニゴーリ"),
                    new CommonSlot5("モスノウ"),
                    new CommonSlot5("ユキメノコ"),
                    new CommonSlot5("ヒヒダルマ", "ガラル", Rom.Sword),
                    new CommonSlot5("コオリッポ", Rom.Shield),
                }
            };
            var iceD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("バニプッチ"),
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot1("カチコール"),
                    new CommonSlot1("ダルマッカ", "ガラル", Rom.Sword),
                    new CommonSlot1("クマシュン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot1("カチコール"),
                    new CommonSlot2("ユキワラシ"),
                    new CommonSlot2("デリバード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ユキワラシ"),
                    new CommonSlot2("デリバード"),
                    new CommonSlot3("クレベース"),
                    new CommonSlot4("オニゴーリ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クレベース"),
                    new CommonSlot4("オニゴーリ"),
                    new CommonSlot5("バイバニラ"),
                    new CommonSlot5("バリコオル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("オニゴーリ"),
                    new CommonSlot5("バイバニラ"),
                    new CommonSlot5("バリコオル"),
                    new CommonSlot5("ラプラス"),
                    new CommonSlot5("ヒヒダルマ", "ガラル", Rom.Sword),
                    new CommonSlot5("コオリッポ", Rom.Shield),
                }
            };
            var iceE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("バニプッチ"),
                    new CommonSlot1("ユキハミ"),
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot1("カチコール"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot1("カチコール"),
                    new CommonSlot2("ユキワラシ"),
                    new CommonSlot2("バニリッチ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ユキワラシ"),
                    new CommonSlot2("バニリッチ"),
                    new CommonSlot3("クレベース"),
                    new CommonSlot4("モスノウ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クレベース"),
                    new CommonSlot4("モスノウ"),
                    new CommonSlot5("バイバニラ"),
                    new CommonSlot5("バリコオル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("モスノウ"),
                    new CommonSlot5("バイバニラ"),
                    new CommonSlot5("バリコオル"),
                    new CommonSlot5("ラプラス"),
                    new CommonSlot5("ユキメノコ"),
                    new CommonSlot5("グレイシア"),
                }
            };
            var iceF = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("バニプッチ", Rom.Shield),
                    new CommonSlot1("クマシュン", Rom.Shield),
                    new CommonSlot1("バリヤード", "ガラル", Rom.Shield),
                    new CommonSlot1("カチコール", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("バリヤード", "ガラル", Rom.Shield),
                    new CommonSlot1("カチコール", Rom.Shield),
                    new CommonSlot2("ユキワラシ", Rom.Shield),
                    new CommonSlot2("デリバード", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ユキワラシ", Rom.Shield),
                    new CommonSlot2("デリバード", Rom.Shield),
                    new CommonSlot3("クレベース", Rom.Shield),
                    new CommonSlot4("オニゴーリ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("クレベース", Rom.Shield),
                    new CommonSlot4("オニゴーリ", Rom.Shield),
                    new CommonSlot5("バイバニラ", Rom.Shield),
                    new CommonSlot5("バリコオル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("オニゴーリ", Rom.Shield),
                    new CommonSlot5("バイバニラ", Rom.Shield),
                    new CommonSlot5("バリコオル", Rom.Shield),
                    new CommonSlot5("コオリッポ", Rom.Shield),
                    new CommonSlot5("ラプラス", "キョダイ", Rom.Shield),
                }
            };
            #endregion

            #region Electric
            var poisonA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("スカンプー"),
                    new CommonSlot1("ヤブクロン"),
                    new CommonSlot1("スコルピ"),
                    new CommonSlot1("ヒドイデ", Rom.Sword),
                    new CommonSlot1("ナゾノクサ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("スコルピ"),
                    new CommonSlot1("ヒドイデ", Rom.Sword),
                    new CommonSlot1("ナゾノクサ", Rom.Shield),
                    new CommonSlot2("ロゼリア"),
                    new CommonSlot2("ナゾノクサ", Rom.Sword),
                    new CommonSlot2("ヒドイデ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ロゼリア"),
                    new CommonSlot3("ハリーセン"),
                    new CommonSlot4("ドラピオン"),
                    new CommonSlot4("ラフレシア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ハリーセン"),
                    new CommonSlot4("ドラピオン"),
                    new CommonSlot4("ラフレシア"),
                    new CommonSlot5("ドヒドイデ", Rom.Sword),
                    new CommonSlot5("スカタンク", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ドラピオン"),
                    new CommonSlot4("ラフレシア"),
                    new CommonSlot5("ドヒドイデ"),
                    new CommonSlot5("スカタンク"),
                    new CommonSlot5("ダストダス"),
                }
            };
            var poisonB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("エレズン"),
                    new CommonSlot1("ゴース"),
                    new CommonSlot1("スコルピ"),
                    new CommonSlot1("ナゾノクサ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("スコルピ"),
                    new CommonSlot1("ナゾノクサ"),
                    new CommonSlot2("クサイハナ"),
                    new CommonSlot2("ゴースト"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ゴースト"),
                    new CommonSlot3("ドガース"),
                    new CommonSlot4("ハリーセン"),
                    new CommonSlot4("ラフレシア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ドガース"),
                    new CommonSlot4("ハリーセン"),
                    new CommonSlot4("ラフレシア"),
                    new CommonSlot5("ロゼリア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ハリーセン"),
                    new CommonSlot4("ラフレシア"),
                    new CommonSlot5("ロゼリア"),
                    new CommonSlot5("ストリンダー", "ハイ"),
                    new CommonSlot5("マタドガス", "ガラル"),
                }
            };
            var poisonC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("スカンプー"),
                    new CommonSlot1("ヤブクロン"),
                    new CommonSlot1("スコルピ"),
                    new CommonSlot1("ドガース"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("スコルピ"),
                    new CommonSlot1("ドガース"),
                    new CommonSlot2("エレズン"),
                    new CommonSlot2("ヒドイデ", Rom.Sword),
                    new CommonSlot2("ヤトウモリ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("エレズン"),
                    new CommonSlot2("ヒドイデ", Rom.Sword),
                    new CommonSlot2("ヤトウモリ", Rom.Shield),
                    new CommonSlot4("ダストダス"),
                    new CommonSlot4("ドラピオン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ダストダス"),
                    new CommonSlot4("ドラピオン"),
                    new CommonSlot5("ストリンダー", "ハイ"),
                    new CommonSlot5("スカタンク"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ダストダス"),
                    new CommonSlot4("ドラピオン"),
                    new CommonSlot5("ストリンダー", "ハイ"),
                    new CommonSlot5("スカタンク"),
                    new CommonSlot5("マタドガス", "ガラル"),
                    new CommonSlot5("ドヒドイデ", Rom.Sword),
                    new CommonSlot5("エンニュート", Rom.Shield),
                }
            };
            var poisonD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("スカンプー"),
                    new CommonSlot1("ヤブクロン"),
                    new CommonSlot1("スコルピ"),
                    new CommonSlot1("ドガース"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("スコルピ"),
                    new CommonSlot1("ドガース"),
                    new CommonSlot2("エレズン"),
                    new CommonSlot2("ヒドイデ", Rom.Sword),
                    new CommonSlot2("ヤトウモリ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("エレズン"),
                    new CommonSlot2("ヒドイデ", Rom.Sword),
                    new CommonSlot2("ヤトウモリ", Rom.Shield),
                    new CommonSlot3("ドラピオン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ドラピオン"),
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot5("スカタンク"),
                    new CommonSlot5("マタドガス", "ガラル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ストリンダー", "ハイ"),
                    new CommonSlot5("スカタンク"),
                    new CommonSlot5("マタドガス", "ガラル"),
                    new CommonSlot4("ダストダス", "キョダイ"),
                    new CommonSlot5("ドヒドイデ", Rom.Sword),
                    new CommonSlot5("エンニュート", Rom.Shield),
                }
            };
            #endregion

            #region Electric
            var psychicA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ラルトス"),
                    new CommonSlot1("ムンナ"),
                    new CommonSlot1("ニャスパー"),
                    new CommonSlot1("リグレー"),
                    new CommonSlot1("ゴチム", Rom.Sword),
                    new CommonSlot1("ユニラン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ニャスパー"),
                    new CommonSlot1("リグレー"),
                    new CommonSlot1("ゴチム", Rom.Sword),
                    new CommonSlot1("ユニラン", Rom.Shield),
                    new CommonSlot2("キルリア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("キルリア"),
                    new CommonSlot3("ニャオニクス", "♂", Rom.Sword),
                    new CommonSlot3("ニャオニクス", "♀", Rom.Shield),
                    new CommonSlot3("ゴチミル", Rom.Sword),
                    new CommonSlot3("ダブラン", Rom.Shield),
                    new CommonSlot4("ムシャーナ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ニャオニクス", "♂", Rom.Sword),
                    new CommonSlot3("ニャオニクス", "♀", Rom.Shield),
                    new CommonSlot3("ゴチミル", Rom.Sword),
                    new CommonSlot3("ダブラン", Rom.Shield),
                    new CommonSlot4("ムシャーナ"),
                    new CommonSlot5("ゴチルゼル", Rom.Sword),
                    new CommonSlot5("ランクルス", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ムシャーナ"),
                    new CommonSlot5("ゴチルゼル", Rom.Sword),
                    new CommonSlot5("ランクルス", Rom.Shield),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("ソルロック", Rom.Sword),
                    new CommonSlot5("ルナトーン", Rom.Shield),
                }
            };
            var psychicB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("マネネ"),
                    new CommonSlot1("サッチムシ"),
                    new CommonSlot1("ネイティ", Rom.Sword),
                    new CommonSlot1("ミブリム", Rom.Sword),
                    new CommonSlot2("ネイティ", Rom.Shield),
                    new CommonSlot2("ミブリム", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ネイティ", Rom.Sword),
                    new CommonSlot2("ネイティ", Rom.Shield),
                    new CommonSlot1("ミブリム", Rom.Sword),
                    new CommonSlot2("ミブリム", Rom.Shield),
                    new CommonSlot2("テブリム", Rom.Sword),
                    new CommonSlot3("テブリム", Rom.Shield),
                    new CommonSlot2("レドームシ", Rom.Sword),
                    new CommonSlot2("ポニータ", "ガラル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("テブリム", Rom.Sword),
                    new CommonSlot3("テブリム", Rom.Shield),
                    new CommonSlot3("シンボラー"),
                    new CommonSlot3("ネイティオ"),
                    new CommonSlot4("イエッサン", "♂", Rom.Sword),
                    new CommonSlot4("イエッサン", "♀", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("テブリム", Rom.Sword),
                    new CommonSlot3("テブリム", Rom.Shield),
                    new CommonSlot3("シンボラー"),
                    new CommonSlot3("ネイティオ"),
                    new CommonSlot4("イエッサン", "♂", Rom.Sword),
                    new CommonSlot4("イエッサン", "♀", Rom.Shield),
                    new CommonSlot5("シンボラー", Rom.Sword),
                    new CommonSlot5("ヤレユータン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("イエッサン", "♂", Rom.Sword),
                    new CommonSlot4("イエッサン", "♀", Rom.Shield),
                    new CommonSlot5("シンボラー", Rom.Sword),
                    new CommonSlot5("ヤレユータン", Rom.Shield),
                    new CommonSlot5("ブリムオン"),
                    new CommonSlot5("イオルブ", Rom.Sword),
                    new CommonSlot5("ギャロップ", "ガラル", Rom.Shield),
                }
            };
            var psychicC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("マネネ"),
                    new CommonSlot1("ソーナノ"),
                    new CommonSlot1("ネイティ"),
                    new CommonSlot1("ヤジロン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ネイティ"),
                    new CommonSlot1("ヤジロン"),
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot3("バリヤード", "ガラル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("バリヤード", "ガラル"),
                    new CommonSlot3("シンボラー"),
                    new CommonSlot3("ネイティオ"),
                    new CommonSlot4("ニャオニクス", "♂", Rom.Sword),
                    new CommonSlot4("ニャオニクス", "♀", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("バリヤード", "ガラル"),
                    new CommonSlot3("シンボラー"),
                    new CommonSlot3("ネイティオ"),
                    new CommonSlot4("ニャオニクス", "♂", Rom.Sword),
                    new CommonSlot4("ニャオニクス", "♀", Rom.Shield),
                    new CommonSlot5("ネンドール"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ニャオニクス", "♂", Rom.Sword),
                    new CommonSlot4("ニャオニクス", "♀", Rom.Shield),
                    new CommonSlot5("ネンドール"),
                    new CommonSlot5("バリコオル"),
                    new CommonSlot5("ソーナンス"),
                }
            };
            var psychicD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("マーイーカ"),
                    new CommonSlot1("ラルトス"),
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot1("コロモリ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot1("コロモリ"),
                    new CommonSlot2("ミブリム"),
                    new CommonSlot2("テブリム"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ミブリム"),
                    new CommonSlot2("テブリム"),
                    new CommonSlot3("キルリア"),
                    new CommonSlot4("ココロモリ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("キルリア"),
                    new CommonSlot4("ココロモリ"),
                    new CommonSlot5("ブリムオン"),
                    new CommonSlot5("バリコオル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ココロモリ"),
                    new CommonSlot5("ブリムオン"),
                    new CommonSlot5("バリコオル"),
                    new CommonSlot5("カラマネロ"),
                    new CommonSlot5("サーナイト"),
                }
            };
            var psychicE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("マーイーカ"),
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot1("コロモリ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("バリヤード", "ガラル"),
                    new CommonSlot1("コロモリ"),
                    new CommonSlot2("ミブリム"),
                    new CommonSlot2("テブリム"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ミブリム"),
                    new CommonSlot2("テブリム"),
                    new CommonSlot3("ドータクン"),
                    new CommonSlot4("ココロモリ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ドータクン"),
                    new CommonSlot4("ココロモリ"),
                    new CommonSlot5("カラマネロ"),
                    new CommonSlot5("バリコオル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ココロモリ"),
                    new CommonSlot5("カラマネロ"),
                    new CommonSlot5("バリコオル"),
                    new CommonSlot5("ブリムオン"),
                    new CommonSlot5("エーフィ"),
                }
            };
            #endregion

            #region Electric
            var fightingA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("バルキー"),
                    new CommonSlot1("ワンリキー"),
                    new CommonSlot1("ドッコラー"),
                    new CommonSlot1("ズルッグ", Rom.Sword),
                    new CommonSlot1("グレッグル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ドッコラー"),
                    new CommonSlot1("ズルッグ", Rom.Sword),
                    new CommonSlot1("グレッグル", Rom.Shield),
                    new CommonSlot2("ゴーリキー"),
                    new CommonSlot2("ドテッコツ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ゴーリキー"),
                    new CommonSlot2("ドテッコツ"),
                    new CommonSlot3("サワムラー", Rom.Sword),
                    new CommonSlot3("エビワラー", Rom.Shield),
                    new CommonSlot4("エビワラー", Rom.Sword),
                    new CommonSlot4("サワムラー", Rom.Shield),
                    new CommonSlot4("ズルズキン", Rom.Sword),
                    new CommonSlot4("ドクロッグ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("サワムラー", Rom.Sword),
                    new CommonSlot3("エビワラー", Rom.Shield),
                    new CommonSlot4("エビワラー", Rom.Sword),
                    new CommonSlot4("サワムラー", Rom.Shield),
                    new CommonSlot4("ズルズキン", Rom.Sword),
                    new CommonSlot4("ドクロッグ", Rom.Shield),
                    new CommonSlot5("ローブシン", Rom.Sword),
                    new CommonSlot5("カポエラー", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("エビワラー", Rom.Sword),
                    new CommonSlot4("サワムラー", Rom.Shield),
                    new CommonSlot4("ズルズキン", Rom.Sword),
                    new CommonSlot4("ドクロッグ", Rom.Shield),
                    new CommonSlot5("カポエラー"),
                    new CommonSlot5("ローブシン"),
                    new CommonSlot5("カイリキー"),
                }
            };
            var fightingB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("バルキー"),
                    new CommonSlot1("ヌイコグマ"),
                    new CommonSlot1("タタッコ"),
                    new CommonSlot1("ヤンチャム"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("タタッコ"),
                    new CommonSlot1("ヤンチャム"),
                    new CommonSlot2("カモネギ", "ガラル", Rom.Sword),
                    new CommonSlot2("ヌイコグマ", Rom.Shield),
                    new CommonSlot2("ダゲキ", Rom.Sword),
                    new CommonSlot2("ナゲキ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ダゲキ", Rom.Sword),
                    new CommonSlot2("ナゲキ", Rom.Shield),
                    new CommonSlot4("キテルグマ"),
                    new CommonSlot4("ゴロンダ"),
                    new CommonSlot4("ルチャブル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("キテルグマ"),
                    new CommonSlot4("ゴロンダ"),
                    new CommonSlot4("ルチャブル"),
                    new CommonSlot5("ネギガナイト", Rom.Sword),
                    new CommonSlot5("キテルグマ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("キテルグマ"),
                    new CommonSlot4("ゴロンダ"),
                    new CommonSlot4("ルチャブル"),
                    new CommonSlot5("ネギガナイト", Rom.Sword),
                    new CommonSlot5("キテルグマ", Rom.Shield),
                    new CommonSlot5("オトスパス"),
                    new CommonSlot5("タイレーツ"),
                }
            };
            var fightingC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("リオル"),
                    new CommonSlot1("ワンリキー"),
                    new CommonSlot1("ヌイコグマ"),
                    new CommonSlot1("ズルッグ", Rom.Sword),
                    new CommonSlot1("グレッグル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヌイコグマ"),
                    new CommonSlot1("ズルッグ", Rom.Sword),
                    new CommonSlot1("グレッグル", Rom.Shield),
                    new CommonSlot2("キテルグマ"),
                    new CommonSlot3("タイレーツ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("キテルグマ"),
                    new CommonSlot3("タイレーツ"),
                    new CommonSlot3("ゴーリキー"),
                    new CommonSlot4("ズルズキン", Rom.Sword),
                    new CommonSlot4("ドクロッグ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("タイレーツ"),
                    new CommonSlot3("ゴーリキー"),
                    new CommonSlot4("ズルズキン", Rom.Sword),
                    new CommonSlot4("ドクロッグ", Rom.Shield),
                    new CommonSlot5("ナゲツケサル", Rom.Sword),
                    new CommonSlot5("ルチャブル", Rom.Shield),
                    new CommonSlot5("カイリキー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ズルズキン", Rom.Sword),
                    new CommonSlot4("ドクロッグ", Rom.Shield),
                    new CommonSlot5("ナゲツケサル", Rom.Sword),
                    new CommonSlot5("ルチャブル", Rom.Shield),
                    new CommonSlot5("カイリキー"),
                    new CommonSlot5("ルカリオ"),
                    new CommonSlot5("エルレイド"),
                }
            };
            var fightingD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("リオル", Rom.Sword),
                    new CommonSlot1("ワンリキー", Rom.Sword),
                    new CommonSlot1("ヌイコグマ", Rom.Sword),
                    new CommonSlot1("カモネギ", "ガラル", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヌイコグマ", Rom.Sword),
                    new CommonSlot1("カモネギ", "ガラル", Rom.Sword),
                    new CommonSlot2("キテルグマ", Rom.Sword),
                    new CommonSlot3("ゴーリキー", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("キテルグマ", Rom.Sword),
                    new CommonSlot3("ゴーリキー", Rom.Sword),
                    new CommonSlot3("タイレーツ", Rom.Sword),
                    new CommonSlot4("ルチャブル", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ゴーリキー", Rom.Sword),
                    new CommonSlot3("タイレーツ", Rom.Sword),
                    new CommonSlot4("ルチャブル", Rom.Sword),
                    new CommonSlot5("ルカリオ", Rom.Sword),
                    new CommonSlot5("エルレイド", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ルチャブル", Rom.Sword),
                    new CommonSlot5("ルカリオ", Rom.Sword),
                    new CommonSlot5("エルレイド", Rom.Sword),
                    new CommonSlot5("ネギガナイト", Rom.Sword),
                    new CommonSlot5("カイリキー", "キョダイ", Rom.Sword),
                }
            };
            #endregion

            #region Electric
            var flyingA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("マメパト"),
                    new CommonSlot1("ホーホー"),
                    new CommonSlot1("ネイティ"),
                    new CommonSlot1("ワシボン", Rom.Sword),
                    new CommonSlot1("バルチャイ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ネイティ"),
                    new CommonSlot1("ワシボン", Rom.Sword),
                    new CommonSlot1("バルチャイ", Rom.Shield),
                    new CommonSlot2("コロモリ"),
                    new CommonSlot2("ハトーボー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ハトーボー"),
                    new CommonSlot3("ケンホロウ"),
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ココロモリ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ケンホロウ"),
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ココロモリ"),
                    new CommonSlot5("ネイティオ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ココロモリ"),
                    new CommonSlot5("ネイティオ"),
                    new CommonSlot5("シンボラー"),
                    new CommonSlot5("ウォーグル", Rom.Sword),
                    new CommonSlot5("バルジーナ", Rom.Shield),
                }
            };
            var flyingB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ココガラ"),
                    new CommonSlot1("オンバット"),
                    new CommonSlot1("キャモメ"),
                    new CommonSlot1("ネイティ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("キャモメ"),
                    new CommonSlot1("ネイティ"),
                    new CommonSlot2("フワンテ"),
                    new CommonSlot2("アオガラス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("アオガラス"),
                    new CommonSlot3("フワライド"),
                    new CommonSlot4("ペリッパー"),
                    new CommonSlot4("ネイティオ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("フワライド"),
                    new CommonSlot4("ペリッパー"),
                    new CommonSlot4("ネイティオ"),
                    new CommonSlot5("アーマーガア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ペリッパー"),
                    new CommonSlot4("ネイティオ"),
                    new CommonSlot5("アーマーガア"),
                    new CommonSlot5("ルチャブル"),
                    new CommonSlot5("ウッウ"),
                }
            };
            var flyingC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ネイティ"),
                    new CommonSlot1("ホーホー"),
                    new CommonSlot1("ココガラ"),
                    new CommonSlot1("キャモメ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ココガラ"),
                    new CommonSlot1("キャモメ"),
                    new CommonSlot2("バタフリー"),
                    new CommonSlot2("アオガラス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("バタフリー"),
                    new CommonSlot2("アオガラス"),
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ペリッパー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ペリッパー"),
                    new CommonSlot5("ネイティオ"),
                    new CommonSlot5("ルチャブル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ペリッパー"),
                    new CommonSlot5("ネイティオ"),
                    new CommonSlot5("ルチャブル"),
                    new CommonSlot5("アーマーガア"),
                    new CommonSlot5("デリバード"),
                }
            };
            var flyingD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ネイティ"),
                    new CommonSlot1("ホーホー"),
                    new CommonSlot1("ココガラ"),
                    new CommonSlot1("キャモメ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ココガラ"),
                    new CommonSlot1("キャモメ"),
                    new CommonSlot2("バタフリー"),
                    new CommonSlot2("アオガラス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("バタフリー"),
                    new CommonSlot2("アオガラス"),
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ペリッパー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ペリッパー"),
                    new CommonSlot5("ネイティオ"),
                    new CommonSlot5("ルチャブル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヨルノズク"),
                    new CommonSlot4("ペリッパー"),
                    new CommonSlot5("ネイティオ"),
                    new CommonSlot5("ルチャブル"),
                    new CommonSlot5("シンボラー"),
                    new CommonSlot5("アーマーガア", "キョダイ"),
                }
            };
            #endregion

            #region Electric
            var bugA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("キャタピー"),
                    new CommonSlot1("アゴジムシ"),
                    new CommonSlot1("ツチニン"),
                    new CommonSlot1("バチュル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ツチニン"),
                    new CommonSlot1("バチュル"),
                    new CommonSlot2("トランセル"),
                    new CommonSlot2("アイアント"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トランセル"),
                    new CommonSlot2("アイアント"),
                    new CommonSlot3("デンヂムシ"),
                    new CommonSlot4("テッカニン"),
                    new CommonSlot4("バタフリー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("デンヂムシ"),
                    new CommonSlot4("テッカニン"),
                    new CommonSlot4("バタフリー"),
                    new CommonSlot5("デンチュラ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("テッカニン"),
                    new CommonSlot4("バタフリー"),
                    new CommonSlot5("デンチュラ"),
                    new CommonSlot5("クワガノン"),
                    new CommonSlot5("アイアント"),
                }
            };
            var bugB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("キャタピー"),
                    new CommonSlot1("ミツハニー"),
                    new CommonSlot1("アブリー"),
                    new CommonSlot1("サッチムシ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("アブリー"),
                    new CommonSlot1("サッチムシ"),
                    new CommonSlot2("バチュル"),
                    new CommonSlot2("トランセル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("バチュル"),
                    new CommonSlot2("トランセル"),
                    new CommonSlot3("レドームシ"),
                    new CommonSlot4("デンチュラ"),
                    new CommonSlot4("バタフリー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("レドームシ"),
                    new CommonSlot4("デンチュラ"),
                    new CommonSlot4("バタフリー"),
                    new CommonSlot5("アブリボン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("デンチュラ"),
                    new CommonSlot4("バタフリー"),
                    new CommonSlot5("アブリボン"),
                    new CommonSlot5("ビークイン"),
                    new CommonSlot5("イオルブ"),
                }
            };
            var bugC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("コソクムシ"),
                    new CommonSlot1("サッチムシ"),
                    new CommonSlot1("シズクモ"),
                    new CommonSlot1("カブルモ", Rom.Sword),
                    new CommonSlot1("チョボマキ", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("シズクモ"),
                    new CommonSlot1("カブルモ", Rom.Sword),
                    new CommonSlot1("チョボマキ", Rom.Sword),
                    new CommonSlot2("チョボマキ", Rom.Sword),
                    new CommonSlot2("カブルモ", Rom.Sword),
                    new CommonSlot2("イシズマイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("チョボマキ", Rom.Sword),
                    new CommonSlot2("カブルモ", Rom.Sword),
                    new CommonSlot2("イシズマイ"),
                    new CommonSlot3("レドームシ"),
                    new CommonSlot4("イオルブ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("レドームシ"),
                    new CommonSlot4("イオルブ"),
                    new CommonSlot5("オニシズクモ"),
                    new CommonSlot5("グソクムシャ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("イオルブ"),
                    new CommonSlot5("オニシズクモ"),
                    new CommonSlot5("グソクムシャ"),
                    new CommonSlot5("ヌケニン"),
                    new CommonSlot5("シュバルゴ", Rom.Sword),
                    new CommonSlot5("アギルダー", Rom.Sword),
                }
            };
            var bugD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("コソクムシ"),
                    new CommonSlot1("サッチムシ"),
                    new CommonSlot1("シズクモ"),
                    new CommonSlot1("カブルモ", Rom.Sword),
                    new CommonSlot1("チョボマキ", Rom.Sword),
                    new CommonSlot5("バタフリー", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("シズクモ"),
                    new CommonSlot1("カブルモ", Rom.Sword),
                    new CommonSlot1("チョボマキ", Rom.Sword),
                    new CommonSlot2("チョボマキ", Rom.Sword),
                    new CommonSlot2("カブルモ", Rom.Sword),
                    new CommonSlot2("イシズマイ"),
                    new CommonSlot5("バタフリー", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("チョボマキ", Rom.Sword),
                    new CommonSlot2("カブルモ", Rom.Sword),
                    new CommonSlot2("イシズマイ"),
                    new CommonSlot3("レドームシ"),
                    new CommonSlot4("イオルブ"),
                    new CommonSlot5("バタフリー", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("レドームシ"),
                    new CommonSlot4("イオルブ"),
                    new CommonSlot5("オニシズクモ"),
                    new CommonSlot5("グソクムシャ"),
                    new CommonSlot5("バタフリー", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("イオルブ"),
                    new CommonSlot5("オニシズクモ"),
                    new CommonSlot5("グソクムシャ"),
                    new CommonSlot5("バタフリー", "キョダイ"),
                    new CommonSlot5("シュバルゴ", Rom.Sword),
                    new CommonSlot5("アギルダー", Rom.Sword),
                }
            };
            var bugE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("コソクムシ"),
                    new CommonSlot1("サッチムシ"),
                    new CommonSlot1("シズクモ"),
                    new CommonSlot1("カブルモ", Rom.Sword),
                    new CommonSlot1("チョボマキ", Rom.Sword),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("シズクモ"),
                    new CommonSlot1("カブルモ", Rom.Sword),
                    new CommonSlot1("チョボマキ", Rom.Sword),
                    new CommonSlot2("チョボマキ", Rom.Sword),
                    new CommonSlot2("カブルモ", Rom.Sword),
                    new CommonSlot2("イシズマイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("チョボマキ", Rom.Sword),
                    new CommonSlot2("カブルモ", Rom.Sword),
                    new CommonSlot2("イシズマイ"),
                    new CommonSlot3("レドームシ"),
                    new CommonSlot4("イオルブ"),
                    new CommonSlot5("イオルブ", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("レドームシ"),
                    new CommonSlot4("イオルブ"),
                    new CommonSlot5("オニシズクモ"),
                    new CommonSlot5("グソクムシャ"),
                    new CommonSlot5("イオルブ", "キョダイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("イオルブ"),
                    new CommonSlot5("オニシズクモ"),
                    new CommonSlot5("グソクムシャ"),
                    new CommonSlot5("イオルブ", "キョダイ"),
                    new CommonSlot5("シュバルゴ", Rom.Sword),
                    new CommonSlot5("アギルダー", Rom.Sword),
                }
            };
            #endregion

            #region Electric
            var ghostA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ゴース"),
                    new CommonSlot1("ヨマワル"),
                    new CommonSlot1("フワンテ"),
                    new CommonSlot1("ボクレー"),
                    new CommonSlot1("プルリル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("フワンテ"),
                    new CommonSlot1("ボクレー"),
                    new CommonSlot1("プルリル"),
                    new CommonSlot2("バケッチャ", "普通"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("バケッチャ", "普通"),
                    new CommonSlot3("ゴースト"),
                    new CommonSlot4("サマヨール"),
                    new CommonSlot4("フワライド"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ゴースト"),
                    new CommonSlot4("サマヨール"),
                    new CommonSlot4("フワライド"),
                    new CommonSlot5("オーロット"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("サマヨール"),
                    new CommonSlot4("フワライド"),
                    new CommonSlot5("オーロット"),
                    new CommonSlot5("パンプジン", "普通"),
                    new CommonSlot5("ブルンゲル"),
                }
            };
            var ghostB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ゴース"),
                    new CommonSlot1("デスマス", "ガラル"),
                    new CommonSlot1("ヤバチャ"),
                    new CommonSlot1("ヨマワル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤバチャ"),
                    new CommonSlot1("ヨマワル"),
                    new CommonSlot2("ゴースト"),
                    new CommonSlot2("バケッチャ", "普通", Rom.Sword),
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ゴースト"),
                    new CommonSlot2("バケッチャ", "普通", Rom.Sword),
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                    new CommonSlot3("サマヨール"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("バケッチャ", "普通", Rom.Sword),
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                    new CommonSlot3("サマヨール"),
                    new CommonSlot5("ポットデス", Rom.Sword),
                    new CommonSlot5("デスバーン", Rom.Shield),
                    new CommonSlot5("パンプジン", "普通", Rom.Sword),
                    new CommonSlot5("サニゴーン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("デスバーン", Rom.Sword),
                    new CommonSlot4("ヤミラミ", Rom.Shield),
                    new CommonSlot5("ヨノワール"),
                    new CommonSlot5("ゲンガー"),
                    new CommonSlot5("ポットデス", Rom.Sword),
                    new CommonSlot5("デスバーン", Rom.Shield),
                    new CommonSlot5("パンプジン", "普通", Rom.Sword),
                    new CommonSlot5("サニゴーン", Rom.Shield),
                }
            };
            var ghostC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒトツキ"),
                    new CommonSlot1("デスマス", "ガラル"),
                    new CommonSlot1("ヤバチャ"),
                    new CommonSlot1("フワンテ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤバチャ"),
                    new CommonSlot1("フワンテ"),
                    new CommonSlot2("ニダンギル"),
                    new CommonSlot3("バケッチャ", "普通", Rom.Sword),
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ニダンギル"),
                    new CommonSlot3("バケッチャ", "普通", Rom.Sword),
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                    new CommonSlot3("フワライド"),
                    new CommonSlot4("パンプジン", "普通", Rom.Sword),
                    new CommonSlot4("ヤミラミ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("バケッチャ", "普通", Rom.Sword),
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                    new CommonSlot3("フワライド"),
                    new CommonSlot4("パンプジン", "普通", Rom.Sword),
                    new CommonSlot4("ヤミラミ", Rom.Shield),
                    new CommonSlot5("パンプジン", "普通", Rom.Sword),
                    new CommonSlot5("サニゴーン", Rom.Shield),
                    new CommonSlot5("ポットデス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("パンプジン", "普通", Rom.Sword),
                    new CommonSlot4("ヤミラミ", Rom.Shield),
                    new CommonSlot5("パンプジン", "普通", Rom.Sword),
                    new CommonSlot5("サニゴーン", Rom.Shield),
                    new CommonSlot5("ポットデス"),
                    new CommonSlot5("デスバーン"),
                    new CommonSlot5("ギルガルド"),
                }
            };
            var ghostD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ヒトツキ", Rom.Shield),
                    new CommonSlot1("デスマス", "ガラル", Rom.Shield),
                    new CommonSlot1("ヤバチャ", Rom.Shield),
                    new CommonSlot1("ゴース", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ヤバチャ", Rom.Shield),
                    new CommonSlot1("ゴース", Rom.Shield),
                    new CommonSlot2("ニダンギル", Rom.Shield),
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ニダンギル", Rom.Shield),
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                    new CommonSlot3("ゴースト", Rom.Shield),
                    new CommonSlot4("ヤミラミ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("サニーゴ", "ガラル", Rom.Shield),
                    new CommonSlot3("ゴースト", Rom.Shield),
                    new CommonSlot4("ヤミラミ", Rom.Shield),
                    new CommonSlot5("ポットデス", Rom.Shield),
                    new CommonSlot5("サニゴーン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ヤミラミ", Rom.Shield),
                    new CommonSlot5("ポットデス", Rom.Shield),
                    new CommonSlot5("サニゴーン", Rom.Shield),
                    new CommonSlot5("デスバーン", Rom.Shield),
                    new CommonSlot5("ゲンガー", "キョダイ", Rom.Shield),
                }
            };
            #endregion

            #region Electric
            var dragonA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("オンバット"),
                    new CommonSlot1("キバゴ"),
                    new CommonSlot1("ナックラー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("オンバット"),
                    new CommonSlot1("キバゴ", Rom.Sword),
                    new CommonSlot1("ナックラー", Rom.Shield),
                    new CommonSlot2("ビブラーバ"),
                    new CommonSlot2("ジャラコ", Rom.Sword),
                    new CommonSlot2("ヌメラ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ビブラーバ"),
                    new CommonSlot3("ジャランゴ", Rom.Sword),
                    new CommonSlot3("オノンド", Rom.Shield),
                    new CommonSlot4("オノンド", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                    new CommonSlot4("オノノクス", Rom.Sword),
                    new CommonSlot4("ヌメルゴン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ジャランゴ", Rom.Sword),
                    new CommonSlot3("オノンド", Rom.Shield),
                    new CommonSlot4("オノンド", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                    new CommonSlot4("オノノクス", Rom.Sword),
                    new CommonSlot4("ヌメルゴン", Rom.Shield),
                    new CommonSlot5("フライゴン", Rom.Sword),
                    new CommonSlot5("オノノクス", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("オノンド", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                    new CommonSlot4("オノノクス", Rom.Sword),
                    new CommonSlot4("ヌメルゴン", Rom.Shield),
                    new CommonSlot5("フライゴン", Rom.Sword),
                    new CommonSlot5("オノノクス", Rom.Shield),
                    new CommonSlot5("バクガメス", Rom.Sword),
                    new CommonSlot5("ジジーロン", Rom.Shield),
                    new CommonSlot5("ジャラランガ", Rom.Sword),
                    new CommonSlot5("ヌメルゴン", Rom.Shield),
                }
            };
            var dragonB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("オンバット"),
                    new CommonSlot1("カジッチュ"),
                    new CommonSlot1("ドラメシヤ"),
                    new CommonSlot1("ジャラコ", Rom.Sword),
                    new CommonSlot1("ヌメラ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ドラメシヤ"),
                    new CommonSlot1("ジャラコ", Rom.Sword),
                    new CommonSlot1("ヌメラ", Rom.Shield),
                    new CommonSlot2("オンバット"),
                    new CommonSlot2("カジッチュ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("カジッチュ"),
                    new CommonSlot3("ドロンチ"),
                    new CommonSlot4("オンバーン"),
                    new CommonSlot4("ジャランゴ", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ドロンチ"),
                    new CommonSlot4("オンバーン"),
                    new CommonSlot4("ジャランゴ", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                    new CommonSlot5("ジャラランガ", Rom.Sword),
                    new CommonSlot5("ヌメルゴン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("オンバーン"),
                    new CommonSlot4("ジャランゴ", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                    new CommonSlot5("ドラパルト"),
                    new CommonSlot5("ジャラランガ", Rom.Sword),
                    new CommonSlot5("ヌメルゴン", Rom.Shield),
                    new CommonSlot5("アップリュー", Rom.Sword),
                    new CommonSlot5("タルップル", Rom.Shield),
                }
            };
            var dragonC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("キバゴ"),
                    new CommonSlot1("ナックラー"),
                    new CommonSlot1("カジッチュ"),
                    new CommonSlot1("ジャラコ", Rom.Sword),
                    new CommonSlot1("ヌメラ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("キバゴ", Rom.Sword),
                    new CommonSlot1("ナックラー", Rom.Shield),
                    new CommonSlot1("ジャラコ", Rom.Sword),
                    new CommonSlot1("ヌメラ", Rom.Shield),
                    new CommonSlot2("ドラメシヤ"),
                    new CommonSlot2("オノンド", Rom.Sword),
                    new CommonSlot2("ビブラーバ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ドラメシヤ"),
                    new CommonSlot2("オノンド", Rom.Sword),
                    new CommonSlot2("ビブラーバ", Rom.Shield),
                    new CommonSlot4("バクガメス", Rom.Sword),
                    new CommonSlot4("ジジーロン", Rom.Shield),
                    new CommonSlot4("ジャランゴ", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("バクガメス", Rom.Sword),
                    new CommonSlot4("ジジーロン", Rom.Shield),
                    new CommonSlot4("ジャランゴ", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                    new CommonSlot5("ドロンチ"),
                    new CommonSlot5("ジャラランガ", Rom.Sword),
                    new CommonSlot5("ヌメルゴン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("バクガメス", Rom.Sword),
                    new CommonSlot4("ジジーロン", Rom.Shield),
                    new CommonSlot4("ジャランゴ", Rom.Sword),
                    new CommonSlot4("ヌメイル", Rom.Shield),
                    new CommonSlot5("ドロンチ"),
                    new CommonSlot5("ドラパルト"),
                    new CommonSlot5("ジャラランガ", Rom.Sword),
                    new CommonSlot5("ヌメルゴン", Rom.Shield),
                    new CommonSlot5("ジャラランガ", Rom.Sword),
                    new CommonSlot5("ヌメルゴン", Rom.Shield),
                    new CommonSlot5("オノノクス", Rom.Sword),
                    new CommonSlot5("フライゴン", Rom.Shield),
                }
            };
            #endregion

            #region Electric
            var darkA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("チョロネコ"),
                    new CommonSlot1("スカンプー"),
                    new CommonSlot1("ニューラ"),
                    new CommonSlot1("マーイーカ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ニューラ"),
                    new CommonSlot1("マーイーカ"),
                    new CommonSlot2("コマタナ"),
                    new CommonSlot2("レパルダス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("レパルダス"),
                    new CommonSlot3("スカタンク"),
                    new CommonSlot4("マニューラ"),
                    new CommonSlot4("カラマネロ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("スカタンク"),
                    new CommonSlot4("マニューラ"),
                    new CommonSlot4("カラマネロ"),
                    new CommonSlot5("キリキザン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("マニューラ"),
                    new CommonSlot4("カラマネロ"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("シザリガー"),
                    new CommonSlot5("ダーテング", Rom.Sword),
                    new CommonSlot5("ヤミラミ", Rom.Shield),
                }
            };
            var darkB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("クスネ"),
                    new CommonSlot1("ジグザグマ", "ガラル"),
                    new CommonSlot1("チョロネコ"),
                    new CommonSlot1("ベロバー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("チョロネコ"),
                    new CommonSlot1("ベロバー"),
                    new CommonSlot2("フォクスライ"),
                    new CommonSlot2("モノズ", Rom.Sword),
                    new CommonSlot2("バルチャイ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("フォクスライ"),
                    new CommonSlot3("マッスグマ", "ガラル"),
                    new CommonSlot4("ギモー"),
                    new CommonSlot4("オーロンゲ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("マッスグマ", "ガラル"),
                    new CommonSlot4("ギモー"),
                    new CommonSlot4("オーロンゲ"),
                    new CommonSlot5("ジヘッド", Rom.Sword),
                    new CommonSlot5("バルジーナ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ギモー"),
                    new CommonSlot4("オーロンゲ"),
                    new CommonSlot5("ジヘッド", Rom.Sword),
                    new CommonSlot5("バルジーナ", Rom.Shield),
                    new CommonSlot5("タチフサグマ"),
                    new CommonSlot5("サザンドラ", Rom.Sword),
                    new CommonSlot5("バンギラス", Rom.Shield),
                }
            };
            var darkC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("クスネ"),
                    new CommonSlot1("ジグザグマ", "ガラル"),
                    new CommonSlot1("ニューラ"),
                    new CommonSlot1("ズルッグ", Rom.Sword),
                    new CommonSlot1("バルチャイ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ニューラ"),
                    new CommonSlot1("ズルッグ", Rom.Sword),
                    new CommonSlot1("バルチャイ", Rom.Shield),
                    new CommonSlot2("レパルダス"),
                    new CommonSlot2("マッスグマ", "ガラル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("レパルダス"),
                    new CommonSlot2("マッスグマ", "ガラル"),
                    new CommonSlot4("フォクスライ"),
                    new CommonSlot4("ゴロンダ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("フォクスライ"),
                    new CommonSlot4("ゴロンダ"),
                    new CommonSlot5("マニューラ"),
                    new CommonSlot5("ズルズキン", Rom.Sword),
                    new CommonSlot5("バルジーナ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("フォクスライ"),
                    new CommonSlot4("ゴロンダ"),
                    new CommonSlot5("マニューラ"),
                    new CommonSlot5("ズルズキン", Rom.Sword),
                    new CommonSlot5("バルジーナ", Rom.Shield),
                    new CommonSlot5("タチフサグマ"),
                    new CommonSlot5("サザンドラ", Rom.Sword),
                    new CommonSlot5("バンギラス", Rom.Shield),
                }
            };
            var darkD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("クスネ"),
                    new CommonSlot1("ジグザグマ", "ガラル"),
                    new CommonSlot1("マーイーカ"),
                    new CommonSlot1("コマタナ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("マーイーカ"),
                    new CommonSlot1("コマタナ"),
                    new CommonSlot2("レパルダス"),
                    new CommonSlot2("マッスグマ", "ガラル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("レパルダス"),
                    new CommonSlot2("マッスグマ", "ガラル"),
                    new CommonSlot4("フォクスライ"),
                    new CommonSlot4("ゴロンダ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("フォクスライ"),
                    new CommonSlot4("ゴロンダ"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("カラマネロ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("フォクスライ"),
                    new CommonSlot4("ゴロンダ"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("カラマネロ"),
                    new CommonSlot5("タチフサグマ"),
                    new CommonSlot5("ブラッキー"),
                }
            };
            var darkE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("クスネ"),
                    new CommonSlot1("ジグザグマ", "ガラル"),
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ズルッグ", Rom.Sword),
                    new CommonSlot1("バルチャイ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ズルッグ", Rom.Sword),
                    new CommonSlot1("バルチャイ", Rom.Shield),
                    new CommonSlot2("レパルダス"),
                    new CommonSlot2("マッスグマ", "ガラル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("レパルダス"),
                    new CommonSlot2("マッスグマ", "ガラル"),
                    new CommonSlot4("ギモー"),
                    new CommonSlot4("フォクスライ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ギモー"),
                    new CommonSlot4("フォクスライ"),
                    new CommonSlot5("ゴロンダ"),
                    new CommonSlot5("ズルズキン", Rom.Sword),
                    new CommonSlot5("バルジーナ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("フォクスライ"),
                    new CommonSlot5("ゴロンダ"),
                    new CommonSlot5("ズルズキン", Rom.Sword),
                    new CommonSlot5("バルジーナ", Rom.Shield),
                    new CommonSlot5("サザンドラ", Rom.Sword),
                    new CommonSlot5("バンギラス", Rom.Shield),
                    new CommonSlot5("オーロンゲ", "キョダイ"),
                }
            };
            #endregion

            #region Electric
            var steelA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ギアル"),
                    new CommonSlot1("ニャース", "ガラル"),
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot1("テッシード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot1("テッシード"),
                    new CommonSlot2("コマタナ"),
                    new CommonSlot2("ゾウドウ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ゾウドウ"),
                    new CommonSlot4("ギギアル"),
                    new CommonSlot4("ニャイキング"),
                    new CommonSlot4("ドータクン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ギギアル"),
                    new CommonSlot4("ニャイキング"),
                    new CommonSlot4("ドータクン"),
                    new CommonSlot5("キリキザン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ギギアル"),
                    new CommonSlot4("ニャイキング"),
                    new CommonSlot4("ドータクン"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("ギギギアル"),
                    new CommonSlot5("ダイオウドウ"),
                }
            };
            var steelB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ギアル"),
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot1("テッシード"),
                    new CommonSlot1("コマタナ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("テッシード"),
                    new CommonSlot1("コマタナ"),
                    new CommonSlot2("ギアル"),
                    new CommonSlot2("ドーミラー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ドーミラー"),
                    new CommonSlot4("ハガネール"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot4("ドータクン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ハガネール"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot4("ドータクン"),
                    new CommonSlot5("キリキザン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ハガネール"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot4("ドータクン"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("トゲデマル"),
                    new CommonSlot5("クチート", Rom.Sword),
                    new CommonSlot5("ハガネール", Rom.Shield),
                }
            };
            var steelC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("リオル"),
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot1("コマタナ"),
                    new CommonSlot1("ギアル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("コマタナ"),
                    new CommonSlot2("ギアル"),
                    new CommonSlot2("イワーク"),
                    new CommonSlot2("アイアント", Rom.Sword),
                    new CommonSlot2("ギギアル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("イワーク"),
                    new CommonSlot2("アイアント", Rom.Sword),
                    new CommonSlot2("ギギアル", Rom.Shield),
                    new CommonSlot3("ギギアル", Rom.Sword),
                    new CommonSlot3("アイアント", Rom.Shield),
                    new CommonSlot4("ドータクン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ギギアル", Rom.Sword),
                    new CommonSlot3("アイアント", Rom.Shield),
                    new CommonSlot4("ドータクン"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("ハガネール"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ドータクン"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("ハガネール"),
                    new CommonSlot5("ギギギアル"),
                    new CommonSlot5("ルカリオ"),
                }
            };
            var steelD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ニャース", "ガラル"),
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot1("コマタナ"),
                    new CommonSlot1("テッシード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("コマタナ"),
                    new CommonSlot1("テッシード"),
                    new CommonSlot2("ヒトツキ"),
                    new CommonSlot2("ドータクン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ヒトツキ"),
                    new CommonSlot2("ドータクン"),
                    new CommonSlot4("ナットレイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot5("ニャイキング"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("マッギョ", "ガラル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot5("ニャイキング"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("マッギョ", "ガラル"),
                    new CommonSlot5("ダイオウドウ"),
                    new CommonSlot5("ジュラルドン"),
                }
            };
            var steelE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("リオル"),
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot1("コマタナ"),
                    new CommonSlot1("ギアル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("コマタナ"),
                    new CommonSlot2("ギアル"),
                    new CommonSlot2("イワーク"),
                    new CommonSlot2("アイアント", Rom.Sword),
                    new CommonSlot2("ギギアル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("イワーク"),
                    new CommonSlot2("アイアント", Rom.Sword),
                    new CommonSlot2("ギギアル", Rom.Shield),
                    new CommonSlot3("ギギアル", Rom.Sword),
                    new CommonSlot3("アイアント", Rom.Shield),
                    new CommonSlot4("ドータクン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ギギアル", Rom.Sword),
                    new CommonSlot3("アイアント", Rom.Shield),
                    new CommonSlot4("ドータクン"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("ハガネール"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ドータクン"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("ハガネール"),
                    new CommonSlot5("ギギギアル"),
                    new CommonSlot5("ジュラルドン", "キョダイ"),
                }
            };
            var steelF = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ニャース", "ガラル"),
                    new CommonSlot1("ドーミラー"),
                    new CommonSlot1("コマタナ"),
                    new CommonSlot1("テッシード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("コマタナ"),
                    new CommonSlot1("テッシード"),
                    new CommonSlot2("ヒトツキ"),
                    new CommonSlot2("ドータクン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ヒトツキ"),
                    new CommonSlot2("ドータクン"),
                    new CommonSlot3("ニャイキング"),
                    new CommonSlot4("ナットレイ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ニャイキング"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("マッギョ", "ガラル"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ニャイキング"),
                    new CommonSlot4("ナットレイ"),
                    new CommonSlot5("キリキザン"),
                    new CommonSlot5("マッギョ", "ガラル"),
                    new CommonSlot5("ジュラルドン"),
                    new CommonSlot5("ダイオウドウ", "キョダイ"),
                }
            };
            #endregion

            #region Electric
            var fairyA = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("ピィ"),
                    new CommonSlot1("トゲピー"),
                    new CommonSlot1("アブリー"),
                    new CommonSlot1("ペロッパフ", Rom.Sword),
                    new CommonSlot1("シュシュプ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("アブリー"),
                    new CommonSlot1("ペロッパフ", Rom.Sword),
                    new CommonSlot1("シュシュプ", Rom.Shield),
                    new CommonSlot2("ピッピ"),
                    new CommonSlot2("ネマシュ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("ネマシュ"),
                    new CommonSlot3("トゲチック"),
                    new CommonSlot4("ピクシー"),
                    new CommonSlot4("アブリボン"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("トゲチック"),
                    new CommonSlot4("ピクシー"),
                    new CommonSlot4("アブリボン"),
                    new CommonSlot5("マシェード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ピクシー"),
                    new CommonSlot4("アブリボン"),
                    new CommonSlot5("マシェード"),
                    new CommonSlot5("トゲキッス"),
                    new CommonSlot5("ペロリーム", Rom.Sword),
                    new CommonSlot5("フレフワン", Rom.Shield),
                }
            };
            var fairyB = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("マネネ"),
                    new CommonSlot1("マホミル"),
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                    new CommonSlot2("ピッピ"),
                    new CommonSlot2("キルリア"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("キルリア"),
                    new CommonSlot3("ギモー"),
                    new CommonSlot4("ピクシー"),
                    new CommonSlot4("サーナイト"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ギモー"),
                    new CommonSlot4("ピクシー"),
                    new CommonSlot4("サーナイト"),
                    new CommonSlot5("マホイップ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ピクシー"),
                    new CommonSlot4("サーナイト"),
                    new CommonSlot5("マホイップ"),
                    new CommonSlot5("オーロンゲ"),
                    new CommonSlot5("クチート", Rom.Sword),
                    new CommonSlot5("ギャロップ", "ガラル", Rom.Shield),
                }
            };
            var fairyC = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("トゲピー"),
                    new CommonSlot1("ネマシュ"),
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                    new CommonSlot2("トゲチック"),
                    new CommonSlot2("マシェード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トゲチック"),
                    new CommonSlot2("マシェード"),
                    new CommonSlot4("ギモー"),
                    new CommonSlot4("クチート", Rom.Sword),
                    new CommonSlot4("ギャロップ", "ガラル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ギモー"),
                    new CommonSlot4("クチート", Rom.Sword),
                    new CommonSlot4("ギャロップ", "ガラル", Rom.Shield),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("トゲキッス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("ギモー"),
                    new CommonSlot4("クチート", Rom.Sword),
                    new CommonSlot4("ギャロップ", "ガラル", Rom.Shield),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("トゲキッス"),
                    new CommonSlot5("オーロンゲ"),
                    new CommonSlot5("ミミッキュ"),
                }
            };
            var fairyD = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("トゲピー"),
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                    new CommonSlot1("ペロッパフ", Rom.Sword),
                    new CommonSlot1("シュシュプ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                    new CommonSlot2("トゲチック"),
                    new CommonSlot2("ギモー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トゲチック"),
                    new CommonSlot2("ギモー"),
                    new CommonSlot4("マホミル"),
                    new CommonSlot4("ペロリーム", Rom.Sword),
                    new CommonSlot4("フレフワン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("マホミル"),
                    new CommonSlot4("ペロリーム", Rom.Sword),
                    new CommonSlot4("フレフワン", Rom.Shield),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("オーロンゲ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("マホミル"),
                    new CommonSlot4("ペロリーム", Rom.Sword),
                    new CommonSlot4("フレフワン", Rom.Shield),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("オーロンゲ"),
                    new CommonSlot5("トゲキッス"),
                    new CommonSlot5("ニンフィア"),
                }
            };
            var fairyE = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("トゲピー"),
                    new CommonSlot1("ネマシュ"),
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                    new CommonSlot2("トゲチック"),
                    new CommonSlot2("マシェード"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トゲチック"),
                    new CommonSlot2("マシェード"),
                    new CommonSlot3("ギモー"),
                    new CommonSlot4("クチート", Rom.Sword),
                    new CommonSlot4("ギャロップ", "ガラル", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot3("ギモー"),
                    new CommonSlot4("クチート", Rom.Sword),
                    new CommonSlot4("ギャロップ", "ガラル", Rom.Shield),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("トゲキッス"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("クチート", Rom.Sword),
                    new CommonSlot4("ギャロップ", "ガラル", Rom.Shield),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("トゲキッス"),
                    new CommonSlot5("オーロンゲ"),
                    new CommonSlot5("マホイップ", "キョダイ"),
                }
            };
            var fairyF = new RaidBattleSlot[][]
            {
                new RaidBattleSlot[]{
                    new CommonSlot1("トゲピー"),
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                    new CommonSlot1("ペロッパフ", Rom.Sword),
                    new CommonSlot1("シュシュプ", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot1("ベロバー"),
                    new CommonSlot1("ラルトス"),
                    new CommonSlot2("トゲチック"),
                    new CommonSlot2("ギモー"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot2("トゲチック"),
                    new CommonSlot2("ギモー"),
                    new CommonSlot4("マホミル"),
                    new CommonSlot3("ペロリーム", Rom.Sword),
                    new CommonSlot3("フレフワン", Rom.Shield),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("マホミル"),
                    new CommonSlot3("ペロリーム", Rom.Sword),
                    new CommonSlot3("フレフワン", Rom.Shield),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("オーロンゲ"),
                },
                new RaidBattleSlot[]{
                    new CommonSlot4("マホミル"),
                    new CommonSlot5("サーナイト"),
                    new CommonSlot5("オーロンゲ"),
                    new CommonSlot5("トゲキッス"),
                    new CommonSlot5("ブリムオン", "キョダイ"),
                }
            };
            #endregion

            #region Electric
            var specialA = new RaidBattleSlot[][]
            {
                rockD[0].Concat(iceF[0]).ToArray(),
                rockD[1].Concat(iceF[1]).ToArray(),
                rockD[2].Concat(iceF[2]).ToArray(),
                rockD[3].Concat(iceF[3]).ToArray(),
                rockD[4].Concat(iceF[4]).ToArray(),
            };
            var specialB = new RaidBattleSlot[][]
            {
                fightingD[0].Concat(ghostD[0]).ToArray(),
                fightingD[1].Concat(ghostD[1]).ToArray(),
                fightingD[2].Concat(ghostD[2]).ToArray(),
                fightingD[3].Concat(ghostD[3]).ToArray(),
                fightingD[4].Concat(ghostD[4]).ToArray(),
            };
            #endregion
            #endregion

            #region Dens
            mapList.Add(new WildAreaMap("うららか平原 A", flyingA));
            mapList.Add(new WildAreaMap("うららか平原 A レア", bugD));
            mapList.Add(new WildAreaMap("うららか平原 B", fightingA));
            mapList.Add(new WildAreaMap("うららか平原 B レア", fightingC));
            mapList.Add(new WildAreaMap("うららか平原 C", flyingA));
            mapList.Add(new WildAreaMap("うららか平原 C レア", bugD));
            mapList.Add(new WildAreaMap("うららか平原 D", groundA));
            mapList.Add(new WildAreaMap("うららか平原 D レア", groundC));
            mapList.Add(new WildAreaMap("うららか平原 E", normalA));
            mapList.Add(new WildAreaMap("うららか平原 E レア", normalC));
            mapList.Add(new WildAreaMap("うららか平原 F", bugA));
            mapList.Add(new WildAreaMap("うららか平原 F レア", bugC));
            mapList.Add(new WildAreaMap("うららか平原 G", dragonA));
            mapList.Add(new WildAreaMap("うららか平原 G レア", dragonC));
            mapList.Add(new WildAreaMap("うららか平原 H", rockA));
            mapList.Add(new WildAreaMap("うららか平原 H レア", rockC));
            mapList.Add(new WildAreaMap("うららか平原 I", fairyA));
            mapList.Add(new WildAreaMap("うららか平原 I レア", fairyC));

            mapList.Add(new WildAreaMap("こもれび林 A", grassA));
            mapList.Add(new WildAreaMap("こもれび林 A レア", grassD));
            mapList.Add(new WildAreaMap("こもれび林 B", grassC));
            mapList.Add(new WildAreaMap("こもれび林 B レア", grassG));
            mapList.Add(new WildAreaMap("こもれび林 C", grassA));
            mapList.Add(new WildAreaMap("こもれび林 C レア", grassE));
            mapList.Add(new WildAreaMap("こもれび林 D", bugA));
            mapList.Add(new WildAreaMap("こもれび林 D レア", bugE));
            mapList.Add(new WildAreaMap("こもれび林 E", grassA));
            mapList.Add(new WildAreaMap("こもれび林 E レア", grassE));

            mapList.Add(new WildAreaMap("見張り塔跡地 A", ghostA));
            mapList.Add(new WildAreaMap("見張り塔跡地 A レア", ghostC));
            mapList.Add(new WildAreaMap("見張り塔跡地 C", psychicA));
            mapList.Add(new WildAreaMap("見張り塔跡地 C レア", psychicD));

            mapList.Add(new WildAreaMap("キバ湖・西 A", darkA));
            mapList.Add(new WildAreaMap("キバ湖・西 A レア", darkC));
            mapList.Add(new WildAreaMap("キバ湖・西 B", waterC));
            mapList.Add(new WildAreaMap("キバ湖・西 B レア", waterK));
            mapList.Add(new WildAreaMap("キバ湖・西 C", waterC));
            mapList.Add(new WildAreaMap("キバ湖・西 C レア", waterH));
            mapList.Add(new WildAreaMap("キバ湖・西 D", waterC));
            mapList.Add(new WildAreaMap("キバ湖・西 D レア", waterH));
            mapList.Add(new WildAreaMap("キバ湖・西 E", waterA));
            mapList.Add(new WildAreaMap("キバ湖・西 E レア", waterF));
            mapList.Add(new WildAreaMap("キバ湖・西 F", waterE));
            mapList.Add(new WildAreaMap("キバ湖・西 F レア", waterF));

            mapList.Add(new WildAreaMap("キバ湖・東 A", normalA));
            mapList.Add(new WildAreaMap("キバ湖・東 A レア", normalC));
            mapList.Add(new WildAreaMap("キバ湖・東 B", poisonA));
            mapList.Add(new WildAreaMap("キバ湖・東 B レア", poisonD));
            mapList.Add(new WildAreaMap("キバ湖・東 C", flyingA));
            mapList.Add(new WildAreaMap("キバ湖・東 C レア", flyingC));
            mapList.Add(new WildAreaMap("キバ湖・東 D", flyingA));
            mapList.Add(new WildAreaMap("キバ湖・東 D レア", flyingC));
            mapList.Add(new WildAreaMap("キバ湖・東 E", waterA));
            mapList.Add(new WildAreaMap("キバ湖・東 E レア", waterF));

            mapList.Add(new WildAreaMap("キバ湖の瞳 A", dragonB));
            mapList.Add(new WildAreaMap("キバ湖の瞳 A レア", dragonC));

            mapList.Add(new WildAreaMap("ミロカロ湖・南 A", bugB));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 A レア", bugC));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 B", fightingA));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 B レア", ghostB));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 C", waterB));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 C レア", waterK));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 D", waterD));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 D レア", waterI));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 E", waterD));
            mapList.Add(new WildAreaMap("ミロカロ湖・南 E レア", waterI));

            mapList.Add(new WildAreaMap("ミロカロ湖・北 A", poisonA));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 A レア", poisonC));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 B", poisonA));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 B レア", poisonC));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 C", waterD));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 C レア", waterI));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 D", waterD));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 D レア", waterI));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 E", fightingB));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 E レア", fightingC));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 F", fightingB));
            mapList.Add(new WildAreaMap("ミロカロ湖・北 F レア", fightingC));

            mapList.Add(new WildAreaMap("巨人の腰かけ A", steelA));
            mapList.Add(new WildAreaMap("巨人の腰かけ A レア", steelE));
            mapList.Add(new WildAreaMap("巨人の腰かけ B", steelB));
            mapList.Add(new WildAreaMap("巨人の腰かけ B レア", steelC));
            mapList.Add(new WildAreaMap("巨人の腰かけ C", steelA));
            mapList.Add(new WildAreaMap("巨人の腰かけ C レア", steelD));
            mapList.Add(new WildAreaMap("巨人の腰かけ D", rockB));
            mapList.Add(new WildAreaMap("巨人の腰かけ D レア", specialA)); 
            mapList.Add(new WildAreaMap("巨人の腰かけ E", rockB));
            mapList.Add(new WildAreaMap("巨人の腰かけ E レア", rockC));

            mapList.Add(new WildAreaMap("エンジンリバーサイド A", poisonB));
            mapList.Add(new WildAreaMap("エンジンリバーサイド A レア", poisonC));
            mapList.Add(new WildAreaMap("エンジンリバーサイド B", psychicC));
            mapList.Add(new WildAreaMap("エンジンリバーサイド B レア", psychicD));
            mapList.Add(new WildAreaMap("エンジンリバーサイド C", electricA));
            mapList.Add(new WildAreaMap("エンジンリバーサイド C レア", electricC));
            mapList.Add(new WildAreaMap("エンジンリバーサイド D", normalB));
            mapList.Add(new WildAreaMap("エンジンリバーサイド D レア", normalC));

            mapList.Add(new WildAreaMap("ハシノマ原っぱ A", fairyB));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ A レア", fairyE));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ B", normalB));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ B レア", normalC));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ C", bugB));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ C レア", bugC));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ D", darkB));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ D レア", darkC));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ E", bugA));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ E レア", bugE));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ F", psychicB));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ F レア", fairyF));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ G", grassB));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ G レア", grassC));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ H", waterB));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ H レア", waterF));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ I", groundA));
            mapList.Add(new WildAreaMap("ハシノマ原っぱ I レア", groundC));

            mapList.Add(new WildAreaMap("ストーンズ原野 A", psychicA));
            mapList.Add(new WildAreaMap("ストーンズ原野 A レア", psychicD));
            mapList.Add(new WildAreaMap("ストーンズ原野 B", grassB));
            mapList.Add(new WildAreaMap("ストーンズ原野 B レア", grassC));
            mapList.Add(new WildAreaMap("ストーンズ原野 C", darkA));
            mapList.Add(new WildAreaMap("ストーンズ原野 C レア", darkC));
            mapList.Add(new WildAreaMap("ストーンズ原野 D", fireB));
            mapList.Add(new WildAreaMap("ストーンズ原野 D レア", fireH));
            mapList.Add(new WildAreaMap("ストーンズ原野 E", fightingA));
            mapList.Add(new WildAreaMap("ストーンズ原野 E レア", specialB));
            mapList.Add(new WildAreaMap("ストーンズ原野 F", ghostA));
            mapList.Add(new WildAreaMap("ストーンズ原野 F レア", ghostB));
            mapList.Add(new WildAreaMap("ストーンズ原野 G", rockA));
            mapList.Add(new WildAreaMap("ストーンズ原野 G レア", rockC));
            mapList.Add(new WildAreaMap("ストーンズ原野 H", steelB));
            mapList.Add(new WildAreaMap("ストーンズ原野 H レア", steelF));
            mapList.Add(new WildAreaMap("ストーンズ原野 I", iceB));
            mapList.Add(new WildAreaMap("ストーンズ原野 I レア", iceD));
            mapList.Add(new WildAreaMap("ストーンズ原野 J", psychicC));
            mapList.Add(new WildAreaMap("ストーンズ原野 J レア", psychicE));
            mapList.Add(new WildAreaMap("ストーンズ原野 K", fireA));
            mapList.Add(new WildAreaMap("ストーンズ原野 K レア", fireE));
            mapList.Add(new WildAreaMap("ストーンズ原野 L", normalB));
            mapList.Add(new WildAreaMap("ストーンズ原野 L レア", normalD));

            mapList.Add(new WildAreaMap("砂塵の窪地 A", waterC));
            mapList.Add(new WildAreaMap("砂塵の窪地 A レア", waterH));
            mapList.Add(new WildAreaMap("砂塵の窪地 B", steelA));
            mapList.Add(new WildAreaMap("砂塵の窪地 B レア", steelD));
            mapList.Add(new WildAreaMap("砂塵の窪地 C", iceA));
            mapList.Add(new WildAreaMap("砂塵の窪地 C レア", iceD));
            mapList.Add(new WildAreaMap("砂塵の窪地 D", groundB));
            mapList.Add(new WildAreaMap("砂塵の窪地 D レア", groundC));
            mapList.Add(new WildAreaMap("砂塵の窪地 E", rockB));
            mapList.Add(new WildAreaMap("砂塵の窪地 E レア", groundD));
            mapList.Add(new WildAreaMap("砂塵の窪地 F", darkB));
            mapList.Add(new WildAreaMap("砂塵の窪地 F レア", darkE));
            mapList.Add(new WildAreaMap("砂塵の窪地 G", fightingB));
            mapList.Add(new WildAreaMap("砂塵の窪地 G レア", fightingC));
            mapList.Add(new WildAreaMap("砂塵の窪地 H", groundB));
            mapList.Add(new WildAreaMap("砂塵の窪地 H レア", groundD));
            mapList.Add(new WildAreaMap("砂塵の窪地 I", fireC));
            mapList.Add(new WildAreaMap("砂塵の窪地 I レア", fireE));

            mapList.Add(new WildAreaMap("巨人の帽子 A", flyingB));
            mapList.Add(new WildAreaMap("巨人の帽子 A レア", flyingD));
            mapList.Add(new WildAreaMap("巨人の帽子 B", electricA));
            mapList.Add(new WildAreaMap("巨人の帽子 B レア", waterJ));
            mapList.Add(new WildAreaMap("巨人の帽子 C", iceC));
            mapList.Add(new WildAreaMap("巨人の帽子 C レア", iceD));
            mapList.Add(new WildAreaMap("巨人の帽子 D", fireA));
            mapList.Add(new WildAreaMap("巨人の帽子 D レア", fireF));
            mapList.Add(new WildAreaMap("巨人の帽子 E", poisonB));
            mapList.Add(new WildAreaMap("巨人の帽子 E レア", poisonC));

            mapList.Add(new WildAreaMap("巨人の鏡池 A", grassB));
            mapList.Add(new WildAreaMap("巨人の鏡池 A レア", grassF));
            mapList.Add(new WildAreaMap("巨人の鏡池 B", darkB));
            mapList.Add(new WildAreaMap("巨人の鏡池 B レア", darkD));
            mapList.Add(new WildAreaMap("巨人の鏡池 C", electricB));
            mapList.Add(new WildAreaMap("巨人の鏡池 C レア", electricE));
            mapList.Add(new WildAreaMap("巨人の鏡池 D", waterE));
            mapList.Add(new WildAreaMap("巨人の鏡池 D レア", waterG));
            mapList.Add(new WildAreaMap("巨人の鏡池 E", grassC));
            mapList.Add(new WildAreaMap("巨人の鏡池 E レア", ghostB));

            mapList.Add(new WildAreaMap("ナックル丘陵 A", poisonB));
            mapList.Add(new WildAreaMap("ナックル丘陵 A レア", electricD));
            mapList.Add(new WildAreaMap("ナックル丘陵 B", steelB));
            mapList.Add(new WildAreaMap("ナックル丘陵 B レア", steelC));
            mapList.Add(new WildAreaMap("ナックル丘陵 C", iceC));
            mapList.Add(new WildAreaMap("ナックル丘陵 C レア", iceE));
            mapList.Add(new WildAreaMap("ナックル丘陵 D", fairyA));
            mapList.Add(new WildAreaMap("ナックル丘陵 D レア", fairyC));
            mapList.Add(new WildAreaMap("ナックル丘陵 E", fireB));
            mapList.Add(new WildAreaMap("ナックル丘陵 E レア", fireD));
            mapList.Add(new WildAreaMap("ナックル丘陵 F", flyingB));
            mapList.Add(new WildAreaMap("ナックル丘陵 F レア", flyingC));
            mapList.Add(new WildAreaMap("ナックル丘陵 G", psychicC));
            mapList.Add(new WildAreaMap("ナックル丘陵 G レア", psychicE));

            mapList.Add(new WildAreaMap("げきりんの湖 A", fireB));
            mapList.Add(new WildAreaMap("げきりんの湖 A レア", fireG));
            mapList.Add(new WildAreaMap("げきりんの湖 B", fairyB));
            mapList.Add(new WildAreaMap("げきりんの湖 B レア", fairyD));
            mapList.Add(new WildAreaMap("げきりんの湖 C", waterB));
            mapList.Add(new WildAreaMap("げきりんの湖 C レア", waterF));
            mapList.Add(new WildAreaMap("げきりんの湖 D", electricB));
            mapList.Add(new WildAreaMap("げきりんの湖 D レア", electricC));

            mapList.Add(new WildAreaMap("イベント", new RaidBattleSlot[][] {
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", true, 1), new RaidBattleSlot("ヌオー", true, 2), new RaidBattleSlot("ヌオー", true, 3), new RaidBattleSlot("ヌオー", true, 4), new RaidBattleSlot("ヌオー", true, 5) },
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", true, 1), new RaidBattleSlot("ヌオー", true, 2), new RaidBattleSlot("ヌオー", true, 3), new RaidBattleSlot("ヌオー", true, 4), new RaidBattleSlot("ヌオー", true, 5) },
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", true, 1), new RaidBattleSlot("ヌオー", true, 2), new RaidBattleSlot("ヌオー", true, 3), new RaidBattleSlot("ヌオー", true, 4), new RaidBattleSlot("ヌオー", true, 5) },
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", true, 1), new RaidBattleSlot("ヌオー", true, 2), new RaidBattleSlot("ヌオー", true, 3), new RaidBattleSlot("ヌオー", true, 4), new RaidBattleSlot("ヌオー", true, 5) },
                new RaidBattleSlot[] { new RaidBattleSlot("ヌオー", true, 1), new RaidBattleSlot("ヌオー", true, 2), new RaidBattleSlot("ヌオー", true, 3), new RaidBattleSlot("ヌオー", true, 4), new RaidBattleSlot("ヌオー", true, 5) },
            }));
            #endregion

            MapList = mapList.ToArray();
        }
    }
}
