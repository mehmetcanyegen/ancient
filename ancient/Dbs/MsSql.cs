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
        /// Execute sql script and get result as DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public static DataTable Dt(string sql, SqlParameter[] spr = null)
        {
            DataTable dt;
            using (var _con = new SqlConnection(Con))
            {
                _con.Open();
                var da = new SqlDataAdapter(sql, _con);
                if (spr != null)
                    da.SelectCommand.Parameters.AddRange(spr);
                dt = new DataTable();
                da.Fill(dt);
                _con.Close();
            }
            return dt;
        }
   
        /// <summary>
        /// Use it to get only one row and one column information 
        /// after executing sql script
        /// </summary>
        /// <param name="sql">sql script</param>
        /// <param name="spr">sql parameters</param>
        /// <returns></returns>
        public object ExSc(string sql, SqlParameter[] spr = null)
        {
            var con = new SqlConnection(Con);
            con.Open();
            var cmd = new SqlCommand(sql, con);
            if (spr != null)
                cmd.Parameters.AddRange(spr);
            var obj = cmd.ExecuteScalar();
            con.Close();
            return obj;
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
