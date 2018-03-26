using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 标签信息
    /// </summary>
    public class TagInfo
    {
        public string Pc { get; set; }
        public string Epc { get; set; }
        public string Tid { get; set; }
        public int Count { get; set; }
        public int Antenna { get; set; }
        public string Data { get; set; }
        public bool IsError { get; set; }
        public double Rssi { get; set; }
    }
}
