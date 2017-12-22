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

        /// <summary>
        /// Sql connection string.
        /// Set this with yours
        /// </summary>
        public static string Con = @"Data Source=YOURDBSERVER;Initial Catalog=YOURDBNAME;User ID=YOURDBUSER;Password=YOURDBPASSWORD";

        /// <summary>
        /// Method to execute sql script with or without parameters
        /// </summary>
        /// <param name="sql">sql script to be executed</param>
        /// <param name="spr">sql parameters</param>
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
        /// Method to execute and read sqlscript with or without parameters
        /// </summary>
        /// <param name="sql">sql script to be executed</param>
        /// <param name="spr">sql parameters</param>
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
