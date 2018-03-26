using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xindeco.Device;
using Xindeco.Device.Model;
using System.Configuration;

namespace HLABigChannel
{
    public partial class CChaSetting : Form
    {
        public PLCController plc = null;

        public CChaSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                plc.SendCommand((PLCResponse)5);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CChaSetting_Load(object sender, EventArgs e)
        {
            try
            {
                plc = new PLCController(getPort());
                if (!plc.Connect())
                {
                    MessageBox.Show("连接PLC设备失败！", "错误");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string getPort()
        {
            return ConfigurationManager.AppSettings["Port"] == null ? "COM1" : ConfigurationManager.AppSettings["Port"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                plc.SendCommand((PLCResponse)6);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CChaSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                plc.Disconnect();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
