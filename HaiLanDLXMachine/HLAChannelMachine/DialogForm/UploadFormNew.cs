using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.ENUM;
using HLACommonLib.DAO;

namespace HLAChannelMachine
{
    public partial class UploadFormNew : Form
    {
        bool mSelAll = false;
        private ReceiveType receiveType;
        InventoryMainForm mParentForm = null;
        public UploadFormNew(ReceiveType rt,InventoryMainForm pf)
        {
            mParentForm = pf;
            receiveType = rt;
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            initData();
        }

        private void UploadForm_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void initData()
        {
            lvData.Items.Clear();
            
            List<UploadData> list = SqliteDataService.GetUnUploadDataList();
            if (list != null && list.Count > 0)
            {
                btnUpload.Enabled = true;
                foreach (UploadData item in list)
                {
                    ResultDataInfo data = item.Data as ResultDataInfo;
                    ListViewItem lvi = new ListViewItem(data.Doc != null && data.Doc.DOCNO!=null ? data.Doc.DOCNO : "异常单号");
                    lvi.SubItems.Add(data.BoxNO != null ? data.BoxNO : "异常箱码");
                    lvi.SubItems.Add(data.ErrorMsg != null ? data.ErrorMsg : "异常");
                    lvi.SubItems.Add(data.CurrentNum.ToString());

                    string sapRe = ReceiveService.GetSaveDataSapResult(item.Guid);
                    if (string.IsNullOrEmpty(sapRe))
                    {
                        lvi.SubItems.Add(item.IsUpload == 0 ? "未上传" : "已上传");
                    }
                    else
                    {
                        lvi.SubItems.Add("SAP:" + sapRe);
                    }
                    lvi.Tag = item;
                    lvData.Items.Add(lvi);

                }
            }
            else
            {
                btnUpload.Enabled = false;
                MessageBox.Show("没有未上传的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
           
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            List<UploadData> uploadData = new List<UploadData>();
            foreach(ListViewItem i in lvData.SelectedItems)
            {
                UploadData tar = i.Tag as UploadData;
                if(tar!=null)
                {
                    tar.IsUpload = 1;
                    uploadData.Add(tar);
                }
                     
            }
            mParentForm.addUploadData(uploadData);
            if (uploadData.Count>0)
                MessageBox.Show("重新上传"+uploadData.Count.ToString()+"条记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkItemButton_Click(object sender, EventArgs e)
        {
            RecordForm form = new RecordForm(receiveType);
            form.ShowDialog();
        }

        private void button1_clearXiang_Click(object sender, EventArgs e)
        {
            DialogForm.ClearHu ch = new DialogForm.ClearHu();
            ch.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lvData.Focus();
            mSelAll = !mSelAll;
            foreach (ListViewItem item in lvData.Items)
            {
                item.Selected = mSelAll;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in lvData.SelectedItems)
            {
                UploadData tar = i.Tag as UploadData;
                if (tar != null)
                {
                    if (!SqliteDataService.SetUploaded(tar.Guid))
                    {
                        LogHelper.WriteLine(string.Format("更新uploaddata出错:GUID[{0}]", tar.Guid));
                    }
                }
            }

            if (lvData.SelectedItems.Count > 0)
                MessageBox.Show("删除" + lvData.SelectedItems.Count.ToString() + "条记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            initData();
        }

        private void button3_shezhi_Click(object sender, EventArgs e)
        {
            CCheckHuForm cf = new CCheckHuForm();
            cf.ShowDialog();
        }
    }
}
