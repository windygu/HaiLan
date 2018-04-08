using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLACommonLib.Model;
using ThingMagic;

namespace HLADeliverChannelMachine.Utils
{
    public class FUM6E : IUHFReader
    {
        private Reader reader = null;
        public FUM6E()
        {
            Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader); //注册TCP     
        }

        public List<string> GetDeviceList()
        {
            throw new NotImplementedException();
        }

        public bool AutoConnect()
        {
            throw new NotImplementedException();
        }

        public bool Connect(string portStr, System.Threading.SynchronizationContext context)
        {
            if (reader == null)
            {
                reader = Reader.Create(string.Format(@"tcp://{0}:8086", portStr));
                
            }
            reader.Connect();
            return true;
        }

        private object objLock = new object();
        void reader_TagRead(object sender, TagReadDataEventArgs e)
        {
            lock (objLock)
            {
                TagInfo taginfo = new TagInfo();
                taginfo.Epc = e.TagReadData.EpcString.Replace(" ", "");
                if (!string.IsNullOrEmpty(taginfo.Epc))
                    taginfo.Epc = taginfo.Epc.ToUpper();
                taginfo.Antenna = e.TagReadData.Antenna;
                taginfo.Rssi = e.TagReadData.Rssi;
                if (OnTagReported != null)
                    OnTagReported(taginfo);
            }
        }

        public bool Disconnect()
        {
            if (reader != null) reader.Destroy();
            return true;
        }

        public bool SetParameter(HLACommonLib.ReaderConfig config)
        {
            if (reader == null) return false;
            if (Reader.Region.UNSPEC == (Reader.Region)reader.ParamGet("/reader/region/id"))
            {
                Reader.Region[] supportedRegions = (Reader.Region[])reader.ParamGet("/reader/region/supportedRegions");
                if (supportedRegions.Length < 1)
                {
                    throw new FAULT_INVALID_REGION_Exception();
                }
                reader.ParamSet("/reader/region/id", supportedRegions[0]);
            }
            int[] antennaList = { 1 };

            //if (config.UseAntenna1)
            //    antennaList[0] = 1;
            //if (config.UseAntenna2)
            //    antennaList[1] = 2;
            //if (config.UseAntenna3)
            //    antennaList[2] = 3;
            //if (config.UseAntenna4)
            //    antennaList[3] = 4;

            SimpleReadPlan plan = new SimpleReadPlan(antennaList, TagProtocol.GEN2, null, null, 1000);

            // Set the created readplan
            reader.ParamSet("/reader/read/plan", plan);
            int readerpower = (int)config.AntennaPower1 * 100;
            reader.ParamSet("/reader/radio/readPower", readerpower);

            reader.TagRead += reader_TagRead;
            return true;
        }

        public string[] Query()
        {
            throw new NotImplementedException();
        }

        public bool StartInventory(int interval, int tidLength)
        {
            if (reader == null) return false;
            reader.StartReading();
            return true;
        }

        public bool StopInventory()
        {
            if (reader == null) return false;
            reader.StopReading();
            return true;
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
