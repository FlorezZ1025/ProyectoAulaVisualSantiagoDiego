using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class Trabajo
    {
        private string tipo_de_regimen;
        private string afiliacion;
        private int costo_Tratamiento;

        public string Tipo_de_regimen { get => tipo_de_regimen; set => tipo_de_regimen = value; }
        public string Afiliacion { get => afiliacion; set => afiliacion = value; }
        public int Costo_Tratamiento { get => costo_Tratamiento; set => costo_Tratamiento = value; }

        public Trabajo(string tipo_de_regimen, string afiliacion, int costo_Tratamiento)
        {
            this.Tipo_de_regimen = tipo_de_regimen;
            this.Afiliacion = afiliacion;
            this.Costo_Tratamiento = costo_Tratamiento;
        }
    }
}