using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{
    public partial class Mark
    {
        public string Name { get; }
        public string Title { get; }
        public MarkCategory Category { get; }

        internal Mark(string name, string title, MarkCategory category)
        {
            Name = name;
            Title = title;
            Category = category;
        }
    }

    public partial class Mark
    {
        public static Mark Null { get; } = new Mark("", "", 0);

        public static Mark Lunchtime { get; } = new Mark("しょうご", "はらペコの", MarkCategory.Time);
        public static Mark SleepyTime { get; } = new Mark("しょうし", "おねむな", MarkCategory.Time);
        public static Mark Dusk { get; } = new Mark("たそがれ", "そろそろねむい", MarkCategory.Time);
        public static Mark Dawn { get; } = new Mark("あかつき", "はやくにめざめた", MarkCategory.Time);
        public static class Time
        {
            public static Mark Lunchtime { get => Mark.Lunchtime; }
            public static Mark SleepyTime { get => Mark.SleepyTime; }
            public static Mark Dusk { get => Mark.Dusk; }
            public static Mark Dawn { get => Mark.Dawn; }
            public static IReadOnlyList<Mark> Marks { get => new[] { Lunchtime, SleepyTime, Dusk, Dawn }; }
        }

        public static Mark Cloudy { get; } = new Mark("どんてん", "くもをみつめる", MarkCategory.Weather);
        public static Mark Rainy { get; } = new Mark("あめふり", "あめにむせぶ", MarkCategory.Weather);
        public static Mark Stomy { get; } = new Mark("いかづち", "かみなりにさわぐ", MarkCategory.Weather);
        public static Mark Snowy { get; } = new Mark("こうせつ", "ゆきにころがる", MarkCategory.Weather);
        public static Mark Blizzard { get; } = new Mark("ごうせつ", "こごえふるえる", MarkCategory.Weather);
        public static Mark Dry { get; } = new Mark("かんそう", "のどカラカラの", MarkCategory.Weather);
        public static Mark Sandstorm { get; } = new Mark("さじん", "すなにまみれる", MarkCategory.Weather);
        public static Mark Misty { get; } = new Mark("のうむ", "きりにとまどう", MarkCategory.Weather);
        public static class Weather
        {
            public static Mark Cloudy { get => Mark.Cloudy; }
            public static Mark Rainy { get => Mark.Rainy; }
            public static Mark Stomy { get => Mark.Stomy; }
            public static Mark Snowy { get => Mark.Snowy; }
            public static Mark Blizzard { get => Mark.Blizzard; }
            public static Mark Dry { get => Mark.Dry; }
            public static Mark Sandstorm { get => Mark.Sandstorm; }
            public static Mark Misty { get => Mark.Misty; }
            public static IReadOnlyList<Mark> Marks { get => new[] { Cloudy, Rainy, Stomy, Snowy, Blizzard, Dry, Sandstorm }; }
        }

        public static Mark Fishing { get; } = new Mark("つりあげられた", "つりたてピチピチの", MarkCategory.Encounter);
        public static Mark Curry { get; } = new Mark("カレーの", "カレーずきな", MarkCategory.Encounter);
        public static class Encounter
        {
            public static Mark Fishing { get => Mark.Fishing; }
            public static Mark Curry { get => Mark.Curry; }
        }

        public static Mark Uncommon { get; } = new Mark("ときどきみる", "ひとになれてる", MarkCategory.Rarely);
        public static Mark Rare { get; } = new Mark("みたことのない", "ひとをしらない", MarkCategory.Rarely);
        public static class Rarely
        {
            public static Mark Uncommon { get => Mark.Uncommon; }
            public static Mark Rare { get => Mark.Rare; }
        }

        public static Mark Rowdy { get; } = new Mark("わんぱくなあかし", "あばれんぼうの ", MarkCategory.Personality);
        public static Mark AbsentMinded { get; } = new Mark("のうてんきなあかし", "なにもかんがえてない ", MarkCategory.Personality);
        public static Mark Jittery { get; } = new Mark("きんちょうのあかし", "ドキドキしてる ", MarkCategory.Personality);
        public static Mark Excited { get; } = new Mark("きたいのあかし", "ワクワクしてる ", MarkCategory.Personality);
        public static Mark Charismatic { get; } = new Mark("カリスマのあかし", "オーラをかんじる ", MarkCategory.Personality);
        public static Mark Calmness { get; } = new Mark("れいせいのあかし", "クールな ", MarkCategory.Personality);
        public static Mark Intense { get; } = new Mark("じょうねつのあかし", "アグレッシブな ", MarkCategory.Personality);
        public static Mark ZonedOut { get; } = new Mark("ゆだんのあかし", "ボーっとしてる ", MarkCategory.Personality);
        public static Mark Joyful { get; } = new Mark("たこうのあかし", "しあわせそうな ", MarkCategory.Personality);
        public static Mark Angry { get; } = new Mark("ふんぬのあかし", "プンプンおこる ", MarkCategory.Personality);
        public static Mark Smiley { get; } = new Mark("びしょうのあかし", "ニコニコわらう ", MarkCategory.Personality);
        public static Mark Teary { get; } = new Mark("ひそうのあかし", "メソメソなく ", MarkCategory.Personality);
        public static Mark Upbeat { get; } = new Mark("かいちょうのあかし", "ごきげんな ", MarkCategory.Personality);
        public static Mark Peeved { get; } = new Mark("げきはつのあかし", "ふきげんな ", MarkCategory.Personality);
        public static Mark Intellectual { get; } = new Mark("りせいのあかし", "ちてきな ", MarkCategory.Personality);
        public static Mark Ferocious { get; } = new Mark("ほんのうのあかし", "あれくるう ", MarkCategory.Personality);
        public static Mark Crafty { get; } = new Mark("こうかつのあかし", "スキをねらう ", MarkCategory.Personality);
        public static Mark Scowling { get; } = new Mark("こわもてのあかし", "いかつい ", MarkCategory.Personality);
        public static Mark Kindly { get; } = new Mark("やさがたのあかし", "やさしげな ", MarkCategory.Personality);
        public static Mark Flustered { get; } = new Mark("どうようのあかし", "あわてんぼうの ", MarkCategory.Personality);
        public static Mark PumpedUp { get; } = new Mark("こうようのあかし", "やるきまんまんの ", MarkCategory.Personality);
        public static Mark ZeroEnergy { get; } = new Mark("けんたいのあかし", "やるきゼロの ", MarkCategory.Personality);
        public static Mark Prideful { get; } = new Mark("じしんのあかし", "ふんぞりかえった ", MarkCategory.Personality);
        public static Mark Unsure { get; } = new Mark("ふしんのあかし", "じしんのない ", MarkCategory.Personality);
        public static Mark Humble { get; } = new Mark("ぼくとつのあかし", "そぼくな ", MarkCategory.Personality);
        public static Mark Thorny { get; } = new Mark("ふじゅんのあかし", "きどっている ", MarkCategory.Personality);
        public static Mark Vigor { get; } = new Mark("げんきのあかし", "げんきいっぱいの ", MarkCategory.Personality);
        public static Mark Slump { get; } = new Mark("ふちょうのあかし", "どこかくたびれた ", MarkCategory.Personality);
        public static class Personality
        {
            public static Mark Rowdy { get => Mark.Rowdy; }
            public static Mark AbsentMinded { get => Mark.AbsentMinded; }
            public static Mark Jittery { get => Mark.Jittery; }
            public static Mark Excited { get => Mark.Excited; }
            public static Mark Charismatic { get => Mark.Charismatic; }
            public static Mark Calmness { get => Mark.Calmness; }
            public static Mark Intense { get => Mark.Intense; }
            public static Mark ZonedOut { get => Mark.ZonedOut; }
            public static Mark Joyful { get => Mark.Joyful; }
            public static Mark Angry { get => Mark.Angry; }
            public static Mark Smiley { get => Mark.Smiley; }
            public static Mark Teary { get => Mark.Teary; }
            public static Mark Upbeat { get => Mark.Upbeat; }
            public static Mark Peeved { get => Mark.Peeved; }
            public static Mark Intellectual { get => Mark.Intellectual; }
            public static Mark Ferocious { get => Mark.Ferocious; }
            public static Mark Crafty { get => Mark.Crafty; }
            public static Mark Scowling { get => Mark.Scowling; }
            public static Mark Kindly { get => Mark.Kindly; }
            public static Mark Flustered { get => Mark.Flustered; }
            public static Mark PumpedUp { get => Mark.PumpedUp; }
            public static Mark ZeroEnergy { get => Mark.ZeroEnergy; }
            public static Mark Prideful { get => Mark.Prideful; }
            public static Mark Unsure { get => Mark.Unsure; }
            public static Mark Humble { get => Mark.Humble; }
            public static Mark Thorny { get => Mark.Thorny; }
            public static Mark Vigor { get => Mark.Vigor; }
            public static Mark Slump { get => Mark.Slump; }
            public static IReadOnlyList<Mark> Marks
            {
                get => new[] {
                    Rowdy,AbsentMinded,Jittery,Excited,Charismatic,Calmness,Intense,
                    ZonedOut,Joyful,Angry,Smiley,Teary,Upbeat,Peeved,
                    Intellectual,Ferocious,Crafty,Scowling,Kindly,Flustered,PumpedUp,
                    ZeroEnergy,Prideful,Unsure,Humble,Thorny,Vigor,Slump
                };
            }
        }

    }

    public enum MarkCategory
    {
        None,
        Encounter,
        Weather,
        Time,
        Rarely,
        Personality,
    }
}
