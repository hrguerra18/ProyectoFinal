using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ENTITY;

namespace DAL
{
   public  class CreditoRepository
    {
         SqlConnection Connection;
        private SqlCommand OrdenSql;
        private SqlDataReader Lector;
        public List<Credito> Creditos = new List<Credito>();
        public Credito Credito;
        public Pago pago;
        public List<Pago> pagos = new List<Pago>();

        public CreditoRepository(ConnectionManager connection)
        {
           Connection = connection.Connection;
        }
        public void RegistrarCredito(Credito credito)
        {
            using (var Comando = Connection.CreateCommand())
            {
                try
                {
                    Comando.CommandText = "Insert into Credito (IdCredito, IdCliente, Saldo, Cuota, NumeroCuota, interes) values(@IdCredito, @IdCliente, @Saldo, @Cuota, @NumeroCuota, @Interes)";
                    Comando.Parameters.Add("@IdCredito", System.Data.SqlDbType.VarChar).Value = credito.IdCredito;
                    Comando.Parameters.Add("@Idcliente", System.Data.SqlDbType.VarChar).Value = credito.IdCliente;
                    Comando.Parameters.Add("@Saldo", System.Data.SqlDbType.VarChar).Value = credito.Saldo;
                    Comando.Parameters.Add("@Cuota", System.Data.SqlDbType.VarChar).Value = credito.Cuota;
                    Comando.Parameters.Add("@NumeroCuota", System.Data.SqlDbType.Int).Value = credito.NumeroCuotas;
                    Comando.Parameters.Add("@Interes", System.Data.SqlDbType.VarChar).Value = credito.Interes;
                    Comando.ExecuteNonQuery();

                }
                catch (Exception)
                {

                    throw;
                }
                             
            }
        }
        public Credito BuscarCredito(String IdentificacionClente)
        {
            try
            {
                MostrarCreditos();
                List<Credito> creditoEncontrados = Creditos.FindAll(creditoss => creditoss.IdCliente == IdentificacionClente);
                Credito = creditoEncontrados[0];
                return Credito;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void ActualizarCredito(Credito credito)
        {
            try
            {
                ActualizarCreditoSql(credito);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        private void ActualizarCreditoSql(Credito credito)
        {
          
            try
            {
                using (var Comando = Connection.CreateCommand())
                {
                    Comando.CommandText = $"update Credito set Saldo = {credito.Saldo}, Cuota = {credito.Cuota}, NumeroCuota = {credito.NumeroCuotas}where IdCredito = {credito.IdCredito}";
                    Comando.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

                throw;
            }
          
        }
        public List<Credito> MostrarCreditos()
        {
            try
            {
                string Consulta = "Select * from Credito";
                OrdenSql = new SqlCommand(Consulta, Connection);
                Lector = OrdenSql.ExecuteReader();
                while (Lector.Read())
                {
                    Credito = new Credito
                    {
                        IdCliente = Lector["IdCliente"].ToString(),
                        IdCredito = Lector["IdCredito"].ToString(),
                        Saldo = Convert.ToDouble(Lector["Saldo"]),
                        Cuota = Convert.ToDouble(Lector["Cuota"]),
                        NumeroCuotas = Convert.ToInt32(Lector["NumeroCuota"]),
                        Interes = Convert.ToDouble(Lector["Interes"])
                    };
                    Creditos.Add(Credito);
                }
                return Creditos;

            }
            catch (Exception)
            {

                throw;
            }
       
            
            
        }

        public string ObtenerCodigo()
        {
            try
            {
                string codigo;
                string Consulta = "select codigoCredito from Codigos";
                OrdenSql = new SqlCommand(Consulta, Connection);
                Lector = OrdenSql.ExecuteReader();
                Lector.Read();
                codigo = Convert.ToString(Lector["codigoCredito"]);
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
                    Comando.CommandText = $"update Codigos  set codigoCredito = {Convert.ToInt32(codigo)}+1";
                    Comando.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public List<Pago> MostarPagosCliente(string id) 
        {
            pagos.Clear();
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = $"Select * from Pago where IdCliente = '{@id}'";
                var datareader = command.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        pago = new Pago();

                        pago.IdPago = (string)datareader["IdPago"];
                        pago.IdCliente = (string)datareader["IdCliente"];
                        pago.IdCredito = (string)datareader["IdCredito"];
                        pago.Fecha = Convert.ToDateTime(datareader["Fecha"]);
                        pago.ValorPago = Convert.ToDouble(datareader["ValorPago"]);

                        pagos.Add(pago);
                        
                    }
                }
            }
            return pagos;

        }
        public List<Credito> MostarCreditoCliente(string id)
        {
            Creditos.Clear();
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = $"Select * from Credito where IdCliente = '{@id}'";
                var datareader = command.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        Credito = new Credito();

                        Credito.IdCredito = (string)datareader["IdCredito"];
                        Credito.IdCliente = (string)datareader["IdCliente"];
                        Credito.Saldo = Convert.ToDouble(datareader["Saldo"]);
                        Credito.Cuota = Convert.ToDouble(datareader["Cuota"]);
                        Credito.NumeroCuotas = (int)(datareader["NumeroCuota"]);
                        Credito.Interes = Convert.ToDouble(datareader["Interes"]);

                        Creditos.Add(Credito);

                    }
                }
            }
            return Creditos;

        }

        public double ValorTotal(List<Pago> pagos)
        {
            return pagos.Sum(l => l.ValorPago);
        }

        public List<Pago> FiltroFecha(List<Pago> pagos, DateTime dia, DateTime mes, DateTime año)
        {
            return pagos.Where(p => p.Fecha.Day.Equals(dia.Day) & p.Fecha.Month.Equals(mes.Month) & p.Fecha.Year.Equals(año.Year)).ToList();
        }







    }
}
