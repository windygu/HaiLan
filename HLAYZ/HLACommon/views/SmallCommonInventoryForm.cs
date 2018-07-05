using DMSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xindeco.Device;
using Xindeco.Device.Model;
using ThingMagic;
using System.IO.Ports;

namespace HLACommon.Views
{
    public partial class SmallCommonInventoryForm : MetroForm
    {
        #region 通用属性
        M6eReader mReader = new M6eReader("192.168.1.21");
        ModbusRtuBase mModbus = new ModbusRtuBase();
        DateTime mDtStart = DateTime.Now;

        public BarcodeDevice barcode1 = null;
        public BarcodeDevice barcode2 = null;
        public Queue<string> boxNoList = new Queue<string>();
        public CheckResult checkResult = new CheckResult();
        public List<string> epcList = new List<string>();
        #endregion
        public List<TagDetailInfo> tagDetailList = new List<TagDetailInfo>();
        public int errorEpcNumber = 0, mainEpcNumber = 0, addEpcNumber = 0;
        public List<HLATagInfo> hlaTagList = null;
        public List<MaterialInfo> materialList = null;
        public SmallCommonInventoryForm()
        {
            InitializeComponent();
        }

        public bool initPLC(string port = "com1")
        {
            bool re = false;
            try
            {
                if (mModbus.OpenPort(port))
                {
                    //软件准备
                    mModbus.WriteDO(5, false);
                    Thread.Sleep(500);
                    byte[] a = mModbus.ReadDIs();
                    if (a == null || a[4] == 0)
                    {

                    }
                    else
                    {
                        //软件启动
                        mModbus.WriteDO(3, true);
                        Thread th = new Thread(GetDIStatus);
                        th.IsBackground = true;
                        th.Start();
                        re = true;
                    }
                }
            }
            catch(Exception)
            {
                re = false;
            }
            return re;
        }
        public bool initReader()
        {
            bool re = false;
            try
            {
                mReader.Connect();
                if (mReader.Connected)
                {
                    mReader.ReaderConfig();
                    mReader.TagsReportedEvent += reader_TagsReportedEvent;
                    re = true;
                }
            }
            catch(Exception)
            {
                re = false;
            }
            return re;
        }

        public void initBarcode()
        {
            try
            {
                barcode1 = new BarcodeDevice(CConfig.mScannerPort_1);
                barcode2 = new BarcodeDevice(CConfig.mScannerPort_2);

                barcode1.OnBarcodeReported += OnBarcodeReported;
                barcode1.Connect();
                barcode2.OnBarcodeReported += OnBarcodeReported;
                barcode2.Connect();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void deInit()
        {
            try
            {
                mReader.Stop();
                mReader.Disconnect();
                mModbus.WriteDO(4, false);

                barcode1.OnBarcodeReported -= OnBarcodeReported;
                barcode1.Disconnect();
                barcode2.OnBarcodeReported -= OnBarcodeReported;
                barcode2.Disconnect();
            }
            catch(Exception)
            { }

        }

        public void setInventoryRe(int re)
        {
            if (re == 1)
            {
                //ok
                mModbus.WriteDO(1, true);
            }
            else if(re == 3)
            {
                mModbus.WriteDO(2, true);
            }
        }
        void GetDIStatus()
        {
            while (mModbus.Connected)
            {
                byte[] a = mModbus.ReadDIs();
                if (a == null)
                {
                    Thread.Sleep(100);
                    continue;
                }
                //开始读
                if (a[0] == 1)
                {
                    mDtStart = DateTime.Now;
                    epcList.Clear();
                    StartInventory();
                    mReader.Start();
                    while ((DateTime.Now - mDtStart).TotalMilliseconds <= CConfig.mDelayTime)
                    {
                        Thread.Sleep(100);
                    }
                    mReader.Stop();
                    StopInventory();
                }
                //硬件启动
                else if (a[2] == 1)
                {
                }
                //硬件停止
                else if (a[3] == 1)
                {
                }
                else
                {
                }

                Thread.Sleep(100);
            }
        }


        void reader_TagsReportedEvent(string tagStr)
        {
            if (string.IsNullOrEmpty(tagStr)) return;
            if (!epcList.Contains(tagStr))
            {
                mDtStart = DateTime.Now;
                epcList.Add(tagStr);

                TagDetailInfo tag = GetTagDetailInfoByEpc(tagStr);
                if (tag != null)   //合法EPC
                {
                    tagDetailList.Add(tag);
                    if (!tag.IsAddEpc)   //主条码
                        mainEpcNumber++;
                    else
                        addEpcNumber++;
                }
                else
                {
                    //累加非法EPC数量
                    errorEpcNumber++;
                }
                UpdateView();
            }

        }
        private void OnBarcodeReported(string barcode)
        {
            if (!boxNoList.Contains(barcode))
                boxNoList.Enqueue(barcode);
        }


        public virtual CheckResult CheckData()
        {
            CheckResult result = new CheckResult();
            if (errorEpcNumber > 0)
            {
                result.UpdateMessage(CErrorMsg.EPC_WEI_ZHU_CE);
                result.InventoryResult = false;
            }
            if (mainEpcNumber != addEpcNumber && addEpcNumber > 0)
            {
                result.UpdateMessage(CErrorMsg.TWO_NUMBER_ERROR);
                result.InventoryResult = false;
            }
            if (boxNoList.Count > 0)
            {
                boxNoList.Clear();
                result.UpdateMessage(CErrorMsg.XIANG_MA_BU_YI_ZHI);
                result.InventoryResult = false;
            }
            if (epcList.Count == 0)
            {
                result.UpdateMessage(CErrorMsg.WEI_SAO_DAO_EPC);
                result.InventoryResult = false;
            }

            return result;
        }


        public virtual void UpdateView()
        {
        }
        
        public TagDetailInfo GetTagDetailInfoByEpc(string epc)
        {
            if (string.IsNullOrEmpty(epc) || epc.Length < 20)
                return null;
            string rfidEpc = epc.Substring(0, 14) + "000000";
            string rfidAddEpc = rfidEpc.Substring(0, 14);
            if (hlaTagList == null || materialList == null)
                return null;
            List<HLATagInfo> tags = hlaTagList.FindAll(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc);
            if (tags == null || tags.Count == 0)
                return null;
            else
            {
                HLATagInfo tag = tags.First();
                MaterialInfo mater = materialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
                if (mater == null)
                    return null;
                else
                {
                    TagDetailInfo item = new TagDetailInfo();
                    item.EPC = epc;
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = string.IsNullOrEmpty(tag.RFID_ADD_EPC) ? "" : tag.RFID_ADD_EPC;
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.BARCD_ADD = tag.BARCD_ADD;
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.ZCOLSN_WFG = mater.ZCOLSN_WFG;
                    item.PXQTY = mater.PXQTY;
                    item.PXQTY_FH = mater.PXQTY_FH;
                    item.PACKMAT = mater.PXMAT;
                    item.PACKMAT_FH = mater.PXMAT_FH;
                    item.PUT_STRA = mater.PUT_STRA;

                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    item.LIFNRS = new List<string>();
                    foreach(HLATagInfo t in tags)
                    {
                        if(!string.IsNullOrEmpty(t.LIFNR))
                        {
                            if(!item.LIFNRS.Contains(t.LIFNR))
                            {
                                item.LIFNRS.Add(t.LIFNR);
                            }
                        }
                    }
                    return item;
                }
            }
        }

        ProcessDialog pd = new ProcessDialog();
        public virtual void ShowLoading(string message)
        {
            Invoke(new Action(() => {
                pd.Show();
                metroPanel1.Show();
                lblText.Text = message;
            }));
            
        }

        public virtual void HideLoading()
        {

            Invoke(new Action(() => {
                pd.Hide();
                metroPanel1.Hide();
                lblText.Text = "";
            }));
        }


        private void SmallCommonInventoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            deInit();
        }

        public virtual void StartInventory()
        {
            throw new NotImplementedException();
        }

        public virtual void StopInventory()
        {
            throw new NotImplementedException();
        }
       

    }

    public class ModbusRtuBase
    {

        SerialPort serialPort;
        object obj = new object();

        public bool Connected = false;
        public bool OpenPort(string com)
        {
            try
            {
                serialPort = new SerialPort();
                serialPort.PortName = com;
                serialPort.BaudRate = 19200;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.Even;
                serialPort.Open();

                Connected = true;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ClosePort()
        {
            bool ret = false;

            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    Connected = false;
                    ret = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ret;
        }

        public byte[] ReadDIs()
        {
            lock (obj)
            {
                try
                {
                    if (serialPort.IsOpen)
                    {
                        //01 02 00 96 00 08 99 E0 //读8个DI
                        byte[] cmd = new byte[] { 0x01, 0x02, 0x00, 0x96, 0x00, 0x08, 0x99, 0xE0 };

                        serialPort.Write(cmd, 0, cmd.Length);

                        Thread.Sleep(30);
                        byte[] response = new byte[20];
                        int responseLen = serialPort.Read(response, 0, response.Length);

                        //Console.Write("接收数据:");
                        //for (int i = 0; i < responseLen; i++)
                        //{
                        //    Console.Write(response[i].ToString("X2") + " ");
                        //}
                        //Console.WriteLine();

                        if (CheckResponse(response, responseLen, 0x02))
                        {
                            return GetDIs(response[3]);
                        }

                    }
                }
                catch (Exception)
                {

                    //throw;
                }
                return null;
            }
        }

        public void WriteDOs(byte[] values)
        {
            //01 0F 00 20 00 08 01 00 7F 52
            lock (obj)
            {
                if (serialPort.IsOpen)
                {
                    byte[] cmd = new byte[10] { 0x01, 0x0f, 0, 0x20, 0, 8, 1, 0, 0, 0 };

                    string str = "";
                    for (int i = values.Length - 1; i >= 0; i--)
                    {
                        str += values[i];
                    }
                    cmd[7] = System.Convert.ToByte(str, 2);

                    byte[] a = new byte[8];
                    Array.Copy(cmd, a, 8);
                    byte[] crc = CRC16_C(a);

                    cmd[8] = crc[0];
                    cmd[9] = crc[1];

                    Console.Write("发送数据:");
                    for (int i = 0; i < cmd.Length; i++)
                    {
                        Console.Write(cmd[i].ToString("X2") + " ");
                    }
                    Console.WriteLine();

                    serialPort.Write(cmd, 0, cmd.Length);
                    Thread.Sleep(30);
                    byte[] response = new byte[20];
                    int responseLen = serialPort.Read(response, 0, response.Length);

                    Console.Write("接收数据:");
                    for (int i = 0; i < responseLen; i++)
                    {
                        Console.Write(response[i].ToString("X2") + " ");
                    }
                    Console.WriteLine();

                    if (CheckResponse(response, responseLen, 0x0F))
                    {

                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="reset">是否需要复位</param>
        public void WriteDO(byte channel, bool reset)
        {
            WrieDo(channel, 1);

            if (reset)
            {
                Thread.Sleep(100);

                WrieDo(channel, 0);
            }
        }

        private void WrieDo(byte channel, byte value)
        {
            //01 0F 00 20 00 08 01 00 7F 52
            lock (obj)
            {
                if (serialPort.IsOpen)
                {
                    byte[] cmd = new byte[8] { 0x01, 0x05, 0, Convert.ToByte(0x20 + channel - 1), 0, 0, 0, 0 };

                    if (value == 1)
                    {
                        cmd[4] = 0xff;
                    }


                    byte[] a = new byte[6];
                    Array.Copy(cmd, a, 6);
                    byte[] crc = CRC16_C(a);

                    cmd[6] = crc[0];
                    cmd[7] = crc[1];

                    Console.Write("发送数据:");
                    for (int i = 0; i < cmd.Length; i++)
                    {
                        Console.Write(cmd[i].ToString("X2") + " ");
                    }
                    Console.WriteLine();

                    serialPort.Write(cmd, 0, cmd.Length);
                    Thread.Sleep(30);
                    byte[] response = new byte[20];
                    int responseLen = serialPort.Read(response, 0, response.Length);

                    Console.Write("接收数据:");
                    for (int i = 0; i < responseLen; i++)
                    {
                        Console.Write(response[i].ToString("X2") + " ");
                    }
                    Console.WriteLine();

                    if (CheckResponse(response, responseLen, 0x0F))
                    {

                    }
                }
            }
        }
        protected bool CheckResponse(byte[] buff, int len, byte functionCode)
        {
            byte[] a = new byte[len - 2];
            Array.Copy(buff, a, len - 2);
            byte[] crc = CRC16_C(a);
            if (buff[1] == functionCode && crc[0] == buff[len - 2] && crc[1] == buff[len - 1])
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算CRC校验码
        /// Cyclic Redundancy Check 循环冗余校验码
        /// 是数据通信领域中最常用的一种差错校验码
        /// 特征是信息字段和校验字段的长度可以任意选定
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected byte[] CRC16_C(byte[] data)
        {
            byte num = 0xff;
            byte num2 = 0xff;

            byte num3 = 1;
            byte num4 = 160;
            byte[] buffer = data;

            for (int i = 0; i < buffer.Length; i++)
            {
                //位异或运算
                num = (byte)(num ^ buffer[i]);

                for (int j = 0; j <= 7; j++)
                {
                    byte num5 = num2;
                    byte num6 = num;

                    //位右移运算
                    num2 = (byte)(num2 >> 1);
                    num = (byte)(num >> 1);

                    //位与运算
                    if ((num5 & 1) == 1)
                    {
                        //位或运算
                        num = (byte)(num | 0x80);
                    }
                    if ((num6 & 1) == 1)
                    {
                        num2 = (byte)(num2 ^ num4);
                        num = (byte)(num ^ num3);
                    }
                }
            }
            return new byte[] { num, num2 };
        }

        protected byte[] GetDIs(byte value)
        {
            byte[] data = new byte[8];
            try
            {
                string r = Convert.ToString(value, 2);
                int i = r.Length;
                while (i < 8)
                {
                    r = "0" + r;
                    i++;
                }
                for (int j = r.Length - 1; j >= 0; j--)
                {
                    data[r.Length - 1 - j] = Convert.ToByte(r[j].ToString());

                }
            }
            catch (Exception)
            {

                //throw;
            }
            return data;
        }
    }

    public class M6eReader
    {
        private ThingMagic.Reader _reader;
        protected string _ip;
        private List<string> _listAll;
        public delegate void TagsReportedHandler(string tag);
        public event TagsReportedHandler TagsReportedEvent;
        public bool Connected = false;
        string readerUri;
        public M6eReader(string ip)
        {
            _ip = ip;
            //TagsReportedEvent = e;
            try
            {
                _reader.Destroy();
            }
            catch (Exception) { }

            ThingMagic.Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader);
            readerUri = ip + ":8086";
            //Creates a Reader Object for operations on the Reader.
            _reader = ThingMagic.Reader.Create(string.Concat("tcp://", readerUri));
            _listAll = new List<string>();
        }


        public bool Connect()
        {
            try
            {
                _reader.Destroy();
            }
            catch (Exception)
            {

                //throw;
            }
            try
            {
                _reader.Connect();
                _reader.TagRead += TagsReadHandler;
                _reader.ReadException += _reader_ReadException;
                Connected = true;
                return Connected;
            }
            catch (Exception)
            {
                Connected = false;
                throw;
            }
        }

        void _reader_ReadException(object sender, ReaderExceptionEventArgs e)
        {
            //throw new NotImplementedException();
            Console.WriteLine(e.ReaderException.Message);

        }


        public void Start()
        {
            try
            {
                _listAll.Clear();
                //设置事件

                SimpleReadPlan readPlan = new SimpleReadPlan(new int[4] { 1, 2, 3, 4 }, TagProtocol.GEN2);
                _reader.ParamSet("/reader/read/plan", readPlan);
                _reader.ParamSet("/reader/radio/readPower", Convert.ToInt32(100 * 31.5));
                _reader.Connect();
                _reader.StartReading();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        //北京加密 天津加密 不加密 湖南加密

        public void TagsReadHandler(object sender, TagReadDataEventArgs e)
        {
            //去掉每个单位空一格
            if (e.TagReadData.EpcString.Length != 24)
            {
                return;
            }

            //SSEncryptor.SSEncrypt enc2 = new SSEncryptor.SSEncrypt(22, 24, "");
            //string epc = enc2.Decryptor(e.TagReadData.EpcString);
            string epc = e.TagReadData.EpcString;
            if (!string.IsNullOrWhiteSpace(epc))
            {
                if (TagsReportedEvent != null)
                {
                    TagsReportedEvent(epc);
                }
            }
        }
        public void Stop()
        {
            try
            {
                _reader.StopReading();
            }
            catch (Exception)
            {
                _reader = ThingMagic.Reader.Create(string.Concat("tcp://", readerUri));
                Connect();
            }

        }

        public void Disconnect()
        {
            try
            {
                _reader.Destroy();
                _reader.Dispose();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        public bool ReaderConfig()
        {
            try
            {
                _reader.ParamSet("/reader/region/id", Reader.Region.NA);
                _reader.ParamSet("/reader/gen2/BLF", Gen2.LinkFrequency.LINK320KHZ);
                _reader.ParamSet("/reader/gen2/tari", Gen2.Tari.TARI_6_25US);
                _reader.ParamSet("/reader/gen2/session", Gen2.Session.S1);
                _reader.ParamSet("/reader/gen2/target", Gen2.Target.A);
                _reader.ParamSet("/reader/gen2/tagEncoding", Gen2.TagEncoding.FM0);
                _reader.ParamSet("/reader/gen2/q", new Gen2.DynamicQ());



                return true;
            }
            catch (Exception ex)
            {
                //InputStreamIOs.logs.Write("M6e", "配置阅读器失败", "ip: [" + Ip + "]" + ex.Message);
                throw new Exception("M6e阅读器配置错误:" + ex.Message);

            }

        }

        public IList<int> GetSelectedAntennaList()
        {
            bool _enableCheck = true;
            ((SerialReader)_reader).CmdSetReaderConfiguration(ThingMagic.SerialReader.Configuration.SAFETY_ANTENNA_CHECK, _enableCheck);
            IList<int> detectedAntennas = (IList<int>)_reader.ParamGet("/reader/antenna/portList");
            detectedAntennas = (IList<int>)_reader.ParamGet("/reader/antenna/connectedPortList");
            return detectedAntennas;
        }
    }

}
