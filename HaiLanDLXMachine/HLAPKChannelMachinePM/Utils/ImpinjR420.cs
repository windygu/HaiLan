using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLACommonLib;
using HLACommonLib.Model;
using Impinj.OctaneSdk;

namespace HLADeliverChannelMachine.Utils
{
    public class ImpinjR420 : IUHFReader
    {
        private ImpinjReader rfidReader = new ImpinjReader();
        public event TagReportedHandler OnTagReported;

        public ImpinjR420()
        {
            rfidReader.TagsReported += new ImpinjReader.TagsReportedHandler(rfidReader_TagsReported);
        }

        void rfidReader_TagsReported(ImpinjReader reader, TagReport report)
        {
            foreach (Tag tag in report.Tags)
            {
                Console.WriteLine("Reader saw {0} on ant#{1}",
                    tag.Epc.ToString(), tag.AntennaPortNumber);
                TagInfo taginfo = new TagInfo();
                taginfo.Epc = tag.Epc.ToString().Replace(" ", "");
                if (!string.IsNullOrEmpty(taginfo.Epc))
                    taginfo.Epc = taginfo.Epc.ToUpper();
                taginfo.Antenna = tag.AntennaPortNumber;
                taginfo.Rssi = tag.PeakRssiInDbm;
                if (OnTagReported != null)
                    OnTagReported(taginfo);
            }
        }

        public List<string> GetDeviceList()
        {
            return null;
        }

        public bool AutoConnect()
        {
            throw new NotImplementedException();
        }

        public bool Connect(string portStr)
        {
            try
            {
                rfidReader.Connect(portStr);



                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        public bool Disconnect()
        {
            try
            {
                rfidReader.Disconnect();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        public bool SetParameter(ReaderConfig config)
        {
            try
            {
                Settings settings = rfidReader.QueryDefaultSettings();
                settings.Report.Mode = ReportMode.Individual;
                settings.Report.IncludeAntennaPortNumber = true;
                settings.Report.IncludePeakRssi = true;
                switch (config.SearchMode)
                {
                    case 1:
                        settings.SearchMode = SearchMode.DualTarget;
                        break;
                    case 2:
                        settings.SearchMode = SearchMode.SingleTarget;
                        break;
                    case 3:
                        //settings.SearchMode = SearchMode.SingleTargetWithSuppression;
                        break;
                    default:
                        break;
                }
                //settings.SearchMode = SearchMode.SingleTarget;
                settings.Session = config.Session;

                //设置功率
                settings.Antennas.GetAntenna(1).IsEnabled = config.UseAntenna1;
                settings.Antennas.GetAntenna(2).IsEnabled = config.UseAntenna2;
                settings.Antennas.GetAntenna(3).IsEnabled = config.UseAntenna3;
                settings.Antennas.GetAntenna(4).IsEnabled = config.UseAntenna4;
                settings.Antennas.GetAntenna(1).TxPowerInDbm = config.AntennaPower1;
                settings.Antennas.GetAntenna(2).TxPowerInDbm = config.AntennaPower2;
                settings.Antennas.GetAntenna(3).TxPowerInDbm = config.AntennaPower3;
                settings.Antennas.GetAntenna(4).TxPowerInDbm = config.AntennaPower4;

                if (SysConfig.IsHub == 1)
                {
                    settings.Antennas.GetAntenna(5).IsEnabled = config.UseAntenna5;
                    settings.Antennas.GetAntenna(6).IsEnabled = config.UseAntenna6;
                    settings.Antennas.GetAntenna(7).IsEnabled = config.UseAntenna7;
                    settings.Antennas.GetAntenna(8).IsEnabled = config.UseAntenna8;
                    settings.Antennas.GetAntenna(5).TxPowerInDbm = config.AntennaPower5;
                    settings.Antennas.GetAntenna(6).TxPowerInDbm = config.AntennaPower6;
                    settings.Antennas.GetAntenna(7).TxPowerInDbm = config.AntennaPower7;
                    settings.Antennas.GetAntenna(8).TxPowerInDbm = config.AntennaPower8;
                }

                rfidReader.ApplySettings(settings);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        public bool StartInventory(int interval, int tidLength)
        {
            try
            {
                rfidReader.Start();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        public bool StopInventory()
        {
            try
            {
                rfidReader.Stop();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }


        public string[] Query()
        {
            throw new NotImplementedException();
        }

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


        public bool Connect(string portStr, System.Threading.SynchronizationContext context)
        {
            try
            {
                rfidReader.Connect(portStr);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }

            return false;
        }

        public string ReadTID()
        {
            throw new NotImplementedException();
        }
    }
}
