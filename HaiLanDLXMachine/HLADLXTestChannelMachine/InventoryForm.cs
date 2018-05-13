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

namespace HLABoxCheckChannelMachine
{

    public partial class InventoryForm : CommonInventoryFormIMP
    {
        CLogManager mLog = new CLogManager(true);
        string mCurBoxNo = "";
        Thread thread = null;
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);
        }
        private void InitView()
        {
            Invoke(new Action(() => {
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                lblLouceng.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.LOUCENG : "设备信息异常";
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";

            }));
        }
        public override void UpdateView()
        {
            Invoke(new Action(() => {
                lblQty.Text = epcList.Count.ToString();
            }));
        }
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();

            btnStart.Enabled = false;
            thread = new Thread(new ThreadStart(() => {
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
                    label9_hu.Text = "";
                    if (btnStart.Enabled)
                    {
                        Start();
                    }
                    lblQty.Text = "0";
                }));
                SetInventoryResult(0);
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();

                mCurBoxNo = "";

                if (boxNoList.Count > 0)
                {
                    mCurBoxNo = boxNoList.Dequeue();
                }
                Invoke(new Action(() =>
                {
                    label9_hu.Text = mCurBoxNo;
                }));
                reader.StartInventory(0, 0, 0);
                isInventory = true;
                lastReadTime = DateTime.Now;

            }
        }
        public override CheckResult CheckData()
        {
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
                if (tagDetailList != null && tagDetailList.Count > 0)
                {
                    TagDetailInfo t = tagDetailList[0];
                    foreach (var v in tagDetailList)
                    {
                        if (v.ZSATNR == t.ZSATNR && v.ZCOLSN == t.ZCOLSN && v.ZSIZTX == t.ZSIZTX)
                        {

                        }
                        else
                        {
                            result.UpdateMessage(@"品色规不唯一");
                            result.InventoryResult = false;

                            break;
                        }
                    }
                }
            }
            if (pinseCheckBool)
            {
                if (tagDetailList != null && tagDetailList.Count > 0)
                {
                    TagDetailInfo t = tagDetailList[0];
                    foreach (var v in tagDetailList)
                    {
                        if (v.ZSATNR == t.ZSATNR && v.ZCOLSN == t.ZCOLSN)
                        {

                        }
                        else
                        {
                            result.UpdateMessage(@"品色不唯一");
                            result.InventoryResult = false;

                            break;
                        }
                    }
                }
            }

            if (result.InventoryResult)
            {
                result.UpdateMessage(Consts.Default.RIGHT);
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

                //show in grid
                Invoke(new Action(() =>
                {
                    grid.Rows.Clear();
                    foreach(var v in epcList)
                    {
                        grid.Rows.Insert(0, v);
                    }
                }));

                SetInventoryResult(1);
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

        List<CTagDetail> getTags()
        {
            List<CTagDetail> re = new List<CTagDetail>();

            try
            {
                if (tagDetailList != null && tagDetailList.Count > 0)
                {
                    foreach (var v in tagDetailList)
                    {
                        if (!v.IsAddEpc)
                        {
                            if (!re.Exists(i => i.proNo == v.MATNR))
                            {
                                CTagDetail t = new CTagDetail();
                                t.proNo = v.MATNR;
                                t.zsatnr = v.ZSATNR;
                                t.zcolsn = v.ZCOLSN;
                                t.zsiztx = v.ZSIZTX;
                                t.charg = v.CHARG;
                                t.quan = 1;

                                re.Add(t);
                            }
                            else
                            {
                                re.FirstOrDefault(i => i.proNo == v.MATNR).quan += 1;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { }

            return re;
        }

        private void InventoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseWindow();
        }
    }
    public class CTagDetail
    {
        public string proNo;
        public string zsatnr;
        public string zcolsn;
        public string zsiztx;
        public string charg;
        public int quan;
    }
}
