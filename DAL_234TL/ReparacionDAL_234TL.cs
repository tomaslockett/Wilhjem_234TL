using BE_234TL;
using Microsoft.Data.SqlClient;

namespace DAL_234TL
{
    public class ReparacionDAL_234TL : AbstractDAL_234TL<Reparacion_234TL, int>
    {
        //TrustServerCertificate=True";
        private readonly string connectionString = "Data Source=.;Initial Catalog=Wilhjem_234TL;Integrated Security=True;TrustServerCertificate=True";

        public override void Guardar(Reparacion_234TL entity)
        {
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = @"INSERT INTO Reparacion_234TL 
    (NumeroReparacion, Estado, DNICliente, NumeroSerieEquipo, DNITecnico, Cobrado, FacturaGenerada, ComprobanteGenerado)
    VALUES 
    (@NumeroReparacion, @Estado, @DNICliente, @NumeroSerieEquipo, @DNITecnico, @Cobrado, @FacturaGenerada, @ComprobanteGenerado)";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@NumeroReparacion", entity.NumeroReparacion);
            cmd.Parameters.AddWithValue("@Estado", entity.Estado);
            cmd.Parameters.AddWithValue("@DNICliente", entity.Cliente.Dni);
            cmd.Parameters.AddWithValue("@NumeroSerieEquipo", entity.Equipo.NumeroSerie);
            cmd.Parameters.AddWithValue("@DNITecnico", entity.Tecnico.Dni);
            cmd.Parameters.AddWithValue("@Cobrado", entity.Cobrado);
            cmd.Parameters.AddWithValue("@FacturaGenerada", entity.FacturaGenerada);
            cmd.Parameters.AddWithValue("@ComprobanteGenerado", entity.ComprobanteGenerado);

            cmd.ExecuteNonQuery();
        }

        public override void Update(Reparacion_234TL entity)
        {
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = @"UPDATE Reparacion_234TL SET 
    Estado = @Estado,
    DNICliente = @DNICliente,
    NumeroSerieEquipo = @NumeroSerieEquipo,
    DNITecnico = @DNITecnico,
    Cobrado = @Cobrado,
    FacturaGenerada = @FacturaGenerada,
    ComprobanteGenerado = @ComprobanteGenerado
    WHERE NumeroReparacion = @NumeroReparacion";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@Estado", entity.Estado);
            cmd.Parameters.AddWithValue("@DNICliente", entity.Cliente.Dni);
            cmd.Parameters.AddWithValue("@NumeroSerieEquipo", entity.Equipo.NumeroSerie);
            cmd.Parameters.AddWithValue("@DNITecnico", entity.Tecnico.Dni);
            cmd.Parameters.AddWithValue("@NumeroReparacion", entity.NumeroReparacion);
            cmd.Parameters.AddWithValue("@Cobrado", entity.Cobrado);
            cmd.Parameters.AddWithValue("@FacturaGenerada", entity.FacturaGenerada);
            cmd.Parameters.AddWithValue("@ComprobanteGenerado", entity.ComprobanteGenerado);

            cmd.ExecuteNonQuery();
        }

        public override void Eliminar(Reparacion_234TL entity)
        {
            EliminarKey(entity.NumeroReparacion);
        }

        public override void EliminarKey(int numeroReparacion)
        {
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = "DELETE FROM Reparacion_234TL WHERE NumeroReparacion = @NumeroReparacion";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@NumeroReparacion", numeroReparacion);

            cmd.ExecuteNonQuery();
        }

        public override Reparacion_234TL GetbyPrimary(Reparacion_234TL entity)
        {
            return GetbyPrimaryKey(entity.NumeroReparacion);
        }

        public override Reparacion_234TL GetbyPrimaryKey(int numeroReparacion)
        {
            using SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();

            string query = @"
        SELECT NumeroReparacion, Estado, Cobrado, FacturaGenerada, ComprobanteGenerado, 
               DNICliente, NumeroSerieEquipo, DNITecnico
        FROM Reparacion_234TL
        WHERE NumeroReparacion = @NumeroReparacion";

            using SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@NumeroReparacion", numeroReparacion);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string dniCliente = reader["DNICliente"].ToString();
                string numeroSerie = reader["NumeroSerieEquipo"].ToString();
                string dniTecnico = reader["DNITecnico"].ToString();

                var clienteDAL = new ClienteDAL_234TL();
                var equipoDAL = new EquipoDAL_234TL();
                var tecnicoDAL = new TecnicoDAL_234TL();

                var cliente = clienteDAL.GetbyPrimaryKey(dniCliente);
                var equipo = equipoDAL.GetbyPrimaryKey(numeroSerie);
                var tecnico = tecnicoDAL.GetbyPrimaryKey(dniTecnico);

                return new Reparacion_234TL
                {
                    NumeroReparacion = Convert.ToInt32(reader["NumeroReparacion"]),
                    Estado = reader["Estado"].ToString(),
                    Cobrado = Convert.ToBoolean(reader["Cobrado"]),
                    FacturaGenerada = Convert.ToBoolean(reader["FacturaGenerada"]),
                    ComprobanteGenerado = Convert.ToBoolean(reader["ComprobanteGenerado"]),
                    Cliente = cliente,
                    Equipo = equipo,
                    Tecnico = tecnico
                };
            }

            return null;
        }

        public override IList<Reparacion_234TL> GetAll()
        {
            List<Reparacion_234TL> lista = new();

            using SqlConnection conexion = new(connectionString);
            conexion.Open();

            string query = @"
        SELECT 
            NumeroReparacion, Estado, Cobrado, FacturaGenerada, ComprobanteGenerado,
            DNICliente, NumeroSerieEquipo, DNITecnico
        FROM Reparacion_234TL";

            using SqlCommand cmd = new(query, conexion);
            using SqlDataReader reader = cmd.ExecuteReader();

            var clienteDAL = new ClienteDAL_234TL();
            var equipoDAL = new EquipoDAL_234TL();
            var tecnicoDAL = new TecnicoDAL_234TL();

            Dictionary<string, Cliente_234TL> cacheClientes = new();
            Dictionary<string, Equipo_234TL> cacheEquipos = new();
            Dictionary<string, Tecnico_234TL> cacheTecnicos = new();

            while (reader.Read())
            {
                string dniCliente = reader["DNICliente"].ToString();
                string numeroSerie = reader["NumeroSerieEquipo"].ToString();
                string dniTecnico = reader["DNITecnico"].ToString();

                if (!cacheClientes.ContainsKey(dniCliente))
                    cacheClientes[dniCliente] = clienteDAL.GetbyPrimaryKey(dniCliente);
                var cliente = cacheClientes[dniCliente];

                if (!cacheEquipos.ContainsKey(numeroSerie))
                    cacheEquipos[numeroSerie] = equipoDAL.GetbyPrimaryKey(numeroSerie);
                var equipo = cacheEquipos[numeroSerie];

                if (!cacheTecnicos.ContainsKey(dniTecnico))
                    cacheTecnicos[dniTecnico] = tecnicoDAL.GetbyPrimaryKey(dniTecnico);
                var tecnico = cacheTecnicos[dniTecnico];

                Reparacion_234TL reparacion = new()
                {
                    NumeroReparacion = Convert.ToInt32(reader["NumeroReparacion"]),
                    Estado = reader["Estado"].ToString(),
                    Cobrado = Convert.ToBoolean(reader["Cobrado"]),
                    FacturaGenerada = Convert.ToBoolean(reader["FacturaGenerada"]),
                    ComprobanteGenerado = Convert.ToBoolean(reader["ComprobanteGenerado"]),
                    Cliente = cliente,
                    Equipo = equipo,
                    Tecnico = tecnico
                };

                lista.Add(reparacion);
            }

            return lista;
        }
    }
}

