using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class TiempoInvalidoException : Exception
    {
        public TiempoInvalidoException() :base("Su tiempo en la Eps debe ser mayor a 3 meses")
        {
        }
    }
    public class UsuarioNoEncontradoException : Exception
    {
        public UsuarioNoEncontradoException() : base("No se encontró el id del usuario")
        {
        }
    }
}