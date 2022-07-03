using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{
    public enum Rom { Sword, Shield }
    public enum Region { WildArea, IsleOfArmor, CrownTundra } 
    public static class CommonExtension
    {
        static private readonly string[] Nature_GER =
        {
            "Robust","Solo","Mutig","Hart","Frech",
            "Kühn","Sanft","Locker","Pfiffig","Lasch",
            "Scheu","Hastig","Ernst","Froh","Niav",
            "Mäßig","Mild","Ruhig","Zaghaft","Hitzig",
            "Still","Zart","Forsch","Sacht","Kauzig",
            "---"
        };
        static private readonly string[] Nature_FRA =
        {
            "Hardi", "Solo", "Brave", "Rigide", "Mauvais",
            "Assuré", "Docile", "Relax", "Malin", "Lâche",
            "Timide", "Présseé", "Sérieux", "Jovial", "Naif",
            "Modeste", "Doux", "Discret", "Pudique", "Foufou",
            "Calme", "Gentil", "Malpoli", "Prudent", "Bizarre",
            "---"
        };
        static private readonly string[] Nature_ITA =
        {
            "Ardita", "Schiva", "Audace", "Decisa", "Birbona",
            "Sicura", "Docile", "Placida", "Scaltra", "Fiacca",
            "Timida", "Lesta", "Seria", "Allegra", "Ingenua",
            "Modesta", "Mite", "Quieta", "Ritrosa", "Ardente",
            "Calma", "Gentile", "Vivace", "Cauta", "Furba",
            "---"
        };
        static private readonly string[] Nature_SPA =
        {
            "Fuerte", "Huraña", "Audaz", "Firme", "Pícara",
            "Osada", "Dócil", "Plácida", "Agitada", "Floja",
            "Miedosa", "Activa", "Seria", "Alegre", "Ingenua",
            "Modesta", "Afable", "Mansa", "Tímida", "Alocada",
            "Serena", "Amable", "Grosera", "Cauta", "Rara",
            "---"
        };
        static private readonly string[] Nature_KOR =
        {
            "노력", "외로움", "용감", "고집", "개구쟁이",
            "대담", "온순", "무사태평", "장난꾸러기", "촐랑",
            "겁쟁이", "성급", "성실", "명랑", "천진난만",
            "조심", "의젓", "냉정", "수줍음", "덜렁",
            "차분", "얌전", "건방", "신중", "변덕",
            "---"
        };
        static private readonly string[] Nature_ZHTW =
        {
            "勤奮", "怕寂寞", "勇敢", "固執", "頑皮",
            "大膽", "坦率", "悠閒", "淘氣", "樂天",
            "膽小", "急躁", "認真", "爽朗", "天真",
            "內斂", "慢吞吞", "冷靜", "害羞", "馬虎",
            "溫和", "溫順", "自大", "慎重", "浮躁",
            "---"
        };
        static private readonly string[] Nature_ZHCN =
        {
            "勤奋", "怕寂寞", "勇敢", "固执", "顽皮",
            "大胆", "坦率", "悠闲", "淘气", "乐天",
            "胆小", "急躁", "认真", "爽朗", "天真",
            "内敛", "慢吞吞", "冷静", "害羞", "马虎",
            "温和", "温顺", "自大", "慎重", "浮躁",
            "---"
        };

        static private readonly string[] romname = new string[] { "---", "剣", "盾" };
        internal static string ToKanji(this Rom rom)
        {
            return romname[(int)rom];
        }
        public static string ToJapanese(this Rom version)
        {
            return version == Rom.Sword ? "ソード" : "シールド";
        }
        public static string ToAbbreviation(this Rom version)
        {
            return version == Rom.Sword ? "SW" : "SH";
        }
        public static string ToJapanese(this Region region)
        {
            if (region == Region.WildArea) return "ワイルドエリア";
            if (region == Region.IsleOfArmor) return "ヨロイ島";
            return "カンムリ雪原";
        }
    }
}
