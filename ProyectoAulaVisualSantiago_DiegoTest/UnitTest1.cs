

using ProyectoAulaVisualSantiago_Diego.Models;
using System.Net.Sockets;

namespace ProyectoAulaVisualSantiago_DiegoTest
{
    [TestClass]
    public class UnitTest1
    {
        ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();


        public Paciente crearPacientePrueba()
        {
            int id = 123456789;
            string nombre = "Edisson";
            string apellido_1 = "Estelio";
            string apeliido_2 = "Gutierrez";
            DateTime fecha_nacimiento = DateTime.Now;
            DateTime tiempo_en_eps = DateTime.Parse("04/02/2004");
            Trabajo trabajo = new Trabajo("Contributivo", "Cotizante", 1000);
            Historial historial = new Historial("Sura", "Ataque al corazon", 0, "Covid");
            Paciente paciente = new Paciente(id, nombre, apellido_1, apeliido_2, fecha_nacimiento,tiempo_en_eps, historial, trabajo);
            return paciente;

        }

        [TestMethod]
        public void guardarPacienteTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            //bool expected = target.Pacientes.Count() > 0;

            //Add
            Paciente paciente = crearPacientePrueba();
            target.guardarPaciente(paciente);


            //Assert
            Assert.IsTrue(target.Pacientes.Count() > 0);
            target.Pacientes.Clear();
        }


        [TestMethod]
        public void EncontrarPacienteTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            int expected = 123456789;

            //Add
            Paciente paciente = crearPacientePrueba();
            target.guardarPaciente(paciente);
            Paciente paciente_hallado = target.EncontrarPaciente(paciente.Id);
            int actual = paciente_hallado.Id;

            //Assert
            Assert.AreEqual(expected, actual);
            target.Pacientes.Clear();
        }

        [TestMethod]
        public void Porcentaje_sin_enfermedadesTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            double expected = 100;

            //Add
            Paciente paciente = crearPacientePrueba();
            target.guardarPaciente(paciente);
            double actual = Clinica.Porcentaje_sin_enfermedades(target.Pacientes);

            //Assert
            Assert.AreEqual(expected, actual);
            target.Pacientes.Clear();
        }

        [TestMethod]
        public void CambioEpsTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            string expected = "Sura";

            //Add
            Paciente paciente = crearPacientePrueba();
            target.guardarPaciente(paciente);
            target.CambioEps(paciente, "Sura");

            //Assert
            Assert.AreEqual(expected, paciente.Historial.Eps);
            target.Pacientes.Clear();
        }

        [TestMethod]
        public void Sumar_costosTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            double expected= 1000;

            //Add
            Paciente paciente = crearPacientePrueba();
            target.guardarPaciente(paciente);
            double actual = Clinica.Sumar_costos(target.Pacientes);
            
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CambioHistoriaTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            string expected = "Me duele el pecho";

            //Add
            Paciente paciente = crearPacientePrueba();
            target.guardarPaciente(paciente);
            target.CambioHistoriaClinica(paciente, "Me duele el pecho");

            //Assert
            Assert.AreEqual(expected, paciente.Historial.Historia_clínica);
            target.Pacientes.Clear();
        }

        [TestMethod]
        public void CambioCostoTratamientoTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            int expected = 100;

            //Add
            Paciente paciente = crearPacientePrueba();
            target.CambioCostoTratamiento(paciente, 100);

            //Assert
            Assert.AreEqual(expected, paciente.Trabajo.Costo_Tratamiento);
        }

        [TestMethod]
        public void CambiofEnfermedad_rTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            string expected = "Cancer";

            //Add
            Paciente paciente = crearPacientePrueba();
            target.CambioEnfermedad_r(paciente, "Cancer");

            //Assert
            Assert.AreEqual(expected, paciente.Historial.Enfermedad_relevante);
        }

        [TestMethod]
        public void Porcentaje_costosTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            double expected1 = 100;
            double expected2 = 0;
            double expected3 = 0;
            double expected4 = 0;
            double expected5 = 0;

            //Add
            Paciente paciente = crearPacientePrueba();
            target.guardarPaciente(paciente);
            List<double> porcentajes = Clinica.Porcentaje_costos(target.Pacientes);
            
            //Assert
            Assert.AreEqual(expected1, porcentajes[0]);
            Assert.AreEqual(expected2, porcentajes[1]);
            Assert.AreEqual(expected3, porcentajes[2]);
            Assert.AreEqual(expected4, porcentajes[3]);
            Assert.AreEqual(expected5, porcentajes[4]);

            target.Pacientes.Clear();
        }

        [TestMethod]
        public void Calcular_costos_por_epsTest()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            double expected1 = 1000;
            double expected2 = 0;
            double expected3 = 0;
            double expected4 = 0;
            double expected5 = 0;

            //Add
            Paciente paciente = crearPacientePrueba();
            target.guardarPaciente(paciente);
            List<double> costos = Clinica.Calcular_costos_por_eps(target.Pacientes);

            //Assert
            Assert.AreEqual(expected1, costos[0]);
            Assert.AreEqual(expected2, costos[1]);
            Assert.AreEqual(expected3, costos[2]);
            Assert.AreEqual(expected4, costos[3]);
            Assert.AreEqual(expected5, costos[4]);

            target.Pacientes.Clear();

        }

    }
}   