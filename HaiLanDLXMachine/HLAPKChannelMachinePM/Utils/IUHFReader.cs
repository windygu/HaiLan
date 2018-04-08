using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HLACommonLib;
using HLACommonLib.Model;

namespace HLADeliverChannelMachine.Utils
{
    public delegate void TagReportedHandler(TagInfo taginfo);

    public interface IUHFReader
    {
        List<string> GetDeviceList();

        bool AutoConnect();

        bool Connect(string portStr, SynchronizationContext context);

        bool Disconnect();

        bool SetParameter(ReaderConfig config);

        string[] Query();

        bool StartInventory(int interval, int tidLength);

        bool StopInventory();

        event TagReportedHandler OnTagReported;

        bool WriteEpc(string oldEpc, string newEpc, string pc, string password);

        bool WriteAccessPassword(string epc, string password);

        bool ProtectAccessPassword(string epc, string password);

        bool ProtectKillPassword(string epc, string password);

        bool ProtectEpc(string epc, string password);

        bool ProtectUser(string epc, string password);
        string ReadTID();
    }
}
