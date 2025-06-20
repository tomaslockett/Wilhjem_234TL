using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Servicios_234TL.Composite_234TL;

namespace DAL_234TL
{
    public class PermisoDAL_234TL : AbstractDAL_234TL<Permiso_234TL, int>
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog = Wilhjem_234TL; Integrated Security = True; Trust Server Certificate=True";

        public override void Eliminar(Permiso_234TL entity)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();
                string query = "DELETE FROM Permiso_234TL WHERE IdPermiso = @IdPermiso";

                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@IdPermiso", entity.IdPermiso);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar el permiso de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override void EliminarKey(int key)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();
                string query = "DELETE FROM Permiso_234TL WHERE IdPermiso = @IdPermiso";

                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@IdPermiso", key);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar el permiso de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }
        

        public override IList<Permiso_234TL> GetAll()
        {
            try
            {
                List<Permiso_234TL> permisos = new();

                using (SqlConnection conexion = new(connectionString))
                {
                    conexion.Open();
                    string query = "SELECT IdPermiso, Nombre FROM Permiso_234TL";

                    using (SqlCommand cmd = new(query, conexion))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                permisos.Add(new Permiso_234TL(reader["Nombre"].ToString())
                                {
                                    IdPermiso = Convert.ToInt32(reader["IdPermiso"])
                                });
                            }
                        }
                    }
                }

                return permisos;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener los permisos de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override Permiso_234TL GetbyPrimary(Permiso_234TL entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                using (SqlConnection conexion = new(connectionString))
                {
                    conexion.Open();
                    string query = "SELECT IdPermiso, Nombre FROM Permiso_234TL WHERE IdPermiso = @IdPermiso";

                    using (SqlCommand cmd = new(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdPermiso", entity.IdPermiso);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Permiso_234TL(reader["Nombre"].ToString())
                                {
                                    IdPermiso = Convert.ToInt32(reader["IdPermiso"])
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener el permiso de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }
    

        public override Permiso_234TL GetbyPrimaryKey(int id)
        {
            try
            {
                using (SqlConnection conexion = new(connectionString))
                {
                    conexion.Open();
                    string query = "SELECT IdPermiso, Nombre FROM Permiso_234TL WHERE IdPermiso = @IdPermiso";

                    using (SqlCommand cmd = new(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdPermiso", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Permiso_234TL(reader["Nombre"].ToString())
                                {
                                    IdPermiso = Convert.ToInt32(reader["IdPermiso"])
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener el permiso de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override void Guardar(Permiso_234TL entity)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                string query = "INSERT INTO Permiso_234TL (Nombre) VALUES (@Nombre); SELECT SCOPE_IDENTITY();";

                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                conexion.Open();
                entity.IdPermiso = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar el permiso en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }

        public override void Update(Permiso_234TL entity)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                string query = "UPDATE Permiso_234TL SET Nombre = @Nombre WHERE IdPermiso = @IdPermiso";

                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@IdPermiso", entity.IdPermiso);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar el permiso en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado.", ex);
            }
        }
    }
}
