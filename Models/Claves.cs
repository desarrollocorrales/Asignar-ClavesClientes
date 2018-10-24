using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asignar_ClavesClientes.Models
{
    public class Claves
    {
        public int clave_ID { set; get; }
        public string Clave { set; get; }
        public int Rol { set; get; }
        public bool Principal { set; get; }
    }
}
