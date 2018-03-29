using HLAWebService.Model;
using HLAWebService.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HLAWebService
{
    /// <summary>
    /// SapDownloadService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SapDownloadService : System.Web.Services.WebService
    {

        [WebMethod(Description = "下载物料信息")]
        public string GetMaterials()
        {
            if (Cache.Instance[CacheKey.MATERIAL] != null)
                return JsonConvert.SerializeObject(Cache.Instance[CacheKey.MATERIAL]);
            else
                return JsonConvert.SerializeObject(LocalDataService.GetMaterialInfoList());
        }

        [WebMethod(Description = "下载吊牌信息")]
        public string GetTags()
        {
            if (Cache.Instance[CacheKey.TAG] != null)
                return JsonConvert.SerializeObject(Cache.Instance[CacheKey.TAG]);
            else
                return JsonConvert.SerializeObject(LocalDataService.GetAllRfidHlaTagList());
        }

        public bool Log(string device,string message)
        {
            return LocalDataService.Log(device, message);
        }
    }
}
