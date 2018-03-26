using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib
{
    public class ReaderConfig
    {
        public int SearchMode { get; set; }
        public ushort Session { get; set; }

        public bool UseAntenna1 { get; set; }
        public bool UseAntenna2 { get; set; }
        public bool UseAntenna3 { get; set; }
        public bool UseAntenna4 { get; set; }
        public bool UseAntenna5 { get; set; }
        public bool UseAntenna6 { get; set; }
        public bool UseAntenna7 { get; set; }
        public bool UseAntenna8 { get; set; }

        public double AntennaPower1 { get; set; }
        public double AntennaPower2 { get; set; }
        public double AntennaPower3 { get; set; }
        public double AntennaPower4 { get; set; }
        public double AntennaPower5 { get; set; }
        public double AntennaPower6 { get; set; }
        public double AntennaPower7 { get; set; }
        public double AntennaPower8 { get; set; }
    }
}
