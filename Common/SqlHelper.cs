using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SqlHelper
    {
        public static readonly string CONN_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
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
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        public static SqlConnection PrepareConn(string temp)
        {
            try
            {
                SqlConnection conn = new SqlConnection(temp);
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
        public static SqlConnection PrepareConn()
        {
            try
            {
                SqlConnection conn = new SqlConnection(CONN_STRING);
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
        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
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
        public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        public static object ExecuteScalar(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception e)
            {
                conn.Close();
                throw e;
            }
        }
        public static DataSet Adapter(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    SqlDataAdapter ada = new SqlDataAdapter(cmd);
                    ada.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds;
            }
        }
        public static DataSet Adapter(SqlTransaction ts, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = ts;
            DataSet ds = new DataSet();
            PrepareCommand(cmd, ts.Connection, null, cmdType, cmdText, cmdParms);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }
        public static DataSet Adapter(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }
        public static DataSet Adapter(string connString, CommandType cmdType, string cmdText, int startIndex, int rowCount, string srcTable, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ada.Fill(ds, startIndex, rowCount, srcTable);
                cmd.Parameters.Clear();
                return ds;
            }
        }
    }
}
