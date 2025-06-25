using BE_234TL;
using DAL_234TL;

namespace BLL_234TL
{
    public class ReparacionBLL_234TL : AbstractaBLL_234TL<Reparacion_234TL, int>
    {
        public ReparacionBLL_234TL() : base(new ReparacionDAL_234TL())
        {
        }

        public string GenerarNumeroReparacion(Cliente_234TL cliente, Equipo_234TL equipo)
        {
            try
            {
                if (cliente == null)
                {
                    throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
                }

                if (equipo == null)
                {
                    throw new ArgumentNullException(nameof(equipo), "El equipo no puede ser nulo.");
                }

                if (string.IsNullOrWhiteSpace(cliente.Dni))
                {
                    throw new ArgumentException("El DNI del cliente no puede estar vacío.", nameof(cliente));
                }

                if (string.IsNullOrWhiteSpace(equipo.NumeroSerie))
                {
                    throw new ArgumentException("El número de serie del equipo no puede estar vacío.", nameof(equipo));
                }

                var todasLasReparaciones = GetAll();

                int CuentaReparaciones = todasLasReparaciones.Count(r => r.Cliente != null && r.Cliente.Dni == cliente.Dni);

                string DniParte = cliente.Dni.Length >= 3 ? cliente.Dni.Substring(cliente.Dni.Length - 3) : cliente.Dni.PadLeft(3, '0');

                string SerialParte = equipo.NumeroSerie.Length >= 3 ? equipo.NumeroSerie.Substring(0, 3) : equipo.NumeroSerie.PadRight(3, '0');

                string ContadorParte = (CuentaReparaciones + 1).ToString("000");

                return $"{DniParte}{SerialParte}{ContadorParte}";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al generar el número de reparación.", ex);
            }
        }

        public void CrearReparacion(Cliente_234TL cliente, Equipo_234TL equipo, Tecnico_234TL tecnico, string estado)
        {
            try
            {
                if (cliente == null)
                {
                    throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
                }

                if (equipo == null)
                {
                    throw new ArgumentNullException(nameof(equipo), "El equipo no puede ser nulo.");
                }

                if (tecnico == null)
                {
                    throw new ArgumentNullException(nameof(tecnico), "El técnico no puede ser nulo.");
                }

                if (string.IsNullOrWhiteSpace(estado))
                {
                    throw new ArgumentException("El estado no puede estar vacío.", nameof(estado));
                }

                string NumeroReparacion = GenerarNumeroReparacion(cliente, equipo);

                if (!int.TryParse(NumeroReparacion, out int numeroReparacionInt))
                {
                    throw new InvalidOperationException("El número de reparación generado no es válido.");
                }

                var nuevareparacion = new Reparacion_234TL(numeroReparacionInt, estado, cliente, equipo, tecnico,false,false,false);

                Guardar(nuevareparacion);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear la reparación.", ex);
            }
        }
        public bool DiagnosticoCobrado(int numeroReparacion)
        {
            var reparacion = GetAll().FirstOrDefault(r => r.NumeroReparacion == numeroReparacion);
            if (reparacion == null)
            {
                throw new ArgumentException("No se encontró la reparación con el número especificado.", nameof(numeroReparacion));
            }
            return reparacion.Cobrado;
        }

        public bool FacturaGenerada(int numeroReparacion)
        {
            var reparacion = GetAll().FirstOrDefault(r => r.NumeroReparacion == numeroReparacion);
            if (reparacion == null)
            {
                throw new ArgumentException("No se encontró la reparación con el número especificado.", nameof(numeroReparacion));
            }
            return reparacion.FacturaGenerada;
        }

        public bool ComprobanteGenerado(int numeroReparacion)
        {
            var reparacion = GetAll().FirstOrDefault(r => r.NumeroReparacion == numeroReparacion);
            if (reparacion == null)
            {
                throw new ArgumentException("No se encontró la reparación con el número especificado.", nameof(numeroReparacion));
            }
            return reparacion.ComprobanteGenerado;
        }

        public void CobrarDiagnostico(int numeroReparacion)
        {
            var reparacion = GetbyPrimaryKey(numeroReparacion);
            if (reparacion == null)
            {
                throw new Exception("No se encontró la reparación.");
            }

            if (reparacion.Cobrado)
            {
                throw new Exception("Esta reparación ya fue cobrada.");
            }

            reparacion.Cobrado = true;
            Update(reparacion);
        }

    }
}
