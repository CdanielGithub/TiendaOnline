﻿using CapaEntidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CD_Categoria
    {
        public List<Categoria> Listar()
        {

            List<Categoria> lista = new List<Categoria>();

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))

                {
                    MySqlDataReader Variable = null;
                    string query = "select idCategoria, descripcion, activo from categoria";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Categoria()
                                {
                                    idCategoria = Convert.ToInt32(dr["IdCategoria"]),
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
                lista = new List<Categoria>();
            }

            return lista;
        }

        public int Registrar(Categoria obj, out string mensaje)
        {

            int idautogenerado = 0;
            mensaje = string.Empty;
            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarCategoria", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("activo", obj.activo);
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

        public bool Editar(Categoria obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EditarCategoria", oconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.idCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", obj.descripcion);
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

        public bool Eliminar(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (MySqlConnection oconexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarCategoria", oconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", id);
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
