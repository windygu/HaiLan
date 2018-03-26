using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HLACommonLib
{
    /// <summary>
    /// 验证字符串是否合法
    /// </summary>
    public class Validator
    {

        /// <summary>
        /// 检查字符串是否是合法的数字
        /// </summary>
        /// <param name="NumString"></param>
        /// <returns></returns>
        public static bool IsNumber(string NumString)
        {
            return Regex.IsMatch(NumString, "^(-?\\d+)(\\.\\d+)?$");
        }

        /// <summary>
        /// 是否是合法的整型数值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsInt(string input)
        {
            return Regex.IsMatch(input, @"\d+");
        }

        /// <summary>
        /// 是否是合法的浮点数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDecimal(string input)
        {
            if (IsInt(input))
                return true;
            return Regex.IsMatch(input, @"\d+.\d+");
        }

        /// <summary>
        /// 检查字符串是否是合法的日期
        /// </summary>
        /// <param name="DateString"></param>
        /// <returns></returns>
        public static bool IsDateTime(string dateString)
        {
            try
            {
                if (dateString == "")
                    return false;

                string regexString = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$ ";
                if (Regex.IsMatch(dateString, regexString))
                    return true;

                Convert.ToDateTime(dateString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查字符串是否是合法的BOOL
        /// </summary>
        /// <param name="AimString"></param>
        /// <returns></returns>
        public static bool IsBool(string AimString)
        {
            try
            {
                if (AimString == "")
                    return false;
                if (AimString.ToLower() != "false" && AimString.ToLower() != "true")
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
