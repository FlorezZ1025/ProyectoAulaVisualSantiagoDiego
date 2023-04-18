using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ProyectoAulaVisualSantiago_Diego.Models.Enum;
using ProyectoAulaVisualSantiago_Diego.Models;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class UsuarioNoEncontradoException : Exception
    {
        public UsuarioNoEncontradoException() : base("No se encontró el id del usuario")
        {
        }
    }
}