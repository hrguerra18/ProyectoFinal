using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructura;

namespace BLL
{
    public class EnviosService
    {
        CorreoArchivos correoArchivos;
        public EnviosService()
        {
            correoArchivos = new CorreoArchivos();
        }


        public void enviar(string mensaje, string asunto, string destinatario, string ruta)
        {
            correoArchivos.enviarCorreo(mensaje , asunto, destinatario, ruta);
        
        }


    }
}
