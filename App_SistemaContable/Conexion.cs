using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.SqlClient;
using System.Collections;


namespace App_SistemaContable
{
    public class Conexion
    {
        private OracleDataReader m_dr;
        private string m_stringConexion = "User Id=USERPETEN; password=USERPETEN;Data Source=localhost:1521/xe; Pooling=false;";
        private OracleConnection m_cnn;
        private OracleCommand cmd;

        public Conexion()
        {
            m_cnn = new OracleConnection(m_stringConexion);
            m_cnn.Open();
        }

        public void Consulta(string query)
        {
            
            if (Cnn.State == System.Data.ConnectionState.Open)
            {
                cmd = new OracleCommand(query, m_cnn);
                Dr.Close();
                Dr = cmd.ExecuteReader();
            }
            else
            {
                m_cnn.Open();
                cmd = new OracleCommand(query, m_cnn);
                Dr = cmd.ExecuteReader();
            }
        }

        public void ConsultaProcedure(string query)
        {

            if (Cnn.State == System.Data.ConnectionState.Open)
            {
                cmd = new OracleCommand(query, m_cnn);
                cmd.Parameters.Add("o_cursor", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Dr.Close(); 
                Dr = cmd.ExecuteReader();
            }
            else
            {
                m_cnn.Open();
                cmd = new OracleCommand(query, m_cnn);
                Dr = cmd.ExecuteReader();
            }
        }

        public OracleDataReader Dr
        {
            get { return m_dr; }
            set { m_dr = value; }
        }

        public void Cerrar()
        {
            
            m_cnn.Close();
        }

        public OracleConnection Cnn
        {
            get { return m_cnn; }
            set { m_cnn = value; }
        }
    }
}