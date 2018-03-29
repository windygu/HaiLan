using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using HLAWebService.Utils;

namespace HLAWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.codetag.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod(Description = "发运箱和下架单关系下载")]
        public bool SIO_EWM_RFID008(string HU, string LOUCENG, string SHIP_DATE, float SHULIANG, string STORE_ID, string XIANGXING)
        {
            DateTime? sd = null;
            try
            {
                sd = DateTime.Parse(SHIP_DATE);
            }
            catch { }
            bool result = LocalDataService.SaveBoxPartnerMapInfo(HU, LOUCENG, sd, SHULIANG, STORE_ID, XIANGXING);
            return result;
        }
    }
}