

using ProyectoAulaVisualSantiago_Diego.Models;

namespace ProyectoAulaVisualSantiago_DiegoTest
{
    [TestClass]
    public class UnitTest1
    {
        public Paciente crearPacientePrueba()
        {
            int id = 123456789;
            string nombre = "Edisson";
            string apellido_1 = "Estelio";
            string apeliido_2 = "Gutierrez";
            DateTime fecha_nacimiento = DateTime.Now;
            int tiempo_en_eps = 4;
            Fecha fechas = new Fecha(fecha_nacimiento, tiempo_en_eps);
            Trabajo trabajo = new Trabajo("Contributivo", "Cotizante", 7500000);
            Historial historial = new Historial("Sura", "Ataque al corazon", 0, "Covid");



            Paciente paciente = new Paciente(id, nombre, apellido_1, apeliido_2, fechas, historial, trabajo);
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
            Assert.AreEqual(paciente.Historial.Eps, expected);
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
            Assert.AreEqual(paciente.Historial.Historia_clínica, expected);
        }

        [TestMethod]
        public void CambioCostoTratamiento()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            int expected = 100;

            //Add
            Paciente paciente = crearPacientePrueba();
            target.CambioCostoTratamiento(paciente, 100);

            //Assert
            Assert.AreEqual(paciente.Trabajo.Costo_Tratamiento, expected);
        }

        [TestMethod]
        public void CambiofEnfermedad_r()
        {
            //Arrange
            ProyectoAulaVisualSantiago_Diego.Models.Clinica target = new ProyectoAulaVisualSantiago_Diego.Models.Clinica();
            string expected = "Cancer";

            //Add
            Paciente paciente = crearPacientePrueba();
            target.CambioEnfermedad_r(paciente, "Cancer");

            //Assert
            Assert.AreEqual(paciente.Historial.Enfermedad_relevante, expected);
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
            List<double> porcentajes = Clinica.Porcentaje_costos(target.Pacientes);
            
            //Assert
            Assert.AreEqual(expected1, porcentajes[0]);
            Assert.AreEqual(expected2, porcentajes[1]);
            Assert.AreEqual(expected3, porcentajes[2]);
            Assert.AreEqual(expected4, porcentajes[3]);
            Assert.AreEqual(expected5, porcentajes[4]);
        }
    }
}   