using Microsoft.Data.SqlClient;
using Servicios_234TL;
using Servicios_234TL.Composite_234TL;

namespace DAL_234TL
{
    public class UsuarioDAL_234TL : AbstractDAL_234TL<Usuario_234TL, string>
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog = Wilhjem_234TL; Integrated Security = True; Trust Server Certificate=True";
        private readonly PerfilDAL_234TL _perfilDAL = new PerfilDAL_234TL();

        private Usuario_234TL UsuarioFromReader(SqlDataReader reader, Dictionary<int, Perfil_234TL> perfilesCache = null)
        {
            var usuario = new Usuario_234TL
            {
                DNI = reader["DNI"].ToString(),
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"].ToString(),
                Email = reader["Email"].ToString(),
                Bloqueado= (bool)reader["Bloqueo"],
                Activo = (bool)reader["Activo"],
                Login = reader["Login"].ToString(),
                Password = reader["Password"].ToString(), 
                IntentosFallidos = (int)reader["IntentosFallidos"],
                UltimoIntentoFallido = reader["UltimoIntentoFallido"] is DBNull ? null : (DateTime?)reader["UltimoIntentoFallido"],
                Idioma = reader["Idioma"] is DBNull ? "es" : reader["Idioma"].ToString()
            };

            if (reader["IdPerfil"] != DBNull.Value)
            {
                int idPerfil = (int)reader["IdPerfil"];
                if (perfilesCache != null && perfilesCache.TryGetValue(idPerfil, out var perfilCacheado))
                {
                    usuario.Perfil = perfilCacheado;
                }
                else
                {
                    usuario.Perfil = _perfilDAL.GetbyPrimaryKey(idPerfil);
                }
            }

            return usuario;
        }


        public override Usuario_234TL GetbyPrimaryKey(string dni)
        {
            try
            {
                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    string query = "SELECT DNI, Nombre, Apellido, Email, IdPerfil, Bloqueo, Activo, Login, Password, IntentosFallidos, UltimoIntentoFallido FROM Usuarios_234TL WHERE DNI = @DNI";
                    using (var comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@DNI", dni);
                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return UsuarioFromReader(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar el usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override IList<Usuario_234TL> GetAll()
        {
            try
            {
                var listaUsuarios = new List<Usuario_234TL>();

                var perfilesCache = _perfilDAL.GetAll().ToDictionary(p => p.IdPerfil);

                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    string query = "SELECT DNI, Nombre, Apellido, Email, IdPerfil, Bloqueo, Activo, Login, Password, IntentosFallidos, UltimoIntentoFallido, Idioma FROM Usuarios_234TL";
                    using (var comando = new SqlCommand(query, conexion))
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaUsuarios.Add(UsuarioFromReader(reader, perfilesCache));
                        }
                    }
                }
                return listaUsuarios;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar el usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override void Guardar(Usuario_234TL entidad)
        {
            try
            {
                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    string query = @"INSERT INTO Usuarios_234TL 
                                 (DNI, Nombre, Apellido, Email, IdPerfil, Bloqueo, Activo, Login, Password, IntentosFallidos, UltimoIntentoFallido, Idioma) 
                                 VALUES 
                                 (@DNI, @Nombre, @Apellido, @Email, @IdPerfil, @Bloqueo, @Activo, @Login, @Password, @IntentosFallidos, @UltimoIntentoFallido, @Idioma)";

                    using (var command = new SqlCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@DNI", entidad.DNI);
                        command.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                        command.Parameters.AddWithValue("@Apellido", entidad.Apellido);
                        command.Parameters.AddWithValue("@Email", entidad.Email);
                        command.Parameters.AddWithValue("@IdPerfil", entidad.Perfil?.IdPerfil ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Bloqueo", entidad.Bloqueado);
                        command.Parameters.AddWithValue("@Activo", entidad.Activo);
                        command.Parameters.AddWithValue("@Login", entidad.Login);
                        command.Parameters.AddWithValue("@Password", entidad.Password);
                        command.Parameters.AddWithValue("@IntentosFallidos", entidad.IntentosFallidos);
                        command.Parameters.AddWithValue("@UltimoIntentoFallido", entidad.UltimoIntentoFallido ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Idioma", entidad.Idioma);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar el usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override void Update(Usuario_234TL entidad)
        {
            try
            {
                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    string query = @"UPDATE Usuarios_234TL SET 
                                 Nombre = @Nombre, Apellido = @Apellido, Email = @Email, IdPerfil = @IdPerfil, 
                                 Bloqueo = @Bloqueo, Activo = @Activo, Login = @Login, Password = @Password, 
                                 IntentosFallidos = @IntentosFallidos, UltimoIntentoFallido = @UltimoIntentoFallido, Idioma = @Idioma
                                 WHERE DNI = @DNI";

                    using (var command = new SqlCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@DNI", entidad.DNI);
                        command.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                        command.Parameters.AddWithValue("@Apellido", entidad.Apellido);
                        command.Parameters.AddWithValue("@Email", entidad.Email);
                        command.Parameters.AddWithValue("@IdPerfil", entidad.Perfil?.IdPerfil ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Bloqueo", entidad.Bloqueado);
                        command.Parameters.AddWithValue("@Activo", entidad.Activo);
                        command.Parameters.AddWithValue("@Login", entidad.Login);
                        command.Parameters.AddWithValue("@Password", entidad.Password);
                        command.Parameters.AddWithValue("@IntentosFallidos", entidad.IntentosFallidos);
                        command.Parameters.AddWithValue("@UltimoIntentoFallido", entidad.UltimoIntentoFallido ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Idioma", entidad.Idioma);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar el usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override void Eliminar(Usuario_234TL Entidad)
        {
            try
            {
                using (SqlConnection BaseDatos = new SqlConnection(connectionString))
                {
                    BaseDatos.Open();
                    string query = "DELETE FROM Usuarios_234TL WHERE DNI = @DNI";

                    using (SqlCommand command = new SqlCommand(query, BaseDatos))
                    {
                        command.Parameters.AddWithValue("@DNI", Entidad.DNI);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar el usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override void EliminarKey(string key)
        {
            try
            {
                using (SqlConnection BaseDatos = new SqlConnection(connectionString))
                {
                    BaseDatos.Open();
                    string query = "DELETE FROM Usuarios_234TL WHERE DNI = @DNI";

                    using (SqlCommand command = new SqlCommand(query, BaseDatos))
                    {
                        command.Parameters.AddWithValue("@DNI", key);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar el usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override Usuario_234TL GetbyPrimary(Usuario_234TL entity)
        {
            try
            {
                return GetbyPrimaryKey(entity.DNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar el usuario en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }
    }
}