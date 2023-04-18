

using ProyectoAulaVisualSantiago_Diego.Models;

namespace ProyectoAulaVisualSantiago_DiegoTest
{
    [TestClass]
    public class UnitTest1
    {
        public void crearPacientePrueba()
        {
            int id = 123456789;
            string nombre = "Edisson";
            string apellido_1 = "Estelio";
            string apeliido_2 = "Gutierrez";
            DateTime fecha_nacimiento = DateTime.Now;
            int tiempo_en_eps = 4;
            Fecha fechas = new Fecha(fecha_nacimiento, tiempo_en_eps);
            Trabajo trabajo = new Trabajo("Contributivo", "Cotizante", 7500000);
            Historial historial = new Historial("Sura", "Ataque al corazon", 5, "cancer");



            Paciente paciente = new Paciente(id,nombre,apellido_1,apeliido_2, fechas,historial, trabajo);
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}