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
    /// Descripción breve de Login
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Login : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Usuario> verificar(string usuario, string contra)
        {
            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("o_cursorLogin", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            oracmd.Parameters.Add("usuario", usuario);
            oracmd.Parameters.Add("contra", contra);
            oracmd.CommandText = "Login.verificarLogin";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            OracleDataReader dr = oracmd.ExecuteReader();

            List<Usuario> usuarioList = new List<Usuario>();
            while (dr.Read())
            {
                usuarioList.Add(new Usuario()
                {
                    Idusuario = dr.GetInt32(0),
                    Nombre = dr.GetString(1),
                    Contra = dr.GetString(2),
                    Idempleado = dr.GetInt32(3)
                    
                    
                });
            }

            oraconn.Cerrar();
            //var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(customerList);
            return usuarioList;
        }
    }
}
