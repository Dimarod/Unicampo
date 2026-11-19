using System;
using System.Configuration;
using System.Data.SqlClient;

namespace unicampo.DAL
{
    public class InscripcionRepository
    {
        private readonly string _connectionString;

        public InscripcionRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["unicampoDb"].ConnectionString;
        }

        public void RegistrarInscripcion(int actividadId, int usuarioId)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                
                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        
                        const string checkSql = "SELECT Cupos FROM dbo.ActividadAcademica WHERE ActividadId = @ActividadId AND Estado = 'Publicada'";
                        int cuposActuales = 0;
                        using (var cmdCheck = new SqlCommand(checkSql, cn, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@ActividadId", actividadId);
                            var result = cmdCheck.ExecuteScalar();
                            if (result == null) throw new Exception("La actividad no existe o fue cancelada.");
                            cuposActuales = Convert.ToInt32(result);
                        }

                        if (cuposActuales <= 0) throw new Exception("Ya no hay cupos disponibles para esta actividad.");

                        const string insertSql = "INSERT INTO dbo.Inscripcion (ActividadId, UsuarioId) VALUES (@ActividadId, @UsuarioId)";
                        using (var cmdInsert = new SqlCommand(insertSql, cn, transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@ActividadId", actividadId);
                            cmdInsert.Parameters.AddWithValue("@UsuarioId", usuarioId);
                            cmdInsert.ExecuteNonQuery();
                        }

                        const string updateSql = "UPDATE dbo.ActividadAcademica SET Cupos = Cupos - 1 WHERE ActividadId = @ActividadId";
                        using (var cmdUpdate = new SqlCommand(updateSql, cn, transaction))
                        {
                            cmdUpdate.Parameters.AddWithValue("@ActividadId", actividadId);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback(); 
                        if (ex.Number == 2627) throw new Exception("Ya te encuentras inscrito en esta actividad.");
                        throw new Exception("Error en la base de datos al inscribir.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}