using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xindeco.Device.Model;
using System.Xml;
using OSharp.Utility.Extensions;
using Newtonsoft.Json;

namespace HLABoxCheckChannelMachine
{

    public partial class InventoryForm : CommonInventoryForm
    {
        CLogManager mLog = new CLogManager(true);
        List<PSGCount> mPSGList = new List<PSGCount>();
        string mCurBoxNo = "";
        Thread thread = null;
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);
        }
        private void InitView()
        {
            Invoke(new Action(() =>
            {
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "设备信息异常";
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";

            }));
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();

            btnStart.Enabled = false;
            thread = new Thread(new ThreadStart(() =>
            {
                ShowLoading("正在连接PLC...");
                if (ConnectPlc())
                    Invoke(new Action(() => { lblPlc.Text = "正常"; lblPlc.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblPlc.Text = "异常"; lblPlc.ForeColor = Color.OrangeRed; }));

                ShowLoading("正在连接条码扫描枪...");
                ConnectBarcode();

                ShowLoading("正在连接读写器...");
                if (ConnectReader())
                    Invoke(new Action(() => { lblReader.Text = "正常"; lblReader.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed; }));

                bool closed = false;

                ShowLoading("正在下载物料数据...");
                materialList = SAPDataService.GetMaterialInfoListAll(SysConfig.LGNUM);
                if (materialList == null || materialList.Count <= 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到物料主数据，请检查网络情况", "提示");

                        closed = true;
                        Close();
                    }));

                }

                if (closed) return;

                ShowLoading("正在下载吊牌数据...");
                hlaTagList = LocalDataService.GetAllRfidHlaTagList();
                if (hlaTagList == null || hlaTagList.Count <= 0)
                {


                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到吊牌主数据，请检查网络情况", "提示");

                        closed = true;
                        Close();
                    }));

                }


                if (closed) return;

                Invoke(new Action(() =>
                {
                    btnStart.Enabled = true;
                }));
                HideLoading();

            }));

            thread.IsBackground = true;
            thread.Start();
        }
        private void Start()
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;

            allCheck.Enabled = false;
            pinseCheck.Enabled = false;
        }
        private void Pause()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;

            allCheck.Enabled = true;
            pinseCheck.Enabled = true;

        }
        public override void StartInventory()
        {
            if (!boxCheckCheckBox.Checked)
            {
                SetInventoryResult(1);
                return;
            }

            if (!isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "开始扫描";
                    if (btnStart.Enabled)
                    {
                        Start();
                    }
                }));
                SetInventoryResult(0);
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();

                mCurBoxNo = "";
                mPSGList.Clear();

                if (boxNoList.Count > 0)
                {
                    mCurBoxNo = boxNoList.Dequeue();
                }

                reader.StartInventory(mGhost, mTrigger, mR6ghost);
                isInventory = true;
                lastReadTime = DateTime.Now;

            }
        }
        public override CheckResult CheckData()
        {
            if (tagDetailList != null)
            {
                try
                {
                    var re = tagDetailList.GroupBy(i => new { i.ZSATNR, i.ZCOLSN, i.ZSIZTX })
                        .Select(i => new { i.Key.ZSATNR, i.Key.ZCOLSN, i.Key.ZSIZTX, Count = i.Count() });

                    if (re != null)
                    {
                        foreach (var item in re)
                        {
                            mPSGList.Add(new PSGCount(item.ZSATNR, item.ZCOLSN, item.ZSIZTX, item.Count));
                        }
                    }
                }
                catch (Exception)
                { }
            }

            CheckResult result = base.CheckData();

            bool pinseCheckBool = false;
            bool allCheckBool = false;
            Invoke(new Action(() =>
            {
                pinseCheckBool = pinseCheck.Checked;
                allCheckBool = allCheck.Checked;
            }));
            if (allCheckBool)
            {
                if (mPSGList.Count != 1)
                {
                    result.UpdateMessage(@"品色规不唯一");
                    result.InventoryResult = false;
                }
            }
            if (pinseCheckBool)
            {
                var re = mPSGList.Select(i => new { i.p, i.s }).Distinct().ToList();
                if (re.Count() != 1)
                {
                    result.UpdateMessage(@"品色不唯一");
                    result.InventoryResult = false;
                }
            }

            if (result.InventoryResult)
            {
                result.UpdateMessage(Consts.Default.RIGHT);

                SetInventoryResult(1);
            }
            else
            {
                SetInventoryResult(3);
            }

            return result;
        }
        public override void StopInventory()
        {
            if (!boxCheckCheckBox.Checked)
            {
                return;
            }

            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();
                CheckResult cre = CheckData();

                //print
                bool shouldPrint = false;
                Invoke(new Action(() =>
                {
                    shouldPrint = printCheckBox.Checked;
                }));
                if (shouldPrint)
                {
                    HLABoxCheckChannelMachine.Utils.PrintHelper.PrintRightTag(mPSGList, mCurBoxNo);
                }

                if (cre.InventoryResult)
                {

                }
                else
                {

                }

                //show in grid
                if (cre.InventoryResult)
                {
                    foreach (PSGCount item in mPSGList)
                    {
                        Invoke(new Action(() =>
                        {
                            grid.Rows.Insert(0, mCurBoxNo, item.p, item.s, item.g, item.count, cre.Message);
                        }));

                    }

                }
                else
                {
                    foreach (PSGCount item in mPSGList)
                    {
                        Invoke(new Action(() =>
                        {
                            grid.Rows.Insert(0, mCurBoxNo, item.p, item.s, item.g, item.count, cre.Message);
                            grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                        }));
                    }
                }

                if (errorEpcNumber > 0)
                {
                    Invoke(new Action(() =>
                    {
                        grid.Rows.Insert(0, mCurBoxNo, "", "", "", errorEpcNumber, "商品未注册");
                        grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }));
                }


            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pinseCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (pinseCheck.Checked)
            {
                pinseCheck.BackColor = Color.Tan;
                allCheck.Checked = false;
                allCheck.BackColor = Color.WhiteSmoke;
            }
            else
            {
                pinseCheck.BackColor = Color.WhiteSmoke;
            }
        }

        private void allCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (allCheck.Checked)
            {
                allCheck.BackColor = Color.Tan;
                pinseCheck.Checked = false;
                pinseCheck.BackColor = Color.WhiteSmoke;
            }
            else
            {
                allCheck.BackColor = Color.WhiteSmoke;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void InventoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseWindow();
        }
    }

    public class PSGCount
    {
        public string p;
        public string s;
        public string g;
        public int count;
        public PSGCount(string p_, string s_, string g_, int count_)
        {
            p = p_; s = s_; g = g_; count = count_;
        }
    }
}
