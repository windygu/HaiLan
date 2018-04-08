using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Codetag.Rfid.ClassLibrary;
using HLACommonLib;

namespace HLADeliverChannelMachine.Utils
{
    public class XDUR840 : IUHFReader
    {
        private RfidReader rfidReader = new RfidReader();
        private string readerType = "USB";
        private SynchronizationContext syncContext = null;
        public event TagReportedHandler OnTagReported;
        private CommParms TIDParms = new CommParms()
        {
            Mode = MemoryMode.TID,
            Begin = 0,
            Length = 6,
            Password = "00000000"
        };
        public XDUR840()
        {
            rfidReader.OnTagsReported += new Codetag.Rfid.ClassLibrary.RfidReader.TagsReportedEventHandler(rfidReader_OnTagsReported);
        }

        public List<string> GetDeviceList()
        {
            return null;
        }

        public bool AutoConnect()
        {
            throw new NotImplementedException();
        }

        public bool Connect(string portStr, SynchronizationContext context)
        {
            try
            {
                //OpenResult result = rfidReader.OpenReader(portStr, 2048, context);
                syncContext = context;
                OpenResult result = new OpenResult() { Success = false };
                if (portStr == "USB")
                {
                    readerType = "USB";

                    throw new Exception("当前不支持USB连接读写器的功能");
                    //ReaderInfo[] list = rfidReader.EnumReader(0);
                    //if (list != null && list.Length > 0)
                    //    result = rfidReader.OpenReader(list[0].ProductType, CommunicationMode.USB);
                }
                else if (portStr.StartsWith("COM"))
                {
                    readerType = "COM";
                    result = rfidReader.OpenReader(portStr, context, CommunicationMode.UART);
                }
                else
                {
                    readerType = "TCP";
                    result = rfidReader.OpenReader(portStr, 2048, context);
                }

                return result.Success ? true : false;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public bool Disconnect()
        {
            try
            {
                rfidReader.CloseReader();

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public bool SetParameter(ReaderConfig config)
        {
            try
            {
                uint Session = 0, Target = 0, Flip = 0;
                switch (config.SearchMode)
                {
                    case 1:
                        Flip = 1;//DualTarget
                        break;
                    case 2:
                        Flip = 0; //SingleTarget
                        break;
                    default:
                        break;
                }

                Target = 0;//Target默认为A
                Session = (uint)config.Session;

                bool result = rfidReader.SetSession(Session, Target, Flip);

                uint dBm1 = (uint)(config.AntennaPower1 * 10);
                uint dBm2 = (uint)(config.AntennaPower2 * 10);
                uint dBm3 = (uint)(config.AntennaPower3 * 10);
                uint dBm4 = (uint)(config.AntennaPower4 * 10);

                rfidReader.SetAntennaPort(0, config.UseAntenna1);
                rfidReader.SetAntennaPort(1, config.UseAntenna2);
                rfidReader.SetAntennaPort(2, config.UseAntenna3);
                rfidReader.SetAntennaPort(3, config.UseAntenna4);
                rfidReader.SetAntennaPower(dBm1, 0);
                rfidReader.SetAntennaPower(dBm2, 1);
                rfidReader.SetAntennaPower(dBm3, 2);
                rfidReader.SetAntennaPower(dBm4, 3);

                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public bool StartInventory(int interval, int tidLength)
        {
            try
            {
                if (readerType == "USB")
                    throw new Exception("当前不支持USB连接读写器的功能");
                else
                    rfidReader.StartInventory((uint)interval, 8192, false);

                return true;
            }
            catch
            {
            }

            return false;
        }

        public bool StopInventory()
        {
            try
            {
                rfidReader.StopInventory();

                return true;
            }
            catch
            { }

            return false;
        }

        private void rfidReader_OnTagsReported(object sender, TagsReportedEventArgs args)
        {
            Console.WriteLine("Reader saw {0} on ant#{1}",
                    args.Epc, args.Antenna);
            HLACommonLib.Model.TagInfo taginfo = new HLACommonLib.Model.TagInfo();
            taginfo.Epc = args.Epc;
            if (!string.IsNullOrEmpty(taginfo.Epc))
                taginfo.Epc = taginfo.Epc.ToUpper();
            taginfo.Antenna = (int)args.Antenna;
            if (!string.IsNullOrEmpty(args.Rssi))
                taginfo.Rssi = double.Parse(args.Rssi.Replace("dBm", ""));
            if (OnTagReported != null)
                OnTagReported(taginfo);
        }

        public string[] Query()
        {
            string password = "00000000";
            EpcReadResult result = rfidReader.ReadEpc(password);

            if (result.Success)
            {
                string epc = result.Epc;
                if (!string.IsNullOrEmpty(epc))
                    epc = epc.ToUpper();
                return new string[] { epc };
            }
            else
            {
                return null;
            }
        }

        public bool WriteEpc(string oldEpc, string newEpc, string pc, string password)
        {
            EpcWriteParms epcParms = new EpcWriteParms();
            epcParms.Password = password;
            epcParms.NewData = newEpc;

            if (rfidReader.WriteEpc(epcParms))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WriteAccessPassword(string epc, string password)
        {
            NormalWriteParms parms = new NormalWriteParms();
            parms.Mode = MemoryMode.RESERVED;
            parms.Begin = 2;
            parms.Length = 2;
            parms.NewData = password;
            parms.Password = "00000000";

            if (rfidReader.WriteData(parms))
                return true;
            else
                return false;
        }

        public bool ProtectAccessPassword(string epc, string password)
        {
            LockTagParms parms = new LockTagParms();
            parms.LockAccessPassword = LockMode.SECURED_WRITEABLE; //锁定访问密码
            parms.LockEpcMemory = LockMode.SECURED_WRITEABLE; //锁定epc
            parms.Password = password;

            if (rfidReader.LockTag(parms))
                return true;
            else
                return false;
        }

        public bool ProtectKillPassword(string epc, string password)
        {
            throw new NotImplementedException();
        }

        public bool ProtectEpc(string epc, string password)
        {
            throw new NotImplementedException();
        }

        public bool ProtectUser(string epc, string password)
        {
            throw new NotImplementedException();
        }
        public string ReadTID()
        {
            var result = rfidReader.ReadData(TIDParms);
            return result.Success ? result.Data : "";
        }
    }
}
