using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using unicampo.entities;

namespace unicampo.DAL
{
    public class ActividadRepository
    {
        private readonly string _connectionString;

        public ActividadRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["unicampoDb"].ConnectionString;
        }

        public List<ActividadDto> ObtenerPublicadas()
        {
            var resultado = new List<ActividadDto>();
            const string sql = @"
                SELECT ActividadId, Titulo, Descripcion, FechaInicio, Lugar, Cupos, Estado
                FROM dbo.ActividadAcademica
                WHERE Estado = @Estado
                ORDER BY FechaInicio;";
         
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Estado", "Publicada");
                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultado.Add(new ActividadDto
                        {
                            ActividadId = reader.GetInt32(0),
                            Titulo = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            FechaInicio = reader.GetDateTime(3),
                            Lugar = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Cupos = reader.GetInt32(5),
                            Estado = reader.GetString(6)
                        });
                    }
                }
            }
            return resultado;
        }
        public void GuardarActividad(ActividadDto actividad)
        {
            const string sql = @"
        INSERT INTO dbo.ActividadAcademica (Titulo, Descripcion, FechaInicio, Lugar, Cupos, Estado)
        VALUES (@Titulo, @Descripcion, @FechaInicio, @Lugar, @Cupos, @Estado);";

            using (var cn = new System.Data.SqlClient.SqlConnection(_connectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Titulo", actividad.Titulo);
                cmd.Parameters.AddWithValue("@Descripcion", actividad.Descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaInicio", actividad.FechaInicio);
                cmd.Parameters.AddWithValue("@Lugar", actividad.Lugar ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Cupos", actividad.Cupos);
                cmd.Parameters.AddWithValue("@Estado", "Publicada");

                cn.Open();
                cmd.ExecuteNonQuery();
            }

        }
        public void ActualizarActividad(ActividadDto actividad)
        {
            const string sql = @"
        UPDATE dbo.ActividadAcademica 
        SET Titulo = @Titulo, 
            Descripcion = @Descripcion, 
            FechaInicio = @FechaInicio, 
            Lugar = @Lugar, 
            Cupos = @Cupos 
        WHERE ActividadId = @ActividadId;";

            using (var cn = new System.Data.SqlClient.SqlConnection(_connectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ActividadId", actividad.ActividadId);
                cmd.Parameters.AddWithValue("@Titulo", actividad.Titulo);
                cmd.Parameters.AddWithValue("@Descripcion", actividad.Descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaInicio", actividad.FechaInicio);
                cmd.Parameters.AddWithValue("@Lugar", actividad.Lugar ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Cupos", actividad.Cupos);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CancelarActividad(int actividadId)
        {
            const string sql = @"
        UPDATE dbo.ActividadAcademica 
        SET Estado = 'Cancelada' 
        WHERE ActividadId = @ActividadId;";

            using (var cn = new System.Data.SqlClient.SqlConnection(_connectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ActividadId", actividadId);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}