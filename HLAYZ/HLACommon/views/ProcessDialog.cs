using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLACommon.Views
{
    public partial class ProcessDialog : Form
    {
        public ProcessDialog()
        {
            InitializeComponent();
            this.Opacity = 0.4;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.KeyDown += processDialog_KeyDown;
            this.KeyUp += processDialog_KeyDown;
            this.IsPictureShow = isPictureShow;
         }
        private bool isPictureShow = false;
        public bool IsPictureShow
        {
            get { return isPictureShow; }
            set { isPictureShow = value; }
        }
        private string _msg = "";
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }
        public ProcessDialog(string msg = "", bool isPictureShow = false)
        {
            InitializeComponent();
            this.Opacity = 0.7;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.KeyDown += processDialog_KeyDown;
            this.KeyUp += processDialog_KeyDown;
            this.IsPictureShow = isPictureShow;
            this.Msg = msg;
            if (!string.IsNullOrEmpty(msg))
            {
                lblMsg.Text = this.Msg;
            }
        }
        #region 圆角绘制
        /// <summary>
        /// 圆角：radius=圆角弧度   rect是要做圆角的矩形
        /// </summary>
        public void SetWindowRegion(int width, int height)
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, width, height);
            FormPath = GetRoundedRectPath(rect, 8);
            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角      
            path.AddArc(arcRect, 180, 90);
            //   右上角      
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //   右下角      
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            //   左下角      
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void Type(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(
                new Point[] {
            new Point(0, sender.Height / p_1),
            new Point(sender.Width / p_1, 0),
            new Point(sender.Width - sender.Width / p_1, 0),
            new Point(sender.Width, sender.Height / p_1),
            new Point(sender.Width, sender.Height - sender.Height / p_1),
            new Point(sender.Width - sender.Width / p_1, sender.Height),
            new Point(sender.Width / p_1, sender.Height),
            new Point(0, sender.Height - sender.Height / p_1) },

                (float)p_2);

            sender.Region = new Region(oPath);
        }
        #endregion
        Font font = new Font("微软雅黑", 20);
        private void processDialog_Paint(object sender, PaintEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Msg))
            {
                lblMsg.Text = this.Msg;
                lblMsg.Location = new Point(this.Width / 2 + metroProgressSpinner1.Width / 2
                    , this.Height / 2 - metroProgressSpinner1.Height / 2);
            }
        }

        private void processDialog_Load(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                if (this.IsPictureShow)
                {
                    metroProgressSpinner1.Location = new Point(
                        this.Width / 2 - metroProgressSpinner1.Width / 2
                        , (this.Height / 2 - metroProgressSpinner1.Height / 2) - 5);
                }
                else
                {
                    metroProgressSpinner1.Visible = false;

                }
                metroProgressSpinner1.Refresh();
            }));
        }

        private void processDialog_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void processDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
    }
}
