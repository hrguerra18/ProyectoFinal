using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Cartera
    {
        public double Capital { get; set; }
        public double DineroPrestado { get; set; }
        public double DineroRestante { get; set; }
        public int CantidadDeudores { get; set; }

        public List<Cliente> Clientes;
        public List<Credito> Creditos;
        public List<Pago> Pagos;

        public Cartera()
        {
         Clientes = new List<Cliente>();
         Creditos = new List<Credito>();
         Pagos = new List<Pago>();
        }

    }
}
