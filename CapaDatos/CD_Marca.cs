using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Org.BouncyCastle.Utilities.Zlib;
using System.Text.RegularExpressions;

namespace CapaDatos
{
    public class CD_Marca
    {
        public List<Marca> Listar()
        {

            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

                {
                   // SqlDataReader Variable = null;
                    string query = "select idMarca, descripcion, activo from marca";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Marca()
                                {
                                    idMarca = Convert.ToInt32(dr["IdMarca"]),
                                    descripcion = dr["Descripcion"].ToString(),
                                    activo = Convert.ToBoolean(dr["Activo"])
                                }

                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Marca>();
            }

            return lista;
        }

        public int Registrar(Marca obj, out string mensaje)
        {

            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMarca", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;


                    oconexion.Open();

                    cmd.ExecuteNonQuery();


                    idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(Marca obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarMarca", oconexion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.idMarca);
                    cmd.Parameters.AddWithValue("Descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.activo);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {

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
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarMarca", oconexion);
                    cmd.Parameters.AddWithValue("IdMarca", id);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {

                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }

        public List<Marca> ListarMarcaporCategoria( int idcategoria)
        {

            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

                {
                    // SqlDataReader Variable = null;
                   StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select distinct m.idMarca, m.descripcion from producto p");
                    sb.AppendLine("INNER JOIN categoria c on c.idCategoria = p.idCategoria");
                    sb.AppendLine("INNER JOIN marca m on m.idMarca = p.idMarca and m.activo = 1");
                    sb.AppendLine("WHERE c.idCategoria = iif(@idcategoria = 0, c.idCategoria, @idcategoria)");

                    //                    DECLARE @idcategoria int  = 0
                    //
                    //
                    //
                    //

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Marca()
                                {
                                    idMarca = Convert.ToInt32(dr["idMarca"]),
                                    descripcion = dr["Descripcion"].ToString()
                                    
                                }

                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Marca>();
            }

            return lista;
        }

        // Final
    }
}
