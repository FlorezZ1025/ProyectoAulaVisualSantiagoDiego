using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class Historial
    {
        private string eps;
        private string historia_clínica;
        private double cantidad_Enfermedades;
        private string enfermedad_relevante;

        public string Eps { get => eps; set => eps = value; }
        public string Historia_clínica { get => historia_clínica; set => historia_clínica = value; }
        public double Cantidad_Enfermedades { get => cantidad_Enfermedades; set => cantidad_Enfermedades = value; }
        public string Enfermedad_relevante { get => enfermedad_relevante; set => enfermedad_relevante = value; }

        public Historial(string eps, string historia_clínica, double cantidad_Enfermedades, string enfermedad_relevante)
        {
            this.Eps = eps;
            this.Historia_clínica = historia_clínica;
            this.Cantidad_Enfermedades = cantidad_Enfermedades;
            this.Enfermedad_relevante = enfermedad_relevante;
        }
    }
}