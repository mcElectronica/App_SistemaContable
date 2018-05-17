using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_SistemaContable.clase
{
    public class Municipio
    {
        private int idMunicipio;
        private string nombre;
        private int idDepartamento;

        public int IdMunicipio
        {
            get
            {
                return idMunicipio;
            }

            set
            {
                idMunicipio = value;
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
    }
}