using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCapaDato = new CD_Cliente();



        public int Registrar(Cliente obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombres) || string.IsNullOrWhiteSpace(obj.nombres))
            {

                mensaje = "El nombre del Cliente no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {

                mensaje = "El apellido del Cliente no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {

                mensaje = "El correo del Cliente no puede estar vacío";
            }

            if (string.IsNullOrEmpty(mensaje))
            {

                obj.clave = CN_Recursos.ConvertirSha256(obj.clave);
                return objCapaDato.Registrar(obj, out mensaje);

               
            }
            else
            {
                return 0;
            }


        }

        public List<Cliente> Listar()
        {

            return objCapaDato.Listar();
        }


        public bool CambiarClave(int idcliente, string nuevaclave, out string mensaje)
        {
            return objCapaDato.CambiarClave(idcliente, nuevaclave, out mensaje);
        }


        public bool ReestablecerClave(int idcliente, string correo, out string mensaje)
        {
            mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idcliente, CN_Recursos.ConvertirSha256(nuevaclave), out mensaje);


            //Validar la clave
            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su contraseña fue reestablecida correctamente</h3></br><p>Su contraseña para acceder  ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta) // Si la respuesta es correcta vamos a actualizar la clave y se procede a registar el usuario
                {
                    return true;

                }
                else// En caso de error se retorna un mensaje
                {
                    mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }
        }

       


    }
}
