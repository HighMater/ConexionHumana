using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Conexion
    {
        public static string Db = "data source = localhost\\SQLEXPRESS; initial catalog = bd_ConexionHumana; user id = root; password = 1234";
        SqlConnection con = new SqlConnection(Db);

       
        public void conectar()
        {
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void desconectar()
        {
            con.Close();
        }
        
        public SqlConnection conex()
        {
            return con;
        }

    }

}