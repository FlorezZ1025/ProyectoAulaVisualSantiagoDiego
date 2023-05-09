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
        private DateTime fecha_de_nacimiento;
        private DateTime tiempo_en_eps;
        private Historial historial;
        private Trabajo trabajo;

        public Paciente(int id, string nombre, string apellido_1, string apellido_2, DateTime fecha_de_nacimiento, DateTime tiempo_en_eps, Historial historial, Trabajo trabajo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido_1 = apellido_1;
            this.Apellido_2 = apellido_2;
            this.Fecha_de_nacimiento = fecha_de_nacimiento;
            this.Tiempo_en_eps = tiempo_en_eps;
            this.Historial = historial;
            this.Trabajo = trabajo;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido_1 { get => apellido_1; set => apellido_1 = value; }
        public string Apellido_2 { get => apellido_2; set => apellido_2 = value; }
        public DateTime Fecha_de_nacimiento { get => fecha_de_nacimiento; set => fecha_de_nacimiento = value; }
        public DateTime Tiempo_en_eps { get => tiempo_en_eps; set => tiempo_en_eps = value; }
        public Historial Historial { get => historial; set => historial = value; }
        public Trabajo Trabajo { get => trabajo; set => trabajo = value; }
    }
}