using DMSkin;
using HLACommonView.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using HLACommonLib;

namespace HLACommonView.Views
{
    public partial class ConfigFormPM : MetroForm
    {        
        public ConfigFormPM()
        {
            InitializeComponent();           
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            LoadGlConfig();
        }

        private void LoadGlConfig()
        {
            textBox1_gl.Text = SysConfig.ReaderPower.ToString();
        }
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            bool exist = false;
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == newKey)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string gl = textBox1_gl.Text.Trim();

                int dgl = 0;
                int.TryParse(gl, out dgl);
                if(dgl<1 || dgl>23)
                {
                    MetroMessageBox.Show(this, "请输入有效功率",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                UpdateAppConfig("ReaderPower", gl);

                MetroMessageBox.Show(this, "更改配置后，需要重新启动程序才能使新配置生效",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.ToString(),
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            Application.Exit();

        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
