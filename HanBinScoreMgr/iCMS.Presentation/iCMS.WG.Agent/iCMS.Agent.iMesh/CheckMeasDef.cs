using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    class MeasDefGear
    {
        private const byte WV_LEN_GEARS_NUM = 5;
        private const byte EV_LEN_GEARS_NUM = 5;

        private const byte UP_FREQ_GEARS_NUM = 4;
        private const byte DW_FREQ_GEARS_NUM = 7;

        private const byte AEV_BW_GEARS_NUM = 5;
        private const byte AEV_FT_GEARS_NUM = 5;

        //波形长度档位
        static int[,] WaveLenGear = new int[WV_LEN_GEARS_NUM, 2]{ { 0, 1024  },
                                                                  { 1, 2048  },
                                                                  { 2, 4096  },
                                                                  { 3, 8192  },
                                                                  { 4, 16384 } };
        // 特征值波形长度档位
        static int[,] EigenLenGear = new int[EV_LEN_GEARS_NUM, 2]{ { 0, 1024  }, 
                                                                   { 1, 2048  },
                                                                   { 2, 4096  }, 
                                                                   { 3, 8192  },
                                                                   { 4, 16384 } };
        
        //上限频率档位
        static int[,] UpFreqGear = new int[UP_FREQ_GEARS_NUM, 2]{ { 0, 1000  }, 
                                                                  { 1, 2000  },
                                                                  { 2, 5000  }, 
                                                                  { 3, 10000 } };
        //下限频率档位
        static int[,] DwFreqGear = new int[DW_FREQ_GEARS_NUM, 2]{ { 0, 1   }, 
                                                                  { 1, 2   },
                                                                  { 2, 5   }, 
                                                                  { 3, 10  }, 
                                                                  { 4, 20  },
                                                                  { 5, 50  }, 
                                                                  { 6, 100 } };

        //包络带宽档位
        static int[,] EnvBwGear = new int[AEV_BW_GEARS_NUM, 2] { { 0, 100  }, 
                                                                 { 1, 200  },
                                                                 { 2, 500  }, 
                                                                 { 3, 1000 }, 
                                                                 { 4, 2000 } };
        //包络滤波器档位
        static int[,] EnvFtGear = new int[AEV_FT_GEARS_NUM, 2] { { 0, 500   }, 
                                                                 { 1, 1000  },
                                                                 { 2, 2000  }, 
                                                                 { 3, 5000  }, 
                                                                 { 4, 10000 } };

        public static byte EncodeWaveLen(UInt16 len)
        {
            byte cd = 0;

            if (len >= WaveLenGear[(WV_LEN_GEARS_NUM - 1), 1])
                cd = WV_LEN_GEARS_NUM - 1;
            else
            {
                for (int i = 0; i < WV_LEN_GEARS_NUM; i++)
                {
                    if (len <= WaveLenGear[i, 1])
                    {
                        cd = (byte)i;
                        break;
                    }
                }
            }

            return (byte)(cd << 4);
        }

        public static UInt16 DecodeWaveLen(byte cd)
        {
            UInt16 len = 0;
            cd = (byte)(cd >> 4);
            for (int i = 0; i < WV_LEN_GEARS_NUM; i++)
            {
                if (cd == WaveLenGear[i, 0])
                {
                    len = (UInt16)WaveLenGear[i, 1];
                    break;
                }
            }

            return len;
        }

        public static byte EncodeEigenLen(UInt16 len)
        {
            byte cd = 0;

            if (len >= EigenLenGear[(EV_LEN_GEARS_NUM - 1), 1])
                cd = EV_LEN_GEARS_NUM - 1;
            else
            {
                for (int i = 0; i < EV_LEN_GEARS_NUM; i++)
                {
                    if (len <= EigenLenGear[i, 1])
                    {
                        cd = (byte)i;
                        break;
                    }
                }
            }

            return cd;
        }

        public static UInt16 DecodeEigenLen(byte cd)
        {
            UInt16 len = 0;
            for (int i = 0; i < EV_LEN_GEARS_NUM; i++)
            {
                if (cd == EigenLenGear[i, 0])
                {
                    len = (UInt16)EigenLenGear[i, 1];
                    break;
                }
            }

            return len;
        }

        public static byte EncodeUpFreq(UInt16 freq)
        {
            byte cd = 0;

            if (freq >= UpFreqGear[(UP_FREQ_GEARS_NUM - 1), 1])
                cd = UP_FREQ_GEARS_NUM - 1;
            else
            {
                for (int i = 0; i < UP_FREQ_GEARS_NUM; i++)
                {
                    if (freq <= UpFreqGear[i, 1])
                    {
                        cd = (byte)i;
                        break;
                    }
                }
            }

            return (byte)(cd << 4);
        }

        public static UInt16 DecodeUpFreq(byte cd)
        {
            UInt16 freq = 0;
            cd = (byte)(cd >> 4);
            for (int i = 0; i < UP_FREQ_GEARS_NUM; i++)
            {
                if (cd == UpFreqGear[i, 0])
                {
                    freq = (UInt16)UpFreqGear[i, 1];
                    break;
                }
            }
            return freq;
        }

        public static byte EncodeDwFreq(UInt16 freq)
        {
            byte cd = 0;

            if (freq >= DwFreqGear[(DW_FREQ_GEARS_NUM - 1), 1])
                cd = DW_FREQ_GEARS_NUM - 1;
            else
            {
                for (int i = 0; i < DW_FREQ_GEARS_NUM; i++)
                {
                    if (freq <= DwFreqGear[i, 1])
                    {
                        cd = (byte)i;
                        break;
                    }
                }
            }

            return cd;
        }

        public static UInt16 DecodeDwFreq(byte cd)
        {
            UInt16 freq = 0;
            for (int i = 0; i < DW_FREQ_GEARS_NUM; i++)
            {
                if (cd == DwFreqGear[i, 0])
                {
                    freq = (UInt16)DwFreqGear[i, 1];
                    break;
                }
            }

            return freq;
        }

        public static byte EncodeAevBw(UInt16 bw)
        {
            byte cd = 0;

            if (bw >= EnvBwGear[(AEV_BW_GEARS_NUM - 1), 1])
                cd = AEV_BW_GEARS_NUM - 1;
            else
            {
                for (int i = 0; i < AEV_BW_GEARS_NUM; i++)
                {
                    if (bw <= EnvBwGear[i, 1])
                    {
                        cd = (byte)i;
                        break;
                    }
                }
            }

            return (byte)(cd << 4);
        }

        public static UInt16 DecodeAevBw(byte cd)
        {
            UInt16 bw = 0;
            cd = (byte)(cd >> 4);
            for (int i = 0; i < AEV_BW_GEARS_NUM; i++)
            {
                if (cd == EnvBwGear[i, 0])
                {
                    bw = (UInt16)EnvBwGear[i, 1];
                    break;
                }
            }

            return bw;
        }

        public static byte EncodeAevFt(UInt16 freq)
        {
            byte cd = 0;

            if (freq >= EnvFtGear[(AEV_FT_GEARS_NUM - 1), 1])
                cd = AEV_FT_GEARS_NUM - 1;
            else
            {
                for (int i = 0; i < AEV_FT_GEARS_NUM; i++)
                {
                    if (freq <= EnvFtGear[i, 1])
                    {
                        cd = (byte)i;
                        break;
                    }
                }
            }

            return cd;
        }

        public static UInt16 DecodeAevFt(byte cd)
        {
            UInt16 freq = 0;
            for (int i = 0; i < AEV_FT_GEARS_NUM; i++)
            {
                if (cd == EnvFtGear[i, 0])
                {
                    freq = (UInt16)EnvBwGear[i, 1];
                    break;
                }
            }

            return freq;
        }
    }
}
        
       