using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Newtonsoft.Json;
using HLAWebService.Utils;
using System.Data;

namespace HLAWebService
{
    /// <summary>
    /// PDAZlkUpload 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.codetag.com.cn/")]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    [SoapRpcService]
    public class PDAZlkUpload : System.Web.Services.WebService
    {

        [SoapRpcMethod, WebMethod(Description = "获取顾客信息")]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [SoapRpcMethod, WebMethod(Description = "获取顾客信息")]
        public string echo(string str)
        {
            return str;
        }

        [SoapRpcMethod, WebMethod(Description = "获取顾客信息")]
        public string Z_EW_RF_230(string data)
        {
            Model.CPDAZlkReData re = new Model.CPDAZlkReData();
            if(string.IsNullOrEmpty(data))
            {
                re.msg = "数据为空";
                re.status = "E";
                return JsonConvert.SerializeObject(re);
            }

            if(Model.SysConfig.IsTest)
            {
                re.msg = "ok";
                re.status = "S";
                re.skuData.Add(new Model.CPDAZlkReDataSKU("1", "1", "df", "180", "3"));
                re.skuData.Add(new Model.CPDAZlkReDataSKU("2", "1", "er", "190", "13"));
                re.skuData.Add(new Model.CPDAZlkReDataSKU("1", "1", "df", "180", "33"));

                return JsonConvert.SerializeObject(re);
            }


            try
            {
                Model.CPDAZlkUploadData uploadData = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.CPDAZlkUploadData>(data);

                LogHelp.LogInfo(uploadData.loucheng + " " + uploadData.gonghao + " " + uploadData.epcs.Count);
                re = uploadPdaZlkData(uploadData);

                List<string> hasEpcs = null;
                saveToLocal(uploadData, out hasEpcs);
                re.skuData = getExistSKU(hasEpcs);
           
            }
            catch(Exception ex)
            {
                LogHelp.LogError(ex);
                re.msg = ex.Message;
                re.status = "E";
            }

            return JsonConvert.SerializeObject(re);

        }

        public static List<Model.CPDAZlkReDataSKU> getExistSKU(List<string> epcs)
        {
            List<Model.CPDAZlkReDataSKU> re = new List<Model.CPDAZlkReDataSKU>();

            try
            {
                if(epcs!=null && epcs.Count>0)
                {
                    foreach(string epc in epcs)
                    {
                        string sql = string.Format("SELECT gongHao,COUNT(*) as cn FROM PdaEpcTongJi where SUBSTRING(epc, 1, 14) = '{0}' group by gongHao", epc.Substring(0, 14));
                        DataTable ds = DBHelper.GetTable(sql, false);

                        if(ds!=null && ds.Rows.Count>0)
                        {
                            foreach (DataRow row in ds.Rows)
                            {
                                Model.CPDAZlkReDataSKU sku = new Model.CPDAZlkReDataSKU();

                                sku.gongHao = row["gongHao"].ToString();
                                sku.count = row["cn"].ToString();
                                Model.TagDetailInfo tag = CommonFunction.GetTagDetailInfoByEpc(epc);
                                if(tag!=null)
                                {
                                    sku.pin = tag.ZSATNR;
                                    sku.se = tag.ZCOLSN;
                                    sku.gui = tag.ZSIZTX;
                                }
                                re.Add(sku);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelp.LogInfo(ex.ToString());
            }

            return re;
        }
        public static void saveToLocal(Model.CPDAZlkUploadData uploadData,out List<string> hasEpcs)
        {
            hasEpcs = new List<string>();

            if (uploadData == null)
                return;

            if (!(uploadData.epcs.Count > 0))
                return;

            try
            {
                foreach (string epc in uploadData.epcs)
                {
                    string sql = string.Format("SELECT count(*) from PdaEpcTongJi where epc = '{0}'", epc);
                    int re = 0;
                    int.TryParse(DBHelper.GetValue(sql, false).ToString(), out re);
                    if (re > 0)
                    {
                        if (!hasEpcs.Exists(i => i.Substring(0, 14) == epc.Substring(0, 14)))
                        {
                            hasEpcs.Add(epc);
                        }
                    }
                    else
                    {
                        sql = string.Format("insert into PdaEpcTongJi (epc,gongHao,louCeng,opTime) values ('{0}','{1}','{2}',GETDATE())", epc, uploadData.gonghao, uploadData.loucheng);
                        DBHelper.ExecuteNonQuery(sql);
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelp.LogInfo(ex.ToString());
            }

        }
        public static Model.CPDAZlkReData uploadPdaZlkData(Model.CPDAZlkUploadData uploadData)
        {
            Model.CPDAZlkReData re = new Model.CPDAZlkReData();

            try
            {
                Model.CHelp.uploadPDAZlkData(uploadData.loucheng
                    , uploadData.gonghao, uploadData.epcs, out re.status, out re.msg);
            }
            catch (Exception ex)
            {
                LogHelp.LogError(ex);
                re.msg = ex.Message;
                re.status = "E";
            }

            return re;
        }
    }
}
