using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HLAWebService.Model;

namespace HLAWebService.Utils
{
    public class CommonFunction
    {
        /// <summary>
        /// 获取吊牌详细信息
        /// </summary>
        /// <param name="epc"></param>
        /// <returns></returns>
        public static TagDetailInfo GetTagDetailInfoByEpc(string epc)
        {
            if (string.IsNullOrEmpty(epc) || epc.Length < 20)
                return null;

            List<HLATagInfo> tagList = Cache.Instance[CacheKey.TAG] as List<HLATagInfo>;
            List<MaterialInfo> materialList = Cache.Instance[CacheKey.MATERIAL] as List<MaterialInfo>;
            string rfidEpc = epc.Substring(0, 14) + "000000";
            string rfidAddEpc = rfidEpc.Substring(0, 14);
            HLATagInfo tag = tagList != null ? tagList.FirstOrDefault(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc) : null;
            if (tag == null)
                return null;
            else
            {
                MaterialInfo mater = materialList != null ? materialList.FirstOrDefault(i => i.MATNR == tag.MATNR) : null;
                if (mater == null)
                    return null;
                else
                {
                    TagDetailInfo item = new TagDetailInfo();
                    item.EPC = epc;
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = tag.RFID_ADD_EPC;
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.PXQTY = mater.PXQTY;

                    //判断是否为辅条码epc
                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    return item;
                }
            }
        }
    }
}