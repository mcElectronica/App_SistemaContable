using App_SistemaContable.clase;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace App_SistemaContable
{
    /// <summary>
    /// Descripción breve de ObtenerCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class ObtenerCliente : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Cliente> GetCustomerNames()
        {

            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("o_cursor", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            oracmd.CommandText = "Pruebas_pkg.ListarDatoPruebas";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            OracleDataReader dr = oracmd.ExecuteReader();

            List<Cliente> customerList = new List<Cliente>();
            while (dr.Read())
            {
                customerList.Add(new Cliente()
                {
                    Idcliente = dr.GetInt16(0),
                    Pnombre = dr.GetString(1),
                    Snombre = dr.GetString(2),
                    Papellido = dr.GetString(3),
                    Sapellido = dr.GetString(4),
                    Dpi= dr.GetInt32(5),
                    Nit = dr.GetInt32(6),
                    Telefono = dr.GetInt32(7),
                    Email = dr.GetString(8),
                    Direccion = dr.GetString(9),
                    Municipio = dr.GetString(10)
                });
            }

            oraconn.Cerrar();


            //var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(customerList);
            return customerList;

        }
    }
}
