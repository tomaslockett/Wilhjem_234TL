using Microsoft.Data.SqlClient;
using Servicios_234TL.Composite_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_234TL
{
    public class PerfilDAL_234TL : AbstractDAL_234TL<Perfil_234TL, int>
    {
        private readonly string connectionString = null;

        public override void Eliminar(Perfil_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();
            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                BorrarFamiliasYPermisos(entity.IdPerfil, conexion, transaccion);

                string deletePerfil = "DELETE FROM Perfiles_234TL WHERE IdPerfil = @IdPerfil";
                using SqlCommand cmd = new(deletePerfil, conexion, transaccion);
                cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public override void EliminarKey(int key)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();
            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                BorrarFamiliasYPermisos(key, conexion, transaccion);

                string deletePerfil = "DELETE FROM Perfiles_234TL WHERE IdPerfil = @IdPerfil";
                using SqlCommand cmd = new(deletePerfil, conexion, transaccion);
                cmd.Parameters.AddWithValue("@IdPerfil", key);
                cmd.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public override IList<Perfil_234TL> GetAll()
        {
            List<Perfil_234TL> perfiles = new();
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = "SELECT IdPerfil FROM Perfiles_234TL";
            using SqlCommand cmd = new(query, conexion);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idPerfil = Convert.ToInt32(reader["IdPerfil"]);
                var perfil = ObtenerPerfilConFamiliasYPermisos(idPerfil, conexion);
                if (perfil != null)
                    perfiles.Add(perfil);
            }
            return perfiles;
        }

        public override Perfil_234TL GetbyPrimary(Perfil_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            Perfil_234TL perfil = null;
            Dictionary<int, Familia_234TL> cacheFamilias = new();

            string queryPerfil = "SELECT Nombre FROM Perfiles_234TL WHERE IdPerfil = @IdPerfil";
            using (SqlCommand cmd = new(queryPerfil, conexion))
            {
                cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    perfil = new Perfil_234TL(reader["Nombre"].ToString())
                    {
                        IdPerfil = entity.IdPerfil
                    };
                }
            }

            if (perfil == null)
                return null;

            string queryPermisos = "SELECT pm.IdPermiso, pm.Nombre FROM PerfilPermiso_234TL p INNER JOIN Permisos_234TL pm ON p.IdPermiso = pm.IdPermiso WHERE p.IdPerfil = @IdPerfil";
            using (SqlCommand cmd = new(queryPermisos, conexion))
            {
                cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var permiso = new Permiso_234TL(reader["Nombre"].ToString())
                    {
                        IdPermiso = Convert.ToInt32(reader["IdPermiso"])
                    };
                    perfil.AgregarComponente(permiso);
                }
            }

            string queryFamilias = "SELECT f.IdFamilia, f.Nombre FROM PerfilFamilia_234TL pf INNER JOIN Familias_234TL f ON pf.IdFamilia = f.IdFamilia WHERE pf.IdPerfil = @IdPerfil";
            using (SqlCommand cmd = new(queryFamilias, conexion))
            {
                cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int idFamilia = Convert.ToInt32(reader["IdFamilia"]);
                    Familia_234TL familia = new(reader["Nombre"].ToString())
                    {
                        IdFamilia = idFamilia
                    };
                    perfil.AgregarComponente(familia);
                    cacheFamilias[idFamilia] = familia;
                }
            }

            string queryPermisosFamilias = "SELECT ff.IdFamilia, p.IdPermiso, p.Nombre FROM FamiliaPermisos_234TL ff INNER JOIN Permisos_234TL p ON ff.IdPermiso = p.IdPermiso";
            using (SqlCommand cmd = new(queryPermisosFamilias, conexion))
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int idFamilia = Convert.ToInt32(reader["IdFamilia"]);
                    if (cacheFamilias.TryGetValue(idFamilia, out var familia))
                    {
                        familia.AgregarHijo(new Permiso_234TL(reader["Nombre"].ToString())
                        {
                            IdPermiso = Convert.ToInt32(reader["IdPermiso"])
                        });
                    }
                }
            }

            string queryJerarquia = "SELECT IdPadre, IdHijo FROM FamiliaHijos_234TL";
            using (SqlCommand cmd = new(queryJerarquia, conexion))
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int idPadre = Convert.ToInt32(reader["IdPadre"]);
                    int idHijo = Convert.ToInt32(reader["IdHijo"]);

                    if (cacheFamilias.TryGetValue(idPadre, out var padre) && cacheFamilias.TryGetValue(idHijo, out var hijo))
                    {
                        padre.AgregarHijo(hijo);
                    }
                }
            }

            return perfil;
        }

        public override Perfil_234TL GetbyPrimaryKey(int id)
        {
            return GetbyPrimary(new Perfil_234TL("") { IdPerfil = id });
        }

        public override void Guardar(Perfil_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();
            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                string insertPerfil = "INSERT INTO Perfil_234TL (Nombre) OUTPUT INSERTED.IdPerfil VALUES (@Nombre)";
                using SqlCommand cmd = new(insertPerfil, conexion, transaccion);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                entity.IdPerfil = (int)cmd.ExecuteScalar();

                GuardarFamiliasYPermisos(entity, conexion, transaccion);

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public override void Update(Perfil_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();
            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                string updatePerfil = "UPDATE Perfiles_234TL SET Nombre = @Nombre WHERE IdPerfil = @IdPerfil";
                using SqlCommand cmd = new(updatePerfil, conexion, transaccion);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                cmd.ExecuteNonQuery();

                BorrarFamiliasYPermisos(entity.IdPerfil, conexion, transaccion);
                GuardarFamiliasYPermisos(entity, conexion, transaccion);

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        private void GuardarFamiliasYPermisos(Perfil_234TL perfil, SqlConnection conexion, SqlTransaction transaccion)
        {
            foreach (var componente in perfil.ObtenerComponentes())
            {
                if (componente is Permiso_234TL p)
                {
                    string insertPermiso = "INSERT INTO PerfilPermiso_234TL (IdPerfil, IdPermiso) VALUES (@IdPerfil, @IdPermiso)";
                    using SqlCommand cmd = new(insertPermiso, conexion, transaccion);
                    cmd.Parameters.AddWithValue("@IdPerfil", perfil.IdPerfil);
                    cmd.Parameters.AddWithValue("@IdPermiso", p.IdPermiso);
                    cmd.ExecuteNonQuery();
                }
                else if (componente is Familia_234TL f)
                {
                    string insertFamilia = "INSERT INTO PerfilFamilia_234TL (IdPerfil, IdFamilia) VALUES (@IdPerfil, @IdFamilia)";
                    using SqlCommand cmd = new(insertFamilia, conexion, transaccion);
                    cmd.Parameters.AddWithValue("@IdPerfil", perfil.IdPerfil);
                    cmd.Parameters.AddWithValue("@IdFamilia", f.IdFamilia);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void BorrarFamiliasYPermisos(int idPerfil, SqlConnection conexion, SqlTransaction transaccion)
        {
            string deletePermisos = "DELETE FROM PerfilPermiso_234TL WHERE IdPerfil = @IdPerfil";
            using SqlCommand cmdPerm = new(deletePermisos, conexion, transaccion);
            cmdPerm.Parameters.AddWithValue("@IdPerfil", idPerfil);
            cmdPerm.ExecuteNonQuery();

            string deleteFamilias = "DELETE FROM PerfilFamilia_234TL WHERE IdPerfil = @IdPerfil";
            using SqlCommand cmdFam = new(deleteFamilias, conexion, transaccion);
            cmdFam.Parameters.AddWithValue("@IdPerfil", idPerfil);
            cmdFam.ExecuteNonQuery();
        }

        private Perfil_234TL ObtenerPerfilConFamiliasYPermisos(int idPerfil, SqlConnection conexionExistente = null)
        {
            bool cerrarConexion = false;
            SqlConnection conexion = conexionExistente;

            if (conexion == null)
            {
                conexion = new SqlConnection(connectionString);
                conexion.Open();
                cerrarConexion = true;
            }

            try
            {
                Perfil_234TL perfil = null;
                Dictionary<int, Familia_234TL> cacheFamilias = new();

                string queryPerfil = "SELECT Nombre FROM Perfiles_234TL WHERE IdPerfil = @IdPerfil";
                using (SqlCommand cmd = new(queryPerfil, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdPerfil", idPerfil);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        perfil = new Perfil_234TL(reader["Nombre"].ToString()) { IdPerfil = idPerfil };
                    }
                }

                if (perfil == null) return null;

                string queryPermisos = "SELECT pm.IdPermiso, pm.Nombre FROM PerfilPermiso_234TL p INNER JOIN Permisos_234TL pm ON p.IdPermiso = pm.IdPermiso WHERE p.IdPerfil = @IdPerfil";
                using (SqlCommand cmd = new(queryPermisos, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdPerfil", idPerfil);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var permiso = new Permiso_234TL(reader["Nombre"].ToString()) { IdPermiso = Convert.ToInt32(reader["IdPermiso"]) };
                        perfil.AgregarComponente(permiso);
                    }
                }

                string queryFamilias = "SELECT f.IdFamilia, f.Nombre FROM PerfilFamilia_234TL pf INNER JOIN Familias_234TL f ON pf.IdFamilia = f.IdFamilia WHERE pf.IdPerfil = @IdPerfil";
                using (SqlCommand cmd = new(queryFamilias, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdPerfil", idPerfil);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int idFamilia = Convert.ToInt32(reader["IdFamilia"]);
                        Familia_234TL familia = new(reader["Nombre"].ToString()) { IdFamilia = idFamilia };
                        perfil.AgregarComponente(familia);
                        cacheFamilias[idFamilia] = familia;
                    }
                }

                string queryPermisosFamilias = "SELECT ff.IdFamilia, p.IdPermiso, p.Nombre FROM FamiliaPermisos_234TL ff INNER JOIN Permisos_234TL p ON ff.IdPermiso = p.IdPermiso";
                using (SqlCommand cmd = new(queryPermisosFamilias, conexion))
                {
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int idFamilia = Convert.ToInt32(reader["IdFamilia"]);
                        if (cacheFamilias.TryGetValue(idFamilia, out var familia))
                        {
                            familia.AgregarHijo(new Permiso_234TL(reader["Nombre"].ToString()) { IdPermiso = Convert.ToInt32(reader["IdPermiso"]) });
                        }
                    }
                }

                string queryJerarquia = "SELECT IdPadre, IdHijo FROM FamiliaHijos_234TL";
                using (SqlCommand cmd = new(queryJerarquia, conexion))
                {
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int idPadre = Convert.ToInt32(reader["IdPadre"]);
                        int idHijo = Convert.ToInt32(reader["IdHijo"]);
                        if (cacheFamilias.TryGetValue(idPadre, out var padre) && cacheFamilias.TryGetValue(idHijo, out var hijo))
                        {
                            padre.AgregarHijo(hijo);
                        }
                    }
                }

                return perfil;
            }
            finally
            {
                if (cerrarConexion && conexion != null)
                    conexion.Close();
            }
        }
    }
}
