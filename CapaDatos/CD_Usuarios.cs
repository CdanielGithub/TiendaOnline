using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

using System.Data.SqlClient;
using System.Data;


namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {

            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

                {
                   // SqlDataReader Variable = null;
                    string query = "select idUsuario, nombre, apellidos, correo, clave, reestablecer, activo from usuario";

                    SqlCommand cmd = new SqlCommand (query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Usuario()
                                {
                                    idUsuario = Convert.ToInt32(dr["idUsuario"]),
                                    nombre = dr["nombre"].ToString(),
                                    apellidos = dr["apellidos"].ToString(),
                                    correo = dr["correo"].ToString(),
                                    clave = dr["clave"].ToString(),
                                    reestablecer = Convert.ToBoolean(dr["reestablecer"]),
                                    activo = Convert.ToBoolean(dr["activo"])
                                }

                                );
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                lista = new List<Usuario>();
            }

            return lista;
        }


        public int Registrar(Usuario obj, out string mensaje) { 
        
            int idautogenerado = 0;
            mensaje = string .Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oconexion);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.correo);
                    cmd.Parameters.AddWithValue("Clave", obj.clave);
                    cmd.Parameters.AddWithValue("Activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();


                    idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }catch(Exception ex)
            {
                idautogenerado = 0;
                mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(Usuario obj, out string mensaje)
         {
               bool resultado = false;
               mensaje = string.Empty;

             try
             {
                 using(SqlConnection oconexion = new SqlConnection(Conexion.cn))
                 {
                     SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oconexion);
                     cmd.Parameters.AddWithValue("idUsuario", obj.idUsuario);
                     cmd.Parameters.AddWithValue("Nombre", obj.nombre);
                     cmd.Parameters.AddWithValue("Apellidos", obj.apellidos);
                     cmd.Parameters.AddWithValue("Correo", obj.correo);
                     cmd.Parameters.AddWithValue("Activo", obj.activo);
                     cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                     cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                     cmd.CommandType = CommandType.StoredProcedure;

                     oconexion.Open();

                     cmd.ExecuteNonQuery();

                     resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                     mensaje = cmd.Parameters["mensaje"].Value.ToString();

                 }

             }catch(Exception ex) {

                 resultado = false;
                 mensaje = ex.Message;
             }

             return resultado;
         }



        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using(SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("DELETE top (1) FROM usuario WHERE idUsuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }catch (Exception ex) { 
                
                resultado = false; 
                mensaje = ex.Message;
            
            }
            return resultado;
        }

        public bool CambiarClave(int idusuario, string nuevaclave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE usuario SET clave = @nuevaclave, reestablecer = 0 WHERE idUsuario = @id", oconexion);

                    
                    cmd.Parameters.AddWithValue("@id", idusuario);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }

        public bool ReestablecerClave(int idusuario, string clave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE usuario set clave = @clave, reestablecer = 1 WHERE idUsuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
                    cmd.Parameters.AddWithValue("@nuevaclave", clave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }


    }
}
