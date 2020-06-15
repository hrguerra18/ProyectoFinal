using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using DAL;
using Infraestructura;
using System.Data.SqlClient;

namespace BLL
{
    public class ClienteService
    {

        public Cliente cliente;
        public List<Cliente> Clientes;
        ClienteRepository ClienteRepository;
        CreditoService creditoService;
        private ConnectionManager connection;


        public ClienteService(string connectionString)
        {

            connection = new ConnectionManager(connectionString);
            ClienteRepository = new ClienteRepository(connection);
            creditoService = new CreditoService(connectionString);

        }

        public List<Cliente> MostrarClientes()
        {
            try
            {
                connection.Open();
                Clientes = ClienteRepository.MostrarCliente();
                return Clientes;
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                connection.Closed();
            }

            
        }
        public void ActualizarEstado(String Identificacion)
        {
            try
            {
                connection.Open();
                ClienteRepository.ActualizarEstado(Identificacion);
                

            }
            catch (Exception  )
            {

                throw;
            }
            finally 
            {
                connection.Closed();

            }



            
        }
        public String Registrar(Cliente cliente, Credito credito)
        {

            try
            {
                Correo correo = new Correo();
                String mensajeCorreo;
                connection.Open();
                if (ClienteRepository.BuscarExisteCliente(cliente.Identificacion))
                {
                    if (ClienteRepository.BuscarEstadoCliente(cliente.Identificacion))
                    {
                        creditoService.Registrar(credito);

                        mensajeCorreo = correo.EnviarEmail(cliente);
                        
                        return $"Ya Existe un cliente registrado con esta identificacion pero su estado es Inactivo. Se volvio activo y se le hizo el prestamo. {mensajeCorreo}";
                    }
                    else
                    {
                       
                        return "Ya existe un cliente con esta identificacion pero su estado es activo, asi que no se puede hacer el prestamo.";
                    }
                }
                mensajeCorreo = correo.EnviarEmail(cliente);
                ClienteRepository.RegistarCliente(cliente);
                creditoService.Registrar(credito);
               
                return $"Registrado correctamente. {mensajeCorreo}";

            }
            catch (Exception e)
            {

                return ("no se pudo registar el clinete" + e.Message);
            }
            finally 
            {
                connection.Closed();
            }  

            
        }

        public bool BuscarInformacion(String Identificacion)
        {
            try
            {
                connection.Open();
                if (ClienteRepository.BuscarInformacion(Identificacion))
                {
                    connection.Closed();
                    return true;
                }
                
                return false;

            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                connection.Closed();
            }

          
        }

        public void GenerarExcel()
        {
            Exel excel = new Exel();
            excel.GenerarReporteClientes(Clientes);

        }
        public Cliente InformacionCliente()
        {
            return ClienteRepository.cliente;
        }



        public List<Cliente> MostrarEstado( string estado) 
        {
            try
            {
                connection.Open();
                Clientes = ClienteRepository.MostrarClienteEstado(estado);
                return Clientes;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Closed();
            }




        }

        public IList<Cliente> BuscarContiene(List<Cliente> clientes, string Identificacion)
        {

            return ClienteRepository.BuscarContiene(clientes, Identificacion);
        }





    }
}
