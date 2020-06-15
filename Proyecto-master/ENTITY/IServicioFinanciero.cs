using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    interface IServicioFinanciero
    {

        public double  Saldo { get; set; }
        public double  Cuota { get; set; }
        public int NumeroCuotas { get; set; }

        public double  Interes { get; set; }

        void AplicarInteres();
        string Abonar(string TipoPago, double Abono, int CantidadCuotas);
    }
}
