using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib.Model
{
    /// <summary>
    /// 设备基本信息表
    /// </summary>
    public class DeviceTable
    {
        /// <summary>
        /// 设备终端号
        /// </summary>
        public string EQUIP_HLA { get; set; }
        /// <summary>
        /// 终端描述
        /// </summary>
        public string EQUIP_DEC { get; set; }
        /// <summary>
        /// 终端类型 
        /// </summary>
        public string EQUIP_TPE { get; set; }
        /// <summary>
        /// 终端类型描述
        /// </summary>
        public string EQUIP_TPC { get; set; }
        /// <summary>
        /// 仓库楼层
        /// </summary>
        public string LOUCENG { get; set; }
        /// <summary>
        /// 存储类型
        /// </summary>
        public List<string> LGTYP { get; set; }
        /// <summary>
        /// 是否打印(01是02否)
        /// </summary>
        public string IS_PRINT { get; set; }
        /// <summary>
        /// 是否信用(x是''否
        /// </summary>
        public string IS_NONUSE { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string EQUIP { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
        /// <summary>
        /// 库房类型（’X’箱装’G’挂装）
        /// </summary>
        public string KF_LX { get; set; }

        public List<GxInfo> GxList { get; set; }

        public List<AuthInfo> AuthList { get; set; }
    }

    public class AuthInfo
    {
        /// <summary>
        /// 权限编码
        /// A:源存储类型
        /// B:目标存储类型
        /// C:实际存储类型
        /// D:交接楼号与入库控制标识的对应关系
        /// E:退货类型对应楼层
        /// F:楼层信息
        /// </summary>
        public string AUTH_CODE { get; set; }
        /// <summary>
        /// 权限属性
        /// </summary>
        public string AUTH_VALUE { get; set; }
        /// <summary>
        /// 权限编码描述
        /// </summary>
        public string AUTH_CODE_DES { get; set; }
        /// <summary>
        /// 权限属性描述
        /// </summary>
        public string AUTH_VALUE_DES { get; set; }
        /// <summary>
        /// 设备终端号
        /// </summary>
        public string EQUIP_HLA { get; set; }
        /// <summary>
        /// 终端描述
        /// </summary>
        public string EQUIP_DEC { get; set; }
    }
}
