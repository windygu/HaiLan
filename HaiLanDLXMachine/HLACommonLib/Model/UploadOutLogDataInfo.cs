using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    public class UploadOutLogDataInfo
    {
        public string Guid { get; set; }

        public OutLogDataInfo UploadData { get; set; }

        public string DeviceNo { get; set; }

        public DateTime CreatTime { get; set; }

        public string ErrorMsg { get; set; }
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryTimes { get; set; }

        public UploadOutLogDataInfo(string _deviceNo, OutLogDataInfo _uploadData)
        {
            CreatTime = DateTime.Now;
            Guid = System.Guid.NewGuid().ToString();
            DeviceNo = _deviceNo;
            UploadData = _uploadData;
        }

        public UploadOutLogDataInfo()
        {

        }
    }

    public class OutLogDataInfo
    {
        public string LGNUM { get; set; }

        public string PickTask { get; set; }

        public string LOUCENG { get; set; }

        public List<InventoryOutLogDetailInfo> OutLogList { get; set; }

        public List<OutLogErrorRecord> ErrorList { get; set; }

        public List<ShippingBox> BoxList { get; set; }

        public List<ShippingBoxDetail> BoxDetailList { get; set; }
    }
}
