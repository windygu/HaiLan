using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;
namespace HLACommonLib.Model.YK
{
    public class YKBoxDetailInfo
    {
        public long Id { get; set; }
        public string Hu { get; set; }
        public string Epc { get; set; }

        public string Matnr { get; set; }

        public string Zsatnr { get; set; }
        public string Zcolsn { get; set; }
        public string Zsiztx { get; set; }
        public string Barcd { get; set; }
        public DateTime Timestamps { get; set; }
        public byte IsAdd { get; set; }

        public static YKBoxDetailInfo BuildBoxDetailInfo(DataRow row)
        {
            YKBoxDetailInfo result = new YKBoxDetailInfo();
            if(row!=null)
            {
                result.Barcd = row["Barcd"] != null ? row["Barcd"].ToString() : "";
                result.Epc = row["Epc"] != null ? row["Epc"].ToString() : "";
                result.Hu = row["Hu"] != null ? row["Hu"].ToString() : "";
                result.Id = row["Id"] != null ? long.Parse(row["Id"].ToString()) : 0;
                result.IsAdd = row["IsAdd"].CastTo<byte>((byte)0);
                result.Matnr = row["Matnr"] != null ? row["Matnr"].ToString() : "";
                result.Zcolsn = row["Zcolsn"] != null ? row["Zcolsn"].ToString() : "";
                result.Zsatnr = row["Zsatnr"] != null ? row["Zsatnr"].ToString() : "";
                result.Zsiztx = row["Zsiztx"] != null ? row["Zsiztx"].ToString() : "";
                result.Timestamps = row["Timestamps"] != null ? DateTime.Parse(row["Timestamps"].ToString()) : DateTime.Now;
            }
            return result;
        }
    }
}
