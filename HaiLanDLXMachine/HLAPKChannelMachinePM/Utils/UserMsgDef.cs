#define   _MSG_IS_CHINESE   //是否中文版

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLADeliverChannelMachine.Utils
{
    class UserMsgDef
    {
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_ISCHINAESE = "1";
#else
        public string  _MSG_XDWL_M6E_DEMO_ISCHINAESE="0";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_MODEL = "模块型号：";
#else
        public string  _MSG_XDWL_M6E_DEMO_MODEL="Module type:";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_HARDWARE = "硬件版本：";
#else
        public string  _MSG_XDWL_M6E_DEMO_HARDWARE="Hardware version:";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_FIRMWARE = "固件版本：";
#else
        public string  _MSG_XDWL_M6E_DEMO_FIRMWARE="Firmware version:";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_CELSIUS = "度";
#else
        public string  _MSG_XDWL_M6E_DEMO_CELSIUS="Celsius";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_MODULE_TEMPERATURE = "模块温度：";
#else
        public string  _MSG_XDWL_M6E_DEMO_MODULE_TEMPERATURE="Module temperature:";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_RFMODULE_CONN_FAILURE = "射频模块连接失败!";
#else
        public string  _MSG_XDWL_M6E_DEMO_RFMODULE_CONN_FAILURE="RF module connection failure!";
#endif


#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INIT_ANTENNA = "初始化天线........";
#else
        public string  _MSG_XDWL_M6E_DEMO_INIT_ANTENNA="Initial antenna........";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INIT_BAUDRATE = "初始化波特率........";
#else
        public string _MSG_XDWL_M6E_DEMO_INIT_BAUDRATE = "Initialize the Region ........";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INIT_ANTENNA_Power = "初始化频段，天线，功率........";
#else
        public string _MSG_XDWL_M6E_DEMO_INIT_ANTENNA_Power = "Initial Frequency Region, Antenna, Power........";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INIT_GEN2 = "初始化Gen2配置........";
#else
        public string  _MSG_XDWL_M6E_DEMO_INIT_GEN2="Initial Gen2........";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INIT_FREQUENCY_ERROR = "初始化频段失败.";
#else
        public string  _MSG_XDWL_M6E_DEMO_INIT_FREQUENCY_ERROR="Initialization frequency failure.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INIT_BAUDRATE_ERROR = "初始化波特率失败.";
#else
        public string _MSG_XDWL_M6E_DEMO_INIT_BAUDRATE_ERROR = "Region initialization failed.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_NET_CONN_ERROR = "网络连接出错!";
#else
        public string  _MSG_XDWL_M6E_DEMO_NET_CONN_ERROR="Network connection error";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_PLEASE_OFF_INVENTORY = "请先关闭盘点!";
#else
        public string  _MSG_XDWL_M6E_DEMO_PLEASE_OFF_INVENTORY="Please turn off the inventory!";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_BAND_ERROR = "频段设置出错!";
#else
        public string _MSG_XDWL_M6E_DEMO_SET_BAND_ERROR = "Frequency Region setting error!";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_BAND_SUCCESS = "频段设置成功!";
#else
        public string _MSG_XDWL_M6E_DEMO_SET_BAND_SUCCESS = "Frequency Region setting Success!";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_ANTENNA_ERROR = "天线设置出错!";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_ANTENNA_ERROR="Antenna setting error!";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_ERROR = "设置失败!";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_ERROR="Set failure!";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_SUCCSSFULLY = "设置成功!";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_SUCCSSFULLY="Set successfully!";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INFO = "提示";
#else
        public string  _MSG_XDWL_M6E_DEMO_INFO="Prompt";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_STOP_INVENTORY_ERROR = "停止盘点出错，是否重新连接!";
#else
        public string  _MSG_XDWL_M6E_DEMO_STOP_INVENTORY_ERROR="To stop the inventory error, whether to re connect!";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_PLEASE_SEL_ANTENNA = "请选择天线.";
#else
        public string  _MSG_XDWL_M6E_DEMO_PLEASE_SEL_ANTENNA="Please Select antenna!";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_PLEASE_INPUT_IP = "请输入IP地址.";
#else
        public string  _MSG_XDWL_M6E_DEMO_PLEASE_INPUT_IP="Please enter the IP address.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_IP_FORMAT_ERROR = "IP地址格式不对.";
#else
        public string  _MSG_XDWL_M6E_DEMO_IP_FORMAT_ERROR="IP address format is incorrect";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_PLEAS_INPUT_MASK_ERROR = "请输入子网掩码地址.";
#else
        public string  _MSG_XDWL_M6E_DEMO_PLEAS_INPUT_MASK_ERROR="Please enter a subnet mask address";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_MASK_FORMAT_ERROR = "子网掩码地址格式不对.";
#else
        public string  _MSG_XDWL_M6E_DEMO_MASK_FORMAT_ERROR="Subnet mask address format is not correct";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_PLEAS_INPUT_GATEWAY = "请输入网关IP地址.";
#else
        public string  _MSG_XDWL_M6E_DEMO_PLEAS_INPUT_GATEWAY="Enter the gateway IP address";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_GATEWAY_FORMAT_ERROR = "网关IP地址格式不对.";
#else
        public string  _MSG_XDWL_M6E_DEMO_GATEWAY_FORMAT_ERROR="Gateway IP address format is incorrect";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_DIFFERENT_SEGMENT = "网段不同请重新输入.";
#else
        public string  _MSG_XDWL_M6E_DEMO_DIFFERENT_SEGMENT="Please re-enter the different segment";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_SUCCESS_PLEASS_5SEC = "设置成功,请等5秒后重新联接.";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_SUCCESS_PLEASS_5SEC="Set the success, please wait 5 seconds after the re connection";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_NO_RECORD = "没有记录!";
#else
        public string  _MSG_XDWL_M6E_DEMO_NO_RECORD="No record!";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_WARNING_NOT_ONE_TAG = "警告：多于一个标签.";
#else
        public string  _MSG_XDWL_M6E_DEMO_WARNING_NOT_ONE_TAG="Warning: more than one label";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_WRITE_TAG_SUCCEED = "写标签成功.";
#else
        public string  _MSG_XDWL_M6E_DEMO_WRITE_TAG_SUCCEED="Write a Tag to succeed.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_WRITE_EPC_FAILURE = "写EPC失败.";
#else
        public string  _MSG_XDWL_M6E_DEMO_WRITE_EPC_FAILURE="Write EPC failure.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_NO_TAG_ERROR = "没有标签.";
#else
        public string  _MSG_XDWL_M6E_DEMO_NO_TAG_ERROR="No Tag.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_ERROR = "错误.";
#else
        public string  _MSG_XDWL_M6E_DEMO_ERROR="ERROR";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_WRITE_SUCCEED = "写入成功.";
#else
        public string  _MSG_XDWL_M6E_DEMO_WRITE_SUCCEED="Write success.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_WRITE_FAILURE = "写入失败.";
#else
        public string  _MSG_XDWL_M6E_DEMO_WRITE_FAILURE="Write failure.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_NEW_PWS_CANNOT_EMPTY = "新密码不能为空.";
#else
        public string  _MSG_XDWL_M6E_DEMO_NEW_PWS_CANNOT_EMPTY="The new password can not be empty.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SUCCESS_SET_ACCESS_PWS = "成功设置访问密码";
#else
        public string  _MSG_XDWL_M6E_DEMO_SUCCESS_SET_ACCESS_PWS="Successfully set access password";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_PWS_ERROR = "密码错误!";
#else
        public string  _MSG_XDWL_M6E_DEMO_PWS_ERROR="Password error";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_USER_FAILED = "设置User区失败";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_USER_FAILED="Set User area failed";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_EPC_FAILED = "设置EPC区失败";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_EPC_FAILED="Set EPC area failed";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_TID_FAILED = "设置TID区失败";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_TID_FAILED="Set TID area failed";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_ACCPWD_FAILED = "设置访问密码失败";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_ACCPWD_FAILED="Set access password failed";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SET_KILLPWD_FAILED = "设置销毁密码失败";
#else
        public string  _MSG_XDWL_M6E_DEMO_SET_KILLPWD_FAILED="Set destruction password failed";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_PLEASE_SEL_LEAST_ONE = "请至少选择一项";
#else
        public string  _MSG_XDWL_M6E_DEMO_PLEASE_SEL_LEAST_ONE="Please select at least one";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_LISE_SERIAL = " 序号";
#else
        public string  _MSG_XDWL_M6E_DEMO_LISE_SERIAL="Serial";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_LISE_ANTENNA = " 天线";
#else
        public string  _MSG_XDWL_M6E_DEMO_LISE_ANTENNA="Antenna";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_LISE_COUNT = " 统计";
#else
        public string  _MSG_XDWL_M6E_DEMO_LISE_COUNT="Count";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_UP_FIRMWARE_PROGRAM = "固件程序(*.a79)|*.a79|所有文件(*.*)|*.*";
#else
        public string  _MSG_XDWL_M6E_DEMO_UP_FIRMWARE_PROGRAM="Firmware program(*.a79)|*.a79|All documents(*.*)|*.*";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_UP_SEL_FIRMWARE_PROGRAM = "请选择固件程序";
#else
        public string  _MSG_XDWL_M6E_DEMO_UP_SEL_FIRMWARE_PROGRAM="Select the firmware program";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_UP_IN_FIRMWARE_UPGRADE = "固件程序升级中，不要中途断电，否则设备将不可用.";
#else
        public string  _MSG_XDWL_M6E_DEMO_UP_IN_FIRMWARE_UPGRADE="In the firmware upgrade, do not cut off the power, otherwise the device will not be available.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_UP_UPGRADE_SUCCESS = "升级成功,请在60秒后重新连接.";
#else
        public string  _MSG_XDWL_M6E_DEMO_UP_UPGRADE_SUCCESS="Upgrade successfully, please re connect in 60 seconds.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_UP_UPGRADE_FAILED = "升级失败.";
#else
        public string  _MSG_XDWL_M6E_DEMO_UP_UPGRADE_FAILED="Upgrade failed.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INPUT_STRADD_CANNOT_BE_LESS_0 = "起始地址不能小于0.";
#else
        public string  _MSG_XDWL_M6E_DEMO_INPUT_STRADD_CANNOT_BE_LESS_0="Starting address can not be less than 0";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_INPUT_STRADD_CANNOT_BE_MORE_10 = "起始地址不能大于10.";
#else
        public string  _MSG_XDWL_M6E_DEMO_INPUT_STRADD_CANNOT_BE_MORE_10="Starting address can not be more than 10";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_LEN_CAN_NOT_BELESS_0 = "长度不能小于0.";
#else
        public string  _MSG_XDWL_M6E_DEMO_LEN_CAN_NOT_BELESS_0="Length can not be less than 0";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_LEN_CAN_NOT_BEMORE_100 = "长度不能大于100.";
#else
        public string  _MSG_XDWL_M6E_DEMO_LEN_CAN_NOT_BEMORE_100="Length can not be more than 100";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_SEND_TIME_BETWEEN_100_3250000 = "发送时间在100-3250000毫秒之间";
#else
        public string  _MSG_XDWL_M6E_DEMO_SEND_TIME_BETWEEN_100_3250000="Send time between 100-3250000";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_HEARTBEAT_TIME_BETWEEN_100_3250000 = "心跳时间在100-3250000毫秒之间";
#else
        public string  _MSG_XDWL_M6E_DEMO_HEARTBEAT_TIME_BETWEEN_100_3250000="Heartbeat time between 100-3250000";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_DEMO_LONG_SET_SUCCSS = "设置成功.在你退出本程序后，服务端才会起动。";
#else
        public string  _MSG_XDWL_M6E_DEMO_LONG_SET_SUCCSS="Set up successfully. After you exit the program, the server will start.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_SET_CONFIG_CURTIME_SUCCESS = "同步时间成功.";
#else
        public string  _MSG_XDWL_M6E_SET_CONFIG_CURTIME_SUCCESS="Time synchronization Success.";
#endif

#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_SET_CONFIG_CURTIME_FAILED = "同步时间失败.";
#else
        public string  _MSG_XDWL_M6E_SET_CONFIG_CURTIME_FAILED="Time synchronization Failed.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_GET_NO_ANTENNAS_DETECTED = "没有检测到有天线.";
#else
        public string _MSG_XDWL_M6E_GET_NO_ANTENNAS_DETECTED = "No antenna was detected.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_GET_ANTENNAS_DETECTED_SUCCESS = "天线检测成功.";
#else
        public string _MSG_XDWL_M6E_GET_ANTENNAS_DETECTED_SUCCESS = "Antenna detection success.";
#endif
#if  _MSG_IS_CHINESE
        public string _MSG_XDWL_M6E_PLEASE_ENTER_FILTER_CHAR = "请输入过滤条件字符.";
#else
        public string _MSG_XDWL_M6E_PLEASE_ENTER_FILTER_CHAR = "Please enter filter condition character.";
#endif
    }

}
