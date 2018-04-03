using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HLAWebService.Utils;
using System.Globalization;
using HLAWebService.Model;

namespace HLAWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.codetag.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod(Description = "发运箱和下架单关系绑定")]
        public bool SIO_EWM_RFID018(string HU, string LOUCENG, string SHIP_DATE, float SHULIANG, string STORE_ID, string XIANGXING, string PICK_TASK)
        {
            HU = !string.IsNullOrEmpty(HU) ? HU.TrimStart('0') : "";

            LogHelp.LogDebug(string.Format("调用绑定函数[SIO_EWM_RFID008],参数=>HU:[{0}],LOUCENG:[{1}],SHIP_DATE:[{2}],SHULIANG:[{3}],STORE_ID:[{4}],XIANGXIANG:[{5}],PICK_TASK:[{6}]",
                HU, LOUCENG, SHIP_DATE, SHULIANG, STORE_ID, XIANGXING, PICK_TASK));

            DateTime? sd = null;
            try
            {
                sd = DateTime.ParseExact(SHIP_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch(Exception ex)
            {
                LogHelp.LogError(ex);
                return false;
            }
            bool result = LocalDataService.SaveBoxPickTaskMapInfo(HU, LOUCENG, sd, SHULIANG, STORE_ID, XIANGXING, PICK_TASK);
            LogHelp.LogDebug(string.Format("调用绑定函数[SIO_EWM_RFID008],结果=>[{0}]", result));
            return result;
        }

        [WebMethod(Description = "发运箱和下架单关系解除绑定")]
        public bool SIO_EWM_RFID019(string HU, string SHIP_DATE)
        {
            HU = !string.IsNullOrEmpty(HU) ? HU.TrimStart('0') : "";

            LogHelp.LogDebug(string.Format("调用绑定函数[SIO_EWM_RFID010],参数=>HU:[{0}],SHIP_DATE:[{1}]",
                HU, SHIP_DATE));

            DateTime? sd = null;
            try
            {
                sd = DateTime.ParseExact(SHIP_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch { }

            if (sd == null)
            {
                LogHelp.LogDebug(string.Format("调用绑定函数[SIO_EWM_RFID010],结果=>[{0}]", "false，发运日期格式错误"));
                return false;
            }

            //根据HU和发运日期到表BoxPickTaskMap获取箱号和下架单号关系列表
            List<BoxPickTaskMapInfo> boxPickTaskMapInfoList = LocalDataService.GetBoxPickTaskMapInfoByHU(HU, sd.Value);
            if (boxPickTaskMapInfoList == null)
            {
                LogHelp.LogDebug(string.Format("调用绑定函数[SIO_EWM_RFID010],结果=>[{0}]", "false，箱码下架单绑定关系不存在"));
                return false;
            }

            BoxPickTaskMapInfo boxPartnerMapInfo = boxPickTaskMapInfoList.Count > 0 ? boxPickTaskMapInfoList[0] : null;
            if (boxPartnerMapInfo != null)
            {
                //根据PARTNER，LOUCENG，ship_date,hu,result到表DeliverBox获取Guid
                PKDeliverBox deliverBox = LocalDataService.GetDeliverBox(boxPartnerMapInfo.LOUCENG, boxPartnerMapInfo.PARTNER, sd.Value, HU, "S");

                if (deliverBox != null)
                {
                    //start edit by wuxw
                    //通过Guid到表delivererrorbox获取扫描记录
                    List<PKDeliverErrorBox> deliverErrorBoxList = LocalDataService.GetDeliverErrorBoxListByBoxGuid(deliverBox.GUID);

                    //到下架单表扣除相应数量
                    if (deliverErrorBoxList != null && deliverErrorBoxList.Count > 0)
                    {
                        foreach (PKDeliverErrorBox errorbox in deliverErrorBoxList)
                        {
                            //更改下架单的实发数量
                            LocalDataService.RemovePickTaskRealNum(errorbox.PICK_TASK, errorbox.PICK_TASK_ITEM, errorbox.MATNR, (int)errorbox.REAL, (int)errorbox.ADD_REAL);
                        }
                    }
                    //end edit by wuxw
                }

                //删除BoxPartnerMap对应记录
                bool result = LocalDataService.DeleteBoxPickTaskMapInfo(HU, sd.Value);

                if (deliverBox != null)
                {
                    //删除DeliverBox对应记录
                    LocalDataService.DeleteDeliverBox(HU, sd.Value);

                    //删除DeliverErrorBox对应记录
                    LocalDataService.DeleteDeliverErrorBox(HU, sd.Value);

                    //删除deliverepcdetail对应记录
                    LocalDataService.DeleteDeliverEpcDetail(HU, sd.Value);
                }
            }

            LogHelp.LogDebug(string.Format("调用绑定函数[SIO_EWM_RFID010],结果=>[{0}]", true));
            return true;
        }



    }
}