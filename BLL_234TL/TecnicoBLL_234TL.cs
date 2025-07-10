using BE_234TL;
using DAL_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using System.Text.RegularExpressions;

namespace BLL_234TL
{
    public class TecnicoBLL_234TL : AbstractaBLL_234TL<Tecnico_234TL, string>
    {
        private readonly ReparacionBLL_234TL _reparacionBLL;
        public TecnicoBLL_234TL() : base(new TecnicoDAL_234TL())
        {
            _reparacionBLL = new ReparacionBLL_234TL();
        }
        public void RegistrarNuevoTecnico(Tecnico_234TL tecnico)
        {
            if (!Regex.IsMatch(tecnico.Dni, @"^\d{7,8}$") || tecnico.Dni.All(c => c == '0'))
            {
                throw new ValidacionesException_234TL("ClienteDniInvalido", "DNI");
            }

            if (!Regex.IsMatch(tecnico.Nombre, @"^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]{2,40}$"))
            {
                throw new ValidacionesException_234TL("ClienteNombreInvalido", "Nombre");
            }

            if (!Regex.IsMatch(tecnico.Apellido, @"^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]{2,40}$"))
            {
                throw new ValidacionesException_234TL("ClienteApellidoInvalido", "Apellido");
            }

            if (string.IsNullOrWhiteSpace(tecnico.Especialidad))
            {
                throw new ValidacionesException_234TL("EspecialidadRequerida", "Especialidad");
            }

            if (this.GetbyPrimaryKey(tecnico.Dni) != null)
            {
                throw new InvalidOperationException(string.Format(IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales().Messages.Error["TecnicoYaExiste"], tecnico.Dni));
            }

            _repositorio.Guardar(tecnico);
        }

        public void ModificarTecnico(Tecnico_234TL tecnico)
        {

            if (!Regex.IsMatch(tecnico.Nombre, @"^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]{2,40}$"))
            {
                throw new ValidacionesException_234TL("El nombre es inválido.", "Nombre");
            }


            if (!Regex.IsMatch(tecnico.Apellido, @"^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]{2,40}$"))
            {
                throw new ValidacionesException_234TL("El apellido es inválido.", "Apellido");
            }


            if (string.IsNullOrWhiteSpace(tecnico.Especialidad))
            {
                throw new ValidacionesException_234TL("La especialidad no puede estar vacía.", "Especialidad");
            }


            _repositorio.Update(tecnico);
        }

        public void EliminarTecnico(Tecnico_234TL tecnico)
        {
            if (tecnico == null)
            {
                throw new ArgumentNullException(nameof(tecnico), "El técnico a eliminar no puede ser nulo.");
            }
            bool estaEnUso = _reparacionBLL.GetAll().Any(reparacion => reparacion.Tecnico.Dni == tecnico.Dni);

            if (estaEnUso)
            {
                throw new InvalidOperationException(IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales().Messages.Error["TecnicoConReparaciones"]);
            }
            _repositorio.Eliminar(tecnico);
        }
    }
}
