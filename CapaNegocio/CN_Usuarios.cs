using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar()
        {

            return objCapaDato.Listar();
        }

        public int Registrar(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if(string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre) ) {

                mensaje = "El nombre del usuario no puede estar vacío";
            }
            else if(string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {

                mensaje = "El apellido del usuario no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {

                mensaje = "El correo del usuario no puede estar vacío";
            }

            if (string.IsNullOrEmpty(mensaje))
            {

                string clave = CN_Recursos.GenerarClave();

                string asunto = "Creación de cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.correo, asunto, mensaje_correo);

                if (respuesta) // Si la respuesta es correcta vamos a actualizar la clave y se procede a registar el usuario
                {
                    obj.clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out mensaje);
                }
                else// En caso de error se retorna un mensaje
                {
                    mensaje = "No se puede enviar el correo";
                    return 0;
                }

               // obj.clave = CN_Recursos.ConvertirSha256(clave);

               // return objCapaDato.Registrar(obj, out mensaje);
            }
            else
            {
                return 0;
            }

            
        }

        public bool Editar(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {

                mensaje = "El nombre del usuario no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(obj.apellidos) || string.IsNullOrWhiteSpace(obj.apellidos))
            {

                mensaje = "El apellido del usuario no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {

                mensaje = "El correo del usuario no puede estar vacío";
            }


            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.Editar(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDato.Eliminar(id, out mensaje);   
        }

        public bool CambiarClave(int idusuario, string nuevaclave, out string mensaje)
        {
            return objCapaDato.CambiarClave(idusuario, nuevaclave, out mensaje);
        }


        public bool ReestablecerClave(int idusuario, string correo, out string mensaje)
        {
            mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idusuario, CN_Recursos.ConvertirSha256(nuevaclave), out mensaje);


            //Validar la clave
            if(resultado)
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
