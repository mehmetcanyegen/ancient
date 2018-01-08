using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ancient.Db
{
    class SqlCe
    {

      
        public static string Con = "Data Source=|DataDirectory|YOURDBNAME.sdf;Max Database Size=4091";

        public void Ex(string sql, SqlCeParameter[] spr = null)
        {
            using (var con = new SqlCeConnection(Con))
            {
                con.Open();
                var cmd = new SqlCeCommand(sql, con);
                if (spr != null)
                    cmd.Parameters.AddRange(spr);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public int ExsId(string sql, SqlCeParameter[] spr = null)
        {
            int result;
            using (var con = new SqlCeConnection(Con))
            {
                con.Open();
                var cmd = new SqlCeCommand(sql, con);
                if (spr != null)
                    cmd.Parameters.AddRange(spr);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@IDENTITY";
                result = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            return result;
        }

        public SqlCeDataReader Exdr(string sql, SqlCeParameter[] spr = null)
        {
            var con = new SqlCeConnection(Con);
            var selectString = sql;
            var cmd = new SqlCeCommand(selectString, con);
            con.Open();
            if (spr != null)
                cmd.Parameters.AddRange(spr);
            var dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dtr;
        }

        public object Exs(string sql, SqlCeParameter[] spr = null)
        {
            object obj;
            using (var con = new SqlCeConnection(Con))
            {
                con.Open();
                var cmd = new SqlCeCommand(sql, con);
                if (spr != null)
                    cmd.Parameters.AddRange(spr);
                obj = cmd.ExecuteScalar();
                con.Close();
            }
            return obj;
        }

    }
}
