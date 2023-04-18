using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class Clinica
    {
        public static List<Paciente> pacientes = new List<Paciente>();


        public List<Paciente> Pacientes { get => pacientes; set => pacientes = value; }

        public void guardarPaciente(Paciente paciente)
        {
            pacientes.Add(paciente);
        }

        public Paciente EncontrarPaciente(int id)
        {
            foreach (Paciente paciente in pacientes)
            {
                if (paciente.Id == id)
                {
                    return paciente;
                }
            }
            return null;
        }


        public void CambioEps(Paciente paciente, string eps)
        {
            paciente.Historial.Eps = eps;
            paciente.Fechas.Tiempo_en_eps = 0;
        }

        public void CambioHistoriaClinica(Paciente paciente, string historia)
        {
            paciente.Historial.Historia_clínica = historia;
        }

        public void CambioCostoTratamiento(Paciente paciente, int costo)
        {
            paciente.Trabajo.Costo_Tratamiento = costo;
        }

        public void CambioRegimen(Paciente paciente, string regimen)
        {
            paciente.Trabajo.Tipo_de_regimen = regimen;
        }

        public void CambioEnfermedad_r(Paciente paciente, string e_relevante)
        {
            paciente.Historial.Enfermedad_relevante = e_relevante;
        }
        public static List<double> Porcentaje_costos(List<Paciente> Lista_de_pacientes)
        {
            List<double> Costos_Sura = new List<double>();
            List<double> Costos_NuevaEPS = new List<double>();
            List<double> Costos_SaludTotal = new List<double>();
            List<double> Costos_Sanitas = new List<double>();
            List<double> Costos_Savia = new List<double>();
            List<double> lista_porcentajes = new List<double>();

            double porcentaje_Sura       = 0;
            double porcentaje_NuevaEPS   = 0;
            double porcentaje_SaludTotal = 0;
            double porcentaje_Sanitas    = 0;
            double porcentaje_Savia      = 0;

            foreach (Paciente paciente in Lista_de_pacientes)
            {
                if (paciente.Historial.Eps == "Sura") { Costos_Sura.Add(paciente.Trabajo.Costo_Tratamiento); }
                else if (paciente.Historial.Eps == "Nueva Eps") { Costos_NuevaEPS.Add(Convert.ToDouble(paciente.Trabajo.Costo_Tratamiento)); }
                else if (paciente.Historial.Eps == "Salud Total") { Costos_SaludTotal.Add(Convert.ToDouble(paciente.Trabajo.Costo_Tratamiento)); }
                else if (paciente.Historial.Eps == "Sanitas") { Costos_Sanitas.Add(Convert.ToDouble(paciente.Trabajo.Costo_Tratamiento)); }
                else if (paciente.Historial.Eps == "Savia") { Costos_Savia.Add(Convert.ToDouble(paciente.Trabajo.Costo_Tratamiento)); }

            }
            double costo_total = Costos_Sura.Sum() + Costos_NuevaEPS.Sum() + Costos_SaludTotal.Sum() + Costos_Sanitas.Sum() + Costos_Savia.Sum();
             
            porcentaje_Sura       = Calcular_porcentaje_costos(Lista_de_pacientes,Costos_Sura, costo_total);
            porcentaje_NuevaEPS   = Calcular_porcentaje_costos(Lista_de_pacientes, Costos_NuevaEPS, costo_total);
            porcentaje_SaludTotal = Calcular_porcentaje_costos(Lista_de_pacientes, Costos_SaludTotal, costo_total);
            porcentaje_Sanitas    = Calcular_porcentaje_costos(Lista_de_pacientes, Costos_Sanitas, costo_total);
            porcentaje_Savia      = Calcular_porcentaje_costos(Lista_de_pacientes, Costos_Savia, costo_total);

            lista_porcentajes.Add(porcentaje_Sura);
            lista_porcentajes.Add(porcentaje_NuevaEPS);
            lista_porcentajes.Add(porcentaje_SaludTotal);
            lista_porcentajes.Add(porcentaje_Sanitas);
            lista_porcentajes.Add(porcentaje_Savia);
            return lista_porcentajes;
        }
        public static List<double> Calcular_costos_por_eps(List<Paciente> Pacientes)
        {

            double total_costos_Sura       = 0;
            double total_costos_NuevaEPS   = 0;
            double total_costos_SaludTotal = 0;
            double total_costos_Sanitas    = 0;
            double total_costos_Savia      = 0;

            var costos_sura        = Pacientes.Where(x => x.Historial.Eps == "Sura").ToList();
            var costos_nueva_eps   = Pacientes.Where(x => x.Historial.Eps == "Nueva Eps").ToList();
            var costos_salud_total = Pacientes.Where(x => x.Historial.Eps == "Salud Total").ToList();
            var costos_sanitas     = Pacientes.Where(x => x.Historial.Eps == "Sanitas").ToList();
            var costos_savia       = Pacientes.Where(x => x.Historial.Eps == "Savia").ToList();

            total_costos_Sura       = Sumar_costos(costos_sura);
            total_costos_NuevaEPS   = Sumar_costos(costos_nueva_eps);
            total_costos_SaludTotal = Sumar_costos(costos_salud_total);
            total_costos_Sanitas    = Sumar_costos(costos_sanitas);
            total_costos_Savia      = Sumar_costos(costos_savia);
            List<double> Costos_totales = new List<double> { total_costos_Sura, total_costos_NuevaEPS, total_costos_SaludTotal, total_costos_Sanitas, total_costos_Savia };

            return Costos_totales;



        }
        public static double Porcentaje_sin_enfermedades(List<Paciente> Pacientes)
        {
            double porcentaje_pacientes_sin_enfermedades;

            var pacientes_sin_enfermedades = Pacientes.Where(x => x.Historial.Cantidad_Enfermedades == 0).ToList();

            porcentaje_pacientes_sin_enfermedades = Calcular_porcentaje(Pacientes, pacientes_sin_enfermedades);

            return porcentaje_pacientes_sin_enfermedades;

        }
        public static List<Paciente> Paciente_mayor_costo_tratamiento(List<Paciente> Pacientes)
        {
            var paciente_mayor_costo = Pacientes.Where(x => x.Trabajo.Costo_Tratamiento == (Pacientes.Max(y => y.Trabajo.Costo_Tratamiento))).ToList();


            return paciente_mayor_costo;
        }


        public static List<double> Rango_de_edad(List<Paciente> Pacientes)
        {
            DateTime fecha_actual = DateTime.Now.Date;

            List<double> ListaRangosEdad = new List<double>();

            double porcentaje_niños          = Clinica.Porcentaje_niños(Pacientes,fecha_actual);
            double porcentaje_adolescentes    = Clinica.Porcentaje_adolescentes(Pacientes, fecha_actual);
            double porcentaje_joven           = Clinica.Porcentaje_jovenes(Pacientes, fecha_actual);
            double porcentaje_adultos         = Clinica.Porcentaje_adultos(Pacientes, fecha_actual);
            double porcentaje_adultos_mayores = Clinica.Porcentaje_adultos_mayores(Pacientes, fecha_actual);
            double porcentaje_ancianos        = Clinica.Porcentaje_ancianos(Pacientes, fecha_actual);

            ListaRangosEdad.Add(porcentaje_niños);
            ListaRangosEdad.Add(porcentaje_adolescentes);
            ListaRangosEdad.Add(porcentaje_joven);
            ListaRangosEdad.Add(porcentaje_adultos);
            ListaRangosEdad.Add(porcentaje_adultos_mayores);
            ListaRangosEdad.Add(porcentaje_ancianos);

            return ListaRangosEdad;

        }


        //-------------------------------------Porcentajes por edad ---------------------------------------------------------------------

        public static double Porcentaje_niños(List<Paciente> Pacientes, DateTime fecha_actual)
        {
            var pacientes_adolescentes = Pacientes.Where(x => (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) < 12).ToList();



            double porcentaje_pacientes_adolescentes = Calcular_porcentaje(Pacientes, pacientes_adolescentes);

            return porcentaje_pacientes_adolescentes;
        }


            public static double Porcentaje_adolescentes(List<Paciente> Pacientes, DateTime fecha_actual)
        {
            var pacientes_adolescentes = Pacientes.Where(x => (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) >= 12 &&
            (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) < 18).ToList();



            double porcentaje_pacientes_adolescentes = Calcular_porcentaje(Pacientes, pacientes_adolescentes);

            return porcentaje_pacientes_adolescentes;
        }

        public static double Porcentaje_jovenes(List<Paciente> Pacientes, DateTime fecha_actual)
        {

            var pacientes_jovenes = Pacientes.Where(x => (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) >= 18 &&
            (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) < 30).ToList();

            double porcentaje_pacientes_jovenes = Calcular_porcentaje(Pacientes, pacientes_jovenes);

            return porcentaje_pacientes_jovenes;
        }

        public static double Porcentaje_adultos(List<Paciente> Pacientes, DateTime fecha_actual)
        {

            var pacientes_adultos = Pacientes.Where(x => (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) >= 30 && (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) < 55).ToList();

            double porcentaje_pacientes_adulto = Calcular_porcentaje(Pacientes, pacientes_adultos);

            return porcentaje_pacientes_adulto;
        }

        public static double Porcentaje_adultos_mayores(List<Paciente> Pacientes, DateTime fecha_actual)
        {

            var pacientes_adultos_mayores = Pacientes.Where(x => (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) >= 55 && (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) < 75).ToList();

            double porcentaje_pacientes_adultos_mayores = Calcular_porcentaje(Pacientes, pacientes_adultos_mayores);

            return porcentaje_pacientes_adultos_mayores;
        }

        public static double Porcentaje_ancianos(List<Paciente> Pacientes, DateTime fecha_actual)
        {

            var pacientes_ancianos = Pacientes.Where(x => (fecha_actual.Year - x.Fechas.Fecha_de_nacimiento.Year) >= 75).ToList();

            double porcentaje_pacientes_ancianos = Calcular_porcentaje(Pacientes, pacientes_ancianos);

            return porcentaje_pacientes_ancianos;
        }

        //-------------------------------------------------------------------------------------------------------------

        //--------------------------------Porcentajes tipo de regimen-----------------------------------------------------------------------------

        public static List<double> Porcentajes_regimen(List<Paciente> pacientes)
        {
            List<double> porcentajes_regimen = new List<double>();

            porcentajes_regimen.Add(Porcentaje_regimen_contributivo(pacientes));

            porcentajes_regimen.Add(Porcentaje_regimen_subsidiado(pacientes));

            return porcentajes_regimen;

        }


        public static double Porcentaje_regimen_contributivo(List<Paciente> Pacientes)
        {
            double porcentaje_regimen_contributivo;

            var pacientes_regimen_contributivo = Pacientes.Where(x => x.Trabajo.Tipo_de_regimen == "Contributivo").ToList();

            porcentaje_regimen_contributivo = Calcular_porcentaje(Pacientes, pacientes_regimen_contributivo);

            return porcentaje_regimen_contributivo;
        }

        public static double Porcentaje_regimen_subsidiado(List<Paciente> Pacientes)
        {
            double porcentaje_regimen_subsidiado;

            var pacientes_regimen_subsidiado = Pacientes.Where(x => x.Trabajo.Tipo_de_regimen == "Subsidiado").ToList();

            porcentaje_regimen_subsidiado = Calcular_porcentaje(Pacientes, pacientes_regimen_subsidiado);

            return porcentaje_regimen_subsidiado;
        }

        //-------------------------------------------------------------------------------------------------------------



        //--------------------------------Porcentajes tipo de afiliacion-----------------------------------------------------------------------------
        public static List<double> CalcularPorcentajesAfiliacion(List<Paciente> Pacientes)
        {
            List<double> porcentajesafiliacion = new List<double> {Porcentaje_afiliacion_cotizante(pacientes),Porcentaje_afiliacion_beneficiarios(Pacientes) };

            return porcentajesafiliacion;
        }

        public static double Porcentaje_afiliacion_cotizante(List<Paciente> Pacientes)
        {
            double porcentaje_afiliacion_cotizante;

            var pacientes_cotizantes = Pacientes.Where(x => x.Trabajo.Afiliacion == "Cotizante").ToList();

            porcentaje_afiliacion_cotizante = Calcular_porcentaje(Pacientes, pacientes_cotizantes);

            return porcentaje_afiliacion_cotizante;
        }

        public static double Porcentaje_afiliacion_beneficiarios(List<Paciente> Pacientes)
        {
            double porcentaje_afiliacion_beneficiarios;

            var pacientes_beneficiarios = Pacientes.Where(x => x.Trabajo.Afiliacion == "Beneficiario").ToList();

            porcentaje_afiliacion_beneficiarios = Calcular_porcentaje(Pacientes, pacientes_beneficiarios);

            return porcentaje_afiliacion_beneficiarios;
        }

        //-------------------------------------------------------------------------------------------------------------


        private static double Calcular_promedio(List<int> lista_costos)
        {
            double total = 0;
            for (int i = 0; i < lista_costos.Count(); i++)
            {
                total += lista_costos[i];
            }
            if (lista_costos.Count() > 0) { return total / lista_costos.Count(); }
            else { return 0; }


        }
        public static int Total_pacientes_con_cancer(List<Paciente> Pacientes)
        {
            int total_pacientes_con_cancer;

            var pacientes_con_cancer = Pacientes.Where(x => x.Historial.Enfermedad_relevante.Trim().ToLower() == "cancer").ToList();

            total_pacientes_con_cancer = pacientes_con_cancer.Count();

            return total_pacientes_con_cancer;
        }
        private static double Sumar_costos(List<Paciente> pacientes)
        {
            double total = 0;
            foreach (Paciente paciente in pacientes)
            {
                total += paciente.Trabajo.Costo_Tratamiento;
            }
            return total;
        }


        private static double Calcular_porcentaje(List<Paciente> pacientes, List<Paciente> pacientes_sin)
        {

            double porcentaje;
            double pacientes_totales = Convert.ToDouble(pacientes.Count);
            double pacientes_especificos = Convert.ToDouble(pacientes_sin.Count);
            try
            {
                porcentaje = Math.Round(((pacientes_especificos / pacientes_totales) * 100.0), 2);

            }
            catch(DivideByZeroException ex) {
                string a = ex.Message;
                porcentaje = 0;
            }
            return porcentaje;
        
        }
        private static double Calcular_porcentaje_costos(List<Paciente> pacientes, List<double> costos, double costo_total)
        {
            if (pacientes.Count == 0)
            {
                return 0;
            }
            else
            {
                double porcentaje = Math.Round((costos.Sum() / costo_total) * 100,2);
                 
                return porcentaje;
            }

        }
    }
}