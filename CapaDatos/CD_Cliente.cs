using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CapaDatos
{
    public class CD_Cliente
    {

        public int Registrar(Cliente obj, out string mensaje)
        {

            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.correo);
                    cmd.Parameters.AddWithValue("Clave", obj.clave);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

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

        public List<Cliente> Listar()
        {

            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))

                {
                    // SqlDataReader Variable = null;
                    string query = "select idCliente, nombres, apellidos, correo, clave, restablecer from cliente";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Cliente()
                                {
                                    idCliente = Convert.ToInt32(dr["idCliente"]),
                                    nombres = dr["Nombres"].ToString(),
                                    apellidos = dr["Apellidos"].ToString(),
                                    correo = dr["Correo"].ToString(),
                                    clave = dr["Clave"].ToString(),
                                    restablecer = Convert.ToBoolean(dr["restablecer"]),
                                  
                                }

                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Cliente>();
            }

            return lista;
        }

        public bool CambiarClave(int idcliente, string nuevaclave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE cliente SET clave = @nuevaclave, restablecer = 0 WHERE idCliente = @id", oconexion);

                    cmd.Parameters.AddWithValue("@id", idcliente);
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


        public bool ReestablecerClave(int idcliente, string clave, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE cliente set clave = @clave, restablecer = 1 WHERE idCliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idcliente);
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
