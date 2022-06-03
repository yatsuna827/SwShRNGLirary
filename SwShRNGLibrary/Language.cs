using System.Linq;
using SwShRNGLibrary.Properties;

using PokemonStandardLibrary.Language;
using PokemonStandardLibrary.Language.Gen8;

namespace SwShRNGLibrary
{
    public static class MapNamesLanguageSet
    {
        private static (string, string)[] Convert(this string source) => source.Replace("\r\n", "\n")
            .Split(new[] { '\n', '\r' })
            .Select(_ => {
                var pair = _.Split(',');
                return (pair[0], pair[1]);
            }).ToArray();
        public static ILanguage ENG = Gen8LanguageSet.ENG.Extends(Resources.MapName_eng.Convert());
    }
}