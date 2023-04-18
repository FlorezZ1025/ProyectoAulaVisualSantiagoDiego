using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class Paciente
    {
        private int id;
        private string nombre;
        private string apellido_1;
        private string apellido_2;
        private Fecha fechas;
        private Historial historial;
        private Trabajo trabajo;

        public Paciente(int id, string nombre, string apellido_1, string apellido_2, Fecha fechas, Historial historial, Trabajo trabajo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido_1 = apellido_1;
            this.Apellido_2 = apellido_2;

            this.Fechas = fechas;
            this.Historial = historial;
            this.Trabajo = trabajo;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido_1 { get => apellido_1; set => apellido_1 = value; }
        public string Apellido_2 { get => apellido_2; set => apellido_2 = value; }
       
        public Fecha Fechas { get => fechas; set => fechas = value; }
        public Historial Historial { get => historial; set => historial = value; }
        public Trabajo Trabajo { get => trabajo; set => trabajo = value; }
    }
}