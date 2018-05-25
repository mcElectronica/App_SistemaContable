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
    /// Descripción breve de Proveedores
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Proveedores : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Proveedor> obtenerProveedores ()
        {
            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("o_cursorProv", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            oracmd.CommandText = "Proveedor.ListarProveedores";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            OracleDataReader dr = oracmd.ExecuteReader();

            List<Proveedor> proveedorList = new List<Proveedor>();
            while (dr.Read())
            {
                proveedorList.Add(new Proveedor()
                {
                    IdProveedor = dr.GetInt32(0),
                    Nombre = dr.GetString(1),
                    Nit = dr.GetInt32(2),
                    Telefono = dr.GetInt32(4),
                    Direccion = dr.GetString(5),
                    Email = dr.GetString(3),
                    Municipio = dr.GetString(6)

                });
            }

            oraconn.Cerrar();

            //var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(customerList);
            return proveedorList;

        }

        [WebMethod]
        public int insertarProveedor(string nombre, int nit, string email, int telefono, string direccion, int municipio)
        {
            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("nombre_in", nombre);
            oracmd.Parameters.Add("nit_in", nit);
            oracmd.Parameters.Add("email_in", email);
            oracmd.Parameters.Add("telefono_in", telefono);
            oracmd.Parameters.Add("direccion_in", direccion);
            oracmd.Parameters.Add("municipio_in", municipio);
            oracmd.CommandText = "insertproveedor";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            int res = oracmd.ExecuteNonQuery();

            return res;
        }
    }
}
