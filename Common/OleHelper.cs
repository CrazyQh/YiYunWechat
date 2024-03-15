using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class OleHelper
    {
        public static readonly string CONN_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        #region OleClient
        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(OleDbTransaction trans, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
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
        public static int ExecuteNonQuery(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {

            OleDbCommand cmd = new OleDbCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static object ExecuteScalar(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        public static OleDbDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(connString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception e)
            {
                conn.Close();
                throw e;
            }
        }
        public static DataSet Adapter(string connString, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            DataSet ds = new DataSet();
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OleDbDataAdapter ada = new OleDbDataAdapter(cmd);
                ada.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }

        }
        public static DataSet Adapter(OleDbTransaction ts, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Transaction = ts;
            DataSet ds = new DataSet();
            PrepareCommand(cmd, ts.Connection, null, cmdType, cmdText, cmdParms);
            OleDbDataAdapter ada = new OleDbDataAdapter(cmd);
            ada.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }
        public static DataSet Adapter(OleDbConnection conn, CommandType cmdType, string cmdText, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            DataSet ds = new DataSet();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            OleDbDataAdapter ada = new OleDbDataAdapter(cmd);
            ada.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }
        public static DataSet Adapter(string connString, CommandType cmdType, string cmdText, int startIndex, int rowCount, string srcTable, params OleDbParameter[] cmdParms)
        {
            OleDbCommand cmd = new OleDbCommand();
            DataSet ds = new DataSet();
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OleDbDataAdapter ada = new OleDbDataAdapter(cmd);
                ada.Fill(ds, startIndex, rowCount, srcTable);
                cmd.Parameters.Clear();
                return ds;
            }
        }
        public static void CacheParameters(string cacheKey, params OleDbParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }
        public static OleDbParameter[] GetCachedParameters(string cacheKey)
        {
            OleDbParameter[] cachedParms = (OleDbParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            OleDbParameter[] clonedParms = new OleDbParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (OleDbParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }
        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, CommandType cmdType, string cmdText, OleDbParameter[] cmdParms)
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
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        public static OleDbConnection PrepareConn(string CONN_STRING)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(CONN_STRING);
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
        #endregion
    }
}
