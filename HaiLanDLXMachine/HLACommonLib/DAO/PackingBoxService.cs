using HLACommonLib.Model.PACKING;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OSharp.Utility.Extensions;
namespace HLACommonLib.DAO
{
    public class PackingBoxService
    {
        public static PBBoxInfo GetBoxByHu (string hu)
        {
            PBBoxInfo result = new PBBoxInfo();
            string sql = string.Format(@"select * from PBBox WHERE HU='{0}'
SELECT * FROM PBBoxDetail WHERE HU = '{0}'", hu);

            DataSet ds = DBHelper.GetDataSet(sql, false);
            if(ds!=null && ds.Tables.Count == 2)
            {
                DataTable boxTable = ds.Tables[0];
                if (boxTable != null && boxTable.Rows.Count > 0)
                {
                    result = PBBoxInfo.BuildPBBoxInfo(boxTable.Rows[0]);
                }
                else
                    return null;
                DataTable detailTable = ds.Tables[1];
                if (detailTable != null && detailTable.Rows.Count > 0)
                {
                    foreach(DataRow row in detailTable.Rows)
                    {
                        result.Details.Add(PBBoxDetailInfo.BuildPBBoxDetailInfo(row));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取未生成交接单的箱主信息和明细信息
        /// </summary>
        /// <returns></returns>
        public static List<PBBoxInfo> GetUnGenerateBoxListWithDetail()
        {
            List<PBBoxInfo> result = null;
            string sql = @"SELECT * FROM dbo.PBBox WHERE IsGenerate=0

                            SELECT * FROM dbo.PBBoxDetail WHERE HU IN(SELECT HU FROM dbo.PBBox WHERE IsGenerate = 0)";
            DataSet ds = DBHelper.GetDataSet(sql, false);
            if(ds!=null && ds.Tables.Count == 2)
            {
                DataTable dtBox = ds.Tables[0];
                DataTable dtBoxDetail = ds.Tables[1];
                if(dtBox.Rows.Count>0)
                {
                    result = new List<PBBoxInfo>();
                    foreach (DataRow row in dtBox.Rows)
                    {
                        PBBoxInfo item = PBBoxInfo.BuildPBBoxInfo(row);
                        if(dtBoxDetail != null && dtBoxDetail.Rows.Count>0)
                        {
                            DataRow[] detailRows = dtBoxDetail.Select(string.Format("HU='{0}'", item.HU));
                            if(detailRows != null && detailRows.Length>0)
                            {
                                foreach (DataRow detailRow in detailRows)
                                {
                                    item.Details.Add(PBBoxDetailInfo.BuildPBBoxDetailInfo(detailRow));
                                }
                            }
                        }
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取需要生成交接单的所有箱主信息
        /// </summary>
        /// <returns></returns>
        public static List<PBBoxInfo> GetNeedGenerateBoxListWithoutDetail()
        {
            List<PBBoxInfo> result = null;
            string sql = @"SELECT * FROM dbo.PBBox WHERE IsGenerate=0";
            DataTable dt = DBHelper.GetTable(sql, false);
            if(dt!=null && dt.Rows.Count>0)
            {
                result = new List<PBBoxInfo>();
                foreach(DataRow row in dt.Rows)
                {
                    PBBoxInfo item = PBBoxInfo.BuildPBBoxInfo(row);
                    result.Add(item);
                }
            }

            return result;
        }

        public static bool SaveBox(PBBoxInfo box)
        {
            //检查是否有已存在的相同的箱码
            string ifsql = string.Format(@"SELECT COUNT(1) FROM dbo.PBBox WHERE HU = '{0}' AND RESULT = 'S'", box.HU);
            if(DBHelper.GetValue(ifsql,false).CastTo<int>(0) > 0)
            {
                return true;
            }
            string sql = string.Format(@"
delete from PBBox where HU='{0}'
delete from PBBoxDetail where HU='{0}'",box.HU);

            string sql1 = string.Format(@"insert into PBBox(HU,LH,QTY,EQUIP,RESULT,PACKRESULT,PACKMSG,MSG,MX,LIFNR)
VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
box.HU,box.LH,box.QTY,box.EQUIP,box.RESULT,box.PACKRESULT,box.PACKMSG,box.MSG,box.MX,box.LIFNR);

            string sql2 = "";
            if(box.Details!=null && box.Details.Count>0)
            {
                sql2 = string.Format(@" insert into PBBoxDetail(EPC,HU,BARCD,MATNR,ZSATNR,ZCOLSN,ZSIZTX,IsAdd) VALUES");
                foreach(PBBoxDetailInfo item in box.Details)
                {
                    string sonsql = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7}),", 
                        item.EPC, item.HU, item.BARCD, item.MATNR, item.ZSATNR, item.ZCOLSN, item.ZSIZTX,item.IsAdd);
                    sql2 += sonsql;
                }

                if (sql2.EndsWith(","))
                    sql2 = sql2.Remove(sql2.Length - 1, 1);
            }

            return DBHelper.ExecuteNonQuery(sql + " " + sql1 + " " + sql2) > 0;
        }

        public static List<LhInfo> GetLhList()
        {
            List<LhInfo> result = null;
            string sql = @"SELECT Lh ,
                           InTag ,
                           ReturnType FROM dbo.Lh";

            DataTable dt = DBHelper.GetTable(sql, false);
            if(dt!=null && dt.Rows.Count>0)
            {
                result = new List<LhInfo>();
                foreach(DataRow row in dt.Rows)
                {
                    LhInfo item = new LhInfo();
                    item.Lh = row["Lh"] != null ? row["Lh"].ToString() : "";
                    item.InTag = row["InTag"] != null ? row["InTag"].ToString() : "";
                    item.ReturnType = row["ReturnType"] != null ? row["ReturnType"].ToString() : "";
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// 检测是否存在箱记录未生成交接单
        /// </summary>
        /// <returns></returns>
        public static bool IsExistsNoGenerateBox()
        {
            bool result = false;
            string sql = @"SELECT COUNT(1) FROM dbo.PBBox WHERE IsGenerate = 0  AND RESULT='S'";
            result = int.Parse(DBHelper.GetValue(sql, false).ToString()) > 0;
            return result;
        }

        public static object GetBoxLastResultByHU(string hu)
        {
            string sql = string.Format(@"SELECT RESULT FROM dbo.PBBox WHERE HU='{0}'", hu);
            return DBHelper.GetValue(sql, false);
        }

        public static bool UpdateBoxGenerated(string lh)
        {
            string sql = string.Format(@"UPDATE dbo.PBBox SET IsGenerate = 1 WHERE LH='{0}'

INSERT INTO  dbo.PBBoxDetailGenerated( EPC ,HU ,BARCD ,MATNR ,ZSATNR ,ZCOLSN ,ZSIZTX ,Timestamps)
SELECT EPC ,HU ,BARCD ,MATNR ,ZSATNR ,ZCOLSN ,ZSIZTX ,Timestamps 
FROM dbo.PBBoxDetail WHERE HU IN(SELECT HU FROM dbo.PBBox WHERE LH = '{0}')

DELETE FROM dbo.PBBoxDetail WHERE HU IN(SELECT HU FROM dbo.PBBox WHERE LH = '{0}')

INSERT INTO dbo.PBBoxGenerated( HU ,LH ,QTY ,EQUIP ,RESULT ,MSG ,PACKRESULT ,PACKMSG ,IsGenerate ,Timestamps,MX)
SELECT HU ,LH ,QTY ,EQUIP ,RESULT ,MSG ,PACKRESULT ,PACKMSG ,IsGenerate ,Timestamps ,MX
FROM dbo.PBBox WHERE LH = '{0}'

DELETE FROM dbo.PBBox WHERE LH = '{0}'", lh);
            return DBHelper.ExecuteNonQuery(sql) > 0;
        }

        public static List<PBBoxDetailInfo> GetBoxDetailsByHU(string hu)
        {
            List<PBBoxDetailInfo> result = null;
            string sql = string.Format(@"SELECT EPC ,HU , BARCD , MATNR , ZSATNR , ZCOLSN , ZSIZTX , Timestamps FROM dbo.PBBoxDetail WHERE HU='{0}'", hu);
            DataTable dt = DBHelper.GetTable(sql, false);
            if(dt!=null && dt.Rows.Count>0)
            {
                result = new List<PBBoxDetailInfo>();
                foreach(DataRow row in dt.Rows)
                {
                    result.Add(PBBoxDetailInfo.BuildPBBoxDetailInfo(row));
                }
            }
            return result;
        }

        public static bool DeleteBoxByHu(List<string> hulist)
        {
            string sql = @"DELETE FROM dbo.PBBox WHERE HU IN({0})
DELETE FROM dbo.PBBoxDetail WHERE HU IN({0})";

            string sonsql = "";
            foreach(string hu in hulist)
            {
                sonsql += string.Format("'{0}',", hu);
            }
            if(sonsql.EndsWith(","))
            {
                sonsql = sonsql.Remove(sonsql.Length - 1, 1);
            }

            return DBHelper.ExecuteNonQuery(string.Format(sql, sonsql)) > 0;

        }

        public static bool SaveReturnType(ReturnTypeInfo param)
        {
            string sql = string.Format(@"IF EXISTS(SELECT 1 FROM dbo.ReturnType WHERE ZPRDNR = '{0}' AND ZCOLSN = '{1}')
BEGIN
	UPDATE dbo.ReturnType SET THTYPE = '{2}' WHERE ZPRDNR = '{0}' AND ZCOLSN = '{1}'
END
ELSE
BEGIN
	INSERT INTO dbo.ReturnType( ZPRDNR, ZCOLSN, THTYPE, Timestamps )
	VALUES  ( '{0}', '{1}', '{2}', GETDATE())
END", param.ZPRDNR, param.ZCOLSN, param.THTYPE);
            return DBHelper.ExecuteNonQuery(sql) > 0;
        }

        public static List<ReturnTypeInfo> GetAllReturnType()
        {
            string sql = @"SELECT * FROM dbo.ReturnType";
            DataTable dt = DBHelper.GetTable(sql, false);
            List<ReturnTypeInfo> result = new List<ReturnTypeInfo>();
            if(dt!= null && dt.Rows.Count>0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ReturnTypeInfo item = ReturnTypeInfo.BuildReturnType(row);
                    result.Add(item);
                }
            }
            return result;
        }

        public static List<ReturnTypeInfo> getReturnType(string pin,string se)
        {
            try
            {
                string sql = string.Format(@"SELECT * FROM dbo.ReturnType where ZPRDNR='{0}' and ZCOLSN='{1}'", pin, se);
                DataTable dt = DBHelper.GetTable(sql, false);
                List<ReturnTypeInfo> result = new List<ReturnTypeInfo>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ReturnTypeInfo item = ReturnTypeInfo.BuildReturnType(row);
                        result.Add(item);
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
