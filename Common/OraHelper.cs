using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class OraHelper
    {
        public static readonly string CONN_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            using (OracleConnection conn = new OracleConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = 9;
            try
            {
                val = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
                throw ex;
            }

            cmd.Parameters.Clear();
            return val;
        }
        public static int ExecuteNonQuery(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static int ExecuteNonQueryList(OracleTransaction trans, CommandType cmdType, string cmdText, Dictionary<string, object[]> columnRowData)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.ArrayBindCount = columnRowData.Values.First().Count();
            cmd.BindByName = true;
            PrepareCommandList(cmd, trans.Connection, trans, cmdType, cmdText, columnRowData);
            int val = 9;
            try
            {
                val = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
                throw ex;
            }

            cmd.Parameters.Clear();
            return val;
        }
        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static object ExecuteScalar(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        public static OracleDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception e)
            {
                conn.Close();
                throw e;
            }
        }
        public static DataSet Adapter(string connString, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            using (OracleConnection conn = new OracleConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataAdapter ada = new OracleDataAdapter(cmd);
                ada.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }

        }
        public static DataSet Adapter(OracleTransaction ts, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Transaction = ts;
            DataSet ds = new DataSet();
            PrepareCommand(cmd, ts.Connection, null, cmdType, cmdText, cmdParms);
            OracleDataAdapter ada = new OracleDataAdapter(cmd);
            ada.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }
        public static DataSet Adapter(OracleConnection conn, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            OracleDataAdapter ada = new OracleDataAdapter(cmd);
            ada.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }
        public static DataSet Adapter(string connString, CommandType cmdType, string cmdText, int startIndex, int rowCount, string srcTable, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            using (OracleConnection conn = new OracleConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataAdapter ada = new OracleDataAdapter(cmd);
                ada.Fill(ds, startIndex, rowCount, srcTable);
                cmd.Parameters.Clear();
                return ds;
            }
        }
        public static void CacheParameters(string cacheKey, params OracleParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }
        public static OracleParameter[] GetCachedParameters(string cacheKey)
        {
            OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            OracleParameter[] clonedParms = new OracleParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (trans != null)
                cmd.Transaction = trans;

            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        private static void PrepareCommandList(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, Dictionary<string, object[]> columnRowData)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (trans != null)
                cmd.Transaction = trans;
            string[] dbColumns = columnRowData.Keys.ToArray();
            OracleParameter oraParam;
            OracleDbType dbType;
            foreach (string colName in dbColumns)
            {
                dbType = OracleDbType.Varchar2;
                oraParam = new OracleParameter(colName, dbType);
                oraParam.Direction = ParameterDirection.Input;
                oraParam.OracleDbTypeEx = dbType;
                oraParam.Value = columnRowData[colName];
                cmd.Parameters.Add(oraParam);
            }
        }
        public static OracleConnection PrepareConn()
        {
            try
            {
                OracleConnection conn = new OracleConnection(CONN_STRING);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    return conn;
                }
                else
                {
                    return conn;
                }
            }
            catch
            {
                throw;
            }

        }
        public static OracleConnection PrepareConn(string temp)
        {
            try
            {
                OracleConnection conn = new OracleConnection(temp);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    return conn;
                }
                else
                {
                    return conn;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
