using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HLAWebService.Model;
using HLAWebService.Utils;
using System.Threading;

namespace HLAWebService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Model.CHelp.LoadConfig();
            Model.CHelp.initSAP();

            List<MaterialInfo> materialList = LocalDataService.GetMaterialInfoList();
            List<HLATagInfo> tagList = LocalDataService.GetAllRfidHlaTagList();

            if (Cache.Instance[CacheKey.MATERIAL] == null)
                Cache.Instance[CacheKey.MATERIAL] = materialList;

            if (Cache.Instance[CacheKey.TAG] == null)
                Cache.Instance[CacheKey.TAG] = tagList;

            Thread thread = new Thread(new ThreadStart(() => {
                while (true)
                {
                    if (DateTime.Now.ToString("HHmmss") == "000000")
                    {
                        Cache.Instance[CacheKey.MATERIAL] = LocalDataService.GetMaterialInfoList();

                        Cache.Instance[CacheKey.TAG] = LocalDataService.GetAllRfidHlaTagList();
                    }
                    Thread.Sleep(100);
                }
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}