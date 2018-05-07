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
    // [System.Web.Script.Services.ScriptService]
    public class InsertarCliente : System.Web.Services.WebService
    {

        [WebMethod]
        public int addcustumers(int id, string nombre, string descrip)
        {
            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("idprueba", id);
            oracmd.Parameters.Add("nombre_in", nombre);
            oracmd.Parameters.Add("descripcion_in", descrip);
            oracmd.CommandText = "insertempleado";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            int res = oracmd.ExecuteNonQuery();

            return res;
        }
    }
}
