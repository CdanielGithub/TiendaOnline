using CapaEntidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Globalization;

namespace CapaDatos
{
    public class CD_Producto
    {
        /*public List<Producto> Listar()
        {

            List<Producto> lista = new List<Producto>();

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))

                {
                    MySqlDataReader Variable = null;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT p.idProducto, p.nombre, p.descripcion,");
                    sb.AppendLine("m.idMarca, m.descripcion AS DesMarca,");
                    sb.AppendLine("c.idCategoria, c.descripcion AS DesCategoria,");
                    sb.AppendLine("p.precio, p.stock, p.rutaImagen, p.nombreImagen, p.activo");
                    sb.AppendLine("* FROM producto p ");
                    sb.AppendLine("INNER JOIN marca m ON m.idMarca = p.idMArca");
                    sb.AppendLine("INNER JOIN categoria c ON c.idCategoria = p.idCategoria");
                  

                    MySqlCommand cmd = new MySqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Producto()
                                {
                                    idProducto = Convert.ToInt32(dr["IdProducto"]),
                                    nombre = dr["Nombre"].ToString(),
                                    descripcion = dr["Descripcion"].ToString(),
                                    oMarca = new Marca() { idMarca = Convert.ToInt32(dr["idMarca"]), descripcion  = dr["DesMarca"].ToString() },
                                    oCategoria = new Categoria() { idCategoria = Convert.ToInt32(dr["idCategoria"]), descripcion = dr["DesCategoria"].ToString() },
                                    precio = Convert.ToDecimal( dr["precio"], new CultureInfo("en-RD") ),
                                    stock = Convert.ToInt32(dr["stock"]),
                                    rutaImagen = dr["rutaImagen"].ToString(),
                                    nombreImagen = dr["nombreImagen"].ToString(),
                                    activo = Convert.ToBoolean(dr["Activo"])
                                }

                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }

            return lista;
        }*/

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT p.idProducto, p.nombre, p.descripcion,");
                    sb.AppendLine("m.idMarca, m.descripcion AS DesMarca,");
                    sb.AppendLine("c.idCategoria, c.descripcion AS DesCategoria,");
                    sb.AppendLine("p.precio, p.stock, p.rutaImagen, p.nombreImagen, p.activo");
                    sb.AppendLine("FROM producto p ");
                    sb.AppendLine("INNER JOIN marca m ON m.idMarca = p.idMarca");
                    sb.AppendLine("INNER JOIN categoria c ON c.idCategoria = p.idCategoria");

                    MySqlCommand cmd = new MySqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                oMarca = new Marca()
                                {
                                    idMarca = Convert.ToInt32(dr["idMarca"]),
                                    descripcion = dr["DesMarca"].ToString()
                                },
                                oCategoria = new Categoria()
                                {
                                    idCategoria = Convert.ToInt32(dr["idCategoria"]),
                                    descripcion = dr["DesCategoria"].ToString()
                                },
                                precio = Convert.ToDecimal(dr["Precio"], CultureInfo.GetCultureInfo("en-RD")),
                                stock = Convert.ToInt32(dr["Stock"]),
                                rutaImagen = dr["rutaImagen"].ToString(),
                                nombreImagen = dr["nombreImagen"].ToString(),
                                activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }

            return lista;
        }


        public int Registrar(Producto obj, out string mensaje)
        {

            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarProducto", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.idMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.idCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.precio);
                    cmd.Parameters.AddWithValue("Stock", obj.stock);
                    cmd.Parameters.AddWithValue("Activo", obj.activo);
                    cmd.Parameters.Add("resultado", MySqlDbType.Int64).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;


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

        public bool Editar(Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EditarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.idProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.descripcion);
                    cmd.Parameters.AddWithValue("Descripcion", obj.oMarca.idMarca);
                    cmd.Parameters.AddWithValue("IdMarca", obj.descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.idCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.precio);
                    cmd.Parameters.AddWithValue("Stock", obj.stock);
                    cmd.Parameters.AddWithValue("Activo", obj.activo);
                    cmd.Parameters.Add("resultado", MySqlDbType.Int64).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
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

       public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    string query = "UPDATE producto SET rutaImagen = @RutaImagen, nombreImagen = @NombreImagen WHERE idProducto = @IdProducto";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@RutaImagen", obj.rutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", obj.nombreImagen);
                    cmd.Parameters.AddWithValue("@IdProducto", obj.idProducto);

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        mensaje = "No se pudo actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }


       /* public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    string query = "UPDATE producto set rutaImagen = RutaImagen, nombreImagen = NombreImagen where idProducto = IdProducto";

                    //MySqlConnection connection = new MySqlConnection(query);
                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("RutaImagen", obj.rutaImagen);
                    cmd.Parameters.AddWithValue("NombreImagen", obj.nombreImagen);
                    cmd.Parameters.AddWithValue("IdProducto", obj.idProducto);
                   cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                   if( cmd.ExecuteNonQuery() > 0){
                        resultado = true;
                    }
                    else
                    {
                        mensaje = "No se pudo actualizar";
                    }

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return resultado;
        }*/

        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("resultado", MySqlDbType.Int64).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
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
    }
}
