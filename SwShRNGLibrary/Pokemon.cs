using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{

    public class Pokemon
    {
        public class Species
        {
            public readonly int DexID;
            public readonly int GalarDexID;
            public readonly string Name;
            public readonly uint[] BS;
            public readonly string[] Ability;
            public readonly (PokeType Type1, PokeType Type2) Type;
            public readonly GenderRatio GenderRatio;
            public readonly string FormName;

            public virtual string GetDefaultName() { return Name; }

            public Individual GetIndividual(uint Lv, uint[] IVs, uint EC, uint PID, Nature nature, uint AbilityIndex, Gender gender)
            {
                return new Individual()
                {
                    Name = Name,
                    Form = FormName,
                    EC = EC,
                    PID = PID,
                    Lv = Lv,
                    Stats = GetStats(IVs, nature, Lv),
                    IVs = IVs,
                    Nature = nature,
                    Ability = Ability[AbilityIndex],
                    Gender = gender,
                    Characteristic = GetCharacteric(IVs, EC)
                };
            }
            
            public (uint[] Min, uint[] Max) CalcIVsRange(uint[] Stats, uint Lv, Nature nature)
            {
                uint[] MinIVs = new uint[6] { 32, 32, 32, 32, 32, 32 };
                uint[] MaxIVs = new uint[6] { 32, 32, 32, 32, 32, 32 };

                double[] mag = nature.ToMagnification();

                uint stat;
                for (MinIVs[0] = 0; MinIVs[0] < 32; MinIVs[0]++)
                {
                    stat = (MinIVs[0] + BS[0] * 2) * Lv / 100 + 10 + Lv;
                    if (stat == Stats[0]) break;
                }
                if (MinIVs[0] != 32)
                {
                    for (MaxIVs[0] = MinIVs[0]; MaxIVs[0] < 32; MaxIVs[0]++)
                    {
                        stat = (MaxIVs[0] + 1 + BS[0] * 2) * Lv / 100 + 10 + Lv;
                        if (stat != Stats[0]) break;
                    }
                    MaxIVs[0] = Math.Min(MaxIVs[0], 31);
                }

                for (int i = 1; i < 6; i++)
                {
                    for (MinIVs[i] = 0; MinIVs[i] < 32; MinIVs[i]++)
                    {
                        stat = (uint)(((MinIVs[i] + BS[i] * 2) * Lv / 100 + 5) * mag[i]);
                        if (stat == Stats[i]) break;
                    }
                    if (MinIVs[i] != 32)
                    {
                        for (MaxIVs[i] = MinIVs[i]; MaxIVs[i] < 32; MaxIVs[i]++)
                        {
                            stat = (uint)(((MaxIVs[i] + 1 + BS[i] * 2) * Lv / 100 + 5) * mag[i]);
                            if (stat != Stats[i]) break;
                        }
                        MaxIVs[i] = Math.Min(MaxIVs[i], 31);
                    }
                }

                return (MinIVs, MaxIVs);
            }
            public (uint[] Min, uint[] Max) CalcIVsRange(uint[] Stats, uint[] EVs, uint Lv, Nature nature)
            {
                uint[] MinIVs = new uint[6] { 32, 32, 32, 32, 32, 32 };
                uint[] MaxIVs = new uint[6] { 32, 32, 32, 32, 32, 32 };
                EVs = EVs.Select(_ => _ / 4).ToArray();

                double[] mag = nature.ToMagnification();

                uint stat;
                for (MinIVs[0] = 0; MinIVs[0] < 32; MinIVs[0]++)
                {
                    stat = (MinIVs[0] + EVs[0] + BS[0] * 2) * Lv / 100 + 10 + Lv;
                    if (stat == Stats[0]) break;
                }
                if (MinIVs[0] != 32)
                {
                    for (MaxIVs[0] = MinIVs[0]; MaxIVs[0] < 32; MaxIVs[0]++)
                    {
                        stat = (MaxIVs[0] + EVs[0] + 1 + BS[0] * 2) * Lv / 100 + 10 + Lv;
                        if (stat != Stats[0]) break;
                    }
                    MaxIVs[0] = Math.Min(MaxIVs[0], 31);
                }

                for (int i = 1; i < 6; i++)
                {
                    for (MinIVs[i] = 0; MinIVs[i] < 32; MinIVs[i]++)
                    {
                        stat = (uint)(((MinIVs[i] + EVs[i] + BS[i] * 2) * Lv / 100 + 5) * mag[i]);
                        if (stat == Stats[i]) break;
                    }
                    if (MinIVs[i] != 32)
                    {
                        for (MaxIVs[i] = MinIVs[i]; MaxIVs[i] < 32; MaxIVs[i]++)
                        {
                            stat = (uint)(((MaxIVs[i] + EVs[i] + 1 + BS[i] * 2) * Lv / 100 + 5) * mag[i]);
                            if (stat != Stats[i]) break;
                        }
                        MaxIVs[i] = Math.Min(MaxIVs[i], 31);
                    }
                }

                return (MinIVs, MaxIVs);
            }
            private uint[] GetStats(uint[] IVs, Nature Nature = Nature.Hardy, uint Lv = 50)
            {
                uint[] stats = new uint[6];
                double[] mag = Nature.ToMagnification();

                stats[0] = (IVs[0] + BS[0] * 2) * Lv / 100 + 10 + Lv;
                if (Name == "ヌケニン") stats[0] = 1;
                for (int i = 1; i < 6; i++)
                    stats[i] = (uint)(((IVs[i] + BS[i] * 2) * Lv / 100 + 5) * mag[i]);

                return stats;
            }

            static private readonly int[] permutation = { 0, 1, 2, 5, 3, 4 };
            static private readonly string[] characteristic = { "昼寝をよくする", "暴れることが好き", "打たれ強い", "物音に敏感", "イタズラが好き", "ちょっぴり見栄っ張り" };
            private string GetCharacteric(uint[] ivs, uint ec)
            {
                if (!ivs.Contains(31u)) return ""; // Vが含まれない場合はいったん保留.

                var i = ec % 6;
                while (ivs[permutation[i]] != 31) if (++i == 6) i = 0;
                return characteristic[i];
            }

            internal Species(int dexID, int galarDexID, string name,string formName, uint[] bs, (PokeType type1, PokeType type2) type, string[] ability, GenderRatio ratio)
            {
                DexID = dexID;
                GalarDexID = galarDexID;
                Name = name;
                FormName = formName;
                BS = bs;
                Ability = ability;
                Type = type;
                GenderRatio = ratio;
            }
        }
        class AnotherForm : Species
        {
            internal AnotherForm(int dexID, int galarDexID, string name, string formName, uint[] bs, (PokeType type1, PokeType type2) type, string[] ability, GenderRatio ratio)
                : base(dexID, galarDexID, name, formName, bs, type, ability, ratio) { }
            public override string GetDefaultName()
            {
                return $"{Name}({FormName})";
            }
        }

        public class Individual
        {
            public string Name { get; internal set; }
            public string Form { get; internal set; }
            public uint Lv { get; internal set; }
            public uint EC { get; internal set; }
            public uint PID { get; internal set; }
            public Nature Nature { get; internal set; }
            public Gender Gender { get; internal set; }
            public string Ability { get; internal set; }
            public uint[] IVs { get; internal set; }
            public uint[] Stats { get; internal set; }
            public ShinyType Shiny { get; internal set; }
            public string Characteristic { get; internal set; }
            public Individual SetShinyType(ShinyType value) { Shiny = value; return this; }
            internal Individual() { }

            public static Individual Empty = GetPokemon("Dummy").GetIndividual(0, new uint[6], 0, 0, Nature.Hardy, 0, Gender.Genderless);
        }

        private static readonly IReadOnlyList<Species> UniqueList;
        private static readonly List<Species> DexData;
        private static readonly ILookup<string, Species> FormDex;
        private static readonly Dictionary<string, Species> UniqueDex;
        private static readonly Dictionary<string, Species> DexDictionary;

        private Pokemon() { }
        public static Species GetPokemon(string Name)
        {
            if (!UniqueDex.ContainsKey(Name)) throw new Exception($"{Name}は登録されていません");
            return UniqueDex[Name];
        }
        public static Species GetPokemon(string Name, string Form)
        {
            if (!DexDictionary.ContainsKey(Name + Form)) throw new Exception($"{Name + Form}は登録されていません");
            return DexDictionary[Name + Form];
        }
        public static Species GetPokemon(string Name, Language lang)
        {
            Name = Name.ToJPN(lang);
            if (!UniqueDex.ContainsKey(Name)) throw new Exception(Name);
            return UniqueDex[Name];
        }
        public static Species GetPokemon(string Name, string Form, Language lang)
        {
            Name = Name.ToJPN(lang);
            Form = Form.ToJPN(lang);
            if (!DexDictionary.ContainsKey(Name + Form)) throw new Exception(Name + Form);
            return DexDictionary[Name + Form];
        }
        public static IReadOnlyList<Species> GetAllForms(string Name)
        {
            return FormDex[Name].ToArray();
        }
        public static IReadOnlyList<Species> GetAllForms(string Name, Language lang)
        {
            Name = Name.ToJPN(lang);
            if (!FormDex.Contains(Name)) throw new Exception(Name);
            return FormDex[Name].ToArray();
        }
        public static IReadOnlyList<Species> GetUniquePokemonList()
        {
            return UniqueList.Where(_=>_.Name != "Dummy").ToArray();
        }
        public static IReadOnlyList<Species> GetAllPokemonList()
        {
            return DexData.Where(_ => _.Name != "Dummy").ToArray();
        }
        static Pokemon()
        {
            DexData = new List<Species>();

            DexData.Add(new Species(-1, -1, "Dummy", "Genderless", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.Genderless));
            DexData.Add(new Species(-1, -1, "Dummy", "MaleOnly", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(-1, -1, "Dummy", "M7F1", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.M7F1));
            DexData.Add(new Species(-1, -1, "Dummy", "M3F1", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.M3F1));
            DexData.Add(new Species(-1, -1, "Dummy", "M1F1", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.M1F1));
            DexData.Add(new Species(-1, -1, "Dummy", "M1F3", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.M1F3));
            DexData.Add(new Species(-1, -1, "Dummy", "FemaleOnly", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.FemaleOnly));

            DexData.Add(new Species(1, -1, "フシギダネ", "", new uint[] { 45, 49, 49, 65, 65, 45 }, (PokeType.Grass, PokeType.Poison), new string[] { "しんりょく", "しんりょく", "ようりょくそ" }, GenderRatio.M7F1));
            DexData.Add(new Species(2, -1, "フシギソウ", "", new uint[] { 60, 62, 63, 80, 80, 60 }, (PokeType.Grass, PokeType.Poison), new string[] { "しんりょく", "しんりょく", "ようりょくそ" }, GenderRatio.M7F1));
            DexData.Add(new Species(3, -1, "フシギバナ", "", new uint[] { 80, 82, 83, 100, 100, 80 }, (PokeType.Grass, PokeType.Poison), new string[] { "しんりょく", "しんりょく", "ようりょくそ" }, GenderRatio.M7F1));
            DexData.Add(new Species(4, 378, "ヒトカゲ", "", new uint[] { 39, 52, 43, 60, 50, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "サンパワー" }, GenderRatio.M7F1));
            DexData.Add(new Species(5, 379, "リザード", "", new uint[] { 58, 64, 58, 80, 65, 80 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "サンパワー" }, GenderRatio.M7F1));
            DexData.Add(new Species(6, 380, "リザードン", "", new uint[] { 78, 84, 78, 109, 85, 100 }, (PokeType.Fire, PokeType.Flying), new string[] { "もうか", "もうか", "サンパワー" }, GenderRatio.M7F1));
            DexData.Add(new AnotherForm(6, 380, "リザードン", "キョダイ", new uint[] { 78, 84, 78, 109, 85, 100 }, (PokeType.Fire, PokeType.Flying), new string[] { "もうか", "もうか", "サンパワー" }, GenderRatio.M7F1));
            DexData.Add(new Species(7, -1, "ゼニガメ", "", new uint[] { 44, 48, 65, 50, 64, 43 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "あめうけざら" }, GenderRatio.M7F1));
            DexData.Add(new Species(8, -1, "カメール", "", new uint[] { 59, 63, 80, 65, 80, 58 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "あめうけざら" }, GenderRatio.M7F1));
            DexData.Add(new Species(9, -1, "カメックス", "", new uint[] { 79, 83, 100, 85, 105, 78 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "あめうけざら" }, GenderRatio.M7F1));
            DexData.Add(new Species(10, 13, "キャタピー", "", new uint[] { 45, 30, 35, 20, 20, 45 }, (PokeType.Bug, PokeType.Non), new string[] { "りんぷん", "りんぷん", "にげあし" }, GenderRatio.M1F1));
            DexData.Add(new Species(11, 14, "トランセル", "", new uint[] { 50, 20, 55, 25, 25, 30 }, (PokeType.Bug, PokeType.Non), new string[] { "だっぴ", "だっぴ", "だっぴ" }, GenderRatio.M1F1));
            DexData.Add(new Species(12, 15, "バタフリー", "", new uint[] { 60, 45, 50, 90, 80, 70 }, (PokeType.Bug, PokeType.Flying), new string[] { "ふくがん", "ふくがん", "いろめがね" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(12, 15, "バタフリー", "キョダイ", new uint[] { 60, 45, 50, 90, 80, 70 }, (PokeType.Bug, PokeType.Flying), new string[] { "ふくがん", "ふくがん", "いろめがね" }, GenderRatio.M1F1));
            DexData.Add(new Species(25, 194, "ピカチュウ", "", new uint[] { 35, 55, 40, 50, 50, 90 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "ひらいしん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(25, 194, "ピカチュウ", "キョダイ", new uint[] { 35, 55, 40, 50, 50, 90 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "ひらいしん" }, GenderRatio.M1F1));
            DexData.Add(new Species(26, 195, "ライチュウ", "", new uint[] { 60, 90, 55, 90, 80, 110 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "ひらいしん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(26, 195, "ライチュウ", "アローラ", new uint[] { 60, 85, 50, 95, 85, 110 }, (PokeType.Electric, PokeType.Psychic), new string[] { "サーフテール", "サーフテール", "サーフテール" }, GenderRatio.M1F1));
            DexData.Add(new Species(35, 255, "ピッピ", "", new uint[] { 70, 45, 48, 60, 65, 35 }, (PokeType.Fairy, PokeType.Non), new string[] { "メロメロボディ", "マジックガード", "フレンドガード" }, GenderRatio.M1F3));
            DexData.Add(new Species(36, 256, "ピクシー", "", new uint[] { 95, 70, 73, 95, 90, 60 }, (PokeType.Fairy, PokeType.Non), new string[] { "メロメロボディ", "マジックガード", "てんねん" }, GenderRatio.M1F3));
            DexData.Add(new Species(37, 68, "ロコン", "", new uint[] { 38, 41, 40, 50, 65, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "もらいび", "もらいび", "ひでり" }, GenderRatio.M1F3));
            DexData.Add(new AnotherForm(37, 68, "ロコン", "アローラ", new uint[] { 38, 41, 40, 50, 65, 65 }, (PokeType.Ice, PokeType.Non), new string[] { "ゆきがくれ", "ゆきがくれ", "ゆきふらし" }, GenderRatio.M1F3));
            DexData.Add(new Species(38, 69, "キュウコン", "", new uint[] { 73, 76, 75, 81, 100, 100 }, (PokeType.Fire, PokeType.Non), new string[] { "もらいび", "もらいび", "ひでり" }, GenderRatio.M1F3));
            DexData.Add(new AnotherForm(38, 69, "キュウコン", "アローラ", new uint[] { 73, 67, 75, 81, 100, 109 }, (PokeType.Ice, PokeType.Fairy), new string[] { "ゆきがくれ", "ゆきがくれ", "ゆきふらし" }, GenderRatio.M1F3));
            DexData.Add(new Species(43, 55, "ナゾノクサ", "", new uint[] { 45, 50, 55, 75, 65, 30 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "にげあし" }, GenderRatio.M1F1));
            DexData.Add(new Species(44, 56, "クサイハナ", "", new uint[] { 60, 65, 70, 85, 75, 40 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "あくしゅう" }, GenderRatio.M1F1));
            DexData.Add(new Species(45, 57, "ラフレシア", "", new uint[] { 75, 80, 85, 110, 90, 50 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "ほうし" }, GenderRatio.M1F1));
            DexData.Add(new Species(50, 164, "ディグダ", "", new uint[] { 10, 55, 25, 35, 45, 95 }, (PokeType.Ground, PokeType.Non), new string[] { "すながくれ", "ありじごく", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(50, 164, "ディグダ", "アローラ", new uint[] { 10, 55, 30, 35, 45, 90 }, (PokeType.Ground, PokeType.Steel), new string[] { "すながくれ", "カーリーヘアー", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(51, 165, "ダグトリオ", "", new uint[] { 35, 100, 50, 50, 70, 120 }, (PokeType.Ground, PokeType.Non), new string[] { "すながくれ", "ありじごく", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(51, 165, "ダグトリオ", "アローラ", new uint[] { 35, 100, 60, 50, 70, 110 }, (PokeType.Ground, PokeType.Steel), new string[] { "すながくれ", "カーリーヘアー", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(52, 182, "ニャース", "", new uint[] { 40, 45, 35, 40, 40, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "テクニシャン", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(52, 182, "ニャース", "キョダイ", new uint[] { 40, 45, 35, 40, 40, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "テクニシャン", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(52, 182, "ニャース", "アローラ", new uint[] { 40, 35, 35, 50, 40, 90 }, (PokeType.Dark, PokeType.Non), new string[] { "ものひろい", "テクニシャン", "びびり" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(52, 182, "ニャース", "ガラル", new uint[] { 50, 65, 55, 40, 40, 40 }, (PokeType.Steel, PokeType.Non), new string[] { "ものひろい", "かたいツメ", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new Species(53, 184, "ペルシアン", "", new uint[] { 65, 70, 60, 65, 65, 115 }, (PokeType.Normal, PokeType.Non), new string[] { "じゅうなん", "テクニシャン", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(53, 184, "ペルシアン", "アローラ", new uint[] { 65, 60, 60, 75, 65, 115 }, (PokeType.Dark, PokeType.Non), new string[] { "ファーコート", "テクニシャン", "びびり" }, GenderRatio.M1F1));
            DexData.Add(new Species(58, 70, "ガーディ", "", new uint[] { 55, 70, 45, 70, 50, 60 }, (PokeType.Fire, PokeType.Non), new string[] { "いかく", "もらいび", "せいぎのこころ" }, GenderRatio.M3F1));
            DexData.Add(new Species(59, 71, "ウインディ", "", new uint[] { 90, 110, 80, 100, 80, 95 }, (PokeType.Fire, PokeType.Non), new string[] { "いかく", "もらいび", "せいぎのこころ" }, GenderRatio.M3F1));
            DexData.Add(new Species(66, 138, "ワンリキー", "", new uint[] { 70, 80, 50, 35, 35, 35 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ノーガード", "ふくつのこころ" }, GenderRatio.M3F1));
            DexData.Add(new Species(67, 139, "ゴーリキー", "", new uint[] { 80, 100, 70, 50, 60, 45 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ノーガード", "ふくつのこころ" }, GenderRatio.M3F1));
            DexData.Add(new Species(68, 140, "カイリキー", "", new uint[] { 90, 130, 80, 65, 85, 55 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ノーガード", "ふくつのこころ" }, GenderRatio.M3F1));
            DexData.Add(new AnotherForm(68, 140, "カイリキー", "キョダイ", new uint[] { 90, 130, 80, 65, 85, 55 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ノーガード", "ふくつのこころ" }, GenderRatio.M3F1));
            DexData.Add(new Species(77, 333, "ポニータ", "", new uint[] { 50, 85, 55, 65, 65, 90 }, (PokeType.Fire, PokeType.Non), new string[] { "にげあし", "もらいび", "ほのおのからだ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(77, 333, "ポニータ", "ガラル", new uint[] { 50, 85, 55, 65, 65, 90 }, (PokeType.Psychic, PokeType.Non), new string[] { "にげあし", "パステルベール", "きけんよち" }, GenderRatio.M1F1));
            DexData.Add(new Species(78, 334, "ギャロップ", "", new uint[] { 65, 100, 70, 80, 80, 105 }, (PokeType.Fire, PokeType.Non), new string[] { "にげあし", "もらいび", "ほのおのからだ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(78, 334, "ギャロップ", "ガラル", new uint[] { 65, 100, 70, 80, 80, 105 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "にげあし", "パステルベール", "きけんよち" }, GenderRatio.M1F1));
            DexData.Add(new Species(83, 218, "カモネギ", "", new uint[] { 52, 90, 55, 58, 62, 60 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "せいしんりょく", "まけんき" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(83, 218, "カモネギ", "ガラル", new uint[] { 52, 95, 55, 58, 62, 55 }, (PokeType.Fighting, PokeType.Non), new string[] { "ふくつのこころ", "ふくつのこころ", "きもったま" }, GenderRatio.M1F1));
            DexData.Add(new Species(90, 150, "シェルダー", "", new uint[] { 30, 65, 100, 45, 25, 40 }, (PokeType.Water, PokeType.Non), new string[] { "シェルアーマー", "スキルリンク", "ぼうじん" }, GenderRatio.M1F1));
            DexData.Add(new Species(91, 151, "パルシェン", "", new uint[] { 50, 95, 180, 85, 45, 70 }, (PokeType.Water, PokeType.Ice), new string[] { "シェルアーマー", "スキルリンク", "ぼうじん" }, GenderRatio.M1F1));
            DexData.Add(new Species(92, 141, "ゴース", "", new uint[] { 30, 35, 30, 100, 35, 80 }, (PokeType.Ghost, PokeType.Poison), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            DexData.Add(new Species(93, 142, "ゴースト", "", new uint[] { 45, 50, 45, 115, 55, 95 }, (PokeType.Ghost, PokeType.Poison), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            DexData.Add(new Species(94, 143, "ゲンガー", "", new uint[] { 60, 65, 60, 130, 75, 110 }, (PokeType.Ghost, PokeType.Poison), new string[] { "のろわれボディ", "のろわれボディ", "のろわれボディ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(94, 143, "ゲンガー", "キョダイ", new uint[] { 60, 65, 60, 130, 75, 110 }, (PokeType.Ghost, PokeType.Poison), new string[] { "のろわれボディ", "のろわれボディ", "のろわれボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(95, 178, "イワーク", "", new uint[] { 35, 45, 160, 30, 45, 70 }, (PokeType.Rock, PokeType.Ground), new string[] { "いしあたま", "がんじょう", "くだけるよろい" }, GenderRatio.M1F1));
            DexData.Add(new Species(98, 98, "クラブ", "", new uint[] { 30, 105, 90, 25, 25, 50 }, (PokeType.Water, PokeType.Non), new string[] { "かいりきバサミ", "シェルアーマー", "ちからずく" }, GenderRatio.M1F1));
            DexData.Add(new Species(99, 99, "キングラー", "", new uint[] { 55, 130, 115, 50, 50, 75 }, (PokeType.Water, PokeType.Non), new string[] { "かいりきバサミ", "シェルアーマー", "ちからずく" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(99, 99, "キングラー", "キョダイ", new uint[] { 55, 130, 115, 50, 50, 75 }, (PokeType.Water, PokeType.Non), new string[] { "かいりきバサミ", "シェルアーマー", "ちからずく" }, GenderRatio.M1F1));
            DexData.Add(new Species(106, 108, "サワムラー", "", new uint[] { 50, 120, 53, 35, 110, 87 }, (PokeType.Fighting, PokeType.Non), new string[] { "じゅうなん", "すてみ", "かるわざ" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(107, 109, "エビワラー", "", new uint[] { 50, 105, 79, 35, 110, 76 }, (PokeType.Fighting, PokeType.Non), new string[] { "するどいめ", "てつのこぶし", "せいしんりょく" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(109, 250, "ドガース", "", new uint[] { 40, 65, 95, 60, 45, 35 }, (PokeType.Poison, PokeType.Non), new string[] { "ふゆう", "かがくへんかガス", "あくしゅう" }, GenderRatio.M1F1));
            DexData.Add(new Species(110, 251, "マタドガス", "", new uint[] { 65, 90, 120, 85, 70, 60 }, (PokeType.Poison, PokeType.Non), new string[] { "ふゆう", "かがくへんかガス", "あくしゅう" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(110, 251, "マタドガス", "ガラル", new uint[] { 65, 90, 120, 85, 70, 60 }, (PokeType.Poison, PokeType.Fairy), new string[] { "ふゆう", "かがくへんかガス", "ミストメイカー" }, GenderRatio.M1F1));
            DexData.Add(new Species(111, 264, "サイホーン", "", new uint[] { 80, 85, 95, 30, 30, 25 }, (PokeType.Ground, PokeType.Rock), new string[] { "ひらいしん", "いしあたま", "すてみ" }, GenderRatio.M1F1));
            DexData.Add(new Species(112, 265, "サイドン", "", new uint[] { 105, 130, 120, 45, 45, 40 }, (PokeType.Ground, PokeType.Rock), new string[] { "ひらいしん", "いしあたま", "すてみ" }, GenderRatio.M1F1));
            DexData.Add(new Species(118, 146, "トサキント", "", new uint[] { 45, 67, 60, 35, 50, 63 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "みずのベール", "ひらいしん" }, GenderRatio.M1F1));
            DexData.Add(new Species(119, 147, "アズマオウ", "", new uint[] { 80, 92, 65, 65, 80, 68 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "みずのベール", "ひらいしん" }, GenderRatio.M1F1));
            DexData.Add(new Species(122, 365, "バリヤード", "", new uint[] { 40, 45, 65, 100, 120, 90 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "ぼうおん", "フィルター", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(122, 365, "バリヤード", "ガラル", new uint[] { 50, 65, 65, 90, 90, 100 }, (PokeType.Ice, PokeType.Psychic), new string[] { "ちどりあし", "バリアフリー", "アイスボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(129, 144, "コイキング", "", new uint[] { 20, 10, 55, 15, 20, 80 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "びびり" }, GenderRatio.M1F1));
            DexData.Add(new Species(130, 145, "ギャラドス", "", new uint[] { 95, 125, 79, 60, 100, 81 }, (PokeType.Water, PokeType.Flying), new string[] { "いかく", "いかく", "じしんかじょう" }, GenderRatio.M1F1));
            DexData.Add(new Species(131, 361, "ラプラス", "", new uint[] { 130, 85, 80, 85, 95, 60 }, (PokeType.Water, PokeType.Ice), new string[] { "ちょすい", "シェルアーマー", "うるおいボディ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(131, 361, "ラプラス", "キョダイ", new uint[] { 130, 85, 80, 85, 95, 60 }, (PokeType.Water, PokeType.Ice), new string[] { "ちょすい", "シェルアーマー", "うるおいボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(132, 373, "メタモン", "", new uint[] { 48, 48, 48, 48, 48, 48 }, (PokeType.Normal, PokeType.Non), new string[] { "じゅうなん", "じゅうなん", "かわりもの" }, GenderRatio.Genderless));
            DexData.Add(new Species(133, 196, "イーブイ", "", new uint[] { 55, 55, 50, 45, 65, 55 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "てきおうりょく", "きけんよち" }, GenderRatio.M7F1));
            DexData.Add(new AnotherForm(133, 196, "イーブイ", "キョダイ", new uint[] { 55, 55, 50, 45, 65, 55 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "てきおうりょく", "きけんよち" }, GenderRatio.M7F1));
            DexData.Add(new Species(134, 197, "シャワーズ", "", new uint[] { 130, 65, 60, 110, 95, 65 }, (PokeType.Water, PokeType.Non), new string[] { "ちょすい", "ちょすい", "うるおいボディ" }, GenderRatio.M7F1));
            DexData.Add(new Species(135, 198, "サンダース", "", new uint[] { 65, 65, 60, 110, 95, 130 }, (PokeType.Electric, PokeType.Non), new string[] { "ちくでん", "ちくでん", "はやあし" }, GenderRatio.M7F1));
            DexData.Add(new Species(136, 199, "ブースター", "", new uint[] { 65, 130, 60, 95, 110, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "もらいび", "もらいび", "こんじょう" }, GenderRatio.M7F1));
            DexData.Add(new Species(143, 261, "カビゴン", "", new uint[] { 160, 110, 65, 65, 110, 30 }, (PokeType.Normal, PokeType.Non), new string[] { "めんえき", "あついしぼう", "くいしんぼう" }, GenderRatio.M7F1));
            DexData.Add(new AnotherForm(143, 261, "カビゴン", "キョダイ", new uint[] { 160, 110, 65, 65, 110, 30 }, (PokeType.Normal, PokeType.Non), new string[] { "めんえき", "あついしぼう", "くいしんぼう" }, GenderRatio.M7F1));
            DexData.Add(new Species(150, -1, "ミュウツー", "", new uint[] { 106, 110, 90, 154, 90, 130 }, (PokeType.Psychic, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "きんちょうかん" }, GenderRatio.Genderless));
            DexData.Add(new Species(151, -1, "ミュウ", "", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Psychic, PokeType.Non), new string[] { "シンクロ", "シンクロ", "シンクロ" }, GenderRatio.Genderless));
            DexData.Add(new Species(163, 19, "ホーホー", "", new uint[] { 60, 30, 30, 36, 56, 50 }, (PokeType.Normal, PokeType.Flying), new string[] { "ふみん", "するどいめ", "いろめがね" }, GenderRatio.M1F1));
            DexData.Add(new Species(164, 20, "ヨルノズク", "", new uint[] { 100, 50, 50, 86, 96, 70 }, (PokeType.Normal, PokeType.Flying), new string[] { "ふみん", "するどいめ", "いろめがね" }, GenderRatio.M1F1));
            DexData.Add(new Species(170, 220, "チョンチー", "", new uint[] { 75, 38, 38, 56, 56, 67 }, (PokeType.Water, PokeType.Electric), new string[] { "ちくでん", "はっこう", "ちょすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(171, 221, "ランターン", "", new uint[] { 125, 58, 58, 76, 76, 67 }, (PokeType.Water, PokeType.Electric), new string[] { "ちくでん", "はっこう", "ちょすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(172, 193, "ピチュー", "", new uint[] { 20, 40, 15, 35, 35, 60 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "ひらいしん" }, GenderRatio.M1F1));
            DexData.Add(new Species(173, 254, "ピィ", "", new uint[] { 50, 25, 28, 45, 55, 15 }, (PokeType.Fairy, PokeType.Non), new string[] { "メロメロボディ", "マジックガード", "フレンドガード" }, GenderRatio.M1F3));
            DexData.Add(new Species(175, 257, "トゲピー", "", new uint[] { 35, 20, 65, 40, 65, 20 }, (PokeType.Fairy, PokeType.Non), new string[] { "はりきり", "てんのめぐみ", "きょううん" }, GenderRatio.M7F1));
            DexData.Add(new Species(176, 258, "トゲチック", "", new uint[] { 55, 40, 85, 80, 105, 40 }, (PokeType.Fairy, PokeType.Flying), new string[] { "はりきり", "てんのめぐみ", "きょううん" }, GenderRatio.M7F1));
            DexData.Add(new Species(177, 92, "ネイティ", "", new uint[] { 40, 50, 45, 70, 45, 70 }, (PokeType.Psychic, PokeType.Flying), new string[] { "シンクロ", "はやおき", "マジックミラー" }, GenderRatio.M1F1));
            DexData.Add(new Species(178, 93, "ネイティオ", "", new uint[] { 65, 75, 70, 95, 70, 95 }, (PokeType.Psychic, PokeType.Flying), new string[] { "シンクロ", "はやおき", "マジックミラー" }, GenderRatio.M1F1));
            DexData.Add(new Species(182, 58, "キレイハナ", "", new uint[] { 75, 80, 95, 90, 100, 50 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "ようりょくそ", "いやしのこころ" }, GenderRatio.M1F1));
            DexData.Add(new Species(185, 253, "ウソッキー", "", new uint[] { 70, 100, 115, 30, 65, 30 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "いしあたま", "びびり" }, GenderRatio.M1F1));
            DexData.Add(new Species(194, 100, "ウパー", "", new uint[] { 55, 45, 45, 25, 25, 15 }, (PokeType.Water, PokeType.Ground), new string[] { "しめりけ", "ちょすい", "てんねん" }, GenderRatio.M1F1));
            DexData.Add(new Species(195, 101, "ヌオー", "", new uint[] { 95, 85, 85, 65, 65, 35 }, (PokeType.Water, PokeType.Ground), new string[] { "しめりけ", "ちょすい", "てんねん" }, GenderRatio.M1F1));
            DexData.Add(new Species(196, 200, "エーフィ", "", new uint[] { 65, 65, 60, 130, 95, 110 }, (PokeType.Psychic, PokeType.Non), new string[] { "シンクロ", "シンクロ", "マジックミラー" }, GenderRatio.M7F1));
            DexData.Add(new Species(197, 201, "ブラッキー", "", new uint[] { 95, 65, 110, 60, 130, 65 }, (PokeType.Dark, PokeType.Non), new string[] { "シンクロ", "シンクロ", "せいしんりょく" }, GenderRatio.M7F1));
            DexData.Add(new Species(202, 217, "ソーナンス", "", new uint[] { 190, 33, 58, 33, 58, 33 }, (PokeType.Psychic, PokeType.Non), new string[] { "かげふみ", "かげふみ", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(208, 179, "ハガネール", "", new uint[] { 75, 85, 200, 55, 65, 30 }, (PokeType.Steel, PokeType.Ground), new string[] { "いしあたま", "がんじょう", "ちからずく" }, GenderRatio.M1F1));
            DexData.Add(new Species(211, 304, "ハリーセン", "", new uint[] { 65, 95, 85, 55, 55, 85 }, (PokeType.Water, PokeType.Poison), new string[] { "どくのトゲ", "すいすい", "いかく" }, GenderRatio.M1F1));
            DexData.Add(new Species(213, 227, "ツボツボ", "", new uint[] { 20, 10, 230, 10, 230, 5 }, (PokeType.Bug, PokeType.Rock), new string[] { "がんじょう", "くいしんぼう", "あまのじゃく" }, GenderRatio.M1F1));
            DexData.Add(new Species(215, 292, "ニューラ", "", new uint[] { 55, 95, 55, 35, 75, 115 }, (PokeType.Dark, PokeType.Ice), new string[] { "せいしんりょく", "するどいめ", "わるいてぐせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(220, 75, "ウリムー", "", new uint[] { 50, 50, 40, 30, 30, 50 }, (PokeType.Ice, PokeType.Ground), new string[] { "どんかん", "ゆきがくれ", "あついしぼう" }, GenderRatio.M1F1));
            DexData.Add(new Species(221, 76, "イノムー", "", new uint[] { 100, 100, 80, 60, 60, 50 }, (PokeType.Ice, PokeType.Ground), new string[] { "どんかん", "ゆきがくれ", "あついしぼう" }, GenderRatio.M1F1));
            DexData.Add(new Species(222, 236, "サニーゴ", "", new uint[] { 65, 55, 95, 65, 95, 35 }, (PokeType.Water, PokeType.Rock), new string[] { "はりきり", "しぜんかいふく", "さいせいりょく" }, GenderRatio.M1F3));
            DexData.Add(new AnotherForm(222, 236, "サニーゴ", "ガラル", new uint[] { 60, 55, 100, 65, 100, 30 }, (PokeType.Ghost, PokeType.Non), new string[] { "くだけるよろい", "くだけるよろい", "のろわれボディ" }, GenderRatio.M1F3));
            DexData.Add(new Species(223, 148, "テッポウオ", "", new uint[] { 35, 65, 35, 65, 35, 65 }, (PokeType.Water, PokeType.Non), new string[] { "はりきり", "スナイパー", "ムラっけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(224, 149, "オクタン", "", new uint[] { 75, 105, 75, 105, 75, 45 }, (PokeType.Water, PokeType.Non), new string[] { "きゅうばん", "スナイパー", "ムラっけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(225, 78, "デリバード", "", new uint[] { 45, 55, 45, 65, 45, 75 }, (PokeType.Ice, PokeType.Flying), new string[] { "やるき", "はりきり", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new Species(226, 355, "マンタイン", "", new uint[] { 85, 40, 70, 80, 140, 70 }, (PokeType.Water, PokeType.Flying), new string[] { "すいすい", "ちょすい", "みずのベール" }, GenderRatio.M1F1));
            DexData.Add(new Species(236, 107, "バルキー", "", new uint[] { 35, 35, 35, 35, 35, 35 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ふくつのこころ", "やるき" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(237, 110, "カポエラー", "", new uint[] { 50, 95, 95, 35, 110, 70 }, (PokeType.Fighting, PokeType.Non), new string[] { "いかく", "テクニシャン", "ふくつのこころ" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(246, 383, "ヨーギラス", "", new uint[] { 50, 64, 50, 45, 50, 41 }, (PokeType.Rock, PokeType.Ground), new string[] { "こんじょう", "こんじょう", "すながくれ" }, GenderRatio.M1F1));
            DexData.Add(new Species(247, 384, "サナギラス", "", new uint[] { 70, 84, 70, 65, 70, 51 }, (PokeType.Rock, PokeType.Ground), new string[] { "だっぴ", "だっぴ", "だっぴ" }, GenderRatio.M1F1));
            DexData.Add(new Species(248, 385, "バンギラス", "", new uint[] { 100, 134, 110, 95, 100, 61 }, (PokeType.Rock, PokeType.Dark), new string[] { "すなおこし", "すなおこし", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new Species(251, -1, "セレビィ", "", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Psychic, PokeType.Grass), new string[] { "しぜんかいふく", "しぜんかいふく", "しぜんかいふく" }, GenderRatio.Genderless));
            DexData.Add(new Species(263, 31, "ジグザグマ", "", new uint[] { 38, 30, 41, 30, 41, 60 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "くいしんぼう", "はやあし" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(263, 31, "ジグザグマ", "ガラル", new uint[] { 38, 30, 41, 30, 41, 60 }, (PokeType.Dark, PokeType.Normal), new string[] { "ものひろい", "くいしんぼう", "はやあし" }, GenderRatio.M1F1));
            DexData.Add(new Species(264, 32, "マッスグマ", "", new uint[] { 78, 70, 61, 50, 61, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "くいしんぼう", "はやあし" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(264, 32, "マッスグマ", "ガラル", new uint[] { 78, 70, 61, 50, 61, 100 }, (PokeType.Dark, PokeType.Normal), new string[] { "ものひろい", "くいしんぼう", "はやあし" }, GenderRatio.M1F1));
            DexData.Add(new Species(270, 36, "ハスボー", "", new uint[] { 40, 30, 30, 40, 50, 30 }, (PokeType.Water, PokeType.Grass), new string[] { "すいすい", "あめうけざら", "マイペース" }, GenderRatio.M1F1));
            DexData.Add(new Species(271, 37, "ハスブレロ", "", new uint[] { 60, 50, 50, 60, 70, 50 }, (PokeType.Water, PokeType.Grass), new string[] { "すいすい", "あめうけざら", "マイペース" }, GenderRatio.M1F1));
            DexData.Add(new Species(272, 38, "ルンパッパ", "", new uint[] { 80, 70, 70, 90, 100, 70 }, (PokeType.Water, PokeType.Grass), new string[] { "すいすい", "あめうけざら", "マイペース" }, GenderRatio.M1F1));
            DexData.Add(new Species(273, 39, "タネボー", "", new uint[] { 40, 40, 50, 30, 30, 30 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "はやおき", "わるいてぐせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(274, 40, "コノハナ", "", new uint[] { 70, 70, 40, 60, 40, 60 }, (PokeType.Grass, PokeType.Dark), new string[] { "ようりょくそ", "はやおき", "わるいてぐせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(275, 41, "ダーテング", "", new uint[] { 90, 100, 60, 90, 60, 80 }, (PokeType.Grass, PokeType.Dark), new string[] { "ようりょくそ", "はやおき", "わるいてぐせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(278, 62, "キャモメ", "", new uint[] { 40, 30, 30, 55, 30, 85 }, (PokeType.Water, PokeType.Flying), new string[] { "するどいめ", "うるおいボディ", "あめうけざら" }, GenderRatio.M1F1));
            DexData.Add(new Species(279, 63, "ペリッパー", "", new uint[] { 60, 50, 100, 95, 70, 65 }, (PokeType.Water, PokeType.Flying), new string[] { "するどいめ", "あめふらし", "あめうけざら" }, GenderRatio.M1F1));
            DexData.Add(new Species(280, 120, "ラルトス", "", new uint[] { 28, 25, 25, 45, 35, 40 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "シンクロ", "トレース", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(281, 121, "キルリア", "", new uint[] { 38, 35, 35, 65, 55, 50 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "シンクロ", "トレース", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(282, 122, "サーナイト", "", new uint[] { 68, 65, 65, 125, 115, 80 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "シンクロ", "トレース", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(290, 104, "ツチニン", "", new uint[] { 31, 45, 90, 30, 30, 40 }, (PokeType.Bug, PokeType.Ground), new string[] { "ふくがん", "ふくがん", "にげあし" }, GenderRatio.M1F1));
            DexData.Add(new Species(291, 105, "テッカニン", "", new uint[] { 61, 90, 45, 50, 50, 160 }, (PokeType.Bug, PokeType.Flying), new string[] { "かそく", "かそく", "すりぬけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(292, 106, "ヌケニン", "", new uint[] { 1, 90, 45, 30, 30, 40 }, (PokeType.Bug, PokeType.Ghost), new string[] { "ふしぎなまもり", "ふしぎなまもり", "ふしぎなまもり" }, GenderRatio.Genderless));
            DexData.Add(new Species(302, 294, "ヤミラミ", "", new uint[] { 50, 75, 75, 65, 65, 50 }, (PokeType.Dark, PokeType.Ghost), new string[] { "するどいめ", "あとだし", "いたずらごころ" }, GenderRatio.M1F1));
            DexData.Add(new Species(303, 295, "クチート", "", new uint[] { 50, 85, 85, 55, 55, 50 }, (PokeType.Steel, PokeType.Fairy), new string[] { "かいりきバサミ", "いかく", "ちからずく" }, GenderRatio.M1F1));
            DexData.Add(new Species(309, 66, "ラクライ", "", new uint[] { 40, 45, 40, 65, 40, 65 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "ひらいしん", "マイナス" }, GenderRatio.M1F1));
            DexData.Add(new Species(310, 67, "ライボルト", "", new uint[] { 70, 75, 60, 105, 60, 105 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "ひらいしん", "マイナス" }, GenderRatio.M1F1));
            DexData.Add(new Species(315, 60, "ロゼリア", "", new uint[] { 50, 60, 45, 100, 80, 65 }, (PokeType.Grass, PokeType.Poison), new string[] { "しぜんかいふく", "どくのトゲ", "リーフガード" }, GenderRatio.M1F1));
            DexData.Add(new Species(320, 356, "ホエルコ", "", new uint[] { 130, 70, 35, 70, 35, 60 }, (PokeType.Water, PokeType.Non), new string[] { "みずのベール", "どんかん", "プレッシャー" }, GenderRatio.M1F1));
            DexData.Add(new Species(321, 357, "ホエルオー", "", new uint[] { 170, 90, 45, 90, 45, 60 }, (PokeType.Water, PokeType.Non), new string[] { "みずのベール", "どんかん", "プレッシャー" }, GenderRatio.M1F1));
            DexData.Add(new Species(324, 300, "コータス", "", new uint[] { 70, 85, 140, 85, 70, 20 }, (PokeType.Fire, PokeType.Non), new string[] { "しろいけむり", "ひでり", "シェルアーマー" }, GenderRatio.M1F1));
            DexData.Add(new Species(328, 321, "ナックラー", "", new uint[] { 45, 100, 45, 45, 45, 10 }, (PokeType.Ground, PokeType.Non), new string[] { "かいりきバサミ", "ありじごく", "ちからずく" }, GenderRatio.M1F1));
            DexData.Add(new Species(329, 322, "ビブラーバ", "", new uint[] { 50, 70, 50, 50, 50, 70 }, (PokeType.Ground, PokeType.Dragon), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            DexData.Add(new Species(330, 323, "フライゴン", "", new uint[] { 80, 100, 80, 80, 80, 100 }, (PokeType.Ground, PokeType.Dragon), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            DexData.Add(new Species(337, 362, "ルナトーン", "", new uint[] { 90, 55, 65, 95, 85, 70 }, (PokeType.Rock, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new Species(338, 363, "ソルロック", "", new uint[] { 90, 95, 85, 55, 65, 70 }, (PokeType.Rock, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new Species(339, 228, "ドジョッチ", "", new uint[] { 50, 48, 43, 46, 41, 60 }, (PokeType.Water, PokeType.Ground), new string[] { "どんかん", "きけんよち", "うるおいボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(340, 229, "ナマズン", "", new uint[] { 110, 78, 73, 76, 71, 60 }, (PokeType.Water, PokeType.Ground), new string[] { "どんかん", "きけんよち", "うるおいボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(341, 102, "ヘイガニ", "", new uint[] { 43, 80, 65, 50, 35, 35 }, (PokeType.Water, PokeType.Non), new string[] { "かいりきバサミ", "シェルアーマー", "てきおうりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(342, 103, "シザリガー", "", new uint[] { 63, 120, 85, 90, 55, 55 }, (PokeType.Water, PokeType.Dark), new string[] { "かいりきバサミ", "シェルアーマー", "てきおうりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(343, 82, "ヤジロン", "", new uint[] { 40, 40, 55, 40, 70, 55 }, (PokeType.Ground, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new Species(344, 83, "ネンドール", "", new uint[] { 60, 70, 105, 70, 120, 75 }, (PokeType.Ground, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new Species(349, 152, "ヒンバス", "", new uint[] { 20, 15, 20, 10, 55, 80 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "どんかん", "てきおうりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(350, 153, "ミロカロス", "", new uint[] { 95, 60, 79, 100, 125, 81 }, (PokeType.Water, PokeType.Non), new string[] { "ふしぎなうろこ", "かちき", "メロメロボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(355, 135, "ヨマワル", "", new uint[] { 20, 40, 90, 30, 90, 25 }, (PokeType.Ghost, PokeType.Non), new string[] { "ふゆう", "ふゆう", "おみとおし" }, GenderRatio.M1F1));
            DexData.Add(new Species(356, 136, "サマヨール", "", new uint[] { 40, 70, 130, 60, 130, 25 }, (PokeType.Ghost, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "おみとおし" }, GenderRatio.M1F1));
            DexData.Add(new Species(360, 216, "ソーナノ", "", new uint[] { 95, 23, 48, 23, 48, 23 }, (PokeType.Psychic, PokeType.Non), new string[] { "かげふみ", "かげふみ", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(361, 79, "ユキワラシ", "", new uint[] { 50, 50, 50, 50, 50, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "せいしんりょく", "アイスボディ", "ムラっけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(362, 80, "オニゴーリ", "", new uint[] { 80, 80, 80, 80, 80, 80 }, (PokeType.Ice, PokeType.Non), new string[] { "せいしんりょく", "アイスボディ", "ムラっけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(385, -1, "ジラーチ", "", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Steel, PokeType.Psychic), new string[] { "てんのめぐみ", "てんのめぐみ", "てんのめぐみ" }, GenderRatio.Genderless));
            DexData.Add(new Species(406, 59, "スボミー", "", new uint[] { 40, 30, 35, 50, 70, 55 }, (PokeType.Grass, PokeType.Poison), new string[] { "しぜんかいふく", "どくのトゲ", "リーフガード" }, GenderRatio.M1F1));
            DexData.Add(new Species(407, 61, "ロズレイド", "", new uint[] { 60, 70, 65, 125, 105, 90 }, (PokeType.Grass, PokeType.Poison), new string[] { "しぜんかいふく", "どくのトゲ", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new Species(415, 116, "ミツハニー", "", new uint[] { 30, 30, 42, 30, 42, 70 }, (PokeType.Bug, PokeType.Flying), new string[] { "みつあつめ", "みつあつめ", "はりきり" }, GenderRatio.M7F1));
            DexData.Add(new Species(416, 117, "ビークイン", "", new uint[] { 70, 80, 102, 80, 102, 40 }, (PokeType.Bug, PokeType.Flying), new string[] { "プレッシャー", "プレッシャー", "きんちょうかん" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(420, 128, "チェリンボ", "", new uint[] { 45, 35, 45, 62, 53, 35 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "ようりょくそ", "ようりょくそ" }, GenderRatio.M1F1));
            DexData.Add(new Species(421, 129, "チェリム", "", new uint[] { 70, 60, 70, 87, 78, 85 }, (PokeType.Grass, PokeType.Non), new string[] { "フラワーギフト", "フラワーギフト", "フラワーギフト" }, GenderRatio.M1F1));
            DexData.Add(new Species(422, 230, "カラナクシ", "", new uint[] { 76, 48, 48, 57, 62, 34 }, (PokeType.Water, PokeType.Non), new string[] { "ねんちゃく", "よびみず", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(423, 231, "トリトドン", "", new uint[] { 111, 83, 68, 92, 82, 39 }, (PokeType.Water, PokeType.Ground), new string[] { "ねんちゃく", "よびみず", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(425, 124, "フワンテ", "", new uint[] { 90, 50, 34, 60, 44, 70 }, (PokeType.Ghost, PokeType.Flying), new string[] { "ゆうばく", "かるわざ", "ねつぼうそう" }, GenderRatio.M1F1));
            DexData.Add(new Species(426, 125, "フワライド", "", new uint[] { 150, 80, 44, 90, 54, 80 }, (PokeType.Ghost, PokeType.Flying), new string[] { "ゆうばく", "かるわざ", "ねつぼうそう" }, GenderRatio.M1F1));
            DexData.Add(new Species(434, 130, "スカンプー", "", new uint[] { 63, 63, 47, 41, 41, 74 }, (PokeType.Poison, PokeType.Dark), new string[] { "あくしゅう", "ゆうばく", "するどいめ" }, GenderRatio.M1F1));
            DexData.Add(new Species(435, 131, "スカタンク", "", new uint[] { 103, 93, 67, 71, 61, 84 }, (PokeType.Poison, PokeType.Dark), new string[] { "あくしゅう", "ゆうばく", "するどいめ" }, GenderRatio.M1F1));
            DexData.Add(new Species(436, 118, "ドーミラー", "", new uint[] { 57, 24, 86, 24, 86, 23 }, (PokeType.Steel, PokeType.Psychic), new string[] { "ふゆう", "たいねつ", "ヘヴィメタル" }, GenderRatio.Genderless));
            DexData.Add(new Species(437, 119, "ドータクン", "", new uint[] { 67, 89, 116, 79, 116, 33 }, (PokeType.Steel, PokeType.Psychic), new string[] { "ふゆう", "たいねつ", "ヘヴィメタル" }, GenderRatio.Genderless));
            DexData.Add(new Species(438, 252, "ウソハチ", "", new uint[] { 50, 80, 95, 10, 45, 10 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "いしあたま", "びびり" }, GenderRatio.M1F1));
            DexData.Add(new Species(439, 364, "マネネ", "", new uint[] { 20, 25, 45, 70, 90, 60 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "ぼうおん", "フィルター", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new Species(446, 260, "ゴンベ", "", new uint[] { 135, 85, 40, 40, 85, 5 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "あついしぼう", "くいしんぼう" }, GenderRatio.M7F1));
            DexData.Add(new Species(447, 298, "リオル", "", new uint[] { 40, 70, 40, 35, 40, 60 }, (PokeType.Fighting, PokeType.Non), new string[] { "ふくつのこころ", "せいしんりょく", "いたずらごころ" }, GenderRatio.M7F1));
            DexData.Add(new Species(448, 299, "ルカリオ", "", new uint[] { 70, 110, 70, 115, 70, 90 }, (PokeType.Fighting, PokeType.Steel), new string[] { "ふくつのこころ", "せいしんりょく", "せいぎのこころ" }, GenderRatio.M7F1));
            DexData.Add(new Species(449, 314, "ヒポポタス", "", new uint[] { 68, 72, 78, 38, 42, 32 }, (PokeType.Ground, PokeType.Non), new string[] { "すなおこし", "すなおこし", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(450, 315, "カバルドン", "", new uint[] { 108, 112, 118, 68, 72, 47 }, (PokeType.Ground, PokeType.Non), new string[] { "すなおこし", "すなおこし", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(451, 285, "スコルピ", "", new uint[] { 40, 50, 90, 30, 55, 65 }, (PokeType.Poison, PokeType.Bug), new string[] { "カブトアーマー", "スナイパー", "するどいめ" }, GenderRatio.M1F1));
            DexData.Add(new Species(452, 286, "ドラピオン", "", new uint[] { 70, 90, 110, 60, 75, 95 }, (PokeType.Poison, PokeType.Dark), new string[] { "カブトアーマー", "スナイパー", "するどいめ" }, GenderRatio.M1F1));
            DexData.Add(new Species(453, 222, "グレッグル", "", new uint[] { 48, 61, 40, 61, 40, 50 }, (PokeType.Poison, PokeType.Fighting), new string[] { "きけんよち", "かんそうはだ", "どくしゅ" }, GenderRatio.M1F1));
            DexData.Add(new Species(454, 223, "ドクロッグ", "", new uint[] { 83, 106, 65, 86, 65, 85 }, (PokeType.Poison, PokeType.Fighting), new string[] { "きけんよち", "かんそうはだ", "どくしゅ" }, GenderRatio.M1F1));
            DexData.Add(new Species(458, 354, "タマンタ", "", new uint[] { 45, 20, 50, 60, 120, 50 }, (PokeType.Water, PokeType.Flying), new string[] { "すいすい", "ちょすい", "みずのベール" }, GenderRatio.M1F1));
            DexData.Add(new Species(459, 96, "ユキカブリ", "", new uint[] { 60, 62, 50, 62, 60, 40 }, (PokeType.Grass, PokeType.Ice), new string[] { "ゆきふらし", "ゆきふらし", "ぼうおん" }, GenderRatio.M1F1));
            DexData.Add(new Species(460, 97, "ユキノオー", "", new uint[] { 90, 92, 75, 92, 85, 60 }, (PokeType.Grass, PokeType.Ice), new string[] { "ゆきふらし", "ゆきふらし", "ぼうおん" }, GenderRatio.M1F1));
            DexData.Add(new Species(461, 293, "マニューラ", "", new uint[] { 70, 120, 65, 45, 85, 125 }, (PokeType.Dark, PokeType.Ice), new string[] { "プレッシャー", "プレッシャー", "わるいてぐせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(464, 266, "ドサイドン", "", new uint[] { 115, 140, 130, 55, 55, 40 }, (PokeType.Ground, PokeType.Rock), new string[] { "ひらいしん", "ハードロック", "すてみ" }, GenderRatio.M1F1));
            DexData.Add(new Species(468, 259, "トゲキッス", "", new uint[] { 85, 50, 95, 120, 115, 80 }, (PokeType.Fairy, PokeType.Flying), new string[] { "はりきり", "てんのめぐみ", "きょううん" }, GenderRatio.M7F1));
            DexData.Add(new Species(470, 202, "リーフィア", "", new uint[] { 65, 110, 130, 60, 65, 95 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "リーフガード", "ようりょくそ" }, GenderRatio.M7F1));
            DexData.Add(new Species(471, 203, "グレイシア", "", new uint[] { 65, 60, 110, 130, 95, 65 }, (PokeType.Ice, PokeType.Non), new string[] { "ゆきがくれ", "ゆきがくれ", "アイスボディ" }, GenderRatio.M7F1));
            DexData.Add(new Species(473, 77, "マンムー", "", new uint[] { 110, 130, 80, 70, 60, 80 }, (PokeType.Ice, PokeType.Ground), new string[] { "どんかん", "ゆきがくれ", "あついしぼう" }, GenderRatio.M1F1));
            DexData.Add(new Species(475, 123, "エルレイド", "", new uint[] { 68, 125, 65, 65, 115, 80 }, (PokeType.Psychic, PokeType.Fighting), new string[] { "ふくつのこころ", "ふくつのこころ", "せいぎのこころ" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(477, 137, "ヨノワール", "", new uint[] { 45, 100, 135, 65, 135, 45 }, (PokeType.Ghost, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "おみとおし" }, GenderRatio.M1F1));
            DexData.Add(new Species(478, 81, "ユキメノコ", "", new uint[] { 70, 80, 70, 80, 70, 110 }, (PokeType.Ice, PokeType.Ghost), new string[] { "ゆきがくれ", "ゆきがくれ", "のろわれボディ" }, GenderRatio.FemaleOnly));
            DexData.Add(new AnotherForm(479, 372, "ロトム", "ノーマル", new uint[] { 50, 50, 77, 95, 77, 91 }, (PokeType.Electric, PokeType.Ghost), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(479, 372, "ロトム", "ヒート", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Fire), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(479, 372, "ロトム", "ウォッシュ", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Water), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(479, 372, "ロトム", "フロスト", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Ice), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(479, 372, "ロトム", "スピン", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Flying), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(479, 372, "ロトム", "カット", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Grass), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            DexData.Add(new Species(509, 44, "チョロネコ", "", new uint[] { 41, 50, 37, 50, 37, 66 }, (PokeType.Dark, PokeType.Non), new string[] { "じゅうなん", "かるわざ", "いたずらごころ" }, GenderRatio.M1F1));
            DexData.Add(new Species(510, 45, "レパルダス", "", new uint[] { 64, 88, 50, 88, 50, 106 }, (PokeType.Dark, PokeType.Non), new string[] { "じゅうなん", "かるわざ", "いたずらごころ" }, GenderRatio.M1F1));
            DexData.Add(new Species(517, 90, "ムンナ", "", new uint[] { 76, 25, 45, 67, 55, 24 }, (PokeType.Psychic, PokeType.Non), new string[] { "よちむ", "シンクロ", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(518, 91, "ムシャーナ", "", new uint[] { 116, 55, 85, 107, 95, 29 }, (PokeType.Psychic, PokeType.Non), new string[] { "よちむ", "シンクロ", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(519, 26, "マメパト", "", new uint[] { 50, 55, 50, 36, 30, 43 }, (PokeType.Normal, PokeType.Flying), new string[] { "はとむね", "きょううん", "とうそうしん" }, GenderRatio.M1F1));
            DexData.Add(new Species(520, 27, "ハトーボー", "", new uint[] { 62, 77, 62, 50, 42, 65 }, (PokeType.Normal, PokeType.Flying), new string[] { "はとむね", "きょううん", "とうそうしん" }, GenderRatio.M1F1));
            DexData.Add(new Species(521, 28, "ケンホロウ", "", new uint[] { 80, 115, 80, 65, 55, 93 }, (PokeType.Normal, PokeType.Flying), new string[] { "はとむね", "きょううん", "とうそうしん" }, GenderRatio.M1F1));
            DexData.Add(new Species(524, 168, "ダンゴロ", "", new uint[] { 55, 75, 85, 25, 25, 15 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "くだけるよろい", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(525, 169, "ガントル", "", new uint[] { 70, 105, 105, 50, 40, 20 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "くだけるよろい", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(526, 170, "ギガイアス", "", new uint[] { 85, 135, 130, 60, 80, 25 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "すなおこし", "すなのちから" }, GenderRatio.M1F1));
            DexData.Add(new Species(527, 174, "コロモリ", "", new uint[] { 65, 45, 43, 55, 43, 72 }, (PokeType.Psychic, PokeType.Flying), new string[] { "てんねん", "ぶきよう", "たんじゅん" }, GenderRatio.M1F1));
            DexData.Add(new Species(528, 175, "ココロモリ", "", new uint[] { 67, 57, 55, 77, 55, 114 }, (PokeType.Psychic, PokeType.Flying), new string[] { "てんねん", "ぶきよう", "たんじゅん" }, GenderRatio.M1F1));
            DexData.Add(new Species(529, 166, "モグリュー", "", new uint[] { 60, 85, 40, 30, 45, 68 }, (PokeType.Ground, PokeType.Non), new string[] { "すなかき", "すなのちから", "かたやぶり" }, GenderRatio.M1F1));
            DexData.Add(new Species(530, 167, "ドリュウズ", "", new uint[] { 110, 135, 60, 50, 65, 88 }, (PokeType.Ground, PokeType.Steel), new string[] { "すなかき", "すなのちから", "かたやぶり" }, GenderRatio.M1F1));
            DexData.Add(new Species(532, 171, "ドッコラー", "", new uint[] { 75, 80, 55, 25, 35, 35 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ちからずく", "てつのこぶし" }, GenderRatio.M3F1));
            DexData.Add(new Species(533, 172, "ドテッコツ", "", new uint[] { 85, 105, 85, 40, 50, 40 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ちからずく", "てつのこぶし" }, GenderRatio.M3F1));
            DexData.Add(new Species(534, 173, "ローブシン", "", new uint[] { 105, 140, 95, 55, 65, 45 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ちからずく", "てつのこぶし" }, GenderRatio.M3F1));
            DexData.Add(new Species(535, 132, "オタマロ", "", new uint[] { 50, 50, 40, 50, 40, 64 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "うるおいボディ", "ちょすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(536, 133, "ガマガル", "", new uint[] { 75, 65, 55, 65, 55, 69 }, (PokeType.Water, PokeType.Ground), new string[] { "すいすい", "うるおいボディ", "ちょすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(537, 134, "ガマゲロゲ", "", new uint[] { 105, 95, 75, 85, 75, 74 }, (PokeType.Water, PokeType.Ground), new string[] { "すいすい", "どくしゅ", "ちょすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(538, 248, "ナゲキ", "", new uint[] { 120, 100, 85, 30, 85, 45 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "せいしんりょく", "かたやぶり" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(539, 249, "ダゲキ", "", new uint[] { 75, 125, 75, 30, 75, 85 }, (PokeType.Fighting, PokeType.Non), new string[] { "がんじょう", "せいしんりょく", "かたやぶり" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(546, 262, "モンメン", "", new uint[] { 40, 27, 60, 37, 50, 66 }, (PokeType.Grass, PokeType.Fairy), new string[] { "いたずらごころ", "すりぬけ", "ようりょくそ" }, GenderRatio.M1F1));
            DexData.Add(new Species(547, 263, "エルフーン", "", new uint[] { 60, 67, 85, 77, 75, 116 }, (PokeType.Grass, PokeType.Fairy), new string[] { "いたずらごころ", "すりぬけ", "ようりょくそ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(550, 154, "バスラオ", "あか", new uint[] { 70, 92, 65, 80, 55, 98 }, (PokeType.Water, PokeType.Non), new string[] { "すてみ", "てきおうりょく", "かたやぶり" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(550, 154, "バスラオ", "あお", new uint[] { 70, 92, 65, 80, 55, 98 }, (PokeType.Water, PokeType.Non), new string[] { "いしあたま", "てきおうりょく", "かたやぶり" }, GenderRatio.M1F1));
            DexData.Add(new Species(554, 367, "ダルマッカ", "", new uint[] { 70, 90, 45, 15, 45, 50 }, (PokeType.Fire, PokeType.Non), new string[] { "はりきり", "はりきり", "せいしんりょく" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(554, 367, "ダルマッカ", "ガラル", new uint[] { 70, 90, 45, 15, 45, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "はりきり", "はりきり", "せいしんりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(555, 368, "ヒヒダルマ", "", new uint[] { 105, 140, 55, 30, 55, 95 }, (PokeType.Fire, PokeType.Non), new string[] { "ちからずく", "ちからずく", "ダルマモード" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(555, 368, "ヒヒダルマ", "ダルマ", new uint[] { 105, 30, 105, 140, 105, 55 }, (PokeType.Fire, PokeType.Psychic), new string[] { "ちからずく", "ちからずく", "ダルマモード" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(555, 368, "ヒヒダルマ", "ガラル", new uint[] { 105, 140, 55, 30, 55, 95 }, (PokeType.Ice, PokeType.Non), new string[] { "ごりむちゅう", "ごりむちゅう", "ダルマモード" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(555, 368, "ヒヒダルマ", "ガラルダルマ", new uint[] { 105, 160, 55, 30, 55, 135 }, (PokeType.Ice, PokeType.Fire), new string[] { "ごりむちゅう", "ごりむちゅう", "ダルマモード" }, GenderRatio.M1F1));
            DexData.Add(new Species(556, 296, "マラカッチ", "", new uint[] { 75, 86, 67, 106, 67, 60 }, (PokeType.Grass, PokeType.Non), new string[] { "ちょすい", "ようりょくそ", "よびみず" }, GenderRatio.M1F1));
            DexData.Add(new Species(557, 86, "イシズマイ", "", new uint[] { 50, 65, 85, 35, 35, 55 }, (PokeType.Bug, PokeType.Rock), new string[] { "がんじょう", "シェルアーマー", "くだけるよろい" }, GenderRatio.M1F1));
            DexData.Add(new Species(558, 87, "イワパレス", "", new uint[] { 70, 105, 125, 65, 75, 45 }, (PokeType.Bug, PokeType.Rock), new string[] { "がんじょう", "シェルアーマー", "くだけるよろい" }, GenderRatio.M1F1));
            DexData.Add(new Species(559, 224, "ズルッグ", "", new uint[] { 50, 75, 70, 35, 70, 48 }, (PokeType.Dark, PokeType.Fighting), new string[] { "だっぴ", "じしんかじょう", "いかく" }, GenderRatio.M1F1));
            DexData.Add(new Species(560, 225, "ズルズキン", "", new uint[] { 65, 90, 115, 45, 115, 58 }, (PokeType.Dark, PokeType.Fighting), new string[] { "だっぴ", "じしんかじょう", "いかく" }, GenderRatio.M1F1));
            DexData.Add(new Species(561, 297, "シンボラー", "", new uint[] { 72, 58, 80, 103, 80, 97 }, (PokeType.Psychic, PokeType.Flying), new string[] { "ミラクルスキン", "マジックガード", "いろめがね" }, GenderRatio.M1F1));
            DexData.Add(new Species(562, 327, "デスマス", "", new uint[] { 38, 30, 85, 55, 65, 30 }, (PokeType.Ghost, PokeType.Non), new string[] { "ミイラ", "ミイラ", "ミイラ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(562, 327, "デスマス", "ガラル", new uint[] { 38, 55, 85, 30, 65, 30 }, (PokeType.Ground, PokeType.Ghost), new string[] { "さまようたましい", "さまようたましい", "さまようたましい" }, GenderRatio.M1F1));
            DexData.Add(new Species(563, 329, "デスカーン", "", new uint[] { 58, 50, 145, 95, 105, 30 }, (PokeType.Ghost, PokeType.Non), new string[] { "ミイラ", "ミイラ", "ミイラ" }, GenderRatio.M1F1));
            DexData.Add(new Species(568, 157, "ヤブクロン", "", new uint[] { 50, 50, 62, 40, 62, 65 }, (PokeType.Poison, PokeType.Non), new string[] { "あくしゅう", "ねんちゃく", "ゆうばく" }, GenderRatio.M1F1));
            DexData.Add(new Species(569, 158, "ダストダス", "", new uint[] { 80, 95, 82, 60, 82, 75 }, (PokeType.Poison, PokeType.Non), new string[] { "あくしゅう", "くだけるよろい", "ゆうばく" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(569, 158, "ダストダス", "キョダイ", new uint[] { 80, 95, 82, 60, 82, 75 }, (PokeType.Poison, PokeType.Non), new string[] { "あくしゅう", "くだけるよろい", "ゆうばく" }, GenderRatio.M1F1));
            DexData.Add(new Species(572, 50, "チラーミィ", "", new uint[] { 55, 50, 40, 40, 40, 75 }, (PokeType.Normal, PokeType.Non), new string[] { "メロメロボディ", "テクニシャン", "スキルリンク" }, GenderRatio.M1F3));
            DexData.Add(new Species(573, 51, "チラチーノ", "", new uint[] { 75, 95, 60, 65, 60, 115 }, (PokeType.Normal, PokeType.Non), new string[] { "メロメロボディ", "テクニシャン", "スキルリンク" }, GenderRatio.M1F3));
            DexData.Add(new Species(574, 267, "ゴチム", "", new uint[] { 45, 30, 50, 55, 65, 45 }, (PokeType.Psychic, PokeType.Non), new string[] { "おみとおし", "かちき", "かげふみ" }, GenderRatio.M1F3));
            DexData.Add(new Species(575, 268, "ゴチミル", "", new uint[] { 60, 45, 70, 75, 85, 55 }, (PokeType.Psychic, PokeType.Non), new string[] { "おみとおし", "かちき", "かげふみ" }, GenderRatio.M1F3));
            DexData.Add(new Species(576, 269, "ゴチルゼル", "", new uint[] { 70, 55, 95, 95, 110, 65 }, (PokeType.Psychic, PokeType.Non), new string[] { "おみとおし", "かちき", "かげふみ" }, GenderRatio.M1F3));
            DexData.Add(new Species(577, 270, "ユニラン", "", new uint[] { 45, 30, 40, 105, 50, 20 }, (PokeType.Psychic, PokeType.Non), new string[] { "ぼうじん", "マジックガード", "さいせいりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(578, 271, "ダブラン", "", new uint[] { 65, 40, 50, 125, 60, 30 }, (PokeType.Psychic, PokeType.Non), new string[] { "ぼうじん", "マジックガード", "さいせいりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(579, 272, "ランクルス", "", new uint[] { 110, 65, 75, 125, 85, 30 }, (PokeType.Psychic, PokeType.Non), new string[] { "ぼうじん", "マジックガード", "さいせいりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(582, 72, "バニプッチ", "", new uint[] { 36, 50, 50, 65, 60, 44 }, (PokeType.Ice, PokeType.Non), new string[] { "アイスボディ", "ゆきがくれ", "くだけるよろい" }, GenderRatio.M1F1));
            DexData.Add(new Species(583, 73, "バニリッチ", "", new uint[] { 51, 65, 65, 80, 75, 59 }, (PokeType.Ice, PokeType.Non), new string[] { "アイスボディ", "ゆきがくれ", "くだけるよろい" }, GenderRatio.M1F1));
            DexData.Add(new Species(584, 74, "バイバニラ", "", new uint[] { 71, 95, 85, 110, 95, 79 }, (PokeType.Ice, PokeType.Non), new string[] { "アイスボディ", "ゆきふらし", "くだけるよろい" }, GenderRatio.M1F1));
            DexData.Add(new Species(588, 273, "カブルモ", "", new uint[] { 50, 75, 45, 40, 45, 60 }, (PokeType.Bug, PokeType.Non), new string[] { "むしのしらせ", "だっぴ", "ノーガード" }, GenderRatio.M1F1));
            DexData.Add(new Species(589, 274, "シュバルゴ", "", new uint[] { 70, 135, 105, 60, 105, 20 }, (PokeType.Bug, PokeType.Steel), new string[] { "むしのしらせ", "シェルアーマー", "ぼうじん" }, GenderRatio.M1F1));
            DexData.Add(new Species(592, 305, "プルリル", "", new uint[] { 55, 40, 50, 65, 85, 40 }, (PokeType.Water, PokeType.Ghost), new string[] { "ちょすい", "のろわれボディ", "しめりけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(593, 306, "ブルンゲル", "", new uint[] { 100, 60, 70, 85, 105, 60 }, (PokeType.Water, PokeType.Ghost), new string[] { "ちょすい", "のろわれボディ", "しめりけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(595, 64, "バチュル", "", new uint[] { 50, 47, 50, 57, 50, 65 }, (PokeType.Bug, PokeType.Electric), new string[] { "ふくがん", "きんちょうかん", "むしのしらせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(596, 65, "デンチュラ", "", new uint[] { 70, 77, 60, 97, 60, 108 }, (PokeType.Bug, PokeType.Electric), new string[] { "ふくがん", "きんちょうかん", "むしのしらせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(597, 189, "テッシード", "", new uint[] { 44, 50, 91, 24, 86, 10 }, (PokeType.Grass, PokeType.Steel), new string[] { "てつのトゲ", "てつのトゲ", "てつのトゲ" }, GenderRatio.M1F1));
            DexData.Add(new Species(598, 190, "ナットレイ", "", new uint[] { 74, 94, 131, 54, 116, 20 }, (PokeType.Grass, PokeType.Steel), new string[] { "てつのトゲ", "てつのトゲ", "きけんよち" }, GenderRatio.M1F1));
            DexData.Add(new Species(599, 113, "ギアル", "", new uint[] { 40, 55, 70, 45, 60, 30 }, (PokeType.Steel, PokeType.Non), new string[] { "プラス", "マイナス", "クリアボディ" }, GenderRatio.Genderless));
            DexData.Add(new Species(600, 114, "ギギアル", "", new uint[] { 60, 80, 95, 70, 85, 50 }, (PokeType.Steel, PokeType.Non), new string[] { "プラス", "マイナス", "クリアボディ" }, GenderRatio.Genderless));
            DexData.Add(new Species(601, 115, "ギギギアル", "", new uint[] { 60, 100, 115, 70, 85, 90 }, (PokeType.Steel, PokeType.Non), new string[] { "プラス", "マイナス", "クリアボディ" }, GenderRatio.Genderless));
            DexData.Add(new Species(605, 277, "リグレー", "", new uint[] { 55, 55, 55, 85, 55, 30 }, (PokeType.Psychic, PokeType.Non), new string[] { "テレパシー", "シンクロ", "アナライズ" }, GenderRatio.M1F1));
            DexData.Add(new Species(606, 278, "オーベム", "", new uint[] { 75, 75, 75, 125, 95, 40 }, (PokeType.Psychic, PokeType.Non), new string[] { "テレパシー", "シンクロ", "アナライズ" }, GenderRatio.M1F1));
            DexData.Add(new Species(607, 287, "ヒトモシ", "", new uint[] { 50, 30, 55, 65, 55, 20 }, (PokeType.Ghost, PokeType.Fire), new string[] { "もらいび", "ほのおのからだ", "すりぬけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(608, 288, "ランプラー", "", new uint[] { 60, 40, 60, 95, 60, 55 }, (PokeType.Ghost, PokeType.Fire), new string[] { "もらいび", "ほのおのからだ", "すりぬけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(609, 289, "シャンデラ", "", new uint[] { 60, 55, 90, 145, 90, 80 }, (PokeType.Ghost, PokeType.Fire), new string[] { "もらいび", "ほのおのからだ", "すりぬけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(610, 324, "キバゴ", "", new uint[] { 46, 87, 60, 30, 40, 57 }, (PokeType.Dragon, PokeType.Non), new string[] { "とうそうしん", "かたやぶり", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new Species(611, 325, "オノンド", "", new uint[] { 66, 117, 70, 40, 50, 67 }, (PokeType.Dragon, PokeType.Non), new string[] { "とうそうしん", "かたやぶり", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new Species(612, 326, "オノノクス", "", new uint[] { 76, 147, 90, 60, 70, 97 }, (PokeType.Dragon, PokeType.Non), new string[] { "とうそうしん", "かたやぶり", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new Species(613, 279, "クマシュン", "", new uint[] { 55, 70, 40, 60, 40, 40 }, (PokeType.Ice, PokeType.Non), new string[] { "ゆきがくれ", "ゆきかき", "びびり" }, GenderRatio.M1F1));
            DexData.Add(new Species(614, 280, "ツンベアー", "", new uint[] { 95, 130, 80, 70, 80, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "ゆきがくれ", "ゆきかき", "すいすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(616, 275, "チョボマキ", "", new uint[] { 50, 40, 85, 40, 65, 25 }, (PokeType.Bug, PokeType.Non), new string[] { "うるおいボディ", "シェルアーマー", "ぼうじん" }, GenderRatio.M1F1));
            DexData.Add(new Species(617, 276, "アギルダー", "", new uint[] { 80, 70, 40, 100, 60, 145 }, (PokeType.Bug, PokeType.Non), new string[] { "うるおいボディ", "ねんちゃく", "かるわざ" }, GenderRatio.M1F1));
            DexData.Add(new Species(618, 226, "マッギョ", "", new uint[] { 109, 66, 84, 81, 99, 32 }, (PokeType.Ground, PokeType.Electric), new string[] { "せいでんき", "じゅうなん", "すながくれ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(618, 226, "マッギョ", "ガラル", new uint[] { 109, 81, 99, 66, 84, 32 }, (PokeType.Ground, PokeType.Steel), new string[] { "ぎたい", "ぎたい", "ぎたい" }, GenderRatio.M1F1));
            DexData.Add(new Species(622, 88, "ゴビット", "", new uint[] { 59, 74, 50, 35, 50, 35 }, (PokeType.Ground, PokeType.Ghost), new string[] { "てつのこぶし", "ぶきよう", "ノーガード" }, GenderRatio.Genderless));
            DexData.Add(new Species(623, 89, "ゴルーグ", "", new uint[] { 89, 124, 80, 55, 80, 55 }, (PokeType.Ground, PokeType.Ghost), new string[] { "てつのこぶし", "ぶきよう", "ノーガード" }, GenderRatio.Genderless));
            DexData.Add(new Species(624, 246, "コマタナ", "", new uint[] { 45, 85, 70, 40, 40, 60 }, (PokeType.Dark, PokeType.Steel), new string[] { "まけんき", "せいしんりょく", "プレッシャー" }, GenderRatio.M1F1));
            DexData.Add(new Species(625, 247, "キリキザン", "", new uint[] { 65, 125, 100, 60, 70, 70 }, (PokeType.Dark, PokeType.Steel), new string[] { "まけんき", "せいしんりょく", "プレッシャー" }, GenderRatio.M1F1));
            DexData.Add(new Species(627, 281, "ワシボン", "", new uint[] { 70, 83, 50, 37, 50, 60 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "ちからずく", "はりきり" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(628, 282, "ウォーグル", "", new uint[] { 100, 123, 75, 57, 75, 80 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "ちからずく", "まけんき" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(629, 283, "バルチャイ", "", new uint[] { 70, 55, 75, 45, 65, 60 }, (PokeType.Dark, PokeType.Flying), new string[] { "はとむね", "ぼうじん", "くだけるよろい" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(630, 284, "バルジーナ", "", new uint[] { 110, 65, 105, 55, 95, 80 }, (PokeType.Dark, PokeType.Flying), new string[] { "はとむね", "ぼうじん", "くだけるよろい" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(631, 317, "クイタラン", "", new uint[] { 85, 97, 66, 105, 66, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "くいしんぼう", "もらいび", "しろいけむり" }, GenderRatio.M1F1));
            DexData.Add(new Species(632, 316, "アイアント", "", new uint[] { 58, 109, 112, 48, 48, 109 }, (PokeType.Bug, PokeType.Steel), new string[] { "むしのしらせ", "はりきり", "なまけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(633, 386, "モノズ", "", new uint[] { 52, 65, 50, 45, 50, 38 }, (PokeType.Dark, PokeType.Dragon), new string[] { "はりきり", "はりきり", "はりきり" }, GenderRatio.M1F1));
            DexData.Add(new Species(634, 387, "ジヘッド", "", new uint[] { 72, 85, 70, 65, 70, 58 }, (PokeType.Dark, PokeType.Dragon), new string[] { "はりきり", "はりきり", "はりきり" }, GenderRatio.M1F1));
            DexData.Add(new Species(635, 388, "サザンドラ", "", new uint[] { 92, 105, 90, 125, 90, 98 }, (PokeType.Dark, PokeType.Dragon), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            DexData.Add(new Species(638, -1, "コバルオン", "", new uint[] { 91, 90, 129, 90, 72, 108 }, (PokeType.Steel, PokeType.Fighting), new string[] { "せいぎのこころ", "せいぎのこころ", "せいぎのこころ" }, GenderRatio.Genderless));
            DexData.Add(new Species(639, -1, "テラキオン", "", new uint[] { 91, 129, 90, 72, 90, 108 }, (PokeType.Rock, PokeType.Fighting), new string[] { "せいぎのこころ", "せいぎのこころ", "せいぎのこころ" }, GenderRatio.Genderless));
            DexData.Add(new Species(640, -1, "ビリジオン", "", new uint[] { 91, 90, 72, 90, 129, 108 }, (PokeType.Grass, PokeType.Fighting), new string[] { "せいぎのこころ", "せいぎのこころ", "せいぎのこころ" }, GenderRatio.Genderless));
            DexData.Add(new Species(643, -1, "レシラム", "", new uint[] { 100, 120, 100, 150, 120, 90 }, (PokeType.Dragon, PokeType.Fire), new string[] { "ターボブレイズ", "ターボブレイズ", "ターボブレイズ" }, GenderRatio.Genderless));
            DexData.Add(new Species(644, -1, "ゼクロム", "", new uint[] { 100, 150, 120, 120, 100, 90 }, (PokeType.Dragon, PokeType.Electric), new string[] { "テラボルテージ", "テラボルテージ", "テラボルテージ" }, GenderRatio.Genderless));
            DexData.Add(new Species(646, -1, "キュレム", "", new uint[] { 125, 130, 90, 130, 90, 95 }, (PokeType.Dragon, PokeType.Ice), new string[] { "プレッシャー", "プレッシャー", "プレッシャー" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(646, -1, "キュレム", "ブラック", new uint[] { 125, 120, 90, 170, 100, 95 }, (PokeType.Dragon, PokeType.Ice), new string[] { "ターボブレイズ", "ターボブレイズ", "ターボブレイズ" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(646, -1, "キュレム", "ホワイト", new uint[] { 125, 170, 100, 120, 90, 95 }, (PokeType.Dragon, PokeType.Ice), new string[] { "テラボルテージ", "テラボルテージ", "テラボルテージ" }, GenderRatio.Genderless));
            DexData.Add(new Species(647, -1, "ケルディオ", "", new uint[] { 91, 72, 90, 129, 90, 108 }, (PokeType.Water, PokeType.Fighting), new string[] { "せいぎのこころ", "せいぎのこころ", "せいぎのこころ" }, GenderRatio.Genderless));
            DexData.Add(new Species(659, 48, "ホルビー", "", new uint[] { 38, 36, 38, 32, 36, 57 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "ほおぶくろ", "ちからもち" }, GenderRatio.M1F1));
            DexData.Add(new Species(660, 49, "ホルード", "", new uint[] { 85, 56, 77, 50, 77, 78 }, (PokeType.Normal, PokeType.Ground), new string[] { "ものひろい", "ほおぶくろ", "ちからもち" }, GenderRatio.M1F1));
            DexData.Add(new Species(674, 111, "ヤンチャム", "", new uint[] { 67, 82, 62, 46, 48, 43 }, (PokeType.Fighting, PokeType.Non), new string[] { "てつのこぶし", "かたやぶり", "きもったま" }, GenderRatio.M1F1));
            DexData.Add(new Species(675, 112, "ゴロンダ", "", new uint[] { 95, 124, 78, 69, 71, 58 }, (PokeType.Fighting, PokeType.Dark), new string[] { "てつのこぶし", "かたやぶり", "きもったま" }, GenderRatio.M1F1));
            DexData.Add(new Species(677, 208, "ニャスパー", "", new uint[] { 62, 48, 54, 63, 60, 68 }, (PokeType.Psychic, PokeType.Non), new string[] { "するどいめ", "すりぬけ", "マイペース" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(678, 209, "ニャオニクス", "♂", new uint[] { 74, 48, 76, 83, 81, 104 }, (PokeType.Psychic, PokeType.Non), new string[] { "するどいめ", "すりぬけ", "いたずらごころ" }, GenderRatio.MaleOnly));
            DexData.Add(new AnotherForm(678, 209, "ニャオニクス", "♀", new uint[] { 74, 48, 76, 83, 81, 104 }, (PokeType.Psychic, PokeType.Non), new string[] { "するどいめ", "すりぬけ", "かちき" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(679, 330, "ヒトツキ", "", new uint[] { 45, 80, 100, 35, 37, 28 }, (PokeType.Steel, PokeType.Ghost), new string[] { "ノーガード", "ノーガード", "ノーガード" }, GenderRatio.M1F1));
            DexData.Add(new Species(680, 331, "ニダンギル", "", new uint[] { 59, 110, 150, 45, 49, 35 }, (PokeType.Steel, PokeType.Ghost), new string[] { "ノーガード", "ノーガード", "ノーガード" }, GenderRatio.M1F1));
            DexData.Add(new Species(681, 332, "ギルガルド", "シールド", new uint[] { 60, 50, 140, 50, 140, 60 }, (PokeType.Steel, PokeType.Ghost), new string[] { "バトルスイッチ", "バトルスイッチ", "バトルスイッチ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(681, 332, "ギルガルド", "ブレード", new uint[] { 60, 140, 50, 140, 50, 60 }, (PokeType.Steel, PokeType.Ghost), new string[] { "バトルスイッチ", "バトルスイッチ", "バトルスイッチ" }, GenderRatio.M1F1));
            DexData.Add(new Species(682, 212, "シュシュプ", "", new uint[] { 78, 52, 60, 63, 65, 23 }, (PokeType.Fairy, PokeType.Non), new string[] { "いやしのこころ", "いやしのこころ", "アロマベール" }, GenderRatio.M1F1));
            DexData.Add(new Species(683, 213, "フレフワン", "", new uint[] { 101, 72, 72, 99, 89, 29 }, (PokeType.Fairy, PokeType.Non), new string[] { "いやしのこころ", "いやしのこころ", "アロマベール" }, GenderRatio.M1F1));
            DexData.Add(new Species(684, 210, "ペロッパフ", "", new uint[] { 62, 48, 66, 59, 57, 49 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "かるわざ" }, GenderRatio.M1F1));
            DexData.Add(new Species(685, 211, "ペロリーム", "", new uint[] { 82, 80, 86, 85, 75, 72 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "かるわざ" }, GenderRatio.M1F1));
            DexData.Add(new Species(686, 290, "マーイーカ", "", new uint[] { 53, 54, 53, 37, 46, 45 }, (PokeType.Dark, PokeType.Psychic), new string[] { "あまのじゃく", "きゅうばん", "すりぬけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(687, 291, "カラマネロ", "", new uint[] { 86, 92, 88, 68, 75, 73 }, (PokeType.Dark, PokeType.Psychic), new string[] { "あまのじゃく", "きゅうばん", "すりぬけ" }, GenderRatio.M1F1));
            DexData.Add(new Species(688, 234, "カメテテ", "", new uint[] { 42, 52, 67, 39, 56, 50 }, (PokeType.Rock, PokeType.Water), new string[] { "かたいツメ", "スナイパー", "わるいてぐせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(689, 235, "ガメノデス", "", new uint[] { 72, 105, 115, 54, 86, 68 }, (PokeType.Rock, PokeType.Water), new string[] { "かたいツメ", "スナイパー", "わるいてぐせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(694, 318, "エリキテル", "", new uint[] { 44, 38, 33, 61, 43, 70 }, (PokeType.Electric, PokeType.Normal), new string[] { "かんそうはだ", "すながくれ", "サンパワー" }, GenderRatio.M1F1));
            DexData.Add(new Species(695, 319, "エレザード", "", new uint[] { 62, 55, 52, 109, 94, 109 }, (PokeType.Electric, PokeType.Normal), new string[] { "かんそうはだ", "すながくれ", "サンパワー" }, GenderRatio.M1F1));
            DexData.Add(new Species(700, 204, "ニンフィア", "", new uint[] { 95, 65, 65, 110, 130, 60 }, (PokeType.Fairy, PokeType.Non), new string[] { "メロメロボディ", "メロメロボディ", "フェアリースキン" }, GenderRatio.M7F1));
            DexData.Add(new Species(701, 320, "ルチャブル", "", new uint[] { 78, 92, 75, 74, 63, 118 }, (PokeType.Fighting, PokeType.Flying), new string[] { "じゅうなん", "かるわざ", "かたやぶり" }, GenderRatio.M1F1));
            DexData.Add(new Species(704, 389, "ヌメラ", "", new uint[] { 45, 50, 35, 55, 75, 40 }, (PokeType.Dragon, PokeType.Non), new string[] { "そうしょく", "うるおいボディ", "ぬめぬめ" }, GenderRatio.M1F1));
            DexData.Add(new Species(705, 390, "ヌメイル", "", new uint[] { 68, 75, 53, 83, 113, 60 }, (PokeType.Dragon, PokeType.Non), new string[] { "そうしょく", "うるおいボディ", "ぬめぬめ" }, GenderRatio.M1F1));
            DexData.Add(new Species(706, 391, "ヌメルゴン", "", new uint[] { 90, 100, 70, 110, 150, 80 }, (PokeType.Dragon, PokeType.Non), new string[] { "そうしょく", "うるおいボディ", "ぬめぬめ" }, GenderRatio.M1F1));
            DexData.Add(new Species(708, 338, "ボクレー", "", new uint[] { 43, 70, 48, 50, 60, 38 }, (PokeType.Ghost, PokeType.Grass), new string[] { "しぜんかいふく", "おみとおし", "しゅうかく" }, GenderRatio.M1F1));
            DexData.Add(new Species(709, 339, "オーロット", "", new uint[] { 85, 110, 76, 65, 82, 56 }, (PokeType.Ghost, PokeType.Grass), new string[] { "しぜんかいふく", "おみとおし", "しゅうかく" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(710, 191, "バケッチャ", "普通", new uint[] { 49, 66, 70, 44, 55, 51 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(710, 191, "バケッチャ", "小", new uint[] { 44, 66, 70, 44, 55, 56 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(710, 191, "バケッチャ", "大", new uint[] { 54, 66, 70, 44, 55, 46 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(710, 191, "バケッチャ", "特大", new uint[] { 59, 66, 70, 44, 55, 41 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(711, 192, "パンプジン", "普通", new uint[] { 65, 90, 122, 58, 75, 84 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(711, 192, "パンプジン", "小", new uint[] { 55, 85, 122, 58, 75, 99 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(711, 192, "パンプジン", "大", new uint[] { 75, 95, 122, 58, 75, 69 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(711, 192, "パンプジン", "特大", new uint[] { 85, 100, 122, 58, 75, 54 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            DexData.Add(new Species(712, 358, "カチコール", "", new uint[] { 55, 69, 85, 32, 35, 28 }, (PokeType.Ice, PokeType.Non), new string[] { "マイペース", "アイスボディ", "がんじょう" }, GenderRatio.M1F1));
            DexData.Add(new Species(713, 359, "クレベース", "", new uint[] { 95, 117, 184, 44, 46, 28 }, (PokeType.Ice, PokeType.Non), new string[] { "マイペース", "アイスボディ", "がんじょう" }, GenderRatio.M1F1));
            DexData.Add(new Species(714, 176, "オンバット", "", new uint[] { 40, 30, 35, 45, 40, 55 }, (PokeType.Flying, PokeType.Dragon), new string[] { "おみとおし", "すりぬけ", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(715, 177, "オンバーン", "", new uint[] { 85, 70, 80, 97, 80, 123 }, (PokeType.Flying, PokeType.Dragon), new string[] { "おみとおし", "すりぬけ", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(722, -1, "モクロー", "", new uint[] { 68, 55, 55, 50, 50, 42 }, (PokeType.Grass, PokeType.Flying), new string[] { "しんりょく", "しんりょく", "えんかく" }, GenderRatio.M7F1));
            DexData.Add(new Species(723, -1, "フクスロー", "", new uint[] { 78, 75, 75, 70, 70, 52 }, (PokeType.Grass, PokeType.Flying), new string[] { "しんりょく", "しんりょく", "えんかく" }, GenderRatio.M7F1));
            DexData.Add(new Species(724, -1, "ジュナイパー", "", new uint[] { 78, 107, 75, 100, 100, 70 }, (PokeType.Grass, PokeType.Ghost), new string[] { "しんりょく", "しんりょく", "えんかく" }, GenderRatio.M7F1));
            DexData.Add(new Species(725, -1, "ニャビー", "", new uint[] { 45, 65, 40, 60, 40, 70 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "いかく" }, GenderRatio.M7F1));
            DexData.Add(new Species(726, -1, "ニャヒート", "", new uint[] { 65, 85, 50, 80, 50, 90 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "いかく" }, GenderRatio.M7F1));
            DexData.Add(new Species(727, -1, "ガオガエン", "", new uint[] { 95, 115, 90, 80, 90, 60 }, (PokeType.Fire, PokeType.Dark), new string[] { "もうか", "もうか", "いかく" }, GenderRatio.M7F1));
            DexData.Add(new Species(728, -1, "アシマリ", "", new uint[] { 50, 54, 54, 66, 56, 40 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "うるおいボイス" }, GenderRatio.M7F1));
            DexData.Add(new Species(729, -1, "オシャマリ", "", new uint[] { 60, 69, 69, 91, 81, 50 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "うるおいボイス" }, GenderRatio.M7F1));
            DexData.Add(new Species(730, -1, "アシレーヌ", "", new uint[] { 80, 74, 74, 126, 116, 60 }, (PokeType.Water, PokeType.Fairy), new string[] { "げきりゅう", "げきりゅう", "うるおいボイス" }, GenderRatio.M7F1));
            DexData.Add(new Species(736, 16, "アゴジムシ", "", new uint[] { 47, 62, 45, 55, 45, 46 }, (PokeType.Bug, PokeType.Non), new string[] { "むしのしらせ", "むしのしらせ", "むしのしらせ" }, GenderRatio.M1F1));
            DexData.Add(new Species(737, 17, "デンヂムシ", "", new uint[] { 57, 82, 95, 55, 75, 36 }, (PokeType.Bug, PokeType.Electric), new string[] { "バッテリー", "バッテリー", "バッテリー" }, GenderRatio.M1F1));
            DexData.Add(new Species(738, 18, "クワガノン", "", new uint[] { 77, 70, 90, 145, 75, 43 }, (PokeType.Bug, PokeType.Electric), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            DexData.Add(new Species(742, 187, "アブリー", "", new uint[] { 40, 45, 40, 55, 40, 84 }, (PokeType.Bug, PokeType.Fairy), new string[] { "みつあつめ", "りんぷん", "スイートベール" }, GenderRatio.M1F1));
            DexData.Add(new Species(743, 188, "アブリボン", "", new uint[] { 60, 55, 60, 95, 70, 124 }, (PokeType.Bug, PokeType.Fairy), new string[] { "みつあつめ", "りんぷん", "スイートベール" }, GenderRatio.M1F1));
            DexData.Add(new Species(746, 155, "ヨワシ", "単独", new uint[] { 45, 20, 20, 25, 25, 40 }, (PokeType.Water, PokeType.Non), new string[] { "ぎょぐん", "ぎょぐん", "ぎょぐん" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(746, 155, "ヨワシ", "群れ", new uint[] { 45, 140, 130, 140, 135, 30 }, (PokeType.Water, PokeType.Non), new string[] { "ぎょぐん", "ぎょぐん", "ぎょぐん" }, GenderRatio.M1F1));
            DexData.Add(new Species(747, 307, "ヒドイデ", "", new uint[] { 50, 53, 62, 43, 52, 45 }, (PokeType.Poison, PokeType.Water), new string[] { "ひとでなし", "じゅうなん", "さいせいりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(748, 308, "ドヒドイデ", "", new uint[] { 50, 63, 152, 53, 142, 35 }, (PokeType.Poison, PokeType.Water), new string[] { "ひとでなし", "じゅうなん", "さいせいりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(749, 84, "ドロバンコ", "", new uint[] { 70, 100, 70, 45, 55, 45 }, (PokeType.Ground, PokeType.Non), new string[] { "マイペース", "じきゅうりょく", "せいしんりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(750, 85, "バンバドロ", "", new uint[] { 100, 125, 100, 55, 85, 35 }, (PokeType.Ground, PokeType.Non), new string[] { "マイペース", "じきゅうりょく", "せいしんりょく" }, GenderRatio.M1F1));
            DexData.Add(new Species(751, 214, "シズクモ", "", new uint[] { 38, 40, 52, 40, 72, 27 }, (PokeType.Water, PokeType.Bug), new string[] { "すいほう", "すいほう", "ちょすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(752, 215, "オニシズクモ", "", new uint[] { 68, 70, 92, 50, 132, 42 }, (PokeType.Water, PokeType.Bug), new string[] { "すいほう", "すいほう", "ちょすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(755, 340, "ネマシュ", "", new uint[] { 40, 35, 55, 65, 75, 15 }, (PokeType.Grass, PokeType.Fairy), new string[] { "はっこう", "ほうし", "あめうけざら" }, GenderRatio.M1F1));
            DexData.Add(new Species(756, 341, "マシェード", "", new uint[] { 60, 45, 80, 90, 100, 30 }, (PokeType.Grass, PokeType.Fairy), new string[] { "はっこう", "ほうし", "あめうけざら" }, GenderRatio.M1F1));
            DexData.Add(new Species(757, 244, "ヤトウモリ", "", new uint[] { 48, 44, 40, 71, 40, 77 }, (PokeType.Poison, PokeType.Fire), new string[] { "ふしょく", "ふしょく", "どんかん" }, GenderRatio.M7F1));
            DexData.Add(new Species(758, 245, "エンニュート", "", new uint[] { 68, 64, 60, 111, 60, 117 }, (PokeType.Poison, PokeType.Fire), new string[] { "ふしょく", "ふしょく", "どんかん" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(759, 94, "ヌイコグマ", "", new uint[] { 70, 75, 50, 45, 50, 50 }, (PokeType.Normal, PokeType.Fighting), new string[] { "もふもふ", "ぶきよう", "メロメロボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(760, 95, "キテルグマ", "", new uint[] { 120, 125, 80, 55, 60, 60 }, (PokeType.Normal, PokeType.Fighting), new string[] { "もふもふ", "ぶきよう", "きんちょうかん" }, GenderRatio.M1F1));
            DexData.Add(new Species(761, 52, "アマカジ", "", new uint[] { 42, 30, 38, 30, 38, 32 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "どんかん", "スイートベール" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(762, 53, "アママイコ", "", new uint[] { 52, 40, 48, 40, 48, 62 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "どんかん", "スイートベール" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(763, 54, "アマージョ", "", new uint[] { 72, 120, 98, 50, 98, 72 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "じょおうのいげん", "スイートベール" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(765, 342, "ヤレユータン", "", new uint[] { 90, 60, 80, 90, 110, 60 }, (PokeType.Normal, PokeType.Psychic), new string[] { "せいしんりょく", "テレパシー", "きょうせい" }, GenderRatio.M1F1));
            DexData.Add(new Species(766, 343, "ナゲツケサル", "", new uint[] { 100, 120, 90, 40, 60, 80 }, (PokeType.Fighting, PokeType.Non), new string[] { "レシーバー", "レシーバー", "まけんき" }, GenderRatio.M1F1));
            DexData.Add(new Species(767, 232, "コソクムシ", "", new uint[] { 25, 35, 40, 20, 30, 80 }, (PokeType.Bug, PokeType.Water), new string[] { "にげごし", "にげごし", "にげごし" }, GenderRatio.M1F1));
            DexData.Add(new Species(768, 233, "グソクムシャ", "", new uint[] { 75, 125, 140, 60, 90, 40 }, (PokeType.Bug, PokeType.Water), new string[] { "ききかいひ", "ききかいひ", "ききかいひ" }, GenderRatio.M1F1));
            DexData.Add(new Species(771, 156, "ナマコブシ", "", new uint[] { 55, 60, 130, 30, 130, 5 }, (PokeType.Water, PokeType.Non), new string[] { "とびだすなかみ", "とびだすなかみ", "てんねん" }, GenderRatio.M1F1));
            DexData.Add(new Species(772, 381, "タイプ:ヌル", "", new uint[] { 95, 95, 95, 95, 95, 59 }, (PokeType.Normal, PokeType.Non), new string[] { "カブトアーマー", "カブトアーマー", "カブトアーマー" }, GenderRatio.Genderless));
            DexData.Add(new Species(773, 382, "シルヴァディ", "", new uint[] { 95, 95, 95, 95, 95, 95 }, (PokeType.Normal, PokeType.Non), new string[] { "ARシステム", "ARシステム", "ARシステム" }, GenderRatio.Genderless));
            DexData.Add(new Species(776, 347, "バクガメス", "", new uint[] { 60, 78, 135, 91, 85, 36 }, (PokeType.Fire, PokeType.Dragon), new string[] { "シェルアーマー", "シェルアーマー", "シェルアーマー" }, GenderRatio.M1F1));
            DexData.Add(new Species(777, 348, "トゲデマル", "", new uint[] { 65, 98, 63, 40, 73, 96 }, (PokeType.Electric, PokeType.Steel), new string[] { "てつのトゲ", "ひらいしん", "がんじょう" }, GenderRatio.M1F1));
            DexData.Add(new Species(778, 301, "ミミッキュ", "", new uint[] { 55, 90, 80, 50, 105, 96 }, (PokeType.Ghost, PokeType.Fairy), new string[] { "ばけのかわ", "ばけのかわ", "ばけのかわ" }, GenderRatio.M1F1));
            DexData.Add(new Species(780, 346, "ジジーロン", "", new uint[] { 78, 60, 85, 135, 91, 36 }, (PokeType.Normal, PokeType.Dragon), new string[] { "ぎゃくじょう", "そうしょく", "ノーてんき" }, GenderRatio.M1F1));
            DexData.Add(new Species(781, 360, "ダダリン", "", new uint[] { 70, 131, 100, 86, 90, 40 }, (PokeType.Ghost, PokeType.Grass), new string[] { "はがねつかい", "はがねつかい", "はがねつかい" }, GenderRatio.Genderless));
            DexData.Add(new Species(782, 392, "ジャラコ", "", new uint[] { 45, 55, 65, 45, 45, 45 }, (PokeType.Dragon, PokeType.Non), new string[] { "ぼうだん", "ぼうおん", "ぼうじん" }, GenderRatio.M1F1));
            DexData.Add(new Species(783, 393, "ジャランゴ", "", new uint[] { 55, 75, 90, 65, 70, 65 }, (PokeType.Dragon, PokeType.Fighting), new string[] { "ぼうだん", "ぼうおん", "ぼうじん" }, GenderRatio.M1F1));
            DexData.Add(new Species(784, 394, "ジャラランガ", "", new uint[] { 75, 110, 125, 100, 105, 85 }, (PokeType.Dragon, PokeType.Fighting), new string[] { "ぼうだん", "ぼうおん", "ぼうじん" }, GenderRatio.M1F1));
            DexData.Add(new Species(789, -1, "コスモッグ", "", new uint[] { 43, 29, 31, 29, 31, 37 }, (PokeType.Psychic, PokeType.Non), new string[] { "てんねん", "てんねん", "てんねん" }, GenderRatio.Genderless));
            DexData.Add(new Species(790, -1, "コスモウム", "", new uint[] { 43, 29, 131, 29, 131, 37 }, (PokeType.Psychic, PokeType.Non), new string[] { "がんじょう", "がんじょう", "がんじょう" }, GenderRatio.Genderless));
            DexData.Add(new Species(791, -1, "ソルガレオ", "", new uint[] { 137, 137, 107, 113, 89, 97 }, (PokeType.Psychic, PokeType.Steel), new string[] { "メタルプロテクト", "メタルプロテクト", "メタルプロテクト" }, GenderRatio.Genderless));
            DexData.Add(new Species(792, -1, "ルナアーラ", "", new uint[] { 137, 113, 89, 137, 107, 97 }, (PokeType.Psychic, PokeType.Ghost), new string[] { "ファントムガード", "ファントムガード", "ファントムガード" }, GenderRatio.Genderless));
            DexData.Add(new Species(800, -1, "ネクロズマ", "", new uint[] { 97, 107, 101, 127, 89, 79 }, (PokeType.Psychic, PokeType.Non), new string[] { "プリズムアーマー", "プリズムアーマー", "プリズムアーマー" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(800, -1, "ネクロズマ", "たそがれ", new uint[] { 97, 157, 127, 113, 109, 77 }, (PokeType.Psychic, PokeType.Steel), new string[] { "プリズムアーマー", "プリズムアーマー", "プリズムアーマー" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(800, -1, "ネクロズマ", "あかつき", new uint[] { 97, 113, 109, 157, 127, 77 }, (PokeType.Psychic, PokeType.Ghost), new string[] { "プリズムアーマー", "プリズムアーマー", "プリズムアーマー" }, GenderRatio.Genderless));
            DexData.Add(new Species(802, -1, "マーシャドー", "", new uint[] { 90, 125, 80, 90, 90, 125 }, (PokeType.Fighting, PokeType.Ghost), new string[] { "テクニシャン", "テクニシャン", "テクニシャン" }, GenderRatio.Genderless));
            DexData.Add(new Species(807, -1, "ゼラオラ", "", new uint[] { 88, 112, 75, 102, 80, 143 }, (PokeType.Electric, PokeType.Non), new string[] { "ちくでん", "ちくでん", "ちくでん" }, GenderRatio.Genderless));
            DexData.Add(new Species(808, -1, "メルタン", "", new uint[] { 46, 65, 65, 55, 35, 34 }, (PokeType.Steel, PokeType.Non), new string[] { "じりょく", "じりょく", "じりょく" }, GenderRatio.Genderless));
            DexData.Add(new Species(809, -1, "メルメタル", "", new uint[] { 135, 143, 143, 80, 65, 34 }, (PokeType.Steel, PokeType.Non), new string[] { "てつのこぶし", "てつのこぶし", "てつのこぶし" }, GenderRatio.Genderless));
            DexData.Add(new Species(810, 1, "サルノリ", "", new uint[] { 50, 65, 50, 40, 40, 65 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "グラスメイカー" }, GenderRatio.M7F1));
            DexData.Add(new Species(811, 2, "バチンキー", "", new uint[] { 70, 85, 70, 55, 60, 80 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "グラスメイカー" }, GenderRatio.M7F1));
            DexData.Add(new Species(812, 3, "ゴリランダー", "", new uint[] { 100, 125, 90, 60, 70, 85 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "グラスメイカー" }, GenderRatio.M7F1));
            DexData.Add(new Species(813, 4, "ヒバニー", "", new uint[] { 50, 71, 40, 40, 40, 69 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "リベロ" }, GenderRatio.M7F1));
            DexData.Add(new Species(814, 5, "ラビフット", "", new uint[] { 65, 86, 60, 55, 60, 94 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "リベロ" }, GenderRatio.M7F1));
            DexData.Add(new Species(815, 6, "エースバーン", "", new uint[] { 80, 116, 75, 65, 75, 119 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "リベロ" }, GenderRatio.M7F1));
            DexData.Add(new Species(816, 7, "メッソン", "", new uint[] { 50, 40, 40, 70, 40, 70 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "スナイパー" }, GenderRatio.M7F1));
            DexData.Add(new Species(817, 8, "ジメレオン", "", new uint[] { 65, 60, 55, 95, 55, 90 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "スナイパー" }, GenderRatio.M7F1));
            DexData.Add(new Species(818, 9, "インテレオン", "", new uint[] { 70, 85, 65, 125, 65, 120 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "スナイパー" }, GenderRatio.M7F1));
            DexData.Add(new Species(819, 24, "ホシガリス", "", new uint[] { 70, 55, 55, 35, 35, 25 }, (PokeType.Normal, PokeType.Non), new string[] { "ほおぶくろ", "ほおぶくろ", "くいしんぼう" }, GenderRatio.M1F1));
            DexData.Add(new Species(820, 25, "ヨクバリス", "", new uint[] { 120, 95, 95, 55, 75, 20 }, (PokeType.Normal, PokeType.Non), new string[] { "ほおぶくろ", "ほおぶくろ", "くいしんぼう" }, GenderRatio.M1F1));
            DexData.Add(new Species(821, 21, "ココガラ", "", new uint[] { 38, 47, 35, 33, 35, 57 }, (PokeType.Flying, PokeType.Non), new string[] { "するどいめ", "きんちょうかん", "はとむね" }, GenderRatio.M1F1));
            DexData.Add(new Species(822, 22, "アオガラス", "", new uint[] { 68, 67, 55, 43, 55, 77 }, (PokeType.Flying, PokeType.Non), new string[] { "するどいめ", "きんちょうかん", "はとむね" }, GenderRatio.M1F1));
            DexData.Add(new Species(823, 23, "アーマーガア", "", new uint[] { 98, 87, 105, 53, 85, 67 }, (PokeType.Flying, PokeType.Steel), new string[] { "プレッシャー", "きんちょうかん", "ミラーアーマー" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(823, 23, "アーマーガア", "キョダイ", new uint[] { 98, 87, 105, 53, 85, 67 }, (PokeType.Flying, PokeType.Steel), new string[] { "プレッシャー", "きんちょうかん", "ミラーアーマー" }, GenderRatio.M1F1));
            DexData.Add(new Species(824, 10, "サッチムシ", "", new uint[] { 25, 20, 20, 25, 45, 45 }, (PokeType.Bug, PokeType.Non), new string[] { "むしのしらせ", "ふくがん", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(825, 11, "レドームシ", "", new uint[] { 50, 35, 80, 50, 90, 30 }, (PokeType.Bug, PokeType.Psychic), new string[] { "むしのしらせ", "ふくがん", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(826, 12, "イオルブ", "", new uint[] { 60, 45, 110, 80, 120, 90 }, (PokeType.Bug, PokeType.Psychic), new string[] { "むしのしらせ", "おみとおし", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(826, 12, "イオルブ", "キョダイ", new uint[] { 60, 45, 110, 80, 120, 90 }, (PokeType.Bug, PokeType.Psychic), new string[] { "むしのしらせ", "おみとおし", "テレパシー" }, GenderRatio.M1F1));
            DexData.Add(new Species(827, 29, "クスネ", "", new uint[] { 40, 28, 28, 47, 52, 50 }, (PokeType.Dark, PokeType.Non), new string[] { "にげあし", "かるわざ", "はりこみ" }, GenderRatio.M1F1));
            DexData.Add(new Species(828, 30, "フォクスライ", "", new uint[] { 70, 58, 58, 87, 92, 90 }, (PokeType.Dark, PokeType.Non), new string[] { "にげあし", "かるわざ", "はりこみ" }, GenderRatio.M1F1));
            DexData.Add(new Species(829, 126, "ヒメンカ", "", new uint[] { 40, 40, 60, 40, 60, 10 }, (PokeType.Grass, PokeType.Non), new string[] { "わたげ", "さいせいりょく", "ほうし" }, GenderRatio.M1F1));
            DexData.Add(new Species(830, 127, "ワタシラガ", "", new uint[] { 60, 50, 90, 80, 120, 60 }, (PokeType.Grass, PokeType.Non), new string[] { "わたげ", "さいせいりょく", "ほうし" }, GenderRatio.M1F1));
            DexData.Add(new Species(831, 34, "ウールー", "", new uint[] { 42, 40, 55, 40, 45, 48 }, (PokeType.Normal, PokeType.Non), new string[] { "もふもふ", "にげあし", "ぼうだん" }, GenderRatio.M1F1));
            DexData.Add(new Species(832, 35, "バイウールー", "", new uint[] { 72, 80, 100, 60, 90, 88 }, (PokeType.Normal, PokeType.Non), new string[] { "もふもふ", "ふくつのこころ", "ぼうだん" }, GenderRatio.M1F1));
            DexData.Add(new Species(833, 42, "カムカメ", "", new uint[] { 50, 64, 50, 38, 38, 44 }, (PokeType.Water, PokeType.Non), new string[] { "がんじょうあご", "シェルアーマー", "すいすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(834, 43, "カジリガメ", "", new uint[] { 90, 115, 90, 48, 68, 74 }, (PokeType.Water, PokeType.Rock), new string[] { "がんじょうあご", "シェルアーマー", "すいすい" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(834, 43, "カジリガメ", "キョダイ", new uint[] { 90, 115, 90, 48, 68, 74 }, (PokeType.Water, PokeType.Rock), new string[] { "がんじょうあご", "シェルアーマー", "すいすい" }, GenderRatio.M1F1));
            DexData.Add(new Species(835, 46, "ワンパチ", "", new uint[] { 59, 45, 50, 40, 50, 26 }, (PokeType.Electric, PokeType.Non), new string[] { "たまひろい", "たまひろい", "びびり" }, GenderRatio.M1F1));
            DexData.Add(new Species(836, 47, "パルスワン", "", new uint[] { 69, 90, 60, 90, 60, 121 }, (PokeType.Electric, PokeType.Non), new string[] { "がんじょうあご", "がんじょうあご", "かちき" }, GenderRatio.M1F1));
            DexData.Add(new Species(837, 161, "タンドン", "", new uint[] { 30, 40, 50, 40, 50, 30 }, (PokeType.Rock, PokeType.Non), new string[] { "じょうききかん", "たいねつ", "もらいび" }, GenderRatio.M1F1));
            DexData.Add(new Species(838, 162, "トロッゴン", "", new uint[] { 80, 60, 90, 60, 70, 50 }, (PokeType.Rock, PokeType.Fire), new string[] { "じょうききかん", "ほのおのからだ", "もらいび" }, GenderRatio.M1F1));
            DexData.Add(new Species(839, 163, "セキタンザン", "", new uint[] { 110, 80, 120, 80, 90, 30 }, (PokeType.Rock, PokeType.Fire), new string[] { "じょうききかん", "ほのおのからだ", "もらいび" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(839, 163, "セキタンザン", "キョダイ", new uint[] { 110, 80, 120, 80, 90, 30 }, (PokeType.Rock, PokeType.Fire), new string[] { "じょうききかん", "ほのおのからだ", "もらいび" }, GenderRatio.M1F1));
            DexData.Add(new Species(840, 205, "カジッチュ", "", new uint[] { 40, 40, 80, 40, 40, 20 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "ぼうだん" }, GenderRatio.M1F1));
            DexData.Add(new Species(841, 206, "アップリュー", "", new uint[] { 70, 110, 80, 95, 60, 70 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "はりきり" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(841, 206, "アップリュー", "キョダイ", new uint[] { 70, 110, 80, 95, 60, 70 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "はりきり" }, GenderRatio.M1F1));
            DexData.Add(new Species(842, 207, "タルップル", "", new uint[] { 110, 85, 80, 100, 80, 30 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "あついしぼう" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(842, 207, "タルップル", "キョダイ", new uint[] { 110, 85, 80, 100, 80, 30 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "あついしぼう" }, GenderRatio.M1F1));
            DexData.Add(new Species(843, 312, "スナヘビ", "", new uint[] { 52, 57, 75, 35, 50, 46 }, (PokeType.Ground, PokeType.Non), new string[] { "すなはき", "だっぴ", "すながくれ" }, GenderRatio.M1F1));
            DexData.Add(new Species(844, 313, "サダイジャ", "", new uint[] { 72, 107, 125, 65, 70, 71 }, (PokeType.Ground, PokeType.Non), new string[] { "すなはき", "だっぴ", "すながくれ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(844, 313, "サダイジャ", "キョダイ", new uint[] { 72, 107, 125, 65, 70, 71 }, (PokeType.Ground, PokeType.Non), new string[] { "すなはき", "だっぴ", "すながくれ" }, GenderRatio.M1F1));
            DexData.Add(new Species(845, 309, "ウッウ", "", new uint[] { 70, 85, 55, 85, 95, 85 }, (PokeType.Flying, PokeType.Water), new string[] { "うのミサイル", "うのミサイル", "うのミサイル" }, GenderRatio.M1F1));
            DexData.Add(new Species(846, 180, "サシカマス", "", new uint[] { 41, 63, 40, 40, 30, 66 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "スクリューおびれ" }, GenderRatio.M1F1));
            DexData.Add(new Species(847, 181, "カマスジョー", "", new uint[] { 61, 123, 60, 60, 50, 136 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "スクリューおびれ" }, GenderRatio.M1F1));
            DexData.Add(new Species(848, 310, "エレズン", "", new uint[] { 40, 38, 35, 54, 35, 40 }, (PokeType.Electric, PokeType.Poison), new string[] { "びびり", "せいでんき", "ぶきよう" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(849, 311, "ストリンダー", "ハイ", new uint[] { 75, 98, 70, 114, 70, 75 }, (PokeType.Electric, PokeType.Poison), new string[] { "パンクロック", "プラス", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(849, 311, "ストリンダー", "ハイキョダイ", new uint[] { 75, 98, 70, 114, 70, 75 }, (PokeType.Electric, PokeType.Poison), new string[] { "パンクロック", "プラス", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(849, 311, "ストリンダー", "ロー", new uint[] { 75, 98, 70, 114, 70, 75 }, (PokeType.Electric, PokeType.Poison), new string[] { "パンクロック", "マイナス", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(849, 311, "ストリンダー", "ローキョダイ", new uint[] { 75, 98, 70, 114, 70, 75 }, (PokeType.Electric, PokeType.Poison), new string[] { "パンクロック", "マイナス", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new Species(850, 159, "ヤクデ", "", new uint[] { 50, 65, 45, 50, 50, 45 }, (PokeType.Fire, PokeType.Bug), new string[] { "もらいび", "しろいけむり", "ほのおのからだ" }, GenderRatio.M1F1));
            DexData.Add(new Species(851, 160, "マルヤクデ", "", new uint[] { 100, 115, 65, 90, 90, 65 }, (PokeType.Fire, PokeType.Bug), new string[] { "もらいび", "しろいけむり", "ほのおのからだ" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(851, 160, "マルヤクデ", "キョダイ", new uint[] { 100, 115, 65, 90, 90, 65 }, (PokeType.Fire, PokeType.Bug), new string[] { "もらいび", "しろいけむり", "ほのおのからだ" }, GenderRatio.M1F1));
            DexData.Add(new Species(852, 351, "タタッコ", "", new uint[] { 50, 68, 60, 50, 50, 32 }, (PokeType.Fighting, PokeType.Non), new string[] { "じゅうなん", "じゅうなん", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new Species(853, 352, "オトスパス", "", new uint[] { 80, 118, 90, 70, 80, 42 }, (PokeType.Fighting, PokeType.Non), new string[] { "じゅうなん", "じゅうなん", "テクニシャン" }, GenderRatio.M1F1));
            DexData.Add(new Species(854, 335, "ヤバチャ", "", new uint[] { 40, 45, 45, 74, 54, 50 }, (PokeType.Ghost, PokeType.Non), new string[] { "くだけるよろい", "くだけるよろい", "のろわれボディ" }, GenderRatio.Genderless));
            DexData.Add(new Species(855, 336, "ポットデス", "", new uint[] { 60, 65, 65, 134, 114, 70 }, (PokeType.Ghost, PokeType.Non), new string[] { "くだけるよろい", "くだけるよろい", "のろわれボディ" }, GenderRatio.Genderless));
            DexData.Add(new Species(856, 241, "ミブリム", "", new uint[] { 42, 30, 45, 56, 53, 39 }, (PokeType.Psychic, PokeType.Non), new string[] { "いやしのこころ", "きけんよち", "マジックミラー" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(857, 242, "テブリム", "", new uint[] { 57, 40, 65, 86, 73, 49 }, (PokeType.Psychic, PokeType.Non), new string[] { "いやしのこころ", "きけんよち", "マジックミラー" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(858, 243, "ブリムオン", "", new uint[] { 57, 90, 95, 136, 103, 29 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "いやしのこころ", "きけんよち", "マジックミラー" }, GenderRatio.FemaleOnly));
            DexData.Add(new AnotherForm(858, 243, "ブリムオン", "キョダイ", new uint[] { 57, 90, 95, 136, 103, 29 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "いやしのこころ", "きけんよち", "マジックミラー" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(859, 238, "ベロバー", "", new uint[] { 45, 45, 30, 55, 40, 50 }, (PokeType.Dark, PokeType.Fairy), new string[] { "いたずらごころ", "おみとおし", "わるいてぐせ" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(860, 239, "ギモー", "", new uint[] { 65, 60, 45, 75, 55, 70 }, (PokeType.Dark, PokeType.Fairy), new string[] { "いたずらごころ", "おみとおし", "わるいてぐせ" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(861, 240, "オーロンゲ", "", new uint[] { 95, 120, 65, 95, 75, 60 }, (PokeType.Dark, PokeType.Fairy), new string[] { "いたずらごころ", "おみとおし", "わるいてぐせ" }, GenderRatio.MaleOnly));
            DexData.Add(new AnotherForm(861, 240, "オーロンゲ", "キョダイ", new uint[] { 95, 120, 65, 95, 75, 60 }, (PokeType.Dark, PokeType.Fairy), new string[] { "いたずらごころ", "おみとおし", "わるいてぐせ" }, GenderRatio.MaleOnly));
            DexData.Add(new Species(862, 33, "タチフサグマ", "", new uint[] { 93, 90, 101, 60, 81, 95 }, (PokeType.Dark, PokeType.Normal), new string[] { "すてみ", "こんじょう", "まけんき" }, GenderRatio.M1F1));
            DexData.Add(new Species(863, 183, "ニャイキング", "", new uint[] { 70, 110, 100, 50, 60, 50 }, (PokeType.Steel, PokeType.Non), new string[] { "カブトアーマー", "かたいツメ", "はがねのせいしん" }, GenderRatio.M1F1));
            DexData.Add(new Species(864, 237, "サニゴーン", "", new uint[] { 60, 95, 50, 145, 130, 30 }, (PokeType.Ghost, PokeType.Non), new string[] { "くだけるよろい", "くだけるよろい", "ほろびのボディ" }, GenderRatio.M1F3));
            DexData.Add(new Species(865, 219, "ネギガナイト", "", new uint[] { 62, 135, 95, 68, 82, 65 }, (PokeType.Fighting, PokeType.Non), new string[] { "ふくつのこころ", "ふくつのこころ", "きもったま" }, GenderRatio.M1F1));
            DexData.Add(new Species(866, 366, "バリコオル", "", new uint[] { 80, 85, 75, 110, 100, 70 }, (PokeType.Ice, PokeType.Psychic), new string[] { "ちどりあし", "バリアフリー", "アイスボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(867, 328, "デスバーン", "", new uint[] { 58, 95, 145, 50, 105, 30 }, (PokeType.Ground, PokeType.Ghost), new string[] { "さまようたましい", "さまようたましい", "さまようたましい" }, GenderRatio.M1F1));
            DexData.Add(new Species(868, 185, "マホミル", "", new uint[] { 45, 40, 40, 50, 61, 34 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "アロマベール" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(869, 186, "マホイップ", "", new uint[] { 65, 60, 75, 110, 121, 64 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "アロマベール" }, GenderRatio.FemaleOnly));
            DexData.Add(new AnotherForm(869, 186, "マホイップ", "キョダイ", new uint[] { 65, 60, 75, 110, 121, 64 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "アロマベール" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(870, 345, "タイレーツ", "", new uint[] { 65, 100, 100, 70, 60, 75 }, (PokeType.Fighting, PokeType.Non), new string[] { "カブトアーマー", "カブトアーマー", "まけんき" }, GenderRatio.Genderless));
            DexData.Add(new Species(871, 353, "バチンウニ", "", new uint[] { 48, 101, 95, 91, 85, 15 }, (PokeType.Electric, PokeType.Non), new string[] { "ひらいしん", "ひらいしん", "エレキメイカー" }, GenderRatio.M1F1));
            DexData.Add(new Species(872, 349, "ユキハミ", "", new uint[] { 30, 25, 35, 45, 30, 20 }, (PokeType.Ice, PokeType.Bug), new string[] { "りんぷん", "りんぷん", "こおりのりんぷん" }, GenderRatio.M1F1));
            DexData.Add(new Species(873, 350, "モスノウ", "", new uint[] { 70, 65, 60, 125, 90, 65 }, (PokeType.Ice, PokeType.Bug), new string[] { "りんぷん", "りんぷん", "こおりのりんぷん" }, GenderRatio.M1F1));
            DexData.Add(new Species(874, 369, "イシヘンジン", "", new uint[] { 100, 125, 135, 20, 20, 70 }, (PokeType.Rock, PokeType.Non), new string[] { "パワースポット", "パワースポット", "パワースポット" }, GenderRatio.M1F1));
            DexData.Add(new Species(875, 370, "コオリッポ", "", new uint[] { 75, 80, 110, 65, 90, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "アイスフェイス", "アイスフェイス", "アイスフェイス" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(876, 337, "イエッサン", "♂", new uint[] { 60, 65, 55, 105, 95, 95 }, (PokeType.Psychic, PokeType.Normal), new string[] { "せいしんりょく", "シンクロ", "サイコメイカー" }, GenderRatio.MaleOnly));
            DexData.Add(new AnotherForm(876, 337, "イエッサン", "♀", new uint[] { 70, 55, 65, 95, 105, 85 }, (PokeType.Psychic, PokeType.Normal), new string[] { "マイペース", "シンクロ", "サイコメイカー" }, GenderRatio.FemaleOnly));
            DexData.Add(new Species(877, 344, "モルペコ", "", new uint[] { 58, 95, 58, 70, 58, 97 }, (PokeType.Electric, PokeType.Dark), new string[] { "はらぺこスイッチ", "はらぺこスイッチ", "はらぺこスイッチ" }, GenderRatio.M1F1));
            DexData.Add(new Species(878, 302, "ゾウドウ", "", new uint[] { 72, 80, 49, 40, 49, 40 }, (PokeType.Steel, PokeType.Non), new string[] { "ちからずく", "ちからずく", "ヘヴィメタル" }, GenderRatio.M1F1));
            DexData.Add(new Species(879, 303, "ダイオウドウ", "", new uint[] { 122, 130, 69, 80, 69, 30 }, (PokeType.Steel, PokeType.Non), new string[] { "ちからずく", "ちからずく", "ヘヴィメタル" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(879, 303, "ダイオウドウ", "キョダイ", new uint[] { 122, 130, 69, 80, 69, 30 }, (PokeType.Steel, PokeType.Non), new string[] { "ちからずく", "ちからずく", "ヘヴィメタル" }, GenderRatio.M1F1));
            DexData.Add(new Species(880, 374, "パッチラゴン", "", new uint[] { 90, 100, 90, 80, 70, 75 }, (PokeType.Electric, PokeType.Dragon), new string[] { "ちくでん", "はりきり", "すなかき" }, GenderRatio.Genderless));
            DexData.Add(new Species(881, 375, "パッチルドン", "", new uint[] { 90, 100, 90, 90, 80, 55 }, (PokeType.Electric, PokeType.Ice), new string[] { "ちくでん", "せいでんき", "ゆきかき" }, GenderRatio.Genderless));
            DexData.Add(new Species(882, 376, "ウオノラゴン", "", new uint[] { 90, 90, 100, 70, 80, 75 }, (PokeType.Water, PokeType.Dragon), new string[] { "ちょすい", "がんじょうあご", "すなかき" }, GenderRatio.Genderless));
            DexData.Add(new Species(883, 377, "ウオチルドン", "", new uint[] { 90, 90, 100, 80, 90, 55 }, (PokeType.Water, PokeType.Ice), new string[] { "ちょすい", "アイスボディ", "ゆきかき" }, GenderRatio.Genderless));
            DexData.Add(new Species(884, 371, "ジュラルドン", "", new uint[] { 70, 95, 115, 120, 50, 85 }, (PokeType.Steel, PokeType.Dragon), new string[] { "ライトメタル", "ヘヴィメタル", "すじがねいり" }, GenderRatio.M1F1));
            DexData.Add(new AnotherForm(884, 371, "ジュラルドン", "キョダイ", new uint[] { 70, 95, 115, 120, 50, 85 }, (PokeType.Steel, PokeType.Dragon), new string[] { "ライトメタル", "ヘヴィメタル", "すじがねいり" }, GenderRatio.M1F1));
            DexData.Add(new Species(885, 395, "ドラメシヤ", "", new uint[] { 28, 60, 30, 40, 30, 82 }, (PokeType.Dragon, PokeType.Ghost), new string[] { "クリアボディ", "すりぬけ", "のろわれボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(886, 396, "ドロンチ", "", new uint[] { 68, 80, 50, 60, 50, 102 }, (PokeType.Dragon, PokeType.Ghost), new string[] { "クリアボディ", "すりぬけ", "のろわれボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(887, 397, "ドラパルト", "", new uint[] { 88, 120, 75, 100, 75, 142 }, (PokeType.Dragon, PokeType.Ghost), new string[] { "クリアボディ", "すりぬけ", "のろわれボディ" }, GenderRatio.M1F1));
            DexData.Add(new Species(888, 398, "ザシアン", "歴戦の勇者", new uint[] { 92, 130, 115, 80, 115, 138 }, (PokeType.Fairy, PokeType.Non), new string[] { "ふとうのけん", "ふとうのけん", "ふとうのけん" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(888, 398, "ザシアン", "剣の王", new uint[] { 92, 170, 115, 80, 115, 148 }, (PokeType.Fairy, PokeType.Steel), new string[] { "ふとうのけん", "ふとうのけん", "ふとうのけん" }, GenderRatio.Genderless));
            DexData.Add(new Species(889, 399, "ザマゼンタ", "歴戦の勇者", new uint[] { 92, 130, 115, 80, 115, 138 }, (PokeType.Fighting, PokeType.Non), new string[] { "ふくつのたて", "ふくつのたて", "ふくつのたて" }, GenderRatio.Genderless));
            DexData.Add(new AnotherForm(889, 399, "ザマゼンタ", "盾の王", new uint[] { 92, 130, 145, 80, 145, 128 }, (PokeType.Fighting, PokeType.Steel), new string[] { "ふくつのたて", "ふくつのたて", "ふくつのたて" }, GenderRatio.Genderless));
            DexData.Add(new Species(890, 400, "ムゲンダイナ", "", new uint[] { 140, 85, 95, 145, 95, 130 }, (PokeType.Poison, PokeType.Dragon), new string[] { "プレッシャー", "プレッシャー", "プレッシャー" }, GenderRatio.Genderless));

            // 名前+フォルムでDictionaryに追加。
            // フォルム名無しがないポケモンはDexDataの若いほうから.
            // DexDataを図鑑番号でDistinctする

            UniqueList = DexData.Distinct(new SpeciesComparer()).ToArray();
            UniqueDex = UniqueList.ToDictionary(_ => _.Name, _ => _);
            DexDictionary = DexData.ToDictionary(_ => _.Name + _.FormName, _ => _);
            FormDex = DexData.ToLookup(_ => _.Name);
        }
        class SpeciesComparer : IEqualityComparer<Species>
        {
            public bool Equals(Species x, Species y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null)
                    return false;
                return x.Name == y.Name;
            }

            public int GetHashCode(Species s) => s.Name.GetHashCode();
        }
    }
}
