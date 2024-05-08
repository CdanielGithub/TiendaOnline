using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;

using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Carrito
    {
        public bool ExisteCarrito(int idcliente, int idproducto)
        {

            bool resultado = true;
           
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ExisteCarrito", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();


                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                   
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                
            }
            return resultado;
        }

        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string mensaje)
        {

            bool resultado = true;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_OperacionCarrito", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.AddWithValue("Sumar", sumar);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
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

        //select count (*) from carrito WHERE idcliente = 1 

        public int CantidadEnCarrito(int idcliente)
        {
            int resultado = 0;
            
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("select count (*) from carrito WHERE idcliente = @idcliente", oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {

                resultado = 0;
              

            }
            return resultado;
        }

        public List<Carrito> ListarProducto( int idcliente)
        {
            List<Carrito> lista = new List<Carrito>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from fn_obtenerCarritoCliente(@idcliente)";

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Carrito()
                            {
                                oProducto = new Producto()
                                {
                                    idProducto = Convert.ToInt32(dr["idProducto"]),
                                    nombre = dr["nombre"].ToString(),
                                    precio = Convert.ToDecimal(dr["Precio"], CultureInfo.GetCultureInfo("en-RD")),
                                    rutaImagen = dr["rutaImagen"].ToString(),
                                    nombreImagen = dr["nombreImagen"].ToString(),
                                    oMarca = new Marca() { descripcion = dr["DesMarca"].ToString() }
                                },
                                cantidad = Convert.ToInt32(dr["cantidad"])

                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Carrito>();
            }

            return lista;
        }

        public bool EliminarCarrito(int idcliente, int idproducto)
        {

            bool resultado = true;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCarrito", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();


                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);

                }
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            return resultado;
        }

    }
}
