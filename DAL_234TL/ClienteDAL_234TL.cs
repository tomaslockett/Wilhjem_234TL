using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_234TL;
using Microsoft.Data.SqlClient;

namespace DAL_234TL
{
    public class ClienteDAL_234TL : AbstractDAL_234TL<Cliente_234TL, string>
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=Wilhjem_234TL;Integrated Security=True;Trust Server Certificate=True";
        public override void Eliminar(Cliente_234TL entity)
        {
            EliminarKey(entity.Dni);    
        }

        public override void EliminarKey(string key)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = "DELETE FROM Cliente_234TL WHERE DNI = @DNI";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@DNI", key);

            comando.ExecuteNonQuery();
        }

        public override IList<Cliente_234TL> GetAll()
        {
            List<Cliente_234TL> lista = new();

            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = "SELECT DNI, Nombre, Apellido, Telefono FROM Cliente_234TL";

            using SqlCommand comando = new(query, conexion);
            using SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Cliente_234TL
                {
                    Dni = reader.GetString(0),
                    Nombre = reader.GetString(1),
                    Apellido = reader.GetString(2),
                    Telefono = reader.IsDBNull(3) ? null : reader.GetString(3)
                });
            }

            return lista;
        }

        public override Cliente_234TL GetbyPrimary(Cliente_234TL entity)
        {
            throw new NotImplementedException();
        }

        public override Cliente_234TL GetbyPrimaryKey(string id)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = "SELECT DNI, Nombre, Apellido, Telefono FROM Cliente_234TL WHERE DNI = @DNI";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@DNI", id);

            using SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente_234TL
                {
                    Dni = reader.GetString(0),
                    Nombre = reader.GetString(1),
                    Apellido = reader.GetString(2),
                    Telefono = reader.IsDBNull(3) ? null : reader.GetString(3)
                };
            }

            return null;
        }

        public override void Guardar(Cliente_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"INSERT INTO Cliente_234TL (DNI, Nombre, Apellido, Telefono)
                             VALUES (@DNI, @Nombre, @Apellido, @Telefono)";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@DNI", entity.Dni);
            comando.Parameters.AddWithValue("@Nombre", entity.Nombre);
            comando.Parameters.AddWithValue("@Apellido", entity.Apellido);
            comando.Parameters.AddWithValue("@Telefono", entity.Telefono ?? (object)DBNull.Value);

            comando.ExecuteNonQuery();
        }

        public override void Update(Cliente_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"UPDATE Cliente_234TL SET 
                                Nombre = @Nombre, 
                                Apellido = @Apellido, 
                                Telefono = @Telefono
                             WHERE DNI = @DNI";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@DNI", entity.Dni);
            comando.Parameters.AddWithValue("@Nombre", entity.Nombre);
            comando.Parameters.AddWithValue("@Apellido", entity.Apellido);
            comando.Parameters.AddWithValue("@Telefono", entity.Telefono ?? (object)DBNull.Value);

            comando.ExecuteNonQuery();
        }
    }
}
