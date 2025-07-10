using BE_234TL;
using DAL_234TL;
using Servicios_234TL;
using Servicios_234TL.Exception_234TL;
using System.Text.RegularExpressions;

namespace BLL_234TL
{
    public class ClienteBLL_234TL : AbstractaBLL_234TL<Cliente_234TL, string>
    {
        public ClienteBLL_234TL() : base(new ClienteDAL_234TL())
        {
        }

        public bool ExisteCliente(string dni)
        {
            return GetAll().Any(c => c.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase));
        }

        public void RegistrarNuevoCliente(Cliente_234TL cliente)
        {
            if (!Regex.IsMatch(cliente.Dni, @"^\d{7,8}$") || cliente.Dni.All(c => c == '0'))
            {
                throw new ValidacionesException_234TL("ClienteDniInvalido", "DNI");
            }

            if (!Regex.IsMatch(cliente.Nombre, @"^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]{2,40}$"))
            {
                throw new ValidacionesException_234TL("ClienteNombreInvalido", "Nombre");
            }

            if (!Regex.IsMatch(cliente.Apellido, @"^[A-ZÁÉÍÓÚÑa-záéíóúñ\s]{2,40}$"))
            {
                throw new ValidacionesException_234TL("ClienteApellidoInvalido", "Apellido");
            }

            if (!Regex.IsMatch(cliente.Telefono, @"^\d{6,15}$"))
            {
                throw new ValidacionesException_234TL("ClienteTelefonoInvalido", "Telefono");
            }

            if (this.GetbyPrimaryKey(cliente.Dni) != null)
            {
                throw new ValidacionesException_234TL("ClienteYaExiste", "DNI", cliente.Dni);
            }
            cliente.Telefono = Encryptador_234TL.EncriptarAES(cliente.Telefono);

            _repositorio.Guardar(cliente);
        }

        public bool EliminarCliente(string dni)
        {
            if (!ExisteCliente(dni))
            {
                return false;
            }

            var cliente = GetAll().FirstOrDefault(c => c.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase));
            if (cliente != null)
            {
                Eliminar(cliente);
            }
            return true;
        }

        public override List<Cliente_234TL> GetAll()
        {
            var clientesEncriptados = base.GetAll();
            foreach (var cliente in clientesEncriptados)
            {
                try
                {
                    cliente.Telefono = Encryptador_234TL.DesencriptarAES(cliente.Telefono);
                }
                catch
                {
                    cliente.Telefono = cliente.Telefono + " (Error al desencriptar)";
                }
            } 
            return (List<Cliente_234TL>)clientesEncriptados;  
        }
           
    }

}


