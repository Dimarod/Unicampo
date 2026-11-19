using System;
using System.Configuration;
using System.Data.SqlClient;
using unicampo.entities;

namespace unicampo.DAL
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["unicampoDb"].ConnectionString;
        }

        public UsuarioDto ObtenerPorCorreo(string correo)
        {
            const string sql = @"
                SELECT UsuarioId, Nombre, Correo, Rol 
                FROM dbo.Usuario 
                WHERE Correo = @Correo;";

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Correo", correo);
                cn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UsuarioDto
                        {
                            UsuarioId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Correo = reader.GetString(2),
                            Rol = reader.GetString(3)
                        };
                    }
                }
            }
            return null; // Si no encuentra el correo, retorna nulo
        }
    }
}