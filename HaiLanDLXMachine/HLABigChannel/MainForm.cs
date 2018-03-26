﻿using DMSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Xindeco.Device;
using Xindeco.Device.Model;
using HLACommonLib;

namespace HLABigChannel
{
    public partial class MainForm : Form
    {
       [DllImport("msvcrt.dll")]
       static extern int system(string cmd);
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnReciever_Click(object sender, EventArgs e)
        {
            this.btnReciever.Enabled = false;

            //StartProgram(sender, "ReceiverDir", "ReceiverExe", "receiver");

            this.btnReciever.Enabled = true;
            this.WindowState = FormWindowState.Maximized;

        }

        private void StartProgram(object sender, string exeName, string dirName)
        {
            string dir = Application.StartupPath + string.Format("\\{0}", dirName);
            string path = dir.TrimEnd('/', '\\') + "\\" + exeName;
            if (File.Exists(path))
            {
                this.WindowState = FormWindowState.Minimized;

                Process myProcess = new Process();
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(path);
                myProcessStartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                myProcess.StartInfo = myProcessStartInfo;
                myProcessStartInfo.Arguments = "\"" + SysConfig.LGNUM + "\"" + " "
                    + "\"" + SysConfig.DeviceNO + "\"" + " "
                    + "\"" + SysConfig.DBUrl + "\"";
                myProcess.Start();

                while (!myProcess.HasExited)
                {
                    myProcess.WaitForExit();

                }
            }
            else
            {
                    MessageBox.Show("程序不存在，请检查安装目录！",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnPacking_Click(object sender, EventArgs e)
        {
            btnPacking.Enabled = false;

            //StartProgram(sender, "PackingDir", "PackingExe", "packing");

            btnPacking.Enabled = true;
            this.WindowState = FormWindowState.Maximized;

        }

        private void btnYk_Click(object sender, EventArgs e)
        {
            btnYk.Enabled = false;

            //StartProgram(sender, "YkDir", "YkExe", "yk");

            btnYk.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnDeliver_Click(object sender, EventArgs e)
        {
            btnDeliver.Enabled = false;

            StartProgram(sender, "HLAPKChannelMachine.exe", "deliver");

            btnDeliver.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnElectronic_Click(object sender, EventArgs e)
        {
            btnElectronic.Enabled = false;

           // StartProgram(sender, "ElectronicDir", "ElectronicExe", "electronic");

            btnElectronic.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string title = ConfigurationManager.AppSettings["Title"];
            if(string.IsNullOrEmpty(title))
            {

            }
            else
            {
                label1.Text = title;
            }

            label2_IP.Text = GetLocalIPAddress();

            openMachine();
        }

        public string getPort()
        {
            return ConfigurationManager.AppSettings["Port"] == null ? "COM1" : ConfigurationManager.AppSettings["Port"];
        }

        void openMachine()
        {
            try
            {
                PLCController plc = new PLCController(getPort());
                if (plc.Connect())
                {
                    plc.SendCommand((PLCResponse)5);
                    plc.Disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void closeMachine()
        {
            try
            {
                PLCController plc = new PLCController(getPort());
                if (plc.Connect())
                {
                    plc.SendCommand((PLCResponse)6);
                    plc.Disconnect();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    string ipStr = ip.ToString();
                    if (ipStr.StartsWith("172"))
                    {
                        return ipStr;
                    }
                }
            }
            return "";
        }
        private void manualDownloadButton_Click(object sender, EventArgs e)
        {
            this.manualDownloadButton.Enabled = false;

            StartProgram(sender, "HLAManualDownload.exe", "mdownload");

            this.manualDownloadButton.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void netStatusButton_Click(object sender, EventArgs e)
        {
            bool networkUp = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (networkUp)
            {
                MetroMessageBox.Show(this, "网络正常", "网络状态", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MetroMessageBox.Show(this, "网络异常，请检查网络", "网络状态", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void sortReviewButton_Click(object sender, EventArgs e)
        {
            this.sortReviewButton.Enabled = false;

           // StartProgram(sender, "SortReviewDir", "SortReviewExe", "sortreview");

            this.sortReviewButton.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void lowLibButton_Click(object sender, EventArgs e)
        {
            this.btnReciever.Enabled = false;

           // StartProgram(sender, "ReceiverDir", "ReceiverExe", "receiverlow");

            this.btnReciever.Enabled = true;
            this.WindowState = FormWindowState.Maximized;

        }

        private void boxReviewButton_Click(object sender, EventArgs e)
        {
            this.boxReviewButton.Enabled = false;

           // StartProgram(sender, "BoxReviewDir", "BoxReviewExe", "boxreview");

            this.boxReviewButton.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void dmButton1_Click(object sender, EventArgs e)
        {
            CChaSetting setform = new CChaSetting();
            setform.ShowDialog();
        }


        private void dmButton3_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void dmButton9_Cancel_Click(object sender, EventArgs e)
        {
            this.boxReviewButton.Enabled = false;

          //  StartProgram(sender, "CancelCheckDir", "CancelCheckExe", "cancelcheck");

            this.boxReviewButton.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private void dmButton9_jiehuodan_Click(object sender, EventArgs e)
        {
            this.dmButton9_jiehuodan.Enabled = false;

        //    StartProgram(sender, "JIANHUODIR", "JIANHUOExe", "jianhuo");

            this.dmButton9_jiehuodan.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
        }


        private void dmButton9_jiaojiedan_Click(object sender, EventArgs e)
        {
            this.dmButton9_jiaojiedan.Enabled = false;

            //StartProgram(sender, "JiaoJieDanDir", "JiaoJieDanExe", "jiaojiedan");

            this.dmButton9_jiaojiedan.Enabled = true;
            this.WindowState = FormWindowState.Maximized;

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeMachine();
        }

    }
}
