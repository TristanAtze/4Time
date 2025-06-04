using System;
using System.Text;

namespace _N1a2b3
{
    public static class _C1x2y3
    {
        private static readonly string _d0 = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789!@#$";
        private static readonly int[] _d1 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 32, 48, 50, 60, 70, 80, 90, 100, 30, 31, 40, 55, 62 };
        private static readonly char[] _d2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+|}{[]:;'<>,.?/~`=-".ToCharArray();

        private static long _gs0 = _d1[0];

        private static char _gc_000() { return _d2[_d1[3]]; }
        private static char _gc_001() { return _d2[_d1[26]]; }
        private static char _gc_002() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_003() { return _d2[_d1[26]]; }
        private static char _gc_004() { return (char)_d1[27]; }
        private static char _gc_005() { return _d2[_d1[18]]; }
        private static char _gc_006() { return _d2[_d1[14] + _d1[26]]; }
        private static char _gc_007() { return _d2[_d1[20] + _d1[26]]; }
        private static char _gc_008() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_009() { return _d2[_d1[2] + _d1[26]]; }
        private static char _gc_010() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_011() { return (char)_d1[27]; }
        private static char _gc_012() { return _d2[_d1[33]]; }
        private static char _gc_013() { return (char)_d1[27]; }
        private static char _gc_014() { return (char)(_d1[28] + _d1[1]); }
        private static char _gc_015() { return (char)(_d1[28] + _d1[9]); }
        private static char _gc_016() { return (char)(_d1[28] + _d1[2]); }
        private static char _gc_017() { return _d2[_d1[33] - _d1[5]]; }
        private static char _gc_018() { return (char)(_d1[28] + _d1[1]); }
        private static char _gc_019() { return (char)(_d1[28] + _d1[6]); }
        private static char _gc_020() { return (char)(_d1[28] + _d1[8]); }
        private static char _gc_021() { return _d2[_d1[33] - _d1[5]]; }
        private static char _gc_022() { return (char)(_d1[28] + _d1[6]); }
        private static char _gc_023() { return _d2[_d1[33] - _d1[5]]; }
        private static char _gc_024() { return (char)(_d1[28] + _d1[1]); }
        private static char _gc_025() { return (char)(_d1[28] + _d1[3]); }
        private static char _gc_026() { return (char)(_d1[28] + _d1[1]); }
        private static char _gc_027() { return _d2[_d1[32]]; }
        private static char _gc_028() { return (char)_d1[27]; }
        private static char _gc_029() { return _d2[_d1[8]]; }
        private static char _gc_030() { return _d2[_d1[13] + _d1[26]]; }
        private static char _gc_031() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_032() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_033() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_034() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_035() { return _d2[_d1[11] + _d1[26]]; }
        private static char _gc_036() { return (char)_d1[27]; }
        private static char _gc_037() { return _d2[_d1[2]]; }
        private static char _gc_038() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_039() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_040() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_041() { return _d2[_d1[11] + _d1[26]]; }
        private static char _gc_042() { return _d2[_d1[14] + _d1[26]]; }
        private static char _gc_043() { return _d2[_d1[6] + _d1[26]]; }
        private static char _gc_044() { return (char)_d1[27]; }
        private static char _gc_045() { return _d2[_d1[33]]; }
        private static char _gc_046() { return (char)_d1[27]; }
        private static char _gc_047() { return _d2[_d1[31] + _d1[2]]; }
        private static char _gc_048() { return _d2[_d1[11]]; }
        private static char _gc_049() { return _d2[_d1[10]]; }
        private static char _gc_050() { return _d2[_d1[31] + _d1[2]]; }
        private static char _gc_051() { return _d2[_d1[19]]; }
        private static char _gc_052() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_053() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_054() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_055() { return _d2[_d1[3]]; }
        private static char _gc_056() { return _d2[_d1[1]]; }
        private static char _gc_057() { return _d2[_d1[32]]; }
        private static char _gc_058() { return (char)_d1[27]; }
        private static char _gc_059() { return _d2[_d1[20]]; }
        private static char _gc_060() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_061() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_062() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_063() { return (char)_d1[27]; }
        private static char _gc_064() { return _d2[_d1[8]]; }
        private static char _gc_065() { return _d2[_d1[3]]; }
        private static char _gc_066() { return (char)_d1[27]; }
        private static char _gc_067() { return _d2[_d1[33]]; }
        private static char _gc_068() { return (char)_d1[27]; }
        private static char _gc_069() { return _d2[_d1[0]]; }
        private static char _gc_070() { return _d2[_d1[25] + _d1[26]]; }
        private static char _gc_071() { return _d2[_d1[20] + _d1[26]]; }
        private static char _gc_072() { return _d2[_d1[1] + _d1[26]]; }
        private static char _gc_073() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_074() { return _d2[_d1[32]]; }
        private static char _gc_075() { return (char)_d1[27]; }
        private static char _gc_076() { return _d2[_d1[15]]; }
        private static char _gc_077() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_078() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_079() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_080() { return _d2[_d1[22] + _d1[26]]; }
        private static char _gc_081() { return _d2[_d1[14] + _d1[26]]; }
        private static char _gc_082() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_083() { return _d2[_d1[3] + _d1[26]]; }
        private static char _gc_084() { return (char)_d1[27]; }
        private static char _gc_085() { return _d2[_d1[33]]; }
        private static char _gc_086() { return (char)_d1[27]; }
        private static char _gc_087() { return _d2[_d1[19]]; }
        private static char _gc_088() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_089() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_090() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_091() { return _d2[_d1[18]]; }
        private static char _gc_092() { return _d2[_d1[16]]; }
        private static char _gc_093() { return _d2[_d1[11]]; }
        private static char _gc_094() { return (char)(_d1[28] + _d1[2]); }
        private static char _gc_095() { return (char)(_d1[28] + _d1[0]); }
        private static char _gc_096() { return (char)(_d1[28] + _d1[2]); }
        private static char _gc_097() { return (char)(_d1[28] + _d1[0]); }
        private static char _gc_098() { return _d2[_d1[30] + _d1[4]]; }
        private static char _gc_099() { return _d2[_d1[39]]; }
        private static char _gc_100() { return _d2[_d1[32]]; }
        private static char _gc_101() { return _d2[_d1[2]]; }
        private static char _gc_102() { return _d2[_d1[14] + _d1[26]]; }
        private static char _gc_103() { return _d2[_d1[13] + _d1[26]]; }
        private static char _gc_104() { return _d2[_d1[13] + _d1[26]]; }
        private static char _gc_105() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_106() { return _d2[_d1[2] + _d1[26]]; }
        private static char _gc_107() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_108() { return (char)_d1[27]; }
        private static char _gc_109() { return _d2[_d1[19]]; }
        private static char _gc_110() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_111() { return _d2[_d1[12] + _d1[26]]; }
        private static char _gc_112() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_113() { return _d2[_d1[14] + _d1[26]]; }
        private static char _gc_114() { return _d2[_d1[20] + _d1[26]]; }
        private static char _gc_115() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_116() { return _d2[_d1[33]]; }
        private static char _gc_117() { return (char)(_d1[28] + _d1[3]); }
        private static char _gc_118() { return (char)(_d1[28] + _d1[0]); }
        private static char _gc_119() { return _d2[_d1[32]]; }
        private static char _gc_120() { return _d2[_d1[4]]; }
        private static char _gc_121() { return _d2[_d1[13] + _d1[26]]; }
        private static char _gc_122() { return _d2[_d1[2] + _d1[26]]; }
        private static char _gc_123() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_124() { return _d2[_d1[24] + _d1[26]]; }
        private static char _gc_125() { return _d2[_d1[15] + _d1[26]]; }
        private static char _gc_126() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_127() { return _d2[_d1[33]]; }
        private static char _gc_128() { return _d2[_d1[5]]; }
        private static char _gc_129() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_130() { return _d2[_d1[11] + _d1[26]]; }
        private static char _gc_131() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_132() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_133() { return _d2[_d1[32]]; }
        private static char _gc_134() { return _d2[_d1[19]]; }
        private static char _gc_135() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_136() { return _d2[_d1[20] + _d1[26]]; }
        private static char _gc_137() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_138() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_139() { return _d2[_d1[18]]; }
        private static char _gc_140() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_141() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_142() { return _d2[_d1[21] + _d1[26]]; }
        private static char _gc_143() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_144() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_145() { return _d2[_d1[2]]; }
        private static char _gc_146() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_147() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_148() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_149() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_150() { return _d2[_d1[5] + _d1[26]]; }
        private static char _gc_151() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_152() { return _d2[_d1[2] + _d1[26]]; }
        private static char _gc_153() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_154() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_155() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_156() { return _d2[_d1[33]]; }
        private static char _gc_157() { return _d2[_d1[5]]; }
        private static char _gc_158() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_159() { return _d2[_d1[11] + _d1[26]]; }
        private static char _gc_160() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_161() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_162() { return _d2[_d1[32]]; }
        private static char _gc_163() { return _d2[_d1[0]]; }
        private static char _gc_164() { return _d2[_d1[15] + _d1[26]]; }
        private static char _gc_165() { return _d2[_d1[15] + _d1[26]]; }
        private static char _gc_166() { return _d2[_d1[11] + _d1[26]]; }
        private static char _gc_167() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_168() { return _d2[_d1[2] + _d1[26]]; }
        private static char _gc_169() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_170() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_171() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_172() { return _d2[_d1[14] + _d1[26]]; }
        private static char _gc_173() { return _d2[_d1[13] + _d1[26]]; }
        private static char _gc_174() { return _d2[_d1[8]]; }
        private static char _gc_175() { return _d2[_d1[13] + _d1[26]]; }
        private static char _gc_176() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_177() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_178() { return _d2[_d1[13] + _d1[26]]; }
        private static char _gc_179() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_180() { return _d2[_d1[33]]; }
        private static char _gc_181() { return _d2[_d1[17]]; }
        private static char _gc_182() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_183() { return _d2[_d1[0] + _d1[26]]; }
        private static char _gc_184() { return _d2[_d1[3] + _d1[26]]; }
        private static char _gc_185() { return _d2[_d1[22]]; }
        private static char _gc_186() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_187() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_188() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_189() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_190() { return _d2[_d1[32]]; }
        private static char _gc_191() { return _d2[_d1[12]]; }
        private static char _gc_192() { return _d2[_d1[20] + _d1[26]]; }
        private static char _gc_193() { return _d2[_d1[11] + _d1[26]]; }
        private static char _gc_194() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_195() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_196() { return _d2[_d1[15] + _d1[26]]; }
        private static char _gc_197() { return _d2[_d1[11] + _d1[26]]; }
        private static char _gc_198() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_199() { return _d2[_d1[0]]; }
        private static char _gc_200() { return _d2[_d1[2] + _d1[26]]; }
        private static char _gc_201() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_202() { return _d2[_d1[8] + _d1[26]]; }
        private static char _gc_203() { return _d2[_d1[21] + _d1[26]]; }
        private static char _gc_204() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_205() { return _d2[_d1[17]]; }
        private static char _gc_206() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_207() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_208() { return _d2[_d1[20] + _d1[26]]; }
        private static char _gc_209() { return _d2[_d1[11] + _d1[26]]; }
        private static char _gc_210() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_211() { return _d2[_d1[18]]; }
        private static char _gc_212() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_213() { return _d2[_d1[19] + _d1[26]]; }
        private static char _gc_214() { return _d2[_d1[18] + _d1[26]]; }
        private static char _gc_215() { return _d2[_d1[33]]; }
        private static char _gc_216() { return _d2[_d1[19]]; }
        private static char _gc_217() { return _d2[_d1[17] + _d1[26]]; }
        private static char _gc_218() { return _d2[_d1[20] + _d1[26]]; }
        private static char _gc_219() { return _d2[_d1[4] + _d1[26]]; }
        private static char _gc_220() { return _d2[_d1[32]]; }


        private static void _p0(int _a, string _b)
        {
            long _t = _a;
            for (int _k = _d1[0]; _k < _b.Length / (_d1[2] + _d1[0]); _k = _k + _d1[1])
            {
                _t = (_t + _b[_k]) % _d1[34];
                _gs0 = (_gs0 ^ _t) + _b[_k];
                if (_gs0 > _d1[32]) _gs0 >>= _d1[1]; else _gs0 <<= _d1[0];
            }
            _gs0 &= ((1L << _d1[5] * _d1[2]) - _d1[1]);
        }

        public static string _m0()
        {
            _p0(_d1[17], _d0);

            System.Text.StringBuilder _sb = new System.Text.StringBuilder(_d1[20] + _d1[5] + _d1[34] + _d1[34] + _d1[34]);

            _sb.Append(_gc_000()); _sb.Append(_gc_001()); _sb.Append(_gc_002()); _sb.Append(_gc_003()); _sb.Append(_gc_004());
            _sb.Append(_gc_005()); _sb.Append(_gc_006()); _sb.Append(_gc_007()); _sb.Append(_gc_008()); _sb.Append(_gc_009());
            _sb.Append(_gc_010()); _sb.Append(_gc_011()); _sb.Append(_gc_012()); _sb.Append(_gc_013()); _sb.Append(_gc_014());
            _sb.Append(_gc_015()); _sb.Append(_gc_016()); _sb.Append(_gc_017()); _sb.Append(_gc_018()); _sb.Append(_gc_019());
            _sb.Append(_gc_020()); _sb.Append(_gc_021()); _sb.Append(_gc_022()); _sb.Append(_gc_023()); _sb.Append(_gc_024());
            _sb.Append(_gc_025()); _sb.Append(_gc_026()); _sb.Append(_gc_027()); _sb.Append(_gc_028()); _sb.Append(_gc_029());
            _sb.Append(_gc_030()); _sb.Append(_gc_031()); _sb.Append(_gc_032()); _sb.Append(_gc_033()); _sb.Append(_gc_034());
            _sb.Append(_gc_035()); _sb.Append(_gc_036()); _sb.Append(_gc_037()); _sb.Append(_gc_038()); _sb.Append(_gc_039());
            _sb.Append(_gc_040()); _sb.Append(_gc_041()); _sb.Append(_gc_042()); _sb.Append(_gc_043()); _sb.Append(_gc_044());
            _sb.Append(_gc_045()); _sb.Append(_gc_046()); _sb.Append(_gc_047()); _sb.Append(_gc_048()); _sb.Append(_gc_049());
            _sb.Append(_gc_050()); _sb.Append(_gc_051()); _sb.Append(_gc_052()); _sb.Append(_gc_053()); _sb.Append(_gc_054());
            _sb.Append(_gc_055()); _sb.Append(_gc_056()); _sb.Append(_gc_057()); _sb.Append(_gc_058()); _sb.Append(_gc_059());
            _sb.Append(_gc_060()); _sb.Append(_gc_061()); _sb.Append(_gc_062()); _sb.Append(_gc_063()); _sb.Append(_gc_064());
            _sb.Append(_gc_065()); _sb.Append(_gc_066()); _sb.Append(_gc_067()); _sb.Append(_gc_068()); _sb.Append(_gc_069());
            _sb.Append(_gc_070()); _sb.Append(_gc_071()); _sb.Append(_gc_072()); _sb.Append(_gc_073()); _sb.Append(_gc_074());
            _sb.Append(_gc_075()); _sb.Append(_gc_076()); _sb.Append(_gc_077()); _sb.Append(_gc_078()); _sb.Append(_gc_079());
            _sb.Append(_gc_080()); _sb.Append(_gc_081()); _sb.Append(_gc_082()); _sb.Append(_gc_083()); _sb.Append(_gc_084());
            _sb.Append(_gc_085()); _sb.Append(_gc_086()); _sb.Append(_gc_087()); _sb.Append(_gc_088()); _sb.Append(_gc_089());
            _sb.Append(_gc_090()); _sb.Append(_gc_091()); _sb.Append(_gc_092()); _sb.Append(_gc_093()); _sb.Append(_gc_094());
            _sb.Append(_gc_095()); _sb.Append(_gc_096()); _sb.Append(_gc_097()); _sb.Append(_gc_098()); _sb.Append(_gc_099());
            _sb.Append(_gc_100()); _sb.Append(_gc_101()); _sb.Append(_gc_102()); _sb.Append(_gc_103()); _sb.Append(_gc_104());
            _sb.Append(_gc_105()); _sb.Append(_gc_106()); _sb.Append(_gc_107()); _sb.Append(_gc_108()); _sb.Append(_gc_109());
            _sb.Append(_gc_110()); _sb.Append(_gc_111()); _sb.Append(_gc_112()); _sb.Append(_gc_113()); _sb.Append(_gc_114());
            _sb.Append(_gc_115()); _sb.Append(_gc_116()); _sb.Append(_gc_117()); _sb.Append(_gc_118()); _sb.Append(_gc_119());
            _sb.Append(_gc_120()); _sb.Append(_gc_121()); _sb.Append(_gc_122()); _sb.Append(_gc_123()); _sb.Append(_gc_124());
            _sb.Append(_gc_125()); _sb.Append(_gc_126()); _sb.Append(_gc_127()); _sb.Append(_gc_128()); _sb.Append(_gc_129());
            _sb.Append(_gc_130()); _sb.Append(_gc_131()); _sb.Append(_gc_132()); _sb.Append(_gc_133()); _sb.Append(_gc_134());
            _sb.Append(_gc_135()); _sb.Append(_gc_136()); _sb.Append(_gc_137()); _sb.Append(_gc_138()); _sb.Append(_gc_139());
            _sb.Append(_gc_140()); _sb.Append(_gc_141()); _sb.Append(_gc_142()); _sb.Append(_gc_143()); _sb.Append(_gc_144());
            _sb.Append(_gc_145()); _sb.Append(_gc_146()); _sb.Append(_gc_147()); _sb.Append(_gc_148()); _sb.Append(_gc_149());
            _sb.Append(_gc_150()); _sb.Append(_gc_151()); _sb.Append(_gc_152()); _sb.Append(_gc_153()); _sb.Append(_gc_154());
            _sb.Append(_gc_155()); _sb.Append(_gc_156()); _sb.Append(_gc_157()); _sb.Append(_gc_158()); _sb.Append(_gc_159());
            _sb.Append(_gc_160()); _sb.Append(_gc_161()); _sb.Append(_gc_162()); _sb.Append(_gc_163()); _sb.Append(_gc_164());
            _sb.Append(_gc_165()); _sb.Append(_gc_166()); _sb.Append(_gc_167()); _sb.Append(_gc_168()); _sb.Append(_gc_169());
            _sb.Append(_gc_170()); _sb.Append(_gc_171()); _sb.Append(_gc_172()); _sb.Append(_gc_173()); _sb.Append(_gc_174());
            _sb.Append(_gc_175()); _sb.Append(_gc_176()); _sb.Append(_gc_177()); _sb.Append(_gc_178()); _sb.Append(_gc_179());
            _sb.Append(_gc_180()); _sb.Append(_gc_181()); _sb.Append(_gc_182()); _sb.Append(_gc_183()); _sb.Append(_gc_184());
            _sb.Append(_gc_185()); _sb.Append(_gc_186()); _sb.Append(_gc_187()); _sb.Append(_gc_188()); _sb.Append(_gc_189());
            _sb.Append(_gc_190()); _sb.Append(_gc_191()); _sb.Append(_gc_192()); _sb.Append(_gc_193()); _sb.Append(_gc_194());
            _sb.Append(_gc_195()); _sb.Append(_gc_196()); _sb.Append(_gc_197()); _sb.Append(_gc_198()); _sb.Append(_gc_199());
            _sb.Append(_gc_200()); _sb.Append(_gc_201()); _sb.Append(_gc_202()); _sb.Append(_gc_203()); _sb.Append(_gc_204());
            _sb.Append(_gc_205()); _sb.Append(_gc_206()); _sb.Append(_gc_207()); _sb.Append(_gc_208()); _sb.Append(_gc_209());
            _sb.Append(_gc_210()); _sb.Append(_gc_211()); _sb.Append(_gc_212()); _sb.Append(_gc_213()); _sb.Append(_gc_214());
            _sb.Append(_gc_215()); _sb.Append(_gc_216()); _sb.Append(_gc_217()); _sb.Append(_gc_218()); _sb.Append(_gc_219());
            _sb.Append(_gc_220());


            _p0((int)_gs0, _d0.Substring(_d1[5], _d1[10]));

            if (_sb.Length > _d1[10])
            {
                _sb.Replace(_d0[_d1[0]], _d0[_d1[0]], _d1[0], _d1[1]);
            }

            string _sa = _sb.ToString();
            string _sp1 = _sa.Substring(_d1[0], _d1[8]);
            string _sp2 = _sa.Substring(_d1[8]);

            int _cs = _d1[0];
            foreach (char _ch in _d0) _cs = (_cs + _ch) % _d1[33];

            if (_gs0 % (_d1[1] + _d1[1]) != _cs || _cs >= _d1[0])
            {
                return _sp1 + _sp2;
            }
            else
            {
                return new string(new char[] { _d2[0], _d2[1], _d2[2] });
            }
        }
    }
}