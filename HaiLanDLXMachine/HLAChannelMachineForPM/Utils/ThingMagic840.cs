using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Codetag.Rfid.ClassLibrary;
using ThingMagic;

namespace HLAChannelMachine.Utils
{
    public class ThingMagic840 : IUHFReader
    {
        private RfidReader rfidReader = new RfidReader();
        private SynchronizationContext syncContext = null;

        public ThingMagic840()
        {
            rfidReader.OnTagsReported += rfidReader_OnTagsReported;
        }

        void rfidReader_OnTagsReported(object sender, TagsReportedEventArgs e)
        {
            if (e != null)
            {
                HLACommonLib.Model.TagInfo taginfo = new HLACommonLib.Model.TagInfo();
                taginfo.Epc = e.Epc;
                if (!string.IsNullOrEmpty(taginfo.Epc))
                    taginfo.Epc = taginfo.Epc.ToUpper();
                taginfo.Antenna = (int)e.Antenna;
                if (!string.IsNullOrEmpty(e.Rssi))
                    taginfo.Rssi = double.Parse(e.Rssi.Replace("dBm", ""));
                if (OnTagReported != null)
                    OnTagReported(taginfo);
            }
        }

        public List<string> GetDeviceList()
        {
            throw new NotImplementedException();
        }

        public bool AutoConnect()
        {
            throw new NotImplementedException();
        }

        public bool Connect(string portStr, SynchronizationContext context)
        {
            try
            {
                syncContext = context;
                OpenResult result = rfidReader.OpenReader(portStr, 2048, context,"M6E");
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

        public bool SetParameter(HLACommonLib.ReaderConfig config)
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

        public string[] Query()
        {
            throw new NotImplementedException();
        }

        public bool StartInventory(int interval, int tidLength)
        {
            try
            {
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

        public event TagReportedHandler OnTagReported;

        public bool WriteEpc(string oldEpc, string newEpc, string pc, string password)
        {
            throw new NotImplementedException();
        }

        public bool WriteAccessPassword(string epc, string password)
        {
            throw new NotImplementedException();
        }

        public bool ProtectAccessPassword(string epc, string password)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
