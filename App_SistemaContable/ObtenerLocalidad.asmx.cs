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
    /// Descripción breve de ObtenerLocalidad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class ObtenerLocalidad : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Departamento> GetDepartament()
        {
            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("o_cursorDep", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            oracmd.CommandText = "Localidad.selectDepartamento";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            OracleDataReader dr = oracmd.ExecuteReader();

            List<Departamento> deptoList = new List<Departamento>();
            while (dr.Read())
            {
                deptoList.Add(new Departamento()
                {
                    IdDepartamento = dr.GetInt32(0),
                    Nombre = dr.GetString(1)
                });
            }

            oraconn.Cerrar();


            //var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(customerList);
            return deptoList;
        }

        [WebMethod]
        public List<Municipio> GetMunicipio(int id)
        {
            Conexion oraconn = new Conexion();
            OracleCommand oracmd = new OracleCommand();
            oracmd.Parameters.Add("o_cursorDep", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
            oracmd.Parameters.Add("iddepto", id);
            oracmd.CommandText = "Localidad.selectMunicipio";
            oracmd.CommandType = System.Data.CommandType.StoredProcedure;
            oracmd.Connection = oraconn.Cnn;
            OracleDataReader dr = oracmd.ExecuteReader();

            List<Municipio> municipioList = new List<Municipio>();
            while (dr.Read())
            {
                municipioList.Add(new Municipio()
                {
                    IdMunicipio = dr.GetInt32(0),
                    Nombre = dr.GetString(1),
                    IdDepartamento = dr.GetInt32(2)
                });
            }

            oraconn.Cerrar();
            //var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(customerList);
            return municipioList;
        }
    }
}
