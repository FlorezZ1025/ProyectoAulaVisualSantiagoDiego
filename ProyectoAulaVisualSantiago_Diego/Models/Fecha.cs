using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class Fecha
    {

        private DateTime fecha_de_nacimiento;
        private int tiempo_en_eps;

        public DateTime Fecha_de_nacimiento { get => fecha_de_nacimiento; set => fecha_de_nacimiento = value; }
        public int Tiempo_en_eps { get => tiempo_en_eps; set => tiempo_en_eps = value; }

        public Fecha(DateTime fecha_de_nacimiento, int tiempo_en_eps)
        {
            this.Fecha_de_nacimiento = fecha_de_nacimiento;
            this.Tiempo_en_eps = tiempo_en_eps;
        }
    }
}