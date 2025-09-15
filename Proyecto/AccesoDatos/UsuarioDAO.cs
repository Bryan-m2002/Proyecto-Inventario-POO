using System;
using System.Data.SqlClient;
using System.Configuration;
using Proyecto.Modelos;

namespace Proyecto.AccesoDatos
{
    public class UsuarioDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;

        public bool InsertarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO usuarios (IdUsuario, Nombre, Rol, Contrasena) VALUES (@IdUsuario, @Nombre, @Rol, @Contrasena)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Rol", usuario.Rol);
                command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);

                try
                {
                    connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar usuario: " + ex.Message);
                    return false;
                }
            }
        }

        public Usuario ObtenerUsuario(string nombre, string contrasena)
        {
            Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdUsuario, Nombre, Rol, Contrasena FROM usuarios WHERE Nombre = @Nombre AND Contrasena = @Contrasena";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Contrasena", contrasena);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Rol = reader.GetString(reader.GetOrdinal("Rol")),
                            Contrasena = reader.GetString(reader.GetOrdinal("Contrasena"))
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener usuario: " + ex.Message);
                }
            }
            return usuario;
        }
    }
}