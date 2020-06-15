using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using System.Data.SqlClient;

namespace DAL
{
    public class CarteraRepository
    {
        private SqlConnection Conexion;
        private SqlCommand OrdenSql;
        private SqlDataReader Lector;
        public Cartera cartera = new Cartera();
        public Pago pago;
        public Cliente cliente;
        public Credito credito;



        public CarteraRepository(ConnectionManager connection)
        {
            Conexion = connection.Connection;


        }

        public void ActualizarDineroPrestado(double CantidadPrestado)
        {
            try
            {
                MostrarCartera();
                cartera.DineroPrestado += CantidadPrestado;
                ActualizarCartera("DineroPrestado", cartera.DineroPrestado.ToString());
                cartera.DineroRestante -= CantidadPrestado;
                ActualizarCartera("DineroRestante", cartera.DineroRestante.ToString());
            }
            catch (Exception)
            {

                throw;
            }

        }


        public void ActualizarDineroRestante(Double CantidadAbono)
        {
            try
            {
                MostrarCartera();
                cartera.DineroPrestado -= CantidadAbono;
                if (cartera.DineroPrestado < 0)
                {
                    cartera.DineroPrestado = 0;
                }
                ActualizarCartera("DineroPrestado", cartera.DineroPrestado.ToString());
                cartera.DineroRestante += CantidadAbono;
                if (cartera.DineroRestante > 30000000)
                {
                    cartera.DineroRestante = 30000000;
                }
                ActualizarCartera("DineroRestante", cartera.DineroRestante.ToString());

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void ActualizarCantidadDeudoresMenos()
        {
            try
            {
                MostrarCartera();
                cartera.CantidadDeudores--;
                ActualizarCartera("CantidadDeudores", cartera.CantidadDeudores.ToString());

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void ActualizarCantidadDeudores()
        {
            try
            {
                MostrarCartera();
                cartera.CantidadDeudores++;
                ActualizarCartera("CantidadDeudores", cartera.CantidadDeudores.ToString());

            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool ValidarDineroRestante(Double cantidadPrestamo)
        {
            try
            {
                MostrarCartera();
                if (cartera.DineroRestante - cantidadPrestamo < 50)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Cartera MostrarCartera()
        {
            try
            {

                string Consulta = "Select * from Cartera";
                OrdenSql = new SqlCommand(Consulta, Conexion);
                Lector = OrdenSql.ExecuteReader();
                Lector.Read();
                cartera.CantidadDeudores = Convert.ToInt32(Lector["CantidadDeudores"]);
                cartera.Capital = Convert.ToDouble(Lector["Capital"]);
                cartera.DineroPrestado = Convert.ToDouble(Lector["DineroPrestado"]);
                cartera.DineroRestante = Convert.ToDouble(Lector["DineroRestante"]);
                return cartera;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void ActualizarCartera(string Campo, string valor)
        {
            try
            {
                // Conexion.Close();
                //Conexion.Open();
                using (var Comando = Conexion.CreateCommand())
                {
                    Comando.CommandText = $"update Cartera set {Campo} = {valor}";
                    Comando.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

                throw;
            }



        }


        public List<Pago> CargarPagos()
        {


            string Consulta = "Select * from Pago";
            OrdenSql = new SqlCommand(Consulta, Conexion);
            Lector = OrdenSql.ExecuteReader();
            while (Lector.Read())
            {
                pago = new Pago
                {
                    IdPago = Lector["IdPago"].ToString(),
                    Fecha = Convert.ToDateTime(Lector["Fecha"]),
                    IdCliente = Lector["IdCliente"].ToString(),
                    IdCredito = Lector["IdCredito"].ToString(),
                    ValorPago = Convert.ToDouble(Lector["ValorPago"])
                };
                cartera.Pagos.Add(pago);
            }
            return cartera.Pagos;


        }


        public List<Cliente> CargarCliente()
        {
            cartera.Clientes.Clear();
            string Consulta = "select * from Cliente ";
            OrdenSql = new SqlCommand(Consulta, Conexion);
            Lector = OrdenSql.ExecuteReader();
            while (Lector.Read())
            {
                cliente = new Cliente
                {
                    Identificacion = Lector["Identificacion"].ToString(),
                    Nombre = Lector["Nombre"].ToString(),
                    Correo = Lector["Correo"].ToString(),
                    Direccion = Lector["Direccion"].ToString(),
                    Estado = Lector["Estado"].ToString(),
                    Telefono = Lector["Telefono"].ToString()
                };
                cartera.Clientes.Add(cliente);
            }
                return cartera.Clientes;


        }
        

        public List<Credito> CreditosCreditos()
        {
            string Consulta = "Select * from Credito";
            OrdenSql = new SqlCommand(Consulta, Conexion);
            Lector = OrdenSql.ExecuteReader();
            while (Lector.Read())
            {
                credito = new Credito
                {
                    IdCliente = Lector["IdCliente"].ToString(),
                    IdCredito = Lector["IdCredito"].ToString(),
                    Saldo = Convert.ToDouble(Lector["Saldo"]),
                    Cuota = Convert.ToDouble(Lector["Cuota"]),
                    NumeroCuotas = Convert.ToInt32(Lector["NumeroCuota"]),
                    Interes = Convert.ToDouble(Lector["Interes"])
                };
                cartera.Creditos.Add(credito);
            }
            return cartera.Creditos;

        }




    }
}
