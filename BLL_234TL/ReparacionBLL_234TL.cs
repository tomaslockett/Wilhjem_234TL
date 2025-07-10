using BE_234TL;
using DAL_234TL;
using Servicios_234TL.Exception_234TL;
using System.Text.RegularExpressions;

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
                    throw new ValidacionesException_234TL("ClienteNulo", nameof(cliente));
                }

                if (equipo == null)
                {
                    throw new ValidacionesException_234TL("EquipoNulo", nameof(equipo));
                }

                if (string.IsNullOrWhiteSpace(cliente.Dni))
                {
                    throw new ValidacionesException_234TL("ClienteDniVacio", nameof(cliente));
                }

                if (string.IsNullOrWhiteSpace(equipo.NumeroSerie))
                {
                    throw new ValidacionesException_234TL("EquipoSerieVacio", nameof(equipo));
                }

                var todasLasReparaciones = GetAll();

                int CuentaReparaciones = todasLasReparaciones.Count(r => r.Cliente != null && r.Cliente.Dni == cliente.Dni);

                string DniParte = cliente.Dni.Length >= 3 ? cliente.Dni.Substring(cliente.Dni.Length - 3) : cliente.Dni.PadLeft(3, '0');

                string SerialParte = equipo.NumeroSerie.Length >= 3 ? equipo.NumeroSerie.Substring(0, 3) : equipo.NumeroSerie.PadRight(3, '0');

                string ContadorParte = (CuentaReparaciones + 1).ToString("000");

                return $"{DniParte}{SerialParte}{ContadorParte}";
            }
            catch (ValidacionesException_234TL)
            {
                throw; 
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
                    throw new ValidacionesException_234TL("ClienteNulo", nameof(cliente));
                }

                if (equipo == null)
                {
                    throw new ValidacionesException_234TL("EquipoNulo", nameof(equipo));
                }

                if (tecnico == null)
                {
                    throw new ValidacionesException_234TL("TecnicoNulo", nameof(tecnico));
                }

                if (string.IsNullOrWhiteSpace(estado))
                {
                    throw new ValidacionesException_234TL("EstadoVacio", nameof(estado));
                }

                string NumeroReparacion = GenerarNumeroReparacion(cliente, equipo);

                if (!int.TryParse(NumeroReparacion, out int numeroReparacionInt))
                {
                    throw new ValidacionesException_234TL("NumeroReparacionInvalido", "General");
                }

                var nuevareparacion = new Reparacion_234TL(numeroReparacionInt, estado, cliente, equipo, tecnico, false, false, false);

                Guardar(nuevareparacion);
            }
            catch (ValidacionesException_234TL)
            {
                throw; 
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
                throw new ValidacionesException_234TL("ReparacionNoEncontrada", nameof(numeroReparacion), numeroReparacion);
            }
            return reparacion.Cobrado;
        }

        public bool FacturaGenerada(int numeroReparacion)
        {
            var reparacion = GetAll().FirstOrDefault(r => r.NumeroReparacion == numeroReparacion);
            if (reparacion == null)
            {
                throw new ValidacionesException_234TL("ReparacionNoEncontrada", nameof(numeroReparacion), numeroReparacion);
            }
            return reparacion.FacturaGenerada;
        }

        public bool ComprobanteGenerado(int numeroReparacion)
        {
            var reparacion = GetAll().FirstOrDefault(r => r.NumeroReparacion == numeroReparacion);
            if (reparacion == null)
            {
                throw new ValidacionesException_234TL("ReparacionNoEncontrada", nameof(numeroReparacion), numeroReparacion);
            }
            return reparacion.ComprobanteGenerado;
        }

        public void CobrarDiagnostico(int numeroReparacion, string numeroTarjeta, string codigoSeguridad, string vencimiento)
        {
            if (!Regex.IsMatch(numeroTarjeta, @"^\d{16}$"))
            {
                throw new ValidacionesException_234TL("TarjetaInvalida", "NumeroTarjeta");
            }


            if (!Regex.IsMatch(codigoSeguridad, @"^\d{3,4}$"))
            {
                throw new ValidacionesException_234TL("CvvInvalido", "CodigoSeguridad");
            }

            if (!Regex.IsMatch(vencimiento, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                throw new ValidacionesException_234TL("VencimientoInvalido", "Vencimiento");
            }
            try
            {
                var partes = vencimiento.Split('/');
                int mes = int.Parse(partes[0]);
                int anio = 2000 + int.Parse(partes[1]); 

                var fechaVencimiento = new DateTime(anio, mes, 1).AddMonths(1).AddDays(-1);

                if (fechaVencimiento < DateTime.Today)
                {
                    throw new ValidacionesException_234TL("TarjetaVencida", "Vencimiento");
                }
            }
            catch
            {
                throw new ValidacionesException_234TL("VencimientoInvalido", "Vencimiento");
            }

            var reparacion = _repositorio.GetbyPrimaryKey(numeroReparacion);
            if (reparacion == null)
            {
                throw new ValidacionesException_234TL("ReparacionNoEncontrada", "General", numeroReparacion);
            }

            if (reparacion.Cobrado)
            {
                throw new ValidacionesException_234TL("DiagnosticoYaCobrado", "General", numeroReparacion);
            }

            reparacion.Cobrado = true;
            _repositorio.Update(reparacion);
        }

    }
}
