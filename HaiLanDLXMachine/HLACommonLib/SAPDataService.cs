using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SAP.Middleware.Connector;
using HLACommonLib.Model;
using HLACommonLib.Comparer;
using HLACommonLib.Model.PK;
using HLACommonLib.Model.DELIVER;
using System.Windows.Forms;
using HLACommonLib.Model.PACKING;
using HLACommonLib.Model.RECEIVE;
using HLACommonLib.Model.YK;
using OSharp.Utility.Extensions;
using HLACommonLib.Model.SAP;
using Newtonsoft.Json;
using HLACommonLib.DAO;
using System.Configuration;
using System.Security.Cryptography;
using System.Net;
using System.Xml;

namespace HLACommonLib
{
    public class SAPDataService
    {
        public static RfcConfigParameters rfcParams = new RfcConfigParameters();
        public static void ReadSAPGroupConfig()
        {
            string UseGroupLogonStr = ConfigurationManager.AppSettings["UseGroupLogon"];
            if (!string.IsNullOrEmpty(UseGroupLogonStr))
            {
                SysConfig.UseGroupLogon = UseGroupLogonStr;
            }

            string LogonGroupStr = ConfigurationManager.AppSettings["LogonGroup"];
            if (!string.IsNullOrEmpty(LogonGroupStr))
            {
                SysConfig.LogonGroup = LogonGroupStr;
            }

            string SystemIDStr = ConfigurationManager.AppSettings["SystemID"];
            if (!string.IsNullOrEmpty(SystemIDStr))
            {
                SysConfig.SystemID = SystemIDStr;
            }

            string MessageServerHostStr = ConfigurationManager.AppSettings["MessageServerHost"];
            if (!string.IsNullOrEmpty(MessageServerHostStr))
            {
                SysConfig.MessageServerHost = MessageServerHostStr;
            }

            string MessageServerServiceStr = ConfigurationManager.AppSettings["MessageServerService"];
            if (!string.IsNullOrEmpty(MessageServerServiceStr))
            {
                SysConfig.MessageServerService = MessageServerServiceStr;
            }
        }
        public static void Init()
        {
            //如果是正式的，使用这个代码
            //if (SysConfig.UseFormal)
            //{
            //    rfcParams.Add(RfcConfigParameters.Name, "HLA");
            //    rfcParams.Add(RfcConfigParameters.MessageServerHost, SysConfig.AppServerHost);   //SAP主机IP
            //    rfcParams.Add(RfcConfigParameters.LogonGroup, "EWM_PRD");
            //    rfcParams.Add(RfcConfigParameters.SystemID, "EWP");
            //    rfcParams.Add(RfcConfigParameters.User, SysConfig.User);  //用户名
            //    rfcParams.Add(RfcConfigParameters.Password, SysConfig.Password);  //密码
            //    rfcParams.Add(RfcConfigParameters.Client, SysConfig.Client);  // Client
            //    rfcParams.Add(RfcConfigParameters.Language, SysConfig.Language);  //登陆语言
            //    rfcParams.Add(RfcConfigParameters.PoolSize, SysConfig.PoolSize);
            //    rfcParams.Add(RfcConfigParameters.PeakConnectionsLimit, SysConfig.PeakConnectionsLimit);
            //    rfcParams.Add(RfcConfigParameters.IdleTimeout, SysConfig.IdleTimeout);
            //}
            //else
            {
                ReadSAPGroupConfig();

                //rfcParams.Add(RfcConfigParameters.Name, "HLA");
                if(SysConfig.LGNUM == "HL01")
                {
                    rfcParams.Add(RfcConfigParameters.Name, "HLA");
                }
                else if(SysConfig.LGNUM == "ET01")
                {
                    rfcParams.Add(RfcConfigParameters.Name, "EHT");
                    //爱居兔不用组登陆
                    //SysConfig.UseGroupLogon = "0";
                }
                if (SysConfig.UseGroupLogon == "1")
                {
                    if(SysConfig.LGNUM == "ET01")
                    {
                        rfcParams.Add(RfcConfigParameters.LogonGroup, "SCP_PRD");
                        rfcParams.Add(RfcConfigParameters.SystemID, "SCP");
                        rfcParams.Add(RfcConfigParameters.MessageServerHost, "172.18.200.61");
                        rfcParams.Add(RfcConfigParameters.MessageServerService, "3600");

                        rfcParams.Add(RfcConfigParameters.User, SysConfig.User);  //用户名
                        rfcParams.Add(RfcConfigParameters.Password, SysConfig.Password);  //密码
                        rfcParams.Add(RfcConfigParameters.Client, SysConfig.Client);  // Client
                        rfcParams.Add(RfcConfigParameters.Language, SysConfig.Language);  //登陆语言
                    }
                    else
                    {
                        rfcParams.Add(RfcConfigParameters.LogonGroup, SysConfig.LogonGroup);
                        rfcParams.Add(RfcConfigParameters.SystemID, SysConfig.SystemID);
                        rfcParams.Add(RfcConfigParameters.MessageServerHost, SysConfig.MessageServerHost);
                        rfcParams.Add(RfcConfigParameters.MessageServerService, SysConfig.MessageServerService);

                        rfcParams.Add(RfcConfigParameters.User, SysConfig.User);  //用户名
                        rfcParams.Add(RfcConfigParameters.Password, SysConfig.Password);  //密码
                        rfcParams.Add(RfcConfigParameters.Client, SysConfig.Client);  // Client
                        rfcParams.Add(RfcConfigParameters.Language, SysConfig.Language);  //登陆语言
                    }
                }
                else
                {
                    rfcParams.Add(RfcConfigParameters.AppServerHost, SysConfig.AppServerHost);   //SAP主机IP
                    rfcParams.Add(RfcConfigParameters.SystemNumber, SysConfig.SystemNumber);  //SAP实例
                    rfcParams.Add(RfcConfigParameters.User, SysConfig.User);  //用户名
                    rfcParams.Add(RfcConfigParameters.Password, SysConfig.Password);  //密码
                    rfcParams.Add(RfcConfigParameters.Client, SysConfig.Client);  // Client
                    rfcParams.Add(RfcConfigParameters.Language, SysConfig.Language);  //登陆语言
                    rfcParams.Add(RfcConfigParameters.PoolSize, SysConfig.PoolSize);
                    rfcParams.Add(RfcConfigParameters.PeakConnectionsLimit, SysConfig.PeakConnectionsLimit);
                    rfcParams.Add(RfcConfigParameters.IdleTimeout, SysConfig.IdleTimeout);
                }
                //if (SysConfig.UseGroupLogon == "1")
                //{
                //    rfcParams.Add(RfcConfigParameters.LogonGroup, SysConfig.LogonGroup);
                //    rfcParams.Add(RfcConfigParameters.SystemID, SysConfig.SystemID);
                //    rfcParams.Add(RfcConfigParameters.MessageServerHost, SysConfig.MessageServerHost);
                //}

            }
        }

        #region 交接单收货相关接口
        /// <summary>
        /// 获取交接单明细列表
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="docno"></param>
        /// <returns></returns>
        public static List<DocDetailInfo> GetTransferDocDetailInfoList(string lgnum, string docno, out List<MaterialInfo> materialList, out List<HLATagInfo> tagList)
        {
            materialList = new List<MaterialInfo>();
            tagList = new List<HLATagInfo>();
            try
            {
                if (SysConfig.IsTest)
                {
                    materialList.Add(new MaterialInfo() { MATNR = "HKCAD3A191AL1011", PXQTY = 40, ZCOLSN = "L1F", ZSATNR = "HKCAD3A191A", ZSIZTX = "180/96A(38)", ZSUPC2 = "A2" });
                    materialList.Add(new MaterialInfo() { MATNR = "HKCAD3A191AL1009", PXQTY = 100, ZCOLSN = "L1F", ZSATNR = "HKCAD3A191A", ZSIZTX = "180/92A(36)", ZSUPC2 = "A1" });
                    List<DocDetailInfo> detailtest = new List<DocDetailInfo>();
                    detailtest.Add(new DocDetailInfo()
                    {
                        DOCNO = docno,
                        ITEMNO = "10",
                        ZSATNR = "HKCAD3A191A",
                        ZCOLSN = "L1F",
                        ZSIZTX = "180/96A(38)",
                        ZCHARG = "A1",
                        PRODUCTNO = "HKCAD3A191AL1011",
                        QTY = 220
                    });
                    detailtest.Add(new DocDetailInfo()
                    {
                        DOCNO = docno,
                        ITEMNO = "80",
                        ZSATNR = "HKCAD3A191A",
                        ZCOLSN = "L1F",
                        ZSIZTX = "180/92A(36)",
                        ZCHARG = "A1",
                        PRODUCTNO = "HKCAD3A191AL1009",
                        QTY = 100
                    });
                    return detailtest.OrderBy(o => int.Parse(o.ITEMNO)).ToList();
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_044");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("DMLSX_NO", docno);//交货单号
                myfun.Invoke(dest);
                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");
                //MessageBox.Show(IrfTable.ToString());
                List<DocDetailInfo> list = new List<DocDetailInfo>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;

                    DocDetailInfo item = new DocDetailInfo();
                    item.DOCNO = IrfTable.GetString("DMLSX_NO");
                    item.ITEMNO = IrfTable.GetString("DMLSX_ITEM");
                    item.PRODUCTNO = IrfTable.GetString("PRODUCTNO");
                    item.QTY = (int)IrfTable.GetFloat("QTY");
                    item.ZCHARG = IrfTable.GetString("ZCHARG");
                    if (!string.IsNullOrEmpty(item.DOCNO))
                    {
                        item.DOCNO = item.DOCNO.TrimStart('0');
                    }
                    if (!string.IsNullOrEmpty(item.ITEMNO))
                    {
                        item.ITEMNO = item.ITEMNO.TrimStart('0');
                    }
                    List<MaterialInfo> mList = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, item.PRODUCTNO);
                    List<HLATagInfo> tList = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, item.PRODUCTNO);
                    if (tList?.Count > 0)
                        tagList.AddRange(tList);
                    MaterialInfo mi = null;
                    if (mList != null && mList.Count > 0)
                    {
                        mi = mList.FirstOrDefault();
                        materialList.AddRange(mList);
                    }
                    if (mi != null)
                    {
                        item.ZSATNR = mi.ZSATNR;
                        item.ZCOLSN = mi.ZCOLSN;
                        item.ZSIZTX = mi.ZSIZTX;
                    }
                    list.Add(item);
                }
                RfcSessionManager.EndContext(dest);
                return list.OrderBy(o => int.Parse(o.ITEMNO)).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 上传交接单包装箱信息
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="docno"></param>
        /// <param name="hu"></param>
        /// <param name="inventoryResult"></param>
        /// <param name="errorMsg"></param>
        /// <param name="tagDetailList"></param>
        /// <param name="sEQUIP_HLA">厂商设备编码对应设备终端号</param>
        /// <returns></returns>
        public static SapResult UploadTransferBoxInfo(string lgnum, string docno, string hu, bool inventoryResult, string errorMsg, Dictionary<string, TagDetailInfoExtend> tagDetailList, RunMode ztype, string subuser, string louceng, string sEQUIP_HLA)
        {
            try
            {
                SapResult result = new SapResult();
                if (SysConfig.IsTest)
                {
                    result.STATUS = "S";
                    result.MSG = "测试环境，上传成功";
                    return result;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_042");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("DMLSX_NO", docno);//交接单号
                myfun.SetValue("HU", hu);//处理单位标识
                myfun.SetValue("CHANGE_D", DateTime.Now.ToString("yyyyMMdd"));//日期
                myfun.SetValue("CHANGE_T", DateTime.Now.ToString("HHmmss"));//时间
                myfun.SetValue("STATUS_IN", inventoryResult ? "S" : "E");//通道机状态（正常口‘S’异常口‘E’）
                myfun.SetValue("MSG_IN", errorMsg);//返回消息
                myfun.SetValue("ZTYPE", ((int)ztype).ToString());//运行模式 默认1 平库
                myfun.SetValue("SUBUSER", subuser);//提交用户 当前登录用户
                myfun.SetValue("LOUCENG", louceng);//楼层
                myfun.SetValue("EQUIP_HLA", sEQUIP_HLA);//设备终端号

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");
                if (tagDetailList.Count > 0)
                {
                    List<string> barcodelist = tagDetailList.Values.Select(o => o.BARCD).Distinct().ToList();//获取条码列表
                    if (barcodelist != null && barcodelist.Count > 0)
                    {
                        foreach (string barcode in barcodelist)
                        {
                            int qty = tagDetailList.Values.Count(o => o.BARCD == barcode && o.HAS_RFID_EPC);//统计对应条码的数量

                            import = rfcrep.GetStructureMetadata("ZSRFID015STR").CreateStructure();
                            import.SetValue("BARCD", barcode);
                            import.SetValue("QTY", qty);
                            IrfTable.Insert(import);
                        }
                    }
                }


                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）
                result.STATUS = status;
                result.MSG = myfun.GetString("MSG_OUT");
                //LogHelper.WriteLine(myfun.GetString("MSG_OUT"));

                RfcSessionManager.EndContext(dest);

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                return new SapResult() { MSG = ex.Message };
            }

            
        }

        /// <summary>
        /// 上传交接单Epc明细
        /// </summary>
        /// <param name="key"></param>
        /// <param name="epcList"></param>
        /// <returns></returns>
        public static bool UploadTransferEpcDetails(string key, List<string> epcList)
        {
            try
            {
                string[] parts = key.Split(',');
                string lgnum = parts[0];
                string docno = parts[1];
                string doccat = parts[2];
                string hu = parts[3];

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_045");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("DMLSX_NO", docno);//交货单号
                myfun.SetValue("DOCCAT", "PDI"); //myfun.SetValue("DOCCAT", doccat);//凭证类别
                myfun.SetValue("HU", hu);//处理单位标识

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");
                if (epcList != null && epcList.Count > 0)
                {
                    foreach (string epc in epcList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID045STR").CreateStructure();
                        import.SetValue("EPC_SER", epc);
                        IrfTable.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）
                RfcSessionManager.EndContext(dest);

                if (status == "S")
                {
                    LocalDataService.SetEpcDetailsHandled(lgnum, docno, doccat, hu, Model.ENUM.ReceiveType.交接单收货);
                    return true;
                }
                else
                    LogHelper.WriteLine(myfun.GetString("MSG_OUT"));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }
        #endregion

        #region 大通道机接口
        /// <summary>
        /// 工号登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Login(string username, string password)
        {
            try
            {
                if (SysConfig.IsTest)
                    return true;
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_021");
                myfun.SetValue("USERID", username);//用户名称
                myfun.SetValue("PASSWORD", password);//用户口令
                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）
                RfcSessionManager.EndContext(dest);

                if (status == "S")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 下架单短拣确认
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool OutConfirm(string username, string password)
        {
            try
            {
                if (SysConfig.IsTest)
                    return true;
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_030");
                myfun.SetValue("UNAME", username);//用户名称
                myfun.SetValue("PASSWORD", password);//用户口令
                if (SysConfig.LGNUM != "ET01")
                {
                    myfun.SetValue("TCODE", "ZEW004");//当前事务代码
                }
                myfun.Invoke(dest);

                string status = myfun.GetString("EV_STATUS"); //执行状态正确‘S’错误‘E’）
                RfcSessionManager.EndContext(dest);

                if (status == "S")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }
        public static string GetEpc(string lgnum, string barcode)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_026");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_BARCD", barcode);//条形码
                myfun.SetValue("IV_QUAN", 1);//要获取的EPC的个数
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DATA");

                if (IrfTable.Count > 0)
                {
                    IrfTable.CurrentIndex = 0;
                    return IrfTable.GetString("EPC_SER");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return "";
        }

        /// <summary>
        /// 根据产品编码获取物料信息
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="MATNR">产品编码</param>
        /// <returns></returns>
        public static List<MaterialInfo> GetMaterialInfoListByMATNR(string lgnum, string MATNR)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_074");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_MATNR", MATNR);//产品编码（’M’的时候必须填写）
                myfun.SetValue("IV_TYPE", "M");//获取方式（’A’全部,’D’按日期,’M’按产品）
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");
                //LogHelper.WriteLine(IrfTable.ToString());
                //构建表结构
                List<MaterialInfo> result = new List<MaterialInfo>();
                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    MaterialInfo item = new MaterialInfo();
                    IrfTable.CurrentIndex = i;
                    item.MATNR = !string.IsNullOrEmpty(IrfTable.GetString("MATNR")) ? IrfTable.GetString("MATNR").TrimStart('0') : "";
                    item.ZSATNR = IrfTable.GetString("ZSATNR");
                    item.ZCOLSN = IrfTable.GetString("ZCOLSN");
                    item.ZSIZTX = IrfTable.GetString("ZSIZTX");
                    item.ZSUPC2 = IrfTable.GetString("ZSUPC2");
                    item.PXQTY = IrfTable.GetInt("PXQTY");
                    item.BRGEW = IrfTable.GetDouble("BRGEW");
                    item.PUT_STRA = IrfTable.GetString("PUT_STRA");
                    item.PXQTY_FH = IrfTable.GetInt("PXQTY_FH");
                    item.ZCOLSN_WFG = IrfTable.GetString("ZCOLSN_WFG");
                    item.PXMAT_FH = IrfTable.GetString("PXMAT_FH").TrimStart('0');
                    item.PXMAT = IrfTable.GetString("PXMAT").TrimStart('0');
                    //item.MAKTX = IrfTable.GetString("MAKTX");
                    item.MAKTX = getZiDuan(IrfTable, "MAKTX");

                    result.Add(item);
                }

                RfcSessionManager.EndContext(dest);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取物料主数据列表
        /// </summary>
        /// <returns></returns>
        public static List<MaterialInfo> GetMaterialInfoList(string lgnum)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_074");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_TYPE", "A");
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");

                //构建表结构
                List<MaterialInfo> materialList = new List<MaterialInfo>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    MaterialInfo item = new MaterialInfo();
                    item.MATNR = !string.IsNullOrEmpty(IrfTable.GetString("MATNR")) ? IrfTable.GetString("MATNR").TrimStart('0') : "";
                    item.ZSATNR = IrfTable.GetString("ZSATNR");
                    item.ZCOLSN = IrfTable.GetString("ZCOLSN");
                    item.ZSIZTX = IrfTable.GetString("ZSIZTX");
                    item.ZSUPC2 = IrfTable.GetString("ZSUPC2");
                    item.PXQTY = IrfTable.GetInt("PXQTY");
                    item.BRGEW = IrfTable.GetDouble("BRGEW");
                    item.PUT_STRA = IrfTable.GetString("PUT_STRA");
                    item.PXQTY_FH = IrfTable.GetInt("PXQTY_FH");
                    item.ZCOLSN_WFG = IrfTable.GetString("ZCOLSN_WFG");
                    item.PXMAT = IrfTable.GetString("PXMAT").TrimStart('0');
                    item.PXMAT_FH = IrfTable.GetString("PXMAT_FH").TrimStart('0');
                    item.MAKTX = getZiDuan(IrfTable, "MAKTX");


                    materialList.Add(item);
                }

                RfcSessionManager.EndContext(dest);

                return materialList;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        public static List<MaterialInfo> GetMaterialInfoListByDate(string lgnum, string dateS, string dateE)
        {
            try
            {

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_074");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_DATE_F", dateS);//开始日期
                myfun.SetValue("IV_DATE_T", dateE);//结束日期
                myfun.SetValue("IV_TYPE", "D");//获取方式（’A’全部,’D’按日期,’M’按产品）
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");

                //构建表结构
                List<MaterialInfo> materialList = new List<MaterialInfo>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    MaterialInfo item = new MaterialInfo();
                    item.MATNR = !string.IsNullOrEmpty(IrfTable.GetString("MATNR")) ? IrfTable.GetString("MATNR").TrimStart('0') : "";
                    item.ZSATNR = IrfTable.GetString("ZSATNR");
                    item.ZCOLSN = IrfTable.GetString("ZCOLSN");
                    item.ZSIZTX = IrfTable.GetString("ZSIZTX");
                    item.ZSUPC2 = IrfTable.GetString("ZSUPC2");
                    item.PXQTY = IrfTable.GetInt("PXQTY");
                    item.BRGEW = IrfTable.GetDouble("BRGEW");
                    item.PUT_STRA = IrfTable.GetString("PUT_STRA");
                    item.PXQTY_FH = IrfTable.GetInt("PXQTY_FH");
                    item.ZCOLSN_WFG = IrfTable.GetString("ZCOLSN_WFG");
                    item.PXMAT = IrfTable.GetString("PXMAT").TrimStart('0');
                    item.PXMAT_FH = IrfTable.GetString("PXMAT_FH").TrimStart('0');
                    item.MAKTX = getZiDuan(IrfTable, "MAKTX");


                    materialList.Add(item);
                }

                RfcSessionManager.EndContext(dest);


                return materialList;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }



        /// <summary>
        /// 根据产品编码获取吊牌信息
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="MATNR">产品编码</param>
        /// <returns></returns>
        public static List<HLATagInfo> GetHLATagInfoListByMATNR(string lgnum, string MATNR)
        {
            try
            {
                if (SysConfig.IsTest)
                {
                    List<HLATagInfo> list = new List<HLATagInfo>();
                    list.Add(new HLATagInfo() { MATNR = "HKCAD3A191AL1011", RFID_EPC = "50000223133428000000", CHARG = "A1" });
                    list.Add(new HLATagInfo() { MATNR = "HKCAD3A191AL1009", RFID_EPC = "50000212133428000000", CHARG = "A1" });
                    return list;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_014");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_MATNR", MATNR);//产品编码（’M’的时候必须填写）
                myfun.SetValue("IV_TYPE", "M");//获取方式（’A’全部,’D’按日期,’M’按产品）
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");

                //构建表结构


                //插入表数据
                List<HLATagInfo> result = new List<HLATagInfo>();
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    HLATagInfo tag = new HLATagInfo();
                    tag.MATNR = !string.IsNullOrEmpty(IrfTable.GetString("MATNR")) ? IrfTable.GetString("MATNR").TrimStart('0') : "";
                    tag.CHARG = IrfTable.GetString("CHARG");
                    tag.BARCD = IrfTable.GetString("BARCD");
                    tag.BARCD_ADD = IrfTable.GetString("BARCD_ADD");
                    tag.RFID_EPC = IrfTable.GetString("RFID_EPC");
                    tag.RFID_ADD_EPC = IrfTable.GetString("RFID_ADD_EPC");
                    tag.BARDL = IrfTable.GetString("BARDL");
                    tag.LIFNR = IrfTable.GetString("LIFNR");
                    result.Add(tag);
                }

                RfcSessionManager.EndContext(dest);


                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }


        public static List<HLATagInfo> GetHLATagInfoListByDate(string lgnum, string dateFrom,string dateEnd)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_014");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_MATNR", "");//产品编码（’M’的时候必须填写）
                myfun.SetValue("IV_TYPE", "D");//获取方式（’A’全部,’D’按日期,’M’按产品）
                myfun.SetValue("IV_DATE_F", dateFrom);//开始日期
                myfun.SetValue("IV_DATE_T", dateEnd);//结束日期
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");

                List<HLATagInfo> result = new List<HLATagInfo>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    HLATagInfo tag = new HLATagInfo();

                    tag.MATNR = !string.IsNullOrEmpty(IrfTable.GetString("MATNR")) ? IrfTable.GetString("MATNR").TrimStart('0') : "";
                    tag.CHARG = IrfTable.GetString("CHARG");
                    tag.BARCD = IrfTable.GetString("BARCD");
                    tag.BARCD_ADD = IrfTable.GetString("BARCD_ADD");
                    tag.RFID_EPC = IrfTable.GetString("RFID_EPC");
                    tag.RFID_ADD_EPC = IrfTable.GetString("RFID_ADD_EPC");
                    tag.BARDL = IrfTable.GetString("BARDL");
                    tag.LIFNR = IrfTable.GetString("LIFNR");
                    result.Add(tag);
                }

                RfcSessionManager.EndContext(dest);

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取吊牌信息列表
        /// </summary>
        /// <returns></returns>
        public static List<HLATagInfo> GetTagInfoList(string lgnum)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_014");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_TYPE", "A");//获取方式（’A’全部,’D’按日期,’M’按产品）
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");

                List<HLATagInfo> result = new List<HLATagInfo>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    HLATagInfo tag = new HLATagInfo();

                    tag.MATNR = !string.IsNullOrEmpty(IrfTable.GetString("MATNR")) ? IrfTable.GetString("MATNR").TrimStart('0') : "";
                    tag.CHARG = IrfTable.GetString("CHARG");
                    tag.BARCD = IrfTable.GetString("BARCD");
                    tag.BARCD_ADD = IrfTable.GetString("BARCD_ADD");
                    tag.RFID_EPC = IrfTable.GetString("RFID_EPC");
                    tag.RFID_ADD_EPC = IrfTable.GetString("RFID_ADD_EPC");
                    tag.BARDL = IrfTable.GetString("BARDL");
                    tag.LIFNR = IrfTable.GetString("LIFNR");
                    result.Add(tag);
                }

                RfcSessionManager.EndContext(dest);

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 上传Epc明细
        /// </summary>
        /// <param name="key"></param>
        /// <param name="epcList"></param>
        /// <returns></returns>
        public static bool UploadEpcDetails(string key, List<string> epcList)
        {
            try
            {
                string[] parts = key.Split(',');
                string lgnum = parts[0];
                string docno = parts[1];
                string doccat = parts[2];
                string hu = parts[3];

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_016");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("DOCNO", docno);//交货单号
                myfun.SetValue("DOCCAT", "PDI"); //myfun.SetValue("DOCCAT", doccat);//凭证类别
                myfun.SetValue("HU", hu);//处理单位标识

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");
                if (epcList != null && epcList.Count > 0)
                {
                    foreach (string epc in epcList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID016STR").CreateStructure();
                        import.SetValue("EPC_SER", epc);
                        IrfTable.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）
                RfcSessionManager.EndContext(dest);

                if (status == "S")
                {
                    //设置epc明细为已处理
                    LocalDataService.SetEpcDetailsHandled(lgnum, docno, doccat, hu, Model.ENUM.ReceiveType.交货单收货);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 获取交货单信息列表
        /// 2015年7月8日 修改：dlv交货日期不再做为参数传入，只需要传入交货单号 -zjr
        /// </summary>
        /// <returns></returns>
        public static List<DocInfo> GetDocInfoList(string lgnum, string docno)
        {
            try
            {
                if (SysConfig.IsTest)
                {
                    List<DocInfo> doctest = new List<DocInfo>();
                    doctest.Add(new DocInfo() { DOCNO = docno, DOCTYPE = "XXDT", GRDATE = "2015-12-30", ZXZWC = "X", ZZJWC = "A",ZYPXFLG = "" });
                    return doctest;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_011");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_DOCNO", docno);//交货单号
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");

                //MessageBox.Show(IrfTable.ToString());
                List<DocInfo> list = new List<DocInfo>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;

                    DocInfo di = new DocInfo();
                    di.DOCNO = IrfTable.GetString("DOCNO");
                    di.DOCTYPE = IrfTable.GetString("DOCTYPE");
                    di.ZXZWC = IrfTable.GetString("ZXZWC");
                    di.ZZJWC = IrfTable.GetString("ZZJWC");
                    di.GRDATE = IrfTable.GetString("GRDATE");
                    di.ZYPXFLG = IrfTable.GetString("ZYPXFLG"); //为X时，表示预拼箱
                    if (!string.IsNullOrEmpty(di.DOCNO))
                    {
                        di.DOCNO = di.DOCNO.TrimStart('0');
                    }

                    list.Add(di);
                }

                RfcSessionManager.EndContext(dest);

                return list;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取交货单明细列表
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="docno"></param>
        /// <returns></returns>
        public static List<DocDetailInfo> GetDocDetailInfoList(string lgnum, string docno, out List<MaterialInfo> materialList, out List<HLATagInfo> tagList)
        {
            materialList = new List<MaterialInfo>();
            tagList = new List<HLATagInfo>();
            try
            {
                if (SysConfig.IsTest)
                {
                    materialList.Add(new MaterialInfo() { MATNR = "HNZAJ3N381ATE004", PXQTY = 40, ZCOLSN = "TEF", ZSATNR = "HNZAJ3N381A", ZSIZTX = "180/96A(52)", ZSUPC2 = "HA1N" });
                    List<DocDetailInfo> detailtest = new List<DocDetailInfo>();
                    detailtest.Add(new DocDetailInfo()
                    {
                        DOCNO = docno,
                        ITEMNO = "10",
                        ZSATNR = "HNZAJ3N381A",
                        ZCOLSN = "TEF",
                        ZSIZTX = "180/96A(52)",
                        ZCHARG = "A1",
                        PRODUCTNO = "HNZAJ3N381ATE004",
                        QTY = 220,
                        ZPBNO = ""
                    });
                    tagList.Add(new HLATagInfo() { Id = 790745,
                        MATNR = "HNZAJ3N381ATE004",
                        CHARG = "A1",
                        BARCD = "HNZAJ3N381ATE004A11",
                        BARCD_ADD = "",
                        RFID_EPC = "50000C83550001000000",
                        RFID_ADD_EPC = "",
                        BARDL = "",
                        LIFNR= "V10006762"
                    });
              
                    return detailtest.OrderBy(o => int.Parse(o.ITEMNO)).ToList();
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_012");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_DOCNO", docno);//交货单号
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");
                //MessageBox.Show(IrfTable.ToString());
                List<DocDetailInfo> list = new List<DocDetailInfo>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;

                    DocDetailInfo item = new DocDetailInfo();
                    item.DOCNO = IrfTable.GetString("DOCNO");
                    item.ITEMNO = IrfTable.GetString("ITEMNO");
                    item.PRODUCTNO = IrfTable.GetString("PRODUCTNO");
                    item.QTY = (int)IrfTable.GetFloat("QTY");
                    item.ZCHARG = IrfTable.GetString("ZCHARG");
                    item.ZPBNO = IrfTable.GetString("ZPBNO");
                    if (!string.IsNullOrEmpty(item.DOCNO))
                    {
                        item.DOCNO = item.DOCNO.TrimStart('0');
                    }
                    if (!string.IsNullOrEmpty(item.ITEMNO))
                    {
                        item.ITEMNO = item.ITEMNO.TrimStart('0');
                    }
                    List<MaterialInfo> mList = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, item.PRODUCTNO);
                    List<HLATagInfo> tList = SAPDataService.GetHLATagInfoListByMATNR(SysConfig.LGNUM, item.PRODUCTNO);
                    if(tList!=null && tList.Count>0)
                    {
                        tagList.AddRange(tList);
                    }
                    MaterialInfo mi = null;
                    if (mList != null && mList.Count > 0)
                    {
                        mi = mList.FirstOrDefault();
                        materialList.AddRange(mList);
                    }
                    if (mi != null)
                    {
                        item.ZSATNR = mi.ZSATNR;
                        item.ZCOLSN = mi.ZCOLSN;
                        item.ZSIZTX = mi.ZSIZTX;
                    }
                    list.Add(item);
                }
                RfcSessionManager.EndContext(dest);
                return list.OrderBy(o => int.Parse(o.ITEMNO)).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 根据配比类型获取配比数量
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="zpbno">配比类型</param>
        /// <returns></returns>
        public static List<MixRatioInfo> GetMixRatioListByZPBNO(string lgnum, string zpbno)
        {
            try
            {
                if(SysConfig.IsTest)
                {
                    List <MixRatioInfo> list = new List<MixRatioInfo>();
                    list.Add(new MixRatioInfo() {
                        ZPBNO = zpbno,
                        MATNR = "HKCAD3A191AL1011",
                        ZSATNR = "HKCAD3A191A",
                        ZCOLSN = "L1F",
                        ZSIZTX = "180/96A(38)",
                        QUAN = 2
                    });

                    list.Add(new MixRatioInfo()
                    {
                        ZPBNO = zpbno,
                        MATNR = "HKCAD3A191AL1009",
                        ZSATNR = "HKCAD3A191A",
                        ZCOLSN = "L1F",
                        ZSIZTX = "180/92A(36)",
                        QUAN = 3
                    });
                    return list;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_057");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("ZPBNO", zpbno);//配比类型
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("IT_DATA");
                List<MixRatioInfo> result = new List<MixRatioInfo>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;

                    MixRatioInfo item = new MixRatioInfo();
                    item.MATNR = IrfTable.GetString("MATNR");
                    item.QUAN = IrfTable.GetInt("QUAN");
                    item.ZPBNO = zpbno;
                    result.Add(item);
                }
                RfcSessionManager.EndContext(dest);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 获取箱码
        /// </summary>
        /// <returns></returns>
        public static Queue<string> GetBoxNo(string lgnum)
        {
            try
            {
                if(SysConfig.IsTest)
                {
                    Queue<string> result = new Queue<string>();
                    result.Enqueue(new Random().Next(10000000, 99999999).ToString());
                    return result;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_GET_NEXT_HU");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                if (lgnum == "ET01")
                {
                    myfun.SetValue("PMAT_MATNR", "1200000131");//包装物料
                }
                else
                {
                    myfun.SetValue("PMTYP", "H003");//包装材料类型
                }

                myfun.SetValue("ADDNUMBER", "10");//张数
                
                myfun.Invoke(dest);

                //string boxNo = myfun.GetString("HU");

                IRfcTable IrfTable = myfun.GetTable("I_OUT");

                //List<MaterialInfo> materialInfoList = LocalDataService.GetMaterialInfoList();
                Queue<string> list = new Queue<string>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    string currentHu = IrfTable.GetString("HU")?.TrimStart('0');

                    //if (lgnum == "ET01")
                    //{
                    //    currentHu = currentHu.Substring(currentHu.Length - 12);
                    //}
                    //else
                    //{
                    //    currentHu = currentHu.Substring(currentHu.Length - 8);
                    //}
                    list.Enqueue(currentHu);

                }
                RfcSessionManager.EndContext(dest);

                //if (!string.IsNullOrEmpty(boxNo))
                //{
                //    boxNo = boxNo.TrimStart('0');
                //}

                return list;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return new Queue<string>();
        }
        public static SapResult UploadJianHuoData(CJianHuoUpload data)
        {
            SapResult result = new SapResult();

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_066");
                myfun.SetValue("LGNUM", data.LGNUM);//仓库编号
                myfun.SetValue("SHIP_DATE", data.SHIP_DATE);//
                myfun.SetValue("HU", data.HU);//
                myfun.SetValue("STATUS_IN", data.STATUS_IN);//
                myfun.SetValue("MSG_IN", data.MSG_IN);//
                myfun.SetValue("SUBUSER", data.SUBUSER);//
                myfun.SetValue("LOUCENG", data.LOUCENG);//
                myfun.SetValue("EQUIP_HLA", data.EQUIP_HLA);//

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");
                foreach (var v in data.bars)
                {
                    import = rfcrep.GetStructureMetadata("ZSRFID066STR").CreateStructure();
                    import.SetValue("PICK_LIST", v.PICK_LIST);
                    import.SetValue("BARCD", v.BARCD);
                    import.SetValue("QTY", v.QTY);
                    import.SetValue("DJ_QTY", v.DJ_QTY);
                    import.SetValue("ERR_QTY", v.ERR_QTY);

                    IrfTable.Insert(import);
                }

                myfun.Invoke(dest);

                result.STATUS = myfun.GetString("STATUS_OUT");
                result.MSG = myfun.GetString("MSG_OUT");
                RfcSessionManager.EndContext(dest);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                result.STATUS = "E";
                result.MSG = ex.ToString();
            }
            return result;
        }
        /// <summary>
        /// 上传包装箱信息
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="docno"></param>
        /// <param name="hu"></param>
        /// <param name="inventoryResult"></param>
        /// <param name="errorMsg"></param>
        /// <param name="tagDetailList"></param>
        /// <param name="sEQUIP_HLA">厂商设备编码对应设备终端号</param>
        /// <returns></returns>
        public static SapResult UploadBoxInfo(string lgnum, string docno, string hu, bool inventoryResult, string errorMsg, Dictionary<string, TagDetailInfoExtend> tagDetailList, RunMode ztype, string subuser, string louceng, string sEQUIP_HLA,string zpbno)
        {
            try
            {
                SapResult result = new SapResult();
                if (SysConfig.IsTest)
                {
                    result.STATUS = "S";
                    result.MSG = "测试环境，上传成功";
                    return result;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_015");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("DOCNO", docno);//交货单号
                myfun.SetValue("HU", hu);//处理单位标识
                myfun.SetValue("CHANGE_D", DateTime.Now.ToString("yyyyMMdd"));//日期
                myfun.SetValue("CHANGE_T", DateTime.Now.ToString("HHmmss"));//时间
                myfun.SetValue("STATUS_IN", inventoryResult ? "S" : "E");//通道机状态（正常口‘S’异常口‘E’）
                myfun.SetValue("MSG_IN", errorMsg);//返回消息
                myfun.SetValue("ZTYPE", ((int)ztype).ToString());//运行模式 默认1 平库
                myfun.SetValue("SUBUSER", subuser);//提交用户 当前登录用户
                myfun.SetValue("LOUCENG", louceng);//楼层
                myfun.SetValue("EQUIP_HLA", sEQUIP_HLA);//楼层
                myfun.SetValue("ZPBNO", zpbno);//配比类型

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");
                if (tagDetailList.Count > 0)
                {
                    List<string> barcodelist = tagDetailList.Values.Select(o => o.BARCD).Distinct().ToList();//获取条码列表
                    if (barcodelist != null && barcodelist.Count > 0)
                    {
                        foreach (string barcode in barcodelist)
                        {
                            int qty = tagDetailList.Values.Count(o => o.BARCD == barcode && o.HAS_RFID_EPC);//统计对应条码的数量

                            import = rfcrep.GetStructureMetadata("ZSRFID015STR").CreateStructure();
                            import.SetValue("BARCD", barcode);
                            import.SetValue("QTY", qty);
                            IrfTable.Insert(import);
                        }
                    }
                }


                myfun.Invoke(dest);

                result.STATUS = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）
                result.MSG = myfun.GetString("MSG_OUT");
                RfcSessionManager.EndContext(dest);

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                return new SapResult() { MSG = ex.Message };
            }
        }

        public static SapResult SanHeUploadBoxInfo(string lgnum, string docno, string hu, bool inventoryResult, string errorMsg, Dictionary<string, TagDetailInfoExtend> tagDetailList, RunMode ztype, string subuser, string louceng, string sEQUIP_HLA, string zpbno)
        {
            try
            {
                SapResult result = new SapResult();
                if (SysConfig.IsTest)
                {
                    result.STATUS = "E";
                    result.MSG = "测试环境，上传成功";
                    return result;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RF_001_A");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("DOCNO", docno);//交货单号
                myfun.SetValue("LLP", hu);//处理单位标识
                myfun.SetValue("LOUCENG", louceng);//楼层
                myfun.SetValue("EQUIP_HLA", sEQUIP_HLA);//楼层

                myfun.SetValue("STATUS1", inventoryResult ? "S" : "E");//通道机状态（正常口‘S’异常口‘E’）
                myfun.SetValue("MSG1", errorMsg);//返回消息

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DATA");
                if (tagDetailList.Count > 0)
                {
                    List<string> barcodelist = tagDetailList.Values.Select(o => o.BARCD).Distinct().ToList();//获取条码列表
                    if (barcodelist != null && barcodelist.Count > 0)
                    {
                        foreach (string barcode in barcodelist)
                        {
                            int qty = tagDetailList.Values.Count(o => o.BARCD == barcode && o.HAS_RFID_EPC);//统计对应条码的数量

                            import = rfcrep.GetStructureMetadata("ZSCREATTANUMSTRNEW1").CreateStructure();
                            import.SetValue("BARCD", barcode);
                            import.SetValue("QTY", qty);
                            IrfTable.Insert(import);
                        }
                    }
                }


                myfun.Invoke(dest);

                result.STATUS = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                result.MSG = myfun.GetString("MSG");
                RfcSessionManager.EndContext(dest);

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                return new SapResult() { MSG = ex.Message };
            }
        }

        /// <summary>
        /// 通道机获取基础信息配置
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="sNo">设备编号</param>
        /// <param name="deviceType">
        /// 设备类型
        /// 01A	箱装收货通道机设备
        /// 01B 箱装发货通道机设备
        /// 01C 箱装退货通道机设备
        /// 01D	箱装复核通道机设备
        /// 01F 整理库装箱设备
        /// 02B 散件发货通道机设备
        /// 03A 挂装收货通道机设备
        /// 03B 挂装发货通道机设备
        /// 03C 挂装退货通道机设备
        /// 03D 三合挂装收货
        /// </param>
        /// <returns></returns>
        public static DeviceTable GetHLANo(string lgnum, string sNo, string deviceType)
        {
            try
            {
                if (SysConfig.IsTest)
                {
                    DeviceTable result = new DeviceTable();
                    result.EQUIP_HLA = "HLA98720";
                    result.LOUCENG = "081";
                    result.KF_LX = "G"; //库房类型：X箱装
                    result.GxList = new List<GxInfo>();
                    GxInfo gx = new GxInfo();
                    gx.GX_CODE = "T001";
                    gx.GX_NAME = "TestGxName";
                    gx.VIEWGROUP = "G001";
                    gx.VIEWUSR = "TestUser";
                    result.GxList.Add(gx);
                    result.AuthList = new List<AuthInfo>();
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "A0001", AUTH_VALUE = "0911", EQUIP_HLA = "HLA98720" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "A0001", AUTH_VALUE = "1411", EQUIP_HLA = "HLA98720" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "B0001", AUTH_VALUE = "1511", EQUIP_HLA = "HLA98720" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "B0001", AUTH_VALUE = "0811", EQUIP_HLA = "HLA98720" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "C0008", AUTH_VALUE = "0211", EQUIP_HLA = "HLA98720" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "D0001", AUTH_VALUE = "07", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "ADM1" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "F0001", AUTH_VALUE = "08", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "0810" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "F0001", AUTH_VALUE = "09", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "ADM1" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "D0001", AUTH_VALUE = "10", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "ADM1" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "D0001", AUTH_VALUE = "TH", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "E0001", AUTH_VALUE = "TH02", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "TH" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "E0001", AUTH_VALUE = "TH03", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "TH" });
                    return result;
                }
                //MessageBox.Show(JsonConvert.SerializeObject(rfcParams));
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_032");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_EQUIP", sNo);//设备编号10E901
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DATA");
                //MessageBox.Show(IrfTable.ToString());
                DeviceTable deviceInfo = null;
                if (IrfTable.Count > 0)
                {
                    for (int i = 0; i < IrfTable.Count; i++)
                    {
                        IrfTable.CurrentIndex = i;
                        string equ = IrfTable.GetString("EQUIP_TPE");
                        if (equ.ToUpper() == deviceType.ToUpper())
                        {
                            deviceInfo = new DeviceTable();
                            deviceInfo.EQUIP_HLA = IrfTable.GetString("EQUIP_HLA");
                            deviceInfo.EQUIP_DEC = IrfTable.GetString("EQUIP_DEC");
                            deviceInfo.EQUIP_TPE = IrfTable.GetString("EQUIP_TPE");
                            deviceInfo.EQUIP_TPC = IrfTable.GetString("EQUIP_TPC");
                            deviceInfo.LOUCENG = IrfTable.GetString("LOUCENG");
                            deviceInfo.KF_LX = IrfTable.GetString("KF_LX");//库房类型（’X’箱装’G’挂装）
                            deviceInfo.IS_PRINT = IrfTable.GetString("IS_PRINT");
                            deviceInfo.IS_NONUSE = IrfTable.GetString("IS_NONUSE");
                            deviceInfo.EQUIP = IrfTable.GetString("EQUIP");
                            deviceInfo.REMARK = IrfTable.GetString("REMARK");
                            break;
                        }
                    }

                    if (deviceInfo == null) return null;
                    IRfcTable IrfTable_GX = myfun.GetTable("ET_DATA_GX");
                    //MessageBox.Show(IrfTable_GX.ToString());
                    if (IrfTable_GX.Count > 0)
                    {
                        deviceInfo.GxList = new List<GxInfo>();
                        for (int i = 0; i < IrfTable_GX.Count; i++)
                        {
                            IrfTable_GX.CurrentIndex = i;
                            string equip_hla = IrfTable_GX.GetString("EQUIP_HLA");
                            if (equip_hla.ToUpper() == deviceInfo.EQUIP_HLA.ToUpper())
                            {
                                GxInfo gx = new GxInfo();
                                gx.GX_CODE = IrfTable_GX.GetString("GX_CODE");
                                gx.GX_NAME = IrfTable_GX.GetString("GX_NAME");
                                gx.VIEWGROUP = IrfTable_GX.GetString("VIEWGROUP");
                                gx.VIEWUSR = IrfTable_GX.GetString("VIEWUSR");
                                deviceInfo.GxList.Add(gx);
                            }
                        }

                    }

                    IRfcTable AuthTable = myfun.GetTable("ET_DATA_AUTH");
                    //MessageBox.Show(AuthTable.ToString());
                    //LogHelper.WriteLine(AuthTable.ToString());
                    if(AuthTable.Count>0)
                    {
                        deviceInfo.AuthList = new List<AuthInfo>();
                        for (int i = 0; i < AuthTable.Count; i++)
                        {
                            AuthTable.CurrentIndex = i;
                            string equip_hla = AuthTable.GetString("EQUIP_HLA");
                            if (equip_hla.ToUpper() == deviceInfo.EQUIP_HLA.ToUpper())
                            {
                                AuthInfo item = new AuthInfo();
                                item.AUTH_CODE = AuthTable.GetString("AUTH_CODE");
                                item.AUTH_CODE_DES = AuthTable.GetString("AUTH_CODE_DES");
                                item.AUTH_VALUE = AuthTable.GetString("AUTH_VALUE");
                                item.AUTH_VALUE_DES = AuthTable.GetString("AUTH_VALUE_DES");
                                item.EQUIP_DEC = AuthTable.GetString("EQUIP_DEC");
                                item.EQUIP_HLA = AuthTable.GetString("EQUIP_HLA");
                                deviceInfo.AuthList.Add(item);
                            }
                        }
                    }
                    return deviceInfo;
                }
                return null; ;//表示错误信息
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 通道机获取基础信息配置
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="sNo">设备编号</param>
        /// <param name="deviceType">终端类型 </param>
        /// <returns></returns>
        public static DeviceTable GetHLANo(string lgnum, string sNo)
        {
            try
            {
                if (SysConfig.IsTest)
                {
                    DeviceTable result = new DeviceTable();
                    result.EQUIP_HLA = "HLA98720";
                    result.LOUCENG = "081";
                    result.KF_LX = "G"; //库房类型：X箱装
                    result.LGTYP = new List<string>();
                    result.LGTYP.Add("0414");
                    result.GxList = new List<GxInfo>();
                    GxInfo gx = new GxInfo();
                    gx.GX_CODE = "T001";
                    gx.GX_NAME = "TestGxName";
                    gx.VIEWGROUP = "G001";
                    gx.VIEWUSR = "TestUser";
                    result.GxList.Add(gx);
                    result.AuthList = new List<AuthInfo>();
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "A0001",AUTH_VALUE = "1411", EQUIP_HLA = "HLA98720" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "B0001",AUTH_VALUE = "0811", EQUIP_HLA = "HLA98720" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "C0008",AUTH_VALUE = "0811", EQUIP_HLA = "HLA98720" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "D0001", AUTH_VALUE = "07", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "ADM1" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "D0001", AUTH_VALUE = "08", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "0810" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "D0001", AUTH_VALUE = "09", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "ADM1" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "D0001", AUTH_VALUE = "10", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "ADM1" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "D0001", AUTH_VALUE = "TH", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "E0001", AUTH_VALUE = "TH02", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "TH" });
                    result.AuthList.Add(new AuthInfo() { AUTH_CODE = "E0001", AUTH_VALUE = "TH03", EQUIP_HLA = "HLA98720", AUTH_VALUE_DES = "TH" });
                    return result;
                }

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_032");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_EQUIP", sNo);//设备编号10E901
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DATA");
                //MessageBox.Show(IrfTable.ToString());
                //IRfcTable AuthTable = myfun.GetTable("ET_DATA_AUTH");
                //LogHelper.WriteLine(IrfTable.ToString());
                DeviceTable deviceInfo = new DeviceTable();
                if (IrfTable.Count > 0)
                {
                    IrfTable.CurrentIndex = 0;
                    deviceInfo.EQUIP_HLA = IrfTable.GetString("EQUIP_HLA");
                    deviceInfo.EQUIP_DEC = IrfTable.GetString("EQUIP_DEC");
                    deviceInfo.EQUIP_TPE = IrfTable.GetString("EQUIP_TPE");
                    deviceInfo.EQUIP_TPC = IrfTable.GetString("EQUIP_TPC");
                    deviceInfo.LOUCENG = IrfTable.GetString("LOUCENG");
                    deviceInfo.KF_LX = IrfTable.GetString("KF_LX");//库房类型（’X’箱装’G’挂装）
                    deviceInfo.IS_PRINT = IrfTable.GetString("IS_PRINT");
                    deviceInfo.IS_NONUSE = IrfTable.GetString("IS_NONUSE");
                    deviceInfo.EQUIP = IrfTable.GetString("EQUIP");
                    deviceInfo.REMARK = IrfTable.GetString("REMARK");


                    IRfcTable IrfTable_GX = myfun.GetTable("ET_DATA_GX");
                    if (IrfTable_GX.Count > 0)
                    {
                        deviceInfo.GxList = new List<GxInfo>();
                        for (int i = 0; i < IrfTable_GX.Count; i++)
                        {
                            IrfTable_GX.CurrentIndex = i;
                            GxInfo gx = new GxInfo();
                            gx.GX_CODE = IrfTable_GX.GetString("GX_CODE");
                            gx.GX_NAME = IrfTable_GX.GetString("GX_NAME");
                            gx.VIEWGROUP = IrfTable_GX.GetString("VIEWGROUP");
                            gx.VIEWUSR = IrfTable_GX.GetString("VIEWUSR");
                            deviceInfo.GxList.Add(gx);
                        }

                    }

                    IRfcTable AuthTable = myfun.GetTable("ET_DATA_AUTH");
                //LogHelper.WriteLine(AuthTable.ToString());
                    if (AuthTable.Count > 0)
                    {
                        deviceInfo.AuthList = new List<AuthInfo>();
                        for (int i = 0; i < AuthTable.Count; i++)
                        {
                            AuthTable.CurrentIndex = i;
                            string equip_hla = AuthTable.GetString("EQUIP_HLA");
                            if (equip_hla.ToUpper() == deviceInfo.EQUIP_HLA.ToUpper())
                            {
                                AuthInfo item = new AuthInfo();
                                item.AUTH_CODE = AuthTable.GetString("AUTH_CODE");
                                item.AUTH_CODE_DES = AuthTable.GetString("AUTH_CODE_DES");
                                item.AUTH_VALUE = AuthTable.GetString("AUTH_VALUE");
                                item.AUTH_VALUE_DES = AuthTable.GetString("AUTH_VALUE_DES");
                                item.EQUIP_DEC = AuthTable.GetString("EQUIP_DEC");
                                item.EQUIP_HLA = AuthTable.GetString("EQUIP_HLA");
                                deviceInfo.AuthList.Add(item);
                            }
                        }
                    }
                    return deviceInfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region 小通道机接口

        public static bool PrintShippingBox(string printername, string lgnum,
            string picktask, ShippingBox box, string louceng)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_025");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_PICKNO", picktask);//下架单编号
                myfun.SetValue("IV_HU", box.HU);//箱号
                myfun.SetValue("IV_PMAT", box.PMAT_MATNR);//包装材料
                myfun.SetValue("IV_PNTCODE", printername);//包装材料
                myfun.SetValue("IV_LOUCENG", louceng);//仓库楼层

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DATA");
                if (box.Details != null && box.Details.Count > 0)
                {
                    var printList = box.Details?.FindAll(j=>j.IsADD == 0)?.GroupBy(i => i.MATNR)?.Select(g =>
                        new PrintShippingBoxDetail() { MATNR = g.Key, UOM = g.FirstOrDefault().UOM, QTY = g.Count().ToString() });
                    foreach (PrintShippingBoxDetail detail in printList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID025STR").CreateStructure();
                        import.SetValue("MATNR", detail.MATNR);
                        import.SetValue("UOM", detail.UOM);
                        import.SetValue("QTY", detail.QTY);
                        IrfTable.Insert(import);
                    }
                }
                myfun.Invoke(dest);

                string status = myfun.GetString("EV_STATUS"); //执行状态正确‘S’错误‘E’）
                RfcSessionManager.EndContext(dest);

                if (status == "S")
                    return true;
                else
                {
                    LogHelper.WriteLine(string.Format("Z_EW_RFID_025打印异常，原因：{0}",
                        myfun.GetString("EV_MSG")));//返回消息
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }



        public static bool PrintMixShippingBoxBeforeUpload(string printername, string lgnum,
            string picktask, ShippingBox box)
        {
            try
            {
                bool result = true;
                if (box.Details != null && box.Details.Count > 0)
                {
                    List<string> pickTaskList = box.Details.FindAll(i => i.IsADD == 0).Select(o => o.PICK_TASK).Distinct().ToList();
                    if (pickTaskList != null && pickTaskList.Count > 0)
                    {
                        if (picktask == null)
                        {
                            foreach (var item in pickTaskList)
                            {
                                picktask = item;
                                // 获取是第几箱
                                int boxindex = pickTaskList.IndexOf(picktask) + 1;
                                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                                RfcRepository rfcrep = dest.Repository;
                                IRfcFunction myfun = null;
                                myfun = rfcrep.CreateFunction("Z_EW_RFID_035");
                                myfun.SetValue("LGNUM", lgnum);//仓库编号
                                myfun.SetValue("PICK_TASK", picktask);//下架单编号
                                myfun.SetValue("HU", box.HU);//箱号
                                myfun.SetValue("PMAT", box.PMAT_MATNR);//包装材料
                                myfun.SetValue("PNT_TYPE", "1");//打印类型
                                myfun.SetValue("PNTCODE", printername);//打印机编号
                                StringBuilder goods_qty = new StringBuilder();
                                Dictionary<string, int> dic = new Dictionary<string, int>();
                                foreach (ShippingBoxDetail detail in box.Details.FindAll(o => o.PICK_TASK == picktask))
                                {
                                    if (dic.ContainsKey(detail.UOM))
                                        dic[detail.UOM]++;
                                    else
                                        dic.Add(detail.UOM, 1);
                                }
                                foreach (KeyValuePair<string, int> keyvalue in dic)
                                {
                                    goods_qty.AppendFormat("{0}${1}$", keyvalue.Value, keyvalue.Key);
                                }
                                myfun.SetValue("GOODS_QTY", goods_qty.ToString());//货品数量
                                myfun.SetValue("TITLE", boxindex == 1 ? "开箱标签" : "拼箱标签");//标签标题
                                LogHelper.WriteLine(string.Format("SERNO:{0}", boxindex.ToString()));
                                myfun.SetValue("SERNO", boxindex.ToString());//序号


                                myfun.Invoke(dest);

                                //string status = myfun.GetString("EV_STATUS"); //执行状态正确‘S’错误‘E’）
                                RfcSessionManager.EndContext(dest);

                                result = true;
                            }
                        }
                        else
                        {
                            // 获取是第几箱
                            int boxindex = pickTaskList.IndexOf(picktask) + 1;
                            RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                            RfcRepository rfcrep = dest.Repository;
                            IRfcFunction myfun = null;
                            myfun = rfcrep.CreateFunction("Z_EW_RFID_035");
                            myfun.SetValue("LGNUM", lgnum);//仓库编号
                            myfun.SetValue("PICK_TASK", picktask);//下架单编号
                            myfun.SetValue("HU", box.HU);//箱号
                            myfun.SetValue("PMAT", box.PMAT_MATNR);//包装材料
                            myfun.SetValue("PNT_TYPE", "1");//打印类型
                            myfun.SetValue("PNTCODE", printername);//打印机编号
                            StringBuilder goods_qty = new StringBuilder();
                            Dictionary<string, int> dic = new Dictionary<string, int>();
                            foreach (ShippingBoxDetail detail in box.Details.FindAll(o => o.PICK_TASK == picktask))
                            {
                                if (dic.ContainsKey(detail.UOM))
                                    dic[detail.UOM]++;
                                else
                                    dic.Add(detail.UOM, 1);
                            }
                            foreach (KeyValuePair<string, int> keyvalue in dic)
                            {
                                goods_qty.AppendFormat("{0}${1}$", keyvalue.Value, keyvalue.Key);
                            }
                            myfun.SetValue("GOODS_QTY", goods_qty.ToString());//货品数量
                            myfun.SetValue("TITLE", boxindex == 1 ? "开箱标签" : "拼箱标签");//标签标题
                            myfun.SetValue("SERNO", boxindex);//序号
                            myfun.Invoke(dest);
                            RfcSessionManager.EndContext(dest);

                            result = true;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);

            }

            return false;
        }

        /*
        public static bool PrintMixShippingBoxAfterUpload(string printername, string lgnum,
            string picktask, List<ShippingBox> boxList)
        {
            try
            {
                bool result = true;
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_PNT_PXBAR");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("PICK_TASK", picktask);//下架单编号
                myfun.SetValue("PNTCODE", printername);//打印机编号
                myfun.SetValue("IN_OPTION", "X");//是否为挂装

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DATA");
                if (boxList != null && boxList.Count > 0)
                {
                    foreach (ShippingBox item in boxList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID035STR").CreateStructure();
                        import.SetValue("HU", item.HU);
                        import.SetValue("MX", item.IsFull == 1 ? "X" : "");
                        IrfTable.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("EV_STATUS"); //执行状态正确‘S’错误‘E’）
                RfcSessionManager.EndContext(dest);

                if (status != "S")
                {
                    LogHelper.WriteLine(string.Format("Z_EW_RFID_035打印异常，原因：{0}",
                        myfun.GetString("EV_MSG")));//返回消息
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);

            }

            return false;
        }*/

        /// <summary>
        /// 查询设备厂商对应存储类型
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="sDeviceNo">设备厂商</param>
        /// <returns></returns>
        public static List<string> GetProType(string lgnum, string sDeviceNo = "D")
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_031");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_VENDOR", sDeviceNo);//设备厂商
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DATA");

                List<string> list = new List<string>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    list.Add(IrfTable.GetString("LGTYP"));
                }
                RfcSessionManager.EndContext(dest);
                return list;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取包装材料[箱型]
        /// </summary>
        /// <param name="lgnum">仓库编码</param>
        /// <param name="sFloor">楼层</param>
        /// <returns></returns>
        public static DataTable GetPackagingMaterialsList(string lgnum, string sFloor, out string def,string brand = "")
        {
            string res = "";
            try
            {
                #region 测试
                if (SysConfig.IsTest)
                {
                    DataTable result = new DataTable();
                    result.Columns.Add("LOUCENG");
                    result.Columns.Add("PMAT_MATNR");
                    result.Columns.Add("MAKTX");
                    result.Columns.Add("DEFAULT_PMAT_MATNR");
                    DataRow dr = result.NewRow();
                    dr["LOUCENG"] = "082";
                    dr["PMAT_MATNR"] = "0000000000000000000000000000001200000037";
                    dr["MAKTX"] = "T恤箱[60*40*25]";
                    dr["DEFAULT_PMAT_MATNR"] = "0000000000000000000000000000001200000037";
                    result.Rows.Add(dr);
                    DataRow dr2 = result.NewRow();
                    dr2["LOUCENG"] = "082";
                    dr2["PMAT_MATNR"] = "0000000000000000000000000000001200000090";
                    dr2["MAKTX"] = "连帽箱[60*60*45]";
                    dr2["DEFAULT_PMAT_MATNR"] = "0000000000000000000000000000001200000090";
                    result.Rows.Add(dr2);
                    def = "";
                    return result;
                }
                #endregion
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_029");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("LOUCENG", sFloor);//楼层
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");

                //构建表结构
                DataTable table = new DataTable();
                table.Columns.Add("LOUCENG");
                table.Columns.Add("PMAT_MATNR");
                table.Columns.Add("MAKTX");
                table.Columns.Add("DEFAULT_PMAT_MATNR");
                table.Columns.Add("BRAND");

                List<string> list = new List<string>();

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;

                    string table_brand = getZiDuan(IrfTable, "BRAND");
                    if (!string.IsNullOrEmpty(brand))
                    {
                        if (table_brand != brand)
                        {
                            continue;
                        }
                    }

                    DataRow dr = table.NewRow();
                    dr["BRAND"] = table_brand;                
                    dr["LOUCENG"] = IrfTable.GetString("LOUCENG");
                    dr["PMAT_MATNR"] = IrfTable.GetString("PMAT_MATNR").TrimStart('0');

                    dr["MAKTX"] = IrfTable.GetString("MAKTX");
                    dr["DEFAULT_PMAT_MATNR"] = IrfTable.GetString("DEFAULT_PMAT_MATNR").TrimStart('0');
                    table.Rows.Add(dr);
                }

                //LogHelper.WriteLine(JsonConvert.SerializeObject(table));
                RfcSessionManager.EndContext(dest);
                def = res;
                return table;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                def = "";
                return null;
            }
        }

        /// <summary>
        /// 获取箱号
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="sPMAT_MATNR">包装物料</param>
        /// <param name="iTotal">获取数量</param>
        /// <returns></returns>
        public static string GetBoxNo(string lgnum, string sPMAT_MATNR, int iTotal)
        {
            try
            {
                if (SysConfig.IsTest)
                {
                    Random random = new Random();
                    return random.Next(10000000, 99999999).ToString();
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_GET_NEXT_HU");//方法名不存在
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("PMAT_MATNR", sPMAT_MATNR);// 包装物料
                myfun.SetValue("PMTYP", "H003");//包装材料类型(箱号H003)
                myfun.SetValue("ADDNUMBER", iTotal);//获取数量
                myfun.Invoke(dest);

                //IRfcTable IrfTable = myfun.GetTable("I_OUT");

                //Queue<string> list = new Queue<string>();

                ////插入表数据
                //for (int i = 0; i < IrfTable.Count; i++)
                //{
                //    IrfTable.CurrentIndex = i;
                //    string currentHu = IrfTable.GetString("HU");
                //    currentHu = currentHu.Substring(currentHu.Length - 8);
                //    list.Enqueue(currentHu);
                //}

                string result = myfun.GetString("HU");
                if(!string.IsNullOrEmpty(result))
                {
                    result = result.TrimStart('0');
                }
                else
                {
                    result = "";
                }
                //result = result.Substring(result.Length - 8);

                RfcSessionManager.EndContext(dest);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取吊牌信息列表
        /// </summary>
        /// <param name="lgnum">仓库编码</param>
        /// <param name="dateFrom">开始日期</param>
        /// <param name="dateEnd">结束日期</param>
        /// <param name="sSKU">产品编码</param>
        /// <param name="sType">获取方式</param> 
        /// <returns></returns>
        public static DataTable GetTagInfoList(string lgnum, string dateFrom, string dateEnd, string sSKU, string sType)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_014");
                myfun.SetValue("IV_LGNUM", lgnum);//仓库编号
                myfun.SetValue("IV_DATE_F", dateFrom);//开始日期
                myfun.SetValue("IV_DATE_T", dateEnd);//结束日期

                if (sType.Equals('M'))
                {
                    myfun.SetValue("IV_MATNR", "");//产品编码（’M’的时候必须填写）
                }
                myfun.SetValue("IV_TYPE", sType);//获取方式（’A’全部,’D’按日期,’M’按产品）
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_OUTPUT");

                //构建表结构
                DataTable table = new DataTable();
                table.Columns.Add("MATNR");
                table.Columns.Add("CHARG");
                table.Columns.Add("BARCD");
                table.Columns.Add("BARCD_ADD");
                table.Columns.Add("RFID_EPC");
                table.Columns.Add("RFID_ADD_EPC");
                table.Columns.Add("BARDL");
                table.Columns.Add("LIFNR");

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    DataRow dr = table.NewRow();
                    dr["MATNR"] = !string.IsNullOrEmpty(IrfTable.GetString("MATNR")) ? IrfTable.GetString("MATNR").TrimStart('0') : "";
                    dr["CHARG"] = IrfTable.GetString("CHARG");
                    dr["BARCD"] = IrfTable.GetString("BARCD");
                    dr["BARCD_ADD"] = IrfTable.GetString("BARCD_ADD");
                    dr["RFID_EPC"] = IrfTable.GetString("RFID_EPC");
                    dr["RFID_ADD_EPC"] = IrfTable.GetString("RFID_ADD_EPC");
                    dr["BARDL"] = IrfTable.GetString("BARDL");
                    dr["LIFNR"] = IrfTable.GetString("LIFNR");
                    table.Rows.Add(dr);
                }

                RfcSessionManager.EndContext(dest);

                return table;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                return null;
            }
        }
        
        /// <summary>
        /// 发运标签信息下载
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="dTime">发运日期</param>
        /// <returns></returns>
        //public static List<ShippingTagInfo> GetHLAINformation(string lgnum, string sTime)
        //{
        //    try
        //    {
        //        RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
        //        RfcRepository rfcrep = dest.Repository;
        //        IRfcFunction myfun = null;
        //        myfun = rfcrep.CreateFunction("Z_EW_RFID_018");
        //        myfun.SetValue("LGNUM", lgnum);//仓库编号
        //        myfun.SetValue("SHIP_DATE", sTime);//下架单编号 
        //        myfun.Invoke(dest);

        //        IRfcTable IrfTable = myfun.GetTable("ET_DETAIL");
        //        MessageBox.Show(IrfTable.ToString());
        //        List<ShippingTagInfo> result = null;
        //        if (IrfTable.Count > 0)
        //            result = new List<ShippingTagInfo>();

        //        //构建表结构
        //        //DataTable table = new DataTable();
        //        //table.Columns.Add("SHIP_DATE");
        //        //table.Columns.Add("STORE_ID");
        //        //table.Columns.Add("STORE_NAME");
        //        //table.Columns.Add("DISPATCH_AREA");
        //        //table.Columns.Add("COLLECT_WAVE");
        //        //table.Columns.Add("VSART_DES");
        //        //table.Columns.Add("ROUTE_NO");
        //        //table.Columns.Add("ADDRESS");
        //        //table.Columns.Add("TEL_NUMBER");
        //        //table.Columns.Add("LANE_ID");

        //        //插入表数据
        //        for (int i = 0; i < IrfTable.Count; i++)
        //        {
        //            IrfTable.CurrentIndex = i;
        //            //DataRow dr = table.NewRow();
        //            ShippingTagInfo item = new ShippingTagInfo();
        //            item.SHIP_DATE = DateTime.Parse(IrfTable.GetString("SHIP_DATE"));
        //            item.STORE_ID = IrfTable.GetString("STORE_ID");
        //            item.STORE_NAME = IrfTable.GetString("STORE_NAME");
        //            item.DISPATCH_AREA = IrfTable.GetString("DISPATCH_AREA");
        //            item.COLLECT_WAVE = IrfTable.GetString("COLLECT_WAVE");
        //            item.VSART_DES = IrfTable.GetString("VSART_DES");
        //            item.ROUTE_NO = IrfTable.GetString("ROUTE_NO");
        //            item.ADDRESS = IrfTable.GetString("ADDRESS");
        //            item.TEL_NUMBER = IrfTable.GetString("TEL_NUMBER");
        //            item.LANE_ID = IrfTable.GetString("LANE_ID");
        //            //table.Rows.Add(dr);
        //            result.Add(item);
        //        }
        //        RfcSessionManager.EndContext(dest);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex.Message, ex.StackTrace);
        //    }
        //    return null;
        //}

        /// <summary>
        /// 获取所有下架单明细
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="dTime">发运日期</param>
        /// <param name="sLGTYP">存储类型</param>
        /// <returns></returns>
        public static List<InventoryOutLogDetailInfo> GetHLAShelvesSingleList(string lgnum, string sTime, string sLGTYP)
        {
            List<InventoryOutLogDetailInfo>  result = new List<InventoryOutLogDetailInfo>();

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_017");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("SHIP_DATE", sTime);//发运日期
                myfun.SetValue("LGTYP", sLGTYP);//存储类型 
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DETAIL");
                //构建表结构
                //DataTable table = new DataTable();
                //table.Columns.Add("LGNUM");//仓库编号
                //table.Columns.Add("PICK_TASK");//下架单号
                //table.Columns.Add("PICK_TASK_ITEM");//下架单行项目号
                //table.Columns.Add("DOCNO");//交货单号
                //table.Columns.Add("ITEMNO");//交货单行项目号
                //table.Columns.Add("PRODUCTNO");//产品
                //table.Columns.Add("QTY");//数量
                //table.Columns.Add("UOM");//单位
                //table.Columns.Add("SHIP_DATE");//发运日期
                //table.Columns.Add("PARTNER");//业务伙伴编号(门店)
                //table.Columns.Add("STOBIN");//仓位
                //table.Columns.Add("LGTYP");//系统存储类型
                //table.Columns.Add("LGTYP_R");//实际存储类型
                //table.Columns.Add("ZXJD_TYPE");//下架单类型(2表示RFID下架单)

                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    InventoryOutLogDetailInfo item = new InventoryOutLogDetailInfo();
                    //DataRow dr = table.NewRow();
                    item.LGNUM = IrfTable.GetString("LGNUM");
                    item.PICK_TASK = IrfTable.GetString("PICK_TASK");
                    item.PICK_TASK_ITEM = IrfTable.GetString("PICK_TASK_ITEM");
                    item.DOCNO = IrfTable.GetString("DOCNO");
                    if(string.IsNullOrEmpty(item.DOCNO))
                    {
                        item.DOCNO = "";
                    }
                    item.ITEMNO = IrfTable.GetString("ITEMNO");
                    if(string.IsNullOrEmpty(item.ITEMNO))
                    {
                        item.ITEMNO = "";
                    }
                    item.PRODUCTNO = IrfTable.GetString("PRODUCTNO");  //产品编码SKU
                    item.QTY = (int)IrfTable.GetDouble("QTY");
                    item.UOM = IrfTable.GetString("UOM");
                    item.SHIP_DATE = DateTime.Parse(IrfTable.GetString("SHIP_DATE"));
                    item.PARTNER = IrfTable.GetString("PARTNER");
                    if(string.IsNullOrEmpty(item.PARTNER))
                    {
                        item.PARTNER = "";
                    }
                    item.STOBIN = IrfTable.GetString("STOBIN");
                    item.LGTYP = IrfTable.GetString("LGTYP");
                    item.LGTYP_R = IrfTable.GetString("LGTYP_R");
                    item.ZXJD_TYPE = IrfTable.GetString("ZXJD_TYPE");
                    item.MX = IrfTable.GetString("MX");
                    item.IsOut = IrfTable.GetString("XJSTATUS").ToUpper() == "X" ? (byte)1 : (byte)0;
                    if (!string.IsNullOrEmpty(item.DOCNO))
                        item.DOCNO = item.DOCNO.TrimStart('0');
                    if (!string.IsNullOrEmpty(item.ITEMNO))
                        item.ITEMNO = item.ITEMNO.TrimStart('0');

                    string VSART = IrfTable.GetString("VSART");
                    item.VSART = VSART != null ? VSART.Trim() : "";

                    string is_fbc = IrfTable.GetString("IS_FBC");
                    item.IS_FBC = is_fbc != null ? is_fbc.Trim() : "";

                    item.BRAND = getZiDuan(IrfTable, "BRAND");
                    result.Add(item);
                }

                RfcSessionManager.EndContext(dest);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }
            return result;
        }
        /// <summary>
        /// 获取所有下架单明细_三禾（合作商）
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="dTime">发运日期</param>
        /// <param name="sLGTYP">存储类型</param>
        /// <returns></returns>
        public static List<InventoryOutLogDetailInfo> GetHLASanHeList(string lgnum)
        {
            List<InventoryOutLogDetailInfo>  result = new List<InventoryOutLogDetailInfo>();

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_050");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                //myfun.SetValue("SHIP_DATE", sTime);//发运日期
                //myfun.SetValue("LGTYP", sLGTYP);//存储类型 
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DETAIL");
                //插入表数据
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    InventoryOutLogDetailInfo item = new InventoryOutLogDetailInfo();
                    //DataRow dr = table.NewRow();
                    item.LGNUM = IrfTable.GetString("LGNUM");//仓库编号
                    item.PICK_TASK_ITEM = IrfTable.GetString("SEQNO");//领路牌流水号
                    item.SHIP_DATE = IrfTable.GetString("SHIP_DATE").CastTo<DateTime>(DateTime.Now.Date); ;//发运日期
                    item.PICK_TASK = IrfTable.GetString("LLP");//领路牌
                    item.DOCNO = IrfTable.GetString("DOCNO");//凭证编号
                    item.ITEMNO = IrfTable.GetString("ITEMNO");//项目编号
                    item.LGTYP_R = IrfTable.GetString("STOTYPER");//存储类型 
                    item.ZXJD_TYPE = IrfTable.GetString("ZXJD_TYPE");//下架单类型(2表示RFID下架单 4为领路牌)
                    item.PARTNER = IrfTable.GetString("PARTNER");//合作伙伴编码
                    item.PRODUCTNO = IrfTable.GetString("SKU");//业务伙伴编号(门店)
                    item.QTY = IrfTable.GetString("QTY").CastTo<int>(0);//业务伙伴编号(门店)
                    item.UOM = IrfTable.GetString("UOM");//单位
                    item.Status = IrfTable.GetString("STATUS").CastTo<short>(0);//数据状态 0-初始状态，9-已上传的数据

                    if (!string.IsNullOrEmpty(item.DOCNO))
                        item.DOCNO = item.DOCNO.TrimStart('0');
                    if (!string.IsNullOrEmpty(item.ITEMNO))
                        item.ITEMNO = item.ITEMNO.TrimStart('0');

                    //string VSART = IrfTable.GetString("VSART");
                    //item.VSART = VSART != null ? VSART.Trim() : "";

                    //string is_fbc = IrfTable.GetString("IS_FBC");
                    //item.IS_FBC = is_fbc != null ? is_fbc.Trim() : "";
                    item.VSART = "";
                    item.IS_FBC = "";

                    result.Add(item);
                }
                RfcSessionManager.EndContext(dest);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }
            return result;
        }

        public static CJieHuoDan GetJieHuoDan(string lgnum,string docno)
        {
            CJieHuoDan re = new CJieHuoDan();
            re.PICK_LIST = docno;

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_073");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("PICK_LIST", docno);//编号 
                myfun.Invoke(dest);
                IRfcTable IrfTable = myfun.GetTable("ET_DETAIL");

                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;

                    CJieHuoDanDetail jd = new CJieHuoDanDetail();

                    jd.PICK_LIST = IrfTable.GetString("PICK_LIST");
                    jd.PICK_LIST_ITEM = IrfTable.GetString("PICK_LIST_ITEM");
                    jd.PRODUCTNO = IrfTable.GetString("PRODUCTNO");

                    string qty_jd = IrfTable.GetString("QTY");
                    if(!string.IsNullOrEmpty(qty_jd))
                    {
                        int int_qty_jd = 0;
                        int.TryParse(qty_jd, out int_qty_jd);
                        jd.QTY = int_qty_jd;
                    }
                    jd.SHIP_DATE = IrfTable.GetString("SHIP_DATE");
                    jd.PICK_DATE = IrfTable.GetString("PICK_DATE");
                    jd.WAVEID = IrfTable.GetString("WAVEID");
                    jd.EXPORT_NO = IrfTable.GetString("EXPORT_NO");
                    jd.STOTYPE = IrfTable.GetString("/SCWM/STOTYPE");
                    jd.STOBIN = IrfTable.GetString("STOBIN");

                    re.mJieHuo.Add(jd);
                }

                re.mStatus = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）
                re.mMsg = myfun.GetString("MSG_OUT");//返回消息

                RfcSessionManager.EndContext(dest);
            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
                re.mStatus = "E";
                re.mMsg = e.ToString();
            }

            return re;
        }
        /// <summary>
        /// 根据下架单号获取下架单信息
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="MATNR">下架单编号</param>
        /// <returns></returns>
        public static List<InventoryOutLogDetailInfo> GetHLAShelvesSingleTask(string lgnum, string sTask)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_028");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("PICK_TASK", sTask);//下架单编号 
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DETAIL");
                List<InventoryOutLogDetailInfo> result = null;
                if (IrfTable.Count > 0)
                    result = new List<InventoryOutLogDetailInfo>();
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    //插入表数据  
                    InventoryOutLogDetailInfo item = new InventoryOutLogDetailInfo();
                    item.LGNUM = IrfTable.GetString("LGNUM");
                    item.PICK_TASK = IrfTable.GetString("PICK_TASK");
                    item.PICK_TASK_ITEM = IrfTable.GetString("PICK_TASK_ITEM");
                    item.DOCNO = IrfTable.GetString("DOCNO");
                    if(string.IsNullOrEmpty(item.DOCNO))
                    {
                        item.DOCNO = "";
                    }
                    item.ITEMNO = IrfTable.GetString("ITEMNO");
                    if(string.IsNullOrEmpty(item.ITEMNO))
                    {
                        item.ITEMNO = "";
                    }
                    item.PRODUCTNO = IrfTable.GetString("PRODUCTNO");  //产品编码SKU
                    item.QTY = (int)IrfTable.GetDouble("QTY");
                    item.UOM = IrfTable.GetString("UOM");
                    item.SHIP_DATE = DateTime.Parse(IrfTable.GetString("SHIP_DATE"));
                    item.PARTNER = IrfTable.GetString("PARTNER");
                    if(string.IsNullOrEmpty(item.PARTNER))
                    {
                        item.PARTNER = "";
                    }
                    item.STOBIN = IrfTable.GetString("STOBIN");
                    item.LGTYP = IrfTable.GetString("LGTYP");
                    item.LGTYP_R = IrfTable.GetString("LGTYP_R");
                    item.ZXJD_TYPE = IrfTable.GetString("ZXJD_TYPE");
                    item.IsOut = IrfTable.GetString("XJSTATUS").ToUpper() == "X" ? (byte)1 : (byte)0;
                    item.MX = IrfTable.GetString("MX");

                    string VSART = IrfTable.GetString("VSART");
                    item.VSART = VSART != null ? VSART.Trim() : "";

                    string is_fbc = IrfTable.GetString("IS_FBC");
                    item.IS_FBC = is_fbc != null ? is_fbc.Trim() : "";

                    if (!string.IsNullOrEmpty(item.DOCNO))
                        item.DOCNO = item.DOCNO.TrimStart('0');
                    if (!string.IsNullOrEmpty(item.ITEMNO))
                        item.ITEMNO = item.ITEMNO.TrimStart('0');

                    item.BRAND = getZiDuan(IrfTable, "BRAND");

                    result.Add(item);
                }
                RfcSessionManager.EndContext(dest);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }
            return null;
        }


        /// <summary>
        /// 上传下架单
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="picktask"></param>
        /// <param name="louceng"></param>
        /// <param name="outLogList"></param>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public static bool UploadOutLogInfo(string lgnum, string picktask, string louceng,
            List<InventoryOutLogDetailInfo> outLogList, List<OutLogErrorRecord> errorList,
            List<ShippingBox> boxList, List<ShippingBoxDetail> boxdetailList, out string errorMsg)
        {
            try
            {
                //if (SysConfig.IsTest)
                //{
                //    errorMsg = "测试中...";
                //    return true;
                //}

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RF_051");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("PICK_TASK", picktask);//交货单号
                myfun.SetValue("LOUCENG", louceng);//仓库楼层
                myfun.SetValue("CHUSER", SysConfig.CurrentLoginUser.UserId);//提交用户
                myfun.SetValue("EQUIP_HLA", SysConfig.DeviceInfo.EQUIP_HLA);//设备终端号

                IRfcStructure import = null;
                IRfcTable IrfTable1 = myfun.GetTable("IT_DATA");
                if (boxdetailList != null && boxdetailList.Count > 0)
                {
                    List<ShippingBoxDetail> distinctDetailList = boxdetailList.FindAll(i => i.IsADD != 1 && i.PICK_TASK == picktask).Distinct(new ShippingBoxDetailComparer()).ToList();
                    Dictionary<string, int> boxsum = new Dictionary<string, int>(); //用来计算已经拆箱的数量
                    foreach (ShippingBoxDetail item in distinctDetailList)
                    {
                        var key = item.PICK_TASK + "|" + item.PICK_TASK_ITEM;
                        if (!boxsum.ContainsKey(key))
                        {
                            boxsum.Add(key, 0);
                        }
                        bool islastbox = boxList.FindIndex(i => i.HU == item.HU) == (boxList.Count - 1);
                        import = rfcrep.GetStructureMetadata("ZSEW_RF_051STR").CreateStructure();
                        import.SetValue("HU", item.HU);//箱号
                        import.SetValue("PACKMAT", boxList.Find(i => i.HU == item.HU).PMAT_MATNR);//包装物料
                        import.SetValue("PICK_TASK_ITEM", item.PICK_TASK_ITEM);//下架单行项目
                        import.SetValue("LGPLA", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).STOBIN);//仓位
                        import.SetValue("DOCNO", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).DOCNO);//凭证编号
                        import.SetValue("ITEMNO", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).ITEMNO);//项目编号
                        import.SetValue("MATNR", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).PRODUCTNO);//产品编号
                        import.SetValue("CHARG", item.CHARG);//批号
                        var plancount = outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).QTY;
                        var realcount = boxdetailList.FindAll(i => i.HU == item.HU && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM && i.PICK_TASK == item.PICK_TASK && i.IsADD != 1).Count;
                        import.SetValue("QTY1", islastbox ? plancount - boxsum[key] : realcount);//计划数量
                        import.SetValue("QTY2", realcount + ".00");//实际数量
                        import.SetValue("QTY_DJ", islastbox ? plancount - boxsum[key] - realcount : 0);

                        import.SetValue("UOM", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).UOM);//计量单位
                        import.SetValue("MX", boxList.Find(i => i.HU == item.HU).IsFull == 1 ? "X" : "");   //满箱标记
                        IrfTable1.Insert(import);
                        boxsum[key] = boxsum[key] + realcount;
                    }
                }

                //List<InventoryOutLogDetailInfo> zeroOutLogList = outLogList.FindAll(i => i.PICK_TASK == picktask && i.REALQTY == 0);
                List<InventoryOutLogDetailInfo> zeroOutLogList = outLogList.FindAll(i => i.PICK_TASK == picktask && i.REALQTY == 0);
                if (zeroOutLogList != null && zeroOutLogList.Count > 0)
                {
                    foreach (InventoryOutLogDetailInfo logDetail in zeroOutLogList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSEW_RF_051STR").CreateStructure();
                        import.SetValue("HU", "");//箱号
                        import.SetValue("PACKMAT", "");//包装物料
                        import.SetValue("PICK_TASK_ITEM", logDetail.PICK_TASK_ITEM);//下架单行项目
                        import.SetValue("LGPLA", logDetail.STOBIN);//仓位
                        import.SetValue("DOCNO", logDetail.DOCNO);//凭证编号
                        import.SetValue("ITEMNO", logDetail.ITEMNO);//项目编号
                        import.SetValue("MATNR", logDetail.PRODUCTNO);//产品编号
                        import.SetValue("CHARG", "");//批号
                        import.SetValue("QTY1", logDetail.QTY);//计划数量
                        import.SetValue("QTY2", logDetail.REALQTY);//实际数量
                        import.SetValue("QTY_DJ",logDetail.DJQTY);
                        import.SetValue("UOM", logDetail.UOM);//计量单位
                        import.SetValue("MX", "");  //满箱标记
                        IrfTable1.Insert(import);
                    }
                }

                IRfcTable IrfTable2 = myfun.GetTable("IT_ERR_PICK");
                if (errorList != null && errorList.Count > 0)
                {
                    var list = errorList.Select(i => new { i.MATNR, i.BARCD,i.ERRTYPE }).Distinct();
                    foreach (var item in list)
                    {
                        string errTypeStr = null;
                        if (item.ERRTYPE == ErrorType.多拣)
                            errTypeStr = "多拣";
                        else if (item.ERRTYPE == ErrorType.拣错)
                            errTypeStr = "拣错";
                        else
                            errTypeStr = "少拣";

                        import = rfcrep.GetStructureMetadata("ZSEW_RF_051STR2").CreateStructure();
                        import.SetValue("MATNR", item.MATNR);//产品编号
                        import.SetValue("BARCD", item.BARCD);//条码
                        import.SetValue("ERRTYPE", errTypeStr);//错误类型（拣错、多拣）
                        import.SetValue("QTY", item.ERRTYPE == ErrorType.少拣 ? errorList.Find(i => i.MATNR == item.MATNR).QTY + ".000"
                            : errorList.Count(i => i.MATNR == item.MATNR && i.BARCD == item.BARCD && i.ERRTYPE == item.ERRTYPE) + ".000");
                        IrfTable2.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                errorMsg = myfun.GetString("MSG");//返回消息

                RfcSessionManager.EndContext(dest);
                #region 打印数据
                //LogHelper.WriteLine("发送给SAP数据："+myfun.ToString());
                //LogHelper.WriteLine(string.Format("本地要上传的数据：{0}", JsonConvert.SerializeObject(errorList)));
                #endregion
                if (status == "S")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                errorMsg = ex.Message;
            }
            return false;
        }
        /// <summary>
        /// 上传下架单_三禾数据 by linzw 160513
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="picktask"></param>
        /// <param name="louceng"></param>
        /// <param name="outLogList"></param>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public static bool UploadOutLogInfo_051(string lgnum, string seqNo, string llp, string louceng,
            List<InventoryOutLogDetailInfo> outLogList, List<OutLogErrorRecord> errorList,
            List<ShippingBox> boxList, List<ShippingBoxDetail> boxdetailList, out string errorMsg)
        {
            try
            {
                if (SysConfig.IsTest)
                {
                    errorMsg = "测试中...";
                    return true;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_051");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("SEQNO", seqNo);//领路牌流水号
                myfun.SetValue("LLP", llp);//领路牌
                myfun.SetValue("LOUCENG", louceng);//仓库楼层
                myfun.SetValue("EQUIP_HLA", SysConfig.DeviceInfo.EQUIP_HLA);//设备终端号(海澜)
                myfun.SetValue("CHUSER", SysConfig.CurrentLoginUser.UserId);//提交人
                IRfcStructure import = null;
                IRfcTable IrfTable1 = myfun.GetTable("IT_DATA");
                if (boxdetailList != null && boxdetailList.Count > 0)
                {
                    List<ShippingBoxDetail> distinctDetailList = boxdetailList.FindAll(i => i.IsADD != 1 && i.PICK_TASK == llp).Distinct(new ShippingBoxDetailComparer()).ToList();
                    Dictionary<string, int> boxsum = new Dictionary<string, int>(); //用来计算已经拆箱的数量
                    foreach (ShippingBoxDetail item in distinctDetailList)
                    {
                        var key = item.PICK_TASK + "|" + item.PICK_TASK_ITEM;
                        if (!boxsum.ContainsKey(key))
                        {
                            boxsum.Add(key, 0);
                        }
                        bool islastbox = boxList.FindIndex(i => i.HU == item.HU) == (boxList.Count - 1);
                        import = rfcrep.GetStructureMetadata("ZSEW_RF_051STR").CreateStructure();
                        import.SetValue("HU", item.HU);//箱号
                        import.SetValue("PACKMAT", boxList.Find(i => i.HU == item.HU).PMAT_MATNR);//包装物料
                        import.SetValue("PICK_TASK_ITEM", item.PICK_TASK_ITEM);//下架单行项目
                        import.SetValue("LGPLA", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).STOBIN);//仓位
                        import.SetValue("DOCNO", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).DOCNO);//凭证编号
                        import.SetValue("ITEMNO", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).ITEMNO);//项目编号
                        import.SetValue("MATNR", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).PRODUCTNO);//产品编号
                        import.SetValue("CHARG", item.CHARG);//批号
                        var plancount = outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).QTY;
                        var realcount = boxdetailList.FindAll(i => i.HU == item.HU && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM && i.PICK_TASK == item.PICK_TASK && i.IsADD != 1).Count;
                        import.SetValue("QTY1", islastbox ? plancount - boxsum[key] : realcount);//计划数量
                        import.SetValue("QTY2", realcount + ".00");//实际数量
                        import.SetValue("UOM", outLogList.Find(i => i.PICK_TASK == item.PICK_TASK && i.PICK_TASK_ITEM == item.PICK_TASK_ITEM).UOM);//计量单位
                        import.SetValue("MX", boxList.Find(i => i.HU == item.HU).IsFull == 1 ? "X" : "");   //满箱标记
                        IrfTable1.Insert(import);
                        boxsum[key] = boxsum[key] + realcount;
                    }
                }

                //List<InventoryOutLogDetailInfo> zeroOutLogList = outLogList.FindAll(i => i.PICK_TASK == picktask && i.REALQTY == 0);
                List<InventoryOutLogDetailInfo> zeroOutLogList = outLogList.FindAll(i => i.PICK_TASK == llp && i.REALQTY == 0);
                if (zeroOutLogList != null && zeroOutLogList.Count > 0)
                {
                    foreach (InventoryOutLogDetailInfo logDetail in zeroOutLogList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSEW_RF_051STR").CreateStructure();
                        import.SetValue("HU", "");//箱号
                        import.SetValue("PACKMAT", "");//包装物料
                        import.SetValue("PICK_TASK_ITEM", logDetail.PICK_TASK_ITEM);//下架单行项目
                        import.SetValue("LGPLA", logDetail.STOBIN);//仓位
                        import.SetValue("DOCNO", logDetail.DOCNO);//凭证编号
                        import.SetValue("ITEMNO", logDetail.ITEMNO);//项目编号
                        import.SetValue("MATNR", logDetail.PRODUCTNO);//产品编号
                        import.SetValue("CHARG", "");//批号
                        import.SetValue("QTY1", logDetail.QTY);//计划数量
                        import.SetValue("QTY2", logDetail.REALQTY);//实际数量
                        import.SetValue("UOM", logDetail.UOM);//计量单位
                        import.SetValue("MX", "");  //满箱标记
                        IrfTable1.Insert(import);
                    }
                }

                IRfcTable IrfTable2 = myfun.GetTable("IT_ERR_PICK");
                if (errorList != null && errorList.Count > 0)
                {
                    foreach (OutLogErrorRecord item in errorList)
                    {
                        string errTypeStr = null;
                        if (item.ERRTYPE == ErrorType.多拣)
                            errTypeStr = "多拣";
                        else if (item.ERRTYPE == ErrorType.拣错)
                            errTypeStr = "拣错";
                        else
                            errTypeStr = "少拣";

                        import = rfcrep.GetStructureMetadata("ZSEW_RF_051STR2").CreateStructure();
                        import.SetValue("MATNR", item.MATNR);//产品编号
                        import.SetValue("BARCD", item.BARCD);//条码
                        import.SetValue("ERRTYPE", errTypeStr);//错误类型（拣错、多拣）
                        IrfTable2.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                errorMsg = myfun.GetString("MSG");//返回消息

                RfcSessionManager.EndContext(dest);

                if (status == "S")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                errorMsg = ex.Message;
            }

            return false;
        }

        public static string nullStr(object obj)
        {
            try
            {
                if (obj == null)
                    return "";
                else
                    return obj.ToString();
            }
            catch (Exception)
            {

            }
            return "";
        }
        /// <summary>
        /// 根据发运日期获取发运标签信息列表
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="shippingDate"></param>
        /// <returns></returns>
        public static List<ShippingLabel> GetShippingLabelList(string lgnum, string shippingDate)
        {
            List<ShippingLabel> result = new List<ShippingLabel>();

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_018");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("SHIP_DATE", shippingDate);//发运日期 
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DETAIL");
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    //插入表数据  
                    ShippingLabel item = new ShippingLabel();
                    item.SHIP_DATE = DateTime.Parse(IrfTable.GetString("SHIP_DATE").Trim());
                    item.STORE_ID = IrfTable.GetString("STORE_ID").Trim();
                    item.STORE_NAME = IrfTable.GetString("STORE_NAME").Trim();
                    item.DISPATCH_AREA = IrfTable.GetString("DISPATCH_AREA").Trim();
                    item.COLLECT_WAVE = IrfTable.GetString("COLLECT_WAVE").Trim();
                    item.VSART_DES = IrfTable.GetString("VSART_DES").Trim();
                    item.ROUTE_NO = IrfTable.GetString("ROUTE_NO").Trim();
                    item.ROUTE_DES = IrfTable.GetString("ROUTE_DES").Trim();
                    item.ADDRESS = IrfTable.GetString("ADDRESS").Trim();
                    item.TEL_NUMBER = IrfTable.GetString("TEL_NUMBER").Trim();
                    item.LANE_ID = IrfTable.GetString("LANE_ID").Trim();

                    item.FYDT = IrfTable.GetString("FYDT").Trim();
                    item.VSART = IrfTable.GetString("VSART").Trim();
                    item.ZYT = IrfTable.GetString("ZYT").Trim();
                    item.FY_WAVE = IrfTable.GetString("FY_WAVE").Trim();
                    item.WAVE_AND_YT = IrfTable.GetString("WAVE_AND_YT").Trim();

                    string is_fbc = IrfTable.GetString("IS_FBC");
                    item.IS_FBC = is_fbc != null ? is_fbc.Trim() : "";

                    string collect_seq = IrfTable.GetString("COLLECT_SEQ").Trim();
                    item.COLLECT_SEQ = collect_seq != null ? collect_seq : "";

                    //string zsdabw = IrfTable.GetString("ZSDABW").Trim();
                    //item.ZSDABW = zsdabw != null ? zsdabw : "";

                    //string zsdabw_des = IrfTable.GetString("ZSDABW_DES").Trim();
                    //item.ZSDABW_DES = zsdabw != null ? zsdabw_des : "";

                    item.ZSDABW = getZiDuan(IrfTable, "ZSDABW");
                    item.ZSDABW_DES = getZiDuan(IrfTable, "ZSDABW_DES");

                    item.DOCNO = getZiDuan(IrfTable, "DOCNO");
                    item.DOCNO = item.DOCNO.TrimStart('0');

                    item.PICK_DATE = Convert.ToDateTime("2000-1-1");
                    string PICK_DATE = getZiDuan(IrfTable, "PICK_DATE");
                    if(!string.IsNullOrEmpty(PICK_DATE))
                    {
                        DateTime pd = Convert.ToDateTime("2000-1-1");
                        DateTime.TryParse(PICK_DATE, out pd);
                        item.PICK_DATE = pd;
                    }

                    result.Add(item);
                }
                RfcSessionManager.EndContext(dest);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }
            return result;
        }
        public static string getZiDuan(IRfcTable t,string ziduan)
        {
            try
            {
                return t.GetString(ziduan).Trim();
            }
            catch(Exception)
            {

            }

            return "";
        }

        public static bool UploadSAPOutLogInfo(out string errorMsg)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RF_051");
                myfun.SetValue("LGNUM", "HL01");//仓库编号
                myfun.SetValue("PICK_TASK", "1005441877");//交货单号
                myfun.SetValue("LOUCENG", "141");//仓库楼层
                myfun.SetValue("CHUSER", "069675");//提交用户
                myfun.SetValue("EQUIP_HLA", "HLCS3001");//设备终端号

                IRfcStructure import = null;
                IRfcTable IrfTable1 = myfun.GetTable("IT_DATA");

                import = rfcrep.GetStructureMetadata("ZSEW_RF_051STR").CreateStructure();
                import.SetValue("HU", "29058859");//箱号
                import.SetValue("PACKMAT", "0000000000000000000000000000001200000002");//包装物料
                import.SetValue("PICK_TASK_ITEM", "0010");//下架单行项目
                import.SetValue("LGPLA", "1411-01");//仓位
                import.SetValue("DOCNO", "30353");//凭证编号
                import.SetValue("ITEMNO", "20");//项目编号
                import.SetValue("MATNR", "HTXAD3A105AA5001");//产品编号
                import.SetValue("CHARG", "A1");//批号
                import.SetValue("QTY1", "1");//计划数量
                import.SetValue("QTY2", "2");//实际数量
                import.SetValue("UOM", "CS");//计量单位
                IrfTable1.Insert(import);

                //IRfcTable IrfTable2 = myfun.GetTable("IT_ERR_PICK");
                //{
                //    string errTypeStr = null;
                //    if (item.ERRTYPE == ErrorType.多拣)
                //        errTypeStr = "多拣";
                //    else if (item.ERRTYPE == ErrorType.拣错)
                //        errTypeStr = "拣错";
                //    else
                //        errTypeStr = "少拣";

                //    import = rfcrep.GetStructureMetadata("ZSEW_RF_051STR2").CreateStructure();
                //    import.SetValue("MATNR", item.MATNR);//产品编号
                //    import.SetValue("BARCD", item.BARCD);//条码
                //    import.SetValue("ERRTYPE", errTypeStr);//错误类型（拣错、多拣）
                //    IrfTable2.Insert(import);
                //}


                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                errorMsg = myfun.GetString("MSG");//返回消息

                RfcSessionManager.EndContext(dest);

                if (status == "S")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                errorMsg = ex.Message;
            }

            return false;
        }

        #endregion

        #region 电商大通道机接口
        /// <summary>
        /// 电商收货明细上传
        /// </summary>
        /// <param name="lgnum">仓库编号，默认"HL01"</param>
        /// <param name="equip_hla">设备终端号</param>
        /// <param name="hu">（处理单位标识）箱码</param>
        /// <param name="changeTime">日期时间</param>
        /// <param name="inventoryResult">通道机状态（正常口‘S’异常口‘E’）</param>
        /// <param name="msg">返回消息</param>
        /// <param name="subuser">提交用户</param>
        /// <param name="barcdList">吊牌列表</param>
        /// <returns></returns>
        public static bool UploadEbBoxInfo(string lgnum, string equip_hla, string hu, DateTime changeTime, bool inventoryResult, string msg, string subuser, List<TagDetailInfo> tagDetailList)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_039");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("EQUIP_HLA", equip_hla);
                myfun.SetValue("HU", hu);
                myfun.SetValue("CHANGE_D", changeTime.ToString("yyyyMMdd"));
                myfun.SetValue("CHANGE_T", changeTime.ToString("HHmmss"));
                string STATUS_IN;
                if (!inventoryResult && !string.IsNullOrEmpty(msg))
                    STATUS_IN = "A";
                else if (inventoryResult)
                    STATUS_IN = "S";
                else
                    STATUS_IN = "E";
                myfun.SetValue("STATUS_IN", STATUS_IN);
                myfun.SetValue("MSG_IN", msg);
                myfun.SetValue("SUBUSER", subuser);//提交用户 当前登录用户

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DATA");
                IRfcTable IrfTable2 = myfun.GetTable("IT_EPC");
                if (tagDetailList != null && tagDetailList.Count > 0)
                {
                    List<string> barcodelist = tagDetailList.Select(o => o.BARCD).Distinct().ToList();//获取条码列表
                    if (barcodelist != null && barcodelist.Count > 0)
                    {
                        foreach (string barcode in barcodelist)
                        {
                            int qty = tagDetailList.Count(o => o.BARCD == barcode && !o.IsAddEpc);//统计对应主条码的数量
                            import = rfcrep.GetStructureMetadata("ZSRFID039STR").CreateStructure();
                            import.SetValue("BARCD", barcode);
                            import.SetValue("QTY", qty);
                            IrfTable.Insert(import);
                        }
                    }

                    foreach (TagDetailInfo tag in tagDetailList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID020STR").CreateStructure();
                        import.SetValue("EPC_SER", tag.EPC);
                        IrfTable2.Insert(import);
                    }
                }


                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                string msg_sap = myfun.GetString("MSG");
                LogHelper.WriteLine(msg_sap);
                RfcSessionManager.EndContext(dest);
                if (status == "S")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }
        private static void trySetValue(IRfcFunction fun,string k,string v)
        {
            try
            {
                fun.SetValue(k, v);
            }
            catch(Exception)
            {

            }
        }
        /// <summary>
        /// 装箱信息获取
        /// 传HU数据的话，默认单箱获取；
        /// 如果是多箱获取，发运日期和门店编码必须输入
        /// </summary>
        /// <param name="lgnum">仓库编号/综合仓库（默认'HL01'）</param>
        /// <param name="hu">箱号</param>
        /// <param name="ship_date">发运日期</param>
        /// <param name="partner">门店编码</param>
        /// <param name="md_type">
        /// 门店类型（’S’16#3楼,’D’电商）
        /// 目前信达统一传“D”
        /// </param>
        /// <returns></returns>
        public static List<EbBoxInfo> GetEbBoxList(string lgnum, string hu, string ship_date,
            string partner, out string errormsg, string md_type = "S",string qinagzhi = "",string vendor = "C")
        {
            errormsg = "";
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_038");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("HU", hu);
                myfun.SetValue("SHIP_DATE", ship_date);
                myfun.SetValue("PARTNER", partner);
                myfun.SetValue("MD_TYPE", md_type);
                //myfun.SetValue("VENDOR", vendor);
                trySetValue(myfun, "VENDOR", vendor);

                if (qinagzhi == "")
                {
                    if (!string.IsNullOrEmpty(hu.Trim()))
                    {
                        //箱码不为空时，强制获取
                        myfun.SetValue("RE_SYNC", "X");
                    }
                    else
                    {
                        myfun.SetValue("RE_SYNC", "");
                    }
                }
                else
                {
                    myfun.SetValue("RE_SYNC", qinagzhi);
                }


                myfun.Invoke(dest);
                string result = myfun.GetString("STATUS");
                if (result == "S")
                {
                    IRfcTable IrfTable = myfun.GetTable("ET_DATA");
                    List<EbBoxInfo> list = new List<EbBoxInfo>();

                    //插入表数据
                    for (int i = 0; i < IrfTable.Count; i++)
                    {
                        IrfTable.CurrentIndex = i;
                        EbBoxInfo box = new EbBoxInfo();
                        box.HU = IrfTable.GetString("HU").TrimStart('0');
                        box.PRODUCTNO = IrfTable.GetString("PRODUCTNO");
                        box.QTY = (int)(double.Parse(IrfTable.GetString("QTY")));
                        box.SHIPDATE = DateTime.Parse(IrfTable.GetString("SHIP_DATE"));
                        list.Add(box);
                    }

                    RfcSessionManager.EndContext(dest);

                    return list;
                }
                else
                {
                    errormsg = myfun.GetString("MSG");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取上架仓位（电商收货复核使用）
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="matnr">产品编号</param>
        /// <returns></returns>
        public static string GetShelvesPosition(string lgnum, string matnr)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_046");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("MATNR", matnr);
                myfun.Invoke(dest);
                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                string msg = myfun.GetString("MSG");
                string lgpla = myfun.GetString("LGPLA");
                RfcSessionManager.EndContext(dest);

                if (status == "S")
                {
                    return lgpla;
                }
                else
                {
                    LogHelper.WriteLine("获取上架仓位时错误，错误信息：" + msg);
                    return lgpla;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return "";
        }

        /// <summary>
        /// 用于删除16#3楼已同步的箱数据
        /// </summary>
        /// <param name="lgnum"></param>
        /// <param name="shipDate"></param>
        /// <param name="hulist"></param>
        /// <returns></returns>
        public static bool DeleteDownloadedBox(string lgnum, DateTime shipDate, List<string> hulist,string vendor = "C")
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_040");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("SHIP_DATE", shipDate.ToString("yyyy-MM-dd"));
                //myfun.SetValue("VENDOR", vendor);
                trySetValue(myfun, "VENDOR", vendor);

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DATA");

                if (hulist != null && hulist.Count > 0)
                {
                    foreach (string hu in hulist)
                    {
                        import = rfcrep.GetStructureMetadata("ZHUSTR").CreateStructure();
                        import.SetValue("HU", hu);
                        IrfTable.Insert(import);
                    }
                }


                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）

                RfcSessionManager.EndContext(dest);

                if (status == "S")
                    return true;
                else
                {
                    LogHelper.WriteLine(myfun.GetString("MSG"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }
        #endregion

        #region 平库大批量发货通道机相关代码
        public static SapResult UploadPKBoxInfo_066(UploadPKBoxInfo item)
        {
            SapResult result = new SapResult();

            if (SysConfig.IsTest)
            {
                result.STATUS = "S";
                result.MSG = "成功";
                return result;
            }
            try
            {
                if(!item.IsJieHuoDanDeliver)
                {
                    result.STATUS = "E";
                    result.MSG = "下架单类型不为5";
                    return result;
                }

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_066");
                myfun.SetValue("LGNUM", item.LGNUM);
                myfun.SetValue("SHIP_DATE", item.SHIP_DATE.ToString("yyyyMMdd"));
                //myfun.SetValue("PARTNER", item.PARTNER);
                myfun.SetValue("HU", item.HU);
                myfun.SetValue("STATUS_IN", item.InventoryResult ? "S" : "E");
                myfun.SetValue("MSG_IN", "");
                myfun.SetValue("SUBUSER", item.SubUser);//提交用户 当前登录用户
                myfun.SetValue("LOUCENG", item.LOUCENG);
                myfun.SetValue("EQUIP_HLA", item.EQUIP_HLA);

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");

                var uploadlist = new List<SAP019Entity>();
                if (item.DeliverErrorBoxList != null && item.DeliverErrorBoxList.Count > 0)
                {
                    foreach (PKDeliverErrorBox eb in item.DeliverErrorBoxList)
                    {
                        if (eb.RecordType != DeliverRecordType.其他)
                        {
                            TagDetailInfo tagDetailInfo = item.TagDetailList.FirstOrDefault(o => o.MATNR == eb.MATNR);
                            if (tagDetailInfo != null)
                            {
                                var uploadItem = uploadlist.FirstOrDefault(o => o.BARCD == tagDetailInfo.BARCD && o.PICK_TASK == eb.PICK_TASK);
                                if (uploadItem != null)
                                {
                                    uploadItem.QTY = uploadItem.QTY + eb.REAL;
                                    uploadItem.Err_Qty = uploadItem.Err_Qty + ((eb.RecordType == DeliverRecordType.多拣 || eb.RecordType == DeliverRecordType.拣错) ? eb.DIFF : 0);
                                    uploadItem.DJ_QTY = uploadItem.DJ_QTY + eb.SHORTQTY;
                                }
                                else
                                {
                                    uploadItem = new SAP019Entity();
                                    uploadItem.BARCD = tagDetailInfo.BARCD;
                                    uploadItem.PICK_TASK = eb.PICK_TASK;
                                    uploadItem.QTY = eb.REAL;
                                    uploadItem.Err_Qty = ((eb.RecordType == DeliverRecordType.多拣 || eb.RecordType == DeliverRecordType.拣错) ? eb.DIFF : 0);
                                    uploadItem.DJ_QTY = eb.SHORTQTY;
                                    uploadlist.Add(uploadItem);
                                }
                            }
                            else
                            {
                                //从数据库获取条码信息
                                HLATagInfo tag = LocalDataService.GetTagInfoByMatnr(eb.MATNR);
                                if (tag != null)
                                {
                                    var uploadItem = uploadlist.FirstOrDefault(o => o.BARCD == tag.BARCD && o.PICK_TASK == eb.PICK_TASK);
                                    if (uploadItem != null)
                                    {
                                        uploadItem.QTY = uploadItem.QTY + eb.REAL;
                                        uploadItem.Err_Qty = uploadItem.Err_Qty + ((eb.RecordType == DeliverRecordType.多拣 || eb.RecordType == DeliverRecordType.拣错) ? eb.DIFF : 0);
                                        uploadItem.DJ_QTY = uploadItem.DJ_QTY + eb.SHORTQTY;
                                    }
                                    else
                                    {
                                        uploadItem = new SAP019Entity();
                                        uploadItem.BARCD = tag.BARCD;
                                        uploadItem.PICK_TASK = eb.PICK_TASK;
                                        uploadItem.QTY = eb.REAL;
                                        uploadItem.Err_Qty = ((eb.RecordType == DeliverRecordType.多拣 || eb.RecordType == DeliverRecordType.拣错) ? eb.DIFF : 0);
                                        uploadItem.DJ_QTY = eb.SHORTQTY;
                                        uploadlist.Add(uploadItem);
                                    }
                                }
                            }
                        }
                    }
                }

                if (uploadlist != null && uploadlist.Count > 0)
                {
                    foreach (SAP019Entity sapEntity in uploadlist)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID066STR").CreateStructure();
                        import.SetValue("BARCD", sapEntity.BARCD);
                        import.SetValue("PICK_LIST", sapEntity.PICK_TASK);
                        import.SetValue("QTY", sapEntity.QTY);
                        import.SetValue("ERR_QTY", sapEntity.Err_Qty);
                        import.SetValue("DJ_QTY", sapEntity.DJ_QTY);
                        IrfTable.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）

                RfcSessionManager.EndContext(dest);

                result.STATUS = status;
                result.MSG = myfun.GetString("MSG_OUT");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                result.MSG = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 上传发运箱数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns>0：成功；-1：失败；-2：异常</returns>
        public static SapResult UploadPKBoxInfo(UploadPKBoxInfo item)
        {
            SapResult result = new SapResult();
            if(SysConfig.IsTest)
            {
                result.STATUS = "S";
                result.MSG = "成功";
                return result;
            }
            try
            {
                if(!item.IsWholeDeviver)
                {
                    result.STATUS = "E";
                    result.MSG = "无法处理类型为5的下架单";
                    return result;

                }

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_019");
                myfun.SetValue("LGNUM", item.LGNUM);
                myfun.SetValue("SHIP_DATE", item.SHIP_DATE.ToString("yyyyMMdd"));
                myfun.SetValue("PARTNER", item.PARTNER);
                myfun.SetValue("HU", item.HU);
                myfun.SetValue("STATUS_IN", item.InventoryResult ? "S" : "E");
                myfun.SetValue("MSG_IN", "");
                myfun.SetValue("SUBUSER", item.SubUser);//提交用户 当前登录用户
                myfun.SetValue("LOUCENG", item.LOUCENG);
                myfun.SetValue("EQUIP_HLA", item.EQUIP_HLA);

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");

                var uploadlist = new List<SAP019Entity>();
                if (item.DeliverErrorBoxList != null && item.DeliverErrorBoxList.Count > 0)
                {
                    foreach (PKDeliverErrorBox eb in item.DeliverErrorBoxList)
                    {
                        if (eb.RecordType != DeliverRecordType.其他)
                        {
                            //string errorType = "";
                            //switch (eb.RecordType)
                            //{
                            //    case DeliverRecordType.多拣:
                            //        errorType = "多拣";
                            //        break;
                            //    case DeliverRecordType.少拣:
                            //        errorType = "少拣";
                            //        break;
                            //    case DeliverRecordType.拣错:
                            //        errorType = "拣错";
                            //        break;
                            //    default:
                            //        break;
                            //}
                            TagDetailInfo tagDetailInfo = item.TagDetailList.FirstOrDefault(o => o.MATNR == eb.MATNR);
                            if (tagDetailInfo != null)
                            {
                                var uploadItem = uploadlist.FirstOrDefault(o => o.BARCD == tagDetailInfo.BARCD && o.PICK_TASK == eb.PICK_TASK);
                                if (uploadItem != null)
                                {
                                    uploadItem.QTY = uploadItem.QTY + eb.REAL;
                                    uploadItem.Err_Qty = uploadItem.Err_Qty + ((eb.RecordType == DeliverRecordType.多拣 || eb.RecordType == DeliverRecordType.拣错) ? eb.DIFF : 0);
                                    uploadItem.DJ_QTY = uploadItem.DJ_QTY + eb.SHORTQTY;
                                }
                                else
                                {
                                    uploadItem = new SAP019Entity();
                                    uploadItem.BARCD = tagDetailInfo.BARCD;
                                    uploadItem.PICK_TASK = eb.PICK_TASK;
                                    uploadItem.QTY = eb.REAL;
                                    uploadItem.Err_Qty = ((eb.RecordType == DeliverRecordType.多拣 || eb.RecordType == DeliverRecordType.拣错) ? eb.DIFF : 0);
                                    uploadItem.DJ_QTY = eb.SHORTQTY;
                                    uploadlist.Add(uploadItem);
                                }
                            }
                            else
                            {
                                //从数据库获取条码信息
                                HLATagInfo tag = LocalDataService.GetTagInfoByMatnr(eb.MATNR);
                                if(tag!=null)
                                {
                                    var uploadItem = uploadlist.FirstOrDefault(o => o.BARCD == tag.BARCD && o.PICK_TASK == eb.PICK_TASK);
                                    if (uploadItem != null)
                                    {
                                        uploadItem.QTY = uploadItem.QTY + eb.REAL;
                                        uploadItem.Err_Qty = uploadItem.Err_Qty + ((eb.RecordType == DeliverRecordType.多拣 || eb.RecordType == DeliverRecordType.拣错) ? eb.DIFF : 0);
                                        uploadItem.DJ_QTY = uploadItem.DJ_QTY + eb.SHORTQTY;
                                    }
                                    else
                                    {
                                        uploadItem = new SAP019Entity();
                                        uploadItem.BARCD = tag.BARCD;
                                        uploadItem.PICK_TASK = eb.PICK_TASK;
                                        uploadItem.QTY = eb.REAL;
                                        uploadItem.Err_Qty = ((eb.RecordType == DeliverRecordType.多拣 || eb.RecordType == DeliverRecordType.拣错) ? eb.DIFF : 0);
                                        uploadItem.DJ_QTY = eb.SHORTQTY;
                                        uploadlist.Add(uploadItem);
                                    }
                                }
                            }
                        }
                    }
                }

                if(uploadlist != null && uploadlist.Count > 0)
                {
                    foreach(SAP019Entity sapEntity in uploadlist)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID019STR").CreateStructure();
                        import.SetValue("BARCD", sapEntity.BARCD);
                        import.SetValue("PICK_TASK", sapEntity.PICK_TASK);
                        import.SetValue("QTY", sapEntity.QTY);
                        import.SetValue("Err_Qty", sapEntity.Err_Qty);
                        import.SetValue("DJ_QTY", sapEntity.DJ_QTY);
                        //import.SetValue("ERR_TYPE", errorType);
                        IrfTable.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）

                RfcSessionManager.EndContext(dest);

                result.STATUS = status;
                result.MSG = myfun.GetString("MSG_OUT");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                result.MSG = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 上传发运箱Epc明细
        /// </summary>
        /// <param name="key"></param>
        /// <param name="epcList"></param>
        /// <returns></returns>
        public static bool UploadDeliverEpcDetails(string key, List<string> epcList)
        {
            try
            {
                string[] parts = key.Split(',');
                string lgnum = parts[0];
                string dateship = parts[1];
                string partner = parts[2];
                string hu = parts[3];

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_020");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("SHIP_DATE", dateship);//发运日期
                myfun.SetValue("PARTNER", partner); //门店
                myfun.SetValue("HU", hu);//处理单位标识

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");
                if (epcList != null && epcList.Count > 0)
                {
                    foreach (string epc in epcList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID020STR").CreateStructure();
                        import.SetValue("EPC_SER", epc);
                        IrfTable.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）
                RfcSessionManager.EndContext(dest);

                if (status == "S")
                {
                    //设置发运箱epc明细为已处理
                    LocalDataService.SetDeliverEpcDetailsHandled(lgnum, dateship, partner, hu);
                    //LocalDataService.SetEpcDetailsHandled(lgnum, dateship, partner, hu);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }
        /// <summary>
        /// 上传挂装机确认下架的EPC
        /// </summary>
        /// <param name="key"></param>
        /// <param name="epcList"></param>
        /// <returns></returns>
        public static bool UploadInventoryOutEpcDetails(string key, List<string> epcList)
        {
            try
            {
                string[] parts = key.Split(',');
                string lgnum = parts[0];
                string dateship = parts[1];
                string partner = parts[2];
                string hu = parts[3];

                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_020");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("SHIP_DATE", dateship);//发运日期
                myfun.SetValue("PARTNER", partner); //门店
                myfun.SetValue("HU", hu);//处理单位标识

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_DETAIL");
                if (epcList != null && epcList.Count > 0)
                {
                    foreach (string epc in epcList)
                    {
                        import = rfcrep.GetStructureMetadata("ZSRFID020STR").CreateStructure();
                        //MessageBox.Show(epc);
                        import.SetValue("EPC_SER", epc);
                        IrfTable.Insert(import);
                    }
                }

                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS_OUT"); //执行状态正确‘S’错误‘E’）
                RfcSessionManager.EndContext(dest);

                if (status == "S")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static List<LoucengLgtypMapInfo> Get_LOUCENG_LGTYP_Map()
        {
            try
            {
                if (SysConfig.IsTest)
                {
                    List<LoucengLgtypMapInfo> list = new List<LoucengLgtypMapInfo>();
                    list.Add(
                        new LoucengLgtypMapInfo()
                        {
                            LGTYP = "0811",
                            LOUCENG = "081"
                        });
                    return list;
                }
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_037");
                myfun.SetValue("IV_TAB", "ZT477");//表名：ZT477，存储类型

                myfun.Invoke(dest);
                RfcSessionManager.EndContext(dest);

                string status = myfun.GetString("EV_STATUS"); //执行状态正确‘S’错误‘E’）

                if (status == "S")
                {
                    IRfcTable IrfTable = myfun.GetTable("EV_OUT");
                    List<LoucengLgtypMapInfo> list = new List<LoucengLgtypMapInfo>();
                    //插入表数据
                    for (int i = 0; i < IrfTable.Count; i++)
                    {
                        IrfTable.CurrentIndex = i;
                        string str = IrfTable.GetString("OUT");
                        if (!string.IsNullOrEmpty(str))
                        {
                            string[] parts = str.Split(new string[] { "&&" }, StringSplitOptions.None);
                            if (parts.Length >= 2)
                            {
                                string louceng = parts[0];
                                string lgtyp = parts[1];

                                if (!list.Exists(o => o.LOUCENG == louceng && o.LGTYP == lgtyp))
                                {
                                    list.Add(new LoucengLgtypMapInfo() { LOUCENG = louceng, LGTYP = lgtyp });
                                }
                            }
                        }

                    }

                    return list;

                }


            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }
        #endregion

        #region 大通道机整理库装箱软件接口
        /// <summary>
        /// 增量下载自定义箱规和退货类型
        /// </summary>
        /// <param name="lgnum">仓库</param>
        /// <param name="first">"X"全量更新 ""为空则只更新昨天的</param>
        public static List<ReturnTypeInfo> GetReturnTypeInfo(string lgnum,string first = "X")
        {
            List<ReturnTypeInfo> returntypelist = new List<ReturnTypeInfo>();

            if (SysConfig.IsTest)
            {
                returntypelist.Add(new ReturnTypeInfo() {  THTYPE="TH01", ZCOLSN= "M1L", ZPRDNR= "HWJAD3N191A" });
                returntypelist.Add(new ReturnTypeInfo() {  THTYPE="TH02", ZCOLSN="CF05", ZPRDNR="HNJDJUI0002"});
                return returntypelist;
            }
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_048");
                myfun.SetValue("LGNUM", lgnum);//仓库编号
                myfun.SetValue("FIRST", first);//是否第一次更新（如果没有导入参数X）
                myfun.Invoke(dest);
                /*IRfcTable XGTable = myfun.GetTable("IT_XG");
                //LogHelper.WriteLine(XGTable.ToString());
                if (XGTable.Count > 0)
                {
                    boxqtylist = new List<BoxQtyInfo>();
                    for (int i = 0; i < XGTable.Count; i++)
                    {
                        XGTable.CurrentIndex = i;
                        BoxQtyInfo bqinfo = new BoxQtyInfo();
                        bqinfo.MATNR = XGTable[i].GetString("MATNR");
                        bqinfo.PXQTY = XGTable[i].GetInt("PXQTY");
                        boxqtylist.Add(bqinfo);
                    }
                }*/
                IRfcTable THTable = myfun.GetTable("IT_TH");
                //LogHelper.WriteLine(THTable.ToString());
                if(THTable.Count >0)
                {
                    for(int i=0;i<THTable.Count;i++)
                    {
                        THTable.CurrentIndex = i;
                        ReturnTypeInfo rtinfo = new ReturnTypeInfo();
                        rtinfo.THTYPE = THTable[i].GetString("THTYPE");
                        rtinfo.ZPRDNR = THTable[i].GetString("ZPRDNR");
                        rtinfo.ZCOLSN = THTable[i].GetString("ZCOLSN");
                        returntypelist.Add(rtinfo);
                    }
                    
                }
                RfcSessionManager.EndContext(dest);
                return returntypelist;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
            
            
        }

        /// <summary>
        /// 上传整理库装箱信息
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="hu">箱码</param>
        /// <param name="equip_hla">设备编码</param>
        /// <param name="louceng">楼层</param>
        /// <param name="subuser">提交用户</param>
        /// <param name="tagdetaillist">箱明细</param>
        /// <returns></returns>
        public static SapResult UploadPackingBox(
            string lgnum,string hu,string equip_hla,string status,string msg,string mx,
            string louceng,string subuser,List<PBBoxDetailInfo> detaillist)
        {
            SapResult result = new SapResult();
            if(SysConfig.IsTest)
            {
                result.STATUS = "S";
                result.MSG = "包装成功";
                return result;
            }
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_047");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("EQUIP_HLA", equip_hla);
                myfun.SetValue("HU", hu);
                myfun.SetValue("LOUCENG", louceng);
                myfun.SetValue("SUBUSER", subuser);//提交用户 当前登录用户
                myfun.SetValue("STATUS_IN", status);//提交用户 当前登录用户
                myfun.SetValue("MSG_IN", msg);//提交用户 当前登录用户
                myfun.SetValue("MX", mx);//提交用户 当前登录用户

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_LIST");
                if (detaillist != null && detaillist.Count > 0)
                {
                    List<string> barcodelist = detaillist.Select(o => o.BARCD).Distinct().ToList();//获取条码列表
                    if (barcodelist != null && barcodelist.Count > 0)
                    {
                        foreach (string barcode in barcodelist)
                        {
                            int qty = detaillist.Count(o => o.BARCD == barcode && o.IsAdd != 1);
                            string matnr = detaillist.FirstOrDefault(o => o.BARCD == barcode).MATNR;
                            import = rfcrep.GetStructureMetadata("ZSEWRFID047").CreateStructure();

                            import.SetValue("BARCD", barcode);
                            import.SetValue("MATNR", matnr);
                            import.SetValue("QTY", qty);
                            IrfTable.Insert(import);
                        }
                    }
                }


                myfun.Invoke(dest);

                result.STATUS = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                result.MSG = myfun.GetString("MSG");
                
                RfcSessionManager.EndContext(dest);
                if (!result.SUCCESS)
                    LogHelper.WriteLine(string.Format("调用接口Z_EW_RFID_047错误：{0}", result.MSG));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                result.STATUS = "E";
                result.MSG = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 生成交接单
        /// </summary>
        /// <param name="lgnum">仓库编号</param>
        /// <param name="scanperson">提交用户</param>
        /// <param name="lh">楼号</param>
        /// <param name="hulist">箱码列表</param>
        /// <returns></returns>
        public static SapResult GenerateDocInfo(string lgnum,string 
            scanperson,string lh,List<string> hulist)
        {
            SapResult result = new SapResult();
            if(SysConfig.IsTest)
            {
                result.MSG = "生成成功";
                result.STATUS = "S";
                return result;
            }
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_049");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("LH", lh);
                myfun.SetValue("SCANPERSON", scanperson);//提交用户 当前登录用户

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_IN");
                if (hulist != null && hulist.Count > 0)
                {
                    foreach (string hu in hulist)
                    {
                        import = rfcrep.GetStructureMetadata("ZSEWRFID049").CreateStructure();
                        import.SetValue("BOX_NO", hu);
                        IrfTable.Insert(import);
                    }
                }


                myfun.Invoke(dest);

                //result.STATUS = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                result.STATUS = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                result.MSG = myfun.GetString("MSG");

                RfcSessionManager.EndContext(dest);
                if (!result.SUCCESS)
                    LogHelper.WriteLine(string.Format("调用接口Z_EW_RFID_049错误：{0}", result.MSG));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                result.STATUS = "E";
                result.MSG = ex.Message;
            }
            return result;
        }
        #endregion

        #region 大通道机移库装箱系统

        public static SapResult UploadZLKBoxInfo(string lgnum, ZLKBoxInfo box)
        {
            SapResult result = new SapResult();
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_058");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("VLTYP", box.Source);
                myfun.SetValue("LGTYP", box.Target);
                myfun.SetValue("IS_FULL", box.IsFull == 1 ? "X" : "");
                myfun.SetValue("EQUIP_HLA", box.EquipHla);
                myfun.SetValue("LOUCENG", box.LouCeng);
                myfun.SetValue("SUBUSER", box.SubUser);
                myfun.SetValue("HU", box.Hu);
                myfun.SetValue("STATUS_IN", box.Status);
                myfun.SetValue("MSG_IN", box.Remark);
                myfun.SetValue("PACKMAT", box.PackMat.TrimStart('0'));
                //myfun.SetValue("IS_RF", "");  //是否通过RF调用
                //LogService.Log(SysConfig.DeviceNO, string.Format("箱号：{0}，上传包材：{1}", box.Hu, box.PackMat));

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_LIST");
                if (box.Details != null && box.Details.Count > 0)
                {
                    //List<string> barcodelist = box.Details.Select(o => o.Barcd).Distinct().ToList();//获取条码列表
                    List<string> barcodelist = new List<string>();
                    foreach (ZLKBoxDetailInfo ydi in box.Details)
                    {
                        if (ydi.IsAdd == 0 && !barcodelist.Contains(ydi.Barcd))
                        {
                            barcodelist.Add(ydi.Barcd);
                        }
                    }

                    if (barcodelist != null && barcodelist.Count > 0)
                    {
                        foreach (string barcode in barcodelist)
                        {
                            int qty = box.Details.Count(o => o.Barcd == barcode);//统计对应主条码的数量
                            import = rfcrep.GetStructureMetadata("ZSEWRFID058").CreateStructure();
                            import.SetValue("BARCD", barcode);
                            import.SetValue("MATNR", box.Details.First(x => x.Barcd == barcode).Matnr);
                            import.SetValue("QTY", qty);
                            IrfTable.Insert(import);
                        }
                    }
                }


                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                string msg_sap = myfun.GetString("MSG");
                RfcSessionManager.EndContext(dest);
                result.STATUS = status;
                result.MSG = msg_sap;
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                result.STATUS = "E";
                result.MSG = ex.Message;
            }

            return result;
        }

        public static SapResult UploadYKBoxInfo(string lgnum, YKBoxInfo box)
        {
            SapResult result = new SapResult();
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_058");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("VLTYP", box.Source);
                myfun.SetValue("LGTYP", box.Target);
                myfun.SetValue("IS_FULL", box.IsFull == 1 ? "X" : "");
                myfun.SetValue("EQUIP_HLA", box.EquipHla);
                myfun.SetValue("LOUCENG", box.LouCeng);
                myfun.SetValue("SUBUSER", box.SubUser);
                myfun.SetValue("HU", box.Hu);
                myfun.SetValue("STATUS_IN", box.Status);
                myfun.SetValue("MSG_IN", box.Remark);
                myfun.SetValue("PACKMAT", box.PackMat.TrimStart('0'));
                //myfun.SetValue("IS_RF", "");  //是否通过RF调用
                //LogService.Log(SysConfig.DeviceNO, string.Format("箱号：{0}，上传包材：{1}", box.Hu, box.PackMat));

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_LIST");
                if (box.Details != null && box.Details.Count > 0)
                {
                    //List<string> barcodelist = box.Details.Select(o => o.Barcd).Distinct().ToList();//获取条码列表
                    List<string> barcodelist = new List<string>();
                    foreach(YKBoxDetailInfo ydi in box.Details)
                    {
                        if(ydi.IsAdd == 0 && !barcodelist.Contains(ydi.Barcd))
                        {
                            barcodelist.Add(ydi.Barcd);
                        }
                    }

                    if (barcodelist != null && barcodelist.Count > 0)
                    {
                        foreach (string barcode in barcodelist)
                        {
                            int qty = box.Details.Count(o => o.Barcd == barcode);//统计对应主条码的数量
                            import = rfcrep.GetStructureMetadata("ZSEWRFID058").CreateStructure();
                            import.SetValue("BARCD", barcode);
                            import.SetValue("MATNR", box.Details.First(x => x.Barcd == barcode).Matnr);
                            import.SetValue("QTY", qty);
                            IrfTable.Insert(import);
                        }
                    }
                }


                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                string msg_sap = myfun.GetString("MSG");
                RfcSessionManager.EndContext(dest);
                result.STATUS = status;
                result.MSG = msg_sap;
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                result.STATUS = "E";
                result.MSG = ex.Message;
            }

            return result;
        }

        public static SapResult UploadYKBoxInfo()
        {
            if(MessageBox.Show(JsonConvert.SerializeObject(rfcParams),"", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return new SapResult()
                {
                    MSG = "取消操作",
                    STATUS = "E"
                };
            }
            SapResult result = new SapResult();
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_058");
                myfun.SetValue("LGNUM", "HL01");
                myfun.SetValue("VLTYP", "THAD");
                myfun.SetValue("LGTYP", "BDMX");
                myfun.SetValue("IS_FULL",  "");
                myfun.SetValue("EQUIP_HLA", "HLCS1007A");
                myfun.SetValue("LOUCENG", "031");
                myfun.SetValue("SUBUSER", "069675");
                myfun.SetValue("HU", "55556666");
                myfun.SetValue("STATUS_IN", "S");
                myfun.SetValue("MSG_IN", "正常");
                myfun.SetValue("PACKMAT", "1200000075"); 
                //myfun.SetValue("PACKMAT", "1200000036"); 
                //myfun.SetValue("IS_RF", "");  //是否通过RF调用

                IRfcStructure import = null;
                IRfcTable IrfTable = myfun.GetTable("IT_LIST");
                import = rfcrep.GetStructureMetadata("ZSEWRFID058").CreateStructure();
                import.SetValue("BARCD", "HTXAD3A150YE0002A11");
                import.SetValue("MATNR", "HTXAD3A150YE0002");
                import.SetValue("QTY", 17);
                IrfTable.Insert(import);


                myfun.Invoke(dest);

                string status = myfun.GetString("STATUS"); //执行状态正确‘S’错误‘E’）
                string msg_sap = myfun.GetString("MSG");
                RfcSessionManager.EndContext(dest);
                result.STATUS = status;
                result.MSG = msg_sap;
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
                result.STATUS = "E";
                result.MSG = ex.Message;
            }

            return result;
        }
        #endregion

        public static List<KeyValuePair<string,int>> GetCancelHuList(string lgnum,List<string> cancelAuth)
        {
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_061");
                myfun.SetValue("LGNUM", lgnum);

                myfun.Invoke(dest);
                string result = myfun.GetString("STATUS");
                if (result == "S")
                {
                    IRfcTable IrfTable = myfun.GetTable("IT_LIST");
                    List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();

                    //插入表数据
                    for (int i = 0; i < IrfTable.Count; i++)
                    {
                        IrfTable.CurrentIndex = i;

                        int qty = 0;
                        int.TryParse(IrfTable.GetString("BOX_QTY"), out qty);
                        KeyValuePair <string, int> box = new KeyValuePair<string,int>(
                            IrfTable.GetString("KXJFCODE"),
                            qty);
                        string THZLD = getZiDuan(IrfTable, "THZLD");
                        if (cancelAuth.Exists(j => j == THZLD))
                        {
                            list.Add(box);
                        }
                    }

                    RfcSessionManager.EndContext(dest);

                    return list;
                }
                else
                {
                    string errormsg = myfun.GetString("MSG");
                    LogHelper.WriteLine("Z_EW_RFID_061_错误消息:" + errormsg);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return null;
        }

        public static CCancelDoc GetCancelHuDocData(string lgnum, string danhao)
        {
            CCancelDoc re = new CCancelDoc();
            re.doc = danhao;
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_062");
                myfun.SetValue("LGNUM", lgnum);
                myfun.SetValue("IV_KXJFCODE", danhao);

                myfun.Invoke(dest);
                string result = myfun.GetString("STATUS");
                string errormsg = myfun.GetString("MSG");

                if (result == "S")
                {
                    IRfcTable IrfTable = myfun.GetTable("IN_DATA");

                    //插入表数据
                    for (int i = 0; i < IrfTable.Count; i++)
                    {
                        IrfTable.CurrentIndex = i;

                        string hu = IrfTable.GetString("BOX_NO");
                        if (string.IsNullOrEmpty(hu))
                            continue;

                        string barcd = getZiDuan(IrfTable, "BARCD");
                        string barcdAdd = getZiDuan(IrfTable, "BARCD_ADD");
                        int qty = IrfTable.GetInt("MENGE");
                        bool isHz = IrfTable.GetString("IS_HZ") == "X";
                        bool isDd = IrfTable.GetString("IS_DD") == "X";
                        bool isCp = IrfTable.GetString("IS_KSCP") == "X";
                        bool isRFID = IrfTable.GetString("IS_RFID") == "X";

                        CCancelDocData ch = null;

                        if (!re.docData.Exists(k=>k.hu == hu))
                        {
                            ch = new CCancelDocData();
                            ch.hu = hu;
                            ch.mIsCp = isCp;
                            ch.mIsDd = isDd;
                            ch.mIsHz = isHz;
                            ch.mIsRFID = isRFID;
                            ch.addBarQty(barcd, qty);
                        }
                        else
                        {
                            ch = re.docData.FirstOrDefault(k => k.hu == hu);
                            ch.addBarQty(barcd, qty);
                        }

                        re.docData.Add(ch);
                    }

                    RfcSessionManager.EndContext(dest);
                }
                else
                {
                    LogHelper.WriteLine("Z_EW_RFID_062_错误消息:" + errormsg);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }

            return re;
        }

        public static void UploadCancelData(CCancelUpload udata,ref string result,ref string sapMsg)
        {
            try
            {
#if DEBUG
                result = "E";
                sapMsg = "lalala";
                return;
#endif
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_063");

                myfun.SetValue("LGNUM", udata.lgnum);
                myfun.SetValue("BOX_NO", udata.boxno);
                myfun.SetValue("SUBUSER", udata.lgnum);
                if (udata.isHZ)
                {
                    myfun.SetValue("STATUS_IN", "E");
                }
                else
                {
                    myfun.SetValue("STATUS_IN", udata.inventoryRe ? "S" : "E");
                }
                myfun.SetValue("EQUIP_HLA", udata.equipID);
                myfun.SetValue("LOUCENG", udata.loucheng);
                myfun.SetValue("KXJFCODE", udata.docno);
                myfun.SetValue("DSWAVE", udata.dianshuBoCi);


                IRfcStructure import = null;
                IRfcTable IrfTable = null;
                
                IrfTable = myfun.GetTable("IN_DATA");

                List<string> barcd = udata.tagDetailList.Select(i => i.BARCD).Distinct().ToList();
                foreach (var item in barcd)
                {
                    import = rfcrep.GetStructureMetadata("ZSEWRFID063").CreateStructure();
                    import.SetValue("BARCD", item);
                    import.SetValue("DSMENGE", udata.tagDetailList.Count(i => i.BARCD == item && !i.IsAddEpc));

                    int add = udata.tagDetailList.Count(i => i.BARCD == item && i.IsAddEpc);
                    if (add != 0)
                    {
                        import.SetValue("BARCD_ADD", udata.tagDetailList.First(i => i.BARCD == item && i.IsAddEpc).BARCD_ADD);
                        import.SetValue("FSMENGE", add);
                    }

                    IrfTable.Insert(import);
                }

                IrfTable = myfun.GetTable("IN_EPC");
                foreach(var item in udata.epcList)
                {
                    import = rfcrep.GetStructureMetadata("ZSEWRFID063A").CreateStructure();
                    import.SetValue("EPC_SER", item);
                    IrfTable.Insert(import);
                }

                myfun.Invoke(dest);
                result = myfun.GetString("STATUS");
                sapMsg = myfun.GetString("MSG");
                RfcSessionManager.EndContext(dest);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }
        }

        public static CDeliverStoreQuery getDeliverStore(string epc)
        {
            CDeliverStoreQuery re = new CDeliverStoreQuery();
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_071");
                myfun.SetValue("LGNUM", SysConfig.LGNUM);
                myfun.SetValue("EPC_SER", epc);
                myfun.Invoke(dest);

                re.store = string.IsNullOrEmpty(myfun.GetString("PARTNER")) ? "" : myfun.GetString("PARTNER");
                re.mtr = string.IsNullOrEmpty(myfun.GetString("MATNR")) ? "" : myfun.GetString("MATNR");
                re.status = string.IsNullOrEmpty(myfun.GetString("STATUS")) ? "" : myfun.GetString("STATUS");
                re.msg = string.IsNullOrEmpty(myfun.GetString("MSG")) ? "" : myfun.GetString("MSG");
                re.bar = string.IsNullOrEmpty(myfun.GetString("BARCD")) ? "" : myfun.GetString("BARCD");
                re.shipDate = string.IsNullOrEmpty(myfun.GetString("SHIP_DATE")) ? "" : myfun.GetString("SHIP_DATE");
                re.hu = string.IsNullOrEmpty(myfun.GetString("HU")) ? "" : myfun.GetString("HU");

                RfcSessionManager.EndContext(dest);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return re;
        }
        public static CStoreQuery getStoreFromSAP(string epc)
        {
            CStoreQuery re = new CStoreQuery();

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_067");
                myfun.SetValue("IN_EPC", epc);
                myfun.Invoke(dest);

                re.barcd = string.IsNullOrEmpty(myfun.GetString("OUT_DP")) ? "" : myfun.GetString("OUT_DP");
                re.storeid = string.IsNullOrEmpty(myfun.GetString("OUT_BP")) ? "" : myfun.GetString("OUT_BP");
                re.status = string.IsNullOrEmpty(myfun.GetString("O_FLAG")) ? "" : myfun.GetString("O_FLAG");
                re.msg = string.IsNullOrEmpty(myfun.GetString("O_MESSAGE")) ? "" : myfun.GetString("O_MESSAGE");
                re.hu = string.IsNullOrEmpty(myfun.GetString("BOX_NO")) ? "" : myfun.GetString("BOX_NO");
                re.pxqty_fh = getZiDuan2Int(myfun, "O_PXQTY_FH");
                re.flag = getZiDuan2(myfun, "OUT_FLAG");
                re.equip_hla = getZiDuan2(myfun, "OUT_EQUIP_HLA");
                re.loucheng = getZiDuan2(myfun, "OUT_LOUCENG");
                re.date = getZiDuan2(myfun, "OUT_SUBDATE");
                re.time = getZiDuan2(myfun, "OUT_SUBTIME");

                RfcSessionManager.EndContext(dest);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return re;
        }
        public static string getZiDuan2(IRfcFunction f, string ziduan)
        {
            try
            {
                return f.GetString(ziduan);
            }
            catch (Exception)
            { }

            return "";
        }
        public static int getZiDuan2Int(IRfcFunction f, string ziduan)
        {
            try
            {
                return f.GetInt(ziduan);
            }
            catch (Exception)
            { }

            return 0;
        }
        public static CJiaoJieDan getJiaoJieDan(string doc, ref string sapRe, ref string sapMsg)
        {
            CJiaoJieDan re = new CJiaoJieDan();
            re.doc = doc;

#if DEBUG
            /*
            {
                sapRe = "S";
                List<CJiaoJieDanData> jjdate = new List<CJiaoJieDanData>();
                jjdate.Add(new CJiaoJieDanData("HZLAD1N013A13001A11", "", 24));
                jjdate.Add(new CJiaoJieDanData("HKXAD1N048A48010A11", "", 23));
                jjdate.Add(new CJiaoJieDanData("HZLAD1N010A10001A11", "", 12));
                re.huData["12345"] = jjdate;

                jjdate = new List<CJiaoJieDanData>();
                jjdate.Add(new CJiaoJieDanData("HTXAD3A064A64005A11", "100000005204", 35));
                jjdate.Add(new CJiaoJieDanData("HNZRD4A002A07003A11", "", 14));
                re.huData["12346"] = jjdate;

                jjdate = new List<CJiaoJieDanData>();
                jjdate.Add(new CJiaoJieDanData("HTXAD3A064A64007A11", "100000005206", 37));
                re.huData["12347"] = jjdate;

                return re;
            }
            */
#endif
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_078");
                myfun.SetValue("LGNUM", SysConfig.LGNUM);
                myfun.SetValue("WBTH_NO", doc);
                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("IT_LIST");
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;

                    string box = IrfTable.GetString("BOX_NO").TrimStart('0');
                    string barcd = IrfTable.GetString("BARCD");
                    string barcd_add = IrfTable.GetString("BARCD_ADD");
                    int quan = IrfTable.GetInt("MENGE");

                    if (!string.IsNullOrEmpty(box))
                    {
                        if(re.huData.ContainsKey(box))
                        {
                            List<CJiaoJieDanData> jjddList = re.huData[box];
                            if(jjddList.Exists(idata=>idata.barcd == barcd))
                            {
                                foreach(var v in jjddList)
                                {
                                    if(v.barcd == barcd)
                                    {
                                        v.quan += quan;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                CJiaoJieDanData jjdd = new CJiaoJieDanData();
                                jjdd.barcd = barcd;
                                jjdd.barcd_add = barcd_add;
                                jjdd.quan = quan;
                                jjddList.Add(jjdd);
                            }
                        }
                        else
                        {
                            List<CJiaoJieDanData> jjddList = new List<CJiaoJieDanData>();
                            CJiaoJieDanData jjdd = new CJiaoJieDanData();
                            jjdd.barcd = barcd;
                            jjdd.barcd_add = barcd_add;
                            jjdd.quan = quan;
                            jjddList.Add(jjdd);
                            re.huData[box] = jjddList;
                        }
                    }
                }

                sapRe = myfun.GetString("STATUS");
                sapMsg = myfun.GetString("MSG");
                RfcSessionManager.EndContext(dest);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
            return re;
        }

        public static void uploadJiaoJieDan(CJJBox box,ref string result,ref string sapMsg)
        {
#if DEBUG
            /*
            result = "S";
            sapMsg = "sap";
            return;
            */
#endif
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_078A");

                myfun.SetValue("LGNUM", SysConfig.LGNUM);
                myfun.SetValue("BOX_NO", box.hu);
                myfun.SetValue("SUBUSER", box.user);
                myfun.SetValue("STATUS_IN", box.inventoryRe);
                myfun.SetValue("EQUIP_HLA", box.devno);
                myfun.SetValue("LOUCENG", box.loucheng);
                myfun.SetValue("WBTH_NO", box.doc);

                IRfcStructure import = null;
                IRfcTable IrfTable = null;

                IrfTable = myfun.GetTable("IN_DATA");
                /*
                List<string> mats = box.tags.Select(i => i.MATNR).Distinct().ToList();
                foreach (var item in mats)
                {
                    import = rfcrep.GetStructureMetadata("ZSEWRFID078A").CreateStructure();
                    import.SetValue("BARCD", box.tags.First(i => i.MATNR == item && !i.IsAddEpc).BARCD);
                    import.SetValue("DSMENGE", box.tags.Count(i => i.MATNR == item && !i.IsAddEpc));
                    int add = box.tags.Count(i => i.MATNR == item && i.IsAddEpc);
                    if(add!=0)
                    {
                        import.SetValue("BARCD_ADD", box.tags.First(i => i.MATNR == item && i.IsAddEpc).BARCD_ADD);
                        import.SetValue("FSMENGE", box.tags.Count(i => i.MATNR == item && i.IsAddEpc));
                    }
                    IrfTable.Insert(import);
                }*/
                List<string> barcdList = box.tags.Select(i => i.BARCD).Distinct().ToList();
                foreach(var item in barcdList)
                {
                    import = rfcrep.GetStructureMetadata("ZSEWRFID078A").CreateStructure();
                    import.SetValue("BARCD", item);
                    import.SetValue("DSMENGE", box.tags.Count(i => i.BARCD == item && !i.IsAddEpc));
                    int add = box.tags.Count(i => i.BARCD == item && i.IsAddEpc);
                    if (add != 0)
                    {
                        import.SetValue("BARCD_ADD", box.tags.First(i => i.BARCD == item && i.IsAddEpc).BARCD_ADD);
                        import.SetValue("FSMENGE", add);
                    }
                    IrfTable.Insert(import);
                }

                IrfTable = myfun.GetTable("IN_EPC");
                foreach (var item in box.epc)
                {
                    import = rfcrep.GetStructureMetadata("ZSEWRFID063A").CreateStructure();
                    import.SetValue("EPC_SER", item);
                    IrfTable.Insert(import);
                }

                myfun.Invoke(dest);
                result = myfun.GetString("STATUS");
                sapMsg = myfun.GetString("MSG");
                RfcSessionManager.EndContext(dest);

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex.StackTrace);
            }
        }

        public static List<string> getIngnoreEpcs(string date = "")
        {
            List<string> re = new List<string>();

            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RF_5000");

                myfun.SetValue("IV_LGNUM", SysConfig.LGNUM);
                if (!string.IsNullOrEmpty(date))
                    myfun.SetValue("IV_DATE", date);

                myfun.Invoke(dest);

                IRfcTable IrfTable = myfun.GetTable("ET_DATA");
                for (int i = 0; i < IrfTable.Count; i++)
                {
                    IrfTable.CurrentIndex = i;
                    string epc = getZiDuan(IrfTable, "EPC");
                    if (!string.IsNullOrEmpty(epc))
                        re.Add(epc);
                }

                string result = myfun.GetString("EV_STATUS");
                string sapMsg = myfun.GetString("EV_MSG");
                RfcSessionManager.EndContext(dest);

            }
            catch (Exception)
            {

            }
            return re;
        }

        public static int RFID_075F(string hu, ref string result, ref string sapMsg)
        {
            int re = 0;
            try
            {
                RfcDestination dest = RfcDestinationManager.GetDestination(rfcParams);
                RfcRepository rfcrep = dest.Repository;
                IRfcFunction myfun = null;
                myfun = rfcrep.CreateFunction("Z_EW_RFID_075F");

                myfun.SetValue("LGNUM", SysConfig.LGNUM);
                myfun.SetValue("BOX_NO", hu);

                myfun.Invoke(dest);
                result = myfun.GetString("STATUS");
                sapMsg = myfun.GetString("MSG");
                re = myfun.GetInt("QTY");
            }
            catch (Exception e)
            {
                result = "E";
                sapMsg = e.ToString();
            }
            return re;
        }

        public static string SUCCESS = "SUCCESS";
        public static string FAILURE = "FAILURE";
        public static string xmlhead = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        //电商采购退
        public static CDianShangDoc getDianShangDocData(string doc,out string errorMsg)
        {
            errorMsg = "";

#if DEBUG
            
            CDianShangDoc re2 = new CDianShangDoc();
            re2.doc = doc;
            re2.dsData.Add(new CBarQty("FNTAJ38508A18001A11", 23));
            re2.dsData.Add(new CBarQty("FNTAJ38508A18002A11", 34));
            return re2;
#endif

            CDianShangDoc re = new CDianShangDoc();
            re.doc = doc;
            try
            {
                CPPInfo pi = new CPPInfo(SysConfig.HttpKey, SysConfig.HttpUrl, SysConfig.HttpSec);
                string postData = "";
                postData = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><purchaseId>{0}</purchaseId></root>", doc.Trim());
                string reData = HttpWebResponseUtility.Submit(postData, "SyncPurchaseInfoSearch", pi);
                
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(reData.Replace(xmlhead,""));

                XmlNode nodeRep = xmldoc.SelectSingleNode("/ewmsResponseRoot/response/bizData");
                if(nodeRep!=null)
                {
                    string flag = nodeRep.SelectSingleNode("flag").InnerText;
                    if(flag == SUCCESS)
                    {
                        XmlNode data = nodeRep.SelectSingleNode("data");
                        XmlNodeList productList = data.SelectNodes("Inner_SyncPurchaseSearchInfoData/products/product");
                        if(productList!=null)
                        {
                            foreach(var v in productList)
                            {

                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return re;
        }
        public static void uploadDianShangBox(CDianShangBox box,ref string sapRe,ref string sapMsg)
        {
            sapRe = "";
            sapMsg = "";
#if DEBUG
            sapRe = FAILURE;
            sapMsg = "sap";
            return;            
#endif
            try
            {
                CPPInfo pi = new CPPInfo(SysConfig.HttpKey, SysConfig.HttpUrl, SysConfig.HttpSec);

                string postData = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><purchaseId>{0}</purchaseId><boxId>{1}</boxId><products></products></root>", box.doc, box.hu);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(postData);

                XmlNode products = doc.SelectSingleNode("/root/products");

                List<string> barList = box.tags.Select(i => i.BARCD).Distinct().ToList();
                foreach (var v in barList)
                {
                    XmlNode product = doc.CreateElement("product");

                    XmlNode barcode = doc.CreateElement("barcode");
                    barcode.InnerText = v;
                    XmlNode qty = doc.CreateElement("qty");
                    qty.InnerText = box.tags.Count(i => i.BARCD == v && !i.IsAddEpc).ToString();

                    product.AppendChild(barcode);
                    product.AppendChild(qty);
                    products.AppendChild(product);
                }
                string reData = HttpWebResponseUtility.Submit(doc.OuterXml, "SyncPurchaseInfoCheck", pi);

                //parse response
                XmlDocument resDoc = new XmlDocument();
                resDoc.LoadXml(reData.Replace(xmlhead, ""));
                if(resDoc!=null)
                {
                    XmlNode nodeRep = resDoc.SelectSingleNode("/ewmsResponseRoot/response/bizData");
                    if(nodeRep!=null)
                    {
                        string flag = nodeRep.SelectSingleNode("flag").InnerText;
                        if (flag == SUCCESS)
                        {
                            sapRe = SUCCESS;
                        }
                        if(flag == FAILURE)
                        {
                            sapRe = FAILURE;
                            XmlNode error = nodeRep.SelectSingleNode("errorDescription");
                            if(error!=null)
                            {
                                sapMsg = error.InnerText;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sapMsg = ex.ToString();
            }
        }

    }
    public class HttpWebResponseUtility
    {
        public static int connectTimeout = 30000;

        public static string StrToUrlEncode(string sdata)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(sdata);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return sb.ToString();
        }


        public static string USerMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("x2");
            }
            return pwd;

        }
        public static string Submit(string postData, string serviceType, CPPInfo ppinfo)
        {
            string requestTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string str = "bizData=" + postData + "&msgId=424&msgType=sync&notifyUrl=&partnerId=" + ppinfo.Inerfae_key + "&partnerKey=" + ppinfo.Secret + "&serviceType=" + serviceType + "&serviceVersion=1.0";
            string qm = USerMd5(str);
            string binzdata = StrToUrlEncode(postData);
            string str2 = "bizData=" + binzdata + "&serviceType=" + serviceType + "&msgId=424&msgType=sync&partnerId=" + ppinfo.Inerfae_key + "&partnerKey=" + ppinfo.Secret + "&serviceVersion=1.0&notifyUrl=&sign=" + qm + "&";


            byte[] bytePostData = Encoding.UTF8.GetBytes(str2);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ppinfo.Interface_url);
            req.Method = "POST";

            req.Timeout = connectTimeout;
            req.ContentType = "application/x-www-form-urlencoded"; ;
            req.ContentLength = bytePostData.Length;
            try
            {
                using (System.IO.Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bytePostData, 0, bytePostData.Length);
                }
                using (WebResponse wr = req.GetResponse())
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8);

                    string responseFromServer = reader.ReadToEnd();
                    return responseFromServer;  
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return "";
        }

    }
}
