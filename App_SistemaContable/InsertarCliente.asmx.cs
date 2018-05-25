using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace App_SistemaContable
{
    /// <summary>
    /// Descripción breve de InsertarCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class InsertarCliente : System.Web.Services.WebService
    {

        [WebMethod]
        public int addcustumers(string pnombre, string snombre, string papellido, string sapellido, int dpi, int nit, int telefono, string email,
            string direccion, int municipio)

        {
            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("p_nombre_in", pnombre);
            oracmd.Parameters.Add("s_nombre_in", snombre);
            oracmd.Parameters.Add("p_apellido_in", papellido);
            oracmd.Parameters.Add("s_apellido_in", sapellido);
            oracmd.Parameters.Add("dpi_in", dpi);
            oracmd.Parameters.Add("nit_in", nit);
            oracmd.Parameters.Add("telefono_in", telefono);
            oracmd.Parameters.Add("email_in", email);
            oracmd.Parameters.Add("direccion_in", direccion);
            oracmd.Parameters.Add("municipio_in", municipio);
            oracmd.CommandText = "insertempleado";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            int res = oracmd.ExecuteNonQuery();
            oraconn.Cerrar();
            return res;
        }

        [WebMethod]
        public int updatecustumers(int id, string pnombre, string snombre, string papellido, string sapellido, int dpi, int nit, int telefono, string email,
            string direccion, int municipio)

        {
            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("idcliente_in", id);
            oracmd.Parameters.Add("pnombre_in", pnombre);
            oracmd.Parameters.Add("snombre_in", snombre);
            oracmd.Parameters.Add("papellido_in", papellido);
            oracmd.Parameters.Add("sapellido_in", sapellido);
            oracmd.Parameters.Add("dpi_in", dpi);
            oracmd.Parameters.Add("nit_in", nit);
            oracmd.Parameters.Add("telefono_in", telefono);
            oracmd.Parameters.Add("email_in", email);
            oracmd.Parameters.Add("direccion_in", direccion);
            oracmd.Parameters.Add("municipio_in", municipio);
            oracmd.CommandText = "updatecliente";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            int res = oracmd.ExecuteNonQuery();


            oraconn.Cerrar();
            return res;
        }
    }
}
