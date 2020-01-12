using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SwShRNGLibrary.Properties;

namespace SwShRNGLibrary
{
    public enum Category
    {
        PokeName, Ability, MapName, System
    }

    public class Language
    {
        private Dictionary<Category, Dictionary<string, string>> words;
        private Dictionary<Category, Dictionary<string, string>> toJPN;
        internal string[] Nature;
        internal string[] PokeType;

        internal virtual string Translate(Category cat, string txt) { return words[cat][txt]; }
        internal virtual string ToJPN(Category cat, string txt) { return toJPN[cat][txt]; }
        internal Language() { }
        internal Language(string[] natures, string[] pokeTypes, string pokeNames, string abilities, string mapNames)
        {
            words = new Dictionary<Category, Dictionary<string, string>>();
            toJPN = new Dictionary<Category, Dictionary<string, string>>();

            Nature = natures;
            PokeType = pokeTypes;

            var p = pokeNames.Replace("\r\n", "\n").Split(new[] { '\n', '\r' }).Select(_ => _.Split(',')).ToArray();
            var ab = abilities.Replace("\r\n", "\n").Split(new[] { '\n', '\r' }).Select(_ => _.Split(',')).ToArray();
            var mn = mapNames.Replace("\r\n", "\n").Split(new[] { '\n', '\r' }).Select(_ => _.Split(',')).ToArray();

            words.Add(Category.PokeName, p.ToDictionary(_ => _[0], _ => _[1]));
            words.Add(Category.Ability, ab.ToDictionary(_ => _[0], _ => _[1]));
            words.Add(Category.MapName, mn.ToDictionary(_ => _[0], _ => _[1]));
            toJPN.Add(Category.PokeName, p.ToDictionary(_ => _[1], _ => _[0]));
            toJPN.Add(Category.Ability, ab.ToDictionary(_ => _[1], _ => _[0]));
            toJPN.Add(Category.MapName, mn.ToDictionary(_ => _[1], _ => _[0]));
        }

        public static Language JPN;
        public static Language ENG;

        static Language()
        {
            string[] nature_JPN = new string[]
            {
                "がんばりや", "さみしがり", "ゆうかん", "いじっぱり", "やんちゃ",
                "ずぶとい", "すなお", "のんき", "わんぱく", "のうてんき",
                "おくびょう", "せっかち", "まじめ", "ようき", "むじゃき",
                "ひかえめ", "おっとり", "れいせい", "てれや", "うっかりや",
                "おだやか", "おとなしい", "なまいき", "しんちょう", "きまぐれ",
                "---"
            };
            string[] pokeType_JPN = new string[]
            {
                "ノーマル", "ほのお", "みず", "くさ", "でんき",
                "こおり", "かくとう", "どく", "じめん", "ひこう",
                "エスパー", "むし", "いわ", "ゴースト", "ドラゴン",
                "あく","はがね","フェアリー", "---"
            };
            string[] nature_ENG =
            {
                "Hardy", "Lonely", "Brave", "Adamant", "Naughty",
                "Bold", "Docile", "Relaxed", "Impish", "Lax",
                "Timid", "Hasty", "Serious", "Jolly", "Naive",
                "Modest", "Mild", "Quiet", "Bashful", "Rash",
                "Calm", "Gentle", "Sassy", "Careful", "Quirky",
                "---"
            };
            string[] pokeType_ENG = new string[]
            {
                "Normal", "Fire", "Water", "Grass", "Electric", "Ice", "Fighting", "Poison",
                "Ground", "Flying", "Psychic", "Bug", "Rock", "Ghost", "Dragon", "Dark", "Steel", "Fairy", "Non"
            };

            JPN = new BasicLanguage(nature_JPN, pokeType_JPN);
            ENG = new Language(nature_ENG, pokeType_ENG, Resources.PokeName_eng, Resources.Ability_eng, Resources.MapName_eng);
        }
        class BasicLanguage : Language
        {
            internal BasicLanguage(string[] nature, string[] pokeType)
            {
                Nature = nature;
                PokeType = pokeType;
            }
            internal override string ToJPN(Category cat, string txt)
            {
                return txt;
            }
            internal override string Translate(Category cat, string txt)
            {
                return txt;
            }
        }
    }

    public static class LangExtension
    {
        public static string ToJPN(this string txt, Category cat, Language from)
        {
            return from.ToJPN(cat, txt);
        }
        /// <summary>
        /// from JPN
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="cat"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string Translate(this string txt, Category cat, Language to)
        {
            return to.Translate(cat, txt);
        }
        public static string Translate(this string txt, Category cat, Language from, Language to)
        {
            return to.Translate(cat, from.ToJPN(cat, txt));
        }
    }
}