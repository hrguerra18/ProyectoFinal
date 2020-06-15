using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
     public class Cliente
    {
        public string  Nombre { get; set; }

        public string Direccion  { get; set; }

        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Identificacion { get; set; }
        public string Estado { get; set; }

        public List<Credito> Creditos;
        public Cliente()
        {
            Creditos = new List<Credito>(); 
             
          }
    }
}
