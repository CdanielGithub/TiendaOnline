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
    public class CD_Venta
    {
        public bool Registrar(Venta obj, DataTable DetalleVenta,out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarVenta", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", obj.idCliente);
                    cmd.Parameters.AddWithValue("TotalProducto", obj.TotalProducto);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.montoTotal);
                    cmd.Parameters.AddWithValue("Contacto", obj.contacto);
                    cmd.Parameters.AddWithValue("IdSector", obj.idSector);
                    cmd.Parameters.AddWithValue("Telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("Direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("IdTransaccion", obj.idTransaccion);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {

                respuesta = false;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public List<DetalleVenta> ListarCompras(int idcliente)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from fn_ListarCompra(@idcliente)";

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DetalleVenta()
                            {
                                oProducto = new Producto()
                                {
                                    
                                    nombre = dr["nombre"].ToString(),
                                    precio = Convert.ToDecimal(dr["Precio"],  CultureInfo.GetCultureInfo("en-RD")),
                                    rutaImagen = dr["rutaImagen"].ToString(),
                                    nombreImagen = dr["nombreImagen"].ToString()
                                    
                                },
                                cantidad = Convert.ToInt32(dr["cantidad"]),
                                total = Convert.ToDecimal(dr["total"], CultureInfo.GetCultureInfo("en-RD")),
                                idTransaccion = dr["idTransaccion"].ToString()


                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<DetalleVenta>();
            }

            return lista;
        }
    }
}
