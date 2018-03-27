using HLACommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HLAEBReceiveChannelMachine.Utils
{
    public class PlcSocketHelper
    {
        static object obj = new object();
        private static PlcSocketHelper instance = null;
        private Socket server = null;
        private bool connected = false;
        public static PlcSocketHelper Instance()
        {
            lock(obj)
            {
                if (instance == null)
                {
                    instance = new PlcSocketHelper();
                }
            }
            return instance;
        }

        /// <summary>
        /// 连接socket
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Connect(string ip, int port)
        {
            if (server != null && server.Connected)
                server.Disconnect(true);
            try
            {
                IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Parse(ip), port);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ipendpoint);
                return true;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(string.Format("connect plc socket error:{0}\r\n{1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        /// <summary>
        /// 开始接收数据
        /// </summary>
        /// <param name="receiveaction">数据接收成功后执行的回调</param>
        public void Receive(Action<string> receiveaction)
        {
            Byte[] msg = new byte[1024];
            server.BeginReceive(msg, 0, msg.Length, SocketFlags.None, ac =>
            {
                server.EndReceive(ac);
                if (receiveaction != null)
                    receiveaction(Encoding.UTF8.GetString(msg).Trim('\0', ' '));
                Receive(receiveaction);
            }, null);
                
        }

        /// <summary>
        /// 向服务器发送数据
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            if (server.Connected == false)
            {
                throw new Exception("还没有建立连接, 不能发送消息");
            }
            Byte[] msg = Encoding.UTF8.GetBytes(message);
            server.BeginSend(msg, 0, msg.Length, SocketFlags.None,
                ar => {

                }, null);
        }
    }
}
