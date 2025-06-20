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
        // esta el MultipleActiveResultSets=True";
        private readonly string connectionString = "Data Source=.;Initial Catalog=Wilhjem_234TL;Integrated Security=True;Trust Server Certificate=True;MultipleActiveResultSets=True";

        public override void Eliminar(Perfil_234TL entity)
        {
            EliminarKey(entity.IdPerfil);
        }

        public override void EliminarKey(int key)
        {
            using (SqlConnection conexion = new(connectionString))
            {
                conexion.Open();
                SqlTransaction transaccion = conexion.BeginTransaction();
                try
                {
                    string deletePermisos = "DELETE FROM PerfilPermiso_234TL WHERE IdPerfil = @IdPerfil";
                    string deleteFamilias = "DELETE FROM PerfilFamilia_234TL WHERE IdPerfil = @IdPerfil";
                    string deletePerfil = "DELETE FROM Perfil_234TL WHERE IdPerfil = @IdPerfil";

                    foreach (var query in new[] { deletePermisos, deleteFamilias, deletePerfil })
                    {
                        using (SqlCommand cmd = new(query, conexion, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@IdPerfil", key);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public override IList<Perfil_234TL> GetAll()
        {
            List<Perfil_234TL> perfiles = new();

            using (SqlConnection conexion = new(connectionString))
            {
                conexion.Open();

                string queryPerfiles = "SELECT IdPerfil, Nombre FROM Perfil_234TL";
                using (SqlCommand cmd = new(queryPerfiles, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        Perfil_234TL perfil = new(nombre) { IdPerfil = id };
                        perfiles.Add(perfil);
                    }
                }

                string queryPermisos = @"SELECT p.IdPerfil, pm.IdPermiso, pm.Nombre
                                          FROM PerfilPermiso_234TL p
                                          INNER JOIN Permiso_234TL pm ON p.IdPermiso = pm.IdPermiso";
                using (SqlCommand cmd = new(queryPermisos, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idPerfil = reader.GetInt32(0);
                        int idPermiso = reader.GetInt32(1);
                        string nombrePermiso = reader.GetString(2);

                        var permiso = new Permiso_234TL(nombrePermiso) { IdPermiso = idPermiso };
                        perfiles.First(p => p.IdPerfil == idPerfil).AgregarComponente(permiso);
                    }
                }

                string queryFamilias = @"SELECT pf.IdPerfil, f.IdFamilia, f.Nombre
                                          FROM PerfilFamilia_234TL pf
                                          INNER JOIN Familia_234TL f ON pf.IdFamilia = f.IdFamilia";
                Dictionary<int, Familia_234TL> cacheFamilias = new();
                using (SqlCommand cmd = new(queryFamilias, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idPerfil = reader.GetInt32(0);
                        int idFamilia = reader.GetInt32(1);
                        string nombreFamilia = reader.GetString(2);

                        Familia_234TL familia = new(nombreFamilia) { IdFamilia = idFamilia };
                        cacheFamilias[idFamilia] = familia;
                        perfiles.First(p => p.IdPerfil == idPerfil).AgregarComponente(familia);
                    }
                }

                string queryPermisosFamilias = @"SELECT ff.IdFamilia, p.IdPermiso, p.Nombre
                                                 FROM FamiliaPermiso_234TL ff
                                                 INNER JOIN Permiso_234TL p ON ff.IdPermiso = p.IdPermiso";
                using (SqlCommand cmd = new(queryPermisosFamilias, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idFamilia = reader.GetInt32(0);
                        int idPermiso = reader.GetInt32(1);
                        string nombre = reader.GetString(2);

                        if (cacheFamilias.TryGetValue(idFamilia, out var familia))
                        {
                            familia.AgregarHijo(new Permiso_234TL(nombre) { IdPermiso = idPermiso });
                        }
                    }
                }

                string queryJerarquia = "SELECT IdPadre, IdHijo FROM FamiliaHijos_234TL";
                using (SqlCommand cmd = new(queryJerarquia, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idPadre = reader.GetInt32(0);
                        int idHijo = reader.GetInt32(1);

                        if (cacheFamilias.TryGetValue(idPadre, out var padre) &&
                            cacheFamilias.TryGetValue(idHijo, out var hijo))
                        {
                            padre.AgregarHijo(hijo);
                        }
                    }
                }
            }

            return perfiles;
        }

        public override Perfil_234TL GetbyPrimary(Perfil_234TL entity)
        {
            return GetbyPrimaryKey(entity.IdPerfil);
        }

        public override Perfil_234TL GetbyPrimaryKey(int id)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            Perfil_234TL perfil = null;
            Dictionary<int, Familia_234TL> cacheFamilias = new();

            string queryPerfil = "SELECT Nombre FROM Perfil_234TL WHERE IdPerfil = @IdPerfil";
            using (SqlCommand cmd = new(queryPerfil, conexion))
            {
                cmd.Parameters.AddWithValue("@IdPerfil", id);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    perfil = new Perfil_234TL(reader.GetString(0)) { IdPerfil = id };
                }
            }

            if (perfil == null) return null;

            string queryPermisos = @"SELECT pm.IdPermiso, pm.Nombre
                                      FROM PerfilPermiso_234TL p
                                      INNER JOIN Permiso_234TL pm ON p.IdPermiso = pm.IdPermiso
                                      WHERE p.IdPerfil = @IdPerfil";
            using (SqlCommand cmd = new(queryPermisos, conexion))
            {
                cmd.Parameters.AddWithValue("@IdPerfil", id);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var permiso = new Permiso_234TL(reader.GetString(1)) { IdPermiso = reader.GetInt32(0) };
                    perfil.AgregarComponente(permiso);
                }
            }

            string queryFamilias = @"SELECT f.IdFamilia, f.Nombre
                                      FROM PerfilFamilia_234TL pf
                                      INNER JOIN Familia_234TL f ON pf.IdFamilia = f.IdFamilia
                                      WHERE pf.IdPerfil = @IdPerfil";
            using (SqlCommand cmd = new(queryFamilias, conexion))
            {
                cmd.Parameters.AddWithValue("@IdPerfil", id);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int idFamilia = reader.GetInt32(0);
                    Familia_234TL familia = new(reader.GetString(1)) { IdFamilia = idFamilia };
                    perfil.AgregarComponente(familia);
                    cacheFamilias[idFamilia] = familia;
                }
            }

            string queryPermisosFamilias = @"SELECT ff.IdFamilia, p.IdPermiso, p.Nombre
                                             FROM FamiliaPermiso_234TL ff
                                             INNER JOIN Permiso_234TL p ON ff.IdPermiso = p.IdPermiso";
            using (SqlCommand cmd = new(queryPermisosFamilias, conexion))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idFamilia = reader.GetInt32(0);
                    if (cacheFamilias.TryGetValue(idFamilia, out var familia))
                    {
                        familia.AgregarHijo(new Permiso_234TL(reader.GetString(2)) { IdPermiso = reader.GetInt32(1) });
                    }
                }
            }

            string queryJerarquia = "SELECT IdPadre, IdHijo FROM FamiliaHijos_234TL";
            using (SqlCommand cmd = new(queryJerarquia, conexion))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idPadre = reader.GetInt32(0);
                    int idHijo = reader.GetInt32(1);
                    if (cacheFamilias.TryGetValue(idPadre, out var padre) && cacheFamilias.TryGetValue(idHijo, out var hijo))
                    {
                        padre.AgregarHijo(hijo);
                    }
                }
            }

            return perfil;
        
        }

        public override void Guardar(Perfil_234TL entity)
        {
            using (SqlConnection conexion = new(connectionString))
            {
                conexion.Open();
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    string insertPerfil = "INSERT INTO Perfil_234TL (Nombre) OUTPUT INSERTED.IdPerfil VALUES (@Nombre)";
                    using (SqlCommand cmd = new(insertPerfil, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        entity.IdPerfil = (int)cmd.ExecuteScalar();
                    }

                    InsertarComponentes(entity, conexion, transaccion);

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        public override void Update(Perfil_234TL entity)
        {
            using (SqlConnection conexion = new(connectionString))
            {
                conexion.Open();
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    string updatePerfil = "UPDATE Perfil_234TL SET Nombre = @Nombre WHERE IdPerfil = @IdPerfil";
                    using (SqlCommand cmd = new(updatePerfil, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                        cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                        cmd.ExecuteNonQuery();
                    }

                    string deletePermisos = "DELETE FROM PerfilPermiso_234TL WHERE IdPerfil = @IdPerfil";
                    string deleteFamilias = "DELETE FROM PerfilFamilia_234TL WHERE IdPerfil = @IdPerfil";

                    foreach (var query in new[] { deletePermisos, deleteFamilias })
                    {
                        using (SqlCommand cmd = new(query, conexion, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    InsertarComponentes(entity, conexion, transaccion);

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        private void InsertarComponentes(Perfil_234TL entity, SqlConnection conexion, SqlTransaction transaccion)
        {
            foreach (var componente in entity.ObtenerComponentes())
            {
                if (componente is Permiso_234TL permiso)
                {
                    string insertPermiso = "INSERT INTO PerfilPermiso_234TL (IdPerfil, IdPermiso) VALUES (@IdPerfil, @IdPermiso)";
                    using (SqlCommand cmd = new(insertPermiso, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                        cmd.Parameters.AddWithValue("@IdPermiso", permiso.IdPermiso);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (componente is Familia_234TL familia)
                {
                    string insertFamilia = "INSERT INTO PerfilFamilia_234TL (IdPerfil, IdFamilia) VALUES (@IdPerfil, @IdFamilia)";
                    using (SqlCommand cmd = new(insertFamilia, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@IdPerfil", entity.IdPerfil);
                        cmd.Parameters.AddWithValue("@IdFamilia", familia.IdFamilia);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
