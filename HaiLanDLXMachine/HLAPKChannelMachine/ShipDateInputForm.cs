using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.PK;

namespace HLAPKChannelMachine
{
    public partial class ShipDateInputForm : MetroForm
    {
        private InventoryMainForm parentForm = null;

        public ShipDateInputForm(InventoryMainForm _parentForm)
        {
            InitializeComponent();

            parentForm = _parentForm;
        }

        private void btnDeliver_Click(object sender, EventArgs e)
        {
            //根据发运日期拉取发运箱数据
            //this.btnDeliver.Enabled = false;
            //DateTime shipDate = this.dtShip.Value.Date;

            //Thread thread = new Thread(new ThreadStart(() => {
            //    this.Invoke(new Action(() => { 
            //        panelLoading.Show();
            //        lblStatus.Text = "正在下载物料和吊牌数据...";
            //    }));

            //    List<MaterialInfo> materialList = LocalDataService.GetMaterialInfoList();
            //    if (materialList == null || materialList.Count <= 0)
            //    {
            //        this.Invoke(new Action(() =>
            //        {
            //            MetroMessageBox.Show(this, "未下载到物料主数据，请检查网络情况", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            GetDataOver();
            //        }));
            //        return;
            //    }
            //    List<HLATagInfo> tagList = LocalDataService.GetAllRfidHlaTagList();
            //    if (tagList == null || tagList.Count <= 0)
            //    {
            //        this.Invoke(new Action(() =>
            //        {
            //            MetroMessageBox.Show(this, "未下载到吊牌数据，请检查网络情况", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            GetDataOver();
            //        }));
            //        return;
            //    }

            //    this.Invoke(new Action(() =>
            //    {
            //        panelLoading.Show();
            //        lblStatus.Text = "正在加载历史信息...";
            //    }));

            //    List<DeliverEpcDetail> deliverEpcDetailList = LocalDataService.GetDeliverEpcDetailList(SysConfig.LGNUM, shipDate, SysConfig.DeviceInfo.LOUCENG);
            //    if (deliverEpcDetailList == null)
            //        deliverEpcDetailList = new List<DeliverEpcDetail>();

            //    //加载发运错误箱历史记录
            //    List<PKDeliverErrorBox> historyDeliverErrorBoxList = LocalDataService.GetDeliverErrorBoxListByLOUCENGAndSHIPDATE(SysConfig.DeviceInfo.LOUCENG, shipDate);
            //    if (historyDeliverErrorBoxList == null)
            //        historyDeliverErrorBoxList = new List<PKDeliverErrorBox>();

            //    //加载发运箱历史记录
            //    List<PKDeliverBox> historyDeliverBoxList = LocalDataService.GetDeliverBoxListByLOUCENGAndSHIPDATE(SysConfig.DeviceInfo.LOUCENG, shipDate);
            //    if (historyDeliverBoxList == null)
            //        historyDeliverBoxList = new List<PKDeliverBox>();

            //    this.Invoke(new Action(() =>
            //    {
            //        GetDataOver();
            //        //InventoryMainForm form = new InventoryMainForm();
            //        //form.ShowDialog();
            //        if (this.parentForm != null)
            //        {
            //            this.parentForm.LoadShipDate(shipDate, materialList, tagList, deliverEpcDetailList, historyDeliverErrorBoxList, historyDeliverBoxList);
            //        }

            //        this.Close();
            //    }));
            //}));
            //thread.IsBackground = true;
            //thread.Start();
        }

        private void GetDataOver()
        {
            this.btnDeliver.Enabled = true;
            panelLoading.Hide();
            lblStatus.Text = "";
        }

        private void ShipDateInputForm_Load(object sender, EventArgs e)
        {
            this.dtShip.Value = DateTime.Now;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
