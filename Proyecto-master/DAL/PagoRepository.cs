using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using System.Data.SqlClient;

namespace DAL
{
    public class PagoRepository
    {
         SqlConnection Connection;
        private SqlCommand OrdenSql;
        private SqlDataReader Lector;
        private Pago Pago;
        public List<Pago> Pagos = new List<Pago>();

        public PagoRepository(ConnectionManager connection)
        {
            Connection = connection.Connection;
           
        }
        public void RegistrarPago(Pago Pago)
        {
            try
            {
                using (var Comando = Connection.CreateCommand())
                {
                    Comando.CommandText = "insert into Pago (IdPago, IdCliente, IdCredito, Fecha, ValorPago) values(@IdPago, @IdCliente, @IdCredito, @Fecha, @ValorPago)";
                    Comando.Parameters.Add("@IdPago", System.Data.SqlDbType.VarChar).Value = Pago.IdPago;
                    Comando.Parameters.Add("@IdCredito", System.Data.SqlDbType.VarChar).Value = Pago.IdCredito;
                    Comando.Parameters.Add("@IdCliente", System.Data.SqlDbType.VarChar).Value = Pago.IdCliente;
                    Comando.Parameters.Add("@Fecha", System.Data.SqlDbType.DateTime).Value = Pago.Fecha;
                    Comando.Parameters.Add("@ValorPago", System.Data.SqlDbType.VarChar).Value = Pago.ValorPago;
                    Comando.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

                throw;
            }

           
        }

        public List<Pago> MostrarPagos()
        {
            try
            {
                string Consulta = "Select * from Pago";
                OrdenSql = new SqlCommand(Consulta, Connection);
                Lector = OrdenSql.ExecuteReader();
                while (Lector.Read())
                {
                    Pago = new Pago
                    {
                        IdPago = Lector["IdPago"].ToString(),
                        Fecha = Convert.ToDateTime(Lector["Fecha"]),
                        IdCliente = Lector["IdCliente"].ToString(),
                        IdCredito = Lector["IdCredito"].ToString(),
                        ValorPago = Convert.ToDouble(Lector["ValorPago"])
                    };
                    Pagos.Add(Pago);
                }
                return Pagos;

            }
            catch (Exception)
            {

                throw;
            }

            //Connection.Close();
            //Connection.Open();
            
        }
        public string ObtenerCodigo()
        {
            try
            {
                string codigo;
                string Consulta = "select codigoPago from Codigos";
                OrdenSql = new SqlCommand(Consulta, Connection);
                Lector = OrdenSql.ExecuteReader();
                Lector.Read();
                codigo = Convert.ToString(Lector["CodigoPago"]);
                ActualizarCodigo(codigo);
                return codigo;

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void ActualizarCodigo(string codigo)
        {
            try
            {
                using (var Comando = Connection.CreateCommand())
                {
                    Comando.CommandText = $"update Codigos set codigoPago = {Convert.ToInt32(codigo)}+1";
                    Comando.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {

                throw;
            }
           // Connection.Close();
            //onnection.Open();
           
        }
       


    }
}
