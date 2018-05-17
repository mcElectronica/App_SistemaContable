using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_SistemaContable.clase
{
    public class Usuario
    {
        private int idusuario;
        private string nombre;
        private string contra;
        private int idempleado;

        public int Idusuario
        {
            get
            {
                return idusuario;
            }

            set
            {
                idusuario = value;
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

        public string Contra
        {
            get
            {
                return contra;
            }

            set
            {
                contra = value;
            }
        }

        public int Idempleado
        {
            get
            {
                return idempleado;
            }

            set
            {
                idempleado = value;
            }
        }
    }
}