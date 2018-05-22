using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLAChannelMachine.Utils
{
    public class TypeConvert
    {
        /// <summary>
        /// 16进制字符串转换为AscII码
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static string HexString2AscII(string hexStr)
        {
            byte[] ascBytes = HexStringToByteArray(hexStr);
            string ascStr = Encoding.ASCII.GetString(ascBytes);

            return ascStr;
        }

        /// <summary>
        /// AscII码转换为16进制字符串
        /// </summary>
        /// <param name="asciiStr"></param>
        /// <returns></returns>
        public static string AscII2HexString(string asciiStr)
        {
            byte[] ascBytes = Encoding.ASCII.GetBytes(asciiStr);
            string hexStr = ByteArrayToHexString(ascBytes);

            return hexStr;
        }

        /// <summary>
        /// 字节数组转换为16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 16进制字符串转换为字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }


        /// <summary>
        /// 16进制字符串转化为二进制字符串
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static string HexString2BinaryString(string hexStr)
        {
            StringBuilder sb = new StringBuilder();
            if (hexStr.Length % 2 != 0)
                hexStr += "0";
            int num = hexStr.Length / 2;
            for (int i = 0; i < num; i++)
            {
                string tempStr = hexStr.Substring(2 * i, 2);
                byte bt = Convert.ToByte(tempStr, 16);
                string binaryStr = Convert.ToString(bt, 2).PadLeft(8, '0');
                sb.Append(binaryStr + " ");
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// 二进制字符串转化为十六进制字符串
        /// </summary>
        /// <param name="binaryStr"></param>
        /// <returns></returns>
        public static string BinaryString2HexString(string binaryStr)
        {
            StringBuilder sb = new StringBuilder();
            if (binaryStr.Length % 8 != 0)
                binaryStr = binaryStr.PadRight(8, '0');
            int num = binaryStr.Length / 8;
            for (int i = 0; i < num; i++)
            {
                string tempStr = binaryStr.Substring(8 * i, 8);
                byte bt = Convert.ToByte(tempStr, 2);
                string hexStr = Convert.ToString(bt, 16).PadLeft(2, '0');
                sb.Append(hexStr);
            }

            return sb.ToString().ToUpper().Trim();
        }

        /// <summary>
        /// 16进制转十进制
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static long HexStringToDecimal(string hexString)
        {
            long value = Convert.ToInt64(hexString, 16);
            return value;
        }

        public static string DecimalToHexString(long value)
        {
            string hexString = string.Format("{0:X}", value);

            return hexString;
        }

        /// <summary>
        /// 中文字符串转16进制字符串
        /// </summary>
        /// <param name="chnStr"></param>
        /// <returns></returns>
        public static string ChineseStringToHexString(string chnStr)
        {
            byte[] temp = Encoding.Default.GetBytes(chnStr);

            if (temp.Length > 0)
            {
                string hexStr = ByteArrayToHexString(temp);

                return hexStr;
            }

            return null;
        }

        /// <summary>
        /// 16进制字符串转换为中文字符串
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static string HexStringToChineseString(string hexStr)
        {
            byte[] temp = HexStringToByteArray(hexStr);

            if (temp.Length > 0)
            {
                return Encoding.Default.GetString(temp).Trim();
            }

            return null;
        }

        public static byte[] CRC16(byte[] data)
        {
            byte[] returnVal = new byte[2];
            int i, Flag, crcValue;
            crcValue = 0xffff;
            for (i = 0; i < data.Length; i++)
            {
                crcValue = (crcValue ^ data[i]);//每一个数据与CRC寄存器进行异或
                for (Flag = 0; Flag <= 7; Flag++)
                {
                    if ((crcValue & 0x01) == 0x01)
                    {
                        crcValue = (crcValue >> 1) ^ 0xA001;
                    }
                    else
                    {
                        crcValue = crcValue >> 1;
                    }
                }
            }

            returnVal[0] = (byte)(crcValue & 0xff);//CRC低位
            returnVal[1] = (byte)((crcValue >> 8) & 0xff);//CRC高位
            return returnVal;
        }

        public static string HexStringToCRCString(string hexStr)
        {
            byte[] ss = TypeConvert.HexStringToByteArray(hexStr);
            byte[] tt = TypeConvert.CRC16(ss);
            string hex = TypeConvert.ByteArrayToHexString(tt);

            return hex;
        }

        public static bool CheckCRC(string hexStr)
        {
            if (hexStr.Length != 16)
                return false;

            byte[] ss = TypeConvert.HexStringToByteArray(hexStr.Substring(0, 12));
            byte[] tt = TypeConvert.CRC16(ss);
            string hex = TypeConvert.ByteArrayToHexString(tt);

            if (hexStr.EndsWith(hex))
                return true;
            else
                return false;
        }
    }
}
