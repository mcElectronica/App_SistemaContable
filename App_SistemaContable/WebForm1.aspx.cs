using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Reflection;
using System.Diagnostics;
using System.Data;

namespace App_SistemaContable
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            //Create a connection to Oracle
            string conString = "User Id=USERPETEN; password=USERPETEN;" +

            //How to connect to an Oracle DB without SQL*Net configuration file
            //also known as tnsnames.ora.
            "Data Source=localhost:1521/xe; Pooling=false;";

            //How to connect to an Oracle Database with a Database alias.
            //Uncomment below and comment above.
            //"Data Source=orcl;Pooling=false;";

            OracleConnection con = new OracleConnection();
            con.ConnectionString = conString;
            con.Open();

            //Create a command within the context of the connection
            //Use the command to display employee names and salary from the Employees table
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select departamento from departamento where id_departamento = 1";

            //Execute the command and use datareader to display the data
            OracleDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Label1.Text = reader.GetString(0);
            
            /*while (reader.Read())
            {
                Console.WriteLine("Employee Name: " + reader.GetString(0));
            }
            Console.ReadLine();*/

            

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Conexion db = new Conexion();

            db.Consulta("select departamento from departamento where id_departamento = 1");
            /*while (db.Dr.Read())
            {
                Console.WriteLine("ID:" + db.Dr.GetInt32(0) + ", Nombre: " + db.Dr.GetString(1) + ", Apellido: " + db.Dr.GetString(2));
            }
            Console.ReadKey();
            */
            db.Dr.Read();
            Label2.Text = db.Dr.GetString(0);

            db.ConsultaProcedure("PACKAGESC.ListarDatoPruebas");
            db.Dr.Read();

            while (db.Dr.Read())
            {
                Debug.WriteLine(db.Dr.GetString(0));
            }

            Debug.WriteLine("aki estoy");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string strConnectionString = "User Id=USERPETEN; password=USERPETEN;Data Source=localhost:1521/xe; Pooling=false;";
            OracleConnection oraconn = new OracleConnection(strConnectionString);
            oraconn.Open();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("o_cursor", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            oracmd.CommandText = "Pruebas_pkg.ListarDatoPruebas";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn;
            OracleDataReader dr = oracmd.ExecuteReader();


            /*OracleDataAdapter da = new OracleDataAdapter(oracmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();*/
            Label1.Text = "";

            while (dr.Read())
            {
                
                Label1.Text += dr.GetString(1) + "" + dr.GetString(2);
            }
            

            oraconn.Close();




        }
    }
}