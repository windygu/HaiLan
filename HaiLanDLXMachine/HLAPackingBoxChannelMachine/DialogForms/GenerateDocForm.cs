using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.PACKING;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAPackingBoxChannelMachine.DialogForms
{
    public delegate void GenerateSuccessHandler(string lh);

    public partial class GenerateDocForm : MetroForm
    {
        public event GenerateSuccessHandler OnGenerateSuccess;
        private List<PBBoxInfo> boxList = null;
        private int errorBoxNum = 0;
        private int rightBoxNum = 0;
        public GenerateDocForm(List<PBBoxInfo> _boxList)
        {
            InitializeComponent();
            boxList = _boxList;
        }

        private void GenerateDocForm_Load(object sender, EventArgs e)
        {
            if (boxList == null || boxList.Count == 0)
                boxList = PackingBoxService.GetNeedGenerateBoxListWithoutDetail();
            if (boxList != null && boxList.Count > 0)
            {
                foreach (PBBoxInfo item in boxList)
                {
                    if (item.RESULT == "E")
                        continue;
                    if (item.PACKRESULT == "E")
                    {
                        //grid.Rows.Insert(0, item.Timestamps.ToString("yyyy-MM-dd"), item.LH, item.HU, item.PACKMSG);
                        //grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                        errorBoxNum++;
                    }
                    else
                    {
                        rightBoxNum++;
                        grid.Rows.Add(item.Timestamps.ToString("yyyy-MM-dd"), item.LH, item.HU, item.PACKMSG);
                    }
                }

                if (errorBoxNum > 0)
                {
                    ShowStatusInfo(false, "有包装失败箱号，是否继续生成交接单?");
                }
                else
                {
                    ShowStatusInfo(true, "包装状态无异常，是否生成交接单?");
                }

                btnYes.Enabled = true;
                btnNo.Enabled = true;
            }
            else
            {
                ShowStatusInfo(false, "当前没有需要生成交接单的箱记录");
                btnYes.Enabled = false;
                btnNo.Enabled = true;
            }

            lblRight.Text = rightBoxNum.ToString();
            lblError.Text = errorBoxNum.ToString();


        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            string lh = boxList != null && boxList.Count > 0 ? boxList.First().LH : "";
            if (string.IsNullOrEmpty(lh))
            {
                ShowStatusInfo(false, "楼号为空，不能生成交接单");
                return;
            }
            SapResult result = SAPDataService.GenerateDocInfo(
                SysConfig.LGNUM, SysConfig.CurrentLoginUser.UserId, lh,
                boxList.FindAll(i => i.LH == lh && i.PACKRESULT == "S" && i.RESULT == "S").Select(i => i.HU).ToList());
            if (result.STATUS == "E")
            {
                ShowStatusInfo(false, result.MSG);
            }
            else
            {
                PackingBoxService.UpdateBoxGenerated(lh);
                if (OnGenerateSuccess != null)
                    OnGenerateSuccess(lh);
                MetroMessageBox.Show(this, "生成交接单成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
        }


        private void ShowStatusInfo(bool right, string msg)
        {
            if (right)
                lblStatus.BackColor = Color.FromArgb(27, 163, 203);
            else
                lblStatus.BackColor = Color.OrangeRed;
            lblStatus.Text = msg;
        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
