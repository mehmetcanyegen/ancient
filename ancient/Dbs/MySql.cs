using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ancient.Dbs
{
    public class MySql
    {

        public static string Con = "Server=YOURSERVERIP;Port=YOURPORT;Database=YOURDBNAME;Uid=YOURDBUSERNAME;Pwd=YOURDBPW; pooling=fals";

        public static SqlDataReader Exdr(string sql)
        {
            SqlDataReader dtr = null;
            var con = new SqlConnection(Con);
            try
            {
                var cmd = new SqlCommand(sql, con);
                con.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                // ignored
            }
            return dtr;
        }

        public static DataTable Exdt(string sql)
        {
            dynamic dt = new DataTable();
            MySqlConnection con = new MySqlConnection(Con);
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(dt);
            con.Close();
            con.Dispose();
            return dt;
        }

        public static void Ex(string sql)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(Con);
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                da.SelectCommand.ExecuteNonQuery();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

    }
}
