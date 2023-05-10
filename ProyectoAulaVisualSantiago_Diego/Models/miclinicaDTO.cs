using ProyectoAulaVisualSantiago_Diego.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProyectoAulaVisualSantiago_Diego.Models;
using System.Data.SqlTypes;

namespace ProyectoAulaVisualSantiago_Diego.Models
{
    public class miclinicaDTO
    {
        static Conexion connection = new Conexion();


        public void guardarPacienteBD(Paciente paciente)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();

                // Inicia una transacción
                SqlTransaction transaction = sqlconnection.BeginTransaction();

                try
                {
                    // Inserta en la tabla TbPaciente
                    using (SqlCommand command = new SqlCommand("INSERT INTO TbHistorial (Eps, historiaClinica, cantidadEnfermedades, enfermedadRelevante) VALUES (@Eps, @historiaClinica, @cantidadEnfermedades, @enfermedadRelevante); SELECT SCOPE_IDENTITY()", sqlconnection, transaction))
                    {
                        command.Parameters.AddWithValue("@Eps", paciente.Historial.Eps);
                        command.Parameters.AddWithValue("@historiaClinica", paciente.Historial.Historia_clínica);
                        command.Parameters.AddWithValue("@cantidadEnfermedades", paciente.Historial.Cantidad_Enfermedades);
                        command.Parameters.AddWithValue("@enfermedadRelevante", paciente.Historial.Enfermedad_relevante);

                        // Obtiene el ID recién insertado
                        int pacienteId = Convert.ToInt32(command.ExecuteScalar());

                        // Inserta en la tabla TbTrabajador
                        using (SqlCommand command2 = new SqlCommand("INSERT INTO TbTrabajo (tipoRegimen, fechaIngresoEPS, costoTratamiento, tipoAfiliacion) VALUES (@tipoRegimen, @fechaIngresoEPS, @costoTratamiento, @tipoAfiliacion); SELECT SCOPE_IDENTITY()", sqlconnection, transaction))
                        {
                            command2.Parameters.AddWithValue("@tipoRegimen", paciente.Trabajo.Tipo_de_regimen);
                            command2.Parameters.AddWithValue("@fechaIngresoEPS", paciente.Tiempo_en_eps);
                            command2.Parameters.AddWithValue("@costoTratamiento", paciente.Trabajo.Costo_Tratamiento);
                            command2.Parameters.AddWithValue("@tipoAfiliacion", paciente.Trabajo.Afiliacion);


                            // Obtiene el ID recién insertado
                            int trabajadorId = Convert.ToInt32(command2.ExecuteScalar());

                            // Inserta en la tabla TbPersona
                            using (SqlCommand command3 = new SqlCommand("INSERT INTO TbPacientes (identificacion, nombre, apellido1, apellido2, fechaNacimiento, id_Paciente, id_Trabajo) VALUES (@identificacion, @nombre, @apellido1, @apellido2, @fechaNacimiento, @id_Paciente, @id_Trabajo)", sqlconnection, transaction))
                            {
                                command3.Parameters.AddWithValue("@identificacion", paciente.Id);
                                command3.Parameters.AddWithValue("@nombre", paciente.Nombre);
                                command3.Parameters.AddWithValue("@apellido1", paciente.Apellido_1);
                                command3.Parameters.AddWithValue("@apellido2", paciente.Apellido_2);
                                command3.Parameters.AddWithValue("@fechaNacimiento", paciente.Fecha_de_nacimiento);
                                command3.Parameters.AddWithValue("@id_Paciente", pacienteId); // Usa el ID del paciente recién insertado
                                command3.Parameters.AddWithValue("@id_Trabajo", trabajadorId); // Usa el ID del trabajador recién insertado

                                // Ejecuta la sentencia
                                command3.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    // Si hay un error, hace un rollback de la transacción
                    transaction.Rollback();

                    throw ex;
                }

                sqlconnection.Close();
            }
        }

        public List<Paciente> ObtnerInformacionPacientesBD()
        {
            List<Paciente> lista = new List<Paciente>();

            using (SqlConnection sqlConnection = new SqlConnection(connection.conexion))
            {
                sqlConnection.Open();

                string query = @"SELECT p.identificacion, p.nombre, p.apellido1,p.apellido2, p.fechaNacimiento, 
                        pa.id_Paciente, pa.Eps, pa.historiaClinica, pa.cantidadEnfermedades, pa.enfermedadRelevante,
                        t.id_Trabajo, t.tipoRegimen,t.fechaIngresoEPS, t.costoTratamiento, t.tipoAfiliacion
                        FROM TbPacientes p
                        JOIN TbHistorial pa ON p.id_Paciente = pa.id_Paciente
                        JOIN TbTrabajo t ON p.id_Trabajo = t.id_Trabajo";


                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Historial historial = new Historial(
                                reader["Eps"].ToString(),
                                reader["historiaClinica"].ToString(), 
                                Convert.ToInt32(reader["cantidadEnfermedades"]),
                                reader["enfermedadRelevante"].ToString());

                            Trabajo trabajo = new Trabajo(
                                reader["tipoRegimen"].ToString(), 
                                reader["tipoAfiliacion"].ToString(), 
                                Convert.ToInt32(reader["costoTratamiento"]));

                            Paciente pacienteBD = new Paciente(
                                Convert.ToInt32(reader["identificacion"]),
                                reader["nombre"].ToString(),
                                reader["apellido1"].ToString(),
                                reader["apellido2"].ToString(),
                                Convert.ToDateTime(reader["fechaNacimiento"]),
                                Convert.ToDateTime(reader["fechaIngresoEPS"]),
                                historial, trabajo);
                                

                            lista.Add(pacienteBD);  
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lista;
        }


        public void cambiarEPSBD(int id, string n_eps)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Paciente FROM TbPacientes WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", id);

                int idPacienteBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbHistorial SET Eps = @nuevaEPS WHERE id_Paciente = @id_Paciente";

                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Paciente", idPacienteBD),
                        new SqlParameter("@nuevaEPS", n_eps)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());


                updateCommand.ExecuteNonQuery();
                
                sqlconnection.Close();
            }
        }
        
        public void cambiarRegimenBD(int id, string nuevo_regimen)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Paciente FROM TbPacientes WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", id);

                int idPacienteBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbTrabajo SET tipoRegimen = @tipoRegimen WHERE id_Trabajo = @id_Paciente";

                var parametros = new List<SqlParameter>{
                    new SqlParameter("@id_Paciente",idPacienteBD),
                    new SqlParameter("@tipoRegimen",nuevo_regimen)
                
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());


                updateCommand.ExecuteNonQuery();

                sqlconnection.Close();

            }
        }

        public void cambiarEnfermedadBD(int id, string enfermedad_relevante)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Paciente FROM TbPacientes WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", id);

                int idPacienteBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbHistorial SET enfermedadRelevante = @enfermedadRelevante WHERE id_Paciente = @id_Paciente";

                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Paciente", idPacienteBD),
                        new SqlParameter("@enfermedadRelevante", enfermedad_relevante)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());


                updateCommand.ExecuteNonQuery();

                sqlconnection.Close();
            }
        }

        public void cambiarHistoriaBD(int id, string n_historia)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Paciente FROM TbPacientes WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", id);

                int idPacienteBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbHistorial SET historiaClinica = @historiaClinica WHERE id_Paciente = @id_Paciente";

                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Paciente", idPacienteBD),
                        new SqlParameter("@historiaClinica", n_historia)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());


                updateCommand.ExecuteNonQuery();

                sqlconnection.Close();
            }
        }

        public void cambiarCostoBD(int id, int n_costo)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand("SELECT id_Paciente FROM TbPacientes WHERE identificacion = @identificacion", sqlconnection);
                command.Parameters.AddWithValue("@identificacion", id);

                int idPacienteBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE TbTrabajo SET costoTratamiento = @costoTratamiento WHERE id_Trabajo = @id_Paciente";

                var parametros = new List<SqlParameter>
                {
                        new SqlParameter("@id_Paciente", idPacienteBD),
                        new SqlParameter("@costoTratamiento", n_costo)
                };

                SqlCommand updateCommand = new SqlCommand(query, sqlconnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());


                updateCommand.ExecuteNonQuery();

                sqlconnection.Close();
            }
        }


    }
}