using BE_234TL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_234TL
{
    public class FacturaDAL_234TL : AbstractDAL_234TL<Factura_234TL, int>
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=Wilhjem_234TL;Integrated Security=True;Trust Server Certificate=True";

        public override void Eliminar(Factura_234TL entity)
        {
            EliminarKey(int.Parse(entity.NumeroFactura));
        }

        public override void EliminarKey(int key)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"DELETE FROM Factura_234TL WHERE NumeroFactura = @NumeroFactura";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@NumeroFactura", key.ToString());

            comando.ExecuteNonQuery();
        }



        public override IList<Factura_234TL> GetAll()
        {
            var lista = new List<Factura_234TL>();
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
                SELECT 
                    f.NumeroFactura, f.Monto, f.NumeroReparacion, f.DNICliente
                FROM 
                    Factura_234TL f";

            using SqlCommand comando = new(query, conexion);
            using SqlDataReader reader = comando.ExecuteReader();

            var reparacionDal = new ReparacionDAL_234TL();
            var clienteDal = new ClienteDAL_234TL();

            while (reader.Read())
            {
                int numeroReparacion = Convert.ToInt32(reader["NumeroReparacion"]);
                string dniCliente = reader["DNICliente"].ToString();

                var reparacion = reparacionDal.GetbyPrimaryKey(numeroReparacion);
                var cliente = clienteDal.GetbyPrimaryKey(dniCliente);

                var factura = new Factura_234TL
                {
                    NumeroFactura = reader["NumeroFactura"].ToString(),
                    Total = Convert.ToDecimal(reader["Monto"]),
                    Reparacion = reparacion,
                    Cliente = cliente
                };

                lista.Add(factura);
            }

            return lista;
        }

        public override Factura_234TL GetbyPrimary(Factura_234TL entity)
        {
            return GetbyPrimaryKey(int.Parse(entity.NumeroFactura));
        }

        public override Factura_234TL GetbyPrimaryKey(int numeroFactura)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
    SELECT 
        f.NumeroFactura, f.Monto,
        r.NumeroReparacion, r.Estado AS EstadoReparacion, r.Cobrado, r.FacturaGenerada, r.ComprobanteGenerado,
        c.DNI AS DNICli, c.Nombre AS NombreCli, c.Apellido AS ApellidoCli, c.Telefono AS TelefonoCli,
        e.NumeroSerie, e.Marca, e.Modelo, e.Estado AS EstadoEquipo,
        t.DNI AS DNITec, t.Nombre AS NombreTec, t.Apellido AS ApellidoTec, t.Telefono AS TelefonoTec, t.Especialidad, t.Disponible
    FROM 
        Factura_234TL f
    JOIN 
        Reparacion_234TL r ON f.NumeroReparacion = r.NumeroReparacion
    JOIN 
        Cliente_234TL c ON f.DNICliente = c.DNI
    JOIN 
        Equipo_234TL e ON r.NumeroSerieEquipo = e.NumeroSerie
    JOIN 
        Tecnico_234TL t ON r.DNITecnico = t.DNI
    WHERE 
        f.NumeroFactura = @NumeroFactura";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@NumeroFactura", numeroFactura);

            using SqlDataReader reader = comando.ExecuteReader();

            if (reader.Read())
            {
                var cliente = new Cliente_234TL
                {
                    Dni = reader["DNICli"].ToString(),
                    Nombre = reader["NombreCli"].ToString(),
                    Apellido = reader["ApellidoCli"].ToString(),
                    Telefono = reader["TelefonoCli"].ToString()
                };

                var equipo = new Equipo_234TL
                {
                    NumeroSerie = reader["NumeroSerie"].ToString(),
                    Marca = reader["Marca"].ToString(),
                    Modelo = reader["Modelo"].ToString(),
                    Estado = reader["EstadoEquipo"].ToString()
                };

                var tecnico = new Tecnico_234TL
                {
                    Dni = reader["DNITec"].ToString(),
                    Nombre = reader["NombreTec"].ToString(),
                    Apellido = reader["ApellidoTec"].ToString(),
                    Telefono = reader["TelefonoTec"].ToString(),
                    Especialidad = reader["Especialidad"].ToString(),
                    Disponible = Convert.ToBoolean(reader["Disponible"])
                };

                var reparacion = new Reparacion_234TL
                {
                    NumeroReparacion = Convert.ToInt32(reader["NumeroReparacion"]),
                    Estado = reader["EstadoReparacion"].ToString(),
                    Cliente = cliente,
                    Equipo = equipo,
                    Tecnico = tecnico,
                    Cobrado = Convert.ToBoolean(reader["Cobrado"]),
                    FacturaGenerada = Convert.ToBoolean(reader["FacturaGenerada"]),
                    ComprobanteGenerado = Convert.ToBoolean(reader["ComprobanteGenerado"])
                };

                return new Factura_234TL
                {
                    NumeroFactura = reader["NumeroFactura"].ToString(),
                    Total = Convert.ToDecimal(reader["Monto"]),
                    Reparacion = reparacion,
                    Cliente = cliente
                };
            }

            return null;
        }

        public override void Guardar(Factura_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
            INSERT INTO Factura_234TL (NumeroFactura, NumeroReparacion, DNICliente, Monto)
            VALUES (@NumeroFactura, @NumeroReparacion, @DNICliente, @Monto)";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@NumeroFactura", entity.NumeroFactura);
            comando.Parameters.AddWithValue("@NumeroReparacion", entity.Reparacion.NumeroReparacion);
            comando.Parameters.AddWithValue("@DNICliente", entity.Cliente.Dni);
            comando.Parameters.AddWithValue("@Monto", entity.Total);

            comando.ExecuteNonQuery();
        }

        public override void Update(Factura_234TL entity)
        {
            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
            UPDATE Factura_234TL
            SET 
                NumeroReparacion = @NumeroReparacion,
                DNICliente = @DNICliente,
                Monto = @Monto
            WHERE NumeroFactura = @NumeroFactura";

            using SqlCommand comando = new(query, conexion);
            comando.Parameters.AddWithValue("@NumeroFactura", entity.NumeroFactura);
            comando.Parameters.AddWithValue("@NumeroReparacion", entity.Reparacion.NumeroReparacion);
            comando.Parameters.AddWithValue("@DNICliente", entity.Cliente.Dni);
            comando.Parameters.AddWithValue("@Monto", entity.Total);

            comando.ExecuteNonQuery();
        }
    }
}
