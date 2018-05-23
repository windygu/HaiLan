using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonView.Configs;
using HLACommonView.Model;
using HLACommonView.Views.Dialogs;
using HLACommonView.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UARTRfidLink.Exparam;
using UARTRfidLink.Extend;

namespace HLACommonView.Views
{
    public partial class CommonPMInventoryForm : MetroForm
    {
        public RfidUARTLinkExtend rfid = new RfidUARTLinkExtend();
        string mComPort;
        uint mPower;

        public bool isConnected = false;
        public bool isInventory = false;
        public DateTime lastReadTime = DateTime.Now;
        public List<string> epcList = new List<string>();
        public List<TagDetailInfo> tagDetailList = new List<TagDetailInfo>();
        public int errorEpcNumber = 0, mainEpcNumber = 0, addEpcNumber = 0;
        public List<HLATagInfo> hlaTagList = null;
        public List<MaterialInfo> materialList = null;
        public Queue<string> boxNoList = new Queue<string>();
        public CheckResult checkResult = new CheckResult();
        private ErrorWarnForm mErrorForm = new ErrorWarnForm();

        public CommonPMInventoryForm()
        {
            InitializeComponent();
        }

        public virtual void InitDevice(string comPort,string power = "23")
        {
            mComPort = comPort;
            mPower = 0;
            uint.TryParse(power, out mPower);

            rfid.RadioInventory += new EventHandler<RadioInventoryEventArgs>(rfid_RadioInventory);

        }
        public void rfid_RadioInventory(object sender, RadioInventoryEventArgs e)
        {
            string epc = "";
            try
            {
                for (int i = 0; i < e.tagInfo.epc.Length; i++)
                {
                    epc += string.Format("{0:X4}", e.tagInfo.epc[i]);
                }
            }
            catch (Exception) { }

            reportEpc(epc);
        }

        public virtual void reportEpc(string epc)
        {
            if (!isInventory || string.IsNullOrEmpty(epc)) return;
            if (!epcList.Contains(epc))
            {
                lastReadTime = DateTime.Now;

                TagDetailInfo tag = GetTagDetailInfoByEpc(epc);

                string errorMsg = "";
                if (!checkTagOK(tag, out errorMsg))
                {
                    mErrorForm.showErrorInfo(tag.EPC, tag, errorMsg);
                    return;
                }

                if (tag != null)   //合法EPC
                {
                    tagDetailList.Add(tag);
                    if (!tag.IsAddEpc)   //主条码
                        mainEpcNumber++;
                    else
                        addEpcNumber++;
                }
                else
                {
                    errorEpcNumber++;
                }

                epcList.Add(epc);
                UpdateView();
            }

        }
        public virtual bool checkTagOK(TagDetailInfo tg,out string msg)
        {
            msg = "";
            return true;
        }
        public virtual void reportBar(string bar)
        {
            TagDetailInfo tag = GetTagDetailInfoByBar(bar);

            string errorMsg = "";
            if (!checkTagOK(tag, out errorMsg))
            {
                mErrorForm.showErrorInfo(bar, tag, errorMsg);
                return;
            }

            if (tag != null)   //合法EPC
            {
                tagDetailList.Add(tag);
                if (!tag.IsAddEpc)   //主条码
                    mainEpcNumber++;
                else
                    addEpcNumber++;
            }
            else
            {
                errorEpcNumber++;
            }

            UpdateView();
        }
        public virtual void UpdateView()
        {

        }
        public virtual bool ConnectReader()
        {
            bool re = true;
            try
            {
                if (rfid.ConnectRadio(mComPort, 115200) == operateResult.ok)
                {
                    // 这里演示初始化参数
                    // 配置天线功率
                    AntennaPortConfiguration portConfig = new AntennaPortConfiguration();
                    portConfig.powerLevel = mPower * 10; // 23dbm
                    portConfig.numberInventoryCycles = 8192;
                    portConfig.dwellTime = 2000;
                    rfid.SetAntennaPortConfiguration(mComPort, 0, portConfig);

                    rfid.SetCurrentLinkProfile(mComPort, 1);

                    // 配置单化算法
                    SingulationAlgorithmParms singParm = new SingulationAlgorithmParms();
                    singParm.singulationAlgorithmType = SingulationAlgorithm.Dynamicq;
                    singParm.startQValue = 4;
                    singParm.minQValue = 0;
                    singParm.maxQValue = 15;
                    singParm.thresholdMultiplier = 4;
                    singParm.toggleTarget = 1;
                    rfid.SetCurrentSingulationAlgorithm(mComPort, singParm);
                    rfid.SetTagGroupSession(mComPort, Session.S0);

                }
                else
                {
                    re = false;
                }
            }
            catch(Exception)
            {
                re = false;
            }

            return re;
        }

        public virtual CheckResult CheckData()
        {
            CheckResult result = new CheckResult();
            if (errorEpcNumber > 0)
            {
                result.UpdateMessage(Consts.Default.EPC_WEI_ZHU_CE);
                result.InventoryResult = false;
            }
            if (mainEpcNumber != addEpcNumber && addEpcNumber > 0)
            {
                result.UpdateMessage(Consts.Default.TWO_NUMBER_ERROR);
                result.InventoryResult = false;
            }
            if (epcList.Count == 0)
            {
                result.UpdateMessage(Consts.Default.WEI_SAO_DAO_EPC);
                result.InventoryResult = false;
            }

            return result;
        }
        
        private void DisconnectReader()
        {
            this.isConnected = false;

            rfid.StopInventory(mComPort);
            rfid.DisconnectRadio(mComPort);
        }
        public TagDetailInfo GetTagDetailInfoByEpc(string epc)
        {
            if (string.IsNullOrEmpty(epc) || epc.Length < 20)
                return null;
            string rfidEpc = epc.Substring(0, 14) + "000000";
            string rfidAddEpc = rfidEpc.Substring(0, 14);
            if (hlaTagList == null || materialList == null)
                return null;
            List<HLATagInfo> tags = hlaTagList.FindAll(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc);
            if (tags == null || tags.Count == 0)
                return null;
            else
            {
                HLATagInfo tag = tags.First();
                MaterialInfo mater = materialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
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
                    item.BARCD_ADD = tag.BARCD_ADD;
                    
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.ZCOLSN_WFG = mater.ZCOLSN_WFG;
                    item.PXQTY = mater.PXQTY;
                    item.PXQTY_FH = mater.PXQTY_FH;
                    item.PACKMAT = mater.PXMAT;
                    item.PACKMAT_FH = mater.PXMAT_FH;
                    item.PUT_STRA = mater.PUT_STRA;
                    item.BRGEW = mater.BRGEW;
                    item.MAKTX = mater.MAKTX;

                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    item.LIFNRS = new List<string>();
                    foreach (HLATagInfo t in tags)
                    {
                        if (!string.IsNullOrEmpty(t.LIFNR))
                        {
                            if (!item.LIFNRS.Contains(t.LIFNR))
                            {
                                item.LIFNRS.Add(t.LIFNR);
                            }
                        }
                    }
                    return item;
                }
            }
        }

        public TagDetailInfo GetTagDetailInfoByBar(string bar)
        {
            if (string.IsNullOrEmpty(bar))
                return null;
            if (hlaTagList == null || materialList == null)
                return null;
            List<HLATagInfo> tags = hlaTagList.FindAll(i => i.BARCD.ToUpper() == bar.ToUpper() || (i.BARCD_ADD.ToUpper() == bar.ToUpper()));

            if (tags == null || tags.Count == 0)
                return null;
            else
            {
                HLATagInfo tag = tags.First();
                MaterialInfo mater = materialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
                if (mater == null)
                    return null;
                else
                {
                    TagDetailInfo item = new TagDetailInfo();
                    item.EPC = "";
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = tag.RFID_ADD_EPC;
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.BARCD_ADD = tag.BARCD_ADD;

                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.ZCOLSN_WFG = mater.ZCOLSN_WFG;
                    item.PXQTY = mater.PXQTY;
                    item.PXQTY_FH = mater.PXQTY_FH;
                    item.PACKMAT = mater.PXMAT;
                    item.PACKMAT_FH = mater.PXMAT_FH;
                    item.PUT_STRA = mater.PUT_STRA;
                    item.BRGEW = mater.BRGEW;
                    item.MAKTX = mater.MAKTX;

                    if (bar.ToUpper() == item.BARCD.ToUpper())
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    item.LIFNRS = new List<string>();
                    foreach (HLATagInfo t in tags)
                    {
                        if (!string.IsNullOrEmpty(t.LIFNR))
                        {
                            if (!item.LIFNRS.Contains(t.LIFNR))
                            {
                                item.LIFNRS.Add(t.LIFNR);
                            }
                        }
                    }
                    return item;
                }
            }
        }

        public virtual void ShowLoading(string message)
        {
            Invoke(new Action(() => {
                metroPanel1.Show();
                lblText.Text = message;
            }));
            
        }

        public virtual void HideLoading()
        {
            Invoke(new Action(() => {
                metroPanel1.Hide();
                lblText.Text = "";
            }));
        }
        

        private void CommonInventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindow();
        }

        public virtual void CloseWindow()
        {
            DisconnectReader();
        }

        public virtual void StartInventory()
        {
            if (rfid.StartInventory(mComPort, RadioOperationMode.Continuous, 1) != operateResult.ok)
            {
                MetroMessageBox.Show(this, "开启扫描失败，请重新启动程序", "Error");
            }
        }

        public virtual void StopInventory()
        {
            rfid.StopInventory(mComPort);
        }
    }
}
