using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ancient.Db
{
    class MsSql
    {

        public static string Con = "";

        public void Ex(string sql, SqlParameter[] spr = null)
        {
            using (var con = new SqlConnection(Con))
            {
                con.Open();
                var cmd = new SqlCommand(sql, con);
                if (spr != null)
                    cmd.Parameters.AddRange(spr);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Method to execute and read sqlscript
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public SqlDataReader ExDr(string sql, SqlParameter[] spr = null)
        {
            var con = new SqlConnection(Con);
            con.Open();
            var cmd = new SqlCommand(sql, con);
            if (spr != null)
                cmd.Parameters.AddRange(spr);
            var rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rd;
        }
    }
}
