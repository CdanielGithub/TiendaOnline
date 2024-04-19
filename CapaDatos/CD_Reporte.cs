using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using CapaEntidad;
using System.Globalization;


namespace CapaDatos
{
    public class CD_Reporte
    {/*
        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {

            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

                {
                    SqlDataReader Variable = null;
                    

                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    fechaVenta = dr["fechaVenta"].ToString(),
                                    cliente = dr["cliente"].ToString(),
                                    producto = dr["producto"].ToString(),
                                    precio = Convert.ToDecimal(dr["precio"], new CultureInfo("ES-RD")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("ES-RD")),
                                    idTransaccion = dr["idtransaccion"].ToString()
                                });


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Reporte>();
            }

            return lista;
        }

        public DashBoard VerDashBoard()
        {

            DashBoard objeto = new DashBoard();

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))

                {
                    MySqlCommand cmd = new MySqlCommand("sp_ReporteDashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new DashBoard()
                            {

                                 TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                 TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                 TotalProducto = Convert.ToInt32(dr["TotalProducto"])
                            };
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objeto = new DashBoard();
            }

            return objeto;
        */

    }
}
