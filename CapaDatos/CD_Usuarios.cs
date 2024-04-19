using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {

            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))

                {
                    MySqlDataReader Variable = null;
                    string query = "select idUsuario, nombre, apellidos, correo, clave, reestablecer, activo from usuario";

                    MySqlCommand cmd = new MySqlCommand (query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using(MySqlDataReader dr = cmd.ExecuteReader())
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
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarUsuario", oconexion);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("Apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.correo);
                    cmd.Parameters.AddWithValue("Clave", obj.clave);
                    cmd.Parameters.AddWithValue("Activo", obj.activo);
                    cmd.Parameters.Add("resultado", MySqlDbType.Int64).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", MySqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    //cmd.CommandType = CommandType.StoredProcedure;

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
                 using(MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                 {
                     MySqlCommand cmd = new MySqlCommand("sp_EditarUsuario", oconexion);
                     cmd.Parameters.AddWithValue("idUsuario", obj.idUsuario);
                     cmd.Parameters.AddWithValue("Nombre", obj.nombre);
                     cmd.Parameters.AddWithValue("Apellidos", obj.apellidos);
                     cmd.Parameters.AddWithValue("Correo", obj.correo);
                     cmd.Parameters.AddWithValue("Activo", obj.activo);
                     cmd.Parameters.Add("resultado", MySqlDbType.Int64).Direction = ParameterDirection.Output;
                     cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
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
                using(MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM usuario WHERE idUsuario = @id LIMIT 1", oconexion);
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
    }
}
