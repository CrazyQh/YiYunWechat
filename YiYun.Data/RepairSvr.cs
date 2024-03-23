using Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YiYun.Entity;

namespace YiYun.Data
{
    public class RepairSvr
    {
        #region private members _conn & _trans
        private SqlConnection _conn;
        private SqlTransaction _trans;
        #endregion

        #region 构造函数
        public RepairSvr()
        { }
        public RepairSvr(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        #endregion

        public List<Repair> GetRepairRecord(string _OpenID)
        {
            string sql = @" 
                SELECT a.ID,
                       a.VillageID,
                       a.HouseID,
                       a.HouseCode,
                       a.HouseOwner,
                       CONVERT(NVARCHAR(50), a.CreateDate, 23) CreateDate,
                       a.ReparirContent,
                       IIF(a.DealState = 'D','进行中','已完成') DealState
                FROM dbo.PMC_Repair a
                    LEFT JOIN
                    (
                        SELECT ID,
                               OPenID1
                        FROM dbo.PMC_BaseHouse
                        WHERE OPenID1 IS NOT NULL
                              AND OPenID1 != ''
                        UNION ALL
                        SELECT ID,
                               OPenID2
                        FROM dbo.PMC_BaseHouse
                        WHERE OPenID2 IS NOT NULL
                              AND OPenID2 != ''
                    ) b
                        ON b.ID = a.HouseID
                WHERE b.OPenID1 = @OpenID
            ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("OpenID",_OpenID)
            };
            DataSet ds = new DataSet();
            if (_trans != null)
            {
                ds = SqlHelper.Adapter(_trans, CommandType.Text, sql, paras);
            }
            else if (_conn != null)
            {
                ds = SqlHelper.Adapter(_conn, CommandType.Text, sql, paras);
            }
            else
            {
                ds = SqlHelper.Adapter(SqlHelper.CONN_STRING, CommandType.Text, sql, paras);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<Repair> ls = ObHelper.GetList<Repair>(ds);
                if (ls != null && ls.Count > 0)
                {
                    return ls;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public int InsertRepair(string HouseID, string Remarks, string PhoneNo, string Files)
        {
            string sql = @" 
                INSERT dbo.PMC_Repair
                (
                    SN,
                    CreateDate,
                    CreateAccount,
                    CreateName,
                    CompanyID,
                    VillageID,
                    ReparirType,
                    HouseID,
                    HouseCode,
                    HouseOwner,
                    Tel,
                    ReparirContent,
                    AdviseDate,
                    DealState,
                    Files,
                    StopFlag
                )
                SELECT dbo.f_GetNewRepairSN(),
                       CONVERT(NVARCHAR(50), GETDATE(), 23),
                       'Wechat',
                       '微信公众号',
                       a.CompanyID,
                       a.VillageID,
                       'YZ',
                       a.ID,
                       a.HouseCode,
                       a.HouseOwner,
                       @PhoneNo,
                       @Remarks,
                       CONVERT(NVARCHAR(50), GETDATE(), 23),
                       'D',
                       @Files,
                       1
                FROM dbo.PMC_BaseHouse a
                WHERE a.ID = @HouseID
            ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("HouseID",HouseID),
                new SqlParameter("Remarks",Remarks),
                new SqlParameter("PhoneNo",PhoneNo),
                new SqlParameter("Files",Files)

            };
            int ir = 0;
            if (_conn != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_conn, CommandType.Text, sql, paras);
            }
            if (_trans != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_trans, CommandType.Text, sql, paras);
            }
            else
            {
                ir = SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, sql, paras);
            }
            return ir;

        }

        public int InsertRepairMX(string Files,string FilePath)
        {
            string sql = @"
                INSERT dbo.PMC_RepairMX
                (
                    ID,
                    Url
                )
                SELECT ID,
                       REPLACE(col, @FilePath, '')
                FROM f_split2(@Files, ',')
                    CROSS JOIN PMC_Repair b
                WHERE b.Files = @Files
            ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("Files",Files),
                new SqlParameter("FilePath",FilePath)
            };
            int ir = 0;
            if (_conn != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_conn, CommandType.Text, sql, paras);
            }
            if (_trans != null)
            {
                ir = SqlHelper.ExecuteNonQuery(_trans, CommandType.Text, sql, paras);
            }
            else
            {
                ir = SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, sql, paras);
            }
            return ir;

        }
    }
}
