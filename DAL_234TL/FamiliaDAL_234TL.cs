using Microsoft.Data.SqlClient;
using Servicios_234TL.Composite_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_234TL
{
    public class FamiliaDAL_234TL : AbstractDAL_234TL<Familia_234TL, int>
    {
        /// <summary>
        /// eeeeeeeeee el connectionstring tiene que tener esto si o si ;MultipleActiveResultSets=True"
        /// </summary>
        private readonly string connectionString = "Data Source=.;Initial Catalog=Wilhjem_234TL;Integrated Security=True;Trust Server Certificate=True;MultipleActiveResultSets=True";
        public override void Eliminar(Familia_234TL entity)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();

                BorrarHijosYPermisos(entity.IdFamilia, conexion);

                string query = "DELETE FROM Familia_234TL WHERE IdFamilia = @IdFamilia";
                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@IdFamilia", entity.IdFamilia);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar la familia de la base de datos.", ex);
            }
        }

        public bool FamiliaEstaAsignadaComoHijo(int idFamilia)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT COUNT(1) FROM FamiliaHijos_234TL WHERE IdHijo = @IdHijo";
                using (var cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdHijo", idFamilia);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public override void EliminarKey(int key)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();

                BorrarHijosYPermisos(key, conexion);

                string query = "DELETE FROM Familia_234TL WHERE IdFamilia = @IdFamilia";
                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@IdFamilia", key);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar la familia de la base de datos.", ex);
            }
        }

        public override IList<Familia_234TL> GetAll()
        {
            List<Familia_234TL> familias = new();

            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();

                string query = "SELECT IdFamilia, Nombre FROM Familia_234TL";
                using SqlCommand cmd = new(query, conexion);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Familia_234TL familia = new(reader["Nombre"].ToString())
                    {
                        IdFamilia = Convert.ToInt32(reader["IdFamilia"])
                    };
                    familias.Add(familia);
                }

                reader.Close();

                foreach (var familia in familias)
                {
                    CargarHijosYPermisos(familia, conexion);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener las familias de la base de datos.", ex);
            }

            return familias;
        }

        public override Familia_234TL GetbyPrimary(Familia_234TL entity)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();

                string query = "SELECT IdFamilia, Nombre FROM Familia_234TL WHERE IdFamilia = @IdFamilia";
                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@IdFamilia", entity.IdFamilia);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Familia_234TL familia = new(reader["Nombre"].ToString())
                    {
                        IdFamilia = Convert.ToInt32(reader["IdFamilia"])
                    };

                    reader.Close();
                    CargarHijosYPermisos(familia, conexion);

                    return familia;
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener la familia de la base de datos.", ex);
            }
        }

        public override Familia_234TL GetbyPrimaryKey(int id)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();

                string query = "SELECT IdFamilia, Nombre FROM Familia_234TL WHERE IdFamilia = @IdFamilia";
                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@IdFamilia", id);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Familia_234TL familia = new(reader["Nombre"].ToString())
                    {
                        IdFamilia = Convert.ToInt32(reader["IdFamilia"])
                    };

                    reader.Close();
                    CargarHijosYPermisos(familia, conexion);

                    return familia;
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener la familia de la base de datos.", ex);
            }
        }

        public override void Guardar(Familia_234TL entity)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();

                string query = "INSERT INTO Familia_234TL (Nombre) VALUES (@Nombre); SELECT SCOPE_IDENTITY();";
                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                entity.IdFamilia = Convert.ToInt32(cmd.ExecuteScalar());

                GuardarHijosYPermisos(entity, conexion);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar la familia en la base de datos.", ex);
            }
        }
        public bool NombreExiste(string nombre)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT COUNT(1) FROM Familia_234TL WHERE Nombre = @Nombre";
                using (var cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public override void Update(Familia_234TL entity)
        {
            try
            {
                using SqlConnection conexion = new(connectionString);
                conexion.Open();

                string query = "UPDATE Familia_234TL SET Nombre = @Nombre WHERE IdFamilia = @IdFamilia";
                using SqlCommand cmd = new(query, conexion);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@IdFamilia", entity.IdFamilia);
                cmd.ExecuteNonQuery();

                BorrarHijosYPermisos(entity.IdFamilia, conexion);
                GuardarHijosYPermisos(entity, conexion);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar la familia en la base de datos.", ex);
            }
        }

        private void CargarHijosYPermisos(Familia_234TL familia, SqlConnection conexion)
        {
            string hijosQuery = "SELECT f.IdFamilia, f.Nombre FROM FamiliaHijos_234TL fh JOIN Familia_234TL f ON fh.IdHijo = f.IdFamilia WHERE fh.IdPadre = @IdFamilia";
            using SqlCommand cmdHijos = new(hijosQuery, conexion);
            cmdHijos.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia);

            using SqlDataReader readerHijos = cmdHijos.ExecuteReader();
            while (readerHijos.Read())
            {
                Familia_234TL hijo = new(readerHijos["Nombre"].ToString())
                {
                    IdFamilia = Convert.ToInt32(readerHijos["IdFamilia"])
                };
                CargarHijosYPermisos(hijo, conexion);
                familia.AgregarHijo(hijo);
            }
            readerHijos.Close();

            string permisosQuery = "SELECT p.IdPermiso, p.Nombre FROM FamiliaPermiso_234TL fp JOIN Permiso_234TL p ON fp.IdPermiso = p.IdPermiso WHERE fp.IdFamilia = @IdFamilia";
            using SqlCommand cmdPermisos = new(permisosQuery, conexion);
            cmdPermisos.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia);

            using SqlDataReader readerPermisos = cmdPermisos.ExecuteReader();
            while (readerPermisos.Read())
            {
                Permiso_234TL permiso = new(readerPermisos["Nombre"].ToString())
                {
                    IdPermiso = Convert.ToInt32(readerPermisos["IdPermiso"])
                };
                familia.AgregarHijo(permiso);
            }
        }

        private void GuardarHijosYPermisos(Familia_234TL familia, SqlConnection conexion)
        {
            foreach (var hijo in familia.ObtenerHijos())
            {
                if (hijo is Familia_234TL familiaHijo)
                {
                    string insertHijo = "INSERT INTO FamiliaHijos_234TL (IdPadre, IdHijo) VALUES (@IdPadre, @IdHijo)";
                    using SqlCommand cmd = new(insertHijo, conexion);
                    cmd.Parameters.AddWithValue("@IdPadre", familia.IdFamilia);
                    cmd.Parameters.AddWithValue("@IdHijo", familiaHijo.IdFamilia);
                    cmd.ExecuteNonQuery();
                }
                else if (hijo is Permiso_234TL permiso)
                {
                    string insertPermiso = "INSERT INTO FamiliaPermiso_234TL (IdFamilia, IdPermiso) VALUES (@IdFamilia, @IdPermiso)";
                    using SqlCommand cmd = new(insertPermiso, conexion);
                    cmd.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia);
                    cmd.Parameters.AddWithValue("@IdPermiso", permiso.IdPermiso);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void BorrarHijosYPermisos(int idFamilia, SqlConnection conexion)
        {
            string deleteHijos = "DELETE FROM FamiliaHijos_234TL WHERE IdPadre = @IdFamilia";
            using SqlCommand cmdHijos = new(deleteHijos, conexion);
            cmdHijos.Parameters.AddWithValue("@IdFamilia", idFamilia);
            cmdHijos.ExecuteNonQuery();

            string deletePermisos = "DELETE FROM FamiliaPermiso_234TL WHERE IdFamilia = @IdFamilia";
            using SqlCommand cmdPermisos = new(deletePermisos, conexion);
            cmdPermisos.Parameters.AddWithValue("@IdFamilia", idFamilia);
            cmdPermisos.ExecuteNonQuery();
        }

        public bool PermisoEstaEnUso(int idPermiso)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    string query = "SELECT COUNT(1) FROM FamiliaPermiso_234TL WHERE IdPermiso = @IdPermiso";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdPermiso", idPermiso);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al verificar el uso del permiso en familias.", ex);
            }
        }

    }
}
