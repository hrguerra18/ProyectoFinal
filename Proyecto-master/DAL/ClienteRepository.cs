using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DAL
{
    public class ClienteRepository
    {
        SqlConnection Connection;
        private SqlCommand Ordensql;
        private SqlDataReader lector;
        public List<Cliente> Clientes = new List<Cliente>();
        public Cliente cliente;
        public Pago pago;
        public Credito credito;


        public ClienteRepository(ConnectionManager connection)
        {
            Connection = connection.Connection;
        }
        public List<Cliente> MostrarCliente()
        {
            try
            {

                string Consulta = "select * from Cliente ";
                Ordensql = new SqlCommand(Consulta, Connection);
                lector = Ordensql.ExecuteReader();
                while (lector.Read())
                {
                    cliente = new Cliente
                    {
                        Identificacion = lector["Identificacion"].ToString(),
                        Nombre = lector["Nombre"].ToString(),
                        Correo = lector["Correo"].ToString(),
                        Direccion = lector["Direccion"].ToString(),
                        Estado = lector["Estado"].ToString(),
                        Telefono = lector["Telefono"].ToString()
                    };
                    Clientes.Add(cliente);
                }
                return Clientes;


            }
            catch (Exception)
            {

                throw;
            }           
        }

        public void ActualizarEstado(string identificacion)
        {
            try
            {
                MostrarCliente();
                List<Cliente> EstadoCliente = Clientes.FindAll(cliente => cliente.Identificacion == identificacion);
                EstadoCliente[0].Estado = "Inactivo";
                ActualizarCliente("Estado", EstadoCliente[0].Estado, EstadoCliente[0].Identificacion);

            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public void ActualizarCliente(string campo, string valor, string identificacion)
        {
            try
            {
                using (var comando = Connection.CreateCommand())
                {
                    comando.CommandText = $"update Cliente set {campo}= '{valor}' where Identificacion ={identificacion}";
                    comando.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

                throw;
            } 
           
        }

        public bool BuscarExisteCliente(string identificacion)
        {
            MostrarCliente();
            List<Cliente> ExisteCliente = Clientes.FindAll(cliente => cliente.Identificacion == identificacion);
            if (ExisteCliente.Count() == 0)
            {
                return false;
            }
            return true;
        }
        public bool BuscarInformacion(string identificacion)
        {
            MostrarCliente();
            List<Cliente> ExisteCliente = Clientes.FindAll(cliente => cliente.Identificacion == identificacion);
            if (ExisteCliente.Count() == 0)
            {
                return false;
            }
            cliente = ExisteCliente[0];
            return true;
        }

        public bool BuscarEstadoCliente(string identificacion)
        {
            try
            {
                MostrarCliente();
                List<Cliente> EstadoCliente = Clientes.FindAll(cliente => cliente.Identificacion == identificacion && cliente.Estado == "Inactivo");
                if (EstadoCliente.Count == 0)
                {
                    return false;
                }
                ActualizarCliente("Estado", "Activo", EstadoCliente[0].Identificacion);
                return true;

            }
            catch (Exception )
            {

                throw;
            }
            
        }



        public void RegistarCliente(Cliente cliente) 
        {
            try
            {
                
                using (var comando = Connection.CreateCommand())
                {
                    comando.CommandText = "Insert into Cliente (Nombre, Direccion, Correo, Telefono, Identificacion,Estado) values(@Nombre, @Direccion, @Correo, @Telefono, @Identificacion,@Estado)";
                    comando.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = cliente.Nombre;
                    comando.Parameters.Add("@Direccion", System.Data.SqlDbType.VarChar).Value = cliente.Direccion;
                    comando.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = cliente.Correo;
                    comando.Parameters.Add("@Telefono", System.Data.SqlDbType.VarChar).Value = cliente.Telefono;
                    comando.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar).Value = cliente.Identificacion;
                    comando.Parameters.Add("@Estado", System.Data.SqlDbType.VarChar).Value = cliente.Estado;
                    comando.ExecuteNonQuery();
                }


            }
            catch (Exception)
            {

                throw;
            }
               
            
        }


        public List<Cliente> MostrarClienteEstado(string estado)
        {
            try
            {

                if (estado == "Activo")
                {

                    string Consulta = "select* from Cliente where Estado = 'Activo' ";
                    Ordensql = new SqlCommand(Consulta, Connection);
                    lector = Ordensql.ExecuteReader();
                    while (lector.Read())
                    {
                        cliente = new Cliente
                        {
                            Identificacion = lector["Identificacion"].ToString(),
                            Nombre = lector["Nombre"].ToString(),
                            Correo = lector["Correo"].ToString(),
                            Direccion = lector["Direccion"].ToString(),
                            Estado = lector["Estado"].ToString(),
                            Telefono = lector["Telefono"].ToString()
                        };
                        Clientes.Add(cliente);
                    }
                    return Clientes;
                }
                else
                {
                    string Consulta = "select* from Cliente where Estado = 'Inactivo' ";
                    Ordensql = new SqlCommand(Consulta, Connection);
                    lector = Ordensql.ExecuteReader();
                    while (lector.Read())
                    {
                        cliente = new Cliente
                        {
                            Identificacion = lector["Identificacion"].ToString(),
                            Nombre = lector["Nombre"].ToString(),
                            Correo = lector["Correo"].ToString(),
                            Direccion = lector["Direccion"].ToString(),
                            Estado = lector["Estado"].ToString(),
                            Telefono = lector["Telefono"].ToString()
                        };
                        Clientes.Add(cliente);
                    }
                    return Clientes;

                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<Cliente> BuscarContiene(List<Cliente> clientes, string Identificacion)
        {
            
            return clientes.Where(l => l.Identificacion.Contains(Identificacion)).ToList();
        }






    }
}
