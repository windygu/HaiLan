using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using HLACommonLib;
using HLAChannelMachine.Utils;
using System.Threading;

namespace HLAChannelMachine
{
    public partial class TestForm : Form
    {

        

        /// <summary>
        /// PLC串口
        /// </summary>
        SerialPort port = null;
        private List<byte> comBuffer = new List<byte>(4096);
        private Queue<string> boxNoList = new Queue<string>();
        private Queue<string> comDataQueue = new Queue<string>();
        #region 条码扫描模组相关属性
        /// <summary>
        /// 条码扫描模组1
        /// </summary>
        SerialPort scannerPort_1 = null;  //条码扫描枪串口
        /// <summary>
        /// 条码扫描模组2
        /// </summary>
        SerialPort scannerPort_2 = null;  //条码扫描枪串口
        /// <summary>
        /// 条码扫描枪串口缓存
        /// </summary>
        private List<byte> scannerComBuffer = new List<byte>(4096);
        #endregion

        /// <summary>
        /// 最后读到的PLC串口数据
        /// </summary>
        private string lastComData = null;
        /// <summary>
        /// 最后读到PLC串口数据的时间
        /// </summary>
        private DateTime lastComDataTime = DateTime.Now;
        private Thread logicThread = null;

        public TestForm()
        {
            InitializeComponent();
            initPlcPort();
            initScannerPort();
            this.logicThread = new Thread(new ThreadStart(LogicThreadFunc));
            this.logicThread.IsBackground = true;
            this.logicThread.Start();
        }
        #region PLC指令-漳州 FS代表工控机发送给PLC的指令 JS代表PLC发送给工控机的指令
        /// <summary>
        /// 打开射频指令
        /// </summary>
        private const string ZZ_JS_KAI_SHE_PIN = "010100000001FDCA";
        /// <summary>
        /// 关闭射频指令
        /// </summary>
        private const string ZZ_JS_GUAN_SHE_PIN = "010200000001B9CA";
        /// <summary>
        /// 询问结果指令
        /// </summary>
        private const string ZZ_JS_XUN_WEN_JIE_GUO = "010300000001840A";
        /// <summary>
        /// 正常指令
        /// </summary>
        private const string ZZ_FS_ZHENG_CHANG = "010300000001840A";
        /// <summary>
        /// 重检指令（已过时）
        /// </summary>
        private const string ZZ_FS_CHONG_JIAN = "010300000002C40B";
        /// <summary>
        /// 异常指令
        /// </summary>
        private const string ZZ_FS_YI_CHANG = "01030000000305CB";
        /// <summary>
        /// 延时检测指令（已过时）
        /// </summary>
        private const string ZZ_FS_YAN_SHI_JIAN_CE = "0103000000044409";
        #endregion
        /// <summary>
        /// PLC信息处理方法
        /// </summary>
        private void LogicThreadFunc()
        {
            while (true)
            {
                if (comDataQueue.Count > 0)
                {
                    string hexStr = comDataQueue.Dequeue();
                    LogHelper.WriteLine("[接收]" + hexStr);
                    if (!string.IsNullOrEmpty(hexStr))
                    {

                        #region 逻辑处理 漳州
                        string responseStr = null;
                        byte[] responseBts = null;
                        if (hexStr == ZZ_JS_KAI_SHE_PIN) //开射频
                        {
                            //result = StartInventory();
                            this.label6.Text = (int.Parse(this.label6.Text) + 1).ToString();
                            if (boxNoList.Count <= 0)
                            {
                                this.label7.Text = (int.Parse(this.label7.Text) + 1).ToString();
                            }
                            else if (boxNoList.Count > 1)
                            {
                                this.label1.Text = boxNoList.Dequeue();
                                this.label8.Text = (int.Parse(this.label8.Text) + 1).ToString();
                            }
                            else
                            {
                                this.label1.Text = boxNoList.Dequeue();                                
                            }
                        }
                        else if (hexStr == ZZ_JS_GUAN_SHE_PIN) //关射频
                        {
                            //result = StopInventory();
                        }
                        else if (hexStr == ZZ_JS_XUN_WEN_JIE_GUO) //询问结果
                        {
                            responseStr = ZZ_FS_YI_CHANG;
                            responseBts = TypeConvert.HexStringToByteArray(responseStr);
                            port.Write(responseBts, 0, 8);
                            this.boxNoList.Clear();
                        }
                        else
                        {
                        }
                        #endregion

                    }

                    Thread.Sleep(20);
                }
            }
        }

        /// <summary>
        /// 初始化扫描模组串口
        /// </summary>
        private void initScannerPort()
        {
            #region 连接条码扫描器串口-add by jrzhuang
            scannerPort_1 = new SerialPort("COM3");
            scannerPort_1.BaudRate = 9600;//波特率
            scannerPort_1.Parity = Parity.None;//无奇偶校验位
            scannerPort_1.StopBits = StopBits.One;//一个停止位
            scannerPort_1.DataBits = 8;
            scannerPort_1.ReadTimeout = 200;
            scannerPort_1.ReadBufferSize = 8;
            scannerPort_1.DataReceived += new SerialDataReceivedEventHandler(scannerPort_1_DataReceived);
            try
            {
                scannerPort_1.Open();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            if (!scannerPort_1.IsOpen)
            {
                MessageBox.Show("连接条码扫描枪串口设备1失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            scannerPort_2 = new SerialPort("COM4");
            scannerPort_2.BaudRate = 9600;//波特率
            scannerPort_2.Parity = Parity.None;//无奇偶校验位
            scannerPort_2.StopBits = StopBits.One;//一个停止位
            scannerPort_2.DataBits = 8;
            scannerPort_2.ReadTimeout = 200;
            scannerPort_2.ReadBufferSize = 8;
            scannerPort_2.DataReceived += new SerialDataReceivedEventHandler(scannerPort_2_DataReceived);
            try
            {
                scannerPort_2.Open();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            if (!scannerPort_2.IsOpen)
            {
                MessageBox.Show("连接条码扫描枪串口设备2失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }

        /// <summary>
        /// 初始化PLC串口通信
        /// </summary>
        private void initPlcPort()
        {
            #region 连接串口
            port = new SerialPort("COM1");
                //通道机-漳州-1
                port.BaudRate = 9600;//波特率
                port.Parity = Parity.None;//无奇偶校验位
                port.StopBits = StopBits.One;//一个停止位
                port.DataBits = 8;
                port.ReadTimeout = 200;
                port.ReadBufferSize = 8;

            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);//DataReceived事件委托

            try
            {
                port.Open();
            }
            catch { }

            if (!port.IsOpen)
            {
                MessageBox.Show("连接串口设备失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            #endregion
        }


        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = port.BytesToRead;
                byte[] buf = new byte[n];
                //默认是杏林的通道机

                port.Read(buf, 0, n);

                //缓存数据
                this.comBuffer.AddRange(buf);

                //判断数据是否达到8个字节
                while (this.comBuffer.Count >= 8)
                {
                    //判断数据头是否为01
                    if (this.comBuffer[0] == 0x01)
                    {
                        byte[] package = new byte[8];
                        this.comBuffer.CopyTo(0, package, 0, 8);
                        string hexStr = TypeConvert.ByteArrayToHexString(package);
                        //移除该8个字节
                        this.comBuffer.RemoveRange(0, 8);

                        //检测校验位是否正确，不正确则跳过
                        if (!TypeConvert.CheckCRC(hexStr))
                        {
                            continue;
                        }
                        this.comDataQueue.Enqueue(hexStr);
                        
                        this.lastComData = hexStr;
                        this.lastComDataTime = DateTime.Now;
                    }
                    else
                    {
                        //数据头不正确，清除
                        this.comBuffer.RemoveAt(0);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }


        void scannerPort_1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = scannerPort_1.BytesToRead;
                byte[] buf = new byte[n];
                scannerPort_1.Read(buf, 0, n);
                scannerPortReadHandle(buf, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        void scannerPort_2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = scannerPort_2.BytesToRead;
                byte[] buf = new byte[n];
                scannerPort_2.Read(buf, 0, n);
                scannerPortReadHandle(buf, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 有两个扫描模组，重构出一个公共的数据读取接口
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="scannerPortNo"></param>
        private void scannerPortReadHandle(byte[] buf, int scannerPortNo)
        {
            string hexStr = TypeConvert.ByteArrayToHexString(buf);
            string barcode = System.Text.Encoding.Default.GetString(buf);
            if (barcode.EndsWith("\r\n"))
            {
                //以回车和换行结尾，表示合法数据
                barcode = barcode.Replace("\r\n", "");

                if (!boxNoList.Contains(barcode))
                {
                    boxNoList.Enqueue(barcode);
                }
            }
        }
    }
}
