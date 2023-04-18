

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
            Historial historial = new Historial("Sura", "Ataque al corazon", 0, "cancer");



            Paciente paciente = new Paciente(id,nombre,apellido_1,apeliido_2, fechas,historial, trabajo);
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
    }
}