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
    public partial class ShadeForm : Form
    {
        public ShadeForm(IWin32Window owner)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            Bounds = (owner as Form).Bounds;
            Width = (owner as Form).Width;
            Height = (owner as Form).Height;
            Opacity = 10;
        }
    }
}
