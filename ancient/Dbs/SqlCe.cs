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
            var result = -1;
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
            SqlCeCommand cmd = null;
            var con = new SqlCeConnection(Con);
            SqlCeDataReader dtr = null;
            var selectString = sql;
            cmd = new SqlCeCommand(selectString, con);
            con.Open();
            if (spr != null)
                cmd.Parameters.AddRange(spr);
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dtr;
        }

        public object Exs(string sql, SqlCeParameter[] spr = null)
        {
            object obj;
            using (var _con = new SqlCeConnection(Con))
            {
                _con.Open();
                var cmd = new SqlCeCommand(sql, _con);
                if (spr != null)
                    cmd.Parameters.AddRange(spr);
                obj = cmd.ExecuteScalar();
                _con.Close();
            }
            return obj;
        }

    }
}
