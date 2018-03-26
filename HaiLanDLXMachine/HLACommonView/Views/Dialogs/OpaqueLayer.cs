using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace HLACommonView.Views.Dialogs
{
    /// <summary>
    /// 自定义控件:半透明控件
    /// </summary>
 
    [ToolboxBitmap(typeof(OpaqueLayer))]
    public class OpaqueLayer : Control
    {
        private bool _transparentBG = true;//是否使用透明
        private int _alpha = 125;//设置透明度

        private System.ComponentModel.Container components = new System.ComponentModel.Container();

        public OpaqueLayer()
            : this(125, true)
        {
        }

        public OpaqueLayer(int Alpha, bool IsShowLoadingImage, string msg = "")
        {
            SetStyle(System.Windows.Forms.ControlStyles.Opaque, true);
            base.CreateControl();

            this._alpha = Alpha;
            int loadingHeiht = 0;
            if (!string.IsNullOrEmpty(msg))
            {
                loadingHeiht = 30;
            }
            if (IsShowLoadingImage)
            {
                PictureBox pictureBox_Loading = new PictureBox();
                pictureBox_Loading.BackColor = System.Drawing.Color.White;
                //pictureBox_Loading.Image = Resources.loading;
                pictureBox_Loading.Name = "pictureBox_Loading";
                pictureBox_Loading.Size = new System.Drawing.Size(48, 48);
                pictureBox_Loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                Point Location = new Point(this.Location.X + (this.Width - pictureBox_Loading.Width) / 2, this.Location.Y + (this.Height - pictureBox_Loading.Height) / 2 - loadingHeiht);//居中
                pictureBox_Loading.Location = Location;
                pictureBox_Loading.Anchor = AnchorStyles.None;
                this.Controls.Add(pictureBox_Loading);
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Label label_Msg = new Label();
                label_Msg.AutoSize = true;
                label_Msg.Text = msg;
                label_Msg.Name = "label_Msg";
                label_Msg.Font = new Font("微软雅黑", 14, FontStyle.Regular);
                Point Location = new Point(this.Location.X + this.Width - label_Msg.Width, this.Location.Y + (this.Height - label_Msg.Height) / 2 + loadingHeiht);//居中
                label_Msg.Location = Location;
                label_Msg.Anchor = AnchorStyles.None;
                this.Controls.Add(label_Msg);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!((components == null)))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 自定义绘制窗体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            float vlblControlWidth;
            float vlblControlHeight;

            Pen labelBorderPen;
            SolidBrush labelBackColorBrush;

            if (_transparentBG)
            {
                Color drawColor = Color.FromArgb(this._alpha, this.BackColor);
                labelBorderPen = new Pen(drawColor, 0);
                labelBackColorBrush = new SolidBrush(drawColor);
            }
            else
            {
                labelBorderPen = new Pen(this.BackColor, 0);
                labelBackColorBrush = new SolidBrush(this.BackColor);
            }
            base.OnPaint(e);
            vlblControlWidth = this.Size.Width;
            vlblControlHeight = this.Size.Height;
            e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
            e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);
        }


        protected override CreateParams CreateParams//v1.10 
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //0x20;
                return cp;
            }
        } 
        [Category("OpaqueLayer"), Description("是否使用透明,默认为True")]
        public bool TransparentBG
        {
            get
            {
                return _transparentBG;
            }
            set
            {
                _transparentBG = value;
                this.Invalidate();
            }
        }

        [Category("OpaqueLayer"), Description("设置透明度")]
        public int Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = value;
                this.Invalidate();
            }
        }
    }


}
