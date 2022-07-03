using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{
    public enum Time
    {
        Others,
        Noon,
        Midnight,
        Dusk,
        Dawn,
    }
    public enum Weather
    {
        Normal, Cloudy, Rain, Thunderstorm, Snow, Blizzard, HarshSunlight, Sandstorm, Fog
    }

    public static class Ext
    {
        private static readonly string[] _weatherJp = new string[] { "晴れ", "曇り", "雨", "豪雨", "雪", "吹雪", "日照り", "砂嵐", "霧" };
        private static readonly string[] _timeJp = new string[] { "", "正午", "真夜中", "早朝", "夕方" };

        public static string ToJapanese(this Weather weather) => _weatherJp[(int)weather];
        public static string ToJapanese(this Time time) => _timeJp[(int)time];
    }
}
