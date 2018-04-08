using DMSkin;
using HLACommonLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAChannelMachine.DialogForm
{
    public partial class ErrorLogForm : MetroForm
    {
        public ErrorLogForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
            epcList.Clear();
            grid.Rows.Clear();
        }

        private void ErrorLogForm_Load(object sender, EventArgs e)
        {
            //grid.ColumnHeadersHeight = 100;
        }

        private List<string> epcList = new List<string>();
        public void UpdateErrorLog(string docno, string epc, int boxqty,TagDetailInfo tag, ErrorType error)
        {
            if (string.IsNullOrEmpty(epc)) return;
            if (epcList.Contains(epc)) return;
            epcList.Add(epc);
            switch(error)
            {
                case ErrorType.不在本单:
                    if (grid.Rows.Count > 0)
                    {
                        bool exist = false;
                        foreach (DataGridViewRow row in grid.Rows)
                        {
                            if(string.IsNullOrEmpty(row.Cells["ZSATNR"].Value.ToString())
                                && string.IsNullOrEmpty(row.Cells["ZCOLSN"].Value.ToString())
                                && string.IsNullOrEmpty(row.Cells["ZSIZTX"].Value.ToString()))
                            {
                                UpdateGridRow(row.Index, "REALQTY", (int)row.Cells["REALQTY"].Value + 1);
                                UpdateGridRow(row.Index, "DIFFERENT", ((int)row.Cells["REALQTY"].Value - (int)row.Cells["PLANQTY"].Value));
                            }
                            exist = true;
                            break;
                        }
                        if(!exist)
                            AddGridRow(docno, "", "", "", 0, 1, 0, 0, error);
                    }
                    else
                        AddGridRow(docno, "", "", "", 0, 1, 0, 0,error);
                    break;
                case ErrorType.串规格:
                    if (grid.Rows.Count > 0)
                    {
                        bool exist = false;
                        foreach (DataGridViewRow row in grid.Rows)
                        {
                            if (row.Cells["ZSATNR"].Value.ToString() == tag.ZSATNR
                                && row.Cells["ZCOLSN"].Value.ToString() == tag.ZCOLSN
                                && row.Cells["ZSIZTX"].Value.ToString() == tag.ZSIZTX)
                            {
                                if (tag.IsAddEpc)
                                {
                                    UpdateGridRow(row.Index, "REALQTYEX", (int)row.Cells["REALQTYEX"].Value + 1);
                                    UpdateGridRow(row.Index, "DIFFERENTEX", ((int)row.Cells["REALQTYEX"].Value - (int)row.Cells["PLANQTYEX"].Value));
                                }
                                else
                                {
                                    UpdateGridRow(row.Index, "REALQTY", (int)row.Cells["REALQTY"].Value + 1);
                                    UpdateGridRow(row.Index, "DIFFERENT", ((int)row.Cells["REALQTY"].Value - (int)row.Cells["PLANQTY"].Value));
                                }
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            if (tag.IsAddEpc)
                                AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 0, 0, 1, error);
                            else
                                AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 1, 0, 0, error);
                        }
                    }
                    else
                    {
                        if (tag.IsAddEpc)
                            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 0, 0, 1, error);
                        else
                            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 1, 0, 0, error);
                    }
                    
                    break;
                case ErrorType.箱规不符:
                case ErrorType.数量大于配置:
                    if (grid.Rows.Count > 0)
                    {
                        bool exist = false;
                        foreach (DataGridViewRow row in grid.Rows)
                        {
                            if (row.Cells["ZSATNR"].Value.ToString() == tag.ZSATNR
                                && row.Cells["ZCOLSN"].Value.ToString() == tag.ZCOLSN
                                && row.Cells["ZSIZTX"].Value.ToString() == tag.ZSIZTX)
                            {
                                if (tag.IsAddEpc)
                                {
                                    UpdateGridRow(row.Index, "REALQTYEX", (int)row.Cells["REALQTYEX"].Value + 1);
                                    UpdateGridRow(row.Index, "DIFFERENTEX", ((int)row.Cells["REALQTYEX"].Value - (int)row.Cells["PLANQTYEX"].Value));
                                }
                                else
                                {
                                    UpdateGridRow(row.Index, "REALQTY", (int)row.Cells["REALQTY"].Value + 1);
                                    UpdateGridRow(row.Index, "DIFFERENT", ((int)row.Cells["REALQTY"].Value - (int)row.Cells["PLANQTY"].Value));
                                }
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            if (tag.IsAddEpc)
                                AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 0, 0, 1, error);
                            else
                                AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 1, 0, 0, error);
                        }
                    }
                    else
                    {
                        if (tag.IsAddEpc)
                            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 0, boxqty, boxqty + 1, error);
                        else
                            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, boxqty, boxqty + 1, 0, 0, error);
                    }
                    break;
                case ErrorType.商品已扫描:
                    if (grid.Rows.Count > 0)
                    {
                        bool exist = false;
                        foreach (DataGridViewRow row in grid.Rows)
                        {
                            if (row.Cells["ZSATNR"].Value.ToString() == tag.ZSATNR
                                && row.Cells["ZCOLSN"].Value.ToString() == tag.ZCOLSN
                                && row.Cells["ZSIZTX"].Value.ToString() == tag.ZSIZTX && row.Cells["MEMO"].Value.ToString() == error.ToString())
                            {
                                if (tag.IsAddEpc)
                                {
                                    UpdateGridRow(row.Index, "REALQTYEX", (int)row.Cells["REALQTYEX"].Value + 1);
                                    UpdateGridRow(row.Index, "DIFFERENTEX", ((int)row.Cells["REALQTYEX"].Value - (int)row.Cells["PLANQTYEX"].Value));
                                }
                                else
                                {
                                    UpdateGridRow(row.Index, "REALQTY", (int)row.Cells["REALQTY"].Value + 1);
                                    UpdateGridRow(row.Index, "DIFFERENT", ((int)row.Cells["REALQTY"].Value - (int)row.Cells["PLANQTY"].Value));
                                }
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            if (tag.IsAddEpc)
                                AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 0, 0, 1, error);
                            else
                                AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 1, 0, 0, error);
                        }
                    }
                    else
                    {
                        if (tag.IsAddEpc)
                            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 0, 0, 1, error);
                        else
                            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, 0, 1, 0, 0, error);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 更新箱规不符的情况
        /// </summary>
        /// <param name="docno"></param>
        /// <param name="tag"></param>
        /// <param name="boxqty"></param>
        /// <param name="currentnum"></param>
        public void UpdateErrorLog(string docno, TagDetailInfoExtend tag, int boxqty, int currentnum)
        {
            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, boxqty, currentnum, 0, 0, ErrorType.箱规不符);
        }

        public void UpdateErrorLogBigThanPeiZhi(string docno, TagDetailInfoExtend tag, int boxqty, int currentnum)
        {
            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, boxqty, currentnum, 0, 0, ErrorType.数量大于配置);
        }
        /// <summary>
        /// 更新主条码和辅条码数量不对应的情况
        /// </summary>
        /// <param name="docno"></param>
        /// <param name="tag"></param>
        /// <param name="boxqty"></param>
        /// <param name="currentnum"></param>
        /// <param name="boxqtyex"></param>
        /// <param name="currentnumex"></param>
        public void UpdateErrorLog(string docno, TagDetailInfoExtend tag, int boxqty, int currentnum, int boxqtyex, int currentnumex)
        {
            AddGridRow(docno, tag.ZSATNR, tag.ZCOLSN, tag.ZSIZTX, boxqty, currentnum, boxqtyex, currentnumex, ErrorType.主条码和辅条码数量不对应);
        }

        private void AddGridRow(string docno,string zsatnr,string zcolsn,string zsiztx,
            int mainplan,int mainscan,int assistplan,int assistscan,ErrorType error)
        {
            grid.Rows.Add(docno, zsatnr, zcolsn, zsiztx, mainplan, mainscan,
                mainscan - mainplan, assistplan, assistscan, assistscan - assistplan, error.ToString());
        }

        private void UpdateGridRow(int rowindex,string columnname,object value)
        {
            if(grid.Rows.Count>rowindex)
            {
                grid.Rows[rowindex].Cells[columnname].Value = value;
            }
        }
    }

    public enum ErrorType
    {
        不在本单=1,
        串规格=2,
        箱规不符=3,
        主条码和辅条码数量不对应 = 4,
        商品已扫描 = 5,
        数量大于配置
    }
}
