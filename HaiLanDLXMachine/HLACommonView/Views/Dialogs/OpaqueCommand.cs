using System;
using System.Windows.Forms;

namespace HLACommonView.Views.Dialogs
{
    class OpaqueCommand
    {
        private OpaqueLayer m_OpaqueLayer = null;//°ëÍ¸Ã÷ÃÉ°å²ã

        /// <summary>
        /// ÏÔÊ¾ÕÚÕÖ²ã
        /// </summary>
        /// <param name="control">¿Ø¼þ</param>
        /// <param name="alpha">Í¸Ã÷¶È</param>
        /// <param name="isShowLoadingImage">ÊÇ·ñÏÔÊ¾Í¼±ê</param>
        public void ShowOpaqueLayer(Control control, int alpha, bool isShowLoadingImage, string msg = "")
        {
            try
            {
                if (this.m_OpaqueLayer == null)
                {
                    this.m_OpaqueLayer = new OpaqueLayer(alpha, isShowLoadingImage, msg);
                    control.Controls.Add(this.m_OpaqueLayer);
                    this.m_OpaqueLayer.Dock = DockStyle.Fill;
                    this.m_OpaqueLayer.BringToFront();
                }
                else
                {
                    Label lbl = new Label();
                    Control[] ctrl = this.m_OpaqueLayer.Controls.Find("label_Msg", true);
                    if (ctrl != null && ctrl.Length > 0)
                    {
                        lbl = (Label)ctrl[0];
                    }
                    lbl.Text = msg;
                }
                this.m_OpaqueLayer.Enabled = true;
                this.m_OpaqueLayer.Visible = true;
            }
            catch { }
        }

        /// <summary>
        /// Òþ²ØÕÚÕÖ²ã
        /// </summary>
        public void HideOpaqueLayer()
        {
            try
            {
                if (this.m_OpaqueLayer != null)
                {
                    this.m_OpaqueLayer.Visible = false;
                    this.m_OpaqueLayer.Enabled = false;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

    }
}
