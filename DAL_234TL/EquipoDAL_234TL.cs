using BE_234TL;
using Microsoft.Data.SqlClient;

namespace DAL_234TL
{
    public class EquipoDAL_234TL : AbstractDAL_234TL<Equipo_234TL, string>
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog = Wilhjem_234TL; Integrated Security = True; Trust Server Certificate=True";
        public override void Eliminar(Equipo_234TL entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                using (SqlConnection BaseDatos = new SqlConnection(connectionString))
                {
                    BaseDatos.Open();
                    string query = "DELETE FROM Equipo_234TL WHERE NumeroSerie = @NumeroSerie";

                    using (SqlCommand command = new SqlCommand(query, BaseDatos))
                    {
                        command.Parameters.AddWithValue("@NumeroSerie", entity.NumeroSerie);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar el equipo de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al eliminar el equipo.", ex);
            }
        }

        public override void EliminarKey(string key)
        {
            throw new NotImplementedException();
        }

        public override IList<Equipo_234TL> GetAll()
        {
            List<Equipo_234TL> listaEquipos = new List<Equipo_234TL>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT NumeroSerie, Marca, Modelo, Estado, FallaReportada, HoraIngreso, Desarmado, DañoVisible FROM Equipo_234TL";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Equipo_234TL equipo = new Equipo_234TL
                        (
                        numeroSerie: reader["NumeroSerie"].ToString(),
                        marca: reader["Marca"]?.ToString() ?? "",
                        modelo: reader["Modelo"]?.ToString() ?? "",
                        estado: reader["Estado"]?.ToString() ?? "",
                        fallaReportada: reader["FallaReportada"]?.ToString() ?? "",
                        horaIngreso: reader["HoraIngreso"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["HoraIngreso"]),
                        desarmado: reader["Desarmado"] != DBNull.Value && Convert.ToBoolean(reader["Desarmado"]),
                        dañoVisible: reader["DañoVisible"] != DBNull.Value && Convert.ToBoolean(reader["DañoVisible"])
                        );
                        listaEquipos.Add(equipo);
                    }
                    reader.Close();
                }
                return listaEquipos;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener los equipos de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener los equipos.", ex);
            }
        }

        public override Equipo_234TL GetbyPrimary(Equipo_234TL entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                using (SqlConnection BaseDatos = new SqlConnection(connectionString))
                {
                    string query = @"SELECT NumeroSerie, Marca, Modelo, Estado, FallaReportada, HoraIngreso, Desarmado, DañoVisible 
                                    FROM Equipo_234TL 
                                    WHERE NumeroSerie = @NumeroSerie";

                    using (SqlCommand command = new SqlCommand(query, BaseDatos))
                    {
                        command.Parameters.AddWithValue("@NumeroSerie", entity.NumeroSerie);

                        BaseDatos.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Equipo_234TL(
                                    numeroSerie: reader["NumeroSerie"].ToString(),
                                    marca: reader["Marca"].ToString(),
                                    modelo: reader["Modelo"].ToString(),
                                    estado: reader["Estado"].ToString(),
                                    fallaReportada: reader["FallaReportada"].ToString(),
                                    horaIngreso: Convert.ToDateTime(reader["HoraIngreso"]),
                                    desarmado: Convert.ToBoolean(reader["Desarmado"]),
                                    dañoVisible: Convert.ToBoolean(reader["DañoVisible"])
                                );
                            }
                        }
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener el equipo de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener el equipo.", ex);
            }
        }

        public override Equipo_234TL GetbyPrimaryKey(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT NumeroSerie, Marca, Modelo, Estado, FallaReportada, HoraIngreso, Desarmado, DañoVisible 
                             FROM Equipo_234TL 
                             WHERE NumeroSerie = @NumeroSerie";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NumeroSerie", id);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Equipo_234TL(
                                    numeroSerie: reader["NumeroSerie"].ToString(),
                                    marca: reader["Marca"].ToString(),
                                    modelo: reader["Modelo"].ToString(),
                                    estado: reader["Estado"].ToString(),
                                    fallaReportada: reader["FallaReportada"].ToString(),
                                    horaIngreso: reader["HoraIngreso"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["HoraIngreso"]),
                                    desarmado: reader["Desarmado"] != DBNull.Value && Convert.ToBoolean(reader["Desarmado"]),
                                    dañoVisible: reader["DañoVisible"] != DBNull.Value && Convert.ToBoolean(reader["DañoVisible"])
                                );
                            }
                        }
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener el equipo por clave primaria desde la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener el equipo por clave primaria.", ex);
            }
        }

        public override void Guardar(Equipo_234TL entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Equipo_234TL 
                        (NumeroSerie, Marca, Modelo, Estado, FallaReportada, HoraIngreso, Desarmado, DañoVisible) 
                        VALUES 
                        (@NumeroSerie, @Marca, @Modelo, @Estado, @FallaReportada, @HoraIngreso, @Desarmado, @DañoVisible)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NumeroSerie", entity.NumeroSerie);
                    command.Parameters.AddWithValue("@Marca", entity.Marca);
                    command.Parameters.AddWithValue("@Modelo", entity.Modelo);
                    command.Parameters.AddWithValue("@Estado", entity.Estado);
                    command.Parameters.AddWithValue("@FallaReportada", entity.FallaReportada);
                    command.Parameters.AddWithValue("@HoraIngreso", entity.HoraIngreso);
                    command.Parameters.AddWithValue("@Desarmado", entity.Desarmado);
                    command.Parameters.AddWithValue("@DañoVisible", entity.DañoVisible);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al guardar el equipo en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al guardar el equipo.", ex);
            }
        }

        public override void Update(Equipo_234TL entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Equipo_234TL 
                        SET Marca = @Marca, 
                            Modelo = @Modelo, 
                            Estado = @Estado, 
                            FallaReportada = @FallaReportada, 
                            HoraIngreso = @HoraIngreso, 
                            Desarmado = @Desarmado, 
                            DañoVisible = @DañoVisible 
                        WHERE NumeroSerie = @NumeroSerie";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NumeroSerie", entity.NumeroSerie);
                    command.Parameters.AddWithValue("@Marca", entity.Marca);
                    command.Parameters.AddWithValue("@Modelo", entity.Modelo);
                    command.Parameters.AddWithValue("@Estado", entity.Estado);
                    command.Parameters.AddWithValue("@FallaReportada", entity.FallaReportada);
                    command.Parameters.AddWithValue("@HoraIngreso", entity.HoraIngreso);
                    command.Parameters.AddWithValue("@Desarmado", entity.Desarmado);
                    command.Parameters.AddWithValue("@DañoVisible", entity.DañoVisible);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar el equipo en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al actualizar el equipo.", ex);
            }
        }
    }

}
