using System;
using System.Configuration;
using System.Data.SqlClient;
using unicampo.entities;

namespace unicampo.DAL
{
    public class ResenaRepository
    {
        private readonly string _connectionString;

        public ResenaRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["unicampoDb"].ConnectionString;
        }

        public void GuardarResena(ResenaDto resena)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                
                const string checkSql = "SELECT COUNT(1) FROM dbo.Inscripcion WHERE ActividadId = @ActividadId AND UsuarioId = @UsuarioId";
                using (var cmdCheck = new SqlCommand(checkSql, cn))
                {
                    cmdCheck.Parameters.AddWithValue("@ActividadId", resena.ActividadId);
                    cmdCheck.Parameters.AddWithValue("@UsuarioId", resena.UsuarioId);
                    int inscrito = (int)cmdCheck.ExecuteScalar();

                    if (inscrito == 0)
                        throw new Exception("Solo puedes calificar actividades en las que te hayas inscrito.");
                }

            
                const string insertSql = @"
                    INSERT INTO dbo.Resena (ActividadId, UsuarioId, Puntuacion, Comentario) 
                    VALUES (@ActividadId, @UsuarioId, @Puntuacion, @Comentario)";

                using (var cmd = new SqlCommand(insertSql, cn))
                {
                    cmd.Parameters.AddWithValue("@ActividadId", resena.ActividadId);
                    cmd.Parameters.AddWithValue("@UsuarioId", resena.UsuarioId);
                    cmd.Parameters.AddWithValue("@Puntuacion", resena.Puntuacion);
                    cmd.Parameters.AddWithValue("@Comentario", resena.Comentario ?? (object)DBNull.Value);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Error de UNIQUE (Duplicado)
                            throw new Exception("Ya dejaste una reseña para esta actividad anteriormente.");
                        throw;
                    }
                }
            }
        }
        public System.Collections.Generic.List<ResenaDto> ObtenerResenasPorActividad(int actividadId)
        {
            var lista = new System.Collections.Generic.List<ResenaDto>();
            const string sql = @"
        SELECT r.Puntuacion, r.Comentario, u.Nombre 
        FROM dbo.Resena r
        INNER JOIN dbo.Usuario u ON r.UsuarioId = u.UsuarioId
        WHERE r.ActividadId = @ActividadId
        ORDER BY r.Fecha DESC;";

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ActividadId", actividadId);
                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ResenaDto
                        {
                            Puntuacion = reader.GetInt32(0),
                            Comentario = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            NombreEstudiante = reader.GetString(2)
                        });
                    }
                }
            }
            return lista;
        }
    }
}