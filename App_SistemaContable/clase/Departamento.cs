using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_SistemaContable.clase
{
    public class Departamento
    {
        private int idDepartamento;
        private string nombre;

        public int IdDepartamento
        {
            get
            {
                return idDepartamento;
            }

            set
            {
                idDepartamento = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }
    }
}