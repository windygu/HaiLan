using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HLACommonLib.DAO
{
    public class LogService
    {
        public static void Log(string device,string msg)
        {
            new Thread(new ThreadStart(() => {
                string sql = string.Format(@"INSERT INTO dbo.DeviceLog( DeviceNO, [Log] ) VALUES  ( '{0}', '{1}')", device, msg);
                DBHelper.ExecuteNonQuery(sql);
            })).Start();
        }
    }
}
