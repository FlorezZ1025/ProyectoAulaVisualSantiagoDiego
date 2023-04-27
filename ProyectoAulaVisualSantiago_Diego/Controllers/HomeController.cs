using ProyectoAulaVisualSantiago_Diego.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static ProyectoAulaVisualSantiago_Diego.Models.Enum;

namespace ProyectoAulaVisualSantiago_Diego.Controllers
{

    public class HomeController : SweetController
    {

        Clinica miclinica = new Clinica();

        public ActionResult ActualizarInformacion()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Ver()
        {
            return View();
        }


        public ActionResult MostrarEstadisticas()
        {
           
            List<double> porcentaje_costos      = Clinica.Porcentaje_costos(miclinica.Pacientes);
            List<double> totalCostosEps         = Clinica.Calcular_costos_por_eps(miclinica.Pacientes);
            List<Paciente> pacientes_mayorCosto = Clinica.Paciente_mayor_costo_tratamiento(miclinica.Pacientes);
            double pacientesNoEnfermos          = Clinica.Porcentaje_sin_enfermedades(miclinica.Pacientes);
            List<double> porcentajes_Edad       = Clinica.Rango_de_edad(miclinica.Pacientes);
            List<double> porcentajesRegimen     = Clinica.Porcentajes_regimen(miclinica.Pacientes);
            int totalPacientesCancer            = Clinica.Total_pacientes_con_cancer(miclinica.Pacientes);
            List<double> porcentajesAfiliacion  = Clinica.CalcularPorcentajesAfiliacion(miclinica.Pacientes);
            
            if(pacientes_mayorCosto.Count == 0)
            {
                //Fecha f     = new Fecha(DateTime.Now,3);
                //Trabajo t   = new Trabajo("", "", 0);
                //Historial h = new Historial("", "", 0, "");
                //Paciente paciente_nulo = new Paciente(0,"No","hay","",f,h,t);
                //Estadistica stats      = new Estadistica(porcentaje_costos, totalCostosEps, pacientesNoEnfermos,paciente_nulo, porcentajes_Edad, porcentajesRegimen, totalPacientesCancer, 
                //    porcentajesAfiliacion);

                return RedirectToAction("MostrarDatosVacios");
            }


            
            Estadistica estadisticas_ = new Estadistica(porcentaje_costos, totalCostosEps, pacientesNoEnfermos, pacientes_mayorCosto[0], porcentajes_Edad, porcentajesRegimen,
                totalPacientesCancer, porcentajesAfiliacion);

            return View(estadisticas_);
        }


        public ActionResult IngresarInformacion()
        {
            try
            {
                int id = Convert.ToInt32(Request.Form["id"]);
                Paciente paciente = miclinica.EncontrarPaciente(id);

                if (paciente == null)
                {
                    throw new UsuarioNoEncontradoException();
                }

                string nuevoId = Convert.ToString(id);
                TempData["nuevoId"] = nuevoId;
                TempData.Keep("nuevoId");
                return View(paciente);
            }
            catch (UsuarioNoEncontradoException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("ActualizarInformacion");
            }
        }

        public ActionResult MostrarCambioRegimen()
        {
            string nuevoId = (string)TempData["nuevoId"];
            TempData.Keep("nuevoId");
            int id = Convert.ToInt32(nuevoId);
            Paciente paciente = miclinica.EncontrarPaciente(id);
            string n_regimen = Request.Form["regimen"].ToString();
            miclinica.CambioRegimen(paciente, n_regimen);
            return View(paciente);
        }


        public ActionResult MostrarPacienteRegistrado()
        {
            int id = Convert.ToInt32((Request.Form["id"]));
            string nombre = Request.Form["nombre"].ToString();
            string apellido_1 = Request.Form["apellido_1"].ToString();
            string apellido_2 = Request.Form["apellido_2"].ToString();
            DateTime fh_nacimineto = DateTime.Parse(Request.Form["fhNacimiento"]);
            string tipo_regimen = Request.Form["regimen"].ToString();
            string tipo_eps = Request.Form["Eps"].ToString();
            string tipo_afiliacion = Request.Form["afiliacion"].ToString();
            string historia_clinica = Request.Form["historia_clinica"].ToString();
            int tiempo_eps = Convert.ToInt32(Request.Form["tiempo_en_eps"]);
            int cantidad_enfermedades = Convert.ToInt32(Request.Form["cantidad_Enfermedades"]);
            string enfermedad_relevante = Request.Form["Enfermedad_relevante"].ToString();
            int costo_tratamiento = Convert.ToInt32(Request.Form["costo_Tratamiento"]);

            Fecha fechas = new Fecha(fh_nacimineto, tiempo_eps);
            Historial historial = new Historial(tipo_eps, historia_clinica, cantidad_enfermedades, enfermedad_relevante);
            Trabajo trabajo = new Trabajo(tipo_regimen, tipo_afiliacion, costo_tratamiento);

            Paciente paciente = new Paciente(id, nombre, apellido_1, apellido_2, fechas, historial, trabajo);
            miclinica.guardarPaciente(paciente);
    

            return View(paciente);

        }

        public ActionResult MostrarDatosPaciente()
        {
            try
            {
                int id = Convert.ToInt32((Request.Form["id"]));
                Paciente paciente = miclinica.EncontrarPaciente(id);

                if (paciente == null)
                {
                    throw new UsuarioNoEncontradoException();
                }
                return View(paciente);
            }
            catch (UsuarioNoEncontradoException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("ver");
            }
        }


        public ActionResult MostrarCambioEps()
        {
            try
            {
                string nuevoId = (string)TempData["nuevoId"];
                TempData.Keep("nuevoId");
                int id = Convert.ToInt32(nuevoId);
                Paciente paciente = miclinica.EncontrarPaciente(id);
                if (paciente.Fechas.Tiempo_en_eps <= 3)
                {
                    throw new TiempoInvalidoException();
                }
                string n_eps = Request.Form["eps"].ToString();
                miclinica.CambioEps(paciente, n_eps);
                return View(paciente);
            }
            catch (TiempoInvalidoException ex)
            {
                Alert(ex.Message, NotificationType.warning);
                return RedirectToAction("ActualizarInformacion");
            }
        }


         public ActionResult MostrarCambioHistoria()
        {
            string nuevoId = (string)TempData["nuevoId"];
            TempData.Keep("nuevoId");
            int id = Convert.ToInt32(nuevoId);
            Paciente paciente = miclinica.EncontrarPaciente(id);
            string nueva_historia = Request.Form["historia"].ToString();

            miclinica.CambioHistoriaClinica(paciente, nueva_historia);
            return View(paciente);
        }

        
        public ActionResult MostrarCambioCosto()
        {
            string nuevoId = (string)TempData["nuevoId"];
            TempData.Keep("nuevoId");
            int id = Convert.ToInt32(nuevoId);
            Paciente paciente = miclinica.EncontrarPaciente(id);
            int nuevo_costo = Convert.ToInt32(Request.Form["cambiarcosto"]);

            miclinica.CambioCostoTratamiento(paciente, nuevo_costo);
            return View(paciente);

        }
        
        public ActionResult MostrarCambioEnfermedad()
        {
            string nuevoId = (string)TempData["nuevoId"];
            TempData.Keep("nuevoId");
            int id = Convert.ToInt32(nuevoId);
            Paciente paciente = miclinica.EncontrarPaciente(id);
            string enfermedad_relevante = Convert.ToString(Request.Form["e_relevante"]);

            miclinica.CambioEnfermedad_r(paciente, enfermedad_relevante);
            return View(paciente);
        }

        public ActionResult MostrarDatosVacios()
        {
            return View();
        }
    }
    
}