﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwShRNGLibrary
{
    public static class XoroshiroReverse
    {
        private static readonly (ulong s0, ulong s1)[] matrix = new (ulong s0, ulong s1)[128]
        {
            (0xFDBEDC250235CA33UL, 0xD8CB345740F9BA96UL),
            (0xB6B9E27E28E4A2C1UL, 0x913D815E0064F882UL),
            (0xF6DF7010FFD0EEA1UL, 0x31B9BE74D34C39BBUL),
            (0x7336724BFD12B618UL, 0xB183EDFC498E255BUL),
            (0x9B45DED9BF145FC7UL, 0x5461A3F3BC19D387UL),
            (0x64033FF49253C65DUL, 0xFDECA5AD78245215UL),
            (0x1F079FF7D9DD7D25UL, 0x00E51DE5C6A2F5C9UL),
            (0x0F9F42C404F2509AUL, 0x3EE019887E001F91UL),
            (0x3B550DC415EA8F03UL, 0x172370DF0B92880FUL),
            (0x9DA3B4DA43C95D3FUL, 0x32B52E823CA3E18AUL),
            (0x4BD61B01154B2B99UL, 0x9BCEB37B087E737DUL),
            (0x335BEE226394B14EUL, 0xD9841BF4E7590AEBUL),
            (0xEC0D4A72D52D83C7UL, 0x787BD8281C37D01CUL),
            (0xB7008E864C11523DUL, 0xCA56A0F331E95E32UL),
            (0x97CCED3A112B92F7UL, 0xC3A80824CFEC16EFUL),
            (0x7FE4AFA0A243DB42UL, 0x37C9C7F9C20BD5A3UL),
            (0x818EC1C1C9C83421UL, 0x0787144063C4E56AUL),
            (0xB81557DC42FA4E94UL, 0x7EB440562794F77DUL),
            (0x032A65A66240057CUL, 0xD8B53CA7EC3A6A22UL),
            (0x172229303CEC2398UL, 0xB320D5116681FE11UL),
            (0xCED410164224E26DUL, 0xDB42F8DF45C4F18DUL),
            (0xAE6CD25C70C35AA0UL, 0xE868420C8727A8A8UL),
            (0xC76AA7EFB11F232AUL, 0x5C25D9DDAABCF72DUL),
            (0xCDD6BEE14AB1DBEDUL, 0x6DBB0B456344CBB8UL),
            (0xF01C40E9DE40AF5EUL, 0xB07F1982456DBF00UL),
            (0xF9DFE3608E390D7CUL, 0xE04D8494C2EBD061UL),
            (0xA0B2D32BD0866A0DUL, 0xF31CCDD0663A10F9UL),
            (0x1102A5613D64E2B4UL, 0xED68DA45EA97E0F4UL),
            (0xCE3A63606E5F9680UL, 0xEE93D49DEA938E3FUL),
            (0xDF02F45956ED70ABUL, 0xE70722DE4FF3E4DAUL),
            (0x45923A161F5A32ABUL, 0x92B4CE9449E0B3F0UL),
            (0x8F7FEC78AAEFFA7BUL, 0xBFAA7B30E072451AUL),
            (0xC02A74D7A75D4411UL, 0xBFFEC08F2FFC4394UL),
            (0xF7EFCF7B86DE880DUL, 0x00A4A26FB47D5CD9UL),
            (0x2EC57504A6EADF7AUL, 0x61E951FA77EEA266UL),
            (0xE1528C9734D8436AUL, 0x5B48BFBC347BE15AUL),
            (0x8045CC1E050DDEF2UL, 0x575B66FBB092C020UL),
            (0x3284AF938E333ACDUL, 0x55474C2C66924C4DUL),
            (0x081DC16A5E588048UL, 0x2D5722C3D37E6556UL),
            (0xCF922710084495B6UL, 0xDB1E308234C9B540UL),
            (0xFB7DB84A046B9466UL, 0xB19668AE81F3752DUL),
            (0x5A9A096B86A6BC7BUL, 0x7599797096FA0B02UL),
            (0x9E36E35275BCEBE6UL, 0x2703968A204D6CE1UL),
            (0xAB5B55C0CC97D008UL, 0x4892A3AD2075889CUL),
            (0x2D3D462DB3155C63UL, 0x0B8AA80F9251434DUL),
            (0x520ED4BB6B85A69DUL, 0xD571214703FB07A7UL),
            (0x23E94DDACF001581UL, 0xB7F304F1353D058BUL),
            (0x5E80ADDB28A00042UL, 0xE936D7E0A5A9D57FUL),
            (0xC1BF97CACB4C5C1EUL, 0xD684F774E5624F80UL),
            (0xA9D7346960DC89A9UL, 0x057B2A896CF9E131UL),
            (0xC8CE3207849311CCUL, 0x46FCB052526D519EUL),
            (0xD715CD1DBC366589UL, 0x69D581A220AD4037UL),
            (0xCF3CCDC399D3833BUL, 0xF41F4AA1DF89ED60UL),
            (0xBEA92C8C4C30337BUL, 0x4CDD09D92F236B9EUL),
            (0xDC55260CAAD1C9DEUL, 0x96E83C45CC39B56BUL),
            (0xEF4F8FFAF1F96ABCUL, 0xC077F82C96055169UL),
            (0x5FE7DF3F8A8C5794UL, 0xC140C1B27781E2BDUL),
            (0x307C938756200C28UL, 0xF4CED48370F77B65UL),
            (0xFFC2EC195D6E4AE9UL, 0xF92B7953F65117FBUL),
            (0x83579840F833F6A5UL, 0xFD0BF85E53541AA0UL),
            (0x7DA4548588D664C5UL, 0x66413B191FF9938DUL),
            (0x265C71D870E0820DUL, 0x4BE79A8367A37839UL),
            (0x0380B21E2D3C174CUL, 0x91D69F4034EBE5BEUL),
            (0xC5DE1DCE9C3D4867UL, 0x375B93BA9220BB50UL),
            (0xF4E5535355AF6A32UL, 0xD8C3B6D911C2F943UL),
            (0x880BA09FA811FB39UL, 0xC6D7781CC76C4150UL),
            (0x8557736375CDD805UL, 0x75C954175599262DUL),
            (0x375A4C6A9C3AAA21UL, 0x9A1E1727ABDCA4A4UL),
            (0x80F325477229BC2AUL, 0xF7284C1B567B37C5UL),
            (0xF7501BD08AEB4C7BUL, 0xD34C4D3EDAACB24DUL),
            (0x02E1EDC2A56792EEUL, 0xB6DC22DF7EDA1BD0UL),
            (0x4E216A9725B6F1ECUL, 0xAA16FD7827A9F5CCUL),
            (0x8C408186F573CD1BUL, 0xEFE16615F9D5D791UL),
            (0x0F33E907A4876EE8UL, 0x52A4590F291DC3AEUL),
            (0x1DEF9072ECD4CD66UL, 0xEAA7E7511BD487CCUL),
            (0x8BA2700D4F11165AUL, 0x03512F31587D1CDFUL),
            (0xFB2B1354E6A50772UL, 0x7C9322D9FBD19D45UL),
            (0x6EF33070CF99653DUL, 0x122E6A422C23CA1CUL),
            (0x6D5B9E34CE37DEC6UL, 0xD218A6A6CD36CD8EUL),
            (0x6F627F1B173D077AUL, 0x982DB026D0192F8CUL),
            (0xD42F120B874EABF6UL, 0xC9C17FFC82F78ED7UL),
            (0xC6F1290211DB866CUL, 0x20F8ED3BDF42DB31UL),
            (0x806FCEF62629D3C8UL, 0xD48C545615F1F5DEUL),
            (0xF70652478BB52E35UL, 0x03FFFF384BBFDAB9UL),
            (0x3C351057D41C019EUL, 0xA8C75F1E6AED268CUL),
            (0x4EE1AC6EAE8747CAUL, 0x5DF7368B1D78224DUL),
            (0x57D9281B82A79DF9UL, 0xC381CA1C735612D1UL),
            (0xD21BF6BE62AB8526UL, 0x15606A856E440DC4UL),
            (0x53D41E2BD7F7D9C8UL, 0xF0808A952C3367DCUL),
            (0x271215740A7B2693UL, 0xD1F2F9E003DDE9D1UL),
            (0x486A0169205F1648UL, 0x55FD3EA03B93BFD5UL),
            (0xF1938CB1C1A096D1UL, 0x5CE73579982951A6UL),
            (0xD776AD2969E6C0F3UL, 0x6335A62A35704F4BUL),
            (0xD5AC129FBD76C0A1UL, 0x0C958AF0E4C1A881UL),
            (0x227D2DB570B5C6E8UL, 0xA68062554F62B96DUL),
            (0x87C229801926222EUL, 0x51F261881DBF6944UL),
            (0x27D1CC00E5117CE6UL, 0x196ED87CCB9E64DAUL),
            (0xFB779B889B8B0DD0UL, 0x6D14655F18F228ACUL),
            (0xBBDC2F5CBA1D9CA3UL, 0x35CC298469860A2BUL),
            (0xF08800EC89A5795DUL, 0x8482F11996EB0DACUL),
            (0x9E221E114C030322UL, 0xF213D4E5E2A9477CUL),
            (0x0000000000000000UL, 0x8000000000000000UL),
            (0x1F592170C07AACFCUL, 0x2BF57CA163845CE9UL),
            (0x39C401B9C50E9B52UL, 0x22387531C36A8FCBUL),
            (0x269BD8AB9B595E1CUL, 0x95CABC2AD9B4E115UL),
            (0x8976BA744D53A1F6UL, 0xD1A0B6B35DACD3CBUL),
            (0x49A992120C5C4513UL, 0x17507449D144702CUL),
            (0x0A5EFEA1959027E5UL, 0xDB18DEDA74A1D6E6UL),
            (0xA0DF142990A250BBUL, 0x4A7B72782CD4F52EUL),
            (0x5B8AC621704CA10CUL, 0x7C610B657923AFCFUL),
            (0xCDE5E955D86A49EBUL, 0xB00CFA81A242B0F8UL),
            (0x2FB10202D702A37FUL, 0xB8B0EB522148DBB2UL),
            (0x5C7CCF179642D38AUL, 0x6D6A9A62DF920B1AUL),
            (0x0F3EEB283209125AUL, 0x82703C3FDB6E8746UL),
            (0x6CF9DF7B41C41B80UL, 0x6C3C65588EE54A17UL),
            (0x79E67E3C44437618UL, 0x88DC160629F0CC5AUL),
            (0x8843685DDABF6E1CUL, 0x57F23BEF89097D17UL),
            (0x2E7D2E5E0C8E1FEBUL, 0xE727749958041434UL),
            (0x3F723F6F2990E47CUL, 0x2F2656B6FC6B1626UL),
            (0x41A2D5A82234EB5AUL, 0x061CB478FCE5CFFEUL),
            (0xF4BFFA00F061D6D6UL, 0xD86BD453BE02B3BEUL),
            (0x7DDD479BE0D121F9UL, 0xB9C692A7BF094A6AUL),
            (0xF046BF196F220EB5UL, 0x5ACFBA43CD2FC572UL),
            (0xCCF4004132110F69UL, 0xCFD648A7C468D314UL),
            (0x8B4B6394BFC07F65UL, 0xBC69F1A72E1DC2D4UL),
            (0x51E42F6104DBBB4BUL, 0x207FC98BB4AF6C6EUL),
            (0xEBCB3CB169EC45F7UL, 0x98DBFFFD4806BD32UL),
            (0xF0C1AE9A53A1EE22UL, 0xD374B8FF0649767CUL),
        };

        private static (ulong s0, ulong s1) Products(this (ulong s0, ulong s1) state, (ulong s0, ulong s1)[] matrix)
        {
            var result = (0UL, 0UL);
            for (int i = 0; i < 64; i++)
            {
                var (s0, s1) = matrix[i];
                // 論理積を取り、立っているビットの偶奇を見ることで畳み込み計算ができる。
                // convolution; 畳み込み
                var conv = (ulong)((s0 & state.s0).PopCount() + (s1 & state.s1).PopCount()) & 1;
                result.Item1 |= (conv << i);
            }
            for (int i = 0; i < 64; i++)
            {
                var (s0, s1) = matrix[i + 64];
                var conv = (ulong)((s0 & state.s0).PopCount() + (s1 & state.s1).PopCount()) & 1;
                result.Item2 |= (conv << i);
            }

            return result;
        }
        private static int PopCount(this ulong x)
        {
            x = (x & 0x5555555555555555UL) + ((x & 0xAAAAAAAAAAAAAAAAUL) >> 1);
            x = (x & 0x3333333333333333UL) + ((x & 0xCCCCCCCCCCCCCCCCUL) >> 2);
            x = (x & 0x0F0F0F0F0F0F0F0FUL) + ((x & 0xF0F0F0F0F0F0F0F0UL) >> 4);

            x += x >> 8;
            x += x >> 16; 
            x += x >> 32;
            return (int)(x & 0x7F);
        }

        /// <summary>
        /// 観測された乱数値の最下位bit列から現在の状態を算出します。
        /// bit列はbits1の下位bitから順に並べてください。
        /// </summary>
        /// <param name="bits1"></param>
        /// <param name="bits2"></param>
        /// <returns></returns>
        public static (ulong s0, ulong s1) ReverseStateByLSB(ulong bits1, ulong bits2)
            => (bits1, bits2).Products(matrix);
        public static (ulong s0, ulong s1) ReverseStateByLSB((ulong bits1, ulong bits2) bits)
            => bits.Products(matrix);
    }
}
