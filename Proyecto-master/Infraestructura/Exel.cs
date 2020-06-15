using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ENTITY;

namespace Infraestructura
{
   public class Exel
    {
        
        public void GenerarReporteClientes(List<Cliente> clientes)
        {
            var worbook = new XLWorkbook();
            var worShet = worbook.Worksheets.Add("Reporte Clientes");
            int i = 3;
            worShet.Cell($"A1").Value = $"Reporte {DateTime.Now.ToString()} ";
            worShet.Cell("A2").Value = "Identidicacion";
            worShet.Cell("B2").Value = "Nombre";
            worShet.Cell("C2").Value = "Telefono";
            worShet.Cell("D2").Value = "Correo";
            worShet.Cell("E2").Value = "Direccion";
            foreach (Cliente cliente in clientes)
            {
                worShet.Cell($"A{i}").Value = cliente.Identificacion;
                worShet.Cell($"B{i}").Value = cliente.Nombre;
                worShet.Cell($"C{i}").Value = cliente.Telefono;
                worShet.Cell($"D{i}").Value = cliente.Correo;
                worShet.Cell($"E{i}").Value = cliente.Direccion;
                i++;
            }
            worbook.SaveAs(@"C:\Reportes PAYMENTF\ReporteClientesActual.xlsx");
            
        }
        public void GenerarReporteCreditos(List<Credito> creditos)
        {
            var worbook = new XLWorkbook();
            var worShet = worbook.Worksheets.Add("Reporte Creditos");
            int i = 3;
            worShet.Cell($"A1").Value = $"Reporte {DateTime.Now.ToString()} ";
            worShet.Cell("A2").Value = "IdCredito";
            worShet.Cell("B2").Value = "IDcliente";
            worShet.Cell("C2").Value = "Saldo";
            worShet.Cell("D2").Value = "Cuota";
            worShet.Cell("E2").Value = "Numero de cuotas";
            worShet.Cell("F2").Value = "Interes";
            foreach (Credito credito in creditos)
            {
                worShet.Cell($"A{i}").Value = credito.IdCredito;
                worShet.Cell($"B{i}").Value = credito.IdCliente;
                worShet.Cell($"C{i}").Value = credito.Saldo;
                worShet.Cell($"D{i}").Value = credito.Cuota;
                worShet.Cell($"E{i}").Value = credito.NumeroCuotas;
                worShet.Cell($"F{i}").Value = credito.Interes;
                i++;
            }
            worbook.SaveAs(@"C:\Reportes PAYMENTF\ReporteCreditosActual.xlsx");
        }

        public void GenerarReportePagos(List<Pago> Pagos)
        {
            var worbook = new XLWorkbook();
            var worShet = worbook.Worksheets.Add("Reporte Pagos");
            int i = 3;
            worShet.Cell($"A1").Value = $"Reporte {DateTime.Now.ToString()} ";
            worShet.Cell("A2").Value = "ID_Pago";
            worShet.Cell("B2").Value = "ID_cliente";
            worShet.Cell("C2").Value = "ID_Credito";
            worShet.Cell("D2").Value = "Valor pagado";
            worShet.Cell("E2").Value = "Fecha";
            foreach (Pago pago in Pagos)
            {
                worShet.Cell($"A{i}").Value = pago.IdPago;
                worShet.Cell($"B{i}").Value = pago.IdCliente;
                worShet.Cell($"C{i}").Value = pago.IdCredito;
                worShet.Cell($"D{i}").Value = pago.ValorPago;
                worShet.Cell($"E{i}").SetValue(pago.Fecha).Style.DateFormat.SetFormat("dd-mm-yyyy").Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                i++;
            }
            worbook.SaveAs(@"C:\Reportes PAYMENTF\ReportePagosActual.xlsx");
        }




    }
}
