using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_SistemaContable.clase
{
    public class Cliente
    {

        private int idcliente;
        private string pnombre;
        private string snombre;
        private string papellido;
        private string sapellido;
        private int dpi;
        private int nit;
        private int telefono;
        private string direccion;
        private string email;
        private string municipio;

        public int Idcliente
        {
            get
            {
                return idcliente;
            }

            set
            {
                idcliente = value;
            }
        }

        public string Pnombre
        {
            get
            {
                return pnombre;
            }

            set
            {
                pnombre = value;
            }
        }

        public string Snombre
        {
            get
            {
                return snombre;
            }

            set
            {
                snombre = value;
            }
        }

        public string Papellido
        {
            get
            {
                return papellido;
            }

            set
            {
                papellido = value;
            }
        }

        public string Sapellido
        {
            get
            {
                return sapellido;
            }

            set
            {
                sapellido = value;
            }
        }

        public int Dpi
        {
            get
            {
                return dpi;
            }

            set
            {
                dpi = value;
            }
        }

        public int Nit
        {
            get
            {
                return nit;
            }

            set
            {
                nit = value;
            }
        }

        public int Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Municipio
        {
            get
            {
                return municipio;
            }

            set
            {
                municipio = value;
            }
        }
    }
}