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
    }
}
