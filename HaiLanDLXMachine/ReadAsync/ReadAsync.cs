using System;
using System.Collections.Generic;
using System.Text;
// for Thread.Sleep
using System.Threading;

// Reference the API
using ThingMagic;

namespace ReadAsync
{
    /// <summary>
    /// Sample program that reads tags in the background and prints the
    /// tags found.
    /// </summary>
    class Program
    {
        static void Usage()
        {
            Console.WriteLine(String.Join("\r\n", new string[] {
                    "Usage: "+"Please provide valid arguments, such as:",
                    "tmr:///com4 or tmr:///com4 --ant 1,2",
                    "tmr://my-reader.example.com or tmr://my-reader.example.com --ant 1,2"
            }));
            Environment.Exit(1);
        }
        static void Main(string[] args)
        {
            // Program setup
            int[] antennaList = {1,2,3,4}; //选择天线1,2,3,4

            try
            {
                Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader);

                // Create Reader object, connecting to physical device.
                // Wrap reader in a "using" block to get automatic
                // reader shutdown (using IDisposable interface).
                using (Reader r = Reader.Create("tcp://192.168.8.166:8086"))  //创建对象
                {
                    r.Connect();  //连接读写器

                    r.ParamSet("/reader/region/id", Reader.Region.NA);

                    // Create a simplereadplan which uses the antenna list created above
                    SimpleReadPlan plan = new SimpleReadPlan(antennaList, TagProtocol.GEN2,null,null,1000); //设置天线和协议
                    // Set the created readplan
                    r.ParamSet("/reader/read/plan", plan);

                    //场景配置,用于隧道机
                    Gen2.LinkFrequency blf = Gen2.LinkFrequency.LINK320KHZ;
                    r.ParamSet("/reader/gen2/BLF", blf);

                    Gen2.Tari tari = Gen2.Tari.TARI_6_25US;
                    r.ParamSet("/reader/gen2/tari", tari);

                    Gen2.TagEncoding tagncoding = Gen2.TagEncoding.FM0;
                    r.ParamSet("/reader/gen2/tagEncoding", tagncoding);

                    Gen2.Session session = Gen2.Session.S1;
                    r.ParamSet("/reader/gen2/session", session);

                    Gen2.Target target = Gen2.Target.A;
                    r.ParamSet("/reader/gen2/target", target);

                    //500~3150
                    r.ParamSet("/reader/radio/readPower", 3000);

                    // 注册标签数据回调方法
                    r.TagRead += delegate(Object sender, TagReadDataEventArgs e)
                    {
                        Console.WriteLine("Background read: " + e.TagReadData);
                    };
                    //创建异常回调方法
                    r.ReadException += new EventHandler<ReaderExceptionEventArgs>(r_ReadException);

                    // 开始读卡
                    r.StartReading();

                    Console.WriteLine("\r\n<Do other work here>\r\n");
                    Thread.Sleep(5000);
                    Console.WriteLine("\r\n<Do other work here>\r\n");
                    Thread.Sleep(10000);
                    //结束读卡
                    r.StopReading();
                }
            }
            catch (ReaderException re)
            {
                Console.WriteLine("Error: " + re.Message);
                Console.Out.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void r_ReadException(object sender, ReaderExceptionEventArgs e)
        {
            Console.WriteLine("Error: " + e.ReaderException.Message);
        }

    }
}
