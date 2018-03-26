using HLACommonLib.Model.PK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HLACommonLib.DAO
{
    public class PKDeliverService
    {
        public static List<PKDeliverErrorBox> GetDeliverErrorBoxByHu(string hu)
        {
            string sql = string.Format(@"SELECT * FROM dbo.DeliverErrorBox WHERE BOXGUID = (SELECT TOP 1 BOXGUID FROM dbo.DeliverErrorBox WHERE HU = '{0}' ORDER BY CreateTime DESC)", hu);
            DataTable dt = DBHelper.GetTable(sql, false);
            List<PKDeliverErrorBox> result = null;
            if(dt?.Rows.Count>0)
            {
                result = new List<PKDeliverErrorBox>();
                foreach(DataRow row in dt.Rows)
                {
                    result.Add(PKDeliverErrorBox.BuildPKDeliverErrorBox(row));
                }
            }

            return result;
        }
    }
}
