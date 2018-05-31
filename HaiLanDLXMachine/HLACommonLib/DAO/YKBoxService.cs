using HLACommonLib.Model.YK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HLACommonLib.DAO
{
    public class YKBoxService
    {
        public static List<YKBoxInfo> GetUnHandoverBoxList(string device)
        {
            List<YKBoxInfo> result = new List<YKBoxInfo>();
            string sql = string.Format(@"SELECT * FROM dbo.YKBox WHERE EquipXindeco = '{0}' AND IsHandover = 0 AND Status='S'
SELECT * FROM dbo.YKBoxDetail WHERE Hu IN (SELECT Hu FROM dbo.YKBox WHERE EquipXindeco = '{0}' AND IsHandover = 0 AND Status='S')
", device);
            DataSet ds = DBHelper.GetDataSet(sql, false);
            if(ds!=null && ds.Tables.Count  == 2)
            {
                result = new List<YKBoxInfo>();
                DataTable dtBox = ds.Tables[0];
                DataTable dtDetail = ds.Tables[1];
                if(dtBox!=null && dtBox.Rows.Count>0)
                {
                    foreach(DataRow row in dtBox.Rows)
                    {
                        YKBoxInfo item = YKBoxInfo.BuildBoxInfo(row);
                        DataRow[] rowsDetail = dtDetail.Select(string.Format("Hu ='{0}'", item.Hu));
                        if(rowsDetail.Length>0)
                        {
                            foreach(DataRow rowdetail in rowsDetail)
                            {
                                YKBoxDetailInfo detail = YKBoxDetailInfo.BuildBoxDetailInfo(rowdetail);
                                item.Details.Add(detail);
                            }
                        }
                        result.Add(item);
                    }
                }
            }
            return result;
        }
        
        public static bool HandoverBoxByDevice(string device)
        {
            string sql = string.Format(@"UPDATE dbo.YKBox SET IsHandover = 1 WHERE EquipXindeco = '{0}'", device);
            return DBHelper.ExecuteNonQuery(sql) > 0;
        }

        public static bool DeleteBoxByHu(List<string> huList)
        {
            if (huList == null || huList.Count == 0) return false;
            string sql = @"DELETE FROM dbo.YKBox WHERE Hu IN ({0})
DELETE FROM dbo.YKBoxDetail WHERE Hu IN ({0})";
            string sonsql = "";
            foreach(string hu in huList)
            {
                sonsql += string.Format("'{0}',", hu);
            }
            if (sonsql.EndsWith(","))
                sonsql = sonsql.Remove(sonsql.Length - 1, 1);

            string sqlFull = string.Format(sql, sonsql);
            return DBHelper.ExecuteNonQuery(sqlFull) > 0;
        }

        public static bool SaveBox(YKBoxInfo box)
        {
            try
            {
                string sql = string.Format(@"
delete from YKBox where HU='{0}'
delete from YKBoxDetail where HU='{0}'", box.Hu);

                string sql1 = string.Format(@"INSERT INTO dbo.YKBox(
Hu ,Source , Target ,  Status , Remark , Timestamps , EquipHla , EquipXindeco , 
IsHandover , IsFull , SubUser,SapStatus,SapRemark,LouCeng,PackMat,LIFNR)
VALUES  ( '{0}' , '{1}' , '{2}' , '{3}' , '{4}' , GETDATE() , '{5}' , 
'{6}' , {7} , {8} , '{9}','{10}','{11}','{12}','{13}','{14}')",
    box.Hu, box.Source, box.Target, box.Status, box.Remark, box.EquipHla,
    box.EquipXindeco, box.IsHandover, box.IsFull,
    box.SubUser, box.SapStatus, box.SapRemark.Replace("'", ""),
    box.LouCeng, box.PackMat, box.LIFNR);

                string sql2 = "";
                if (box.Details != null && box.Details.Count > 0)
                {
                    sql2 = string.Format(@" INSERT INTO dbo.YKBoxDetail( Hu , Epc , Matnr , Zsatnr , Zcolsn , Zsiztx , Timestamps , Barcd, IsAdd) VALUES");
                    foreach (YKBoxDetailInfo item in box.Details)
                    {
                        string sonsql = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}',GETDATE() ,'{6}',{7}),", item.Hu, item.Epc, item.Matnr, item.Zsatnr, item.Zcolsn, item.Zsiztx, item.Barcd, item.IsAdd);
                        sql2 += sonsql;
                    }

                    if (sql2.EndsWith(","))
                        sql2 = sql2.Remove(sql2.Length - 1, 1);
                }

                return DBHelper.ExecuteNonQuery(sql + " " + sql1 + " " + sql2) > 0;
            }
            catch (Exception ex)
            {
                Log4netHelper.LogError(ex);
            }

            return true;
        }

        public static YKBoxInfo GetBoxByHu(string hu)
        {
            string sql = string.Format(@"SELECT * FROM dbo.YKBox WHERE Hu = '{0}' AND Status = 'S' 
SELECT * FROM dbo.YKBoxDetail WHERE Hu = '{0}'", hu);
            DataSet ds = DBHelper.GetDataSet(sql, false);
            YKBoxInfo result = null;
            if(ds?.Tables?.Count == 2)
            {
                DataTable dtMain = ds.Tables[0];
                DataTable dtDetail = ds.Tables[1];
                if(dtMain.Rows.Count>0)
                {
                    result = YKBoxInfo.BuildBoxInfo(dtMain.Rows[0]);
                    if(dtDetail.Rows.Count>0)
                    {
                        foreach(DataRow row in dtDetail.Rows)
                        {
                            result.Details.Add(YKBoxDetailInfo.BuildBoxDetailInfo(row));
                        }
                    }
                }
            }
            return result;
        }
    }
}
