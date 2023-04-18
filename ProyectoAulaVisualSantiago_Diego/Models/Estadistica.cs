using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class Estadistica
    {
        public List<double> porcentajeCostosEps;
        public List<double> totalCostosEps;
        public double pacientesNoEnfermos;
        public Paciente pacienteMayorcosto;
        public List<double> porcentajesRangoEdad;
        public List<double> porcentajesRegimen;
        public int pacientesCancer = 0;
        public List<double> porcentajesAfiliacion;
        public Estadistica(List<double> porcentajeCostosEps, List<double> totalCostosEps, double pacientesNoEnfermos, Paciente pacienteMayorcosto,
            List<double> porcentajesRangoEdad, List<double> porcentajesRegimen, int pacientesCancer, List<double> porcentajesAfiliacion)
        {
            this.porcentajeCostosEps = porcentajeCostosEps;
            this.totalCostosEps = totalCostosEps;
            this.pacientesNoEnfermos = pacientesNoEnfermos;
            this.pacienteMayorcosto = pacienteMayorcosto; 
            this.porcentajesRangoEdad = porcentajesRangoEdad;
            this.porcentajesRegimen = porcentajesRegimen;
            this.pacientesCancer = pacientesCancer;
            this.porcentajesAfiliacion = porcentajesAfiliacion;
        }
    }


}
