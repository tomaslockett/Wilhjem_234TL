using Microsoft.Data.SqlClient;
using Servicios_234TL;

namespace DAL_234TL
{
    public class UsuarioDAL_234TL : AbstractDAL_234TL<Usuario_234TL, string>
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog = Wilhjem_234TL; Integrated Security = True; Trust Server Certificate=True";

        public override Usuario_234TL GetbyPrimaryKey(string dni)
        {
            try
            {
                using (SqlConnection BaseDatos = new(connectionString))
                {
                    BaseDatos.Open();
                    string query = "SELECT DNI, Nombre, Apellido, Email, Rol, Bloqueo, Activo, Login, Password, IntentosFallidos,UltimoIntentoFallido FROM Usuarios_234TL WHERE DNI = @DNI";

                    using (SqlCommand comando = new(query, BaseDatos))
                    {
                        comando.Parameters.AddWithValue("@DNI", dni);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Usuario_234TL
                                {
                                    DNI = reader.GetString(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Rol = reader.GetString(4),
                                    Bloqueado = reader.GetBoolean(5),
                                    Activo = reader.GetBoolean(6),
                                    Login = reader.GetString(7),
                                    Password = reader.GetString(8),
                                    IntentosFallidos = reader.GetInt32(9),
                                    UltimoIntentoFallido = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10)
                                };
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
                List<Usuario_234TL> ListaUsuarios = new();

                using (SqlConnection BaseDatos = new(connectionString))
                {
                    BaseDatos.Open();
                    string query = "SELECT DNI, Nombre, Apellido, Email, Rol, Bloqueo, Activo, Login, Password, IntentosFallidos,UltimoIntentoFallido FROM Usuarios_234TL";

                    using (SqlCommand Comando = new(query, BaseDatos))
                    {
                        using (SqlDataReader reader = Comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuario_234TL usuario = new Usuario_234TL
                                {
                                    DNI = reader.GetString(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Rol = reader.GetString(4),
                                    Bloqueado = reader.GetBoolean(5),
                                    Activo = reader.GetBoolean(6),
                                    Login = reader.GetString(7),
                                    Password = reader.GetString(8),
                                    IntentosFallidos = reader.GetInt32(9),
                                    UltimoIntentoFallido = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10)
                                };
                                ListaUsuarios.Add(usuario);
                            }
                        }
                    }
                }

                return ListaUsuarios;
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

        public override void Guardar(Usuario_234TL Entidad)
        {
            try
            {
                if (Entidad == null)
                    throw new ArgumentNullException(nameof(Entidad));

                using (SqlConnection BaseDatos = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Usuarios_234TL
                        (DNI, Nombre, Apellido, Email, Rol, Bloqueo, Activo, Login, Password, IntentosFallidos,UltimoIntentoFallido)
                        VALUES
                        (@DNI, @Nombre, @Apellido, @Email, @Rol, @Bloqueo, @Activo, @Login, @Password, @IntentosFallidos,@UltimoIntentoFallido)";

                    using (SqlCommand command = new SqlCommand(query, BaseDatos))
                    {
                        command.Parameters.AddWithValue("@DNI", Entidad.DNI);
                        command.Parameters.AddWithValue("@Nombre", Entidad.Nombre);
                        command.Parameters.AddWithValue("@Apellido", Entidad.Apellido);
                        command.Parameters.AddWithValue("@Email", Entidad.Email);
                        command.Parameters.AddWithValue("@Rol", Entidad.Rol);
                        command.Parameters.AddWithValue("@Bloqueo", Entidad.Bloqueado);
                        command.Parameters.AddWithValue("@Activo", Entidad.Activo);
                        command.Parameters.AddWithValue("@Login", Entidad.Login);
                        command.Parameters.AddWithValue("@Password", Entidad.Password);
                        command.Parameters.AddWithValue("@IntentosFallidos", Entidad.IntentosFallidos);
                        command.Parameters.AddWithValue("@UltimoIntentoFallido", Entidad.UltimoIntentoFallido ?? (object)DBNull.Value);

                        BaseDatos.Open();
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

        public override void Update(Usuario_234TL Entidad)
        {
            try
            {
                if (Entidad == null)
                    throw new ArgumentNullException(nameof(Entidad));
                using (SqlConnection BaseDatos = new(connectionString))
                {
                    BaseDatos.Open();
                    string query = @"UPDATE Usuarios_234TL
                         SET Nombre = @Nombre,
                             Apellido = @Apellido,
                             Email = @Email,
                             Rol = @Rol,
                             Bloqueo = @Bloqueo,
                             Activo = @Activo,
                             Login = @Login,
                             Password = @Password,
                             IntentosFallidos = @IntentosFallidos,
                             UltimoIntentoFallido = @UltimoIntentoFallido
                         WHERE DNI = @DNI";

                    using (SqlCommand command = new(query, BaseDatos))
                    {
                        command.Parameters.AddWithValue("@DNI", Entidad.DNI);
                        command.Parameters.AddWithValue("@Nombre", Entidad.Nombre);
                        command.Parameters.AddWithValue("@Apellido", Entidad.Apellido);
                        command.Parameters.AddWithValue("@Email", Entidad.Email);
                        command.Parameters.AddWithValue("@Rol", Entidad.Rol);
                        command.Parameters.AddWithValue("@Bloqueo", Entidad.Bloqueado);
                        command.Parameters.AddWithValue("@Activo", Entidad.Activo);
                        command.Parameters.AddWithValue("@Password", Entidad.Password);
                        command.Parameters.AddWithValue("@IntentosFallidos", Entidad.IntentosFallidos);
                        command.Parameters.AddWithValue("@Login", Entidad.Login);
                        command.Parameters.AddWithValue("@UltimoIntentoFallido", Entidad.UltimoIntentoFallido ?? (object)DBNull.Value);
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

        //CAMBIAR
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

        //CAMBIAR
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
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                using (SqlConnection BaseDatos = new SqlConnection(connectionString))
                {
                    string query = @"SELECT DNI, Nombre, Apellido, Email, Rol, Bloqueo, Activo,
                        Login, Password, IntentosFallidos
                        FROM Usuarios_234TL
                        WHERE Login = @Login AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, BaseDatos))
                    {
                        command.Parameters.AddWithValue("@Login", entity.Login);
                        command.Parameters.AddWithValue("@Password", entity.Password);

                        BaseDatos.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Usuario_234TL
                                {
                                    DNI = reader.GetString(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Rol = reader.GetString(4),
                                    Bloqueado = reader.GetBoolean(5),
                                    Activo = reader.GetBoolean(6),
                                    Login = reader.GetString(7),
                                    Password = reader.GetString(8),
                                    IntentosFallidos = reader.GetInt32(9),
                                    UltimoIntentoFallido = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10)
                                };
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
    }
}