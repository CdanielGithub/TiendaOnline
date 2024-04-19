using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

using System.Net.Mail;// Estas referencias las estoy usando para usar las clases para enviar un correo
using System.Net;
using System.IO;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);

            return clave;
        }

        //Encriptar contraseña
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                   Sb.Append(b.ToString("x2"));
                
            }
            return Sb.ToString();
        }

        // Este metodo fue creado para enviar la contraseña a los usuarios
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            // Configuración del correo
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("sistemaweb.exe@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                // Aquí se realiza la configuaración para enviar el mensaje usando el servidor de Gmail
                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("sistemaweb.exe@gmail.com", "ipee o srk ialp fwxk"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                };

                smtp.Send(mail);
                resultado = true;

            }catch (Exception ex) {
                resultado = false;
            }
            return resultado;
        }

        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textoBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);
            }catch (Exception ex)
            { 
                        conversion = false;
            }

            return textoBase64;
        }
    }
}
