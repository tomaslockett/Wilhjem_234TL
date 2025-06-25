using BE_234TL;
using DAL_234TL;

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

        public bool RegistrarCliente(Cliente_234TL cliente)
        {
            if (ExisteCliente(cliente.Dni))
            {
                return false; // El cliente ya existe
            }
                

            Guardar(cliente);
            return true;
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

    }

}


