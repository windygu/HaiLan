using DMSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HLACommon.Views
{
    public interface UploadMsgFormMethod
    {
        void Upload(CUploadData ud);
    }
    public partial class UploadMsgForm<T> : MetroForm
    {
        UploadMsgFormMethod mUploadMethod = null;
        ProcessDialog pd = new ProcessDialog();

        bool mSelAll = false;
        public UploadMsgForm(UploadMsgFormMethod p)
        {
            mUploadMethod = p;
            InitializeComponent();
        }

        private void initData()
        {
            Invoke(new Action(() =>
            {
                grid.Rows.Clear();

                List<CUploadData> list = SqliteDataService.GetExpUploadFromSqlite<T>();
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        grid.Rows.Insert(0, false, item.HU, item.MSG);
                        grid.Rows[0].Tag = item;
                    }
                }
            }));
        }
        private void UploadMgForm_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count > 0)
            {
                if (MetroMessageBox.Show(this, "确认要清除记录吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow row in grid.SelectedRows)
                    {
                        CUploadData box = row.Tag as CUploadData;
                        SqliteDataService.delUploadFromSqlite(box.Guid);
                    }

                    MetroMessageBox.Show(this, "成功清除", "提示");
                    initData();
                }
            }
        }

        public virtual void ShowLoading(string message)
        {
            Invoke(new Action(() => {
                pd.Show();
                metroPanel1.Show();
                lblText.Text = message;
            }));

        }

        public virtual void HideLoading()
        {
            Invoke(new Action(() => {
                pd.Hide();
                metroPanel1.Hide();
                lblText.Text = "";
            }));
        }
        private void btnReupload_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count>0)
            {
                Thread t = new Thread(new ThreadStart(() =>
                {
                    ShowLoading("正在上传...");
                    foreach (DataGridViewRow row in grid.SelectedRows)
                    {
                        CUploadData box = row.Tag as CUploadData;
                        SqliteDataService.delUploadFromSqlite(box.Guid);
                        mUploadMethod.Upload(box);
                    }
                    initData();
                    HideLoading();
                }));
                t.IsBackground = true;
                t.Start();
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            mSelAll = !mSelAll;
            grid.Focus();

            if(mSelAll)
            {
                grid.SelectAll();
            }
            else
            {
                grid.ClearSelection();
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            initData();
        }

        private void metroButton3_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
