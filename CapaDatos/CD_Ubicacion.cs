using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.Configuration;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Ubicacion
    {
        //public string Nombre { get; private set; }

        public List<Ciudad> ObtenerCiudad()
        {
            List<Ciudad> lista = new List<Ciudad>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT * FROM ciudad";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Ciudad()
                            {
                                idCiudad = dr["idCiudad"].ToString(),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Ciudad>();
            }

            return lista;
        }




        public List<Sector> ObtenerSector()
        {
            List<Sector> lista = new List<Sector>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT * FROM sector";

                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    oconexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Sector sector = new Sector
                        {
                            idSector = dr["idSector"].ToString(),
                            Nombre = dr["Nombre"].ToString()
                        };
                        lista.Add(sector);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores aquí
            }

            return lista;
        }



    }
}
