using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonView.Views;
using HLACommonView.Model;
using System.Threading;
using HLACommonLib;
using System.Configuration;

namespace HLAZZZTest
{
    public partial class Form1 : CommonInventoryForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SysConfig.DBUrl = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            SysConfig.LGNUM = ConfigurationManager.AppSettings["LGNUM"];
            SysConfig.DeviceNO = ConfigurationManager.AppSettings["DeviceNO"];

            int i, j, k;
            LocalDataService.GetGhostAndTrigger(out i, out j, out k);


            grid.Rows.Add("123", "12345678901234567890", "123", "12345678901234567890", "12345678901234567890", "12345678901234567890", "12345678901234567890", "123456789012312345678901234567890123456789012345678904567890");
            grid.Rows.Add("123", "12345678901234567890", "123", "12345678901234567890", "12345678901234567890", "12345678901234567890", "12345678901234567890", "123456789012312345678901234567890123456789012345678904567890");
            grid.Rows.Add("123", "12345678901234567890", "123", "12345678901234567890", "12345678901234567890", "12345678901234567890", "12345678901234567890", "123456789012312345678901234567890123456789012345678904567890");

        }
    }
}
