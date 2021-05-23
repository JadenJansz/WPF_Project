using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace WPF_Project
{
    class Database
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        public Database()
        {
            con = new SqlConnection("Server=tcp:raveeshanibm.database.windows.net,1433;Initial Catalog=Hotel_Reservation;Persist Security Info=False;User ID=RaveeshaNIBM;Password=Jj070300;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public int save_update_delete(string s)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }

                con.Open();

                cmd = new SqlCommand(s, con);

                int i = cmd.ExecuteNonQuery();

                con.Close();

                return i;
        }

        public DataTable displayData(string s)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();

            da = new SqlDataAdapter(s, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.Close();

            return dt;
        }

        public DataTable increament(string a)
        {
            con.Open();

            da = new SqlDataAdapter(a, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.Close();

            return dt;
        }
    }
}
