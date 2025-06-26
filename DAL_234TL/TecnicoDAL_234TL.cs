using BE_234TL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_234TL
{
    public class TecnicoDAL_234TL : AbstractDAL_234TL<Tecnico_234TL, string>
    {
        //TrustServerCertificate=True";
        private readonly string connectionString = "Data Source=.;Initial Catalog=Wilhjem_234TL;Integrated Security=True;TrustServerCertificate=True";

        public override void Guardar(Tecnico_234TL entity)
        {
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = @"INSERT INTO Tecnico_234TL 
                            (DNI, Nombre, Apellido, Telefono, Especialidad, Disponible)
                             VALUES 
                            (@DNI, @Nombre, @Apellido, @Telefono, @Especialidad, @Disponible)";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@DNI", entity.Dni);
            cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", entity.Apellido);
            cmd.Parameters.AddWithValue("@Telefono", entity.Telefono);
            cmd.Parameters.AddWithValue("@Especialidad", entity.Especialidad);
            cmd.Parameters.AddWithValue("@Disponible", entity.Disponible);

            cmd.ExecuteNonQuery();
        }

        public override void Update(Tecnico_234TL entity)
        {
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = @"UPDATE Tecnico_234TL SET 
                                Nombre = @Nombre,
                                Apellido = @Apellido,
                                Telefono = @Telefono,
                                Especialidad = @Especialidad,
                                Disponible = @Disponible
                             WHERE DNI = @DNI";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", entity.Apellido);
            cmd.Parameters.AddWithValue("@Telefono", entity.Telefono);
            cmd.Parameters.AddWithValue("@Especialidad", entity.Especialidad);
            cmd.Parameters.AddWithValue("@Disponible", entity.Disponible);
            cmd.Parameters.AddWithValue("@DNI", entity.Dni);

            cmd.ExecuteNonQuery();
        }

        public override void Eliminar(Tecnico_234TL entity)
        {
            EliminarKey(entity.Dni);
        }

        public override void EliminarKey(string dni)
        {
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = "DELETE FROM Tecnico_234TL WHERE DNI = @DNI";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@DNI", dni);
            cmd.ExecuteNonQuery();
        }

        public override Tecnico_234TL GetbyPrimary(Tecnico_234TL entity)
        {
            return GetbyPrimaryKey(entity.Dni);
        }

        public override Tecnico_234TL GetbyPrimaryKey(string dni)
        {
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = "SELECT * FROM Tecnico_234TL WHERE DNI = @DNI";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@DNI", dni);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Tecnico_234TL
                {
                    Dni = reader["DNI"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    Apellido = reader["Apellido"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    Especialidad = reader["Especialidad"].ToString(),
                    Disponible = Convert.ToBoolean(reader["Disponible"])
                };
            }

            return null;
        }

        public override IList<Tecnico_234TL> GetAll()
        {
            var lista = new List<Tecnico_234TL>();

            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = "SELECT * FROM Tecnico_234TL";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var tecnico = new Tecnico_234TL
                {
                    Dni = reader["DNI"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    Apellido = reader["Apellido"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    Especialidad = reader["Especialidad"].ToString(),
                    Disponible = Convert.ToBoolean(reader["Disponible"])
                };
                lista.Add(tecnico);
            }

            return lista;
        }
    }
}

