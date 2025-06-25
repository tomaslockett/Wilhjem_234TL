using BE_234TL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_234TL
{
    public class ComprobanteIngresoDAL_234TL : AbstractDAL_234TL<ComprobanteIngreso_234TL, string>
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=Wilhjem_234TL;Integrated Security=True;Trust Server Certificate=True";
        public override void Eliminar(ComprobanteIngreso_234TL entity)
        {
            EliminarKey(entity.NumeroIngreso);
        }

        public override void EliminarKey(string key)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = "DELETE FROM ComprobanteIngreso_234TL WHERE NumeroIngreso = @id";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@id", key);
            comando.ExecuteNonQuery();
        }

        public override IList<ComprobanteIngreso_234TL> GetAll()
        {
            List<ComprobanteIngreso_234TL> lista = new();

            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
        SELECT 
            CI.NumeroIngreso, CI.HoraIngreso,
            R.NumeroReparacion, R.Estado, R.DNICliente, R.NumeroSerieEquipo, R.DNITecnico, R.Cobrado, R.FacturaGenerada, R.ComprobanteGenerado
        FROM ComprobanteIngreso_234TL CI
        INNER JOIN Reparacion_234TL R ON CI.NumeroReparacion = R.NumeroReparacion";

            using SqlCommand comando = new(query, conexion);
            using SqlDataReader reader = comando.ExecuteReader();

            var clienteDAL = new ClienteDAL_234TL();
            var tecnicoDAL = new TecnicoDAL_234TL();
            var equipoDAL = new EquipoDAL_234TL();

            while (reader.Read())
            {
                int numeroReparacion = reader.GetInt32(2);
                string dniCliente = reader.GetString(4);
                string numeroSerieEquipo = reader.GetString(5);
                string dniTecnico = reader.GetString(6);

                Cliente_234TL cliente = clienteDAL.GetbyPrimaryKey(dniCliente);
                Tecnico_234TL tecnico = tecnicoDAL.GetbyPrimaryKey(dniTecnico);
                Equipo_234TL equipo = equipoDAL.GetbyPrimaryKey(numeroSerieEquipo);

                Reparacion_234TL reparacion = new Reparacion_234TL
                {
                    NumeroReparacion = numeroReparacion,
                    Estado = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Cliente = cliente,
                    Equipo = equipo,
                    Tecnico = tecnico,
                    Cobrado = reader.GetBoolean(7),
                    FacturaGenerada = reader.GetBoolean(8),
                    ComprobanteGenerado = reader.GetBoolean(9)
                };

                ComprobanteIngreso_234TL comprobante = new ComprobanteIngreso_234TL
                {
                    NumeroIngreso = reader.GetString(0),
                    HoraIngreso = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1),
                    Reparacion = reparacion,
                    Equipo = equipo 
                };

                lista.Add(comprobante);
            }

            return lista;
        }

        public override ComprobanteIngreso_234TL GetbyPrimary(ComprobanteIngreso_234TL entity)
        {
            return GetbyPrimaryKey(entity.NumeroIngreso);
        }

        public override ComprobanteIngreso_234TL GetbyPrimaryKey(string id)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
        SELECT NumeroIngreso, NumeroReparacion, NumeroSerieEquipo, HoraIngreso
        FROM ComprobanteIngreso_234TL
        WHERE NumeroIngreso = @id";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = comando.ExecuteReader();

            if (reader.Read())
            {
                return new ComprobanteIngreso_234TL
                {
                    NumeroIngreso = reader.GetString(0),
                    Reparacion = new Reparacion_234TL { NumeroReparacion = reader.GetInt32(1) }, 
                    Equipo = new Equipo_234TL { NumeroSerie = reader.GetString(2) },
                    HoraIngreso = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3)
                };
            }

            return null!;
        }

        public override void Guardar(ComprobanteIngreso_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
        INSERT INTO ComprobanteIngreso_234TL 
        (NumeroIngreso, NumeroReparacion, NumeroSerieEquipo, HoraIngreso)
        VALUES (@NumeroIngreso, @NumeroReparacion, @NumeroSerieEquipo, @HoraIngreso)";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@NumeroIngreso", entity.NumeroIngreso);
            comando.Parameters.AddWithValue("@NumeroReparacion", entity.Reparacion.NumeroReparacion);
            comando.Parameters.AddWithValue("@NumeroSerieEquipo", entity.Equipo.NumeroSerie);
            comando.Parameters.AddWithValue("@HoraIngreso", (object?)entity.HoraIngreso ?? DBNull.Value);

            comando.ExecuteNonQuery();
        }

        public override void Update(ComprobanteIngreso_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
        UPDATE ComprobanteIngreso_234TL
        SET 
            NumeroReparacion = @NumeroReparacion,
            NumeroSerieEquipo = @NumeroSerieEquipo,
            HoraIngreso = @HoraIngreso
        WHERE NumeroIngreso = @NumeroIngreso";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@NumeroIngreso", entity.NumeroIngreso);
            comando.Parameters.AddWithValue("@NumeroReparacion", entity.Reparacion.NumeroReparacion);
            comando.Parameters.AddWithValue("@NumeroSerieEquipo", entity.Equipo.NumeroSerie);
            comando.Parameters.AddWithValue("@HoraIngreso", (object?)entity.HoraIngreso ?? DBNull.Value);

            comando.ExecuteNonQuery();
        }
    }
}
