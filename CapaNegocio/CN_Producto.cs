using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del Producto no puede estar vacía";
            }

            else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "La descripción del Producto no puede estar vacío";
            }

           else if (obj.oMarca.idMarca == 0)
            {
                mensaje = "Debe seleccionar una marca";
            }

           else if (obj.oCategoria.idCategoria == 0)
            {
                mensaje = "Debe seleccionar una categoría";
            }

           else if (obj.precio == 0)
            {
                mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.stock == 0)
            {
                mensaje = "Debe ingresar el Stock del producto";
            }


            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.Registrar(obj, out mensaje);
            }
            else
            {
                return 0;
            }


        }

        public bool Editar(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del Producto no puede estar vacía";
            }

            else if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                mensaje = "La descripción del Producto no puede estar vacío";
            }

            else if (obj.oMarca.idMarca == 0)
            {
                mensaje = "Debe seleccionar una marca";
            }

            else if (obj.oCategoria.idCategoria == 0)
            {
                mensaje = "Debe seleccionar una categoría";
            }

            else if (obj.precio == 0)
            {
                mensaje = "Debe ingresar el precio del producto";
            }
            else if (obj.stock == 0)
            {
                mensaje = "Debe ingresar el Stock del producto";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.Editar(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarDatosImagen(Producto obj, out string mensaje)
        {
           return objCapaDato.GuardarDatosImagen(obj, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDato.Eliminar(id, out mensaje);
        }
    }
}
