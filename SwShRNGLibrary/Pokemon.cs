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
            public readonly int ArmorDexID;
            public readonly int CrownDexID;
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
                    Gender = gender
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
            internal Species(int dexID, int galarDexID, int armorDexID, string name, string formName, uint[] bs, (PokeType type1, PokeType type2) type, string[] ability, GenderRatio ratio)
            {
                DexID = dexID;
                GalarDexID = galarDexID;
                ArmorDexID = armorDexID;
                Name = name;
                FormName = formName;
                BS = bs;
                Ability = ability;
                Type = type;
                GenderRatio = ratio;
            }
            internal Species(int dexID, int galarDexID, int armorDexID, int crownDexID, string name, string formName, uint[] bs, (PokeType type1, PokeType type2) type, string[] ability, GenderRatio ratio)
            {
                DexID = dexID;
                GalarDexID = galarDexID;
                ArmorDexID = armorDexID;
                CrownDexID = crownDexID; 
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
            internal AnotherForm(int dexID, int galarDexID, int armorDexID, string name, string formName, uint[] bs, (PokeType type1, PokeType type2) type, string[] ability, GenderRatio ratio)
                : base(dexID, galarDexID, armorDexID, name, formName, bs, type, ability, ratio) { }
            internal AnotherForm(int dexID, int galarDexID, int armorDexID, int crownDexID, string name, string formName, uint[] bs, (PokeType type1, PokeType type2) type, string[] ability, GenderRatio ratio)
                : base(dexID, galarDexID, armorDexID, crownDexID, name, formName, bs, type, ability, ratio) { }
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
            public Individual SetShinyType(ShinyType value) { Shiny = value; return this; }
            internal Individual() { }

            public static Individual Empty = GetPokemon("Dummy").GetIndividual(0, new uint[6], 0, 0, Nature.Hardy, 0, Gender.Genderless);
        }

        private static readonly IReadOnlyList<Species> uniqueList;
        private static readonly List<Species> dexData;
        private static readonly ILookup<string, Species> formDex;
        private static readonly Dictionary<string, Species> uniqueDex;
        private static readonly Dictionary<string, Species> dexDictionary;
        private static readonly IReadOnlyList<Species> galarDex;
        private static readonly IReadOnlyList<Species> armorDex;
        private static readonly IReadOnlyList<Species> crownDex;

        private Pokemon() { }
        public static Species GetPokemon(string Name)
        {
            if (!uniqueDex.ContainsKey(Name)) throw new Exception($"{Name}は登録されていません");
            return uniqueDex[Name];
        }
        public static Species GetPokemon(string Name, string Form)
        {
            if (!dexDictionary.ContainsKey(Name + Form)) throw new Exception($"{Name + Form}は登録されていません");
            return dexDictionary[Name + Form];
        }
        public static Species GetPokemon(string Name, Language lang)
        {
            Name = Name.ToJPN(lang);
            if (!uniqueDex.ContainsKey(Name)) throw new Exception(Name);
            return uniqueDex[Name];
        }
        public static Species GetPokemon(string Name, string Form, Language lang)
        {
            Name = Name.ToJPN(lang);
            Form = Form.ToJPN(lang);
            if (!dexDictionary.ContainsKey(Name + Form)) throw new Exception(Name + Form);
            return dexDictionary[Name + Form];
        }
        public static IReadOnlyList<Species> GetAllForms(string Name)
        {
            return formDex[Name].ToArray();
        }
        public static IReadOnlyList<Species> GetAllForms(string Name, Language lang)
        {
            Name = Name.ToJPN(lang);
            if (!formDex.Contains(Name)) throw new Exception(Name);
            return formDex[Name].ToArray();
        }
        public static IReadOnlyList<Species> GetUniquePokemonList()=> uniqueList.Where(_ => _.Name != "Dummy").ToArray();
        public static IReadOnlyList<Species> GetAllPokemonList() => dexData.Where(_ => _.Name != "Dummy").ToArray();
        public static IReadOnlyList<Species> GetGalarDex()=> galarDex.ToArray();
        public static IReadOnlyList<Species> GetArmorDex() => armorDex.ToArray();
        public static IReadOnlyList<Species> GetCrownDex() => crownDex.ToArray();
        static Pokemon()
        {
            dexData = new List<Species>();

            dexData.Add(new Species(-1, -1, "Dummy", "Genderless", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.Genderless));
            dexData.Add(new Species(-1, -1, "Dummy", "MaleOnly", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(-1, -1, "Dummy", "M7F1", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.M7F1));
            dexData.Add(new Species(-1, -1, "Dummy", "M3F1", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.M3F1));
            dexData.Add(new Species(-1, -1, "Dummy", "M1F1", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.M1F1));
            dexData.Add(new Species(-1, -1, "Dummy", "M1F3", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.M1F3));
            dexData.Add(new Species(-1, -1, "Dummy", "FemaleOnly", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "特性1", "特性2", "隠れ特性" }, GenderRatio.FemaleOnly));

            dexData.Add(new Species(1, -1, 68, -1, "フシギダネ", "", new uint[] { 45, 49, 49, 65, 65, 45 }, (PokeType.Grass, PokeType.Poison), new string[] { "しんりょく", "しんりょく", "ようりょくそ" }, GenderRatio.M7F1));
            dexData.Add(new Species(2, -1, 69, -1, "フシギソウ", "", new uint[] { 60, 62, 63, 80, 80, 60 }, (PokeType.Grass, PokeType.Poison), new string[] { "しんりょく", "しんりょく", "ようりょくそ" }, GenderRatio.M7F1));
            dexData.Add(new Species(3, -1, 70, -1, "フシギバナ", "", new uint[] { 80, 82, 83, 100, 100, 80 }, (PokeType.Grass, PokeType.Poison), new string[] { "しんりょく", "しんりょく", "ようりょくそ" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(3, -1, 70, -1, "フシギバナ", "キョダイ", new uint[] { 80, 82, 83, 100, 100, 80 }, (PokeType.Grass, PokeType.Poison), new string[] { "しんりょく", "しんりょく", "ようりょくそ" }, GenderRatio.M7F1));
            dexData.Add(new Species(4, 378, -1, -1, "ヒトカゲ", "", new uint[] { 39, 52, 43, 60, 50, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "サンパワー" }, GenderRatio.M7F1));
            dexData.Add(new Species(5, 379, -1, -1, "リザード", "", new uint[] { 58, 64, 58, 80, 65, 80 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "サンパワー" }, GenderRatio.M7F1));
            dexData.Add(new Species(6, 380, -1, -1, "リザードン", "", new uint[] { 78, 84, 78, 109, 85, 100 }, (PokeType.Fire, PokeType.Flying), new string[] { "もうか", "もうか", "サンパワー" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(6, 380, -1, -1, "リザードン", "キョダイ", new uint[] { 78, 84, 78, 109, 85, 100 }, (PokeType.Fire, PokeType.Flying), new string[] { "もうか", "もうか", "サンパワー" }, GenderRatio.M7F1));
            dexData.Add(new Species(7, -1, 71, -1, "ゼニガメ", "", new uint[] { 44, 48, 65, 50, 64, 43 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "あめうけざら" }, GenderRatio.M7F1));
            dexData.Add(new Species(8, -1, 72, -1, "カメール", "", new uint[] { 59, 63, 80, 65, 80, 58 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "あめうけざら" }, GenderRatio.M7F1));
            dexData.Add(new Species(9, -1, 73, -1, "カメックス", "", new uint[] { 79, 83, 100, 85, 105, 78 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "あめうけざら" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(9, -1, 73, -1, "カメックス", "キョダイ", new uint[] { 79, 83, 100, 85, 105, 78 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "あめうけざら" }, GenderRatio.M7F1));
            dexData.Add(new Species(10, 13, -1, -1, "キャタピー", "", new uint[] { 45, 30, 35, 20, 20, 45 }, (PokeType.Bug, PokeType.Non), new string[] { "りんぷん", "りんぷん", "にげあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(11, 14, -1, -1, "トランセル", "", new uint[] { 50, 20, 55, 25, 25, 30 }, (PokeType.Bug, PokeType.Non), new string[] { "だっぴ", "だっぴ", "だっぴ" }, GenderRatio.M1F1));
            dexData.Add(new Species(12, 15, -1, -1, "バタフリー", "", new uint[] { 60, 45, 50, 90, 80, 70 }, (PokeType.Bug, PokeType.Flying), new string[] { "ふくがん", "ふくがん", "いろめがね" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(12, 15, -1, -1, "バタフリー", "キョダイ", new uint[] { 60, 45, 50, 90, 80, 70 }, (PokeType.Bug, PokeType.Flying), new string[] { "ふくがん", "ふくがん", "いろめがね" }, GenderRatio.M1F1));
            dexData.Add(new Species(13, -1, -1, -1, "ビードル", "", new uint[] { 40, 35, 30, 20, 20, 50 }, (PokeType.Bug, PokeType.Poison), new string[] { "りんぷん", "りんぷん", "にげあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(14, -1, -1, -1, "コクーン", "", new uint[] { 45, 25, 50, 25, 25, 35 }, (PokeType.Bug, PokeType.Poison), new string[] { "だっぴ", "だっぴ", "だっぴ" }, GenderRatio.M1F1));
            dexData.Add(new Species(15, -1, -1, -1, "スピアー", "", new uint[] { 65, 90, 40, 45, 80, 75 }, (PokeType.Bug, PokeType.Poison), new string[] { "むしのしらせ", "むしのしらせ", "スナイパー" }, GenderRatio.M1F1));
            dexData.Add(new Species(16, -1, -1, -1, "ポッポ", "", new uint[] { 40, 45, 40, 35, 35, 56 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "ちどりあし", "はとむね" }, GenderRatio.M1F1));
            dexData.Add(new Species(17, -1, -1, -1, "ピジョン", "", new uint[] { 63, 60, 55, 50, 50, 71 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "ちどりあし", "はとむね" }, GenderRatio.M1F1));
            dexData.Add(new Species(18, -1, -1, -1, "ピジョット", "", new uint[] { 83, 80, 75, 70, 70, 101 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "ちどりあし", "はとむね" }, GenderRatio.M1F1));
            dexData.Add(new Species(19, -1, -1, -1, "コラッタ", "", new uint[] { 30, 56, 35, 25, 35, 72 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "こんじょう", "はりきり" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(19, -1, -1, -1, "コラッタ", "アローラ", new uint[] { 30, 56, 35, 25, 35, 72 }, (PokeType.Dark, PokeType.Normal), new string[] { "くいしんぼう", "はりきり", "あついしぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(20, -1, -1, -1, "ラッタ", "", new uint[] { 55, 81, 60, 50, 70, 97 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "こんじょう", "はりきり" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(20, -1, -1, -1, "ラッタ", "アローラ", new uint[] { 75, 71, 70, 40, 80, 77 }, (PokeType.Dark, PokeType.Normal), new string[] { "くいしんぼう", "はりきり", "あついしぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(21, -1, -1, -1, "オニスズメ", "", new uint[] { 40, 60, 30, 31, 31, 70 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "するどいめ", "スナイパー" }, GenderRatio.M1F1));
            dexData.Add(new Species(22, -1, -1, -1, "オニドリル", "", new uint[] { 65, 90, 65, 61, 61, 100 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "するどいめ", "スナイパー" }, GenderRatio.M1F1));
            dexData.Add(new Species(23, -1, -1, -1, "アーボ", "", new uint[] { 35, 60, 44, 40, 54, 55 }, (PokeType.Poison, PokeType.Non), new string[] { "いかく", "だっぴ", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(24, -1, -1, -1, "アーボック", "", new uint[] { 60, 95, 69, 65, 79, 80 }, (PokeType.Poison, PokeType.Non), new string[] { "いかく", "だっぴ", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(25, 194, 85, -1, "ピカチュウ", "", new uint[] { 35, 55, 40, 50, 50, 90 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "ひらいしん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(25, 194, 85, -1, "ピカチュウ", "キョダイ", new uint[] { 35, 55, 40, 50, 50, 90 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "ひらいしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(26, 195, 86, -1, "ライチュウ", "", new uint[] { 60, 90, 55, 90, 80, 110 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "ひらいしん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(26, 195, 86, -1, "ライチュウ", "アローラ", new uint[] { 60, 85, 50, 95, 85, 110 }, (PokeType.Electric, PokeType.Psychic), new string[] { "サーフテール", "サーフテール", "サーフテール" }, GenderRatio.M1F1));
            dexData.Add(new Species(27, -1, 168, -1, "サンド", "", new uint[] { 50, 75, 85, 20, 30, 40 }, (PokeType.Ground, PokeType.Non), new string[] { "すながくれ", "すながくれ", "すなかき" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(27, -1, 168, -1, "サンド", "アローラ", new uint[] { 50, 75, 90, 10, 35, 40 }, (PokeType.Ice, PokeType.Steel), new string[] { "ゆきがくれ", "ゆきがくれ", "ゆきかき" }, GenderRatio.M1F1));
            dexData.Add(new Species(28, -1, 169, -1, "サンドパン", "", new uint[] { 75, 100, 110, 45, 55, 65 }, (PokeType.Ground, PokeType.Non), new string[] { "すながくれ", "すながくれ", "すなかき" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(28, -1, 169, -1, "サンドパン", "アローラ", new uint[] { 75, 100, 120, 25, 65, 65 }, (PokeType.Ice, PokeType.Steel), new string[] { "ゆきがくれ", "ゆきがくれ", "ゆきかき" }, GenderRatio.M1F1));
            dexData.Add(new Species(29, -1, -1, 65, "ニドラン♀", "", new uint[] { 55, 47, 52, 40, 40, 41 }, (PokeType.Poison, PokeType.Non), new string[] { "どくのトゲ", "とうそうしん", "はりきり" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(30, -1, -1, 66, "ニドリーナ", "", new uint[] { 70, 62, 67, 55, 55, 56 }, (PokeType.Poison, PokeType.Non), new string[] { "どくのトゲ", "とうそうしん", "はりきり" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(31, -1, -1, 67, "ニドクイン", "", new uint[] { 90, 92, 87, 75, 85, 76 }, (PokeType.Poison, PokeType.Ground), new string[] { "どくのトゲ", "とうそうしん", "ちからずく" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(32, -1, -1, 68, "ニドラン♂", "", new uint[] { 46, 57, 40, 40, 40, 50 }, (PokeType.Poison, PokeType.Non), new string[] { "どくのトゲ", "とうそうしん", "はりきり" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(33, -1, -1, 69, "ニドリーノ", "", new uint[] { 61, 72, 57, 55, 55, 65 }, (PokeType.Poison, PokeType.Non), new string[] { "どくのトゲ", "とうそうしん", "はりきり" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(34, -1, -1, 70, "ニドキング", "", new uint[] { 81, 102, 77, 85, 75, 85 }, (PokeType.Poison, PokeType.Ground), new string[] { "どくのトゲ", "とうそうしん", "ちからずく" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(35, 255, -1, 44, "ピッピ", "", new uint[] { 70, 45, 48, 60, 65, 35 }, (PokeType.Fairy, PokeType.Non), new string[] { "メロメロボディ", "マジックガード", "フレンドガード" }, GenderRatio.M1F3));
            dexData.Add(new Species(36, 256, -1, 45, "ピクシー", "", new uint[] { 95, 70, 73, 95, 90, 60 }, (PokeType.Fairy, PokeType.Non), new string[] { "メロメロボディ", "マジックガード", "てんねん" }, GenderRatio.M1F3));
            dexData.Add(new Species(37, 68, -1, -1, "ロコン", "", new uint[] { 38, 41, 40, 50, 65, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "もらいび", "もらいび", "ひでり" }, GenderRatio.M1F3));
            dexData.Add(new AnotherForm(37, 68, -1, -1, "ロコン", "アローラ", new uint[] { 38, 41, 40, 50, 65, 65 }, (PokeType.Ice, PokeType.Non), new string[] { "ゆきがくれ", "ゆきがくれ", "ゆきふらし" }, GenderRatio.M1F3));
            dexData.Add(new Species(38, 69, -1, -1, "キュウコン", "", new uint[] { 73, 76, 75, 81, 100, 100 }, (PokeType.Fire, PokeType.Non), new string[] { "もらいび", "もらいび", "ひでり" }, GenderRatio.M1F3));
            dexData.Add(new AnotherForm(38, 69, -1, -1, "キュウコン", "アローラ", new uint[] { 73, 67, 75, 81, 100, 109 }, (PokeType.Ice, PokeType.Fairy), new string[] { "ゆきがくれ", "ゆきがくれ", "ゆきふらし" }, GenderRatio.M1F3));
            dexData.Add(new Species(39, -1, 12, -1, "プリン", "", new uint[] { 115, 45, 20, 45, 25, 20 }, (PokeType.Normal, PokeType.Fairy), new string[] { "メロメロボディ", "かちき", "フレンドガード" }, GenderRatio.M1F3));
            dexData.Add(new Species(40, -1, 13, -1, "プクリン", "", new uint[] { 140, 70, 45, 85, 50, 45 }, (PokeType.Normal, PokeType.Fairy), new string[] { "メロメロボディ", "かちき", "おみとおし" }, GenderRatio.M1F3));
            dexData.Add(new Species(41, -1, -1, 144, "ズバット", "", new uint[] { 40, 45, 35, 30, 40, 55 }, (PokeType.Poison, PokeType.Flying), new string[] { "せいしんりょく", "せいしんりょく", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(42, -1, -1, 145, "ゴルバット", "", new uint[] { 75, 80, 70, 65, 75, 90 }, (PokeType.Poison, PokeType.Flying), new string[] { "せいしんりょく", "せいしんりょく", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(43, 55, -1, -1, "ナゾノクサ", "", new uint[] { 45, 50, 55, 75, 65, 30 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "にげあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(44, 56, -1, -1, "クサイハナ", "", new uint[] { 60, 65, 70, 85, 75, 40 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "あくしゅう" }, GenderRatio.M1F1));
            dexData.Add(new Species(45, 57, -1, -1, "ラフレシア", "", new uint[] { 75, 80, 85, 110, 90, 50 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "ほうし" }, GenderRatio.M1F1));
            dexData.Add(new Species(46, -1, -1, -1, "パラス", "", new uint[] { 35, 70, 55, 45, 55, 25 }, (PokeType.Bug, PokeType.Grass), new string[] { "ほうし", "かんそうはだ", "しめりけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(47, -1, -1, -1, "パラセクト", "", new uint[] { 60, 95, 80, 60, 80, 30 }, (PokeType.Bug, PokeType.Grass), new string[] { "ほうし", "かんそうはだ", "しめりけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(48, -1, -1, -1, "コンパン", "", new uint[] { 60, 55, 50, 40, 55, 45 }, (PokeType.Bug, PokeType.Poison), new string[] { "ふくがん", "いろめがね", "にげあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(49, -1, -1, -1, "モルフォン", "", new uint[] { 70, 65, 60, 90, 75, 90 }, (PokeType.Bug, PokeType.Poison), new string[] { "りんぷん", "いろめがね", "ミラクルスキン" }, GenderRatio.M1F1));
            dexData.Add(new Species(50, 164, -1, -1, "ディグダ", "", new uint[] { 10, 55, 25, 35, 45, 95 }, (PokeType.Ground, PokeType.Non), new string[] { "すながくれ", "ありじごく", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(50, 164, -1, -1, "ディグダ", "アローラ", new uint[] { 10, 55, 30, 35, 45, 90 }, (PokeType.Ground, PokeType.Steel), new string[] { "すながくれ", "カーリーヘアー", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(51, 165, -1, -1, "ダグトリオ", "", new uint[] { 35, 100, 50, 50, 70, 120 }, (PokeType.Ground, PokeType.Non), new string[] { "すながくれ", "ありじごく", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(51, 165, -1, -1, "ダグトリオ", "アローラ", new uint[] { 35, 100, 60, 50, 70, 110 }, (PokeType.Ground, PokeType.Steel), new string[] { "すながくれ", "カーリーヘアー", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(52, 182, -1, -1, "ニャース", "", new uint[] { 40, 45, 35, 40, 40, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "テクニシャン", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(52, 182, -1, -1, "ニャース", "キョダイ", new uint[] { 40, 45, 35, 40, 40, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "テクニシャン", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(52, 182, -1, -1, "ニャース", "アローラ", new uint[] { 40, 35, 35, 50, 40, 90 }, (PokeType.Dark, PokeType.Non), new string[] { "ものひろい", "テクニシャン", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(52, 182, -1, -1, "ニャース", "ガラル", new uint[] { 50, 65, 55, 40, 40, 40 }, (PokeType.Steel, PokeType.Non), new string[] { "ものひろい", "かたいツメ", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(53, 184, -1, -1, "ペルシアン", "", new uint[] { 65, 70, 60, 65, 65, 115 }, (PokeType.Normal, PokeType.Non), new string[] { "じゅうなん", "テクニシャン", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(53, 184, -1, -1, "ペルシアン", "アローラ", new uint[] { 65, 60, 60, 75, 65, 115 }, (PokeType.Dark, PokeType.Non), new string[] { "ファーコート", "テクニシャン", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(54, -1, 146, -1, "コダック", "", new uint[] { 50, 52, 48, 65, 50, 55 }, (PokeType.Water, PokeType.Non), new string[] { "しめりけ", "ノーてんき", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(55, -1, 147, -1, "ゴルダック", "", new uint[] { 80, 82, 78, 95, 80, 85 }, (PokeType.Water, PokeType.Non), new string[] { "しめりけ", "ノーてんき", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(56, -1, -1, -1, "マンキー", "", new uint[] { 40, 80, 35, 35, 45, 70 }, (PokeType.Fighting, PokeType.Non), new string[] { "やるき", "いかりのつぼ", "まけんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(57, -1, -1, -1, "オコリザル", "", new uint[] { 65, 105, 60, 60, 70, 95 }, (PokeType.Fighting, PokeType.Non), new string[] { "やるき", "いかりのつぼ", "まけんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(58, 70, -1, -1, "ガーディ", "", new uint[] { 55, 70, 45, 70, 50, 60 }, (PokeType.Fire, PokeType.Non), new string[] { "いかく", "もらいび", "せいぎのこころ" }, GenderRatio.M3F1));
            dexData.Add(new Species(59, 71, -1, -1, "ウインディ", "", new uint[] { 90, 110, 80, 100, 80, 95 }, (PokeType.Fire, PokeType.Non), new string[] { "いかく", "もらいび", "せいぎのこころ" }, GenderRatio.M3F1));
            dexData.Add(new Species(60, -1, 142, -1, "ニョロモ", "", new uint[] { 40, 50, 40, 40, 40, 90 }, (PokeType.Water, PokeType.Non), new string[] { "ちょすい", "しめりけ", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(61, -1, 143, -1, "ニョロゾ", "", new uint[] { 65, 65, 65, 50, 50, 90 }, (PokeType.Water, PokeType.Non), new string[] { "ちょすい", "しめりけ", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(62, -1, 144, -1, "ニョロボン", "", new uint[] { 90, 95, 95, 70, 90, 70 }, (PokeType.Water, PokeType.Fighting), new string[] { "ちょすい", "しめりけ", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(63, -1, 31, -1, "ケーシィ", "", new uint[] { 25, 20, 15, 105, 55, 90 }, (PokeType.Psychic, PokeType.Non), new string[] { "シンクロ", "せいしんりょく", "マジックガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(64, -1, 32, -1, "ユンゲラー", "", new uint[] { 40, 35, 30, 120, 70, 105 }, (PokeType.Psychic, PokeType.Non), new string[] { "シンクロ", "せいしんりょく", "マジックガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(65, -1, 33, -1, "フーディン", "", new uint[] { 55, 50, 45, 135, 95, 120 }, (PokeType.Psychic, PokeType.Non), new string[] { "シンクロ", "せいしんりょく", "マジックガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(66, 138, -1, -1, "ワンリキー", "", new uint[] { 70, 80, 50, 35, 35, 35 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ノーガード", "ふくつのこころ" }, GenderRatio.M3F1));
            dexData.Add(new Species(67, 139, -1, -1, "ゴーリキー", "", new uint[] { 80, 100, 70, 50, 60, 45 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ノーガード", "ふくつのこころ" }, GenderRatio.M3F1));
            dexData.Add(new Species(68, 140, -1, -1, "カイリキー", "", new uint[] { 90, 130, 80, 65, 85, 55 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ノーガード", "ふくつのこころ" }, GenderRatio.M3F1));
            dexData.Add(new AnotherForm(68, 140, -1, -1, "カイリキー", "キョダイ", new uint[] { 90, 130, 80, 65, 85, 55 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ノーガード", "ふくつのこころ" }, GenderRatio.M3F1));
            dexData.Add(new Species(69, -1, -1, -1, "マダツボミ", "", new uint[] { 50, 75, 35, 70, 30, 40 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(70, -1, -1, -1, "ウツドン", "", new uint[] { 65, 90, 50, 85, 45, 55 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(71, -1, -1, -1, "ウツボット", "", new uint[] { 80, 105, 65, 100, 70, 70 }, (PokeType.Grass, PokeType.Poison), new string[] { "ようりょくそ", "ようりょくそ", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(72, -1, 40, -1, "メノクラゲ", "", new uint[] { 40, 40, 35, 50, 100, 70 }, (PokeType.Water, PokeType.Poison), new string[] { "クリアボディ", "ヘドロえき", "あめうけざら" }, GenderRatio.M1F1));
            dexData.Add(new Species(73, -1, 41, -1, "ドククラゲ", "", new uint[] { 80, 70, 65, 80, 120, 100 }, (PokeType.Water, PokeType.Poison), new string[] { "クリアボディ", "ヘドロえき", "あめうけざら" }, GenderRatio.M1F1));
            dexData.Add(new Species(74, -1, -1, -1, "イシツブテ", "", new uint[] { 40, 80, 100, 30, 30, 20 }, (PokeType.Rock, PokeType.Ground), new string[] { "いしあたま", "がんじょう", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(74, -1, -1, -1, "イシツブテ", "アローラ", new uint[] { 40, 80, 100, 30, 30, 20 }, (PokeType.Rock, PokeType.Electric), new string[] { "じりょく", "がんじょう", "エレキスキン" }, GenderRatio.M1F1));
            dexData.Add(new Species(75, -1, -1, -1, "ゴローン", "", new uint[] { 55, 95, 115, 45, 45, 35 }, (PokeType.Rock, PokeType.Ground), new string[] { "いしあたま", "がんじょう", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(75, -1, -1, -1, "ゴローン", "アローラ", new uint[] { 55, 95, 115, 45, 45, 35 }, (PokeType.Rock, PokeType.Electric), new string[] { "じりょく", "がんじょう", "エレキスキン" }, GenderRatio.M1F1));
            dexData.Add(new Species(76, -1, -1, -1, "ゴローニャ", "", new uint[] { 80, 120, 130, 55, 65, 45 }, (PokeType.Rock, PokeType.Ground), new string[] { "いしあたま", "がんじょう", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(76, -1, -1, -1, "ゴローニャ", "アローラ", new uint[] { 80, 120, 130, 55, 65, 45 }, (PokeType.Rock, PokeType.Electric), new string[] { "じりょく", "がんじょう", "エレキスキン" }, GenderRatio.M1F1));
            dexData.Add(new Species(77, 333, -1, 105, "ポニータ", "", new uint[] { 50, 85, 55, 65, 65, 90 }, (PokeType.Fire, PokeType.Non), new string[] { "にげあし", "もらいび", "ほのおのからだ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(77, 333, -1, 105, "ポニータ", "ガラル", new uint[] { 50, 85, 55, 65, 65, 90 }, (PokeType.Psychic, PokeType.Non), new string[] { "にげあし", "パステルベール", "きけんよち" }, GenderRatio.M1F1));
            dexData.Add(new Species(78, 334, -1, 106, "ギャロップ", "", new uint[] { 65, 100, 70, 80, 80, 105 }, (PokeType.Fire, PokeType.Non), new string[] { "にげあし", "もらいび", "ほのおのからだ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(78, 334, -1, 106, "ギャロップ", "ガラル", new uint[] { 65, 100, 70, 80, 80, 105 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "にげあし", "パステルベール", "きけんよち" }, GenderRatio.M1F1));
            dexData.Add(new Species(79, -1, 1, -1, "ヤドン", "", new uint[] { 90, 65, 65, 40, 40, 15 }, (PokeType.Water, PokeType.Psychic), new string[] { "どんかん", "マイペース", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(79, -1, 1, -1, "ヤドン", "ガラル", new uint[] { 90, 65, 65, 40, 40, 15 }, (PokeType.Psychic, PokeType.Non), new string[] { "くいしんぼう", "マイペース", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(80, -1, 2, -1, "ヤドラン", "", new uint[] { 95, 75, 110, 100, 80, 30 }, (PokeType.Water, PokeType.Psychic), new string[] { "どんかん", "マイペース", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(80, -1, 2, -1, "ヤドラン", "ガラル", new uint[] { 95, 100, 95, 100, 70, 30 }, (PokeType.Poison, PokeType.Psychic), new string[] { "クイックドロウ", "マイペース", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(81, -1, 105, -1, "コイル", "", new uint[] { 25, 35, 70, 95, 55, 45 }, (PokeType.Electric, PokeType.Steel), new string[] { "じりょく", "がんじょう", "アナライズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(82, -1, 106, -1, "レアコイル", "", new uint[] { 50, 60, 95, 120, 70, 70 }, (PokeType.Electric, PokeType.Steel), new string[] { "じりょく", "がんじょう", "アナライズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(83, 218, -1, -1, "カモネギ", "", new uint[] { 52, 90, 55, 58, 62, 60 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "せいしんりょく", "まけんき" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(83, 218, -1, -1, "カモネギ", "ガラル", new uint[] { 52, 95, 55, 58, 62, 55 }, (PokeType.Fighting, PokeType.Non), new string[] { "ふくつのこころ", "ふくつのこころ", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(84, -1, -1, -1, "ドードー", "", new uint[] { 35, 85, 45, 35, 35, 75 }, (PokeType.Normal, PokeType.Flying), new string[] { "にげあし", "はやおき", "ちどりあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(85, -1, -1, -1, "ドードリオ", "", new uint[] { 60, 110, 70, 60, 60, 110 }, (PokeType.Normal, PokeType.Flying), new string[] { "にげあし", "はやおき", "ちどりあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(86, -1, -1, -1, "パウワウ", "", new uint[] { 65, 45, 55, 45, 70, 45 }, (PokeType.Water, PokeType.Non), new string[] { "あついしぼう", "うるおいボディ", "アイスボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(87, -1, -1, -1, "ジュゴン", "", new uint[] { 90, 70, 80, 70, 95, 70 }, (PokeType.Water, PokeType.Ice), new string[] { "あついしぼう", "うるおいボディ", "アイスボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(88, -1, -1, -1, "ベトベター", "", new uint[] { 80, 80, 50, 40, 50, 25 }, (PokeType.Poison, PokeType.Non), new string[] { "あくしゅう", "ねんちゃく", "どくしゅ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(88, -1, -1, -1, "ベトベター", "アローラ", new uint[] { 80, 80, 50, 40, 50, 25 }, (PokeType.Poison, PokeType.Dark), new string[] { "どくしゅ", "くいしんぼう", "かがくのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(89, -1, -1, -1, "ベトベトン", "", new uint[] { 105, 105, 75, 65, 100, 50 }, (PokeType.Poison, PokeType.Non), new string[] { "あくしゅう", "ねんちゃく", "どくしゅ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(89, -1, -1, -1, "ベトベトン", "アローラ", new uint[] { 105, 105, 75, 65, 100, 50 }, (PokeType.Poison, PokeType.Dark), new string[] { "どくしゅ", "くいしんぼう", "かがくのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(90, 150, 131, -1, "シェルダー", "", new uint[] { 30, 65, 100, 45, 25, 40 }, (PokeType.Water, PokeType.Non), new string[] { "シェルアーマー", "スキルリンク", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(91, 151, 132, -1, "パルシェン", "", new uint[] { 50, 95, 180, 85, 45, 70 }, (PokeType.Water, PokeType.Ice), new string[] { "シェルアーマー", "スキルリンク", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(92, 141, -1, -1, "ゴース", "", new uint[] { 30, 35, 30, 100, 35, 80 }, (PokeType.Ghost, PokeType.Poison), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(93, 142, -1, -1, "ゴースト", "", new uint[] { 45, 50, 45, 115, 55, 95 }, (PokeType.Ghost, PokeType.Poison), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(94, 143, -1, -1, "ゲンガー", "", new uint[] { 60, 65, 60, 130, 75, 110 }, (PokeType.Ghost, PokeType.Poison), new string[] { "のろわれボディ", "のろわれボディ", "のろわれボディ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(94, 143, -1, -1, "ゲンガー", "キョダイ", new uint[] { 60, 65, 60, 130, 75, 110 }, (PokeType.Ghost, PokeType.Poison), new string[] { "のろわれボディ", "のろわれボディ", "のろわれボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(95, 178, -1, -1, "イワーク", "", new uint[] { 35, 45, 160, 30, 45, 70 }, (PokeType.Rock, PokeType.Ground), new string[] { "いしあたま", "がんじょう", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(96, -1, -1, -1, "スリープ", "", new uint[] { 60, 48, 45, 43, 90, 42 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふみん", "よちむ", "せいしんりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(97, -1, -1, -1, "スリーパー", "", new uint[] { 85, 73, 70, 73, 115, 67 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふみん", "よちむ", "せいしんりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(98, 98, 38, -1, "クラブ", "", new uint[] { 30, 105, 90, 25, 25, 50 }, (PokeType.Water, PokeType.Non), new string[] { "かいりきバサミ", "シェルアーマー", "ちからずく" }, GenderRatio.M1F1));
            dexData.Add(new Species(99, 99, 39, -1, "キングラー", "", new uint[] { 55, 130, 115, 50, 50, 75 }, (PokeType.Water, PokeType.Non), new string[] { "かいりきバサミ", "シェルアーマー", "ちからずく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(99, 99, -1, -1, "キングラー", "キョダイ", new uint[] { 55, 130, 115, 50, 50, 75 }, (PokeType.Water, PokeType.Non), new string[] { "かいりきバサミ", "シェルアーマー", "ちからずく" }, GenderRatio.M1F1));
            dexData.Add(new Species(100, -1, -1, -1, "ビリリダマ", "", new uint[] { 40, 30, 50, 55, 55, 100 }, (PokeType.Electric, PokeType.Non), new string[] { "ぼうおん", "せいでんき", "ゆうばく" }, GenderRatio.Genderless));
            dexData.Add(new Species(101, -1, -1, -1, "マルマイン", "", new uint[] { 60, 50, 70, 80, 80, 150 }, (PokeType.Electric, PokeType.Non), new string[] { "ぼうおん", "せいでんき", "ゆうばく" }, GenderRatio.Genderless));
            dexData.Add(new Species(102, -1, 205, -1, "タマタマ", "", new uint[] { 60, 40, 80, 60, 45, 40 }, (PokeType.Grass, PokeType.Psychic), new string[] { "ようりょくそ", "ようりょくそ", "しゅうかく" }, GenderRatio.M1F1));
            dexData.Add(new Species(103, -1, 206, -1, "ナッシー", "", new uint[] { 95, 95, 85, 125, 75, 55 }, (PokeType.Grass, PokeType.Psychic), new string[] { "ようりょくそ", "ようりょくそ", "しゅうかく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(103, -1, 206, -1, "ナッシー", "アローラ", new uint[] { 95, 105, 85, 125, 75, 45 }, (PokeType.Grass, PokeType.Dragon), new string[] { "おみとおし", "おみとおし", "しゅうかく" }, GenderRatio.M1F1));
            dexData.Add(new Species(104, -1, 170, -1, "カラカラ", "", new uint[] { 50, 50, 95, 40, 50, 35 }, (PokeType.Ground, PokeType.Non), new string[] { "いしあたま", "ひらいしん", "カブトアーマー" }, GenderRatio.M1F1));
            dexData.Add(new Species(105, -1, 171, -1, "ガラガラ", "", new uint[] { 60, 80, 110, 50, 80, 45 }, (PokeType.Ground, PokeType.Non), new string[] { "いしあたま", "ひらいしん", "カブトアーマー" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(105, -1, 171, -1, "ガラガラ", "アローラ", new uint[] { 60, 80, 110, 50, 80, 45 }, (PokeType.Fire, PokeType.Ghost), new string[] { "のろわれボディ", "ひらいしん", "いしあたま" }, GenderRatio.M1F1));
            dexData.Add(new Species(106, 108, -1, -1, "サワムラー", "", new uint[] { 50, 120, 53, 35, 110, 87 }, (PokeType.Fighting, PokeType.Non), new string[] { "じゅうなん", "すてみ", "かるわざ" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(107, 109, -1, -1, "エビワラー", "", new uint[] { 50, 105, 79, 35, 110, 76 }, (PokeType.Fighting, PokeType.Non), new string[] { "するどいめ", "てつのこぶし", "せいしんりょく" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(108, -1, 54, -1, "ベロリンガ", "", new uint[] { 90, 55, 75, 60, 75, 30 }, (PokeType.Normal, PokeType.Non), new string[] { "マイペース", "どんかん", "ノーてんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(109, 250, -1, -1, "ドガース", "", new uint[] { 40, 65, 95, 60, 45, 35 }, (PokeType.Poison, PokeType.Non), new string[] { "ふゆう", "かがくへんかガス", "あくしゅう" }, GenderRatio.M1F1));
            dexData.Add(new Species(110, 251, -1, -1, "マタドガス", "", new uint[] { 65, 90, 120, 85, 70, 60 }, (PokeType.Poison, PokeType.Non), new string[] { "ふゆう", "かがくへんかガス", "あくしゅう" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(110, 251, -1, -1, "マタドガス", "ガラル", new uint[] { 65, 90, 120, 85, 70, 60 }, (PokeType.Poison, PokeType.Fairy), new string[] { "ふゆう", "かがくへんかガス", "ミストメイカー" }, GenderRatio.M1F1));
            dexData.Add(new Species(111, 264, 183, -1, "サイホーン", "", new uint[] { 80, 85, 95, 30, 30, 25 }, (PokeType.Ground, PokeType.Rock), new string[] { "ひらいしん", "いしあたま", "すてみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(112, 265, 184, -1, "サイドン", "", new uint[] { 105, 130, 120, 45, 45, 40 }, (PokeType.Ground, PokeType.Rock), new string[] { "ひらいしん", "いしあたま", "すてみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(113, -1, 7, -1, "ラッキー", "", new uint[] { 250, 5, 5, 35, 105, 50 }, (PokeType.Normal, PokeType.Non), new string[] { "しぜんかいふく", "てんのめぐみ", "いやしのこころ" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(114, -1, 80, -1, "モンジャラ", "", new uint[] { 65, 55, 115, 100, 40, 60 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "リーフガード", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(115, -1, 172, -1, "ガルーラ", "", new uint[] { 105, 95, 80, 40, 80, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "はやおき", "きもったま", "せいしんりょく" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(116, -1, 198, -1, "タッツー", "", new uint[] { 30, 40, 70, 70, 25, 60 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "スナイパー", "しめりけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(117, -1, 199, -1, "シードラ", "", new uint[] { 55, 65, 95, 95, 45, 85 }, (PokeType.Water, PokeType.Non), new string[] { "どくのトゲ", "スナイパー", "しめりけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(118, 146, 94, -1, "トサキント", "", new uint[] { 45, 67, 60, 35, 50, 63 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "みずのベール", "ひらいしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(119, 147, 95, -1, "アズマオウ", "", new uint[] { 80, 92, 65, 65, 80, 68 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "みずのベール", "ひらいしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(120, -1, 98, -1, "ヒトデマン", "", new uint[] { 30, 45, 55, 70, 55, 85 }, (PokeType.Water, PokeType.Non), new string[] { "はっこう", "しぜんかいふく", "アナライズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(121, -1, 99, -1, "スターミー", "", new uint[] { 60, 75, 85, 100, 85, 115 }, (PokeType.Water, PokeType.Psychic), new string[] { "はっこう", "しぜんかいふく", "アナライズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(122, 365, -1, 11, "バリヤード", "", new uint[] { 40, 45, 65, 100, 120, 90 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "ぼうおん", "フィルター", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(122, 365, -1, 11, "バリヤード", "ガラル", new uint[] { 50, 65, 65, 90, 90, 100 }, (PokeType.Ice, PokeType.Psychic), new string[] { "ちどりあし", "バリアフリー", "アイスボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(123, -1, 118, -1, "ストライク", "", new uint[] { 70, 110, 80, 55, 80, 105 }, (PokeType.Bug, PokeType.Flying), new string[] { "むしのしらせ", "テクニシャン", "ふくつのこころ" }, GenderRatio.M1F1));
            dexData.Add(new Species(124, -1, -1, 14, "ルージュラ", "", new uint[] { 65, 50, 35, 115, 95, 95 }, (PokeType.Ice, PokeType.Psychic), new string[] { "どんかん", "よちむ", "かんそうはだ" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(125, -1, -1, 16, "エレブー", "", new uint[] { 65, 83, 57, 95, 85, 105 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "やるき" }, GenderRatio.M3F1));
            dexData.Add(new Species(126, -1, -1, 19, "ブーバー", "", new uint[] { 65, 95, 57, 100, 85, 93 }, (PokeType.Fire, PokeType.Non), new string[] { "ほのおのからだ", "ほのおのからだ", "やるき" }, GenderRatio.M3F1));
            dexData.Add(new Species(127, -1, 120, -1, "カイロス", "", new uint[] { 65, 125, 100, 55, 70, 85 }, (PokeType.Bug, PokeType.Non), new string[] { "かいりきバサミ", "かたやぶり", "じしんかじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(128, -1, 116, -1, "ケンタロス", "", new uint[] { 75, 100, 95, 40, 70, 110 }, (PokeType.Normal, PokeType.Non), new string[] { "いかく", "いかりのつぼ", "ちからずく" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(129, 144, 42, 62, "コイキング", "", new uint[] { 20, 10, 55, 15, 20, 80 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(130, 145, 43, 63, "ギャラドス", "", new uint[] { 95, 125, 79, 60, 100, 81 }, (PokeType.Water, PokeType.Flying), new string[] { "いかく", "いかく", "じしんかじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(131, 361, -1, 190, "ラプラス", "", new uint[] { 130, 85, 80, 85, 95, 60 }, (PokeType.Water, PokeType.Ice), new string[] { "ちょすい", "シェルアーマー", "うるおいボディ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(131, 361, -1, 190, "ラプラス", "キョダイ", new uint[] { 130, 85, 80, 85, 95, 60 }, (PokeType.Water, PokeType.Ice), new string[] { "ちょすい", "シェルアーマー", "うるおいボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(132, 373, 207, -1, "メタモン", "", new uint[] { 48, 48, 48, 48, 48, 48 }, (PokeType.Normal, PokeType.Non), new string[] { "じゅうなん", "じゅうなん", "かわりもの" }, GenderRatio.Genderless));
            dexData.Add(new Species(133, 196, -1, 74, "イーブイ", "", new uint[] { 55, 55, 50, 45, 65, 55 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "てきおうりょく", "きけんよち" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(133, 196, -1, 74, "イーブイ", "キョダイ", new uint[] { 55, 55, 50, 45, 65, 55 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "てきおうりょく", "きけんよち" }, GenderRatio.M7F1));
            dexData.Add(new Species(134, 197, -1, 75, "シャワーズ", "", new uint[] { 130, 65, 60, 110, 95, 65 }, (PokeType.Water, PokeType.Non), new string[] { "ちょすい", "ちょすい", "うるおいボディ" }, GenderRatio.M7F1));
            dexData.Add(new Species(135, 198, -1, 76, "サンダース", "", new uint[] { 65, 65, 60, 110, 95, 130 }, (PokeType.Electric, PokeType.Non), new string[] { "ちくでん", "ちくでん", "はやあし" }, GenderRatio.M7F1));
            dexData.Add(new Species(136, 199, -1, 77, "ブースター", "", new uint[] { 65, 130, 60, 95, 110, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "もらいび", "もらいび", "こんじょう" }, GenderRatio.M7F1));
            dexData.Add(new Species(137, -1, 208, -1, "ポリゴン", "", new uint[] { 65, 60, 70, 85, 75, 40 }, (PokeType.Normal, PokeType.Non), new string[] { "トレース", "ダウンロード", "アナライズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(138, -1, -1, 123, "オムナイト", "", new uint[] { 35, 40, 100, 90, 55, 35 }, (PokeType.Rock, PokeType.Water), new string[] { "すいすい", "シェルアーマー", "くだけるよろい" }, GenderRatio.M7F1));
            dexData.Add(new Species(139, -1, -1, 124, "オムスター", "", new uint[] { 70, 60, 125, 115, 70, 55 }, (PokeType.Rock, PokeType.Water), new string[] { "すいすい", "シェルアーマー", "くだけるよろい" }, GenderRatio.M7F1));
            dexData.Add(new Species(140, -1, -1, 125, "カブト", "", new uint[] { 30, 80, 90, 55, 45, 55 }, (PokeType.Rock, PokeType.Water), new string[] { "すいすい", "カブトアーマー", "くだけるよろい" }, GenderRatio.M7F1));
            dexData.Add(new Species(141, -1, -1, 126, "カブトプス", "", new uint[] { 60, 115, 105, 65, 70, 80 }, (PokeType.Rock, PokeType.Water), new string[] { "すいすい", "カブトアーマー", "くだけるよろい" }, GenderRatio.M7F1));
            dexData.Add(new Species(142, -1, -1, 127, "プテラ", "", new uint[] { 80, 105, 65, 60, 75, 130 }, (PokeType.Rock, PokeType.Flying), new string[] { "いしあたま", "プレッシャー", "きんちょうかん" }, GenderRatio.M7F1));
            dexData.Add(new Species(143, 261, -1, 173, "カビゴン", "", new uint[] { 160, 110, 65, 65, 110, 30 }, (PokeType.Normal, PokeType.Non), new string[] { "めんえき", "あついしぼう", "くいしんぼう" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(143, 261, -1, 173, "カビゴン", "キョダイ", new uint[] { 160, 110, 65, 65, 110, 30 }, (PokeType.Normal, PokeType.Non), new string[] { "めんえき", "あついしぼう", "くいしんぼう" }, GenderRatio.M7F1));
            dexData.Add(new Species(144, -1, -1, 202, "フリーザー", "", new uint[] { 90, 85, 100, 95, 125, 85 }, (PokeType.Ice, PokeType.Flying), new string[] { "プレッシャー", "プレッシャー", "ゆきがくれ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(144, -1, -1, 202, "フリーザー", "ガラル", new uint[] { 90, 85, 85, 125, 100, 95 }, (PokeType.Psychic, PokeType.Flying), new string[] { "かちき", "かちき", "かちき" }, GenderRatio.Genderless));
            dexData.Add(new Species(145, -1, -1, 203, "サンダー", "", new uint[] { 90, 90, 85, 125, 90, 100 }, (PokeType.Electric, PokeType.Flying), new string[] { "プレッシャー", "プレッシャー", "せいでんき" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(145, -1, -1, 203, "サンダー", "ガラル", new uint[] { 90, 125, 90, 85, 90, 100 }, (PokeType.Fighting, PokeType.Flying), new string[] { "まけんき", "まけんき", "まけんき" }, GenderRatio.Genderless));
            dexData.Add(new Species(146, -1, -1, 204, "ファイヤー", "", new uint[] { 90, 100, 90, 125, 85, 90 }, (PokeType.Fire, PokeType.Flying), new string[] { "プレッシャー", "プレッシャー", "ほのおのからだ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(146, -1, -1, 204, "ファイヤー", "ガラル", new uint[] { 90, 85, 90, 100, 125, 90 }, (PokeType.Dark, PokeType.Flying), new string[] { "ぎゃくじょう", "ぎゃくじょう", "ぎゃくじょう" }, GenderRatio.Genderless));
            dexData.Add(new Species(147, -1, -1, 194, "ミニリュウ", "", new uint[] { 41, 64, 45, 50, 50, 50 }, (PokeType.Dragon, PokeType.Non), new string[] { "だっぴ", "だっぴ", "ふしぎなうろこ" }, GenderRatio.M1F1));
            dexData.Add(new Species(148, -1, -1, 195, "ハクリュー", "", new uint[] { 61, 84, 65, 70, 70, 70 }, (PokeType.Dragon, PokeType.Non), new string[] { "だっぴ", "だっぴ", "ふしぎなうろこ" }, GenderRatio.M1F1));
            dexData.Add(new Species(149, -1, -1, 196, "カイリュー", "", new uint[] { 91, 134, 95, 100, 100, 80 }, (PokeType.Dragon, PokeType.Flying), new string[] { "せいしんりょく", "せいしんりょく", "マルチスケイル" }, GenderRatio.M1F1));
            dexData.Add(new Species(150, -1, -1, -1, "ミュウツー", "", new uint[] { 106, 110, 90, 154, 90, 130 }, (PokeType.Psychic, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "きんちょうかん" }, GenderRatio.Genderless));
            dexData.Add(new Species(151, -1, -1, -1, "ミュウ", "", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Psychic, PokeType.Non), new string[] { "シンクロ", "シンクロ", "シンクロ" }, GenderRatio.Genderless));
            dexData.Add(new Species(152, -1, -1, -1, "チコリータ", "", new uint[] { 45, 49, 65, 49, 65, 45 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "リーフガード" }, GenderRatio.M7F1));
            dexData.Add(new Species(153, -1, -1, -1, "ベイリーフ", "", new uint[] { 60, 62, 80, 63, 80, 60 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "リーフガード" }, GenderRatio.M7F1));
            dexData.Add(new Species(154, -1, -1, -1, "メガニウム", "", new uint[] { 80, 82, 100, 83, 100, 80 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "リーフガード" }, GenderRatio.M7F1));
            dexData.Add(new Species(155, -1, -1, -1, "ヒノアラシ", "", new uint[] { 39, 52, 43, 60, 50, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "もらいび" }, GenderRatio.M7F1));
            dexData.Add(new Species(156, -1, -1, -1, "マグマラシ", "", new uint[] { 58, 64, 58, 80, 65, 80 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "もらいび" }, GenderRatio.M7F1));
            dexData.Add(new Species(157, -1, -1, -1, "バクフーン", "", new uint[] { 78, 84, 78, 109, 85, 100 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "もらいび" }, GenderRatio.M7F1));
            dexData.Add(new Species(158, -1, -1, -1, "ワニノコ", "", new uint[] { 50, 65, 64, 44, 48, 43 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "ちからずく" }, GenderRatio.M7F1));
            dexData.Add(new Species(159, -1, -1, -1, "アリゲイツ", "", new uint[] { 65, 80, 80, 59, 63, 58 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "ちからずく" }, GenderRatio.M7F1));
            dexData.Add(new Species(160, -1, -1, -1, "オーダイル", "", new uint[] { 85, 105, 100, 79, 83, 78 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "ちからずく" }, GenderRatio.M7F1));
            dexData.Add(new Species(161, -1, -1, -1, "オタチ", "", new uint[] { 35, 46, 34, 35, 45, 20 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "するどいめ", "おみとおし" }, GenderRatio.M1F1));
            dexData.Add(new Species(162, -1, -1, -1, "オオタチ", "", new uint[] { 85, 76, 64, 45, 55, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "するどいめ", "おみとおし" }, GenderRatio.M1F1));
            dexData.Add(new Species(163, 19, -1, -1, "ホーホー", "", new uint[] { 60, 30, 30, 36, 56, 50 }, (PokeType.Normal, PokeType.Flying), new string[] { "ふみん", "するどいめ", "いろめがね" }, GenderRatio.M1F1));
            dexData.Add(new Species(164, 20, -1, -1, "ヨルノズク", "", new uint[] { 100, 50, 50, 86, 96, 70 }, (PokeType.Normal, PokeType.Flying), new string[] { "ふみん", "するどいめ", "いろめがね" }, GenderRatio.M1F1));
            dexData.Add(new Species(165, -1, -1, -1, "レディバ", "", new uint[] { 40, 20, 30, 40, 80, 55 }, (PokeType.Bug, PokeType.Flying), new string[] { "むしのしらせ", "はやおき", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(166, -1, -1, -1, "レディアン", "", new uint[] { 55, 35, 50, 55, 110, 85 }, (PokeType.Bug, PokeType.Flying), new string[] { "むしのしらせ", "はやおき", "てつのこぶし" }, GenderRatio.M1F1));
            dexData.Add(new Species(167, -1, -1, -1, "イトマル", "", new uint[] { 40, 60, 40, 40, 40, 30 }, (PokeType.Bug, PokeType.Poison), new string[] { "むしのしらせ", "ふみん", "スナイパー" }, GenderRatio.M1F1));
            dexData.Add(new Species(168, -1, -1, -1, "アリアドス", "", new uint[] { 70, 90, 70, 60, 70, 40 }, (PokeType.Bug, PokeType.Poison), new string[] { "むしのしらせ", "ふみん", "スナイパー" }, GenderRatio.M1F1));
            dexData.Add(new Species(169, -1, -1, 146, "クロバット", "", new uint[] { 85, 90, 80, 70, 80, 130 }, (PokeType.Poison, PokeType.Flying), new string[] { "せいしんりょく", "せいしんりょく", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(170, 220, 188, -1, "チョンチー", "", new uint[] { 75, 38, 38, 56, 56, 67 }, (PokeType.Water, PokeType.Electric), new string[] { "ちくでん", "はっこう", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(171, 221, 189, -1, "ランターン", "", new uint[] { 125, 58, 58, 76, 76, 67 }, (PokeType.Water, PokeType.Electric), new string[] { "ちくでん", "はっこう", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(172, 193, 84, -1, "ピチュー", "", new uint[] { 20, 40, 15, 35, 35, 60 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "ひらいしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(173, 254, -1, 43, "ピィ", "", new uint[] { 50, 25, 28, 45, 55, 15 }, (PokeType.Fairy, PokeType.Non), new string[] { "メロメロボディ", "マジックガード", "フレンドガード" }, GenderRatio.M1F3));
            dexData.Add(new Species(174, -1, 11, -1, "ププリン", "", new uint[] { 90, 30, 15, 40, 20, 15 }, (PokeType.Normal, PokeType.Fairy), new string[] { "メロメロボディ", "かちき", "フレンドガード" }, GenderRatio.M1F3));
            dexData.Add(new Species(175, 257, -1, -1, "トゲピー", "", new uint[] { 35, 20, 65, 40, 65, 20 }, (PokeType.Fairy, PokeType.Non), new string[] { "はりきり", "てんのめぐみ", "きょううん" }, GenderRatio.M7F1));
            dexData.Add(new Species(176, 258, -1, -1, "トゲチック", "", new uint[] { 55, 40, 85, 80, 105, 40 }, (PokeType.Fairy, PokeType.Flying), new string[] { "はりきり", "てんのめぐみ", "きょううん" }, GenderRatio.M7F1));
            dexData.Add(new Species(177, 92, -1, -1, "ネイティ", "", new uint[] { 40, 50, 45, 70, 45, 70 }, (PokeType.Psychic, PokeType.Flying), new string[] { "シンクロ", "はやおき", "マジックミラー" }, GenderRatio.M1F1));
            dexData.Add(new Species(178, 93, -1, -1, "ネイティオ", "", new uint[] { 65, 75, 70, 95, 70, 95 }, (PokeType.Psychic, PokeType.Flying), new string[] { "シンクロ", "はやおき", "マジックミラー" }, GenderRatio.M1F1));
            dexData.Add(new Species(179, -1, -1, -1, "メリープ", "", new uint[] { 55, 40, 40, 65, 45, 35 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "プラス" }, GenderRatio.M1F1));
            dexData.Add(new Species(180, -1, -1, -1, "モココ", "", new uint[] { 70, 55, 55, 80, 60, 45 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "プラス" }, GenderRatio.M1F1));
            dexData.Add(new Species(181, -1, -1, -1, "デンリュウ", "", new uint[] { 90, 75, 85, 115, 90, 55 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "プラス" }, GenderRatio.M1F1));
            dexData.Add(new Species(182, 58, -1, -1, "キレイハナ", "", new uint[] { 75, 80, 95, 90, 100, 50 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "ようりょくそ", "いやしのこころ" }, GenderRatio.M1F1));
            dexData.Add(new Species(183, -1, 140, -1, "マリル", "", new uint[] { 70, 20, 50, 20, 50, 40 }, (PokeType.Water, PokeType.Fairy), new string[] { "あついしぼう", "ちからもち", "そうしょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(184, -1, 141, -1, "マリルリ", "", new uint[] { 100, 50, 80, 60, 80, 50 }, (PokeType.Water, PokeType.Fairy), new string[] { "あついしぼう", "ちからもち", "そうしょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(185, 253, -1, -1, "ウソッキー", "", new uint[] { 70, 100, 115, 30, 65, 30 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "いしあたま", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(186, -1, 145, -1, "ニョロトノ", "", new uint[] { 90, 75, 75, 90, 100, 70 }, (PokeType.Water, PokeType.Non), new string[] { "ちょすい", "しめりけ", "あめふらし" }, GenderRatio.M1F1));
            dexData.Add(new Species(187, -1, -1, -1, "ハネッコ", "", new uint[] { 35, 35, 40, 35, 55, 50 }, (PokeType.Grass, PokeType.Flying), new string[] { "ようりょくそ", "リーフガード", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(188, -1, -1, -1, "ポポッコ", "", new uint[] { 55, 45, 50, 45, 65, 80 }, (PokeType.Grass, PokeType.Flying), new string[] { "ようりょくそ", "リーフガード", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(189, -1, -1, -1, "ワタッコ", "", new uint[] { 75, 55, 70, 55, 95, 110 }, (PokeType.Grass, PokeType.Flying), new string[] { "ようりょくそ", "リーフガード", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(190, -1, -1, -1, "エイパム", "", new uint[] { 55, 70, 55, 40, 55, 85 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "ものひろい", "スキルリンク" }, GenderRatio.M1F1));
            dexData.Add(new Species(191, -1, -1, -1, "ヒマナッツ", "", new uint[] { 30, 30, 30, 30, 30, 30 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "サンパワー", "はやおき" }, GenderRatio.M1F1));
            dexData.Add(new Species(192, -1, -1, -1, "キマワリ", "", new uint[] { 75, 75, 55, 105, 85, 30 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "サンパワー", "はやおき" }, GenderRatio.M1F1));
            dexData.Add(new Species(193, -1, -1, -1, "ヤンヤンマ", "", new uint[] { 65, 65, 45, 75, 45, 95 }, (PokeType.Bug, PokeType.Flying), new string[] { "かそく", "ふくがん", "おみとおし" }, GenderRatio.M1F1));
            dexData.Add(new Species(194, 100, 58, -1, "ウパー", "", new uint[] { 55, 45, 45, 25, 25, 15 }, (PokeType.Water, PokeType.Ground), new string[] { "しめりけ", "ちょすい", "てんねん" }, GenderRatio.M1F1));
            dexData.Add(new Species(195, 101, 59, -1, "ヌオー", "", new uint[] { 95, 85, 85, 65, 65, 35 }, (PokeType.Water, PokeType.Ground), new string[] { "しめりけ", "ちょすい", "てんねん" }, GenderRatio.M1F1));
            dexData.Add(new Species(196, 200, -1, 79, "エーフィ", "", new uint[] { 65, 65, 60, 130, 95, 110 }, (PokeType.Psychic, PokeType.Non), new string[] { "シンクロ", "シンクロ", "マジックミラー" }, GenderRatio.M7F1));
            dexData.Add(new Species(197, 201, -1, 78, "ブラッキー", "", new uint[] { 95, 65, 110, 60, 130, 65 }, (PokeType.Dark, PokeType.Non), new string[] { "シンクロ", "シンクロ", "せいしんりょく" }, GenderRatio.M7F1));
            dexData.Add(new Species(198, -1, -1, -1, "ヤミカラス", "", new uint[] { 60, 85, 42, 85, 42, 91 }, (PokeType.Dark, PokeType.Flying), new string[] { "ふみん", "きょううん", "いたずらごころ" }, GenderRatio.M1F1));
            dexData.Add(new Species(199, -1, 3, -1, "ヤドキング", "", new uint[] { 95, 75, 80, 100, 110, 30 }, (PokeType.Water, PokeType.Psychic), new string[] { "どんかん", "マイペース", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(199, -1, 3, -1, "ヤドキング", "ガラル", new uint[] { 95, 65, 80, 110, 110, 30 }, (PokeType.Poison, PokeType.Psychic), new string[] { "きみょうなくすり", "マイペース", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(200, -1, -1, -1, "ムウマ", "", new uint[] { 60, 60, 60, 85, 85, 85 }, (PokeType.Ghost, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(201, -1, -1, -1, "アンノーン", "", new uint[] { 48, 72, 48, 72, 48, 48 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(202, 217, -1, -1, "ソーナンス", "", new uint[] { 190, 33, 58, 33, 58, 33 }, (PokeType.Psychic, PokeType.Non), new string[] { "かげふみ", "かげふみ", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(203, -1, -1, -1, "キリンリキ", "", new uint[] { 70, 80, 65, 90, 65, 85 }, (PokeType.Normal, PokeType.Psychic), new string[] { "せいしんりょく", "はやおき", "そうしょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(204, -1, -1, -1, "クヌギダマ", "", new uint[] { 50, 65, 90, 35, 35, 15 }, (PokeType.Bug, PokeType.Non), new string[] { "がんじょう", "がんじょう", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(205, -1, -1, -1, "フォレトス", "", new uint[] { 75, 90, 140, 60, 60, 40 }, (PokeType.Bug, PokeType.Steel), new string[] { "がんじょう", "がんじょう", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(206, -1, 52, -1, "ノコッチ", "", new uint[] { 100, 70, 70, 65, 65, 45 }, (PokeType.Normal, PokeType.Non), new string[] { "てんのめぐみ", "にげあし", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(207, -1, -1, -1, "グライガー", "", new uint[] { 65, 75, 105, 35, 65, 85 }, (PokeType.Ground, PokeType.Flying), new string[] { "かいりきバサミ", "すながくれ", "めんえき" }, GenderRatio.M1F1));
            dexData.Add(new Species(208, 179, -1, -1, "ハガネール", "", new uint[] { 75, 85, 200, 55, 65, 30 }, (PokeType.Steel, PokeType.Ground), new string[] { "いしあたま", "がんじょう", "ちからずく" }, GenderRatio.M1F1));
            dexData.Add(new Species(209, -1, -1, -1, "ブルー", "", new uint[] { 60, 80, 50, 40, 40, 30 }, (PokeType.Fairy, PokeType.Non), new string[] { "いかく", "にげあし", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(210, -1, -1, -1, "グランブル", "", new uint[] { 90, 120, 75, 60, 60, 45 }, (PokeType.Fairy, PokeType.Non), new string[] { "いかく", "にげあし", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(211, 304, -1, -1, "ハリーセン", "", new uint[] { 65, 95, 85, 55, 55, 85 }, (PokeType.Water, PokeType.Poison), new string[] { "どくのトゲ", "すいすい", "いかく" }, GenderRatio.M1F1));
            dexData.Add(new Species(212, -1, 119, -1, "ハッサム", "", new uint[] { 70, 130, 100, 55, 80, 65 }, (PokeType.Bug, PokeType.Steel), new string[] { "むしのしらせ", "テクニシャン", "ライトメタル" }, GenderRatio.M1F1));
            dexData.Add(new Species(213, 227, -1, 170, "ツボツボ", "", new uint[] { 20, 10, 230, 10, 230, 5 }, (PokeType.Bug, PokeType.Rock), new string[] { "がんじょう", "くいしんぼう", "あまのじゃく" }, GenderRatio.M1F1));
            dexData.Add(new Species(214, -1, 121, -1, "ヘラクロス", "", new uint[] { 80, 125, 75, 40, 95, 85 }, (PokeType.Bug, PokeType.Fighting), new string[] { "むしのしらせ", "こんじょう", "じしんかじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(215, 292, -1, 28, "ニューラ", "", new uint[] { 55, 95, 55, 35, 75, 115 }, (PokeType.Dark, PokeType.Ice), new string[] { "せいしんりょく", "するどいめ", "わるいてぐせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(216, -1, -1, -1, "ヒメグマ", "", new uint[] { 60, 80, 50, 50, 50, 40 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "はやあし", "みつあつめ" }, GenderRatio.M1F1));
            dexData.Add(new Species(217, -1, -1, -1, "リングマ", "", new uint[] { 90, 130, 75, 75, 75, 55 }, (PokeType.Normal, PokeType.Non), new string[] { "こんじょう", "はやあし", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(218, -1, -1, -1, "マグマッグ", "", new uint[] { 40, 40, 40, 70, 40, 20 }, (PokeType.Fire, PokeType.Non), new string[] { "マグマのよろい", "ほのおのからだ", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(219, -1, -1, -1, "マグカルゴ", "", new uint[] { 60, 50, 120, 90, 80, 30 }, (PokeType.Fire, PokeType.Rock), new string[] { "マグマのよろい", "ほのおのからだ", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(220, 75, -1, 7, "ウリムー", "", new uint[] { 50, 50, 40, 30, 30, 50 }, (PokeType.Ice, PokeType.Ground), new string[] { "どんかん", "ゆきがくれ", "あついしぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(221, 76, -1, 8, "イノムー", "", new uint[] { 100, 100, 80, 60, 60, 50 }, (PokeType.Ice, PokeType.Ground), new string[] { "どんかん", "ゆきがくれ", "あついしぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(222, 236, -1, -1, "サニーゴ", "", new uint[] { 65, 55, 95, 65, 95, 35 }, (PokeType.Water, PokeType.Rock), new string[] { "はりきり", "しぜんかいふく", "さいせいりょく" }, GenderRatio.M1F3));
            dexData.Add(new AnotherForm(222, 236, -1, -1, "サニーゴ", "ガラル", new uint[] { 60, 55, 100, 65, 100, 30 }, (PokeType.Ghost, PokeType.Non), new string[] { "くだけるよろい", "くだけるよろい", "のろわれボディ" }, GenderRatio.M1F3));
            dexData.Add(new Species(223, 148, 44, -1, "テッポウオ", "", new uint[] { 35, 65, 35, 65, 35, 65 }, (PokeType.Water, PokeType.Non), new string[] { "はりきり", "スナイパー", "ムラっけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(224, 149, 45, -1, "オクタン", "", new uint[] { 75, 105, 75, 105, 75, 45 }, (PokeType.Water, PokeType.Non), new string[] { "きゅうばん", "スナイパー", "ムラっけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(225, 78, -1, 120, "デリバード", "", new uint[] { 45, 55, 45, 65, 45, 75 }, (PokeType.Ice, PokeType.Flying), new string[] { "やるき", "はりきり", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new Species(226, 355, 47, -1, "マンタイン", "", new uint[] { 85, 40, 70, 80, 140, 70 }, (PokeType.Water, PokeType.Flying), new string[] { "すいすい", "ちょすい", "みずのベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(227, -1, 153, -1, "エアームド", "", new uint[] { 65, 80, 140, 40, 70, 70 }, (PokeType.Steel, PokeType.Flying), new string[] { "するどいめ", "がんじょう", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(228, -1, -1, -1, "デルビル", "", new uint[] { 45, 60, 30, 80, 50, 65 }, (PokeType.Dark, PokeType.Fire), new string[] { "はやおき", "もらいび", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(229, -1, -1, -1, "ヘルガー", "", new uint[] { 75, 90, 50, 110, 80, 95 }, (PokeType.Dark, PokeType.Fire), new string[] { "はやおき", "もらいび", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(230, -1, 200, -1, "キングドラ", "", new uint[] { 75, 95, 95, 95, 95, 85 }, (PokeType.Water, PokeType.Dragon), new string[] { "すいすい", "スナイパー", "しめりけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(231, -1, -1, -1, "ゴマゾウ", "", new uint[] { 90, 60, 60, 40, 40, 40 }, (PokeType.Ground, PokeType.Non), new string[] { "ものひろい", "ものひろい", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(232, -1, -1, -1, "ドンファン", "", new uint[] { 90, 120, 120, 60, 60, 50 }, (PokeType.Ground, PokeType.Non), new string[] { "ものひろい", "ものひろい", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(233, -1, 209, -1, "ポリゴン2", "", new uint[] { 85, 80, 90, 105, 95, 60 }, (PokeType.Normal, PokeType.Non), new string[] { "トレース", "ダウンロード", "アナライズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(234, -1, -1, -1, "オドシシ", "", new uint[] { 73, 95, 62, 85, 65, 85 }, (PokeType.Normal, PokeType.Non), new string[] { "いかく", "おみとおし", "そうしょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(235, -1, -1, -1, "ドーブル", "", new uint[] { 55, 20, 35, 20, 45, 75 }, (PokeType.Normal, PokeType.Non), new string[] { "マイペース", "テクニシャン", "ムラっけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(236, 107, -1, -1, "バルキー", "", new uint[] { 35, 35, 35, 35, 35, 35 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ふくつのこころ", "やるき" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(237, 110, -1, -1, "カポエラー", "", new uint[] { 50, 95, 95, 35, 110, 70 }, (PokeType.Fighting, PokeType.Non), new string[] { "いかく", "テクニシャン", "ふくつのこころ" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(238, -1, -1, 13, "ムチュール", "", new uint[] { 45, 30, 15, 85, 65, 65 }, (PokeType.Ice, PokeType.Psychic), new string[] { "どんかん", "よちむ", "うるおいボディ" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(239, -1, -1, 15, "エレキッド", "", new uint[] { 45, 63, 37, 65, 55, 95 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "せいでんき", "やるき" }, GenderRatio.M3F1));
            dexData.Add(new Species(240, -1, -1, 18, "ブビィ", "", new uint[] { 45, 75, 37, 70, 55, 83 }, (PokeType.Fire, PokeType.Non), new string[] { "ほのおのからだ", "ほのおのからだ", "やるき" }, GenderRatio.M3F1));
            dexData.Add(new Species(241, -1, 117, -1, "ミルタンク", "", new uint[] { 95, 80, 105, 40, 70, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "あついしぼう", "きもったま", "そうしょく" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(242, -1, 8, -1, "ハピナス", "", new uint[] { 255, 10, 10, 75, 135, 55 }, (PokeType.Normal, PokeType.Non), new string[] { "しぜんかいふく", "てんのめぐみ", "いやしのこころ" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(243, -1, -1, -1, "ライコウ", "", new uint[] { 90, 85, 75, 115, 100, 115 }, (PokeType.Electric, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "せいしんりょく" }, GenderRatio.Genderless));
            dexData.Add(new Species(244, -1, -1, -1, "エンテイ", "", new uint[] { 115, 115, 85, 90, 75, 100 }, (PokeType.Fire, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "せいしんりょく" }, GenderRatio.Genderless));
            dexData.Add(new Species(245, -1, -1, -1, "スイクン", "", new uint[] { 100, 75, 115, 90, 115, 85 }, (PokeType.Water, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "せいしんりょく" }, GenderRatio.Genderless));
            dexData.Add(new Species(246, 383, -1, 139, "ヨーギラス", "", new uint[] { 50, 64, 50, 45, 50, 41 }, (PokeType.Rock, PokeType.Ground), new string[] { "こんじょう", "こんじょう", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(247, 384, -1, 140, "サナギラス", "", new uint[] { 70, 84, 70, 65, 70, 51 }, (PokeType.Rock, PokeType.Ground), new string[] { "だっぴ", "だっぴ", "だっぴ" }, GenderRatio.M1F1));
            dexData.Add(new Species(248, 385, -1, 141, "バンギラス", "", new uint[] { 100, 134, 110, 95, 100, 61 }, (PokeType.Rock, PokeType.Dark), new string[] { "すなおこし", "すなおこし", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(249, -1, -1, -1, "ルギア", "", new uint[] { 106, 90, 130, 90, 154, 110 }, (PokeType.Psychic, PokeType.Flying), new string[] { "プレッシャー", "プレッシャー", "マルチスケイル" }, GenderRatio.Genderless));
            dexData.Add(new Species(250, -1, -1, -1, "ホウオウ", "", new uint[] { 106, 130, 90, 110, 154, 90 }, (PokeType.Fire, PokeType.Flying), new string[] { "プレッシャー", "プレッシャー", "さいせいりょく" }, GenderRatio.Genderless));
            dexData.Add(new Species(251, -1, -1, -1, "セレビィ", "", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Psychic, PokeType.Grass), new string[] { "しぜんかいふく", "しぜんかいふく", "しぜんかいふく" }, GenderRatio.Genderless));
            dexData.Add(new Species(252, -1, -1, -1, "キモリ", "", new uint[] { 40, 45, 35, 65, 55, 70 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "かるわざ" }, GenderRatio.M7F1));
            dexData.Add(new Species(253, -1, -1, -1, "ジュプトル", "", new uint[] { 50, 65, 45, 85, 65, 95 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "かるわざ" }, GenderRatio.M7F1));
            dexData.Add(new Species(254, -1, -1, -1, "ジュカイン", "", new uint[] { 70, 85, 65, 105, 85, 120 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "かるわざ" }, GenderRatio.M7F1));
            dexData.Add(new Species(255, -1, -1, -1, "アチャモ", "", new uint[] { 45, 60, 40, 70, 50, 45 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "かそく" }, GenderRatio.M7F1));
            dexData.Add(new Species(256, -1, -1, -1, "ワカシャモ", "", new uint[] { 60, 85, 60, 85, 60, 55 }, (PokeType.Fire, PokeType.Fighting), new string[] { "もうか", "もうか", "かそく" }, GenderRatio.M7F1));
            dexData.Add(new Species(257, -1, -1, -1, "バシャーモ", "", new uint[] { 80, 120, 70, 110, 70, 80 }, (PokeType.Fire, PokeType.Fighting), new string[] { "もうか", "もうか", "かそく" }, GenderRatio.M7F1));
            dexData.Add(new Species(258, -1, -1, -1, "ミズゴロウ", "", new uint[] { 50, 70, 50, 50, 50, 40 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "しめりけ" }, GenderRatio.M7F1));
            dexData.Add(new Species(259, -1, -1, -1, "ヌマクロー", "", new uint[] { 70, 85, 70, 60, 70, 50 }, (PokeType.Water, PokeType.Ground), new string[] { "げきりゅう", "げきりゅう", "しめりけ" }, GenderRatio.M7F1));
            dexData.Add(new Species(260, -1, -1, -1, "ラグラージ", "", new uint[] { 100, 110, 90, 85, 90, 60 }, (PokeType.Water, PokeType.Ground), new string[] { "げきりゅう", "げきりゅう", "しめりけ" }, GenderRatio.M7F1));
            dexData.Add(new Species(261, -1, -1, -1, "ポチエナ", "", new uint[] { 35, 55, 35, 30, 30, 35 }, (PokeType.Dark, PokeType.Non), new string[] { "にげあし", "はやあし", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(262, -1, -1, -1, "グラエナ", "", new uint[] { 70, 90, 70, 60, 60, 70 }, (PokeType.Dark, PokeType.Non), new string[] { "いかく", "はやあし", "じしんかじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(263, 31, -1, 71, "ジグザグマ", "", new uint[] { 38, 30, 41, 30, 41, 60 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "くいしんぼう", "はやあし" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(263, 31, -1, 71, "ジグザグマ", "ガラル", new uint[] { 38, 30, 41, 30, 41, 60 }, (PokeType.Dark, PokeType.Normal), new string[] { "ものひろい", "くいしんぼう", "はやあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(264, 32, -1, 72, "マッスグマ", "", new uint[] { 78, 70, 61, 50, 61, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "くいしんぼう", "はやあし" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(264, 32, -1, 72, "マッスグマ", "ガラル", new uint[] { 78, 70, 61, 50, 61, 100 }, (PokeType.Dark, PokeType.Normal), new string[] { "ものひろい", "くいしんぼう", "はやあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(265, -1, -1, -1, "ケムッソ", "", new uint[] { 45, 45, 35, 20, 30, 20 }, (PokeType.Bug, PokeType.Non), new string[] { "りんぷん", "りんぷん", "にげあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(266, -1, -1, -1, "カラサリス", "", new uint[] { 50, 35, 55, 25, 25, 15 }, (PokeType.Bug, PokeType.Non), new string[] { "だっぴ", "だっぴ", "だっぴ" }, GenderRatio.M1F1));
            dexData.Add(new Species(267, -1, -1, -1, "アゲハント", "", new uint[] { 60, 70, 50, 100, 50, 65 }, (PokeType.Bug, PokeType.Flying), new string[] { "むしのしらせ", "むしのしらせ", "とうそうしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(268, -1, -1, -1, "マユルド", "", new uint[] { 50, 35, 55, 25, 25, 15 }, (PokeType.Bug, PokeType.Non), new string[] { "だっぴ", "だっぴ", "だっぴ" }, GenderRatio.M1F1));
            dexData.Add(new Species(269, -1, -1, -1, "ドクケイル", "", new uint[] { 60, 50, 70, 50, 90, 65 }, (PokeType.Bug, PokeType.Poison), new string[] { "りんぷん", "りんぷん", "ふくがん" }, GenderRatio.M1F1));
            dexData.Add(new Species(270, 36, -1, -1, "ハスボー", "", new uint[] { 40, 30, 30, 40, 50, 30 }, (PokeType.Water, PokeType.Grass), new string[] { "すいすい", "あめうけざら", "マイペース" }, GenderRatio.M1F1));
            dexData.Add(new Species(271, 37, -1, -1, "ハスブレロ", "", new uint[] { 60, 50, 50, 60, 70, 50 }, (PokeType.Water, PokeType.Grass), new string[] { "すいすい", "あめうけざら", "マイペース" }, GenderRatio.M1F1));
            dexData.Add(new Species(272, 38, -1, -1, "ルンパッパ", "", new uint[] { 80, 70, 70, 90, 100, 70 }, (PokeType.Water, PokeType.Grass), new string[] { "すいすい", "あめうけざら", "マイペース" }, GenderRatio.M1F1));
            dexData.Add(new Species(273, 39, -1, -1, "タネボー", "", new uint[] { 40, 40, 50, 30, 30, 30 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "はやおき", "わるいてぐせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(274, 40, -1, -1, "コノハナ", "", new uint[] { 70, 70, 40, 60, 40, 60 }, (PokeType.Grass, PokeType.Dark), new string[] { "ようりょくそ", "はやおき", "わるいてぐせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(275, 41, -1, -1, "ダーテング", "", new uint[] { 90, 100, 60, 90, 60, 80 }, (PokeType.Grass, PokeType.Dark), new string[] { "ようりょくそ", "はやおき", "わるいてぐせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(276, -1, -1, -1, "スバメ", "", new uint[] { 40, 55, 30, 30, 30, 85 }, (PokeType.Normal, PokeType.Flying), new string[] { "こんじょう", "こんじょう", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(277, -1, -1, -1, "オオスバメ", "", new uint[] { 60, 85, 60, 75, 50, 125 }, (PokeType.Normal, PokeType.Flying), new string[] { "こんじょう", "こんじょう", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(278, 62, 48, -1, "キャモメ", "", new uint[] { 40, 30, 30, 55, 30, 85 }, (PokeType.Water, PokeType.Flying), new string[] { "するどいめ", "うるおいボディ", "あめうけざら" }, GenderRatio.M1F1));
            dexData.Add(new Species(279, 63, 49, -1, "ペリッパー", "", new uint[] { 60, 50, 100, 95, 70, 65 }, (PokeType.Water, PokeType.Flying), new string[] { "するどいめ", "あめふらし", "あめうけざら" }, GenderRatio.M1F1));
            dexData.Add(new Species(280, 120, 34, -1, "ラルトス", "", new uint[] { 28, 25, 25, 45, 35, 40 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "シンクロ", "トレース", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(281, 121, 35, -1, "キルリア", "", new uint[] { 38, 35, 35, 65, 55, 50 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "シンクロ", "トレース", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(282, 122, 36, -1, "サーナイト", "", new uint[] { 68, 65, 65, 125, 115, 80 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "シンクロ", "トレース", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(283, -1, -1, -1, "アメタマ", "", new uint[] { 40, 30, 32, 50, 52, 65 }, (PokeType.Bug, PokeType.Water), new string[] { "すいすい", "すいすい", "あめうけざら" }, GenderRatio.M1F1));
            dexData.Add(new Species(284, -1, -1, -1, "アメモース", "", new uint[] { 70, 60, 62, 100, 82, 80 }, (PokeType.Bug, PokeType.Flying), new string[] { "いかく", "いかく", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(285, -1, -1, -1, "キノココ", "", new uint[] { 60, 40, 60, 40, 60, 35 }, (PokeType.Grass, PokeType.Non), new string[] { "ほうし", "ポイズンヒール", "はやあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(286, -1, -1, -1, "キノガッサ", "", new uint[] { 60, 130, 80, 60, 60, 70 }, (PokeType.Grass, PokeType.Fighting), new string[] { "ほうし", "ポイズンヒール", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new Species(287, -1, -1, -1, "ナマケロ", "", new uint[] { 60, 60, 60, 35, 35, 30 }, (PokeType.Normal, PokeType.Non), new string[] { "なまけ", "なまけ", "なまけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(288, -1, -1, -1, "ヤルキモノ", "", new uint[] { 80, 80, 80, 55, 55, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "やるき", "やるき", "やるき" }, GenderRatio.M1F1));
            dexData.Add(new Species(289, -1, -1, -1, "ケッキング", "", new uint[] { 150, 160, 100, 95, 65, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "なまけ", "なまけ", "なまけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(290, 104, -1, -1, "ツチニン", "", new uint[] { 31, 45, 90, 30, 30, 40 }, (PokeType.Bug, PokeType.Ground), new string[] { "ふくがん", "ふくがん", "にげあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(291, 105, -1, -1, "テッカニン", "", new uint[] { 61, 90, 45, 50, 50, 160 }, (PokeType.Bug, PokeType.Flying), new string[] { "かそく", "かそく", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(292, 106, -1, -1, "ヌケニン", "", new uint[] { 1, 90, 45, 30, 30, 40 }, (PokeType.Bug, PokeType.Ghost), new string[] { "ふしぎなまもり", "ふしぎなまもり", "ふしぎなまもり" }, GenderRatio.Genderless));
            dexData.Add(new Species(293, -1, 148, -1, "ゴニョニョ", "", new uint[] { 64, 51, 23, 51, 23, 28 }, (PokeType.Normal, PokeType.Non), new string[] { "ぼうおん", "ぼうおん", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(294, -1, 149, -1, "ドゴーム", "", new uint[] { 84, 71, 43, 71, 43, 48 }, (PokeType.Normal, PokeType.Non), new string[] { "ぼうおん", "ぼうおん", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(295, -1, 150, -1, "バクオング", "", new uint[] { 104, 91, 63, 91, 73, 68 }, (PokeType.Normal, PokeType.Non), new string[] { "ぼうおん", "ぼうおん", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(296, -1, -1, -1, "マクノシタ", "", new uint[] { 72, 60, 30, 20, 30, 25 }, (PokeType.Fighting, PokeType.Non), new string[] { "あついしぼう", "こんじょう", "ちからずく" }, GenderRatio.M3F1));
            dexData.Add(new Species(297, -1, -1, -1, "ハリテヤマ", "", new uint[] { 144, 120, 60, 40, 60, 50 }, (PokeType.Fighting, PokeType.Non), new string[] { "あついしぼう", "こんじょう", "ちからずく" }, GenderRatio.M3F1));
            dexData.Add(new Species(298, -1, 139, -1, "ルリリ", "", new uint[] { 50, 20, 40, 20, 40, 20 }, (PokeType.Normal, PokeType.Fairy), new string[] { "あついしぼう", "ちからもち", "そうしょく" }, GenderRatio.M1F3));
            dexData.Add(new Species(299, -1, -1, -1, "ノズパス", "", new uint[] { 30, 45, 135, 45, 90, 30 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "じりょく", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(300, -1, -1, -1, "エネコ", "", new uint[] { 50, 45, 45, 35, 35, 50 }, (PokeType.Normal, PokeType.Non), new string[] { "メロメロボディ", "ノーマルスキン", "ミラクルスキン" }, GenderRatio.M1F3));
            dexData.Add(new Species(301, -1, -1, -1, "エネコロロ", "", new uint[] { 70, 65, 65, 55, 55, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "メロメロボディ", "ノーマルスキン", "ミラクルスキン" }, GenderRatio.M1F3));
            dexData.Add(new Species(302, 294, -1, 174, "ヤミラミ", "", new uint[] { 50, 75, 75, 65, 65, 50 }, (PokeType.Dark, PokeType.Ghost), new string[] { "するどいめ", "あとだし", "いたずらごころ" }, GenderRatio.M1F1));
            dexData.Add(new Species(303, 295, -1, 175, "クチート", "", new uint[] { 50, 85, 85, 55, 55, 50 }, (PokeType.Steel, PokeType.Fairy), new string[] { "かいりきバサミ", "いかく", "ちからずく" }, GenderRatio.M1F1));
            dexData.Add(new Species(304, -1, -1, 191, "ココドラ", "", new uint[] { 50, 70, 100, 40, 40, 30 }, (PokeType.Steel, PokeType.Rock), new string[] { "がんじょう", "いしあたま", "ヘヴィメタル" }, GenderRatio.M1F1));
            dexData.Add(new Species(305, -1, -1, 192, "コドラ", "", new uint[] { 60, 90, 140, 50, 50, 40 }, (PokeType.Steel, PokeType.Rock), new string[] { "がんじょう", "いしあたま", "ヘヴィメタル" }, GenderRatio.M1F1));
            dexData.Add(new Species(306, -1, -1, 193, "ボスゴドラ", "", new uint[] { 70, 110, 180, 60, 60, 50 }, (PokeType.Steel, PokeType.Rock), new string[] { "がんじょう", "いしあたま", "ヘヴィメタル" }, GenderRatio.M1F1));
            dexData.Add(new Species(307, -1, -1, -1, "アサナン", "", new uint[] { 30, 40, 55, 40, 55, 60 }, (PokeType.Fighting, PokeType.Psychic), new string[] { "ヨガパワー", "ヨガパワー", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(308, -1, -1, -1, "チャーレム", "", new uint[] { 60, 60, 75, 60, 75, 80 }, (PokeType.Fighting, PokeType.Psychic), new string[] { "ヨガパワー", "ヨガパワー", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(309, 66, -1, -1, "ラクライ", "", new uint[] { 40, 45, 40, 65, 40, 65 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "ひらいしん", "マイナス" }, GenderRatio.M1F1));
            dexData.Add(new Species(310, 67, -1, -1, "ライボルト", "", new uint[] { 70, 75, 60, 105, 60, 105 }, (PokeType.Electric, PokeType.Non), new string[] { "せいでんき", "ひらいしん", "マイナス" }, GenderRatio.M1F1));
            dexData.Add(new Species(311, -1, -1, -1, "プラスル", "", new uint[] { 60, 50, 40, 85, 75, 95 }, (PokeType.Electric, PokeType.Non), new string[] { "プラス", "プラス", "ひらいしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(312, -1, -1, -1, "マイナン", "", new uint[] { 60, 40, 50, 75, 85, 95 }, (PokeType.Electric, PokeType.Non), new string[] { "マイナス", "マイナス", "ちくでん" }, GenderRatio.M1F1));
            dexData.Add(new Species(313, -1, -1, -1, "バルビート", "", new uint[] { 65, 73, 75, 47, 85, 85 }, (PokeType.Bug, PokeType.Non), new string[] { "はっこう", "むしのしらせ", "いたずらごころ" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(314, -1, -1, -1, "イルミーゼ", "", new uint[] { 65, 47, 75, 73, 85, 85 }, (PokeType.Bug, PokeType.Non), new string[] { "どんかん", "いろめがね", "いたずらごころ" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(315, 60, -1, -1, "ロゼリア", "", new uint[] { 50, 60, 45, 100, 80, 65 }, (PokeType.Grass, PokeType.Poison), new string[] { "しぜんかいふく", "どくのトゲ", "リーフガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(316, -1, -1, -1, "ゴクリン", "", new uint[] { 70, 43, 53, 43, 53, 40 }, (PokeType.Poison, PokeType.Non), new string[] { "ヘドロえき", "ねんちゃく", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(317, -1, -1, -1, "マルノーム", "", new uint[] { 100, 73, 83, 73, 83, 55 }, (PokeType.Poison, PokeType.Non), new string[] { "ヘドロえき", "ねんちゃく", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(318, -1, 111, -1, "キバニア", "", new uint[] { 45, 90, 20, 65, 20, 65 }, (PokeType.Water, PokeType.Dark), new string[] { "さめはだ", "さめはだ", "かそく" }, GenderRatio.M1F1));
            dexData.Add(new Species(319, -1, 112, -1, "サメハダー", "", new uint[] { 70, 120, 40, 95, 40, 95 }, (PokeType.Water, PokeType.Dark), new string[] { "さめはだ", "さめはだ", "かそく" }, GenderRatio.M1F1));
            dexData.Add(new Species(320, 356, 190, -1, "ホエルコ", "", new uint[] { 130, 70, 35, 70, 35, 60 }, (PokeType.Water, PokeType.Non), new string[] { "みずのベール", "どんかん", "プレッシャー" }, GenderRatio.M1F1));
            dexData.Add(new Species(321, 357, 191, -1, "ホエルオー", "", new uint[] { 170, 90, 45, 90, 45, 60 }, (PokeType.Water, PokeType.Non), new string[] { "みずのベール", "どんかん", "プレッシャー" }, GenderRatio.M1F1));
            dexData.Add(new Species(322, -1, -1, -1, "ドンメル", "", new uint[] { 60, 60, 40, 65, 45, 35 }, (PokeType.Fire, PokeType.Ground), new string[] { "どんかん", "たんじゅん", "マイペース" }, GenderRatio.M1F1));
            dexData.Add(new Species(323, -1, -1, -1, "バクーダ", "", new uint[] { 70, 100, 70, 105, 75, 40 }, (PokeType.Fire, PokeType.Ground), new string[] { "マグマのよろい", "ハードロック", "いかりのつぼ" }, GenderRatio.M1F1));
            dexData.Add(new Species(324, 300, -1, -1, "コータス", "", new uint[] { 70, 85, 140, 85, 70, 20 }, (PokeType.Fire, PokeType.Non), new string[] { "しろいけむり", "ひでり", "シェルアーマー" }, GenderRatio.M1F1));
            dexData.Add(new Species(325, -1, -1, -1, "バネブー", "", new uint[] { 60, 25, 35, 70, 80, 60 }, (PokeType.Psychic, PokeType.Non), new string[] { "あついしぼう", "マイペース", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(326, -1, -1, -1, "ブーピッグ", "", new uint[] { 80, 45, 65, 90, 110, 80 }, (PokeType.Psychic, PokeType.Non), new string[] { "あついしぼう", "マイペース", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(327, -1, -1, -1, "パッチール", "", new uint[] { 60, 60, 60, 60, 60, 60 }, (PokeType.Normal, PokeType.Non), new string[] { "マイペース", "ちどりあし", "あまのじゃく" }, GenderRatio.M1F1));
            dexData.Add(new Species(328, 321, -1, -1, "ナックラー", "", new uint[] { 45, 100, 45, 45, 45, 10 }, (PokeType.Ground, PokeType.Non), new string[] { "かいりきバサミ", "ありじごく", "ちからずく" }, GenderRatio.M1F1));
            dexData.Add(new Species(329, 322, -1, -1, "ビブラーバ", "", new uint[] { 50, 70, 50, 50, 50, 70 }, (PokeType.Ground, PokeType.Dragon), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(330, 323, -1, -1, "フライゴン", "", new uint[] { 80, 100, 80, 80, 80, 100 }, (PokeType.Ground, PokeType.Dragon), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(331, -1, -1, -1, "サボネア", "", new uint[] { 50, 85, 40, 85, 40, 35 }, (PokeType.Grass, PokeType.Non), new string[] { "すながくれ", "すながくれ", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(332, -1, -1, -1, "ノクタス", "", new uint[] { 70, 115, 60, 115, 60, 55 }, (PokeType.Grass, PokeType.Dark), new string[] { "すながくれ", "すながくれ", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(333, -1, -1, 35, "チルット", "", new uint[] { 45, 40, 60, 40, 75, 50 }, (PokeType.Normal, PokeType.Flying), new string[] { "しぜんかいふく", "しぜんかいふく", "ノーてんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(334, -1, -1, 36, "チルタリス", "", new uint[] { 75, 70, 90, 70, 105, 80 }, (PokeType.Dragon, PokeType.Flying), new string[] { "しぜんかいふく", "しぜんかいふく", "ノーてんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(335, -1, -1, -1, "ザングース", "", new uint[] { 73, 115, 60, 60, 60, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "めんえき", "めんえき", "どくぼうそう" }, GenderRatio.M1F1));
            dexData.Add(new Species(336, -1, -1, -1, "ハブネーク", "", new uint[] { 73, 100, 60, 100, 60, 65 }, (PokeType.Poison, PokeType.Non), new string[] { "だっぴ", "だっぴ", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(337, 362, -1, -1, "ルナトーン", "", new uint[] { 90, 55, 65, 95, 85, 70 }, (PokeType.Rock, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(338, 363, -1, -1, "ソルロック", "", new uint[] { 90, 95, 85, 55, 65, 70 }, (PokeType.Rock, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(339, 228, 137, 60, "ドジョッチ", "", new uint[] { 50, 48, 43, 46, 41, 60 }, (PokeType.Water, PokeType.Ground), new string[] { "どんかん", "きけんよち", "うるおいボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(340, 229, 138, 61, "ナマズン", "", new uint[] { 110, 78, 73, 76, 71, 60 }, (PokeType.Water, PokeType.Ground), new string[] { "どんかん", "きけんよち", "うるおいボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(341, 102, 91, -1, "ヘイガニ", "", new uint[] { 43, 80, 65, 50, 35, 35 }, (PokeType.Water, PokeType.Non), new string[] { "かいりきバサミ", "シェルアーマー", "てきおうりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(342, 103, 92, -1, "シザリガー", "", new uint[] { 63, 120, 85, 90, 55, 55 }, (PokeType.Water, PokeType.Dark), new string[] { "かいりきバサミ", "シェルアーマー", "てきおうりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(343, 82, -1, 151, "ヤジロン", "", new uint[] { 40, 40, 55, 40, 70, 55 }, (PokeType.Ground, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(344, 83, -1, 152, "ネンドール", "", new uint[] { 60, 70, 105, 70, 120, 75 }, (PokeType.Ground, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(345, -1, -1, 183, "リリーラ", "", new uint[] { 66, 41, 77, 61, 87, 23 }, (PokeType.Rock, PokeType.Grass), new string[] { "きゅうばん", "きゅうばん", "よびみず" }, GenderRatio.M7F1));
            dexData.Add(new Species(346, -1, -1, 184, "ユレイドル", "", new uint[] { 86, 81, 97, 81, 107, 43 }, (PokeType.Rock, PokeType.Grass), new string[] { "きゅうばん", "きゅうばん", "よびみず" }, GenderRatio.M7F1));
            dexData.Add(new Species(347, -1, -1, 185, "アノプス", "", new uint[] { 45, 95, 50, 40, 50, 75 }, (PokeType.Rock, PokeType.Bug), new string[] { "カブトアーマー", "カブトアーマー", "すいすい" }, GenderRatio.M7F1));
            dexData.Add(new Species(348, -1, -1, 186, "アーマルド", "", new uint[] { 75, 125, 100, 70, 80, 45 }, (PokeType.Rock, PokeType.Bug), new string[] { "カブトアーマー", "カブトアーマー", "すいすい" }, GenderRatio.M7F1));
            dexData.Add(new Species(349, 152, -1, 188, "ヒンバス", "", new uint[] { 20, 15, 20, 10, 55, 80 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "どんかん", "てきおうりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(350, 153, -1, 189, "ミロカロス", "", new uint[] { 95, 60, 79, 100, 125, 81 }, (PokeType.Water, PokeType.Non), new string[] { "ふしぎなうろこ", "かちき", "メロメロボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(351, -1, -1, -1, "ポワルン", "", new uint[] { 70, 70, 70, 70, 70, 70 }, (PokeType.Normal, PokeType.Non), new string[] { "てんきや", "てんきや", "てんきや" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(351, -1, -1, -1, "ポワルン", "太陽", new uint[] { 70, 70, 70, 70, 70, 70 }, (PokeType.Fire, PokeType.Non), new string[] { "てんきや", "てんきや", "てんきや" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(351, -1, -1, -1, "ポワルン", "雨水", new uint[] { 70, 70, 70, 70, 70, 70 }, (PokeType.Water, PokeType.Non), new string[] { "てんきや", "てんきや", "てんきや" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(351, -1, -1, -1, "ポワルン", "雪雲", new uint[] { 70, 70, 70, 70, 70, 70 }, (PokeType.Ice, PokeType.Non), new string[] { "てんきや", "てんきや", "てんきや" }, GenderRatio.M1F1));
            dexData.Add(new Species(352, -1, -1, -1, "カクレオン", "", new uint[] { 60, 90, 70, 60, 120, 40 }, (PokeType.Normal, PokeType.Non), new string[] { "へんしょく", "へんしょく", "へんげんじざい" }, GenderRatio.M1F1));
            dexData.Add(new Species(353, -1, -1, -1, "カゲボウズ", "", new uint[] { 44, 75, 35, 63, 33, 45 }, (PokeType.Ghost, PokeType.Non), new string[] { "ふみん", "おみとおし", "のろわれボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(354, -1, -1, -1, "ジュペッタ", "", new uint[] { 64, 115, 65, 83, 63, 65 }, (PokeType.Ghost, PokeType.Non), new string[] { "ふみん", "おみとおし", "のろわれボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(355, 135, -1, -1, "ヨマワル", "", new uint[] { 20, 40, 90, 30, 90, 25 }, (PokeType.Ghost, PokeType.Non), new string[] { "ふゆう", "ふゆう", "おみとおし" }, GenderRatio.M1F1));
            dexData.Add(new Species(356, 136, -1, -1, "サマヨール", "", new uint[] { 40, 70, 130, 60, 130, 25 }, (PokeType.Ghost, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "おみとおし" }, GenderRatio.M1F1));
            dexData.Add(new Species(357, -1, -1, -1, "トロピウス", "", new uint[] { 99, 68, 83, 72, 87, 51 }, (PokeType.Grass, PokeType.Flying), new string[] { "ようりょくそ", "サンパワー", "しゅうかく" }, GenderRatio.M1F1));
            dexData.Add(new Species(358, -1, -1, -1, "チリーン", "", new uint[] { 75, 50, 80, 95, 90, 65 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(359, -1, -1, 107, "アブソル", "", new uint[] { 65, 130, 60, 75, 60, 75 }, (PokeType.Dark, PokeType.Non), new string[] { "プレッシャー", "きょううん", "せいぎのこころ" }, GenderRatio.M1F1));
            dexData.Add(new Species(360, 216, -1, -1, "ソーナノ", "", new uint[] { 95, 23, 48, 23, 48, 23 }, (PokeType.Psychic, PokeType.Non), new string[] { "かげふみ", "かげふみ", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(361, 79, -1, 25, "ユキワラシ", "", new uint[] { 50, 50, 50, 50, 50, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "せいしんりょく", "アイスボディ", "ムラっけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(362, 80, -1, 26, "オニゴーリ", "", new uint[] { 80, 80, 80, 80, 80, 80 }, (PokeType.Ice, PokeType.Non), new string[] { "せいしんりょく", "アイスボディ", "ムラっけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(363, -1, -1, 159, "タマザラシ", "", new uint[] { 70, 40, 50, 55, 50, 25 }, (PokeType.Ice, PokeType.Water), new string[] { "あついしぼう", "アイスボディ", "どんかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(364, -1, -1, 160, "トドグラー", "", new uint[] { 90, 60, 70, 75, 70, 45 }, (PokeType.Ice, PokeType.Water), new string[] { "あついしぼう", "アイスボディ", "どんかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(365, -1, -1, 161, "トドゼルガ", "", new uint[] { 110, 80, 90, 95, 90, 65 }, (PokeType.Ice, PokeType.Water), new string[] { "あついしぼう", "アイスボディ", "どんかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(366, -1, -1, -1, "パールル", "", new uint[] { 35, 64, 85, 74, 55, 32 }, (PokeType.Water, PokeType.Non), new string[] { "シェルアーマー", "シェルアーマー", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(367, -1, -1, -1, "ハンテール", "", new uint[] { 55, 104, 105, 94, 75, 52 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "みずのベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(368, -1, -1, -1, "サクラビス", "", new uint[] { 55, 84, 105, 114, 75, 52 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "うるおいボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(369, -1, -1, 187, "ジーランス", "", new uint[] { 100, 90, 130, 45, 65, 55 }, (PokeType.Water, PokeType.Rock), new string[] { "すいすい", "いしあたま", "がんじょう" }, GenderRatio.M7F1));
            dexData.Add(new Species(370, -1, -1, -1, "ラブカス", "", new uint[] { 43, 30, 55, 40, 65, 97 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "うるおいボディ" }, GenderRatio.M1F3));
            dexData.Add(new Species(371, -1, -1, 113, "タツベイ", "", new uint[] { 45, 75, 60, 40, 30, 50 }, (PokeType.Dragon, PokeType.Non), new string[] { "いしあたま", "いしあたま", "ちからずく" }, GenderRatio.M1F1));
            dexData.Add(new Species(372, -1, -1, 114, "コモルー", "", new uint[] { 65, 95, 100, 60, 50, 50 }, (PokeType.Dragon, PokeType.Non), new string[] { "いしあたま", "いしあたま", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(373, -1, -1, 115, "ボーマンダ", "", new uint[] { 95, 135, 80, 110, 80, 100 }, (PokeType.Dragon, PokeType.Flying), new string[] { "いかく", "いかく", "じしんかじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(374, -1, -1, 129, "ダンバル", "", new uint[] { 40, 55, 80, 35, 60, 30 }, (PokeType.Steel, PokeType.Psychic), new string[] { "クリアボディ", "クリアボディ", "ライトメタル" }, GenderRatio.Genderless));
            dexData.Add(new Species(375, -1, -1, 130, "メタング", "", new uint[] { 60, 75, 100, 55, 80, 50 }, (PokeType.Steel, PokeType.Psychic), new string[] { "クリアボディ", "クリアボディ", "ライトメタル" }, GenderRatio.Genderless));
            dexData.Add(new Species(376, -1, -1, 131, "メタグロス", "", new uint[] { 80, 135, 130, 95, 90, 70 }, (PokeType.Steel, PokeType.Psychic), new string[] { "クリアボディ", "クリアボディ", "ライトメタル" }, GenderRatio.Genderless));
            dexData.Add(new Species(377, -1, -1, 197, "レジロック", "", new uint[] { 80, 100, 200, 50, 100, 50 }, (PokeType.Rock, PokeType.Non), new string[] { "クリアボディ", "クリアボディ", "がんじょう" }, GenderRatio.Genderless));
            dexData.Add(new Species(378, -1, -1, 198, "レジアイス", "", new uint[] { 80, 50, 100, 100, 200, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "クリアボディ", "クリアボディ", "アイスボディ" }, GenderRatio.Genderless));
            dexData.Add(new Species(379, -1, -1, 199, "レジスチル", "", new uint[] { 80, 75, 150, 75, 150, 50 }, (PokeType.Steel, PokeType.Non), new string[] { "クリアボディ", "クリアボディ", "ライトメタル" }, GenderRatio.Genderless));
            dexData.Add(new Species(380, -1, -1, -1, "ラティアス", "", new uint[] { 80, 80, 90, 110, 130, 110 }, (PokeType.Dragon, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(381, -1, -1, -1, "ラティオス", "", new uint[] { 80, 90, 80, 130, 110, 110 }, (PokeType.Dragon, PokeType.Psychic), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(382, -1, -1, -1, "カイオーガ", "", new uint[] { 100, 100, 90, 150, 140, 90 }, (PokeType.Water, PokeType.Non), new string[] { "あめふらし", "あめふらし", "あめふらし" }, GenderRatio.Genderless));
            dexData.Add(new Species(383, -1, -1, -1, "グラードン", "", new uint[] { 100, 150, 140, 100, 90, 90 }, (PokeType.Ground, PokeType.Non), new string[] { "ひでり", "ひでり", "ひでり" }, GenderRatio.Genderless));
            dexData.Add(new Species(384, -1, -1, -1, "レックウザ", "", new uint[] { 105, 150, 90, 150, 90, 95 }, (PokeType.Dragon, PokeType.Flying), new string[] { "エアロック", "エアロック", "エアロック" }, GenderRatio.Genderless));
            dexData.Add(new Species(385, -1, -1, -1, "ジラーチ", "", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Steel, PokeType.Psychic), new string[] { "てんのめぐみ", "てんのめぐみ", "てんのめぐみ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(386, -1, -1, -1, "デオキシス", "ノーマル", new uint[] { 50, 150, 50, 150, 50, 150 }, (PokeType.Psychic, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "プレッシャー" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(386, -1, -1, -1, "デオキシス", "アタック", new uint[] { 50, 180, 20, 180, 20, 150 }, (PokeType.Psychic, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "プレッシャー" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(386, -1, -1, -1, "デオキシス", "ディフェンス", new uint[] { 50, 70, 160, 70, 160, 90 }, (PokeType.Psychic, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "プレッシャー" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(386, -1, -1, -1, "デオキシス", "スピード", new uint[] { 50, 95, 90, 95, 90, 180 }, (PokeType.Psychic, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "プレッシャー" }, GenderRatio.Genderless));
            dexData.Add(new Species(387, -1, -1, -1, "ナエトル", "", new uint[] { 55, 68, 64, 45, 55, 31 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "シェルアーマー" }, GenderRatio.M7F1));
            dexData.Add(new Species(388, -1, -1, -1, "ハヤシガメ", "", new uint[] { 75, 89, 85, 55, 65, 36 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "シェルアーマー" }, GenderRatio.M7F1));
            dexData.Add(new Species(389, -1, -1, -1, "ドダイトス", "", new uint[] { 95, 109, 105, 75, 85, 56 }, (PokeType.Grass, PokeType.Ground), new string[] { "しんりょく", "しんりょく", "シェルアーマー" }, GenderRatio.M7F1));
            dexData.Add(new Species(390, -1, -1, -1, "ヒコザル", "", new uint[] { 44, 58, 44, 58, 44, 61 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "てつのこぶし" }, GenderRatio.M7F1));
            dexData.Add(new Species(391, -1, -1, -1, "モウカザル", "", new uint[] { 64, 78, 52, 78, 52, 81 }, (PokeType.Fire, PokeType.Fighting), new string[] { "もうか", "もうか", "てつのこぶし" }, GenderRatio.M7F1));
            dexData.Add(new Species(392, -1, -1, -1, "ゴウカザル", "", new uint[] { 76, 104, 71, 104, 71, 108 }, (PokeType.Fire, PokeType.Fighting), new string[] { "もうか", "もうか", "てつのこぶし" }, GenderRatio.M7F1));
            dexData.Add(new Species(393, -1, -1, -1, "ポッチャマ", "", new uint[] { 53, 51, 53, 61, 56, 40 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "まけんき" }, GenderRatio.M7F1));
            dexData.Add(new Species(394, -1, -1, -1, "ポッタイシ", "", new uint[] { 64, 66, 68, 81, 76, 50 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "まけんき" }, GenderRatio.M7F1));
            dexData.Add(new Species(395, -1, -1, -1, "エンペルト", "", new uint[] { 84, 86, 88, 111, 101, 60 }, (PokeType.Water, PokeType.Steel), new string[] { "げきりゅう", "げきりゅう", "まけんき" }, GenderRatio.M7F1));
            dexData.Add(new Species(396, -1, -1, -1, "ムックル", "", new uint[] { 40, 55, 30, 30, 30, 60 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "するどいめ", "すてみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(397, -1, -1, -1, "ムクバード", "", new uint[] { 55, 75, 50, 40, 40, 80 }, (PokeType.Normal, PokeType.Flying), new string[] { "いかく", "いかく", "すてみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(398, -1, -1, -1, "ムクホーク", "", new uint[] { 85, 120, 70, 50, 60, 100 }, (PokeType.Normal, PokeType.Flying), new string[] { "いかく", "いかく", "すてみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(399, -1, -1, -1, "ビッパ", "", new uint[] { 59, 45, 40, 35, 40, 31 }, (PokeType.Normal, PokeType.Non), new string[] { "たんじゅん", "てんねん", "ムラっけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(400, -1, -1, -1, "ビーダル", "", new uint[] { 79, 85, 60, 55, 60, 71 }, (PokeType.Normal, PokeType.Non), new string[] { "たんじゅん", "てんねん", "ムラっけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(401, -1, -1, -1, "コロボーシ", "", new uint[] { 37, 25, 41, 25, 41, 25 }, (PokeType.Bug, PokeType.Non), new string[] { "だっぴ", "だっぴ", "にげあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(402, -1, -1, -1, "コロトック", "", new uint[] { 77, 85, 51, 55, 51, 65 }, (PokeType.Bug, PokeType.Non), new string[] { "むしのしらせ", "むしのしらせ", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new Species(403, -1, 25, -1, "コリンク", "", new uint[] { 45, 65, 34, 40, 34, 45 }, (PokeType.Electric, PokeType.Non), new string[] { "とうそうしん", "いかく", "こんじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(404, -1, 26, -1, "ルクシオ", "", new uint[] { 60, 85, 49, 60, 49, 60 }, (PokeType.Electric, PokeType.Non), new string[] { "とうそうしん", "いかく", "こんじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(405, -1, 27, -1, "レントラー", "", new uint[] { 80, 120, 79, 95, 79, 70 }, (PokeType.Electric, PokeType.Non), new string[] { "とうそうしん", "いかく", "こんじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(406, 59, -1, -1, "スボミー", "", new uint[] { 40, 30, 35, 50, 70, 55 }, (PokeType.Grass, PokeType.Poison), new string[] { "しぜんかいふく", "どくのトゲ", "リーフガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(407, 61, -1, -1, "ロズレイド", "", new uint[] { 60, 70, 65, 125, 105, 90 }, (PokeType.Grass, PokeType.Poison), new string[] { "しぜんかいふく", "どくのトゲ", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new Species(408, -1, -1, -1, "ズガイドス", "", new uint[] { 67, 125, 40, 30, 30, 58 }, (PokeType.Rock, PokeType.Non), new string[] { "かたやぶり", "かたやぶり", "ちからずく" }, GenderRatio.M7F1));
            dexData.Add(new Species(409, -1, -1, -1, "ラムパルド", "", new uint[] { 97, 165, 60, 65, 50, 58 }, (PokeType.Rock, PokeType.Non), new string[] { "かたやぶり", "かたやぶり", "ちからずく" }, GenderRatio.M7F1));
            dexData.Add(new Species(410, -1, -1, -1, "タテトプス", "", new uint[] { 30, 42, 118, 42, 88, 30 }, (PokeType.Rock, PokeType.Steel), new string[] { "がんじょう", "がんじょう", "ぼうおん" }, GenderRatio.M7F1));
            dexData.Add(new Species(411, -1, -1, -1, "トリデプス", "", new uint[] { 60, 52, 168, 47, 138, 30 }, (PokeType.Rock, PokeType.Steel), new string[] { "がんじょう", "がんじょう", "ぼうおん" }, GenderRatio.M7F1));
            dexData.Add(new Species(412, -1, -1, -1, "ミノムッチ", "", new uint[] { 40, 29, 45, 29, 45, 36 }, (PokeType.Bug, PokeType.Grass), new string[] { "だっぴ", "だっぴ", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(413, -1, -1, -1, "ミノマダム", "草木", new uint[] { 60, 59, 85, 79, 105, 36 }, (PokeType.Bug, PokeType.Grass), new string[] { "きけんよち", "きけんよち", "ぼうじん" }, GenderRatio.FemaleOnly));
            dexData.Add(new AnotherForm(413, -1, -1, -1, "ミノマダム", "砂地", new uint[] { 60, 79, 105, 59, 85, 36 }, (PokeType.Bug, PokeType.Ground), new string[] { "きけんよち", "きけんよち", "ぼうじん" }, GenderRatio.FemaleOnly));
            dexData.Add(new AnotherForm(413, -1, -1, -1, "ミノマダム", "ゴミ", new uint[] { 60, 69, 95, 69, 95, 36 }, (PokeType.Bug, PokeType.Steel), new string[] { "きけんよち", "きけんよち", "ぼうじん" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(414, -1, -1, -1, "ガーメイル", "", new uint[] { 70, 94, 50, 94, 50, 66 }, (PokeType.Bug, PokeType.Flying), new string[] { "むしのしらせ", "むしのしらせ", "いろめがね" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(415, 116, 203, -1, "ミツハニー", "", new uint[] { 30, 30, 42, 30, 42, 70 }, (PokeType.Bug, PokeType.Flying), new string[] { "みつあつめ", "みつあつめ", "はりきり" }, GenderRatio.M7F1));
            dexData.Add(new Species(416, 117, 204, -1, "ビークイン", "", new uint[] { 70, 80, 102, 80, 102, 40 }, (PokeType.Bug, PokeType.Flying), new string[] { "プレッシャー", "プレッシャー", "きんちょうかん" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(417, -1, -1, -1, "パチリス", "", new uint[] { 60, 45, 70, 45, 90, 95 }, (PokeType.Electric, PokeType.Non), new string[] { "にげあし", "ものひろい", "ちくでん" }, GenderRatio.M1F1));
            dexData.Add(new Species(418, -1, -1, -1, "ブイゼル", "", new uint[] { 55, 65, 35, 60, 30, 85 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "みずのベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(419, -1, -1, -1, "フローゼル", "", new uint[] { 85, 105, 55, 85, 50, 115 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "みずのベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(420, 128, -1, -1, "チェリンボ", "", new uint[] { 45, 35, 45, 62, 53, 35 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "ようりょくそ", "ようりょくそ" }, GenderRatio.M1F1));
            dexData.Add(new Species(421, 129, -1, -1, "チェリム", "", new uint[] { 70, 60, 70, 87, 78, 85 }, (PokeType.Grass, PokeType.Non), new string[] { "フラワーギフト", "フラワーギフト", "フラワーギフト" }, GenderRatio.M1F1));
            dexData.Add(new Species(422, 230, -1, -1, "カラナクシ", "", new uint[] { 76, 48, 48, 57, 62, 34 }, (PokeType.Water, PokeType.Non), new string[] { "ねんちゃく", "よびみず", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(423, 231, -1, -1, "トリトドン", "", new uint[] { 111, 83, 68, 92, 82, 39 }, (PokeType.Water, PokeType.Ground), new string[] { "ねんちゃく", "よびみず", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(424, -1, -1, -1, "エテボース", "", new uint[] { 75, 100, 66, 60, 66, 115 }, (PokeType.Normal, PokeType.Non), new string[] { "テクニシャン", "ものひろい", "スキルリンク" }, GenderRatio.M1F1));
            dexData.Add(new Species(425, 124, 135, -1, "フワンテ", "", new uint[] { 90, 50, 34, 60, 44, 70 }, (PokeType.Ghost, PokeType.Flying), new string[] { "ゆうばく", "かるわざ", "ねつぼうそう" }, GenderRatio.M1F1));
            dexData.Add(new Species(426, 125, 136, -1, "フワライド", "", new uint[] { 150, 80, 44, 90, 54, 80 }, (PokeType.Ghost, PokeType.Flying), new string[] { "ゆうばく", "かるわざ", "ねつぼうそう" }, GenderRatio.M1F1));
            dexData.Add(new Species(427, -1, 4, -1, "ミミロル", "", new uint[] { 55, 66, 44, 44, 56, 85 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "ぶきよう", "じゅうなん" }, GenderRatio.M1F1));
            dexData.Add(new Species(428, -1, 5, -1, "ミミロップ", "", new uint[] { 65, 76, 84, 54, 96, 105 }, (PokeType.Normal, PokeType.Non), new string[] { "メロメロボディ", "ぶきよう", "じゅうなん" }, GenderRatio.M1F1));
            dexData.Add(new Species(429, -1, -1, -1, "ムウマージ", "", new uint[] { 60, 60, 60, 105, 105, 105 }, (PokeType.Ghost, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(430, -1, -1, -1, "ドンカラス", "", new uint[] { 100, 125, 52, 105, 52, 71 }, (PokeType.Dark, PokeType.Flying), new string[] { "ふみん", "きょううん", "じしんかじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(431, -1, -1, -1, "ニャルマー", "", new uint[] { 49, 55, 42, 42, 37, 85 }, (PokeType.Normal, PokeType.Non), new string[] { "じゅうなん", "マイペース", "するどいめ" }, GenderRatio.M1F3));
            dexData.Add(new Species(432, -1, -1, -1, "ブニャット", "", new uint[] { 71, 82, 64, 64, 59, 112 }, (PokeType.Normal, PokeType.Non), new string[] { "あついしぼう", "マイペース", "まけんき" }, GenderRatio.M1F3));
            dexData.Add(new Species(433, -1, -1, -1, "リーシャン", "", new uint[] { 45, 30, 50, 65, 50, 45 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(434, 130, -1, -1, "スカンプー", "", new uint[] { 63, 63, 47, 41, 41, 74 }, (PokeType.Poison, PokeType.Dark), new string[] { "あくしゅう", "ゆうばく", "するどいめ" }, GenderRatio.M1F1));
            dexData.Add(new Species(435, 131, -1, -1, "スカタンク", "", new uint[] { 103, 93, 67, 71, 61, 84 }, (PokeType.Poison, PokeType.Dark), new string[] { "あくしゅう", "ゆうばく", "するどいめ" }, GenderRatio.M1F1));
            dexData.Add(new Species(436, 118, -1, 87, "ドーミラー", "", new uint[] { 57, 24, 86, 24, 86, 23 }, (PokeType.Steel, PokeType.Psychic), new string[] { "ふゆう", "たいねつ", "ヘヴィメタル" }, GenderRatio.Genderless));
            dexData.Add(new Species(437, 119, -1, 88, "ドータクン", "", new uint[] { 67, 89, 116, 79, 116, 33 }, (PokeType.Steel, PokeType.Psychic), new string[] { "ふゆう", "たいねつ", "ヘヴィメタル" }, GenderRatio.Genderless));
            dexData.Add(new Species(438, 252, -1, -1, "ウソハチ", "", new uint[] { 50, 80, 95, 10, 45, 10 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "いしあたま", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(439, 364, -1, 10, "マネネ", "", new uint[] { 20, 25, 45, 70, 90, 60 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "ぼうおん", "フィルター", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new Species(440, -1, 6, -1, "ピンプク", "", new uint[] { 100, 5, 5, 15, 65, 30 }, (PokeType.Normal, PokeType.Non), new string[] { "しぜんかいふく", "てんのめぐみ", "フレンドガード" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(441, -1, -1, -1, "ペラップ", "", new uint[] { 76, 65, 45, 92, 42, 91 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "ちどりあし", "はとむね" }, GenderRatio.M1F1));
            dexData.Add(new Species(442, -1, -1, 47, "ミカルゲ", "", new uint[] { 50, 92, 108, 92, 108, 35 }, (PokeType.Ghost, PokeType.Dark), new string[] { "プレッシャー", "プレッシャー", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(443, -1, -1, 116, "フカマル", "", new uint[] { 58, 70, 45, 40, 45, 42 }, (PokeType.Dragon, PokeType.Ground), new string[] { "すながくれ", "すながくれ", "さめはだ" }, GenderRatio.M1F1));
            dexData.Add(new Species(444, -1, -1, 117, "ガバイト", "", new uint[] { 68, 90, 65, 50, 55, 82 }, (PokeType.Dragon, PokeType.Ground), new string[] { "すながくれ", "すながくれ", "さめはだ" }, GenderRatio.M1F1));
            dexData.Add(new Species(445, -1, -1, 118, "ガブリアス", "", new uint[] { 108, 130, 95, 80, 85, 102 }, (PokeType.Dragon, PokeType.Ground), new string[] { "すながくれ", "すながくれ", "さめはだ" }, GenderRatio.M1F1));
            dexData.Add(new Species(446, 260, -1, 172, "ゴンベ", "", new uint[] { 135, 85, 40, 40, 85, 5 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "あついしぼう", "くいしんぼう" }, GenderRatio.M7F1));
            dexData.Add(new Species(447, 298, -1, 134, "リオル", "", new uint[] { 40, 70, 40, 35, 40, 60 }, (PokeType.Fighting, PokeType.Non), new string[] { "ふくつのこころ", "せいしんりょく", "いたずらごころ" }, GenderRatio.M7F1));
            dexData.Add(new Species(448, 299, -1, 135, "ルカリオ", "", new uint[] { 70, 110, 70, 115, 70, 90 }, (PokeType.Fighting, PokeType.Steel), new string[] { "ふくつのこころ", "せいしんりょく", "せいぎのこころ" }, GenderRatio.M7F1));
            dexData.Add(new Species(449, 314, -1, -1, "ヒポポタス", "", new uint[] { 68, 72, 78, 38, 42, 32 }, (PokeType.Ground, PokeType.Non), new string[] { "すなおこし", "すなおこし", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(450, 315, -1, -1, "カバルドン", "", new uint[] { 108, 112, 118, 68, 72, 47 }, (PokeType.Ground, PokeType.Non), new string[] { "すなおこし", "すなおこし", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(451, 285, 50, -1, "スコルピ", "", new uint[] { 40, 50, 90, 30, 55, 65 }, (PokeType.Poison, PokeType.Bug), new string[] { "カブトアーマー", "スナイパー", "するどいめ" }, GenderRatio.M1F1));
            dexData.Add(new Species(452, 286, 51, -1, "ドラピオン", "", new uint[] { 70, 90, 110, 60, 75, 95 }, (PokeType.Poison, PokeType.Dark), new string[] { "カブトアーマー", "スナイパー", "するどいめ" }, GenderRatio.M1F1));
            dexData.Add(new Species(453, 222, 82, -1, "グレッグル", "", new uint[] { 48, 61, 40, 61, 40, 50 }, (PokeType.Poison, PokeType.Fighting), new string[] { "きけんよち", "かんそうはだ", "どくしゅ" }, GenderRatio.M1F1));
            dexData.Add(new Species(454, 223, 83, -1, "ドクロッグ", "", new uint[] { 83, 106, 65, 86, 65, 85 }, (PokeType.Poison, PokeType.Fighting), new string[] { "きけんよち", "かんそうはだ", "どくしゅ" }, GenderRatio.M1F1));
            dexData.Add(new Species(455, -1, -1, -1, "マスキッパ", "", new uint[] { 74, 100, 72, 90, 72, 46 }, (PokeType.Grass, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(456, -1, -1, -1, "ケイコウオ", "", new uint[] { 49, 49, 56, 49, 61, 66 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "よびみず", "みずのベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(457, -1, -1, -1, "ネオラント", "", new uint[] { 69, 69, 76, 69, 86, 91 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "よびみず", "みずのベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(458, 354, 46, -1, "タマンタ", "", new uint[] { 45, 20, 50, 60, 120, 50 }, (PokeType.Water, PokeType.Flying), new string[] { "すいすい", "ちょすい", "みずのベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(459, 96, -1, 31, "ユキカブリ", "", new uint[] { 60, 62, 50, 62, 60, 40 }, (PokeType.Grass, PokeType.Ice), new string[] { "ゆきふらし", "ゆきふらし", "ぼうおん" }, GenderRatio.M1F1));
            dexData.Add(new Species(460, 97, -1, 32, "ユキノオー", "", new uint[] { 90, 92, 75, 92, 85, 60 }, (PokeType.Grass, PokeType.Ice), new string[] { "ゆきふらし", "ゆきふらし", "ぼうおん" }, GenderRatio.M1F1));
            dexData.Add(new Species(461, 293, -1, 29, "マニューラ", "", new uint[] { 70, 120, 65, 45, 85, 125 }, (PokeType.Dark, PokeType.Ice), new string[] { "プレッシャー", "プレッシャー", "わるいてぐせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(462, -1, 107, -1, "ジバコイル", "", new uint[] { 70, 70, 115, 130, 90, 60 }, (PokeType.Electric, PokeType.Steel), new string[] { "じりょく", "がんじょう", "アナライズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(463, -1, 55, -1, "ベロベルト", "", new uint[] { 110, 85, 95, 80, 95, 50 }, (PokeType.Normal, PokeType.Non), new string[] { "マイペース", "どんかん", "ノーてんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(464, 266, 185, -1, "ドサイドン", "", new uint[] { 115, 140, 130, 55, 55, 40 }, (PokeType.Ground, PokeType.Rock), new string[] { "ひらいしん", "ハードロック", "すてみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(465, -1, 81, -1, "モジャンボ", "", new uint[] { 100, 100, 125, 110, 50, 50 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "リーフガード", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(466, -1, -1, 17, "エレキブル", "", new uint[] { 75, 123, 67, 95, 85, 95 }, (PokeType.Electric, PokeType.Non), new string[] { "でんきエンジン", "でんきエンジン", "やるき" }, GenderRatio.M3F1));
            dexData.Add(new Species(467, -1, -1, 20, "ブーバーン", "", new uint[] { 75, 95, 67, 125, 95, 83 }, (PokeType.Fire, PokeType.Non), new string[] { "ほのおのからだ", "ほのおのからだ", "やるき" }, GenderRatio.M3F1));
            dexData.Add(new Species(468, 259, -1, -1, "トゲキッス", "", new uint[] { 85, 50, 95, 120, 115, 80 }, (PokeType.Fairy, PokeType.Flying), new string[] { "はりきり", "てんのめぐみ", "きょううん" }, GenderRatio.M7F1));
            dexData.Add(new Species(469, -1, -1, -1, "メガヤンマ", "", new uint[] { 86, 76, 86, 116, 56, 95 }, (PokeType.Bug, PokeType.Flying), new string[] { "かそく", "いろめがね", "おみとおし" }, GenderRatio.M1F1));
            dexData.Add(new Species(470, 202, -1, 81, "リーフィア", "", new uint[] { 65, 110, 130, 60, 65, 95 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "リーフガード", "ようりょくそ" }, GenderRatio.M7F1));
            dexData.Add(new Species(471, 203, -1, 80, "グレイシア", "", new uint[] { 65, 60, 110, 130, 95, 65 }, (PokeType.Ice, PokeType.Non), new string[] { "ゆきがくれ", "ゆきがくれ", "アイスボディ" }, GenderRatio.M7F1));
            dexData.Add(new Species(472, -1, -1, -1, "グライオン", "", new uint[] { 75, 95, 125, 45, 75, 95 }, (PokeType.Ground, PokeType.Flying), new string[] { "かいりきバサミ", "すながくれ", "ポイズンヒール" }, GenderRatio.M1F1));
            dexData.Add(new Species(473, 77, -1, 9, "マンムー", "", new uint[] { 110, 130, 80, 70, 60, 80 }, (PokeType.Ice, PokeType.Ground), new string[] { "どんかん", "ゆきがくれ", "あついしぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(474, -1, 210, -1, "ポリゴンZ", "", new uint[] { 85, 80, 70, 135, 75, 90 }, (PokeType.Normal, PokeType.Non), new string[] { "てきおうりょく", "ダウンロード", "アナライズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(475, 123, 37, -1, "エルレイド", "", new uint[] { 68, 125, 65, 65, 115, 80 }, (PokeType.Psychic, PokeType.Fighting), new string[] { "ふくつのこころ", "ふくつのこころ", "せいぎのこころ" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(476, -1, -1, -1, "ダイノーズ", "", new uint[] { 60, 55, 145, 75, 150, 40 }, (PokeType.Rock, PokeType.Steel), new string[] { "がんじょう", "じりょく", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(477, 137, -1, -1, "ヨノワール", "", new uint[] { 45, 100, 135, 65, 135, 45 }, (PokeType.Ghost, PokeType.Non), new string[] { "プレッシャー", "プレッシャー", "おみとおし" }, GenderRatio.M1F1));
            dexData.Add(new Species(478, 81, -1, 27, "ユキメノコ", "", new uint[] { 70, 80, 70, 80, 70, 110 }, (PokeType.Ice, PokeType.Ghost), new string[] { "ゆきがくれ", "ゆきがくれ", "のろわれボディ" }, GenderRatio.FemaleOnly));
            dexData.Add(new AnotherForm(479, 372, -1, -1, "ロトム", "ノーマル", new uint[] { 50, 50, 77, 95, 77, 91 }, (PokeType.Electric, PokeType.Ghost), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(479, 372, -1, -1, "ロトム", "ヒート", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Fire), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(479, 372, -1, -1, "ロトム", "ウォッシュ", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Water), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(479, 372, -1, -1, "ロトム", "フロスト", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Ice), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(479, 372, -1, -1, "ロトム", "スピン", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Flying), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(479, 372, -1, -1, "ロトム", "カット", new uint[] { 50, 65, 107, 105, 107, 86 }, (PokeType.Electric, PokeType.Grass), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(480, -1, -1, -1, "ユクシー", "", new uint[] { 75, 75, 130, 75, 130, 95 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(481, -1, -1, -1, "エムリット", "", new uint[] { 80, 105, 105, 105, 105, 80 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(482, -1, -1, -1, "アグノム", "", new uint[] { 75, 125, 70, 125, 70, 115 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(483, -1, -1, -1, "ディアルガ", "", new uint[] { 100, 120, 120, 150, 100, 90 }, (PokeType.Steel, PokeType.Dragon), new string[] { "プレッシャー", "プレッシャー", "テレパシー" }, GenderRatio.Genderless));
            dexData.Add(new Species(484, -1, -1, -1, "パルキア", "", new uint[] { 90, 120, 100, 150, 120, 100 }, (PokeType.Water, PokeType.Dragon), new string[] { "プレッシャー", "プレッシャー", "テレパシー" }, GenderRatio.Genderless));
            dexData.Add(new Species(485, -1, -1, -1, "ヒードラン", "", new uint[] { 91, 90, 106, 130, 106, 77 }, (PokeType.Fire, PokeType.Steel), new string[] { "もらいび", "もらいび", "ほのおのからだ" }, GenderRatio.M1F1));
            dexData.Add(new Species(486, -1, -1, -1, "レジギガス", "", new uint[] { 110, 160, 110, 80, 110, 100 }, (PokeType.Normal, PokeType.Non), new string[] { "スロースタート", "スロースタート", "スロースタート" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(487, -1, -1, -1, "ギラティナ", "アナザー", new uint[] { 150, 100, 120, 100, 120, 90 }, (PokeType.Ghost, PokeType.Dragon), new string[] { "プレッシャー", "プレッシャー", "テレパシー" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(487, -1, -1, -1, "ギラティナ", "オリジン", new uint[] { 150, 120, 100, 120, 100, 90 }, (PokeType.Ghost, PokeType.Dragon), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(488, -1, -1, -1, "クレセリア", "", new uint[] { 120, 70, 120, 75, 130, 85 }, (PokeType.Psychic, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(489, -1, -1, -1, "フィオネ", "", new uint[] { 80, 80, 80, 80, 80, 80 }, (PokeType.Water, PokeType.Non), new string[] { "うるおいボディ", "うるおいボディ", "うるおいボディ" }, GenderRatio.Genderless));
            dexData.Add(new Species(490, -1, -1, -1, "マナフィ", "", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Water, PokeType.Non), new string[] { "うるおいボディ", "うるおいボディ", "うるおいボディ" }, GenderRatio.Genderless));
            dexData.Add(new Species(491, -1, -1, -1, "ダークライ", "", new uint[] { 70, 90, 90, 135, 90, 125 }, (PokeType.Dark, PokeType.Non), new string[] { "ナイトメア", "ナイトメア", "ナイトメア" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(492, -1, -1, -1, "シェイミ", "ランド", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Grass, PokeType.Non), new string[] { "しぜんかいふく", "しぜんかいふく", "しぜんかいふく" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(492, -1, -1, -1, "シェイミ", "スカイ", new uint[] { 100, 103, 75, 120, 75, 127 }, (PokeType.Grass, PokeType.Flying), new string[] { "てんのめぐみ", "てんのめぐみ", "てんのめぐみ" }, GenderRatio.Genderless));
            dexData.Add(new Species(493, -1, -1, -1, "アルセウス", "", new uint[] { 120, 120, 120, 120, 120, 120 }, (PokeType.Normal, PokeType.Non), new string[] { "マルチタイプ", "マルチタイプ", "マルチタイプ" }, GenderRatio.Genderless));
            dexData.Add(new Species(494, -1, -1, -1, "ビクティニ", "", new uint[] { 100, 100, 100, 100, 100, 100 }, (PokeType.Psychic, PokeType.Fire), new string[] { "しょうりのほし", "しょうりのほし", "しょうりのほし" }, GenderRatio.Genderless));
            dexData.Add(new Species(495, -1, -1, -1, "ツタージャ", "", new uint[] { 45, 45, 55, 45, 55, 63 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "あまのじゃく" }, GenderRatio.M7F1));
            dexData.Add(new Species(496, -1, -1, -1, "ジャノビー", "", new uint[] { 60, 60, 75, 60, 75, 83 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "あまのじゃく" }, GenderRatio.M7F1));
            dexData.Add(new Species(497, -1, -1, -1, "ジャローダ", "", new uint[] { 75, 75, 95, 75, 95, 113 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "あまのじゃく" }, GenderRatio.M7F1));
            dexData.Add(new Species(498, -1, -1, -1, "ポカブ", "", new uint[] { 65, 63, 45, 45, 45, 45 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "あついしぼう" }, GenderRatio.M7F1));
            dexData.Add(new Species(499, -1, -1, -1, "チャオブー", "", new uint[] { 90, 93, 55, 70, 55, 55 }, (PokeType.Fire, PokeType.Fighting), new string[] { "もうか", "もうか", "あついしぼう" }, GenderRatio.M7F1));
            dexData.Add(new Species(500, -1, -1, -1, "エンブオー", "", new uint[] { 110, 123, 65, 100, 65, 65 }, (PokeType.Fire, PokeType.Fighting), new string[] { "もうか", "もうか", "すてみ" }, GenderRatio.M7F1));
            dexData.Add(new Species(501, -1, -1, -1, "ミジュマル", "", new uint[] { 55, 55, 45, 63, 45, 45 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "シェルアーマー" }, GenderRatio.M7F1));
            dexData.Add(new Species(502, -1, -1, -1, "フタチマル", "", new uint[] { 75, 75, 60, 83, 60, 60 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "シェルアーマー" }, GenderRatio.M7F1));
            dexData.Add(new Species(503, -1, -1, -1, "ダイケンキ", "", new uint[] { 95, 100, 85, 108, 70, 70 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "シェルアーマー" }, GenderRatio.M7F1));
            dexData.Add(new Species(504, -1, -1, -1, "ミネズミ", "", new uint[] { 45, 55, 39, 35, 39, 42 }, (PokeType.Normal, PokeType.Non), new string[] { "にげあし", "するどいめ", "アナライズ" }, GenderRatio.M1F1));
            dexData.Add(new Species(505, -1, -1, -1, "ミルホッグ", "", new uint[] { 60, 85, 69, 60, 69, 77 }, (PokeType.Normal, PokeType.Non), new string[] { "はっこう", "するどいめ", "アナライズ" }, GenderRatio.M1F1));
            dexData.Add(new Species(506, -1, 113, -1, "ヨーテリー", "", new uint[] { 45, 60, 45, 25, 45, 55 }, (PokeType.Normal, PokeType.Non), new string[] { "やるき", "ものひろい", "にげあし" }, GenderRatio.M1F1));
            dexData.Add(new Species(507, -1, 114, -1, "ハーデリア", "", new uint[] { 65, 80, 65, 35, 65, 60 }, (PokeType.Normal, PokeType.Non), new string[] { "いかく", "すなかき", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(508, -1, 115, -1, "ムーランド", "", new uint[] { 85, 110, 90, 45, 90, 80 }, (PokeType.Normal, PokeType.Non), new string[] { "いかく", "すなかき", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(509, 44, -1, -1, "チョロネコ", "", new uint[] { 41, 50, 37, 50, 37, 66 }, (PokeType.Dark, PokeType.Non), new string[] { "じゅうなん", "かるわざ", "いたずらごころ" }, GenderRatio.M1F1));
            dexData.Add(new Species(510, 45, -1, -1, "レパルダス", "", new uint[] { 64, 88, 50, 88, 50, 106 }, (PokeType.Dark, PokeType.Non), new string[] { "じゅうなん", "かるわざ", "いたずらごころ" }, GenderRatio.M1F1));
            dexData.Add(new Species(511, -1, -1, -1, "ヤナップ", "", new uint[] { 50, 53, 48, 53, 48, 64 }, (PokeType.Grass, PokeType.Non), new string[] { "くいしんぼう", "くいしんぼう", "しんりょく" }, GenderRatio.M7F1));
            dexData.Add(new Species(512, -1, -1, -1, "ヤナッキー", "", new uint[] { 75, 98, 63, 98, 63, 101 }, (PokeType.Grass, PokeType.Non), new string[] { "くいしんぼう", "くいしんぼう", "しんりょく" }, GenderRatio.M7F1));
            dexData.Add(new Species(513, -1, -1, -1, "バオップ", "", new uint[] { 50, 53, 48, 53, 48, 64 }, (PokeType.Fire, PokeType.Non), new string[] { "くいしんぼう", "くいしんぼう", "もうか" }, GenderRatio.M7F1));
            dexData.Add(new Species(514, -1, -1, -1, "バオッキー", "", new uint[] { 75, 98, 63, 98, 63, 101 }, (PokeType.Fire, PokeType.Non), new string[] { "くいしんぼう", "くいしんぼう", "もうか" }, GenderRatio.M7F1));
            dexData.Add(new Species(515, -1, -1, -1, "ヒヤップ", "", new uint[] { 50, 53, 48, 53, 48, 64 }, (PokeType.Water, PokeType.Non), new string[] { "くいしんぼう", "くいしんぼう", "げきりゅう" }, GenderRatio.M7F1));
            dexData.Add(new Species(516, -1, -1, -1, "ヒヤッキー", "", new uint[] { 75, 98, 63, 98, 63, 101 }, (PokeType.Water, PokeType.Non), new string[] { "くいしんぼう", "くいしんぼう", "げきりゅう" }, GenderRatio.M7F1));
            dexData.Add(new Species(517, 90, -1, -1, "ムンナ", "", new uint[] { 76, 25, 45, 67, 55, 24 }, (PokeType.Psychic, PokeType.Non), new string[] { "よちむ", "シンクロ", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(518, 91, -1, -1, "ムシャーナ", "", new uint[] { 116, 55, 85, 107, 95, 29 }, (PokeType.Psychic, PokeType.Non), new string[] { "よちむ", "シンクロ", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(519, 26, -1, -1, "マメパト", "", new uint[] { 50, 55, 50, 36, 30, 43 }, (PokeType.Normal, PokeType.Flying), new string[] { "はとむね", "きょううん", "とうそうしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(520, 27, -1, -1, "ハトーボー", "", new uint[] { 62, 77, 62, 50, 42, 65 }, (PokeType.Normal, PokeType.Flying), new string[] { "はとむね", "きょううん", "とうそうしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(521, 28, -1, -1, "ケンホロウ", "", new uint[] { 80, 115, 80, 65, 55, 93 }, (PokeType.Normal, PokeType.Flying), new string[] { "はとむね", "きょううん", "とうそうしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(522, -1, -1, -1, "シママ", "", new uint[] { 45, 60, 32, 50, 32, 76 }, (PokeType.Electric, PokeType.Non), new string[] { "ひらいしん", "でんきエンジン", "そうしょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(523, -1, -1, -1, "ゼブライカ", "", new uint[] { 75, 100, 63, 80, 63, 116 }, (PokeType.Electric, PokeType.Non), new string[] { "ひらいしん", "でんきエンジン", "そうしょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(524, 168, 154, -1, "ダンゴロ", "", new uint[] { 55, 75, 85, 25, 25, 15 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "くだけるよろい", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(525, 169, 155, -1, "ガントル", "", new uint[] { 70, 105, 105, 50, 40, 20 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "くだけるよろい", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(526, 170, 156, -1, "ギガイアス", "", new uint[] { 85, 135, 130, 60, 80, 25 }, (PokeType.Rock, PokeType.Non), new string[] { "がんじょう", "すなおこし", "すなのちから" }, GenderRatio.M1F1));
            dexData.Add(new Species(527, 174, 151, -1, "コロモリ", "", new uint[] { 65, 45, 43, 55, 43, 72 }, (PokeType.Psychic, PokeType.Flying), new string[] { "てんねん", "ぶきよう", "たんじゅん" }, GenderRatio.M1F1));
            dexData.Add(new Species(528, 175, 152, -1, "ココロモリ", "", new uint[] { 67, 57, 55, 77, 55, 114 }, (PokeType.Psychic, PokeType.Flying), new string[] { "てんねん", "ぶきよう", "たんじゅん" }, GenderRatio.M1F1));
            dexData.Add(new Species(529, 166, -1, -1, "モグリュー", "", new uint[] { 60, 85, 40, 30, 45, 68 }, (PokeType.Ground, PokeType.Non), new string[] { "すなかき", "すなのちから", "かたやぶり" }, GenderRatio.M1F1));
            dexData.Add(new Species(530, 167, -1, -1, "ドリュウズ", "", new uint[] { 110, 135, 60, 50, 65, 88 }, (PokeType.Ground, PokeType.Steel), new string[] { "すなかき", "すなのちから", "かたやぶり" }, GenderRatio.M1F1));
            dexData.Add(new Species(531, -1, -1, 21, "タブンネ", "", new uint[] { 103, 60, 86, 60, 86, 50 }, (PokeType.Normal, PokeType.Non), new string[] { "いやしのこころ", "さいせいりょく", "ぶきよう" }, GenderRatio.M1F1));
            dexData.Add(new Species(532, 171, -1, 57, "ドッコラー", "", new uint[] { 75, 80, 55, 25, 35, 35 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ちからずく", "てつのこぶし" }, GenderRatio.M3F1));
            dexData.Add(new Species(533, 172, -1, 58, "ドテッコツ", "", new uint[] { 85, 105, 85, 40, 50, 40 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ちからずく", "てつのこぶし" }, GenderRatio.M3F1));
            dexData.Add(new Species(534, 173, -1, 59, "ローブシン", "", new uint[] { 105, 140, 95, 55, 65, 45 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "ちからずく", "てつのこぶし" }, GenderRatio.M3F1));
            dexData.Add(new Species(535, 132, -1, -1, "オタマロ", "", new uint[] { 50, 50, 40, 50, 40, 64 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "うるおいボディ", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(536, 133, -1, -1, "ガマガル", "", new uint[] { 75, 65, 55, 65, 55, 69 }, (PokeType.Water, PokeType.Ground), new string[] { "すいすい", "うるおいボディ", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(537, 134, -1, -1, "ガマゲロゲ", "", new uint[] { 105, 95, 75, 85, 75, 74 }, (PokeType.Water, PokeType.Ground), new string[] { "すいすい", "どくしゅ", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(538, 248, -1, -1, "ナゲキ", "", new uint[] { 120, 100, 85, 30, 85, 45 }, (PokeType.Fighting, PokeType.Non), new string[] { "こんじょう", "せいしんりょく", "かたやぶり" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(539, 249, -1, -1, "ダゲキ", "", new uint[] { 75, 125, 75, 30, 75, 85 }, (PokeType.Fighting, PokeType.Non), new string[] { "がんじょう", "せいしんりょく", "かたやぶり" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(540, -1, -1, -1, "クルミル", "", new uint[] { 45, 53, 70, 40, 60, 42 }, (PokeType.Bug, PokeType.Grass), new string[] { "むしのしらせ", "ようりょくそ", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(541, -1, -1, -1, "クルマユ", "", new uint[] { 55, 63, 90, 50, 80, 42 }, (PokeType.Bug, PokeType.Grass), new string[] { "リーフガード", "ようりょくそ", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(542, -1, -1, -1, "ハハコモリ", "", new uint[] { 75, 103, 80, 70, 80, 92 }, (PokeType.Bug, PokeType.Grass), new string[] { "むしのしらせ", "ようりょくそ", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(543, -1, 74, -1, "フシデ", "", new uint[] { 30, 45, 59, 30, 39, 57 }, (PokeType.Bug, PokeType.Poison), new string[] { "どくのトゲ", "むしのしらせ", "かそく" }, GenderRatio.M1F1));
            dexData.Add(new Species(544, -1, 75, -1, "ホイーガ", "", new uint[] { 40, 55, 99, 40, 79, 47 }, (PokeType.Bug, PokeType.Poison), new string[] { "どくのトゲ", "むしのしらせ", "かそく" }, GenderRatio.M1F1));
            dexData.Add(new Species(545, -1, 76, -1, "ペンドラー", "", new uint[] { 60, 100, 89, 55, 69, 112 }, (PokeType.Bug, PokeType.Poison), new string[] { "どくのトゲ", "むしのしらせ", "かそく" }, GenderRatio.M1F1));
            dexData.Add(new Species(546, 262, -1, 168, "モンメン", "", new uint[] { 40, 27, 60, 37, 50, 66 }, (PokeType.Grass, PokeType.Fairy), new string[] { "いたずらごころ", "すりぬけ", "ようりょくそ" }, GenderRatio.M1F1));
            dexData.Add(new Species(547, 263, -1, 169, "エルフーン", "", new uint[] { 60, 67, 85, 77, 75, 116 }, (PokeType.Grass, PokeType.Fairy), new string[] { "いたずらごころ", "すりぬけ", "ようりょくそ" }, GenderRatio.M1F1));
            dexData.Add(new Species(548, -1, 201, -1, "チュリネ", "", new uint[] { 45, 35, 50, 70, 50, 30 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "マイペース", "リーフガード" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(549, -1, 202, -1, "ドレディア", "", new uint[] { 70, 60, 75, 110, 75, 90 }, (PokeType.Grass, PokeType.Non), new string[] { "ようりょくそ", "マイペース", "リーフガード" }, GenderRatio.FemaleOnly));
            dexData.Add(new AnotherForm(550, 154, -1, 64, "バスラオ", "あか", new uint[] { 70, 92, 65, 80, 55, 98 }, (PokeType.Water, PokeType.Non), new string[] { "すてみ", "てきおうりょく", "かたやぶり" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(550, 154, -1, 64, "バスラオ", "あお", new uint[] { 70, 92, 65, 80, 55, 98 }, (PokeType.Water, PokeType.Non), new string[] { "いしあたま", "てきおうりょく", "かたやぶり" }, GenderRatio.M1F1));
            dexData.Add(new Species(551, -1, 176, -1, "メグロコ", "", new uint[] { 50, 72, 35, 35, 35, 65 }, (PokeType.Ground, PokeType.Dark), new string[] { "いかく", "じしんかじょう", "いかりのつぼ" }, GenderRatio.M1F1));
            dexData.Add(new Species(552, -1, 177, -1, "ワルビル", "", new uint[] { 60, 82, 45, 45, 45, 74 }, (PokeType.Ground, PokeType.Dark), new string[] { "いかく", "じしんかじょう", "いかりのつぼ" }, GenderRatio.M1F1));
            dexData.Add(new Species(553, -1, 178, -1, "ワルビアル", "", new uint[] { 95, 117, 80, 65, 70, 92 }, (PokeType.Ground, PokeType.Dark), new string[] { "いかく", "じしんかじょう", "いかりのつぼ" }, GenderRatio.M1F1));
            dexData.Add(new Species(554, 367, -1, 103, "ダルマッカ", "", new uint[] { 70, 90, 45, 15, 45, 50 }, (PokeType.Fire, PokeType.Non), new string[] { "はりきり", "はりきり", "せいしんりょく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(554, 367, -1, 103, "ダルマッカ", "ガラル", new uint[] { 70, 90, 45, 15, 45, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "はりきり", "はりきり", "せいしんりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(555, 368, -1, 104, "ヒヒダルマ", "", new uint[] { 105, 140, 55, 30, 55, 95 }, (PokeType.Fire, PokeType.Non), new string[] { "ちからずく", "ちからずく", "ダルマモード" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(555, 368, -1, 104, "ヒヒダルマ", "ダルマ", new uint[] { 105, 30, 105, 140, 105, 55 }, (PokeType.Fire, PokeType.Psychic), new string[] { "ちからずく", "ちからずく", "ダルマモード" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(555, 368, -1, 104, "ヒヒダルマ", "ガラル", new uint[] { 105, 140, 55, 30, 55, 95 }, (PokeType.Ice, PokeType.Non), new string[] { "ごりむちゅう", "ごりむちゅう", "ダルマモード" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(555, 368, -1, 104, "ヒヒダルマ", "ガラルダルマ", new uint[] { 105, 160, 55, 30, 55, 135 }, (PokeType.Ice, PokeType.Fire), new string[] { "ごりむちゅう", "ごりむちゅう", "ダルマモード" }, GenderRatio.M1F1));
            dexData.Add(new Species(556, 296, -1, -1, "マラカッチ", "", new uint[] { 75, 86, 67, 106, 67, 60 }, (PokeType.Grass, PokeType.Non), new string[] { "ちょすい", "ようりょくそ", "よびみず" }, GenderRatio.M1F1));
            dexData.Add(new Species(557, 86, 122, -1, "イシズマイ", "", new uint[] { 50, 65, 85, 35, 35, 55 }, (PokeType.Bug, PokeType.Rock), new string[] { "がんじょう", "シェルアーマー", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(558, 87, 123, -1, "イワパレス", "", new uint[] { 70, 105, 125, 65, 75, 45 }, (PokeType.Bug, PokeType.Rock), new string[] { "がんじょう", "シェルアーマー", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(559, 224, 161, -1, "ズルッグ", "", new uint[] { 50, 75, 70, 35, 70, 48 }, (PokeType.Dark, PokeType.Fighting), new string[] { "だっぴ", "じしんかじょう", "いかく" }, GenderRatio.M1F1));
            dexData.Add(new Species(560, 225, 162, -1, "ズルズキン", "", new uint[] { 65, 90, 115, 45, 115, 58 }, (PokeType.Dark, PokeType.Fighting), new string[] { "だっぴ", "じしんかじょう", "いかく" }, GenderRatio.M1F1));
            dexData.Add(new Species(561, 297, -1, -1, "シンボラー", "", new uint[] { 72, 58, 80, 103, 80, 97 }, (PokeType.Psychic, PokeType.Flying), new string[] { "ミラクルスキン", "マジックガード", "いろめがね" }, GenderRatio.M1F1));
            dexData.Add(new Species(562, 327, -1, -1, "デスマス", "", new uint[] { 38, 30, 85, 55, 65, 30 }, (PokeType.Ghost, PokeType.Non), new string[] { "ミイラ", "ミイラ", "ミイラ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(562, 327, -1, -1, "デスマス", "ガラル", new uint[] { 38, 55, 85, 30, 65, 30 }, (PokeType.Ground, PokeType.Ghost), new string[] { "さまようたましい", "さまようたましい", "さまようたましい" }, GenderRatio.M1F1));
            dexData.Add(new Species(563, 329, -1, -1, "デスカーン", "", new uint[] { 58, 50, 145, 95, 105, 30 }, (PokeType.Ghost, PokeType.Non), new string[] { "ミイラ", "ミイラ", "ミイラ" }, GenderRatio.M1F1));
            dexData.Add(new Species(564, -1, -1, 147, "プロトーガ", "", new uint[] { 54, 78, 103, 53, 45, 22 }, (PokeType.Water, PokeType.Rock), new string[] { "ハードロック", "がんじょう", "すいすい" }, GenderRatio.M7F1));
            dexData.Add(new Species(565, -1, -1, 148, "アバゴーラ", "", new uint[] { 74, 108, 133, 83, 65, 32 }, (PokeType.Water, PokeType.Rock), new string[] { "ハードロック", "がんじょう", "すいすい" }, GenderRatio.M7F1));
            dexData.Add(new Species(566, -1, -1, 149, "アーケン", "", new uint[] { 55, 112, 45, 74, 45, 70 }, (PokeType.Rock, PokeType.Flying), new string[] { "よわき", "よわき", "よわき" }, GenderRatio.M7F1));
            dexData.Add(new Species(567, -1, -1, 150, "アーケオス", "", new uint[] { 75, 140, 65, 112, 65, 110 }, (PokeType.Rock, PokeType.Flying), new string[] { "よわき", "よわき", "よわき" }, GenderRatio.M7F1));
            dexData.Add(new Species(568, 157, -1, -1, "ヤブクロン", "", new uint[] { 50, 50, 62, 40, 62, 65 }, (PokeType.Poison, PokeType.Non), new string[] { "あくしゅう", "ねんちゃく", "ゆうばく" }, GenderRatio.M1F1));
            dexData.Add(new Species(569, 158, -1, -1, "ダストダス", "", new uint[] { 80, 95, 82, 60, 82, 75 }, (PokeType.Poison, PokeType.Non), new string[] { "あくしゅう", "くだけるよろい", "ゆうばく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(569, 158, -1, -1, "ダストダス", "キョダイ", new uint[] { 80, 95, 82, 60, 82, 75 }, (PokeType.Poison, PokeType.Non), new string[] { "あくしゅう", "くだけるよろい", "ゆうばく" }, GenderRatio.M1F1));
            dexData.Add(new Species(570, -1, 87, -1, "ゾロア", "", new uint[] { 40, 65, 40, 80, 40, 65 }, (PokeType.Dark, PokeType.Non), new string[] { "イリュージョン", "イリュージョン", "イリュージョン" }, GenderRatio.M7F1));
            dexData.Add(new Species(571, -1, 88, -1, "ゾロアーク", "", new uint[] { 60, 105, 60, 120, 60, 105 }, (PokeType.Dark, PokeType.Non), new string[] { "イリュージョン", "イリュージョン", "イリュージョン" }, GenderRatio.M7F1));
            dexData.Add(new Species(572, 50, -1, -1, "チラーミィ", "", new uint[] { 55, 50, 40, 40, 40, 75 }, (PokeType.Normal, PokeType.Non), new string[] { "メロメロボディ", "テクニシャン", "スキルリンク" }, GenderRatio.M1F3));
            dexData.Add(new Species(573, 51, -1, -1, "チラチーノ", "", new uint[] { 75, 95, 60, 65, 60, 115 }, (PokeType.Normal, PokeType.Non), new string[] { "メロメロボディ", "テクニシャン", "スキルリンク" }, GenderRatio.M1F3));
            dexData.Add(new Species(574, 267, -1, 51, "ゴチム", "", new uint[] { 45, 30, 50, 55, 65, 45 }, (PokeType.Psychic, PokeType.Non), new string[] { "おみとおし", "かちき", "かげふみ" }, GenderRatio.M1F3));
            dexData.Add(new Species(575, 268, -1, 52, "ゴチミル", "", new uint[] { 60, 45, 70, 75, 85, 55 }, (PokeType.Psychic, PokeType.Non), new string[] { "おみとおし", "かちき", "かげふみ" }, GenderRatio.M1F3));
            dexData.Add(new Species(576, 269, -1, 53, "ゴチルゼル", "", new uint[] { 70, 55, 95, 95, 110, 65 }, (PokeType.Psychic, PokeType.Non), new string[] { "おみとおし", "かちき", "かげふみ" }, GenderRatio.M1F3));
            dexData.Add(new Species(577, 270, -1, 54, "ユニラン", "", new uint[] { 45, 30, 40, 105, 50, 20 }, (PokeType.Psychic, PokeType.Non), new string[] { "ぼうじん", "マジックガード", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(578, 271, -1, 55, "ダブラン", "", new uint[] { 65, 40, 50, 125, 60, 30 }, (PokeType.Psychic, PokeType.Non), new string[] { "ぼうじん", "マジックガード", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(579, 272, -1, 56, "ランクルス", "", new uint[] { 110, 65, 75, 125, 85, 30 }, (PokeType.Psychic, PokeType.Non), new string[] { "ぼうじん", "マジックガード", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(580, -1, -1, -1, "コアルヒー", "", new uint[] { 62, 44, 50, 44, 50, 55 }, (PokeType.Water, PokeType.Flying), new string[] { "するどいめ", "はとむね", "うるおいボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(581, -1, -1, -1, "スワンナ", "", new uint[] { 75, 87, 63, 87, 63, 98 }, (PokeType.Water, PokeType.Flying), new string[] { "するどいめ", "はとむね", "うるおいボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(582, 72, -1, 22, "バニプッチ", "", new uint[] { 36, 50, 50, 65, 60, 44 }, (PokeType.Ice, PokeType.Non), new string[] { "アイスボディ", "ゆきがくれ", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(583, 73, -1, 23, "バニリッチ", "", new uint[] { 51, 65, 65, 80, 75, 59 }, (PokeType.Ice, PokeType.Non), new string[] { "アイスボディ", "ゆきがくれ", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(584, 74, -1, 24, "バイバニラ", "", new uint[] { 71, 95, 85, 110, 95, 79 }, (PokeType.Ice, PokeType.Non), new string[] { "アイスボディ", "ゆきふらし", "くだけるよろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(585, -1, -1, -1, "シキジカ", "", new uint[] { 60, 60, 50, 40, 50, 75 }, (PokeType.Normal, PokeType.Grass), new string[] { "ようりょくそ", "そうしょく", "てんのめぐみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(586, -1, -1, -1, "メブキジカ", "", new uint[] { 80, 100, 70, 60, 70, 95 }, (PokeType.Normal, PokeType.Grass), new string[] { "ようりょくそ", "そうしょく", "てんのめぐみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(587, -1, 102, -1, "エモンガ", "", new uint[] { 55, 75, 60, 75, 60, 103 }, (PokeType.Electric, PokeType.Flying), new string[] { "せいでんき", "せいでんき", "でんきエンジン" }, GenderRatio.M1F1));
            dexData.Add(new Species(588, 273, 66, 95, "カブルモ", "", new uint[] { 50, 75, 45, 40, 45, 60 }, (PokeType.Bug, PokeType.Non), new string[] { "むしのしらせ", "だっぴ", "ノーガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(589, 274, 67, 96, "シュバルゴ", "", new uint[] { 70, 135, 105, 60, 105, 20 }, (PokeType.Bug, PokeType.Steel), new string[] { "むしのしらせ", "シェルアーマー", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(590, -1, 77, -1, "タマゲタケ", "", new uint[] { 69, 55, 45, 55, 55, 15 }, (PokeType.Grass, PokeType.Poison), new string[] { "ほうし", "ほうし", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(591, -1, 78, -1, "モロバレル", "", new uint[] { 114, 85, 70, 85, 80, 30 }, (PokeType.Grass, PokeType.Poison), new string[] { "ほうし", "ほうし", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(592, 305, 192, -1, "プルリル", "", new uint[] { 55, 40, 50, 65, 85, 40 }, (PokeType.Water, PokeType.Ghost), new string[] { "ちょすい", "のろわれボディ", "しめりけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(593, 306, 193, -1, "ブルンゲル", "", new uint[] { 100, 60, 70, 85, 105, 60 }, (PokeType.Water, PokeType.Ghost), new string[] { "ちょすい", "のろわれボディ", "しめりけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(594, -1, -1, -1, "ママンボウ", "", new uint[] { 165, 75, 80, 40, 45, 65 }, (PokeType.Water, PokeType.Non), new string[] { "いやしのこころ", "うるおいボディ", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(595, 64, -1, 93, "バチュル", "", new uint[] { 50, 47, 50, 57, 50, 65 }, (PokeType.Bug, PokeType.Electric), new string[] { "ふくがん", "きんちょうかん", "むしのしらせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(596, 65, -1, 94, "デンチュラ", "", new uint[] { 70, 77, 60, 97, 60, 108 }, (PokeType.Bug, PokeType.Electric), new string[] { "ふくがん", "きんちょうかん", "むしのしらせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(597, 189, -1, 179, "テッシード", "", new uint[] { 44, 50, 91, 24, 86, 10 }, (PokeType.Grass, PokeType.Steel), new string[] { "てつのトゲ", "てつのトゲ", "てつのトゲ" }, GenderRatio.M1F1));
            dexData.Add(new Species(598, 190, -1, 180, "ナットレイ", "", new uint[] { 74, 94, 131, 54, 116, 20 }, (PokeType.Grass, PokeType.Steel), new string[] { "てつのトゲ", "てつのトゲ", "きけんよち" }, GenderRatio.M1F1));
            dexData.Add(new Species(599, 113, -1, -1, "ギアル", "", new uint[] { 40, 55, 70, 45, 60, 30 }, (PokeType.Steel, PokeType.Non), new string[] { "プラス", "マイナス", "クリアボディ" }, GenderRatio.Genderless));
            dexData.Add(new Species(600, 114, -1, -1, "ギギアル", "", new uint[] { 60, 80, 95, 70, 85, 50 }, (PokeType.Steel, PokeType.Non), new string[] { "プラス", "マイナス", "クリアボディ" }, GenderRatio.Genderless));
            dexData.Add(new Species(601, 115, -1, -1, "ギギギアル", "", new uint[] { 60, 100, 115, 70, 85, 90 }, (PokeType.Steel, PokeType.Non), new string[] { "プラス", "マイナス", "クリアボディ" }, GenderRatio.Genderless));
            dexData.Add(new Species(602, -1, -1, -1, "シビシラス", "", new uint[] { 35, 55, 40, 45, 40, 60 }, (PokeType.Electric, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(603, -1, -1, -1, "シビビール", "", new uint[] { 65, 85, 70, 75, 70, 40 }, (PokeType.Electric, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(604, -1, -1, -1, "シビルドン", "", new uint[] { 85, 115, 80, 105, 80, 50 }, (PokeType.Electric, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(605, 277, -1, -1, "リグレー", "", new uint[] { 55, 55, 55, 85, 55, 30 }, (PokeType.Psychic, PokeType.Non), new string[] { "テレパシー", "シンクロ", "アナライズ" }, GenderRatio.M1F1));
            dexData.Add(new Species(606, 278, -1, -1, "オーベム", "", new uint[] { 75, 75, 75, 125, 95, 40 }, (PokeType.Psychic, PokeType.Non), new string[] { "テレパシー", "シンクロ", "アナライズ" }, GenderRatio.M1F1));
            dexData.Add(new Species(607, 287, -1, 48, "ヒトモシ", "", new uint[] { 50, 30, 55, 65, 55, 20 }, (PokeType.Ghost, PokeType.Fire), new string[] { "もらいび", "ほのおのからだ", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(608, 288, -1, 49, "ランプラー", "", new uint[] { 60, 40, 60, 95, 60, 55 }, (PokeType.Ghost, PokeType.Fire), new string[] { "もらいび", "ほのおのからだ", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(609, 289, -1, 50, "シャンデラ", "", new uint[] { 60, 55, 90, 145, 90, 80 }, (PokeType.Ghost, PokeType.Fire), new string[] { "もらいび", "ほのおのからだ", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(610, 324, -1, -1, "キバゴ", "", new uint[] { 46, 87, 60, 30, 40, 57 }, (PokeType.Dragon, PokeType.Non), new string[] { "とうそうしん", "かたやぶり", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(611, 325, -1, -1, "オノンド", "", new uint[] { 66, 117, 70, 40, 50, 67 }, (PokeType.Dragon, PokeType.Non), new string[] { "とうそうしん", "かたやぶり", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(612, 326, -1, -1, "オノノクス", "", new uint[] { 76, 147, 90, 60, 70, 97 }, (PokeType.Dragon, PokeType.Non), new string[] { "とうそうしん", "かたやぶり", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(613, 279, -1, 121, "クマシュン", "", new uint[] { 55, 70, 40, 60, 40, 40 }, (PokeType.Ice, PokeType.Non), new string[] { "ゆきがくれ", "ゆきかき", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(614, 280, -1, 122, "ツンベアー", "", new uint[] { 95, 130, 80, 70, 80, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "ゆきがくれ", "ゆきかき", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(615, -1, -1, 30, "フリージオ", "", new uint[] { 80, 50, 50, 95, 135, 105 }, (PokeType.Ice, PokeType.Non), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.Genderless));
            dexData.Add(new Species(616, 275, 64, 97, "チョボマキ", "", new uint[] { 50, 40, 85, 40, 65, 25 }, (PokeType.Bug, PokeType.Non), new string[] { "うるおいボディ", "シェルアーマー", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(617, 276, 65, 98, "アギルダー", "", new uint[] { 80, 70, 40, 100, 60, 145 }, (PokeType.Bug, PokeType.Non), new string[] { "うるおいボディ", "ねんちゃく", "かるわざ" }, GenderRatio.M1F1));
            dexData.Add(new Species(618, 226, -1, -1, "マッギョ", "", new uint[] { 109, 66, 84, 81, 99, 32 }, (PokeType.Ground, PokeType.Electric), new string[] { "せいでんき", "じゅうなん", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(618, 226, -1, -1, "マッギョ", "ガラル", new uint[] { 109, 81, 99, 66, 84, 32 }, (PokeType.Ground, PokeType.Steel), new string[] { "ぎたい", "ぎたい", "ぎたい" }, GenderRatio.M1F1));
            dexData.Add(new Species(619, -1, 163, -1, "コジョフー", "", new uint[] { 45, 85, 50, 55, 50, 65 }, (PokeType.Fighting, PokeType.Non), new string[] { "せいしんりょく", "さいせいりょく", "すてみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(620, -1, 164, -1, "コジョンド", "", new uint[] { 65, 125, 60, 95, 60, 105 }, (PokeType.Fighting, PokeType.Non), new string[] { "せいしんりょく", "さいせいりょく", "すてみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(621, -1, 63, 119, "クリムガン", "", new uint[] { 77, 120, 90, 60, 90, 48 }, (PokeType.Dragon, PokeType.Non), new string[] { "さめはだ", "ちからずく", "かたやぶり" }, GenderRatio.M1F1));
            dexData.Add(new Species(622, 88, -1, 153, "ゴビット", "", new uint[] { 59, 74, 50, 35, 50, 35 }, (PokeType.Ground, PokeType.Ghost), new string[] { "てつのこぶし", "ぶきよう", "ノーガード" }, GenderRatio.Genderless));
            dexData.Add(new Species(623, 89, -1, 154, "ゴルーグ", "", new uint[] { 89, 124, 80, 55, 80, 55 }, (PokeType.Ground, PokeType.Ghost), new string[] { "てつのこぶし", "ぶきよう", "ノーガード" }, GenderRatio.Genderless));
            dexData.Add(new Species(624, 246, 29, -1, "コマタナ", "", new uint[] { 45, 85, 70, 40, 40, 60 }, (PokeType.Dark, PokeType.Steel), new string[] { "まけんき", "せいしんりょく", "プレッシャー" }, GenderRatio.M1F1));
            dexData.Add(new Species(625, 247, 30, -1, "キリキザン", "", new uint[] { 65, 125, 100, 60, 70, 70 }, (PokeType.Dark, PokeType.Steel), new string[] { "まけんき", "せいしんりょく", "プレッシャー" }, GenderRatio.M1F1));
            dexData.Add(new Species(626, -1, 53, -1, "バッフロン", "", new uint[] { 95, 110, 95, 40, 95, 55 }, (PokeType.Normal, PokeType.Non), new string[] { "すてみ", "そうしょく", "ぼうおん" }, GenderRatio.M1F1));
            dexData.Add(new Species(627, 281, 179, -1, "ワシボン", "", new uint[] { 70, 83, 50, 37, 50, 60 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "ちからずく", "はりきり" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(628, 282, 180, -1, "ウォーグル", "", new uint[] { 100, 123, 75, 57, 75, 80 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "ちからずく", "まけんき" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(629, 283, 181, -1, "バルチャイ", "", new uint[] { 70, 55, 75, 45, 65, 60 }, (PokeType.Dark, PokeType.Flying), new string[] { "はとむね", "ぼうじん", "くだけるよろい" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(630, 284, 182, -1, "バルジーナ", "", new uint[] { 110, 65, 105, 55, 95, 80 }, (PokeType.Dark, PokeType.Flying), new string[] { "はとむね", "ぼうじん", "くだけるよろい" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(631, 317, -1, 102, "クイタラン", "", new uint[] { 85, 97, 66, 105, 66, 65 }, (PokeType.Fire, PokeType.Non), new string[] { "くいしんぼう", "もらいび", "しろいけむり" }, GenderRatio.M1F1));
            dexData.Add(new Species(632, 316, -1, 101, "アイアント", "", new uint[] { 58, 109, 112, 48, 48, 109 }, (PokeType.Bug, PokeType.Steel), new string[] { "むしのしらせ", "はりきり", "なまけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(633, 386, -1, 136, "モノズ", "", new uint[] { 52, 65, 50, 45, 50, 38 }, (PokeType.Dark, PokeType.Dragon), new string[] { "はりきり", "はりきり", "はりきり" }, GenderRatio.M1F1));
            dexData.Add(new Species(634, 387, -1, 137, "ジヘッド", "", new uint[] { 72, 85, 70, 65, 70, 58 }, (PokeType.Dark, PokeType.Dragon), new string[] { "はりきり", "はりきり", "はりきり" }, GenderRatio.M1F1));
            dexData.Add(new Species(635, 388, -1, 138, "サザンドラ", "", new uint[] { 92, 105, 90, 125, 90, 98 }, (PokeType.Dark, PokeType.Dragon), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(636, -1, 186, -1, "メラルバ", "", new uint[] { 55, 85, 55, 50, 55, 60 }, (PokeType.Bug, PokeType.Fire), new string[] { "ほのおのからだ", "ほのおのからだ", "むしのしらせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(637, -1, 187, -1, "ウルガモス", "", new uint[] { 85, 60, 65, 135, 105, 100 }, (PokeType.Bug, PokeType.Fire), new string[] { "ほのおのからだ", "ほのおのからだ", "むしのしらせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(638, -1, -1, 205, "コバルオン", "", new uint[] { 91, 90, 129, 90, 72, 108 }, (PokeType.Steel, PokeType.Fighting), new string[] { "せいぎのこころ", "せいぎのこころ", "せいぎのこころ" }, GenderRatio.Genderless));
            dexData.Add(new Species(639, -1, -1, 206, "テラキオン", "", new uint[] { 91, 129, 90, 72, 90, 108 }, (PokeType.Rock, PokeType.Fighting), new string[] { "せいぎのこころ", "せいぎのこころ", "せいぎのこころ" }, GenderRatio.Genderless));
            dexData.Add(new Species(640, -1, -1, 207, "ビリジオン", "", new uint[] { 91, 90, 72, 90, 129, 108 }, (PokeType.Grass, PokeType.Fighting), new string[] { "せいぎのこころ", "せいぎのこころ", "せいぎのこころ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(641, -1, -1, -1, "トルネロス", "化身", new uint[] { 79, 115, 70, 125, 80, 111 }, (PokeType.Flying, PokeType.Non), new string[] { "いたずらごころ", "いたずらごころ", "まけんき" }, GenderRatio.MaleOnly));
            dexData.Add(new AnotherForm(641, -1, -1, -1, "トルネロス", "霊獣", new uint[] { 79, 100, 80, 110, 90, 121 }, (PokeType.Flying, PokeType.Non), new string[] { "さいせいりょく", "さいせいりょく", "さいせいりょく" }, GenderRatio.MaleOnly));
            dexData.Add(new AnotherForm(642, -1, -1, -1, "ボルトロス", "化身", new uint[] { 79, 115, 70, 125, 80, 111 }, (PokeType.Electric, PokeType.Flying), new string[] { "いたずらごころ", "いたずらごころ", "まけんき" }, GenderRatio.MaleOnly));
            dexData.Add(new AnotherForm(642, -1, -1, -1, "ボルトロス", "霊獣", new uint[] { 79, 105, 70, 145, 80, 101 }, (PokeType.Electric, PokeType.Flying), new string[] { "ちくでん", "ちくでん", "ちくでん" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(643, -1, -1, -1, "レシラム", "", new uint[] { 100, 120, 100, 150, 120, 90 }, (PokeType.Dragon, PokeType.Fire), new string[] { "ターボブレイズ", "ターボブレイズ", "ターボブレイズ" }, GenderRatio.Genderless));
            dexData.Add(new Species(644, -1, -1, -1, "ゼクロム", "", new uint[] { 100, 150, 120, 120, 100, 90 }, (PokeType.Dragon, PokeType.Electric), new string[] { "テラボルテージ", "テラボルテージ", "テラボルテージ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(645, -1, -1, -1, "ランドロス", "化身", new uint[] { 89, 125, 90, 115, 80, 101 }, (PokeType.Ground, PokeType.Flying), new string[] { "すなのちから", "すなのちから", "ちからずく" }, GenderRatio.MaleOnly));
            dexData.Add(new AnotherForm(645, -1, -1, -1, "ランドロス", "霊獣", new uint[] { 89, 145, 90, 105, 80, 91 }, (PokeType.Ground, PokeType.Flying), new string[] { "いかく", "いかく", "いかく" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(646, -1, -1, -1, "キュレム", "", new uint[] { 125, 130, 90, 130, 90, 95 }, (PokeType.Dragon, PokeType.Ice), new string[] { "プレッシャー", "プレッシャー", "プレッシャー" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(646, -1, -1, -1, "キュレム", "ブラック", new uint[] { 125, 120, 90, 170, 100, 95 }, (PokeType.Dragon, PokeType.Ice), new string[] { "ターボブレイズ", "ターボブレイズ", "ターボブレイズ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(646, -1, -1, -1, "キュレム", "ホワイト", new uint[] { 125, 170, 100, 120, 90, 95 }, (PokeType.Dragon, PokeType.Ice), new string[] { "テラボルテージ", "テラボルテージ", "テラボルテージ" }, GenderRatio.Genderless));
            dexData.Add(new Species(647, -1, -1, -1, "ケルディオ", "", new uint[] { 91, 72, 90, 129, 90, 108 }, (PokeType.Water, PokeType.Fighting), new string[] { "せいぎのこころ", "せいぎのこころ", "せいぎのこころ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(648, -1, -1, -1, "メロエッタ", "ボイス", new uint[] { 100, 77, 77, 128, 128, 90 }, (PokeType.Normal, PokeType.Psychic), new string[] { "てんのめぐみ", "てんのめぐみ", "てんのめぐみ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(648, -1, -1, -1, "メロエッタ", "ステップ", new uint[] { 100, 128, 90, 77, 77, 128 }, (PokeType.Normal, PokeType.Fighting), new string[] { "てんのめぐみ", "てんのめぐみ", "てんのめぐみ" }, GenderRatio.Genderless));
            dexData.Add(new Species(649, -1, -1, -1, "ゲノセクト", "", new uint[] { 71, 120, 95, 120, 95, 99 }, (PokeType.Bug, PokeType.Steel), new string[] { "ダウンロード", "ダウンロード", "ダウンロード" }, GenderRatio.Genderless));
            dexData.Add(new Species(650, -1, -1, -1, "ハリマロン", "", new uint[] { 56, 61, 65, 48, 45, 38 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "ぼうだん" }, GenderRatio.M7F1));
            dexData.Add(new Species(651, -1, -1, -1, "ハリボーグ", "", new uint[] { 61, 78, 95, 56, 58, 57 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "ぼうだん" }, GenderRatio.M7F1));
            dexData.Add(new Species(652, -1, -1, -1, "ブリガロン", "", new uint[] { 88, 107, 122, 74, 75, 64 }, (PokeType.Grass, PokeType.Fighting), new string[] { "しんりょく", "しんりょく", "ぼうだん" }, GenderRatio.M7F1));
            dexData.Add(new Species(653, -1, -1, -1, "フォッコ", "", new uint[] { 40, 45, 40, 62, 60, 60 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "マジシャン" }, GenderRatio.M7F1));
            dexData.Add(new Species(654, -1, -1, -1, "テールナー", "", new uint[] { 59, 59, 58, 90, 70, 73 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "マジシャン" }, GenderRatio.M7F1));
            dexData.Add(new Species(655, -1, -1, -1, "マフォクシー", "", new uint[] { 75, 69, 72, 114, 100, 104 }, (PokeType.Fire, PokeType.Psychic), new string[] { "もうか", "もうか", "マジシャン" }, GenderRatio.M7F1));
            dexData.Add(new Species(656, -1, -1, -1, "ケロマツ", "", new uint[] { 41, 56, 40, 62, 44, 71 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "へんげんじざい" }, GenderRatio.M7F1));
            dexData.Add(new Species(657, -1, -1, -1, "ゲコガシラ", "", new uint[] { 54, 63, 52, 83, 56, 97 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "へんげんじざい" }, GenderRatio.M7F1));
            dexData.Add(new Species(658, -1, -1, -1, "ゲッコウガ", "", new uint[] { 72, 95, 67, 103, 71, 122 }, (PokeType.Water, PokeType.Dark), new string[] { "げきりゅう", "げきりゅう", "へんげんじざい" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(658, -1, -1, -1, "ゲッコウガ", "サトシ", new uint[] { 72, 145, 67, 153, 71, 132 }, (PokeType.Water, PokeType.Dark), new string[] { "きずなへんげ", "きずなへんげ", "きずなへんげ" }, GenderRatio.Genderless));
            dexData.Add(new Species(659, 48, -1, -1, "ホルビー", "", new uint[] { 38, 36, 38, 32, 36, 57 }, (PokeType.Normal, PokeType.Non), new string[] { "ものひろい", "ほおぶくろ", "ちからもち" }, GenderRatio.M1F1));
            dexData.Add(new Species(660, 49, -1, -1, "ホルード", "", new uint[] { 85, 56, 77, 50, 77, 78 }, (PokeType.Normal, PokeType.Ground), new string[] { "ものひろい", "ほおぶくろ", "ちからもち" }, GenderRatio.M1F1));
            dexData.Add(new Species(661, -1, 22, -1, "ヤヤコマ", "", new uint[] { 45, 50, 43, 40, 38, 62 }, (PokeType.Normal, PokeType.Flying), new string[] { "はとむね", "はとむね", "はやてのつばさ" }, GenderRatio.M1F1));
            dexData.Add(new Species(662, -1, 23, -1, "ヒノヤコマ", "", new uint[] { 62, 73, 55, 56, 52, 84 }, (PokeType.Fire, PokeType.Flying), new string[] { "ほのおのからだ", "ほのおのからだ", "はやてのつばさ" }, GenderRatio.M1F1));
            dexData.Add(new Species(663, -1, 24, -1, "ファイアロー", "", new uint[] { 78, 81, 71, 74, 69, 126 }, (PokeType.Fire, PokeType.Flying), new string[] { "ほのおのからだ", "ほのおのからだ", "はやてのつばさ" }, GenderRatio.M1F1));
            dexData.Add(new Species(664, -1, -1, -1, "コフキムシ", "", new uint[] { 38, 35, 40, 27, 25, 35 }, (PokeType.Bug, PokeType.Non), new string[] { "りんぷん", "ふくがん", "フレンドガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(665, -1, -1, -1, "コフーライ", "", new uint[] { 45, 22, 60, 27, 30, 29 }, (PokeType.Bug, PokeType.Non), new string[] { "だっぴ", "だっぴ", "フレンドガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(666, -1, -1, -1, "ビビヨン", "", new uint[] { 80, 52, 50, 90, 50, 89 }, (PokeType.Bug, PokeType.Flying), new string[] { "りんぷん", "ふくがん", "フレンドガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(667, -1, -1, -1, "シシコ", "", new uint[] { 62, 50, 58, 73, 54, 72 }, (PokeType.Fire, PokeType.Normal), new string[] { "とうそうしん", "きんちょうかん", "じしんかじょう" }, GenderRatio.M1F7));
            dexData.Add(new Species(668, -1, -1, -1, "カエンジシ", "", new uint[] { 86, 68, 72, 109, 66, 106 }, (PokeType.Fire, PokeType.Normal), new string[] { "とうそうしん", "きんちょうかん", "じしんかじょう" }, GenderRatio.M1F7));
            dexData.Add(new Species(669, -1, -1, -1, "フラベベ", "", new uint[] { 44, 38, 39, 61, 79, 42 }, (PokeType.Fairy, PokeType.Non), new string[] { "フラワーベール", "フラワーベール", "きょうせい" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(670, -1, -1, -1, "フラエッテ", "", new uint[] { 54, 45, 47, 75, 98, 52 }, (PokeType.Fairy, PokeType.Non), new string[] { "フラワーベール", "フラワーベール", "きょうせい" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(671, -1, -1, -1, "フラージェス", "", new uint[] { 78, 65, 68, 112, 154, 75 }, (PokeType.Fairy, PokeType.Non), new string[] { "フラワーベール", "フラワーベール", "きょうせい" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(672, -1, -1, -1, "メェークル", "", new uint[] { 66, 65, 48, 62, 57, 52 }, (PokeType.Grass, PokeType.Non), new string[] { "そうしょく", "そうしょく", "くさのけがわ" }, GenderRatio.M1F1));
            dexData.Add(new Species(673, -1, -1, -1, "ゴーゴート", "", new uint[] { 123, 100, 62, 97, 81, 68 }, (PokeType.Grass, PokeType.Non), new string[] { "そうしょく", "そうしょく", "くさのけがわ" }, GenderRatio.M1F1));
            dexData.Add(new Species(674, 111, -1, -1, "ヤンチャム", "", new uint[] { 67, 82, 62, 46, 48, 43 }, (PokeType.Fighting, PokeType.Non), new string[] { "てつのこぶし", "かたやぶり", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(675, 112, -1, -1, "ゴロンダ", "", new uint[] { 95, 124, 78, 69, 71, 58 }, (PokeType.Fighting, PokeType.Dark), new string[] { "てつのこぶし", "かたやぶり", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(676, -1, -1, -1, "トリミアン", "", new uint[] { 75, 80, 60, 65, 90, 102 }, (PokeType.Normal, PokeType.Non), new string[] { "ファーコート", "ファーコート", "ファーコート" }, GenderRatio.M1F1));
            dexData.Add(new Species(677, 208, -1, -1, "ニャスパー", "", new uint[] { 62, 48, 54, 63, 60, 68 }, (PokeType.Psychic, PokeType.Non), new string[] { "するどいめ", "すりぬけ", "マイペース" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(678, 209, -1, -1, "ニャオニクス", "♂", new uint[] { 74, 48, 76, 83, 81, 104 }, (PokeType.Psychic, PokeType.Non), new string[] { "するどいめ", "すりぬけ", "いたずらごころ" }, GenderRatio.MaleOnly));
            dexData.Add(new AnotherForm(678, 209, -1, -1, "ニャオニクス", "♀", new uint[] { 74, 48, 76, 83, 81, 104 }, (PokeType.Psychic, PokeType.Non), new string[] { "するどいめ", "すりぬけ", "かちき" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(679, 330, -1, -1, "ヒトツキ", "", new uint[] { 45, 80, 100, 35, 37, 28 }, (PokeType.Steel, PokeType.Ghost), new string[] { "ノーガード", "ノーガード", "ノーガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(680, 331, -1, -1, "ニダンギル", "", new uint[] { 59, 110, 150, 45, 49, 35 }, (PokeType.Steel, PokeType.Ghost), new string[] { "ノーガード", "ノーガード", "ノーガード" }, GenderRatio.M1F1));
            dexData.Add(new Species(681, 332, -1, -1, "ギルガルド", "シールド", new uint[] { 60, 50, 140, 50, 140, 60 }, (PokeType.Steel, PokeType.Ghost), new string[] { "バトルスイッチ", "バトルスイッチ", "バトルスイッチ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(681, 332, -1, -1, "ギルガルド", "ブレード", new uint[] { 60, 140, 50, 140, 50, 60 }, (PokeType.Steel, PokeType.Ghost), new string[] { "バトルスイッチ", "バトルスイッチ", "バトルスイッチ" }, GenderRatio.M1F1));
            dexData.Add(new Species(682, 212, -1, -1, "シュシュプ", "", new uint[] { 78, 52, 60, 63, 65, 23 }, (PokeType.Fairy, PokeType.Non), new string[] { "いやしのこころ", "いやしのこころ", "アロマベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(683, 213, -1, -1, "フレフワン", "", new uint[] { 101, 72, 72, 99, 89, 29 }, (PokeType.Fairy, PokeType.Non), new string[] { "いやしのこころ", "いやしのこころ", "アロマベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(684, 210, -1, -1, "ペロッパフ", "", new uint[] { 62, 48, 66, 59, 57, 49 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "かるわざ" }, GenderRatio.M1F1));
            dexData.Add(new Species(685, 211, -1, -1, "ペロリーム", "", new uint[] { 82, 80, 86, 85, 75, 72 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "かるわざ" }, GenderRatio.M1F1));
            dexData.Add(new Species(686, 290, 108, -1, "マーイーカ", "", new uint[] { 53, 54, 53, 37, 46, 45 }, (PokeType.Dark, PokeType.Psychic), new string[] { "あまのじゃく", "きゅうばん", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(687, 291, 109, -1, "カラマネロ", "", new uint[] { 86, 92, 88, 68, 75, 73 }, (PokeType.Dark, PokeType.Psychic), new string[] { "あまのじゃく", "きゅうばん", "すりぬけ" }, GenderRatio.M1F1));
            dexData.Add(new Species(688, 234, -1, -1, "カメテテ", "", new uint[] { 42, 52, 67, 39, 56, 50 }, (PokeType.Rock, PokeType.Water), new string[] { "かたいツメ", "スナイパー", "わるいてぐせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(689, 235, -1, -1, "ガメノデス", "", new uint[] { 72, 105, 115, 54, 86, 68 }, (PokeType.Rock, PokeType.Water), new string[] { "かたいツメ", "スナイパー", "わるいてぐせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(690, -1, 194, -1, "クズモー", "", new uint[] { 50, 60, 60, 60, 60, 30 }, (PokeType.Poison, PokeType.Water), new string[] { "どくのトゲ", "どくしゅ", "てきおうりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(691, -1, 195, -1, "ドラミドロ", "", new uint[] { 65, 75, 90, 97, 123, 44 }, (PokeType.Poison, PokeType.Dragon), new string[] { "どくのトゲ", "どくしゅ", "てきおうりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(692, -1, 196, -1, "ウデッポウ", "", new uint[] { 50, 53, 62, 58, 63, 44 }, (PokeType.Water, PokeType.Non), new string[] { "メガランチャー", "メガランチャー", "メガランチャー" }, GenderRatio.M1F1));
            dexData.Add(new Species(693, -1, 197, -1, "ブロスター", "", new uint[] { 71, 73, 88, 120, 89, 59 }, (PokeType.Water, PokeType.Non), new string[] { "メガランチャー", "メガランチャー", "メガランチャー" }, GenderRatio.M1F1));
            dexData.Add(new Species(694, 318, -1, -1, "エリキテル", "", new uint[] { 44, 38, 33, 61, 43, 70 }, (PokeType.Electric, PokeType.Normal), new string[] { "かんそうはだ", "すながくれ", "サンパワー" }, GenderRatio.M1F1));
            dexData.Add(new Species(695, 319, -1, -1, "エレザード", "", new uint[] { 62, 55, 52, 109, 94, 109 }, (PokeType.Electric, PokeType.Normal), new string[] { "かんそうはだ", "すながくれ", "サンパワー" }, GenderRatio.M1F1));
            dexData.Add(new Species(696, -1, -1, 83, "チゴラス", "", new uint[] { 58, 89, 77, 45, 45, 48 }, (PokeType.Rock, PokeType.Dragon), new string[] { "がんじょうあご", "がんじょうあご", "がんじょう" }, GenderRatio.M7F1));
            dexData.Add(new Species(697, -1, -1, 84, "ガチゴラス", "", new uint[] { 82, 121, 119, 69, 59, 71 }, (PokeType.Rock, PokeType.Dragon), new string[] { "がんじょうあご", "がんじょうあご", "いしあたま" }, GenderRatio.M7F1));
            dexData.Add(new Species(698, -1, -1, 85, "アマルス", "", new uint[] { 77, 59, 50, 67, 63, 46 }, (PokeType.Rock, PokeType.Ice), new string[] { "フリーズスキン", "フリーズスキン", "ゆきふらし" }, GenderRatio.M7F1));
            dexData.Add(new Species(699, -1, -1, 86, "アマルルガ", "", new uint[] { 123, 77, 72, 99, 92, 58 }, (PokeType.Rock, PokeType.Ice), new string[] { "フリーズスキン", "フリーズスキン", "ゆきふらし" }, GenderRatio.M7F1));
            dexData.Add(new Species(700, 204, -1, 82, "ニンフィア", "", new uint[] { 95, 65, 65, 110, 130, 60 }, (PokeType.Fairy, PokeType.Non), new string[] { "メロメロボディ", "メロメロボディ", "フェアリースキン" }, GenderRatio.M7F1));
            dexData.Add(new Species(701, 320, -1, -1, "ルチャブル", "", new uint[] { 78, 92, 75, 74, 63, 118 }, (PokeType.Fighting, PokeType.Flying), new string[] { "じゅうなん", "かるわざ", "かたやぶり" }, GenderRatio.M1F1));
            dexData.Add(new Species(702, -1, 103, -1, "デデンネ", "", new uint[] { 67, 58, 57, 81, 67, 101 }, (PokeType.Electric, PokeType.Fairy), new string[] { "ほおぶくろ", "ものひろい", "プラス" }, GenderRatio.M1F1));
            dexData.Add(new Species(703, -1, -1, 128, "メレシー", "", new uint[] { 50, 50, 150, 50, 150, 50 }, (PokeType.Rock, PokeType.Fairy), new string[] { "クリアボディ", "クリアボディ", "がんじょう" }, GenderRatio.Genderless));
            dexData.Add(new Species(704, 389, 60, -1, "ヌメラ", "", new uint[] { 45, 50, 35, 55, 75, 40 }, (PokeType.Dragon, PokeType.Non), new string[] { "そうしょく", "うるおいボディ", "ぬめぬめ" }, GenderRatio.M1F1));
            dexData.Add(new Species(705, 390, 61, -1, "ヌメイル", "", new uint[] { 68, 75, 53, 83, 113, 60 }, (PokeType.Dragon, PokeType.Non), new string[] { "そうしょく", "うるおいボディ", "ぬめぬめ" }, GenderRatio.M1F1));
            dexData.Add(new Species(706, 391, 62, -1, "ヌメルゴン", "", new uint[] { 90, 100, 70, 110, 150, 80 }, (PokeType.Dragon, PokeType.Non), new string[] { "そうしょく", "うるおいボディ", "ぬめぬめ" }, GenderRatio.M1F1));
            dexData.Add(new Species(707, -1, 28, -1, "クレッフィ", "", new uint[] { 57, 80, 91, 80, 87, 75 }, (PokeType.Steel, PokeType.Fairy), new string[] { "いたずらごころ", "いたずらごころ", "マジシャン" }, GenderRatio.M1F1));
            dexData.Add(new Species(708, 338, -1, 33, "ボクレー", "", new uint[] { 43, 70, 48, 50, 60, 38 }, (PokeType.Ghost, PokeType.Grass), new string[] { "しぜんかいふく", "おみとおし", "しゅうかく" }, GenderRatio.M1F1));
            dexData.Add(new Species(709, 339, -1, 34, "オーロット", "", new uint[] { 85, 110, 76, 65, 82, 56 }, (PokeType.Ghost, PokeType.Grass), new string[] { "しぜんかいふく", "おみとおし", "しゅうかく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(710, 191, -1, -1, "バケッチャ", "普通", new uint[] { 49, 66, 70, 44, 55, 51 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(710, 191, -1, -1, "バケッチャ", "小", new uint[] { 44, 66, 70, 44, 55, 56 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(710, 191, -1, -1, "バケッチャ", "大", new uint[] { 54, 66, 70, 44, 55, 46 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(710, 191, -1, -1, "バケッチャ", "特大", new uint[] { 59, 66, 70, 44, 55, 41 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(711, 192, -1, -1, "パンプジン", "普通", new uint[] { 65, 90, 122, 58, 75, 84 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(711, 192, -1, -1, "パンプジン", "小", new uint[] { 55, 85, 122, 58, 75, 99 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(711, 192, -1, -1, "パンプジン", "大", new uint[] { 75, 95, 122, 58, 75, 69 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(711, 192, -1, -1, "パンプジン", "特大", new uint[] { 85, 100, 122, 58, 75, 54 }, (PokeType.Ghost, PokeType.Grass), new string[] { "ものひろい", "おみとおし", "ふみん" }, GenderRatio.M1F1));
            dexData.Add(new Species(712, 358, -1, 142, "カチコール", "", new uint[] { 55, 69, 85, 32, 35, 28 }, (PokeType.Ice, PokeType.Non), new string[] { "マイペース", "アイスボディ", "がんじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(713, 359, -1, 143, "クレベース", "", new uint[] { 95, 117, 184, 44, 46, 28 }, (PokeType.Ice, PokeType.Non), new string[] { "マイペース", "アイスボディ", "がんじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(714, 176, -1, 181, "オンバット", "", new uint[] { 40, 30, 35, 45, 40, 55 }, (PokeType.Flying, PokeType.Dragon), new string[] { "おみとおし", "すりぬけ", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(715, 177, -1, 182, "オンバーン", "", new uint[] { 85, 70, 80, 97, 80, 123 }, (PokeType.Flying, PokeType.Dragon), new string[] { "おみとおし", "すりぬけ", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(716, -1, -1, -1, "ゼルネアス", "", new uint[] { 126, 131, 95, 131, 98, 99 }, (PokeType.Fairy, PokeType.Non), new string[] { "フェアリーオーラ", "フェアリーオーラ", "フェアリーオーラ" }, GenderRatio.Genderless));
            dexData.Add(new Species(717, -1, -1, -1, "イベルタル", "", new uint[] { 126, 131, 95, 131, 98, 99 }, (PokeType.Dark, PokeType.Flying), new string[] { "ダークオーラ", "ダークオーラ", "ダークオーラ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(718, -1, -1, -1, "ジガルデ", "50%", new uint[] { 108, 100, 121, 81, 95, 95 }, (PokeType.Dragon, PokeType.Ground), new string[] { "オーラブレイク", "オーラブレイク", "スワームチェンジ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(718, -1, -1, -1, "ジガルデ", "10%", new uint[] { 54, 100, 71, 61, 85, 115 }, (PokeType.Dragon, PokeType.Ground), new string[] { "オーラブレイク", "オーラブレイク", "スワームチェンジ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(718, -1, -1, -1, "ジガルデ", "パーフェクト", new uint[] { 216, 100, 121, 91, 95, 85 }, (PokeType.Dragon, PokeType.Ground), new string[] { "スワームチェンジ", "スワームチェンジ", "スワームチェンジ" }, GenderRatio.Genderless));
            dexData.Add(new Species(719, -1, -1, -1, "ディアンシー", "", new uint[] { 50, 100, 150, 100, 150, 50 }, (PokeType.Rock, PokeType.Fairy), new string[] { "クリアボディ", "クリアボディ", "クリアボディ" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(720, -1, -1, -1, "フーパ", "戒め", new uint[] { 80, 110, 60, 150, 130, 70 }, (PokeType.Psychic, PokeType.Ghost), new string[] { "マジシャン", "マジシャン", "マジシャン" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(720, -1, -1, -1, "フーパ", "解放", new uint[] { 80, 160, 60, 170, 130, 80 }, (PokeType.Psychic, PokeType.Dark), new string[] { "マジシャン", "マジシャン", "マジシャン" }, GenderRatio.Genderless));
            dexData.Add(new Species(721, -1, -1, -1, "ボルケニオン", "", new uint[] { 80, 110, 120, 130, 90, 70 }, (PokeType.Fire, PokeType.Water), new string[] { "ちょすい", "ちょすい", "ちょすい" }, GenderRatio.Genderless));
            dexData.Add(new Species(722, -1, -1, -1, "モクロー", "", new uint[] { 68, 55, 55, 50, 50, 42 }, (PokeType.Grass, PokeType.Flying), new string[] { "しんりょく", "しんりょく", "えんかく" }, GenderRatio.M7F1));
            dexData.Add(new Species(723, -1, -1, -1, "フクスロー", "", new uint[] { 78, 75, 75, 70, 70, 52 }, (PokeType.Grass, PokeType.Flying), new string[] { "しんりょく", "しんりょく", "えんかく" }, GenderRatio.M7F1));
            dexData.Add(new Species(724, -1, -1, -1, "ジュナイパー", "", new uint[] { 78, 107, 75, 100, 100, 70 }, (PokeType.Grass, PokeType.Ghost), new string[] { "しんりょく", "しんりょく", "えんかく" }, GenderRatio.M7F1));
            dexData.Add(new Species(725, -1, -1, -1, "ニャビー", "", new uint[] { 45, 65, 40, 60, 40, 70 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "いかく" }, GenderRatio.M7F1));
            dexData.Add(new Species(726, -1, -1, -1, "ニャヒート", "", new uint[] { 65, 85, 50, 80, 50, 90 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "いかく" }, GenderRatio.M7F1));
            dexData.Add(new Species(727, -1, -1, -1, "ガオガエン", "", new uint[] { 95, 115, 90, 80, 90, 60 }, (PokeType.Fire, PokeType.Dark), new string[] { "もうか", "もうか", "いかく" }, GenderRatio.M7F1));
            dexData.Add(new Species(728, -1, -1, -1, "アシマリ", "", new uint[] { 50, 54, 54, 66, 56, 40 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "うるおいボイス" }, GenderRatio.M7F1));
            dexData.Add(new Species(729, -1, -1, -1, "オシャマリ", "", new uint[] { 60, 69, 69, 91, 81, 50 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "うるおいボイス" }, GenderRatio.M7F1));
            dexData.Add(new Species(730, -1, -1, -1, "アシレーヌ", "", new uint[] { 80, 74, 74, 126, 116, 60 }, (PokeType.Water, PokeType.Fairy), new string[] { "げきりゅう", "げきりゅう", "うるおいボイス" }, GenderRatio.M7F1));
            dexData.Add(new Species(731, -1, -1, -1, "ツツケラ", "", new uint[] { 35, 75, 30, 30, 30, 65 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "スキルリンク", "ものひろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(732, -1, -1, -1, "ケララッパ", "", new uint[] { 55, 85, 50, 40, 50, 75 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "スキルリンク", "ものひろい" }, GenderRatio.M1F1));
            dexData.Add(new Species(733, -1, -1, -1, "ドデカバシ", "", new uint[] { 80, 120, 75, 75, 75, 60 }, (PokeType.Normal, PokeType.Flying), new string[] { "するどいめ", "スキルリンク", "ちからずく" }, GenderRatio.M1F1));
            dexData.Add(new Species(734, -1, -1, -1, "ヤングース", "", new uint[] { 48, 70, 30, 30, 30, 45 }, (PokeType.Normal, PokeType.Non), new string[] { "はりこみ", "がんじょうあご", "てきおうりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(735, -1, -1, -1, "デカグース", "", new uint[] { 88, 110, 60, 55, 60, 45 }, (PokeType.Normal, PokeType.Non), new string[] { "はりこみ", "がんじょうあご", "てきおうりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(736, 16, -1, -1, "アゴジムシ", "", new uint[] { 47, 62, 45, 55, 45, 46 }, (PokeType.Bug, PokeType.Non), new string[] { "むしのしらせ", "むしのしらせ", "むしのしらせ" }, GenderRatio.M1F1));
            dexData.Add(new Species(737, 17, -1, -1, "デンヂムシ", "", new uint[] { 57, 82, 95, 55, 75, 36 }, (PokeType.Bug, PokeType.Electric), new string[] { "バッテリー", "バッテリー", "バッテリー" }, GenderRatio.M1F1));
            dexData.Add(new Species(738, 18, -1, -1, "クワガノン", "", new uint[] { 77, 70, 90, 145, 75, 43 }, (PokeType.Bug, PokeType.Electric), new string[] { "ふゆう", "ふゆう", "ふゆう" }, GenderRatio.M1F1));
            dexData.Add(new Species(739, -1, -1, -1, "マケンカニ", "", new uint[] { 47, 82, 57, 42, 47, 63 }, (PokeType.Fighting, PokeType.Non), new string[] { "かいりきバサミ", "てつのこぶし", "いかりのつぼ" }, GenderRatio.M1F1));
            dexData.Add(new Species(740, -1, -1, -1, "ケケンカニ", "", new uint[] { 97, 132, 77, 62, 67, 43 }, (PokeType.Fighting, PokeType.Ice), new string[] { "かいりきバサミ", "てつのこぶし", "いかりのつぼ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(741, -1, -1, -1, "オドリドリ", "めらめら", new uint[] { 75, 70, 70, 98, 70, 93 }, (PokeType.Fire, PokeType.Flying), new string[] { "おどりこ", "おどりこ", "おどりこ" }, GenderRatio.M1F3));
            dexData.Add(new AnotherForm(741, -1, -1, -1, "オドリドリ", "ぱちぱち", new uint[] { 75, 70, 70, 98, 70, 93 }, (PokeType.Electric, PokeType.Flying), new string[] { "おどりこ", "おどりこ", "おどりこ" }, GenderRatio.M1F3));
            dexData.Add(new AnotherForm(741, -1, -1, -1, "オドリドリ", "ふらふら", new uint[] { 75, 70, 70, 98, 70, 93 }, (PokeType.Psychic, PokeType.Flying), new string[] { "おどりこ", "おどりこ", "おどりこ" }, GenderRatio.M1F3));
            dexData.Add(new AnotherForm(741, -1, -1, -1, "オドリドリ", "まいまい", new uint[] { 75, 70, 70, 98, 70, 93 }, (PokeType.Ghost, PokeType.Flying), new string[] { "おどりこ", "おどりこ", "おどりこ" }, GenderRatio.M1F3));
            dexData.Add(new Species(742, 187, -1, -1, "アブリー", "", new uint[] { 40, 45, 40, 55, 40, 84 }, (PokeType.Bug, PokeType.Fairy), new string[] { "みつあつめ", "りんぷん", "スイートベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(743, 188, -1, -1, "アブリボン", "", new uint[] { 60, 55, 60, 95, 70, 124 }, (PokeType.Bug, PokeType.Fairy), new string[] { "みつあつめ", "りんぷん", "スイートベール" }, GenderRatio.M1F1));
            dexData.Add(new Species(744, -1, 157, -1, "イワンコ", "", new uint[] { 45, 65, 40, 30, 40, 60 }, (PokeType.Rock, PokeType.Non), new string[] { "するどいめ", "やるき", "ふくつのこころ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(744, -1, 157, -1, "イワンコ", "夕", new uint[] { 45, 65, 40, 30, 40, 60 }, (PokeType.Rock, PokeType.Non), new string[] { "マイペース", "マイペース", "マイペース" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(745, -1, 158, -1, "ルガルガン", "昼", new uint[] { 75, 115, 65, 55, 65, 112 }, (PokeType.Rock, PokeType.Non), new string[] { "するどいめ", "すなかき", "ふくつのこころ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(745, -1, 158, -1, "ルガルガン", "夜", new uint[] { 85, 115, 75, 55, 75, 82 }, (PokeType.Rock, PokeType.Non), new string[] { "するどいめ", "やるき", "ノーガード" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(745, -1, 158, -1, "ルガルガン", "夕", new uint[] { 75, 117, 65, 55, 65, 110 }, (PokeType.Rock, PokeType.Non), new string[] { "かたいツメ", "かたいツメ", "かたいツメ" }, GenderRatio.M1F1));
            dexData.Add(new Species(746, 155, 110, -1, "ヨワシ", "単独", new uint[] { 45, 20, 20, 25, 25, 40 }, (PokeType.Water, PokeType.Non), new string[] { "ぎょぐん", "ぎょぐん", "ぎょぐん" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(746, 155, 110, -1, "ヨワシ", "群れ", new uint[] { 45, 140, 130, 140, 135, 30 }, (PokeType.Water, PokeType.Non), new string[] { "ぎょぐん", "ぎょぐん", "ぎょぐん" }, GenderRatio.M1F1));
            dexData.Add(new Species(747, 307, 127, -1, "ヒドイデ", "", new uint[] { 50, 53, 62, 43, 52, 45 }, (PokeType.Poison, PokeType.Water), new string[] { "ひとでなし", "じゅうなん", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(748, 308, 128, -1, "ドヒドイデ", "", new uint[] { 50, 63, 152, 53, 142, 35 }, (PokeType.Poison, PokeType.Water), new string[] { "ひとでなし", "じゅうなん", "さいせいりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(749, 84, -1, -1, "ドロバンコ", "", new uint[] { 70, 100, 70, 45, 55, 45 }, (PokeType.Ground, PokeType.Non), new string[] { "マイペース", "じきゅうりょく", "せいしんりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(750, 85, -1, -1, "バンバドロ", "", new uint[] { 100, 125, 100, 55, 85, 35 }, (PokeType.Ground, PokeType.Non), new string[] { "マイペース", "じきゅうりょく", "せいしんりょく" }, GenderRatio.M1F1));
            dexData.Add(new Species(751, 214, -1, 91, "シズクモ", "", new uint[] { 38, 40, 52, 40, 72, 27 }, (PokeType.Water, PokeType.Bug), new string[] { "すいほう", "すいほう", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(752, 215, -1, 92, "オニシズクモ", "", new uint[] { 68, 70, 92, 50, 132, 42 }, (PokeType.Water, PokeType.Bug), new string[] { "すいほう", "すいほう", "ちょすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(753, -1, 17, -1, "カリキリ", "", new uint[] { 40, 55, 35, 50, 35, 35 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "リーフガード", "あまのじゃく" }, GenderRatio.M1F1));
            dexData.Add(new Species(754, -1, 18, -1, "ラランテス", "", new uint[] { 70, 105, 90, 80, 90, 45 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "リーフガード", "あまのじゃく" }, GenderRatio.M1F1));
            dexData.Add(new Species(755, 340, -1, -1, "ネマシュ", "", new uint[] { 40, 35, 55, 65, 75, 15 }, (PokeType.Grass, PokeType.Fairy), new string[] { "はっこう", "ほうし", "あめうけざら" }, GenderRatio.M1F1));
            dexData.Add(new Species(756, 341, -1, -1, "マシェード", "", new uint[] { 60, 45, 80, 90, 100, 30 }, (PokeType.Grass, PokeType.Fairy), new string[] { "はっこう", "ほうし", "あめうけざら" }, GenderRatio.M1F1));
            dexData.Add(new Species(757, 244, 159, -1, "ヤトウモリ", "", new uint[] { 48, 44, 40, 71, 40, 77 }, (PokeType.Poison, PokeType.Fire), new string[] { "ふしょく", "ふしょく", "どんかん" }, GenderRatio.M7F1));
            dexData.Add(new Species(758, 245, 160, -1, "エンニュート", "", new uint[] { 68, 64, 60, 111, 60, 117 }, (PokeType.Poison, PokeType.Fire), new string[] { "ふしょく", "ふしょく", "どんかん" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(759, 94, -1, -1, "ヌイコグマ", "", new uint[] { 70, 75, 50, 45, 50, 50 }, (PokeType.Normal, PokeType.Fighting), new string[] { "もふもふ", "ぶきよう", "メロメロボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(760, 95, -1, -1, "キテルグマ", "", new uint[] { 120, 125, 80, 55, 60, 60 }, (PokeType.Normal, PokeType.Fighting), new string[] { "もふもふ", "ぶきよう", "きんちょうかん" }, GenderRatio.M1F1));
            dexData.Add(new Species(761, 52, -1, -1, "アマカジ", "", new uint[] { 42, 30, 38, 30, 38, 32 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "どんかん", "スイートベール" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(762, 53, -1, -1, "アママイコ", "", new uint[] { 52, 40, 48, 40, 48, 62 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "どんかん", "スイートベール" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(763, 54, -1, -1, "アマージョ", "", new uint[] { 72, 120, 98, 50, 98, 72 }, (PokeType.Grass, PokeType.Non), new string[] { "リーフガード", "じょおうのいげん", "スイートベール" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(764, -1, 79, -1, "キュワワー", "", new uint[] { 51, 52, 90, 82, 110, 100 }, (PokeType.Fairy, PokeType.Non), new string[] { "フラワーベール", "ヒーリングシフト", "しぜんかいふく" }, GenderRatio.M1F3));
            dexData.Add(new Species(765, 342, 89, -1, "ヤレユータン", "", new uint[] { 90, 60, 80, 90, 110, 60 }, (PokeType.Normal, PokeType.Psychic), new string[] { "せいしんりょく", "テレパシー", "きょうせい" }, GenderRatio.M1F1));
            dexData.Add(new Species(766, 343, 90, -1, "ナゲツケサル", "", new uint[] { 100, 120, 90, 40, 60, 80 }, (PokeType.Fighting, PokeType.Non), new string[] { "レシーバー", "レシーバー", "まけんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(767, 232, 124, -1, "コソクムシ", "", new uint[] { 25, 35, 40, 20, 30, 80 }, (PokeType.Bug, PokeType.Water), new string[] { "にげごし", "にげごし", "にげごし" }, GenderRatio.M1F1));
            dexData.Add(new Species(768, 233, 125, -1, "グソクムシャ", "", new uint[] { 75, 125, 140, 60, 90, 40 }, (PokeType.Bug, PokeType.Water), new string[] { "ききかいひ", "ききかいひ", "ききかいひ" }, GenderRatio.M1F1));
            dexData.Add(new Species(769, -1, 133, -1, "スナバァ", "", new uint[] { 55, 55, 80, 70, 45, 15 }, (PokeType.Ghost, PokeType.Ground), new string[] { "みずがため", "みずがため", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(770, -1, 134, -1, "シロデスナ", "", new uint[] { 85, 75, 110, 100, 75, 35 }, (PokeType.Ghost, PokeType.Ground), new string[] { "みずがため", "みずがため", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(771, 156, -1, -1, "ナマコブシ", "", new uint[] { 55, 60, 130, 30, 130, 5 }, (PokeType.Water, PokeType.Non), new string[] { "とびだすなかみ", "とびだすなかみ", "てんねん" }, GenderRatio.M1F1));
            dexData.Add(new Species(772, 381, -1, -1, "タイプ:ヌル", "", new uint[] { 95, 95, 95, 95, 95, 59 }, (PokeType.Normal, PokeType.Non), new string[] { "カブトアーマー", "カブトアーマー", "カブトアーマー" }, GenderRatio.Genderless));
            dexData.Add(new Species(773, 382, -1, -1, "シルヴァディ", "", new uint[] { 95, 95, 95, 95, 95, 95 }, (PokeType.Normal, PokeType.Non), new string[] { "ARシステム", "ARシステム", "ARシステム" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(774, -1, -1, -1, "メテノ", "流星", new uint[] { 60, 60, 100, 60, 100, 60 }, (PokeType.Rock, PokeType.Flying), new string[] { "リミットシールド", "リミットシールド", "リミットシールド" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(774, -1, -1, -1, "メテノ", "コア", new uint[] { 60, 100, 60, 100, 60, 120 }, (PokeType.Rock, PokeType.Flying), new string[] { "リミットシールド", "リミットシールド", "リミットシールド" }, GenderRatio.Genderless));
            dexData.Add(new Species(775, -1, -1, -1, "ネッコアラ", "", new uint[] { 65, 115, 65, 75, 95, 65 }, (PokeType.Normal, PokeType.Non), new string[] { "ぜったいねむり", "ぜったいねむり", "ぜったいねむり" }, GenderRatio.M1F1));
            dexData.Add(new Species(776, 347, -1, -1, "バクガメス", "", new uint[] { 60, 78, 135, 91, 85, 36 }, (PokeType.Fire, PokeType.Dragon), new string[] { "シェルアーマー", "シェルアーマー", "シェルアーマー" }, GenderRatio.M1F1));
            dexData.Add(new Species(777, 348, -1, -1, "トゲデマル", "", new uint[] { 65, 98, 63, 40, 73, 96 }, (PokeType.Electric, PokeType.Steel), new string[] { "てつのトゲ", "ひらいしん", "がんじょう" }, GenderRatio.M1F1));
            dexData.Add(new Species(778, 301, -1, 46, "ミミッキュ", "", new uint[] { 55, 90, 80, 50, 105, 96 }, (PokeType.Ghost, PokeType.Fairy), new string[] { "ばけのかわ", "ばけのかわ", "ばけのかわ" }, GenderRatio.M1F1));
            dexData.Add(new Species(779, -1, -1, -1, "ハギギシリ", "", new uint[] { 68, 105, 70, 70, 70, 92 }, (PokeType.Water, PokeType.Psychic), new string[] { "ビビッドボディ", "がんじょうあご", "ミラクルスキン" }, GenderRatio.M1F1));
            dexData.Add(new Species(780, 346, -1, -1, "ジジーロン", "", new uint[] { 78, 60, 85, 135, 91, 36 }, (PokeType.Normal, PokeType.Dragon), new string[] { "ぎゃくじょう", "そうしょく", "ノーてんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(781, 360, -1, 162, "ダダリン", "", new uint[] { 70, 131, 100, 86, 90, 40 }, (PokeType.Ghost, PokeType.Grass), new string[] { "はがねつかい", "はがねつかい", "はがねつかい" }, GenderRatio.Genderless));
            dexData.Add(new Species(782, 392, 165, -1, "ジャラコ", "", new uint[] { 45, 55, 65, 45, 45, 45 }, (PokeType.Dragon, PokeType.Non), new string[] { "ぼうだん", "ぼうおん", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(783, 393, 166, -1, "ジャランゴ", "", new uint[] { 55, 75, 90, 65, 70, 65 }, (PokeType.Dragon, PokeType.Fighting), new string[] { "ぼうだん", "ぼうおん", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(784, 394, 167, -1, "ジャラランガ", "", new uint[] { 75, 110, 125, 100, 105, 85 }, (PokeType.Dragon, PokeType.Fighting), new string[] { "ぼうだん", "ぼうおん", "ぼうじん" }, GenderRatio.M1F1));
            dexData.Add(new Species(785, -1, -1, -1, "カプ・コケコ", "", new uint[] { 70, 115, 85, 95, 75, 130 }, (PokeType.Electric, PokeType.Fairy), new string[] { "エレキメイカー", "エレキメイカー", "テレパシー" }, GenderRatio.Genderless));
            dexData.Add(new Species(786, -1, -1, -1, "カプ・テテフ", "", new uint[] { 70, 85, 75, 130, 115, 95 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "サイコメイカー", "サイコメイカー", "テレパシー" }, GenderRatio.Genderless));
            dexData.Add(new Species(787, -1, -1, -1, "カプ・ブルル", "", new uint[] { 70, 130, 115, 85, 95, 75 }, (PokeType.Grass, PokeType.Fairy), new string[] { "グラスメイカー", "グラスメイカー", "テレパシー" }, GenderRatio.Genderless));
            dexData.Add(new Species(788, -1, -1, -1, "カプ・レヒレ", "", new uint[] { 70, 75, 115, 95, 130, 85 }, (PokeType.Water, PokeType.Fairy), new string[] { "ミストメイカー", "ミストメイカー", "テレパシー" }, GenderRatio.Genderless));
            dexData.Add(new Species(789, -1, -1, -1, "コスモッグ", "", new uint[] { 43, 29, 31, 29, 31, 37 }, (PokeType.Psychic, PokeType.Non), new string[] { "てんねん", "てんねん", "てんねん" }, GenderRatio.Genderless));
            dexData.Add(new Species(790, -1, -1, -1, "コスモウム", "", new uint[] { 43, 29, 131, 29, 131, 37 }, (PokeType.Psychic, PokeType.Non), new string[] { "がんじょう", "がんじょう", "がんじょう" }, GenderRatio.Genderless));
            dexData.Add(new Species(791, -1, -1, -1, "ソルガレオ", "", new uint[] { 137, 137, 107, 113, 89, 97 }, (PokeType.Psychic, PokeType.Steel), new string[] { "メタルプロテクト", "メタルプロテクト", "メタルプロテクト" }, GenderRatio.Genderless));
            dexData.Add(new Species(792, -1, -1, -1, "ルナアーラ", "", new uint[] { 137, 113, 89, 137, 107, 97 }, (PokeType.Psychic, PokeType.Ghost), new string[] { "ファントムガード", "ファントムガード", "ファントムガード" }, GenderRatio.Genderless));
            dexData.Add(new Species(793, -1, -1, -1, "ウツロイド", "", new uint[] { 109, 53, 47, 127, 131, 103 }, (PokeType.Rock, PokeType.Poison), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(794, -1, -1, -1, "マッシブーン", "", new uint[] { 107, 139, 139, 53, 53, 79 }, (PokeType.Bug, PokeType.Fighting), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(795, -1, -1, -1, "フェローチェ", "", new uint[] { 71, 137, 37, 137, 37, 151 }, (PokeType.Bug, PokeType.Fighting), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(796, -1, -1, -1, "デンジュモク", "", new uint[] { 83, 89, 71, 173, 71, 83 }, (PokeType.Electric, PokeType.Non), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(797, -1, -1, -1, "テッカグヤ", "", new uint[] { 97, 101, 103, 107, 101, 61 }, (PokeType.Steel, PokeType.Flying), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(798, -1, -1, -1, "カミツルギ", "", new uint[] { 59, 181, 131, 59, 31, 109 }, (PokeType.Grass, PokeType.Steel), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(799, -1, -1, -1, "アクジキング", "", new uint[] { 223, 101, 53, 97, 53, 43 }, (PokeType.Dark, PokeType.Dragon), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(800, -1, -1, -1, "ネクロズマ", "", new uint[] { 97, 107, 101, 127, 89, 79 }, (PokeType.Psychic, PokeType.Non), new string[] { "プリズムアーマー", "プリズムアーマー", "プリズムアーマー" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(800, -1, -1, -1, "ネクロズマ", "たそがれ", new uint[] { 97, 157, 127, 113, 109, 77 }, (PokeType.Psychic, PokeType.Steel), new string[] { "プリズムアーマー", "プリズムアーマー", "プリズムアーマー" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(800, -1, -1, -1, "ネクロズマ", "あかつき", new uint[] { 97, 113, 109, 157, 127, 77 }, (PokeType.Psychic, PokeType.Ghost), new string[] { "プリズムアーマー", "プリズムアーマー", "プリズムアーマー" }, GenderRatio.Genderless));
            dexData.Add(new Species(801, -1, -1, -1, "マギアナ", "", new uint[] { 80, 95, 115, 130, 115, 65 }, (PokeType.Steel, PokeType.Fairy), new string[] { "ソウルハート", "ソウルハート", "ソウルハート" }, GenderRatio.Genderless));
            dexData.Add(new Species(802, -1, -1, -1, "マーシャドー", "", new uint[] { 90, 125, 80, 90, 90, 125 }, (PokeType.Fighting, PokeType.Ghost), new string[] { "テクニシャン", "テクニシャン", "テクニシャン" }, GenderRatio.Genderless));
            dexData.Add(new Species(803, -1, -1, -1, "ベベノム", "", new uint[] { 67, 73, 67, 73, 67, 73 }, (PokeType.Poison, PokeType.Non), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(804, -1, -1, -1, "アーゴヨン", "", new uint[] { 73, 73, 73, 127, 73, 121 }, (PokeType.Poison, PokeType.Dragon), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(805, -1, -1, -1, "ツンデツンデ", "", new uint[] { 61, 131, 211, 53, 101, 13 }, (PokeType.Rock, PokeType.Steel), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(806, -1, -1, -1, "ズガドーン", "", new uint[] { 53, 127, 53, 151, 79, 107 }, (PokeType.Fire, PokeType.Ghost), new string[] { "ビーストブースト", "ビーストブースト", "ビーストブースト" }, GenderRatio.Genderless));
            dexData.Add(new Species(807, -1, -1, -1, "ゼラオラ", "", new uint[] { 88, 112, 75, 102, 80, 143 }, (PokeType.Electric, PokeType.Non), new string[] { "ちくでん", "ちくでん", "ちくでん" }, GenderRatio.Genderless));
            dexData.Add(new Species(808, -1, -1, -1, "メルタン", "", new uint[] { 46, 65, 65, 55, 35, 34 }, (PokeType.Steel, PokeType.Non), new string[] { "じりょく", "じりょく", "じりょく" }, GenderRatio.Genderless));
            dexData.Add(new Species(809, -1, -1, -1, "メルメタル", "", new uint[] { 135, 143, 143, 80, 65, 34 }, (PokeType.Steel, PokeType.Non), new string[] { "てつのこぶし", "てつのこぶし", "てつのこぶし" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(809, -1, -1, -1, "メルメタル", "キョダイ", new uint[] { 135, 143, 143, 80, 65, 34 }, (PokeType.Steel, PokeType.Non), new string[] { "てつのこぶし", "てつのこぶし", "てつのこぶし" }, GenderRatio.Genderless));
            dexData.Add(new Species(810, 1, -1, -1, "サルノリ", "", new uint[] { 50, 65, 50, 40, 40, 65 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "グラスメイカー" }, GenderRatio.M7F1));
            dexData.Add(new Species(811, 2, -1, -1, "バチンキー", "", new uint[] { 70, 85, 70, 55, 60, 80 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "グラスメイカー" }, GenderRatio.M7F1));
            dexData.Add(new Species(812, 3, -1, -1, "ゴリランダー", "", new uint[] { 100, 125, 90, 60, 70, 85 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "グラスメイカー" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(812, 3, -1, -1, "ゴリランダー", "キョダイ", new uint[] { 100, 125, 90, 60, 70, 85 }, (PokeType.Grass, PokeType.Non), new string[] { "しんりょく", "しんりょく", "グラスメイカー" }, GenderRatio.M7F1));
            dexData.Add(new Species(813, 4, -1, -1, "ヒバニー", "", new uint[] { 50, 71, 40, 40, 40, 69 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "リベロ" }, GenderRatio.M7F1));
            dexData.Add(new Species(814, 5, -1, -1, "ラビフット", "", new uint[] { 65, 86, 60, 55, 60, 94 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "リベロ" }, GenderRatio.M7F1));
            dexData.Add(new Species(815, 6, -1, -1, "エースバーン", "", new uint[] { 80, 116, 75, 65, 75, 119 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "リベロ" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(815, 6, -1, -1, "エースバーン", "キョダイ", new uint[] { 80, 116, 75, 65, 75, 119 }, (PokeType.Fire, PokeType.Non), new string[] { "もうか", "もうか", "リベロ" }, GenderRatio.M7F1));
            dexData.Add(new Species(816, 7, -1, -1, "メッソン", "", new uint[] { 50, 40, 40, 70, 40, 70 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "スナイパー" }, GenderRatio.M7F1));
            dexData.Add(new Species(817, 8, -1, -1, "ジメレオン", "", new uint[] { 65, 60, 55, 95, 55, 90 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "スナイパー" }, GenderRatio.M7F1));
            dexData.Add(new Species(818, 9, -1, -1, "インテレオン", "", new uint[] { 70, 85, 65, 125, 65, 120 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "スナイパー" }, GenderRatio.M7F1));
            dexData.Add(new AnotherForm(818, 9, -1, -1, "インテレオン", "キョダイ", new uint[] { 70, 85, 65, 125, 65, 120 }, (PokeType.Water, PokeType.Non), new string[] { "げきりゅう", "げきりゅう", "スナイパー" }, GenderRatio.M7F1));
            dexData.Add(new Species(819, 24, 9, 5, "ホシガリス", "", new uint[] { 70, 55, 55, 35, 35, 25 }, (PokeType.Normal, PokeType.Non), new string[] { "ほおぶくろ", "ほおぶくろ", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(820, 25, 10, 6, "ヨクバリス", "", new uint[] { 120, 95, 95, 55, 75, 20 }, (PokeType.Normal, PokeType.Non), new string[] { "ほおぶくろ", "ほおぶくろ", "くいしんぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(821, 21, -1, 163, "ココガラ", "", new uint[] { 38, 47, 35, 33, 35, 57 }, (PokeType.Flying, PokeType.Non), new string[] { "するどいめ", "きんちょうかん", "はとむね" }, GenderRatio.M1F1));
            dexData.Add(new Species(822, 22, -1, 164, "アオガラス", "", new uint[] { 68, 67, 55, 43, 55, 77 }, (PokeType.Flying, PokeType.Non), new string[] { "するどいめ", "きんちょうかん", "はとむね" }, GenderRatio.M1F1));
            dexData.Add(new Species(823, 23, -1, 165, "アーマーガア", "", new uint[] { 98, 87, 105, 53, 85, 67 }, (PokeType.Flying, PokeType.Steel), new string[] { "プレッシャー", "きんちょうかん", "ミラーアーマー" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(823, 23, -1, 165, "アーマーガア", "キョダイ", new uint[] { 98, 87, 105, 53, 85, 67 }, (PokeType.Flying, PokeType.Steel), new string[] { "プレッシャー", "きんちょうかん", "ミラーアーマー" }, GenderRatio.M1F1));
            dexData.Add(new Species(824, 10, 14, -1, "サッチムシ", "", new uint[] { 25, 20, 20, 25, 45, 45 }, (PokeType.Bug, PokeType.Non), new string[] { "むしのしらせ", "ふくがん", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(825, 11, 15, -1, "レドームシ", "", new uint[] { 50, 35, 80, 50, 90, 30 }, (PokeType.Bug, PokeType.Psychic), new string[] { "むしのしらせ", "ふくがん", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(826, 12, 16, -1, "イオルブ", "", new uint[] { 60, 45, 110, 80, 120, 90 }, (PokeType.Bug, PokeType.Psychic), new string[] { "むしのしらせ", "おみとおし", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(826, 12, 16, -1, "イオルブ", "キョダイ", new uint[] { 60, 45, 110, 80, 120, 90 }, (PokeType.Bug, PokeType.Psychic), new string[] { "むしのしらせ", "おみとおし", "テレパシー" }, GenderRatio.M1F1));
            dexData.Add(new Species(827, 29, -1, -1, "クスネ", "", new uint[] { 40, 28, 28, 47, 52, 50 }, (PokeType.Dark, PokeType.Non), new string[] { "にげあし", "かるわざ", "はりこみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(828, 30, -1, -1, "フォクスライ", "", new uint[] { 70, 58, 58, 87, 92, 90 }, (PokeType.Dark, PokeType.Non), new string[] { "にげあし", "かるわざ", "はりこみ" }, GenderRatio.M1F1));
            dexData.Add(new Species(829, 126, -1, 166, "ヒメンカ", "", new uint[] { 40, 40, 60, 40, 60, 10 }, (PokeType.Grass, PokeType.Non), new string[] { "わたげ", "さいせいりょく", "ほうし" }, GenderRatio.M1F1));
            dexData.Add(new Species(830, 127, -1, 167, "ワタシラガ", "", new uint[] { 60, 50, 90, 80, 120, 60 }, (PokeType.Grass, PokeType.Non), new string[] { "わたげ", "さいせいりょく", "ほうし" }, GenderRatio.M1F1));
            dexData.Add(new Species(831, 34, -1, 3, "ウールー", "", new uint[] { 42, 40, 55, 40, 45, 48 }, (PokeType.Normal, PokeType.Non), new string[] { "もふもふ", "にげあし", "ぼうだん" }, GenderRatio.M1F1));
            dexData.Add(new Species(832, 35, -1, 4, "バイウールー", "", new uint[] { 72, 80, 100, 60, 90, 88 }, (PokeType.Normal, PokeType.Non), new string[] { "もふもふ", "ふくつのこころ", "ぼうだん" }, GenderRatio.M1F1));
            dexData.Add(new Species(833, 42, 56, -1, "カムカメ", "", new uint[] { 50, 64, 50, 38, 38, 44 }, (PokeType.Water, PokeType.Non), new string[] { "がんじょうあご", "シェルアーマー", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(834, 43, 57, -1, "カジリガメ", "", new uint[] { 90, 115, 90, 48, 68, 74 }, (PokeType.Water, PokeType.Rock), new string[] { "がんじょうあご", "シェルアーマー", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(834, 43, 57, -1, "カジリガメ", "キョダイ", new uint[] { 90, 115, 90, 48, 68, 74 }, (PokeType.Water, PokeType.Rock), new string[] { "がんじょうあご", "シェルアーマー", "すいすい" }, GenderRatio.M1F1));
            dexData.Add(new Species(835, 46, -1, 155, "ワンパチ", "", new uint[] { 59, 45, 50, 40, 50, 26 }, (PokeType.Electric, PokeType.Non), new string[] { "たまひろい", "たまひろい", "びびり" }, GenderRatio.M1F1));
            dexData.Add(new Species(836, 47, -1, 156, "パルスワン", "", new uint[] { 69, 90, 60, 90, 60, 121 }, (PokeType.Electric, PokeType.Non), new string[] { "がんじょうあご", "がんじょうあご", "かちき" }, GenderRatio.M1F1));
            dexData.Add(new Species(837, 161, -1, 176, "タンドン", "", new uint[] { 30, 40, 50, 40, 50, 30 }, (PokeType.Rock, PokeType.Non), new string[] { "じょうききかん", "たいねつ", "もらいび" }, GenderRatio.M1F1));
            dexData.Add(new Species(838, 162, -1, 177, "トロッゴン", "", new uint[] { 80, 60, 90, 60, 70, 50 }, (PokeType.Rock, PokeType.Fire), new string[] { "じょうききかん", "ほのおのからだ", "もらいび" }, GenderRatio.M1F1));
            dexData.Add(new Species(839, 163, -1, 178, "セキタンザン", "", new uint[] { 110, 80, 120, 80, 90, 30 }, (PokeType.Rock, PokeType.Fire), new string[] { "じょうききかん", "ほのおのからだ", "もらいび" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(839, 163, -1, 178, "セキタンザン", "キョダイ", new uint[] { 110, 80, 120, 80, 90, 30 }, (PokeType.Rock, PokeType.Fire), new string[] { "じょうききかん", "ほのおのからだ", "もらいび" }, GenderRatio.M1F1));
            dexData.Add(new Species(840, 205, 19, -1, "カジッチュ", "", new uint[] { 40, 40, 80, 40, 40, 20 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "ぼうだん" }, GenderRatio.M1F1));
            dexData.Add(new Species(841, 206, 20, -1, "アップリュー", "", new uint[] { 70, 110, 80, 95, 60, 70 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "はりきり" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(841, 206, 20, -1, "アップリュー", "キョダイ", new uint[] { 70, 110, 80, 95, 60, 70 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "はりきり" }, GenderRatio.M1F1));
            dexData.Add(new Species(842, 207, 21, -1, "タルップル", "", new uint[] { 110, 85, 80, 100, 80, 30 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "あついしぼう" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(842, 207, 21, -1, "タルップル", "キョダイ", new uint[] { 110, 85, 80, 100, 80, 30 }, (PokeType.Grass, PokeType.Dragon), new string[] { "じゅくせい", "くいしんぼう", "あついしぼう" }, GenderRatio.M1F1));
            dexData.Add(new Species(843, 312, 174, -1, "スナヘビ", "", new uint[] { 52, 57, 75, 35, 50, 46 }, (PokeType.Ground, PokeType.Non), new string[] { "すなはき", "だっぴ", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(844, 313, 175, -1, "サダイジャ", "", new uint[] { 72, 107, 125, 65, 70, 71 }, (PokeType.Ground, PokeType.Non), new string[] { "すなはき", "だっぴ", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(844, 313, 175, -1, "サダイジャ", "キョダイ", new uint[] { 72, 107, 125, 65, 70, 71 }, (PokeType.Ground, PokeType.Non), new string[] { "すなはき", "だっぴ", "すながくれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(845, 309, 93, -1, "ウッウ", "", new uint[] { 70, 85, 55, 85, 95, 85 }, (PokeType.Flying, PokeType.Water), new string[] { "うのミサイル", "うのミサイル", "うのミサイル" }, GenderRatio.M1F1));
            dexData.Add(new Species(846, 180, 96, -1, "サシカマス", "", new uint[] { 41, 63, 40, 40, 30, 66 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "スクリューおびれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(847, 181, 97, -1, "カマスジョー", "", new uint[] { 61, 123, 60, 60, 50, 136 }, (PokeType.Water, PokeType.Non), new string[] { "すいすい", "すいすい", "スクリューおびれ" }, GenderRatio.M1F1));
            dexData.Add(new Species(848, 310, -1, -1, "エレズン", "", new uint[] { 40, 38, 35, 54, 35, 40 }, (PokeType.Electric, PokeType.Poison), new string[] { "びびり", "せいでんき", "ぶきよう" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(849, 311, -1, -1, "ストリンダー", "ハイ", new uint[] { 75, 98, 70, 114, 70, 75 }, (PokeType.Electric, PokeType.Poison), new string[] { "パンクロック", "プラス", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(849, 311, -1, -1, "ストリンダー", "ハイキョダイ", new uint[] { 75, 98, 70, 114, 70, 75 }, (PokeType.Electric, PokeType.Poison), new string[] { "パンクロック", "プラス", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(849, 311, -1, -1, "ストリンダー", "ロー", new uint[] { 75, 98, 70, 114, 70, 75 }, (PokeType.Electric, PokeType.Poison), new string[] { "パンクロック", "マイナス", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(849, 311, -1, -1, "ストリンダー", "ローキョダイ", new uint[] { 75, 98, 70, 114, 70, 75 }, (PokeType.Electric, PokeType.Poison), new string[] { "パンクロック", "マイナス", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new Species(850, 159, -1, 99, "ヤクデ", "", new uint[] { 50, 65, 45, 50, 50, 45 }, (PokeType.Fire, PokeType.Bug), new string[] { "もらいび", "しろいけむり", "ほのおのからだ" }, GenderRatio.M1F1));
            dexData.Add(new Species(851, 160, -1, 100, "マルヤクデ", "", new uint[] { 100, 115, 65, 90, 90, 65 }, (PokeType.Fire, PokeType.Bug), new string[] { "もらいび", "しろいけむり", "ほのおのからだ" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(851, 160, -1, 100, "マルヤクデ", "キョダイ", new uint[] { 100, 115, 65, 90, 90, 65 }, (PokeType.Fire, PokeType.Bug), new string[] { "もらいび", "しろいけむり", "ほのおのからだ" }, GenderRatio.M1F1));
            dexData.Add(new Species(852, 351, 129, -1, "タタッコ", "", new uint[] { 50, 68, 60, 50, 50, 32 }, (PokeType.Fighting, PokeType.Non), new string[] { "じゅうなん", "じゅうなん", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new Species(853, 352, 130, -1, "オトスパス", "", new uint[] { 80, 118, 90, 70, 80, 42 }, (PokeType.Fighting, PokeType.Non), new string[] { "じゅうなん", "じゅうなん", "テクニシャン" }, GenderRatio.M1F1));
            dexData.Add(new Species(854, 335, -1, 132, "ヤバチャ", "", new uint[] { 40, 45, 45, 74, 54, 50 }, (PokeType.Ghost, PokeType.Non), new string[] { "くだけるよろい", "くだけるよろい", "のろわれボディ" }, GenderRatio.Genderless));
            dexData.Add(new Species(855, 336, -1, 133, "ポットデス", "", new uint[] { 60, 65, 65, 134, 114, 70 }, (PokeType.Ghost, PokeType.Non), new string[] { "くだけるよろい", "くだけるよろい", "のろわれボディ" }, GenderRatio.Genderless));
            dexData.Add(new Species(856, 241, -1, 40, "ミブリム", "", new uint[] { 42, 30, 45, 56, 53, 39 }, (PokeType.Psychic, PokeType.Non), new string[] { "いやしのこころ", "きけんよち", "マジックミラー" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(857, 242, -1, 41, "テブリム", "", new uint[] { 57, 40, 65, 86, 73, 49 }, (PokeType.Psychic, PokeType.Non), new string[] { "いやしのこころ", "きけんよち", "マジックミラー" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(858, 243, -1, 42, "ブリムオン", "", new uint[] { 57, 90, 95, 136, 103, 29 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "いやしのこころ", "きけんよち", "マジックミラー" }, GenderRatio.FemaleOnly));
            dexData.Add(new AnotherForm(858, 243, -1, 42, "ブリムオン", "キョダイ", new uint[] { 57, 90, 95, 136, 103, 29 }, (PokeType.Psychic, PokeType.Fairy), new string[] { "いやしのこころ", "きけんよち", "マジックミラー" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(859, 238, -1, 37, "ベロバー", "", new uint[] { 45, 45, 30, 55, 40, 50 }, (PokeType.Dark, PokeType.Fairy), new string[] { "いたずらごころ", "おみとおし", "わるいてぐせ" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(860, 239, -1, 38, "ギモー", "", new uint[] { 65, 60, 45, 75, 55, 70 }, (PokeType.Dark, PokeType.Fairy), new string[] { "いたずらごころ", "おみとおし", "わるいてぐせ" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(861, 240, -1, 39, "オーロンゲ", "", new uint[] { 95, 120, 65, 95, 75, 60 }, (PokeType.Dark, PokeType.Fairy), new string[] { "いたずらごころ", "おみとおし", "わるいてぐせ" }, GenderRatio.MaleOnly));
            dexData.Add(new AnotherForm(861, 240, -1, 39, "オーロンゲ", "キョダイ", new uint[] { 95, 120, 65, 95, 75, 60 }, (PokeType.Dark, PokeType.Fairy), new string[] { "いたずらごころ", "おみとおし", "わるいてぐせ" }, GenderRatio.MaleOnly));
            dexData.Add(new Species(862, 33, -1, 73, "タチフサグマ", "", new uint[] { 93, 90, 101, 60, 81, 95 }, (PokeType.Dark, PokeType.Normal), new string[] { "すてみ", "こんじょう", "まけんき" }, GenderRatio.M1F1));
            dexData.Add(new Species(863, 183, -1, -1, "ニャイキング", "", new uint[] { 70, 110, 100, 50, 60, 50 }, (PokeType.Steel, PokeType.Non), new string[] { "カブトアーマー", "かたいツメ", "はがねのせいしん" }, GenderRatio.M1F1));
            dexData.Add(new Species(864, 237, -1, -1, "サニゴーン", "", new uint[] { 60, 95, 50, 145, 130, 30 }, (PokeType.Ghost, PokeType.Non), new string[] { "くだけるよろい", "くだけるよろい", "ほろびのボディ" }, GenderRatio.M1F3));
            dexData.Add(new Species(865, 219, -1, -1, "ネギガナイト", "", new uint[] { 62, 135, 95, 68, 82, 65 }, (PokeType.Fighting, PokeType.Non), new string[] { "ふくつのこころ", "ふくつのこころ", "きもったま" }, GenderRatio.M1F1));
            dexData.Add(new Species(866, 366, -1, 12, "バリコオル", "", new uint[] { 80, 85, 75, 110, 100, 70 }, (PokeType.Ice, PokeType.Psychic), new string[] { "ちどりあし", "バリアフリー", "アイスボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(867, 328, -1, -1, "デスバーン", "", new uint[] { 58, 95, 145, 50, 105, 30 }, (PokeType.Ground, PokeType.Ghost), new string[] { "さまようたましい", "さまようたましい", "さまようたましい" }, GenderRatio.M1F1));
            dexData.Add(new Species(868, 185, -1, -1, "マホミル", "", new uint[] { 45, 40, 40, 50, 61, 34 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "アロマベール" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(869, 186, -1, -1, "マホイップ", "", new uint[] { 65, 60, 75, 110, 121, 64 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "アロマベール" }, GenderRatio.FemaleOnly));
            dexData.Add(new AnotherForm(869, 186, -1, -1, "マホイップ", "キョダイ", new uint[] { 65, 60, 75, 110, 121, 64 }, (PokeType.Fairy, PokeType.Non), new string[] { "スイートベール", "スイートベール", "アロマベール" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(870, 345, -1, -1, "タイレーツ", "", new uint[] { 65, 100, 100, 70, 60, 75 }, (PokeType.Fighting, PokeType.Non), new string[] { "カブトアーマー", "カブトアーマー", "まけんき" }, GenderRatio.Genderless));
            dexData.Add(new Species(871, 353, 126, 158, "バチンウニ", "", new uint[] { 48, 101, 95, 91, 85, 15 }, (PokeType.Electric, PokeType.Non), new string[] { "ひらいしん", "ひらいしん", "エレキメイカー" }, GenderRatio.M1F1));
            dexData.Add(new Species(872, 349, -1, 1, "ユキハミ", "", new uint[] { 30, 25, 35, 45, 30, 20 }, (PokeType.Ice, PokeType.Bug), new string[] { "りんぷん", "りんぷん", "こおりのりんぷん" }, GenderRatio.M1F1));
            dexData.Add(new Species(873, 350, -1, 2, "モスノウ", "", new uint[] { 70, 65, 60, 125, 90, 65 }, (PokeType.Ice, PokeType.Bug), new string[] { "りんぷん", "りんぷん", "こおりのりんぷん" }, GenderRatio.M1F1));
            dexData.Add(new Species(874, 369, -1, 89, "イシヘンジン", "", new uint[] { 100, 125, 135, 20, 20, 70 }, (PokeType.Rock, PokeType.Non), new string[] { "パワースポット", "パワースポット", "パワースポット" }, GenderRatio.M1F1));
            dexData.Add(new Species(875, 370, -1, 90, "コオリッポ", "", new uint[] { 75, 80, 110, 65, 90, 50 }, (PokeType.Ice, PokeType.Non), new string[] { "アイスフェイス", "アイスフェイス", "アイスフェイス" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(876, 337, -1, 171, "イエッサン", "♂", new uint[] { 60, 65, 55, 105, 95, 95 }, (PokeType.Psychic, PokeType.Normal), new string[] { "せいしんりょく", "シンクロ", "サイコメイカー" }, GenderRatio.MaleOnly));
            dexData.Add(new AnotherForm(876, 337, -1, 171, "イエッサン", "♀", new uint[] { 70, 55, 65, 95, 105, 85 }, (PokeType.Psychic, PokeType.Normal), new string[] { "マイペース", "シンクロ", "サイコメイカー" }, GenderRatio.FemaleOnly));
            dexData.Add(new Species(877, 344, 104, 157, "モルペコ", "", new uint[] { 58, 95, 58, 70, 58, 97 }, (PokeType.Electric, PokeType.Dark), new string[] { "はらぺこスイッチ", "はらぺこスイッチ", "はらぺこスイッチ" }, GenderRatio.M1F1));
            dexData.Add(new Species(878, 302, -1, 108, "ゾウドウ", "", new uint[] { 72, 80, 49, 40, 49, 40 }, (PokeType.Steel, PokeType.Non), new string[] { "ちからずく", "ちからずく", "ヘヴィメタル" }, GenderRatio.M1F1));
            dexData.Add(new Species(879, 303, -1, 109, "ダイオウドウ", "", new uint[] { 122, 130, 69, 80, 69, 30 }, (PokeType.Steel, PokeType.Non), new string[] { "ちからずく", "ちからずく", "ヘヴィメタル" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(879, 303, -1, 109, "ダイオウドウ", "キョダイ", new uint[] { 122, 130, 69, 80, 69, 30 }, (PokeType.Steel, PokeType.Non), new string[] { "ちからずく", "ちからずく", "ヘヴィメタル" }, GenderRatio.M1F1));
            dexData.Add(new Species(880, 374, -1, -1, "パッチラゴン", "", new uint[] { 90, 100, 90, 80, 70, 75 }, (PokeType.Electric, PokeType.Dragon), new string[] { "ちくでん", "はりきり", "すなかき" }, GenderRatio.Genderless));
            dexData.Add(new Species(881, 375, -1, -1, "パッチルドン", "", new uint[] { 90, 100, 90, 90, 80, 55 }, (PokeType.Electric, PokeType.Ice), new string[] { "ちくでん", "せいでんき", "ゆきかき" }, GenderRatio.Genderless));
            dexData.Add(new Species(882, 376, -1, -1, "ウオノラゴン", "", new uint[] { 90, 90, 100, 70, 80, 75 }, (PokeType.Water, PokeType.Dragon), new string[] { "ちょすい", "がんじょうあご", "すなかき" }, GenderRatio.Genderless));
            dexData.Add(new Species(883, 377, -1, -1, "ウオチルドン", "", new uint[] { 90, 90, 100, 80, 90, 55 }, (PokeType.Water, PokeType.Ice), new string[] { "ちょすい", "アイスボディ", "ゆきかき" }, GenderRatio.Genderless));
            dexData.Add(new Species(884, 371, -1, -1, "ジュラルドン", "", new uint[] { 70, 95, 115, 120, 50, 85 }, (PokeType.Steel, PokeType.Dragon), new string[] { "ライトメタル", "ヘヴィメタル", "すじがねいり" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(884, 371, -1, -1, "ジュラルドン", "キョダイ", new uint[] { 70, 95, 115, 120, 50, 85 }, (PokeType.Steel, PokeType.Dragon), new string[] { "ライトメタル", "ヘヴィメタル", "すじがねいり" }, GenderRatio.M1F1));
            dexData.Add(new Species(885, 395, -1, 110, "ドラメシヤ", "", new uint[] { 28, 60, 30, 40, 30, 82 }, (PokeType.Dragon, PokeType.Ghost), new string[] { "クリアボディ", "すりぬけ", "のろわれボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(886, 396, -1, 111, "ドロンチ", "", new uint[] { 68, 80, 50, 60, 50, 102 }, (PokeType.Dragon, PokeType.Ghost), new string[] { "クリアボディ", "すりぬけ", "のろわれボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(887, 397, -1, 112, "ドラパルト", "", new uint[] { 88, 120, 75, 100, 75, 142 }, (PokeType.Dragon, PokeType.Ghost), new string[] { "クリアボディ", "すりぬけ", "のろわれボディ" }, GenderRatio.M1F1));
            dexData.Add(new Species(888, 398, -1, -1, "ザシアン", "歴戦の勇者", new uint[] { 92, 130, 115, 80, 115, 138 }, (PokeType.Fairy, PokeType.Non), new string[] { "ふとうのけん", "ふとうのけん", "ふとうのけん" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(888, 398, -1, -1, "ザシアン", "剣の王", new uint[] { 92, 170, 115, 80, 115, 148 }, (PokeType.Fairy, PokeType.Steel), new string[] { "ふとうのけん", "ふとうのけん", "ふとうのけん" }, GenderRatio.Genderless));
            dexData.Add(new Species(889, 399, -1, -1, "ザマゼンタ", "歴戦の勇者", new uint[] { 92, 130, 115, 80, 115, 138 }, (PokeType.Fighting, PokeType.Non), new string[] { "ふくつのたて", "ふくつのたて", "ふくつのたて" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(889, 399, -1, -1, "ザマゼンタ", "盾の王", new uint[] { 92, 130, 145, 80, 145, 128 }, (PokeType.Fighting, PokeType.Steel), new string[] { "ふくつのたて", "ふくつのたて", "ふくつのたて" }, GenderRatio.Genderless));
            dexData.Add(new Species(890, 400, -1, -1, "ムゲンダイナ", "", new uint[] { 140, 85, 95, 145, 95, 130 }, (PokeType.Poison, PokeType.Dragon), new string[] { "プレッシャー", "プレッシャー", "プレッシャー" }, GenderRatio.Genderless));
            dexData.Add(new Species(891, -1, 100, -1, "ダクマ", "", new uint[] { 60, 90, 60, 53, 50, 72 }, (PokeType.Fighting, PokeType.Non), new string[] { "せいしんりょく", "せいしんりょく", "せいしんりょく" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(892, -1, 101, -1, "ウーラオス", "いちげき", new uint[] { 100, 130, 100, 63, 60, 97 }, (PokeType.Fighting, PokeType.Dark), new string[] { "ふかしのこぶし", "ふかしのこぶし", "ふかしのこぶし" }, GenderRatio.M1F1));
            dexData.Add(new AnotherForm(892, -1, 101, -1, "ウーラオス", "れんげき", new uint[] { 100, 130, 100, 63, 60, 97 }, (PokeType.Fighting, PokeType.Water), new string[] { "ふかしのこぶし", "ふかしのこぶし", "ふかしのこぶし" }, GenderRatio.M1F1));
            dexData.Add(new Species(893, -1, 211, -1, "ザルード", "", new uint[] { 105, 120, 105, 70, 95, 105 }, (PokeType.Dark, PokeType.Grass), new string[] { "リーフガード", "リーフガード", "リーフガード" }, GenderRatio.Genderless));
            dexData.Add(new Species(894, -1, -1, 200, "レジエレキ", "", new uint[] { 80, 100, 50, 100, 50, 200 }, (PokeType.Electric, PokeType.Non), new string[] { "トランジスタ", "トランジスタ", "トランジスタ" }, GenderRatio.Genderless));
            dexData.Add(new Species(895, -1, -1, 201, "レジドラゴ", "", new uint[] { 200, 100, 50, 100, 50, 80 }, (PokeType.Dragon, PokeType.Non), new string[] { "りゅうのあぎと", "りゅうのあぎと", "りゅうのあぎと" }, GenderRatio.Genderless));
            dexData.Add(new Species(896, -1, -1, 208, "ブリザポス", "", new uint[] { 100, 145, 130, 65, 110, 30 }, (PokeType.Ice, PokeType.Non), new string[] { "しろのいななき", "しろのいななき", "しろのいななき" }, GenderRatio.Genderless));
            dexData.Add(new Species(897, -1, -1, 209, "レイスポス", "", new uint[] { 100, 65, 60, 145, 80, 130 }, (PokeType.Ghost, PokeType.Non), new string[] { "くろのいななき", "くろのいななき", "くろのいななき" }, GenderRatio.Genderless));
            dexData.Add(new Species(898, -1, -1, 210, "バドレックス", "", new uint[] { 100, 80, 80, 80, 80, 80 }, (PokeType.Psychic, PokeType.Grass), new string[] { "きんちょうかん", "きんちょうかん", "きんちょうかん" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(898, -1, -1, 210, "バドレックス", "はくばじょう", new uint[] { 100, 165, 150, 85, 130, 50 }, (PokeType.Psychic, PokeType.Ice), new string[] { "じんばいったい", "じんばいったい", "じんばいったい" }, GenderRatio.Genderless));
            dexData.Add(new AnotherForm(898, -1, -1, 210, "バドレックス", "こくばじょう", new uint[] { 100, 85, 80, 165, 100, 150 }, (PokeType.Psychic, PokeType.Ghost), new string[] { "じんばいったい", "じんばいったい", "じんばいったい" }, GenderRatio.Genderless));

            // 名前+フォルムでDictionaryに追加。
            // フォルム名無しがないポケモンはDexDataの若いほうから.
            // DexDataを図鑑番号でDistinctする

            uniqueList = dexData.Distinct(new SpeciesComparer()).ToArray();
            uniqueDex = uniqueList.ToDictionary(_ => _.Name, _ => _);
            dexDictionary = dexData.ToDictionary(_ => _.Name + _.FormName, _ => _);
            formDex = dexData.ToLookup(_ => _.Name);
            galarDex = dexData.Where(_ => _.GalarDexID >= 0).OrderBy(_ => _.GalarDexID).ToArray();
            armorDex = dexData.Where(_ => _.ArmorDexID >= 0).OrderBy(_ => _.ArmorDexID).ToArray();
            crownDex = dexData.Where(_ => _.CrownDexID >= 0).OrderBy(_ => _.CrownDexID).ToArray();
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
